﻿namespace User.API.DTOs
{
    public class AuthenticationModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
