﻿namespace DaHo.M151.DataFormatValidator.Models.ServiceModels
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}