using Dtos;

namespace ApiHost.Database
{
    public class SqlHandler
    {
        private readonly DatabaseQueryExecutor _databaseQueryExecutor;
        private readonly DatabaseUtils _databaseUtils;
        private readonly Queries _queries;
        public readonly string ConnectionString;

        public SqlHandler(DatabaseQueryExecutor databaseQueryExecutor, DatabaseUtils databaseUtils, Queries queries, string sqlServerName)
        {
            _databaseQueryExecutor = databaseQueryExecutor;
            _databaseUtils = databaseUtils;
            _queries = queries;
            ConnectionString = _databaseUtils.GenerateConnectionString(sqlServerName);
        }

        #region safetychecks
        internal async Task CreateLogTableIfTableExists()
        {
            try
            {
                var result = await _databaseQueryExecutor.CreateSqlReader_ThenExecuteAndReturnAllRowsAsync(
                ConnectionString,
                _queries.FetchLogsTableIfExists());

                if (result.Count == 0)
                    throw new Exception("Table not found!");
            }
            catch
            {
                var createResult = await _databaseQueryExecutor.ExecuteNonQueryAsync(ConnectionString, _queries.CreateLogTable());

                if (createResult == 0)
                    throw new Exception($"Error in {GetType().Name}: Could not create database");
            }
        }

        internal async Task CreateLogsDatabaseIfDoesNotExist()
        {
            try
            {
                var result = await _databaseQueryExecutor.CreateSqlReader_ThenExecuteAndReturnAllRowsAsync(
                ConnectionString,
               _queries.FetchLogsDatabaseIfExists());

                if (result.Count == 0)
                    throw new Exception("Database not found!");
            }
            catch
            {
                var createResult = await _databaseQueryExecutor.ExecuteNonQueryAsync(ConnectionString, _queries.CreateLogDatabase());

                if (createResult == 0)
                    throw new Exception($"Error in {GetType().Name}: Could not create database");
            }
        }

        internal async Task<bool> CheckSqlServerConnection()
        {
            return await _databaseUtils.CheckSqlServerConnectionAsync(ConnectionString);
        }
        #endregion

        internal async Task<bool> Post(LogDTO log)
        {
            var createResult = await _databaseQueryExecutor.ExecuteNonQueryAsync(ConnectionString, _queries.InsertIntoLogTable(log));

            return createResult > 0;
        }

        internal async Task<List<string>> Get()
        {
            var results = await _databaseQueryExecutor.CreateSqlReader_ThenExecuteAndReturnAllRowsAsync(ConnectionString, _queries.FetchAllLogs());
            return results;
        }
    }
}