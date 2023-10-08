namespace UsersCrud.Auth
{
    public class TokenSettings
    {
        public string ValidAudience { get; set; }

        public string ValidIssuer { get; set; }

        public string SecretKey { get; set; }
    }
}