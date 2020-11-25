using System;
using System.Threading.Tasks;
using Boutiqaat.Celebrity.Core.Request;
using Boutiqaat.Celebrity.Repository.Teacher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Boutiqaat.Celebrity.Api.Controllers
{
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepostory _teacherRepostory;
        private readonly ILogger _logger;

        public TeacherController(ITeacherRepostory teacherRepostory, ILogger<TeacherController> logger)
        {
            _teacherRepostory = teacherRepostory;
            _logger = logger;

        }

        [HttpPost, Route("api/Teacher/Add")]
        public async Task<IActionResult> Add(TeacherRequest model)
        {
            try
            {
                string jsonStr = JsonConvert.SerializeObject(model);
                return Ok(await _teacherRepostory.AddTeacherAsync(jsonStr));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return BadRequest(new { Code = 0, Message = ex.Message });
            }
        }

        [HttpPost, Route("api/Teacher/Edit")]
        public async Task<IActionResult> Edit(TeacherRequest model)
        {
            try
            {
                string jsonStr = JsonConvert.SerializeObject(model);
                return Ok(await _teacherRepostory.EditTeacherAsync(jsonStr));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return BadRequest(new { Code = 0, Message = ex.Message });
            }
        }

        [HttpPost, Route("api/Teacher/Delete")]
        public async Task<IActionResult> Delete(TeacherRequest model)
        {
            try
            {
                string jsonStr = JsonConvert.SerializeObject(model);
                return Ok(await _teacherRepostory.DeleteTeacherAsync(jsonStr));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return BadRequest(new { Code = 0, Message = ex.Message });
            }
        }

        [HttpGet, Route("api/Teacher/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                TeacherRequest teacher = new TeacherRequest() { Status = "GETALL" };
                string jsonStr = JsonConvert.SerializeObject(teacher);
                return Ok(await _teacherRepostory.GetTeachersAsync(jsonStr));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return BadRequest(new { Code = 0, Message = ex.Message });
            }
        }

        [HttpPost, Route("api/Teacher/GetById")]
        public async Task<IActionResult> GetById(TeacherRequest teacher)
        {
            try
            {
                string jsonStr = JsonConvert.SerializeObject(teacher);
                return Ok(await _teacherRepostory.GetTeacherByIdAsync(jsonStr));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return BadRequest(new { Code = 0, Message = ex.Message });
            }
        }

    }
}