﻿using WebBog.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace WebBog.Application.Exceptions
{
    public class EmailExistedException: CustomException
    {
        public EmailExistedException() : base(StatusCodes.Status409Conflict, ErrorCodeEnum.ExistedEmail, "Email already exists")
        {
        }
    }
}
