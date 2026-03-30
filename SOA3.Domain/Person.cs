using SOA3.Domain.NotificationPatterns;

namespace SOA3.Domain
{
    public class Person
    {
        private Guid _id;
        private string _name;
        public string Email { get; private set; }
        public List<NotificationChannel> NotificationPreferences { get; private set; }

        public Person(string name, string email)
        {
            _name = name;
            Email = email;
            NotificationPreferences = new List<NotificationChannel>();
        }

        public Person(string name, string email, List<NotificationChannel> notificationPreferences)
        {
            _name = name;
            Email = email;
            NotificationPreferences = notificationPreferences;
        }

        public void AddNotificationPreference(NotificationChannel channel) => NotificationPreferences.Add(channel);
        public string GetName() => _name;   

    }
}
