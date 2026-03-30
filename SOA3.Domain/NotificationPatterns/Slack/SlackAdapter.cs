namespace SOA3.Domain.NotificationPatterns.Slack
{
    public class SlackAdapter : ISlackNotifier
    {
        private SlackClient _slackClient = new SlackClient();
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
