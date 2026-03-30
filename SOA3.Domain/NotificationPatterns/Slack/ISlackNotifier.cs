namespace SOA3.Domain.NotificationPatterns.Slack
{
    public interface ISlackNotifier : INotificationSubscriber
    {
        bool ShouldSendMessage(Person person);
    }
}
