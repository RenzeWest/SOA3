namespace SOA3.Domain.NotificationPatterns
{
    public interface INotificationSubscriber
    {
        void Update(Notification notification, List<Person> recipients);
    }
}
