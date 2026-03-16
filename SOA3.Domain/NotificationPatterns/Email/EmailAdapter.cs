namespace SOA3.Domain.NotificationPatterns.Email
{
    public class EmailAdapter : IEmailNotifier
    {
        public void Update(Notification notification)
        {
            EmailClient.SendEmail();
        }
    }
}
