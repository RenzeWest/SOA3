namespace SOA3.Domain.NotificationPatterns.Slack
{
    public class SlackAdapter : ISlackNotifier
    {
        public bool ShouldSendMessage(Person person) => false; // person.GetNotificationChannels().Contains(Slack);

        public void Update(Notification notification)
        {
            SlackClient.SendSlackMessage();
        }
    }
}
