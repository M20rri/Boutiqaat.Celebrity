using System;
using System.Threading.Tasks;
using Boutiqaat.Celebrity.Core.Request;
using Boutiqaat.Celebrity.Repository.Teacher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boutiqaat.Celebrity.Api.Controllers
{
    [ApiController]
    public class AuthorizeController : Controller
    {
        private readonly IAuthRepository _authRepostory;
        private readonly ILogger _logger;

        public AuthorizeController(IAuthRepository authRepostory, ILogger<TeacherController> logger)
        {
            _authRepostory = authRepostory;
            _logger = logger;

        }

        [HttpPost, Route("api/Auth/Login")]
        public async Task<IActionResult> Login(TeacherRequest model)
        {
            try
            {
                var auth = await _authRepostory.Authenticate(model.Email, model.Password);

                if (auth == null)
                {
                    return NotFound("Invalid User");
                }

                return Ok(auth);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return BadRequest(new { Code = 0, Message = ex.Message });
            }
        }

    }
}