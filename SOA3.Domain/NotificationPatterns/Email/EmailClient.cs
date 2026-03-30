namespace SOA3.Domain.NotificationPatterns.Email
{
    public class EmailClient
    {
        public static void SendEmail(string emailAddress, DateTime notifiedAtDateTime, string message) => Console.WriteLine($"[Send Email Notification to {emailAddress}] notified at {DateTime.UtcNow} (UTC): {message}");
    }
}
