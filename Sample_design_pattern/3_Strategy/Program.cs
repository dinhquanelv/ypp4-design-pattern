using _3_Strategy;

public class Program
{
    public static void Main()
    {

        Console.WriteLine($"\n=== Shipping Factory ===");

        var calculator = new ShippingCalculator();

        var details = new ShippingDetails
        {
            Weight = 2.5m,
            Distance = 15,
            OrderValue = 300000,
            FromAddress = "New York",
            ToAddress = "Hawaii"
        };

        // Standard shipping
        calculator.SetStrategy(new StandardShipping());
        var result1 = calculator.CalculateShipping(details);
        result1.Display();

        // Express shipping
        calculator.SetStrategy(new ExpressShipping());
        var result2 = calculator.CalculateShipping(details);
        result2.Display();

        // Same day shipping
        calculator.SetStrategy(new SamedayShipping());
        var result3 = calculator.CalculateShipping(details);
        result3.Display();


        Console.WriteLine($"\n=== Discount Factory ===");
        var discountContext = new DiscountContext();
        var order = new Order
        {
            OrderId = "ORD1001",
            TotalAmount = 1_000_000,
            ItemCount = 6,
            CustomerType = CustomerType.Regular,
            CustomerLoyaltyYears = 4,
            OrderDate = DateTime.Now
        };
        // New Customer Discount
        discountContext.SetStrategy(new NewCustomer());
        var discount1 = discountContext.GetDiscount(order);
        Console.WriteLine($"Discount for New Customer: {discount1:N0} VND");
        // Regular Customer Discount
        discountContext.SetStrategy(new RegularCustomer());
        var discount2 = discountContext.GetDiscount(order);
        Console.WriteLine($"Discount for Regular Customer: {discount2:N0} VND");
        // Loyal Customer Discount
        discountContext.SetStrategy(new LoyalCustomer());
        var discount3 = discountContext.GetDiscount(order);
        Console.WriteLine($"Discount for Loyal Customer: {discount3:N0} VND");


        Console.WriteLine($"\n=== Strategy Pattern: User Authentication ===");
        var authContext = new AuthContext();
        var user = new UserAuth("john doe", "password123");
        // Google Authentication
        authContext.SetStrategy(new GoogleAuth());
        authContext.Authenticate(user);

        // Apple Authentication
        authContext.SetStrategy(new AppleAuth());
        authContext.Authenticate(user);

        // Facebook Authentication
        authContext.SetStrategy(new FacebookAuth());
        authContext.Authenticate(user);

    }
}