namespace SOA3.Domain.NotificationPatterns
{
    public class NotificationPublisher
    {
        private List<INotificationSubscriber> _subscribers = new List<INotificationSubscriber>();

        public Subscribe(INotificationSubscriber subscriber)
        {

        }

        public Unsubscribe(INotificationSubscriber subscriber)
        {

        }

        public NotifySubscribers(Notification notification)
        {
            _subscribers.ForEach((subscriber) => subscriber.Update(notification));
        }
    }
}
