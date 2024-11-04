﻿namespace WebBlog.Application.Dtos
{
    public class CommentDto
    {
        public string CommentId { get; set; } = null!;

        public string CommentText { get; set; } = null!;

        public string? UserId { get; set; }

        public string? PostId { get; set; }

        public long? Timestamp { get; set; }

        public string? CommentType { get; set; }
    }
}
