namespace ApiHost.DTO
{
    public class LogDTO
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public DateTime DateTime { get; private set; }

        public LogDTO(string name, string context)
        {
            Name = name;
            Context = context;
            DateTime = DateTime.Now;
        }
    }
}
