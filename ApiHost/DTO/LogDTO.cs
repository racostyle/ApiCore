namespace ApiHost.DTO
{
    public struct LogDTO
    {
        public string Name;
        public DateTime DateTime;
        public string Context;

        public LogDTO(string name, DateTime dateTime, string context)
        {
            Name = name;
            DateTime = dateTime;
            Context = context;
        }
    }
}
