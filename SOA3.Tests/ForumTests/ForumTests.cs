using SOA3.Domain;
using SOA3.Domain.ForumPattern;
using System;
using Xunit;
using System.Linq;

namespace SOA3.Tests.ForumTests
{
    [Collection("Sequential")]
    public class ForumTests
    {
        // Helper to create a Person
        private Person CreatePerson(string name, string email = "test@example.com")
            => new Person(name, email);

        [Fact]
        public void Comment_Should_AddAndRemoveReply()
        {
            var author = CreatePerson("Alice");
            var comment = new Comment("Main comment", author);

            var replyAuthor = CreatePerson("Bob");
            var reply = new Comment("Reply comment", replyAuthor);

            // Add reply
            comment.AddComment(reply);
            Assert.Single(comment.GetComments());
            Assert.Equal(reply, comment.GetComments().First());

            // Remove reply
            comment.RemoveComment(reply);
            Assert.Empty(comment.GetComments());
        }

        [Fact]
        public void DiscussionThread_Should_AddAndRemoveComments()
        {
            var thread = new DiscussionThread();
            var author = CreatePerson("Alice");

            var comment1 = new Comment("Comment 1", author);
            var comment2 = new Comment("Comment 2", author);

            // Add comments
            thread.AddComment(comment1);
            thread.AddComment(comment2);

            var allComments = thread.GetAllComments();
            Assert.Equal(2, allComments.Count);
            Assert.Contains(comment1, allComments);
            Assert.Contains(comment2, allComments);

            // Remove a comment
            thread.RemoveComment(comment1);
            allComments = thread.GetAllComments();
            Assert.Single(allComments);
            Assert.Contains(comment2, allComments);
        }

        [Fact]
        public void Forum_Should_AddDiscussionThread()
        {
            var forum = new Forum();
            var thread1 = new DiscussionThread();
            var thread2 = new DiscussionThread();

            forum.AddDiscussionThread(thread1);
            forum.AddDiscussionThread(thread2);

            // No public getter in Forum? Let's expose for test
            var threadsField = typeof(Forum)
                .GetField("_discussionThreads", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(forum) as System.Collections.Generic.List<DiscussionThread>;

            Assert.Equal(2, threadsField.Count);
            Assert.Contains(thread1, threadsField);
            Assert.Contains(thread2, threadsField);
        }

        [Fact]
        public void NestedReplies_Should_BeMaintained()
        {
            var author = CreatePerson("Alice");
            var comment = new Comment("Root comment", author);

            var reply1 = new Comment("Reply 1", CreatePerson("Bob"));
            var reply2 = new Comment("Reply 2", CreatePerson("Charlie"));

            comment.AddComment(reply1);
            reply1.AddComment(reply2); // Nested reply

            var firstLevelReplies = comment.GetComments();
            Assert.Single(firstLevelReplies);
            Assert.Equal(reply1, firstLevelReplies.First());

            var secondLevelReplies = firstLevelReplies.First().GetComments();
            Assert.Single(secondLevelReplies);
            Assert.Equal(reply2, secondLevelReplies.First());
        }
    }
}