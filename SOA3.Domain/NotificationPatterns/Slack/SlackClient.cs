namespace SOA3.Domain.NotificationPatterns.Slack
{
    public class SlackClient
    {
        public void SendSlackMessage(string username, DateTime notifiedAtDateTime, string message) => Console.WriteLine($"[Send Slack Notification to {username}] notified at {DateTime.UtcNow} (UTC): {message}");
    }
}
