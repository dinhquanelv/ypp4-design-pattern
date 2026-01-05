using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Strategy
{
    public enum CustomerType
    {
        New,
        Regular,
        Loyal
    }

    public class Order
    {
        public string OrderId { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }
        public CustomerType CustomerType { get; set; }
        public int CustomerLoyaltyYears { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(Order order);
    }

    public class NoDiscount : IDiscountStrategy
    {
        public decimal CalculateDiscount(Order order)
        {
            return 0;
        }
    }

    public class NewCustomer : IDiscountStrategy
    {
        public decimal CalculateDiscount(Order order)
        {
            return order.TotalAmount * 0.05m;
        }
    }

    public class RegularCustomer : IDiscountStrategy
    {
        public decimal CalculateDiscount(Order order)
        {
            decimal discount = order.TotalAmount * 0.10m;
            if (order.ItemCount > 5)
            {
                discount += 50000;
            }
            return discount;
        }
    }
    public class LoyalCustomer : IDiscountStrategy
    {
        public decimal CalculateDiscount(Order order)
        {
            decimal discount = order.TotalAmount * 0.15m;
            if (order.CustomerLoyaltyYears > 3)
            {
                discount += 100000;
            }
            return discount;
        }
    }
    public class DiscountContext
    {
        private IDiscountStrategy _discountStrategy;
        public void SetStrategy(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }
        public decimal GetDiscount(Order order)
        {
            return _discountStrategy.CalculateDiscount(order);
        }
    }
}
