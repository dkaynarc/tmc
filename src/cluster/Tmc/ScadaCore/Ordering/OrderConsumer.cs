#region Header
/// FileName: OrderConsumer.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    public class OrderConsumer
    {
        private class MockOrderSource
        {
            public List<Order> Orders {get; set; }

            public MockOrderSource()
            {
                Orders = new List<Order>();
                for (int i = 0; i < 2; i++)
                {
                    var order = new Order { Id = "TestOrder#" + i };
                    order.Configuration.AddTablet(TabletColors.Blue, i * 2);
                    order.Configuration.AddTablet(TabletColors.Green, i + 2);
                    Orders.Add(order);
                }
            }
        }

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
        // TODO: Replace with DB
        private MockOrderSource _orderSource;
        private Timer _updateTimer;

        public OrderConsumer()
        {
            this._orderQueue = new Queue<Order>();

            // TODO: Replace with DB
            this._orderSource = new MockOrderSource();
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
            foreach (var order in this._orderSource.Orders)
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