using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(new
            {
                Id = 1,
                Avatar = "myprofile.jpg",
                Login = "George",
                FirstName = "Ivan",
                LasName = "Petrov",
                Age = "21",
                City = "Москва",
                Work = "Университет Синергия"
            });
        }

    }
}
