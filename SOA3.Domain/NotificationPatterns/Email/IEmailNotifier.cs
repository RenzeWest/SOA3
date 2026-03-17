namespace SOA3.Domain.NotificationPatterns.Email
{
    public interface IEmailNotifier : INotificationSubscriber
    {
        bool ShouldSendEmail(Person person);
    }
}
