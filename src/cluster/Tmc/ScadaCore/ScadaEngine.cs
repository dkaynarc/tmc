using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Scada.Core.Sequencing;
using Tmc.Scada.Core.Reporting;
using System.ServiceModel;
using TmcData;
using Tmc.Common;
using Tmc.Robotics;

namespace Tmc.Scada.Core
{
    public enum ScadaStatus
    {
        Offline,
        Online
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ScadaEngine : IScada
    {
        public string Name { get; set; }
        public bool IsInitialized { get; private set; }
        public ScadaStatus Status { get; private set; }
        public ClusterConfig ClusterConfig { get; set; }
        public OrderConsumer OrderConsumer { get; set; }
        public TabletMagazine TabletMagazine { get; set; }
        public HardwareMonitor HardwareMonitor { get; set; }
        public EnvironmentMonitor EnvironmentMonitor { get; set; }
        private ISequencer _sequencer;
        private EmergencyStopMonitor _eStopMonitor;

        public ScadaEngine()
        {
            this.Status = ScadaStatus.Offline;   
        }

        private void StartAllTimers()
        {
            this.OrderConsumer.Start();
            this.HardwareMonitor.Start();
            this.EnvironmentMonitor.Start();
            //this._eStopMonitor.Start();
        }

        public bool Initialize()
        {
            return this.Initialize(@"./Configuration/ClusterConfig.xml");
        }

        public bool Initialize(string configFile)
        {
            var success = this.Create(configFile);
            if (success)
            {
                this.StartSequencing();
            }
            this.IsInitialized = success;
            return success;
        }

        public void Start()
        {
            if (_sequencer.TransitionLogger.CurrentState == State.Shutdown || 
                _sequencer.TransitionLogger.CurrentState == State.Startup)
            {
                this._sequencer.FireStartTrigger();
                Logger.Instance.Write(new LogEntry("[SCADA] Cluster operation started", LogType.Message));
                this.Status = ScadaStatus.Online;
            }
        }

        public void Stop()
        {
            if (this.IsInitialized)
            {
                if ((_sequencer.TransitionLogger.CurrentState != State.Stopped) || (_sequencer.TransitionLogger.CurrentState != State.Shutdown))
                {
                    this._sequencer.FireStopTrigger();
                    Logger.Instance.Write(new LogEntry("[SCADA] Cluster operation stopped", LogType.Message));
                    this.Status = ScadaStatus.Offline;
                }
            }
        }

        public void Resume()
        {
            if (this.IsInitialized)
            {
                if (_sequencer.TransitionLogger.CurrentState == State.Stopped)
                {
                    this._sequencer.FireResumeTrigger();
                    Logger.Instance.Write(new LogEntry("[SCADA] Cluster operation resumed", LogType.Message));
                }
            }
        }

        public void Shutdown()
        {
            if (this.IsInitialized)
            {
                if (_sequencer.TransitionLogger.CurrentState != State.Shutdown)
                {
                    this._sequencer.FireShutdownTrigger();
                    Logger.Instance.Write(new LogEntry("[SCADA] Cluster operation shut down", LogType.Message));
                }
            }
        }

        public void EmergencyStop()
        {
            if (this.IsInitialized)
            {
                Logger.Instance.Write(new LogEntry("[SCADA] Emergency stop commencing", LogType.Warning));
                this.Status = ScadaStatus.Offline;
                _sequencer.FireStopTrigger();
                _sequencer.StopSequencing();
                foreach (var hardware in ClusterConfig.GetAllHardware())
                {
                    Logger.Instance.Write(new LogEntry(String.Format("[SCADA] Emergency Stopping {0}", hardware.Name), LogType.Warning));
                    hardware.EmergencyStop();
                }
                Logger.Instance.Write(new LogEntry("[SCADA] Emergency stop complete", LogType.Warning));
            }
        }

        public void SetOperatingMode(OperationMode mode)
        {
            this._sequencer.Mode = mode;
        }

        public IDictionary<string, HardwareStatus> GetLastHardwareStatuses()
        {
            IDictionary<string, HardwareStatus> statuses = new Dictionary<string, HardwareStatus>();
            if (this.IsInitialized)
            {
                statuses = this.HardwareMonitor.PreviousHardwareStatuses;
            }
            return statuses;
        }

        public IList<string> GetAllHardwareNames()
        {
            IList<string> hardware = new List<string>();
            if (this.IsInitialized)
            {
                hardware = this.ClusterConfig.GetAllHardware().Select(x => x.Name).ToList();
            }
            return hardware;
        }

        public void SetSpeed(int speed)
        {
            if (this.IsInitialized)
            {
                foreach (IRobot robot in ClusterConfig.Robots.Values.ToList())
                {
                    robot.SetSpeed(speed);
                }
            }
        }

        private void StartSequencing()
        {
            this._sequencer.StartSequencing();
            Logger.Instance.Write(new LogEntry("TMC control system initialised", LogType.Message));
        }
        private bool Create(string configFile)
        {
            try
            {
                this.ClusterConfig = ClusterFactory.Instance.CreateCluster(configFile);

                this.EnvironmentMonitor = new EnvironmentMonitor(this.ClusterConfig);
                this.HardwareMonitor = new HardwareMonitor(this.ClusterConfig);
                this.TabletMagazine = new TabletMagazine();
                this.OrderConsumer = new OrderConsumer();
                this._sequencer = new FSMSequencer(this);
                //this._eStopMonitor = new EmergencyStopMonitor(this);
            }
            catch (Exception ex)
            {
                var outer = new Exception("Unable to initialise the SCADA system", ex);

                Logger.Instance.Write(new LogEntry(outer, LogType.Error));
                return false;
            }
            this.StartAllTimers();
            return true;
        }
    }
}
