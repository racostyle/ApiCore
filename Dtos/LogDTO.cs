using System;

namespace Dtos
{
    public class LogDTO
    {
        public string Source { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; private set; }
        public string Operation { get; set; }
        public string Context { get; set; }

        public LogDTO(string source, string type, string operation, string context)
        {
            Source = source;
            Type = type;
            DateTime = DateTime.Now;
            Operation = operation;
            Context = context;
            
        }
    }
}
