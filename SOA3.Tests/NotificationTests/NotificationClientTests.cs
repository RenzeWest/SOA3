using SOA3.Domain.NotificationPatterns.Email;
using SOA3.Domain.NotificationPatterns.Slack;

namespace SOA3.Tests.NotificationTests
{
    [Collection("Sequential")]
    public class NotificationClientTests
    {
        private string CaptureConsoleOutput(Action action)
        {
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                action();
                return writer.ToString();
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Email_Should_Be_Send()
        {
            // Arrange
            var now = DateTime.UtcNow;

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            EmailClient client = new EmailClient();
            var consoleOutput = CaptureConsoleOutput(() => client.SendEmail("john@gmail.com", now, "messageSend"));

            // Assert
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
            var consoleOutput = CaptureConsoleOutput(() => client.SendSlackMessage("john", now, "messageSend"));

            // Assert
            Assert.Equal($"[Send Slack Notification to john] notified at {now} (UTC): messageSend\r\n", consoleOutput);
        }
    }
}
