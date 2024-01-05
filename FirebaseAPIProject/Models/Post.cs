namespace FirebaseAPIProject.Models
{
    public class Post
    {
        public long CreatedTime { get; set; }
        public string Description { get; set; }
        public string PostType { get; set; }
        public string Postid { get; set; }
        public string Postimage { get; set; }
        public string Postvid { get; set; }
        public string Publisher { get; set; }
        public Post(long createdTime, string description, string postType, string postid, string postimage, string postvid, string publisher)
        {
            CreatedTime = createdTime;
            Description = description;
            PostType = postType;
            Postid = postid;
            Postimage = postimage;
            Postvid = postvid;
            Publisher = publisher;
        }
        public override bool Equals(object? obj)
        {
            var postItem = obj as Post;
            if (postItem == null)
                return false;
            return this.Postid.Equals(postItem.Postid);
        }
       
    }
}
