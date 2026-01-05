using _1_Singleton;

public class Program
{
    public static void Main()
    {
        Console.WriteLine($"\n=== Database Connection Manager ===");
        DatabaseConnectionManager.ExecuteDatabaseManager();


        Console.WriteLine($"\n=== Logger Service ===");
        var userName = "john_doe";

        var userservice = new UserService();
        userservice.CreateUser(userName);

        decimal amount = 99.99m;
        var paymentService = new PaymentService();
        paymentService.ProcessPayment(amount);


        Console.WriteLine($"\n=== AppConfiguration ===");
        var ApiService = new Apiservice();
        ApiService.CallApi();

        long fileSize = 2048;
        var fileService = new FileUploadService();
        var allowUpload = fileService.ValidateFileSize(fileSize);
        if (allowUpload)
        {
            Console.WriteLine($"File of size {fileSize} bytes is allowed for upload.");
        }
        else
        {
            Console.WriteLine($"File of size {fileSize} bytes exceeds the maximum allowed size.");
        }
    }
}