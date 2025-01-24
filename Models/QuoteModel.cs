namespace QuoteSharing_WebApplication.Models
{
    public class QuoteModel
    {
        public int QuoteID { get; set; }
        public string QuoteWriter { get; set; }
        public string QuoteText { get; set; }
        public string UploadedEmail { get; set; }
        public bool IsDeleted { get; set; }
    }
}
