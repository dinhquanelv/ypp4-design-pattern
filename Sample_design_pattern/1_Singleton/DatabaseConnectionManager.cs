namespace _1_Singleton
{
    public sealed class DatabaseConnectionManager
    {
        private static DatabaseConnectionManager _instance;
        private static readonly object _lock = new object();
        private readonly string _connectionString;
        private int _connectionCount;

        private readonly List<MockConnection> _connectionPool;

        private readonly List<MockConnection> _connections;

        private DatabaseConnectionManager()
        {
            _connectionString = "Server=localhost;Database=HealthCareDB;User=sa;Password=123456";
            _connectionPool = new List<MockConnection>();
            Console.WriteLine("DatabaseConnectionManager được khởi tạo lần đầu tiên");
        }

        public static DatabaseConnectionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseConnectionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public MockConnection GetConnection()
        {
            _connectionCount++;
            var connection = new MockConnection
            {
                Id = _connectionCount,
                ConnectionString = _connectionString,
                IsOpen = true,
                CreatedAt = DateTime.Now
            };
            _connectionPool.Add(connection);
            Console.WriteLine($"Connection #{connection.Id} được tạo. Tổng số: {_connectionPool.Count}");
            return connection;
        }

        public void CloseConnection(int ConnectionId)
        {
            var connection = _connectionPool.FirstOrDefault(c => c.Id == ConnectionId);
            if (connection != null)
            {
                connection.IsOpen = false;
                Console.WriteLine($"Connection #{connection.Id} đã đóng");
            }
        }

        public void ShowStatistics()
        {
            Console.WriteLine($"\n=== Database Statistics ===");
            Console.WriteLine($"Connection String: {_connectionString}");
            Console.WriteLine($"Tổng connections đã tạo: {_connectionCount}");
            Console.WriteLine($"Connections đang mở: {_connectionPool.Count(c => c.IsOpen)}");
            Console.WriteLine($"Connections đã đóng: {_connectionPool.Count(c => !c.IsOpen)}");
        }

        public static void ExecuteDatabaseManager()
        {
            var dbManager1 = DatabaseConnectionManager.Instance;
            var con1 = dbManager1.GetConnection();

            var dbManager2 = DatabaseConnectionManager.Instance;
            var con2 = dbManager2.GetConnection();

            var dbManager3 = DatabaseConnectionManager.Instance;
            var con3 = dbManager3.GetConnection();

            Console.WriteLine($"\ndbManager1 == dbManager2: {ReferenceEquals(dbManager1, dbManager2)}");
            Console.WriteLine($"dbManager2 == dbManager3: {ReferenceEquals(dbManager2, dbManager3)}");

            dbManager1.CloseConnection(1);
            dbManager2.ShowStatistics();
        }
    }

    public class MockConnection
    {
        public int Id { get; set; }
        public string ConnectionString { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
