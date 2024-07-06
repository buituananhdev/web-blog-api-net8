using Microsoft.AspNetCore.Mvc;

namespace WebBog.Api.Params
{
    public class UserIdParam
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
