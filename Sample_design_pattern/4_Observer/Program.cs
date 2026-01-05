using _4_Observer;
using static _4_Observer.OrderTracker;

public class Program
{
    public static void Main()
    {
        var patientMonitor = new Patient
        {
            PatientId = "P12345",
            PatientName = "Nguyen Van A"
        };

        Console.WriteLine("=== Setting up Patient Monitoring System ===\n");
        patientMonitor.Attach(new Doctor("Tran Van B"));
        patientMonitor.Attach(new FamilyMember("Nguyen Thi C", "0901234567"));

        Console.WriteLine("\n--- Scenario 1: Normal Vital Signs ---");
        patientMonitor.VitalSigns = new PatientVitalSigns
        {
            HeartRate = 75,
            BloodPressure = "120/80",
            Temperature = 36.8,
            OxygenLevel = 98,
            RecordedAt = DateTime.Now
        };

        Thread.Sleep(2000);

        // Simulation 2: Abnormal heart rate
        Console.WriteLine("\n--- Scenario 2: Abnormal Heart Rate ---");
        patientMonitor.VitalSigns = new PatientVitalSigns
        {
            HeartRate = 125,
            BloodPressure = "140/90",
            Temperature = 37.2,
            OxygenLevel = 96,
            RecordedAt = DateTime.Now
        };

        Thread.Sleep(2000);

        // Simulation 3: Critical condition
        Console.WriteLine("\n--- Scenario 3: Critical Condition ---");
        patientMonitor.VitalSigns = new PatientVitalSigns
        {
            HeartRate = 45,
            BloodPressure = "90/60",
            Temperature = 40.1,
            OxygenLevel = 88,
            RecordedAt = DateTime.Now
        };



        Console.WriteLine("\n--- Tracking ---");

        var orderTracker = new OrderTracker();

        // Subscribe observers
        orderTracker.Subscribe(new Customer());
        orderTracker.Subscribe(new InventorySystem());
        orderTracker.Subscribe(new ShippingProvider());
        orderTracker.Subscribe(new AnalyticsSystem());

        // Create order
        var order = new Order
        {
            OrderId = "ORD20240105001",
            CustomerEmail = "customer@example.com",
            CustomerPhone = "0901234567",
            Items = new List<OrderItem>
            {
                new OrderItem { ProductName = "Product A", Quantity = 2, Price = 50000 },
                new OrderItem { ProductName = "Product B", Quantity = 1, Price = 30000 }
            },
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.Now
        };

        // Simulate order lifecycle
        orderTracker.UpdateOrderStatus(order, OrderStatus.Confirmed);
        Thread.Sleep(1000);

        orderTracker.UpdateOrderStatus(order, OrderStatus.Processing);
        Thread.Sleep(1000);

        orderTracker.UpdateOrderStatus(order, OrderStatus.Shipping);
        Thread.Sleep(1000);

        orderTracker.UpdateOrderStatus(order, OrderStatus.Delivered);
    }
}