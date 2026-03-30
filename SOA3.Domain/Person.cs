namespace SOA3.Domain
{
    public class Person
    {
        private Guid _id;
        private string _name;
        private string _email;

        public Person(string name, string email)
        {
            _name = name;
            _email = email;
        }

        //private List<NotificationChannel> _notificationPreferences = new List<NotificationChannel>();

        public string GetName() => _name;   

    }
}
