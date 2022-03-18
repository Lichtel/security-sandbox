using System;

namespace TokenProvider.Model
{
    public class Token
    {
        public string Jwt { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}