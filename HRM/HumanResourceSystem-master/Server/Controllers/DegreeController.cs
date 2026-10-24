using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreeController(IDegreeInterface genericRepositoryInterface) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() => Ok(await genericRepositoryInterface.GetAll());

        [HttpGet("all/employee/{trainingProgramId}")]
        public async Task<IActionResult> GetAllEmployee(int trainingProgramId)
        {
            if (trainingProgramId <= 0) return BadRequest("Invalid request send");
            return Ok(await genericRepositoryInterface.GetAllByTrainingProgramId(trainingProgramId));
        }

        [HttpGet("all/trainingprogram/{employeeId}")]
        public async Task<IActionResult> GetAllTrainingProgram(int employeeId)
        {
            if (employeeId <= 0) return BadRequest("Invalid request send");
            return Ok(await genericRepositoryInterface.GetAllByEmployeeId(employeeId));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid request send");
            return Ok(await genericRepositoryInterface.DeleteById(id));
        }

        [HttpGet("single/{trainingProgramId}&{employeeId}")]
        public async Task<IActionResult> GetById(int trainingProgramId, int employeeId)
        {
            if ((trainingProgramId <= 0) || (employeeId <= 0)) return BadRequest("Invalid request send");
            return Ok(await genericRepositoryInterface.GetById(trainingProgramId, employeeId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Degree model)
        {
            if (model is null) return BadRequest("Bad request made");
            return Ok(await genericRepositoryInterface.Insert(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Degree model)
        {
            if (model is null) return BadRequest("Bad request made");
            return Ok(await genericRepositoryInterface.Update(model));
        }
    }
}
