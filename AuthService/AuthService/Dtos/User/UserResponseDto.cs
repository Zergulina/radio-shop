﻿namespace AuthService.Dtos.User
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
    }
}
