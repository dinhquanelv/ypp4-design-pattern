using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Factory
{

    public enum PaymentMethod
    {
        MoMo,
        VNPay
    }

    public class PaymentRequest
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string CustomerEmail { get; set; }
        public string Description { get; set; }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
        public DateTime ProcessedAt { get; set; }
    }

    public class RefundResult
    {
        public bool Success { get; set; }
        public string RefundId { get; set; }
    }

    public interface IPaymentGateway
    {
        PaymentResult ProcessPayment(PaymentRequest request);
        bool ValidatePayment(string transactionId);
        RefundResult ProcessRefund(string transactionId, decimal amount);
    }

    public class MoMoPayment : IPaymentGateway
    {
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            Console.WriteLine($"Processing MoMo payment for {request.Amount:C}");
            return new PaymentResult
            {
                Success = true,
                TransactionId = $"MOMO-{Guid.NewGuid().ToString().Substring(0, 8)}",
                Message = "Payment processed via MoMo successfully",
                ProcessedAt = DateTime.Now
            };
        }

        public bool ValidatePayment(string transactionId)
        {
            Console.WriteLine($"Validating MoMo transaction: {transactionId}");
            return transactionId.StartsWith("MOMO-");
        }

        public RefundResult ProcessRefund(string transactionId, decimal amount)
        {
            Console.WriteLine($"Processing MoMo refund: {amount:C}");
            return new RefundResult { Success = true, RefundId = $"REFUND-MOMO-{Guid.NewGuid()}" };
        }
    }

    public class VNPayPayment : IPaymentGateway
    {
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            Console.WriteLine($"Processing VNPay payment for {request.Amount:C}");
            return new PaymentResult
            {
                Success = true,
                TransactionId = $"VNPAY-{Guid.NewGuid().ToString().Substring(0, 8)}",
                Message = "Payment processed via VNPay successfully",
                ProcessedAt = DateTime.Now
            };
        }
        public bool ValidatePayment(string transactionId)
        {
            Console.WriteLine($"Validating VNPay transaction: {transactionId}");
            return transactionId.StartsWith("VNPAY-");
        }
        public RefundResult ProcessRefund(string transactionId, decimal amount)
        {
            Console.WriteLine($"Processing VNPay refund: {amount:C}");
            return new RefundResult { Success = true, RefundId = $"REFUND-VNPAY-{Guid.NewGuid()}" };
        }
    }

    public class PaymentGatewayFactory
    {
        public static IPaymentGateway CreatePaymentGateway(PaymentMethod method)
    {
        return method switch
        {
            PaymentMethod.MoMo => new MoMoPayment(),
            PaymentMethod.VNPay => new VNPayPayment(),
            _ => throw new ArgumentException($"Unsupported payment method: {method}")
        };
    }
    }

    public class CheckoutService
    {
        public void ProcessCheckout(string orderId, decimal amount, PaymentMethod method)
        {
            var paymentGateway = PaymentGatewayFactory.CreatePaymentGateway(method);

            var request = new PaymentRequest
            {
                OrderId = orderId,
                Amount = amount,
                CustomerEmail = "customer@example.com",
                Description = $"Payment for order {orderId}"
            };

            var result = paymentGateway.ProcessPayment(request);

            if (result.Success)
            {
                Console.WriteLine($"✓ Payment successful: {result.TransactionId}");
                paymentGateway.ValidatePayment(result.TransactionId);
            }
        }
    }
}
