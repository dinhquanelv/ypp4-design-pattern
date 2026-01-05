using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Strategy
{
    public class ShippingDetails
    {
        public decimal Weight { get; set; } // kg
        public decimal Distance { get; set; } // km
        public decimal OrderValue { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
    }

    public class ShippingResult
    {
        public string ShippingMethod { get; set; }
        public decimal Cost { get; set; }
        public string EstimatedDeliveryTime { get; set; }
        public ShippingDetails Details { get; set; }

        public void Display()
        {
            Console.WriteLine($"\n✓ Result:");

            Console.WriteLine($"Method: {ShippingMethod}");

            Console.WriteLine($"Shipping Fee: {Cost:N0} VND");

            Console.WriteLine($"Estimated DeliveryTime: {EstimatedDeliveryTime}");
        }
    }

    public interface IShippingFactory
    {
        decimal CalculateShippingCost(ShippingDetails details);
        string GetEstimatedDeliveryTime(ShippingDetails details);
        string GetShippingMethodName();
    }

    public class StandardShipping : IShippingFactory
    {
        public decimal CalculateShippingCost(ShippingDetails details)
        {
            decimal baseCost = 30000;
            decimal weightCost = details.Weight * 2000;
            decimal distanceCost = details.Distance * 500;

            var total = baseCost + weightCost + distanceCost;
            Console.WriteLine($"   Base: {baseCost:N0} + Weight({details.Weight}kg): {weightCost:N0} + Distance({details.Distance}km): {distanceCost:N0}");

            return total;
        }

        public string GetEstimatedDeliveryTime(ShippingDetails details)
        {
            int days = details.Distance > 100 ? 5 : 3;
            return $"{days}-{days + 2} working days";
        }

        public string GetShippingMethodName() => "Standard delivery";
    }

    public class ExpressShipping : IShippingFactory
    {
        public decimal CalculateShippingCost(ShippingDetails details)
        {
            decimal baseCost = 50000;
            decimal weightCost = details.Weight * 4000;
            decimal distanceCost = details.Distance * 1000;
            var total = baseCost + weightCost + distanceCost;
            Console.WriteLine($"   Base: {baseCost:N0} + Weight({details.Weight}kg): {weightCost:N0} + Distance({details.Distance}km): {distanceCost:N0}");
            return total;
        }
        public string GetEstimatedDeliveryTime(ShippingDetails details)
        {
            int days = details.Distance > 100 ? 2 : 1;
            return $"{days}-{days + 1} working days";
        }
        public string GetShippingMethodName() => "Express delivery";
    }

    public class SamedayShipping : IShippingFactory
    {
        public decimal CalculateShippingCost(ShippingDetails details)
        {
            decimal baseCost = 80000;
            decimal weightCost = details.Weight * 6000;
            decimal distanceCost = details.Distance * 1500;
            var total = baseCost + weightCost + distanceCost;
            Console.WriteLine($"   Base: {baseCost:N0} + Weight({details.Weight}kg): {weightCost:N0} + Distance({details.Distance}km): {distanceCost:N0}");
            return total;
        }
        public string GetEstimatedDeliveryTime(ShippingDetails details)
        {
            return "Same day delivery";
        }
        public string GetShippingMethodName() => "Same-day delivery";
    }
    public class ShippingCalculator
    {
        private IShippingFactory _strategy;

        public void SetStrategy(IShippingFactory strategy)
        {
            _strategy = strategy;
        }

        public ShippingResult CalculateShipping(ShippingDetails details)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Shipping strategy chưa được thiết lập");
            }

            Console.WriteLine($"\n📦 Tính phí vận chuyển - {_strategy.GetShippingMethodName()}");

            var cost = _strategy.CalculateShippingCost(details);
            var deliveryTime = _strategy.GetEstimatedDeliveryTime(details);

            return new ShippingResult
            {
                ShippingMethod = _strategy.GetShippingMethodName(),
                Cost = cost,
                EstimatedDeliveryTime = deliveryTime,
                Details = details
            };
        }
    }
}

    
