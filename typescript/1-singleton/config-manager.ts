class ConfigManager {
  private static instance: ConfigManager | null = null;

  private config: { [key: string]: string } = {};

  private constructor() {
    console.log("Init Config Manager");
  }

  static getInstance(): ConfigManager {
    if (!ConfigManager.instance) {
      ConfigManager.instance = new ConfigManager();
    }

    return ConfigManager.instance;
  }

  set(key: string, value: string) {
    this.config[key] = value;
  }

  get(key: string): string | null {
    return this.config[key];
  }
}

const config1 = ConfigManager.getInstance();
const config2 = ConfigManager.getInstance();

config1.set("url", "https://example.com");
console.log(config2.get("url"));
