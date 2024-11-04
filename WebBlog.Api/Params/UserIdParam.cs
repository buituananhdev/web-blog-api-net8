using Microsoft.AspNetCore.Mvc;

namespace WebBlog.Api.Params
{
    public class UserIdParam
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
