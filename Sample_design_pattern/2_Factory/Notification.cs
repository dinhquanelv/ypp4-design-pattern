using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Factory
{

    public enum NotificationType
    {
        Email,
        Sms
    }

    public class NotificationMessage
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class NotificationStatus
    {
        public string MessageId { get; set; }
        public string Status { get; set; }
        public DateTime DeliveredAt { get; set; }
    }


    public interface INotification
    {
        Task SendNotificationAsync(NotificationMessage message);
        bool ValidateRecipient(string recipient);
        Task<NotificationStatus> GetStatusAsync(string messageId);
    }

    public class EmailNotification : INotification
    {
        public async Task SendNotificationAsync(NotificationMessage message)
        {
            Console.WriteLine($"📧 Sending EMAIL to {message.Recipient}");
            Console.WriteLine($"   Subject: {message.Subject}");
            Console.WriteLine($"   Body: {message.Body}");
            await Task.Delay(100);
            Console.WriteLine("   ✓ Email sent successfully");
        }
        public bool ValidateRecipient(string recipient)
        {
            return recipient.Contains("@") && recipient.Contains(".");
        }

        public async Task<NotificationStatus> GetStatusAsync(string messageId)
        {
            await Task.Delay(50);
            return new NotificationStatus { MessageId = messageId, Status = "Delivered", DeliveredAt = DateTime.Now };
        }
    }

    public class SmsNotification : INotification
    {
        public async Task SendNotificationAsync(NotificationMessage message)
        {
            Console.WriteLine($"📱 Sending SMS to {message.Recipient}");
            Console.WriteLine($"   Body: {message.Body}");
            await Task.Delay(100);
            Console.WriteLine("   ✓ SMS sent successfully");
        }
        public bool ValidateRecipient(string recipient)
        {
            return recipient.All(char.IsDigit) && recipient.Length == 10;
        }
        public async Task<NotificationStatus> GetStatusAsync(string messageId)
        {
            await Task.Delay(50);
            return new NotificationStatus { MessageId = messageId, Status = "Delivered", DeliveredAt = DateTime.Now };
        }
    }

    public class NotificationFactory
    {
        public static INotification CreateNotification(NotificationType type)
        {
            return type switch
            {
                NotificationType.Email => new EmailNotification(),
                NotificationType.Sms => new SmsNotification(),
                _ => throw new NotSupportedException($"Notification type {type} is not supported.")
            };
        }
    }


    public class AppointmentService
    {
        public async Task SendAppointmentReminderAsync(string patientEmail, string patientPhone)
        {
            var message = new NotificationMessage
            {
                Subject = "Appointment reminder",
                Body = "You have a doctor's appointment for 10:00 AM tomorrow."
            };

            var emailService = NotificationFactory.CreateNotification(NotificationType.Email);
            message.Recipient = patientEmail;
            await emailService.SendNotificationAsync(message);

            var smsService = NotificationFactory.CreateNotification(NotificationType.Sms);
            message.Recipient = patientPhone;
            await smsService.SendNotificationAsync(message);

        }
    }
}


