namespace FirebaseAPIProject.Models
{
    public class Comments
    {
        public string CommentId { get; set; }
        public string Comment { get; set; }
        public string Publisher { get; set; }
        public string Postid { get; set; }
        public Comments(string commentId, string comment, string publisher, string postid)
        {
            CommentId = commentId;
            Comment = comment;
            Publisher = publisher;
            Postid = postid;
        }
        public override bool Equals(object obj)
        {
            var postItem = obj as Comments;
            if (postItem == null)
                return false;
            return this.CommentId.Equals(postItem.CommentId);
        }
    }
}
