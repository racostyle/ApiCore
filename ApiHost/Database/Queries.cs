using Common.Dto;

namespace ApiHost.Database
{
    public class Queries
    {
        internal readonly string LOG_DATABASE_NAME = "MonitorLogs";
        internal readonly string LOG_TABLE_NAME = "Entries";

        public string CreateLogDatabase()
        {
            return @$"CREATE DATABASE {LOG_DATABASE_NAME}
            COLLATE Slovenian_CI_AS;";
        }

        public string CreateLogTable()
        {
            return @$"
            USE {LOG_DATABASE_NAME};
            CREATE TABLE {LOG_TABLE_NAME} (
            ID INT PRIMARY KEY IDENTITY(1,1),
            Source VARCHAR(50),
            Type VARCHAR(10),
            Time DATETIME,
            Operation VARCHAR(10),
            Context VARCHAR(MAX));";
        }

        internal string InsertIntoLogTable(LogDTO dto)
        {
            return @$"
                USE {LOG_DATABASE_NAME};
                INSERT INTO {LOG_TABLE_NAME} ([Source], [Type], [Time], [Operation], [Context])
                VALUES ('{dto.Source}','{dto.Type}','{dto.DateTime.ToString("yyyy-MM-dd HH:mm:ss")}','{dto.Operation}','{dto.Context}');
            ";
        }

        internal string FetchAllLogs()
        {
            return $@"SELECT * FROM {LOG_DATABASE_NAME}.dbo.{LOG_TABLE_NAME};";
        }

        internal string FetchLogsDatabaseIfExists()
        {
            return @$"
                SELECT 1 FROM sys.databases 
                WHERE name = '{LOG_DATABASE_NAME}';";
        }

        internal string FetchLogsTableIfExists()
        {
            return @$"
                USE {LOG_DATABASE_NAME};
                SELECT 1 FROM sys.objects 
                WHERE object_id = OBJECT_ID('dbo.{LOG_TABLE_NAME}') 
                AND type = 'U'";
        }
    }
}
