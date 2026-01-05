interface Database {
  connect(): void;
}

class PostgresDatabase implements Database {
  connect(): void {
    console.log("Connect to Postgres");
  }
}

class MongoDatabase implements Database {
  connect(): void {
    console.log("Connect to Mongo");
  }
}

class FirebaseDatabase implements Database {
  connect(): void {
    console.log("Connect to Firebase");
  }
}

class DatabaseFactory {
  private static readonly databases = new Map<string, new () => Database>([
    ["postgres", PostgresDatabase],
    ["mongo", MongoDatabase],
    ["firebase", FirebaseDatabase],
  ]);

  static initDatabase(type: string) {
    const DatabaseClass = this.databases.get(type);

    if (!DatabaseClass) {
      throw new Error("Invalid type");
    }

    return new DatabaseClass();
  }
}

const postgres = DatabaseFactory.initDatabase("postgres");
const mongo = DatabaseFactory.initDatabase("mongo");
const firebase = DatabaseFactory.initDatabase("firebase");

postgres.connect();
mongo.connect();
firebase.connect();
