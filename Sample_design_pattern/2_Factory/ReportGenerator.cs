namespace _2_Factory
{
    public enum ReportFormat
    {
        Pdf,
        Excel
    }

    public class ReportData
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Records { get; set; }
    }

    public interface IReportGenerator
    {
        byte[] GenerateReport(ReportData data);
        string GetFileExtension();
        string GetMimeType();
    }

    public class PdfReportGenerator : IReportGenerator
    {
        public byte[] GenerateReport(ReportData data)
        {
            Console.WriteLine($"📄 Generating PDF report: {data.Title}");
            Console.WriteLine($"   Period: {data.StartDate:dd/MM/yyyy} - {data.EndDate:dd/MM/yyyy}");
            Console.WriteLine($"   Total records: {data.Records.Count}");

            var content = $"PDF Report\n{data.Title}\n\n";
            foreach (var record in data.Records)
            {
                content += $"{record}\n";
            }

            return System.Text.Encoding.UTF8.GetBytes(content);
        }

        public string GetFileExtension() => ".pdf";
        public string GetMimeType() => "application/pdf";
    }

    public class ExcelReportGenerator : IReportGenerator
    {
        public byte[] GenerateReport(ReportData data)
        {
            Console.WriteLine($"📊 Generating Excel report: {data.Title}");
            Console.WriteLine($"   Period: {data.StartDate:dd/MM/yyyy} - {data.EndDate:dd/MM/yyyy}");
            Console.WriteLine($"   Total records: {data.Records.Count}");
            var content = $"Excel Report\n{data.Title}\n\n";
            foreach (var record in data.Records)
            {
                content += $"{record}\n";
            }
            return System.Text.Encoding.UTF8.GetBytes(content);
        }
        public string GetFileExtension() => ".xlsx";
        public string GetMimeType() => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }

    public static class ReportGeneratorFactory
    {
        public static IReportGenerator CreateReportGenerator(ReportFormat format) =>
            format switch
            {
                ReportFormat.Pdf => new PdfReportGenerator(),
                ReportFormat.Excel => new ExcelReportGenerator(),
                _ => throw new NotSupportedException($"Report format {format} is not supported.")
            };
    }

    public class HealthcareReportService
    {
        public void GeneratePatientReport(ReportFormat format)
        {
            var data = new ReportData
            {
                Title = "Patient Visit Report",
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now,
                Records = new List<string>
            {
                "Patient: Nguyen Van A - Visit: 01/12/2024",
                "Patient: Tran Thi B - Visit: 05/12/2024",
                "Patient: Le Van C - Visit: 10/12/2024",
                "Patient: Pham Thi D - Visit: 15/12/2024"
            }
            };

            var generator = ReportGeneratorFactory.CreateReportGenerator(format);
            var reportBytes = generator.GenerateReport(data);
            var fileName = $"patient_report_{DateTime.Now:yyyyMMdd}{generator.GetFileExtension()}";

            Console.WriteLine($"✓ Report generated: {fileName} ({reportBytes.Length} bytes)");
            Console.WriteLine($"   MIME Type: {generator.GetMimeType()}\n");
        }
    }
}
