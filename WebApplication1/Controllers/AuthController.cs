using Microsoft.AspNetCore.Mvc;
using WebApplication1.Jwt;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        [HttpGet("{login}/{password}")]
        [HttpPost("{login}/{password}")]
        public IActionResult Auth(string login, string password)
        {
            if (login == "login" &&
                password == "passw")
            {
            var jwtProcessor = new JwtConfiguration();

            var generatedFreshToken = jwtProcessor.GenerateToken(1, login);

            return Ok(generatedFreshToken);}


            return Unauthorized();
        }
        
        [HttpPost]
        public IActionResult AuthRightWay([FromBody] AuthModel credentials)
        {
            if (credentials != null &&
                credentials.Login == "login" &&
                credentials.Password == "passw")
            {
                var jwtProcessor = new JwtConfiguration();

                var generatedFreshToken = jwtProcessor.GenerateToken(1, credentials.Login);

                return Ok(generatedFreshToken);
            }

            return Unauthorized();
        }
    }
}
