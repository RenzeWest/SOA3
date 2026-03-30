using SOA3.Domain;
using SOA3.Domain.NotificationPatterns.Email;
using SOA3.Domain.NotificationPatterns.Slack;

namespace SOA3.Tests.NotificationTests
{
    public class NotificationClientTests
    {
        [Fact]
        public void Email_Should_Be_Send()
        {
            // Arrange
            var now = DateTime.UtcNow;

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            EmailClient client = new EmailClient();
            client.SendEmail("john@gmail.com", now, "messageSend");

            // Assert
            var consoleOutput = stringWriter.ToString();
            Assert.Equal($"[Send Email Notification to john@gmail.com] notified at {now} (UTC): messageSend\r\n", consoleOutput);
        }

        [Fact]
        public void Slack_Should_Be_Send()
        {
            // Arrange
            var now = DateTime.UtcNow;

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            SlackClient client = new SlackClient();
            client.SendSlackMessage("john", now, "messageSend");

            // Assert
            var consoleOutput = stringWriter.ToString();
            Assert.Equal($"[Send Slack Notification to john] notified at {now} (UTC): messageSend\r\n", consoleOutput);
        }
    }
}
