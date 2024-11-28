namespace ApiHost.Database
{
    public class DatabaseSafetyChecks : IDisposable
    {
        private readonly SqlHandler _sqlHandler;

        public DatabaseSafetyChecks(SqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }

        internal async ValueTask CreateLogsTableIfItDoesNotExist()
        {
            await _sqlHandler.CreateLogTableIfTableExists();
        }

        internal async ValueTask CreateLogsDatabaseIfItDoesNotExist()
        {
            await _sqlHandler.CreateLogsDatabaseIfDoesNotExist();
        }

        internal async ValueTask<bool> DoesServerExist()
        {
            var result = await _sqlHandler.CheckSqlServerConnection();
            return result;
        }

        public void Dispose()
        {
            
        }
    }
}
