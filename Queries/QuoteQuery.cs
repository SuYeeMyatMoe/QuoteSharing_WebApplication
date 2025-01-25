namespace QuoteSharing_WebApplication.Queries
{
    public class QuoteQuery
    {
        public static string GetQuoteListQuery { get; } =
       @"SELECT [QuoteID]
      ,[QuoteWriter]
      ,[QuoteText]
      ,[UploadedEmail]
      ,[IsDeleted]
  FROM [QuoteSharing_DB].[dbo].[Tbl_QuoteSharing]
  WHERE IsDeleted = @IsDeleted ORDER BY QuoteID DESC";

        public static string CreateBlogQuery { get; } =
            @"INSERT INTO Tbl_QuoteSharing
(QuoteWriter,QuoteText,UploadedEmail) VALUES (@QuoteWriter,@QuoteText,@UploadedEmail)";

        public static string GetQuoteByIdQuery { get; } =
        @"SELECT QuoteID, QuoteWriter, QuoteText, UploadedEmail, IsDeleted
FROM Tbl_QuoteSharing WHERE IsDeleted = @IsDeleted AND QuoteID = @QuoteID";
    }
}
