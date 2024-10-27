using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SozlukApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        //public Guid? UserId => new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        public Guid? UserId =>
       Guid.TryParse(HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)
           ? userId
           : null;
    }
}
