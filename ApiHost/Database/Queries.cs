﻿namespace ApiHost.Database
{
    public class Queries
    {
        internal readonly string LOG_DATABASE_NAME = "Logs";
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
            Type VARCHAR(30) NOT NULL,
            Description VARCHAR(MAX) NOT NULL);";
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
