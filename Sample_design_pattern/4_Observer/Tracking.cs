using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Observer
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Processing,
        Shipping,
        Delivered,
        Cancelled
    }

    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
        public string TrackingNumber { get; set; }
    }

    public interface IOrderObserver
    {
        void OnOrderStatusChanged(Order order);
    }

    public class OrderItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderTracker
    {
        private List<IOrderObserver> _observer = new List<IOrderObserver>();
        private Order _currentOrder;

        public void Subscribe(IOrderObserver observer)
        {
            _observer.Add(observer);
        }

        public void Unsubscribe(IOrderObserver observer)
        {
            _observer.Remove(observer);
        }

        public void UpdateOrderStatus(Order order, OrderStatus newStatus)
        {
            _currentOrder = order;
            _currentOrder.Status = newStatus;
            _currentOrder.LastUpdated = DateTime.Now;

            Console.WriteLine($"\n Order {order.OrderId} status changed to: {newStatus}");
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observer)
            {
                observer.OnOrderStatusChanged(_currentOrder);
            }
        }

        public class Customer : IOrderObserver
        {
            public void OnOrderStatusChanged(Order order)
            {
                var message = order.Status switch
                {
                    OrderStatus.Confirmed => $"Order #{order.OrderId} has been confirmed",
                    OrderStatus.Processing => $"Order #{order.OrderId} is being processed",
                    OrderStatus.Shipping => $"Order #{order.OrderId} is being shipped. Tracking number: {order.TrackingNumber}",

                    OrderStatus.Delivered => $"Order #{order.OrderId} has been successfully delivered",

                    OrderStatus.Cancelled => $"Order #{order.OrderId} has been cancelled",

                    _ => $"Order #{order.OrderId} has been updated"
                };

                Console.WriteLine($" Email sent to {order.CustomerEmail}: {message}");

                Console.WriteLine($" SMS sent to {order.CustomerPhone}: {message}");
            }
        }

        public class InventorySystem : IOrderObserver
        {
            public void OnOrderStatusChanged(Order order)
            {
                if (order.Status == OrderStatus.Processing)
                {
                    Console.WriteLine($" Inventory System: Reserving stock for Order #{order.OrderId}");
                }
                else if (order.Status == OrderStatus.Cancelled)
                {
                    Console.WriteLine($" Inventory System: Releasing stock for Order #{order.OrderId}");
                }
            }
        }

        public class ShippingProvider : IOrderObserver
        {
            public void OnOrderStatusChanged(Order order)
            {
                if (order.Status == OrderStatus.Shipping)
                {
                    Console.WriteLine($" Shipping Provider: Preparing shipment for Order #{order.OrderId} with Tracking Number: {order.TrackingNumber}");
                }
            }
        }

        public class AnalyticsSystem : IOrderObserver
        {
            public void OnOrderStatusChanged(Order order)
            {
                Console.WriteLine($" Analytics System: Logging status change for Order #{order.OrderId} to {order.Status} at {order.LastUpdated}");
            }
        }
    }
}
