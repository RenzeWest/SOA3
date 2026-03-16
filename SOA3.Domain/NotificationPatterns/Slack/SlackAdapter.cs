namespace SOA3.Domain.NotificationPatterns.Slack
{
    public class SlackAdapter : ISlackNotifier
    {
        public void Update(Notification notification)
        {
            SlackClient.SendSlackMessage();
        }
    }
}
