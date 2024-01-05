namespace FirebaseAPIProject.Models
{
    public class User
    {
        public int Activity { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }

        public string Id { get; set; }
        public string Imageurl { get; set; }
        public string Phonenumber { get; set; }


        public string UserName { get; set; }
        public string Token { get; set; }
        public string Website { get; set; }
        public User(int activity, string bio, string email, string fullname, string id, string imageurl, string phonenumber, string userName, string token, string website)
        {
            Activity = activity;
            Bio = bio;
            Email = email;
            Fullname = fullname;
            Id = id;
            Imageurl = imageurl;
            Phonenumber = phonenumber;
            UserName = userName;
            Token = token;
            Website = website;
        }
       public override bool Equals(object? obj)
        {
            var postItem = obj as User;
            if (postItem == null)
                return false;
            return this.Id.Equals(postItem.Id);
        }
        public override int GetHashCode()
{
    return this.Id.GetHashCode();
}
    }
}
