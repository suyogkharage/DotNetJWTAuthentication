using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // Public endpoint open to all
        [HttpGet("public")]
        public IActionResult GetPublic() => Ok("This is a public endpoint. No token required.");

        // Protected endpoint for every authenticated user
        [Authorize]
        [HttpGet("protected")]
        public IActionResult GetProtected() => Ok("This is a protected endpoint. Valid JWT required.");

        // Only Admin role can access this endpoint
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            return Ok(new[] { "User1", "User2", "User3" });
        }

        // Both Admin and User can access
        [HttpGet("profile")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetProfile()
        {
            return Ok(new { Name = "John Doe", Role = "User" });
        }
    }
}
