namespace TokenProvider.Dto
{
    public class TokenResponse
    {
        public string Jwt { get; set; }

        public long ExpiresAt { get; set; }
    }
}