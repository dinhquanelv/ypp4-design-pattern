using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_Singleton
{
    public sealed class AppConfigurationManager
    {
        private static AppConfigurationManager _instance;
        private static readonly object _lock = new object();
        private Dictionary<string, string> _settings;

        private AppConfigurationManager()
        {
            LoadConfiguration();
        }

        public static AppConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppConfigurationManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private void LoadConfiguration()
        {
            _settings = new Dictionary<string, string>
        {
            { "ApiBaseUrl", "https://api.healthcare.com" },
            { "MaxUploadSize", "10485760" }, // 10MB
            { "SessionTimeout", "30" }, // minutes
            { "EnableAIDiagnosis", "true" },
            { "SmtpHost", "smtp.gmail.com" },
            { "SmtpPort", "587" },
            { "CacheExpiration", "3600" } // seconds
        };
            Console.WriteLine("Configuration loaded successfully");
        }
        public string GetSetting(string key)
        {
            return _settings.ContainsKey(key) ? _settings[key] : null;
        }

        public T GetSetting<T>(string key)
        {
            if (_settings.ContainsKey(key))
            {
                var value = _settings[key];
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return default(T);
        }

        public void UpdateSetting(string key, string value)
        {
            if (_settings.ContainsKey(key))
            {
                _settings[key] = value;
                Console.WriteLine($"Setting '{key}' updated to '{value}'");
            }
            else
            {
                _settings.Add(key, value);
                Console.WriteLine($"Setting '{key}' added with value '{value}'");
            }
        }

        public void DisplayAllSettings()
        {
            Console.WriteLine("Current Application Settings:");
            foreach (var setting in _settings)
            {
                Console.WriteLine($"{setting.Key}: {setting.Value}");
            }
        }
    }

    public class Apiservice
    {
        private readonly string _apiBaseUrl;
        public Apiservice()
        {
            _apiBaseUrl = AppConfigurationManager.Instance.GetSetting("ApiBaseUrl");
        }
        public void CallApi()
        {
            Console.WriteLine($"Calling API at {_apiBaseUrl}");
        }
    }

    public class FileUploadService
    {
        public bool ValidateFileSize(long fileSize)
        {
            var maxSize = AppConfigurationManager.Instance.GetSetting<long>("MaxUploadSize");
            return fileSize <= maxSize;
        }
    }
}
