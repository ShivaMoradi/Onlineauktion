﻿namespace Server.Dtos.User
{
    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        // public string Role { get; set; } = string.Empty; <-- Is being set by default in Model.
    }
}