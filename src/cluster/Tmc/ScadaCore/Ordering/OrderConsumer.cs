using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using Tmc.Common;
using TmcData;

namespace Tmc.Scada.Core
{
    public class OrderConsumer
    {
        private static OrderConsumer _instance;

        public static OrderConsumer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderConsumer();
                }
                return _instance;
            }
        }

        private Queue<Order> _orderQueue;
        private Timer _updateTimer;
        public List<Order> Orders { get; set; }

        public OrderConsumer()
        {
            this._orderQueue = new Queue<Order>();

            foreach (var orderInfo in TmcRepository.OrderInfo())
            {
                var order = new Order();
                order.Configuration.AddTablet(TabletColors.Black, orderInfo.Black);
                order.Configuration.AddTablet(TabletColors.Blue, orderInfo.Blue);
                order.Configuration.AddTablet(TabletColors.Green, orderInfo.Green);
                order.Configuration.AddTablet(TabletColors.Red, orderInfo.Red);
                order.Configuration.AddTablet(TabletColors.White, orderInfo.White);
                Orders.Add(order);
            }

            int updateTime = 1000;
            if (!Int32.TryParse(ConfigurationManager.AppSettings["OrderConsumerUpdateRateMsec"], out updateTime))
            {
                Logger.Instance.Write(new LogEntry("OrderConsumerUpdateRateMsec is invalid, defaulting to 1000 msec",
                    LogType.Warning));
            }
            this._updateTimer = new Timer(updateTime);
            this._updateTimer.Elapsed += new ElapsedEventHandler((s, e) => this.Update());
        }

        public void Start()
        {
            this._updateTimer.Start();
        }

        public void Stop()
        {
            this._updateTimer.Stop();
        }

        public Order PeekOrder()
        {
            return _orderQueue.Peek();
        }

        public IEnumerable<Order> OrdersByStatus(OrderStatus status)
        {
            return this._orderQueue.Select(x => x).Where(y => y.Status == status);
        }

        private void Update()
        {
            foreach (var order in this.Orders)
            {
                if (!this._orderQueue.Contains(order) && order.Status == OrderStatus.Open)
                {
                    order.Status = OrderStatus.Pending;
                    this._orderQueue.Enqueue(order);
                }
            }
        }
    }
}