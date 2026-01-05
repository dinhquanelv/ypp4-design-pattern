class DatabaseConnection {
  private static instance: DatabaseConnection;

  private constructor() {
    console.log("Create database connection");
  }

  public static getInstance(): DatabaseConnection {
    if (!DatabaseConnection.instance) {
      DatabaseConnection.instance = new DatabaseConnection();
    }

    return DatabaseConnection.instance;
  }

  public query(sql: string): void {
    console.log(`Executing query: ${sql}`);
  }
}

const db1 = DatabaseConnection.getInstance();
const db2 = DatabaseConnection.getInstance();

db1.query("SELECT email FROM users;");
db2.query("SELECT product_name FROM products;");

console.log("### Compare ###", db1 === db2); // true
