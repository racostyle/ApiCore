namespace FormsUtils.Logging
{
    public interface IRTBLogger
    {
        void Log(string message);
        void Log(string message, bool newSection);
        void Log(string message, TextTag tag);
        void Log(string message, TextTag tag, bool newSection);
    }
}
