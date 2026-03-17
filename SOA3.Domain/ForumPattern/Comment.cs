namespace SOA3.Domain.ForumPattern
{
    public class Comment
    {
        private string _comment;
        private Person _author;
        private DateTime _UTCPostedAt;
        private List<Comment> _replies = new List<Comment>();

        public Comment(string comment, Person author)
        {
            _UTCPostedAt = DateTime.UtcNow;
            _comment = comment;
            _author = author;
        }

        public void AddComment(Comment comment) => _replies.Add(comment);
        public void RemoveComment(Comment comment) => _replies.Remove(comment);
        public List<Comment> GetComments() => _replies;

        public override string? ToString()
        {
            return $"{_UTCPostedAt.ToString()} - {_author.GetName}: {_comment}";
        }
    }
}