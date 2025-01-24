namespace QuoteSharing_WebApplication.Configs
{
    public class DbHelper
    {
        public static string ConnectionString { get; } =
        @"Server=.;Database=QuoteSharing_DB;User ID=sa;Password=su$2003;TrustServerCertificate=True;Max Pool Size=10;";
    }
}
