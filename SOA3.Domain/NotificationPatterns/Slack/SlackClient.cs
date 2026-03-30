namespace SOA3.Domain.NotificationPatterns.Slack
{
    public class SlackClient
    {
        public static void SendSlackMessage(string username, DateTime notifiedAtDateTime, string message) => Console.WriteLine($"[Send Email Notification to {username}] notified at {DateTime.UtcNow} (UTC): {message}");
    }
}
