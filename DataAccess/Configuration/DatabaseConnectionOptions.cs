namespace DataAccess.Configuration
{
    public sealed class DatabaseConnectionOptions
    {
        public const string SectionName = "ConnectionStrings";

        public string? Ilfrm { get; set; }

        public string? Intf { get; set; }
    }
}
