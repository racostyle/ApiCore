using Microsoft.Data.SqlClient;

namespace ApiHost.Database
{
    public class DatabaseUtils
    {
        public async Task<bool> CheckSqlServerConnectionAsync(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    return true;
                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }

        public string ParseSqlServerToRealName(string serverName)
        {

            if (serverName.Contains("localhost\\"))
                serverName = serverName.Replace("localhost\\", $"{Environment.MachineName}\\");
            if (serverName.Contains(".\\"))
                serverName = serverName.Replace(".\\", $"{Environment.MachineName}\\");

            return serverName;
        }

        public string GenerateConnectionString(string serverName)
        {
            serverName = ParseSqlServerToRealName(serverName);

            return $@"Server={serverName};Trusted_Connection=True;Integrated Security=True;Encrypt=False;";
        }
    }
}
