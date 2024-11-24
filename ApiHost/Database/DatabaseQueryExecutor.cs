using Microsoft.Data.SqlClient;
using System.Text;

namespace ApiHost.Database
{
    public class DatabaseQueryExecutor
    {
        public static readonly string SQL_NULL_FIELD = "null";
        public static readonly string SQL_COLUMN_SEPARATOR = "||";
        public static readonly string SQL_ERROR_POSTFIX = "-ERROR";

        private readonly StringBuilder _sb = new StringBuilder();

        public async ValueTask<int> ExecuteNonQueryAsync(string connectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    await connection.OpenAsync();
                    int affectedRows = await command.ExecuteNonQueryAsync();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }

        public async ValueTask<List<string>> CreateSqlReader_ThenExecuteAndReturnAllRowsAsync(string connectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    var result = await ReadAllReaderRows(reader);
                    reader.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }

        #region SQL READER METHODS
        private async ValueTask<List<string>> ReadAllReaderRows(SqlDataReader reader)
        {
            var values = new List<string>();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    _sb.Clear();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = CheckForNullOrEmpty(reader[i]);
                        if (i < reader.FieldCount - 1)
                            _sb.Append($"{value}{SQL_COLUMN_SEPARATOR}");
                        else
                            _sb.Append($"{value}");
                    }
                    values.Add(_sb.ToString());
                }
            }
            return values;
        }

        private string CheckForNullOrEmpty(object entry)
        {
            var result = entry?.ToString()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(result))
                return SQL_NULL_FIELD;
            return result;
        }
        #endregion

    }
}
