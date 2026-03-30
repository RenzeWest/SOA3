namespace SOA3.Domain.NotificationPatterns.Slack
{
    public class SlackAdapter : ISlackNotifier
    {
        private readonly SlackClient _slackClient;
        public SlackAdapter(SlackClient slackClient) => _slackClient = slackClient;
        public bool ShouldSendMessage(Person person) => person.NotificationPreferences.Contains(NotificationChannel.Slack); 
        public void Update(Notification notification, List<Person> recipients)
        {
            foreach (var person in recipients)
            {
                if (ShouldSendMessage(person)) _slackClient.SendSlackMessage(person.GetName(), notification.DateTime, notification.GetMessage());
            }
        }
    }
}
