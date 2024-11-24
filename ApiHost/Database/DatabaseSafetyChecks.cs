namespace ApiHost.Database
{
    public class DatabaseSafetyChecks : IDisposable
    {
        private readonly DatabaseQueryExecutor _executor;
        private readonly DatabaseUtils _utils;
        private readonly Queries _queries;
        private readonly string _connectionString;

        public DatabaseSafetyChecks(DatabaseQueryExecutor executor, DatabaseUtils utils, Queries queries, string sqlServerName)
        {
            _executor = executor;
            _utils = utils;
            _queries = queries;
            _connectionString = _utils.GenerateConnectionString(sqlServerName);
        }

        internal async ValueTask CreateLogsTableIfItDoesNotExist()
        {
            try
            {
                 var result = await _executor.CreateSqlReader_ThenExecuteAndReturnAllRowsAsync(
                 _connectionString,
                 _queries.FetchLogsTableIfExists());

                if (result.Count == 0)
                    throw new Exception("Table not found!");
            }
            catch
            {
                var createResult = await _executor.ExecuteNonQueryAsync(_connectionString, _queries.CreateLogTable());

                if (createResult == 0)
                    throw new Exception($"Error in {GetType().Name}: Could not create database");
            }
        }

        internal async ValueTask CreateLogsDatabaseIfItDoesNotExist()
        {
            try
            {
                var result = await _executor.CreateSqlReader_ThenExecuteAndReturnAllRowsAsync(
               _connectionString,
               _queries.FetchLogsDatabaseIfExists());

                if (result.Count == 0)
                    throw new Exception("Database not found!");
            }
            catch
            {
                var createResult = await _executor.ExecuteNonQueryAsync(_connectionString, _queries.CreateLogDatabase());

                if (createResult == 0)
                    throw new Exception($"Error in {GetType().Name}: Could not create database");
            }
        }

        internal async ValueTask<bool> DoesServerExist()
        {
            var result = await _utils.CheckSqlServerConnection(_connectionString);
            return result;
        }

        public void Dispose()
        {
            
        }
    }
}
