namespace ApiHost.TaskManagement
{
    public class Dispatcher
    {
        private readonly Action<string> _logger;
        private readonly Queue<Task> _queue;

        public Dispatcher(Action<string> logger, Queue<Task> queue)
        {
            _logger = logger;
            _queue = queue;
        }

        internal async Task AccessDatabase(Task task)
        {
            //_logger?.Invoke($"Task {taskId} is requesting access to the database.");

            //// Request access by waiting to enter the semaphore.
            //_semaphore.Wait();
            //try
            //{
            //    _logger?.Invoke($"Task {taskId} has entered the database.");
            //    await Task.Delay(1000);
            //}
            //finally
            //{
            //    // Release the semaphore.
            //    _logger?.Invoke($"Task {taskId} is leaving the database.");
            //    _semaphore.Release();
            //}
        }
    }
}
