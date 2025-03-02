namespace QuoteSharing_WebApplication.Queries
{
    public class QuoteQuery
    {
        //{ get; } to be able to see reference in other classes
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

        public static string UpdateQuoteQuery { get; } =
        @"UPDATE Tbl_QuoteSharing SET QuoteWriter = @QuoteWriter, QuoteText = @QuoteText,
UploadedEmail = @UploadedEmail WHERE QuoteID = @QuoteID AND IsDeleted = @IsDeleted";

        public static string DeleteQuoteQuery { get; } =
       @"UPDATE Tbl_QuoteSharing SET IsDeleted = @IsDeleted WHERE QuoteID = @QuoteID";

        public static string SearchQuote { get; } =
        @"declare @value NVARCHAR(50) = @value;
SELECT TOP (1000) [QuoteID]
      ,[QuoteWriter]
      ,[QuoteText]
      ,[UploadedEmail]
      ,[IsDeleted]
  FROM [QuoteSharing_DB].[dbo].[Tbl_QuoteSharing]
  WHERE (QuoteWriter LIKE '%' + @value + '%'
  OR QuoteText LIKE '%' + @value + '%'
  OR UploadedEmail LIKE '%' + @value + '%')
  AND IsDeleted = 0";
    }   
}
