﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebBlog.Application.Dtos
{
    public class RegistrationDto
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
