class Logger {
  private static instance: Logger;

  private constructor() {
    console.log("Logger initialized");
  }

  public static getInstance(): Logger {
    if (!Logger.instance) {
      Logger.instance = new Logger();
    }

    return Logger.instance;
  }

  public log(message: string): void {
    console.log(`[LOG]: ${message}`);
  }
}

const logger1 = Logger.getInstance();
const logger2 = Logger.getInstance();

logger1.log("This is a log message.");
logger2.log("This is another log message.");
console.log("### Compare ###", logger1 === logger2); // true
