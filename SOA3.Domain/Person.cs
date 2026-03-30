namespace SOA3.Domain
{
    public class Person
    {
        public Guid Id { get; } = Guid.NewGuid();
        public required string Name { get; init; }
        public required string Email { get; init; }
        //private List<NotificationChannel> _notificationPreferences = new List<NotificationChannel>();
    }
}
