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
}

const db1 = DatabaseConnection.getInstance();
const db2 = DatabaseConnection.getInstance();

console.log("### Compare ###", db1 === db2); // true
