namespace ComplaintPortalEntities
{
    public sealed class ApplicationSettings
    {
        public DatabaseSettings DatabaseSettings { get; set; } = new();
        public JwtSettings JwtSettings { get; set; } = new();
    }

    public sealed class DatabaseSettings
    {
        public string? DataBase_name { get; set; }
        //public string? Collection_Name { get; set; }
        public string? Connection_String { get; set; }
    }

    public sealed class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string SecretKey { get; init; } = null!;
        public int ExpiryMinutes { get; init; }
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public int RefreshTokenExpirydays { get; init; }
    }
}
