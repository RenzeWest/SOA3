namespace SOA3.Domain.NotificationPatterns
{
    public class NotificationPublisher
    {
        private List<INotificationSubscriber> _subscribers = new List<INotificationSubscriber>();
        private List<Person> _recipients = new List<Person>();

        public void Subscribe(INotificationSubscriber subscriber) => _subscribers.Add(subscriber);
        public void Unsubscribe(INotificationSubscriber subscriber) => _subscribers.Remove(subscriber);
        public void NotifySubscribers(Notification notification) => _subscribers.ForEach((subscriber) => subscriber.Update(notification, _recipients));
        public void AddRecipient(Person recipient) => _recipients.Add(recipient);
        public void RemoveRecipient(Person recipient) => _recipients.Remove(recipient);
    }
}
