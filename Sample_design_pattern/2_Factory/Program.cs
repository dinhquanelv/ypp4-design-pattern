using _2_Factory;
using System.ComponentModel.DataAnnotations;

public class Program
{
    public static async Task Main()
    {
        Console.WriteLine($"\n=== Payment Gateway ===");

        var checkoutService = new CheckoutService();

        checkoutService.ProcessCheckout("ORD001", 500000, PaymentMethod.MoMo);
        Console.WriteLine();
        checkoutService.ProcessCheckout("ORD002", 750000, PaymentMethod.VNPay);
        Console.WriteLine();

        Console.WriteLine($"\n=== Notification ===");

        var patientEmail = "abc@gmail.com";
        var patientPhone = "0123456789";

        var notificationService = new AppointmentService();
        await notificationService.SendAppointmentReminderAsync(patientEmail, patientPhone);


        Console.WriteLine($"\n=== Report Generator ===");

        var reportservice = new HealthcareReportService();

        reportservice.GeneratePatientReport(ReportFormat.Pdf);

        reportservice.GeneratePatientReport(ReportFormat.Excel);
    }
}