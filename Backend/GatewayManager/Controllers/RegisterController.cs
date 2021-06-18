using Microsoft.AspNetCore.Mvc;

namespace GatewayManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController
    {
        [HttpPost("/register")]
        public IActionResult Register()
        {
            return null;
        }
    }
}