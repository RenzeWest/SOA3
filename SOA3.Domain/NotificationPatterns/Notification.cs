namespace SOA3.Domain.NotificationPatterns
{
    public class Notification
    {
        private Guid _id = new Guid();
        private string _message;
        public DateTime DateTime { get; private set; } = DateTime.UtcNow;
        public Notification(string message) => _message = message;
        public string GetMessage() => _message;
    }
}