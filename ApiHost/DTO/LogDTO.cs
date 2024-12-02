namespace ApiHost.DTO
{
    public class LogDTO
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; private set; }

        public LogDTO(string name, string type, string context)
        {
            Name = name;
            Type = type;
            Context = context;
            DateTime = DateTime.Now;
        }
    }
}
