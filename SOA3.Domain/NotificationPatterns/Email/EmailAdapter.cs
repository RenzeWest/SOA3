namespace SOA3.Domain.NotificationPatterns.Email
{
    public class EmailAdapter : IEmailNotifier
    {
        private readonly EmailClient _emailClient;
        public EmailAdapter(EmailClient emailClient) => _emailClient = emailClient;

        public bool ShouldSendEmail(Person person) => person.NotificationPreferences.Contains(NotificationChannel.Email);
        public void Update(Notification notification, List<Person> recipients)
        {
            foreach (var person in recipients)
            {
                if (ShouldSendEmail(person)) _emailClient.SendEmail(person.Email, notification.DateTime, notification.GetMessage());
            }
        } 
    }
}
