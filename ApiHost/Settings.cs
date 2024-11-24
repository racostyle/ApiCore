
using System.Text.Json;

namespace ApiHost
{
    public class Settings
    {
        private const string SQL_SERVER_NAME = "SqlServer";
        private const string CONFIG_NAME = "appsettings.Secrets.json";

        private readonly Dictionary<string, string> _config;

        public Settings()
        {
            try
            {
                var json = File.ReadAllText(CONFIG_NAME);
                if (!string.IsNullOrEmpty(json))
                {
                    _config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                }
                else
                    throw new Exception("No content in appsettings.Secrets");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public string GetSqlServerName()
        {
            return TryGetValue(SQL_SERVER_NAME);
        }

        #region Auxiliary
        private string TryGetValue(string key)
        {
            string value = string.Empty;
            if (_config.ContainsKey(key))
                value = _config[key];
            else
                throw new KeyNotFoundException(key);

            if (string.IsNullOrEmpty(value))
                throw new NullReferenceException();

            return value;
        }
        #endregion
    }
}