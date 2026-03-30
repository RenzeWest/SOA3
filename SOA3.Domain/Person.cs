using SOA3.Domain.NotificationPatterns;

namespace SOA3.Domain
{
    public class Person
    {
        private Guid _id;
        private string _name;
        public string Email { get; private set; }

        public Person(string name, string email)
        {
            _name = name;
            Email = email;
        }

        public List<NotificationChannel> NotificationPreferences { get; private set; } = new List<NotificationChannel>();

        public string GetName() => _name;   

    }
}
