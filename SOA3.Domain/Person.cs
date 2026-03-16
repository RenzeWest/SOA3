namespace SOA3.Domain
{
    public class Person
    {
        private Guid _id;
        private string _name;
        private string _email;
        private List<NotificationChannel> _notificationPreferences = new List<NotificationChannel>();

    }
}
