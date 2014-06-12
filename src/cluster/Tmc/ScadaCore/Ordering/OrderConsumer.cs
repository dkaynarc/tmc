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

        private Queue<Order> _toUpdate;
        private Order _assemblingOrder;
        private Timer _updateTimer;
        public List<Order> Orders { get; set; }

        public event EventHandler OrdersAvailable;

        public OrderConsumer()
        {
            this.Orders = new List<Order>();
            this._toUpdate = new Queue<Order>();

            int updateTime = 5000;
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

        public bool IsNewOrderAvailable()
        {
            return Orders.Count > 0;
        }

        public Order peekNextOrder()
        {
            return Orders.FirstOrDefault();
        }

        public Order GetNextOrder()
        {
            _assemblingOrder = Orders.First();
            _assemblingOrder.Status = OrderStatus.Assembling;
            TmcRepository.UpdateOrderStatus(_assemblingOrder.Id, (int)_assemblingOrder.Status);
            //_toUpdate.Enqueue(_assemblingOrder);

            return _assemblingOrder;
        }

        public void CompleteOrder()
        {
            _assemblingOrder.Status = OrderStatus.Completed;

            TmcRepository.CompleteOrder(_assemblingOrder.Id);
            //_toUpdate.Enqueue(_assemblingOrder);
            
            foreach(var order in Orders.Where(e => e.Id == _assemblingOrder.Id))
            {
                Orders.Remove(order);
            }
        }

        public IEnumerable<Order> OrdersByStatus(OrderStatus status)
        {
            return this.Orders.Select(x => x).Where(y => y.Status == status);
        }

        private void Update()
        {
            Orders = new List<Order>();

            _updateTimer.Stop();

            var list = new List<OrderListView>();
            list.AddRange(TmcRepository.GetOrdersByStatus((int)OrderStatus.Pending));
            list.AddRange(TmcRepository.GetOrdersByStatus((int)OrderStatus.Open));

            foreach (var orderInfo in list)
            {
                var order = new Order();
                order.Configuration.AddTablet(TabletColors.Black, orderInfo.Black);
                order.Configuration.AddTablet(TabletColors.Blue, orderInfo.Blue);
                order.Configuration.AddTablet(TabletColors.Green, orderInfo.Green);
                order.Configuration.AddTablet(TabletColors.Red, orderInfo.Red);
                order.Configuration.AddTablet(TabletColors.White, orderInfo.White);
                order.Id = orderInfo.OrderID;
                Orders.Add(order);
            }

           if (Orders.Count > 0)
            {
                OnOrdersAvailable(new EventArgs());
            }

            UpdateAll();
            _updateTimer.Start();
        }

        private void UpdateAll()
        {
            while (_toUpdate.Count > 0)
            {
                var order = _toUpdate.Dequeue();
                TmcRepository.UpdateOrderStatus(order.Id, (int)order.Status);
            }
        }

        private void OnOrdersAvailable(EventArgs e)
        {
            var handler = OrdersAvailable;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}