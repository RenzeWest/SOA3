using Moq;
using SOA3.Domain;
using SOA3.Domain.NotificationPatterns;

namespace SOA3.Tests.NotificationTests
{
    [Collection("Sequential")]
    public class NotificationPublisherTest
    {
        [Fact]
        public void Subscribe_Should_Notify_Subscribed_Subscriber()
        {
            // Arrange
            var publisher = new NotificationPublisher();
            var subscriberMock = new Mock<INotificationSubscriber>();
            Notification notification = null!;

            publisher.Subscribe(subscriberMock.Object);

            // Act
            publisher.NotifySubscribers(notification);

            // Assert
            subscriberMock.Verify(
                s => s.Update(notification, It.IsAny<List<Person>>()),
                Times.Once);
        }

        [Fact]
        public void Unsubscribe_Should_Not_Notify_Unsubscribed_Subscriber()
        {
            // Arrange
            var publisher = new NotificationPublisher();
            var subscriberMock = new Mock<INotificationSubscriber>();
            Notification notification = null!;

            publisher.Subscribe(subscriberMock.Object);
            publisher.Unsubscribe(subscriberMock.Object);

            // Act
            publisher.NotifySubscribers(notification);

            // Assert
            subscriberMock.Verify(
                s => s.Update(It.IsAny<Notification>(), It.IsAny<List<Person>>()),
                Times.Never);
        }

        [Fact]
        public void NotifySubscribers_Should_Notify_All_Subscribers()
        {
            // Arrange
            var publisher = new NotificationPublisher();
            var subscriberMock1 = new Mock<INotificationSubscriber>();
            var subscriberMock2 = new Mock<INotificationSubscriber>();
            Notification notification = null!;

            publisher.Subscribe(subscriberMock1.Object);
            publisher.Subscribe(subscriberMock2.Object);

            // Act
            publisher.NotifySubscribers(notification);

            // Assert
            subscriberMock1.Verify(
                s => s.Update(notification, It.IsAny<List<Person>>()),
                Times.Once);

            subscriberMock2.Verify(
                s => s.Update(notification, It.IsAny<List<Person>>()),
                Times.Once);
        }

        [Fact]
        public void AddRecipient_Should_Send_Recipient_List_To_Subscriber()
        {
            // Arrange
            var publisher = new NotificationPublisher();
            var subscriberMock = new Mock<INotificationSubscriber>();
            Person recipient = null!;
            Notification notification = null!;

            publisher.Subscribe(subscriberMock.Object);
            publisher.AddRecipient(recipient);

            // Act
            publisher.NotifySubscribers(notification);

            // Assert
            subscriberMock.Verify(
                s => s.Update(
                    notification,
                    It.Is<List<Person>>(recipients =>
                        recipients.Count == 1 &&
                        recipients.Contains(recipient))),
                Times.Once);
        }

        [Fact]
        public void RemoveRecipient_Should_Remove_Recipient_From_List_Before_Notifying()
        {
            // Arrange
            var publisher = new NotificationPublisher();
            var subscriberMock = new Mock<INotificationSubscriber>();
            Person recipient = null!;
            Notification notification = null!;

            publisher.Subscribe(subscriberMock.Object);
            publisher.AddRecipient(recipient);
            publisher.RemoveRecipient(recipient);

            // Act
            publisher.NotifySubscribers(notification);

            // Assert
            subscriberMock.Verify(
                s => s.Update(
                    notification,
                    It.Is<List<Person>>(recipients => !recipients.Contains(recipient))),
                Times.Once);
        }
    }
}