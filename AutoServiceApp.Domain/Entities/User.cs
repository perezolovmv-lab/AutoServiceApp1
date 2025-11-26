using AutoServiceApp.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoServiceApp.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}