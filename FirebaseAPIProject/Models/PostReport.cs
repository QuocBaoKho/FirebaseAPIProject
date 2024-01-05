namespace FirebaseAPIProject.Models
{
    public class PostReport
    {
        public string Id { get; set; }
        public string Report { get; set; }
        public string Reported { get; set; }
        public string Reporter { get; set; }
        public string Username { get; set; }

        public PostReport(string id, string report, string reported, string reporter, string username)
        {
            Id = id;
            Report = report;
            Reported = reported;
            Reporter = reporter;
            Username = username;
        }
        public override bool Equals(object? obj)
        {
            var postReportItem = obj as PostReport;
            if (postReportItem == null)
                return false;
            return this.Id.Equals(postReportItem.Id);
        }
    }
}
