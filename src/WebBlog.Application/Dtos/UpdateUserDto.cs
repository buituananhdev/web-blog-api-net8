﻿using System.Text.Json.Serialization;

namespace WebBlog.Application.Dtos
{
    public class UpdateUserDto
    {
        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
