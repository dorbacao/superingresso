﻿namespace Web.Api.Infraestrutura.Authentication
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string DurationInDays { get; set; }
        public string RefreshTokenExpiration { get; set; }
    }
}
