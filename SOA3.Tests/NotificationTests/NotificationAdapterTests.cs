using Moq;
using SOA3.Domain;
using SOA3.Domain.NotificationPatterns;
using SOA3.Domain.NotificationPatterns.Email;
using SOA3.Domain.NotificationPatterns.Slack;

namespace SOA3.Tests.NotificationTests
{
    [Collection("Sequential")]
    public class NotificationAdapterTests
    {
        private readonly Mock<EmailClient> _emailClientMock = new Mock<EmailClient>();
        private readonly Mock<SlackClient> _slackClientMock = new Mock<SlackClient>();

        private readonly EmailAdapter _emailAdapter;
        private readonly SlackAdapter _slackAdapter;

        public NotificationAdapterTests()
        {
            _emailClientMock = new Mock<EmailClient>();
            _slackClientMock = new Mock<SlackClient>();

            _emailAdapter = new EmailAdapter(_emailClientMock.Object);
            _slackAdapter = new SlackAdapter(_slackClientMock.Object);
        }

        // TC-NotificationAdapter-001
        [Fact]
        public void Email_Should_Be_Sent_When_Preference_Is_Email()
        {
            var person = new Person("John", "john@test.com",
                new List<NotificationChannel> { NotificationChannel.Email });

            var notification = new Notification(DateTime.Now, "Test message");

            _emailAdapter.Update(notification, new List<Person> { person });

            _emailClientMock.Verify(x =>
                x.SendEmail(person.Email, notification.DateTime, notification.GetMessage()),
                Times.Once);
        }

        // TC-NotificationAdapter-002
        [Fact]
        public void Email_Should_Be_Sent_To_Multiple_Recipients()
        {
            var persons = new List<Person>
            {
                new Person("John", "john@test.com",
                    new List<NotificationChannel> { NotificationChannel.Email }),
                new Person("Jane", "jane@test.com",
                    new List<NotificationChannel> { NotificationChannel.Email })
            };

            var notification = new Notification(DateTime.Now, "Test message");

            _emailAdapter.Update(notification, persons);

            _emailClientMock.Verify(x => x.SendEmail(
                It.IsAny<string>(),
                notification.DateTime,
                notification.GetMessage()),
                Times.Exactly(2));
        }

        // TC-NotificationAdapter-003
        [Fact]
        public void Email_Should_Not_Be_Sent_When_Preference_Is_Not_Email()
        {
            var person = new Person("John", "john@test.com",
                new List<NotificationChannel> { NotificationChannel.Slack });

            var notification = new Notification(DateTime.Now, "Test message");

            _emailAdapter.Update(notification, new List<Person> { person });

            _emailClientMock.Verify(x =>
                x.SendEmail(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()),
                Times.Never);
        }

        // TC-NotificationAdapter-004
        [Fact]
        public void Slack_Should_Only_Be_Sent_When_Preference_Is_Slack()
        {
            var personWithSlack = new Person("John", "john@test.com",
                new List<NotificationChannel> { NotificationChannel.Slack });

            var personWithoutSlack = new Person("Jane", "jane@test.com",
                new List<NotificationChannel> { NotificationChannel.Email });

            var notification = new Notification(DateTime.Now, "Test message");

            _slackAdapter.Update(notification, new List<Person>
            {
                personWithSlack,
                personWithoutSlack
            });

            _slackClientMock.Verify(x =>
                x.SendSlackMessage(personWithSlack.GetName(), notification.DateTime, notification.GetMessage()),
                Times.Once);

            _slackClientMock.Verify(x =>
                x.SendSlackMessage(personWithoutSlack.GetName(), It.IsAny<DateTime>(), It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public void ShouldSendEmail_Returns_True_When_Preference_Is_Email()
        {
            var person = new Person("John", "john@test.com",
                new List<NotificationChannel> { NotificationChannel.Email });

            var adapter = new EmailAdapter(new Mock<EmailClient>().Object);

            var result = adapter.ShouldSendEmail(person);

            Assert.True(result);
        }

        [Fact]
        public void ShouldSendEmail_Returns_False_When_Preference_Is_Not_Email()
        {
            var person = new Person("John", "john@test.com",
                new List<NotificationChannel> { NotificationChannel.Slack });

            var adapter = new EmailAdapter(new Mock<EmailClient>().Object);

            var result = adapter.ShouldSendEmail(person);

            Assert.False(result);
        }

        [Fact]
        public void ShouldSendMessage_Returns_True_When_Preference_Is_Slack()
        {
            var person = new Person("John", "john@test.com",
                new List<NotificationChannel> { NotificationChannel.Slack });

            var adapter = new SlackAdapter(new Mock<SlackClient>().Object);

            var result = adapter.ShouldSendMessage(person);

            Assert.True(result);
        }

        [Fact]
        public void ShouldSendMessage_Returns_False_When_Preference_Is_Not_Slack()
        {
            var person = new Person("John", "john@test.com",
                new List<NotificationChannel> { NotificationChannel.Email });

            var adapter = new SlackAdapter(new Mock<SlackClient>().Object);

            var result = adapter.ShouldSendMessage(person);

            Assert.False(result);
        }
    }
}