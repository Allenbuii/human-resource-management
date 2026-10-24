using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodicEvaluationController : GenericController<PeriodicEvaluation>
    {
        private readonly IPeriodicEvaluationInterface _periodicEvaluationInterface;

        public PeriodicEvaluationController(IPeriodicEvaluationInterface periodicEvaluationInterface)
            : base(periodicEvaluationInterface)
        {
            _periodicEvaluationInterface = periodicEvaluationInterface;
        }

        [HttpGet("all/employee/{id}")]
        public async Task<IActionResult> GetByEmployeeId(int id)
        {
            if (id <= 0) return BadRequest("Bad request made");
            return Ok(await _periodicEvaluationInterface.GetAllByEmployeeId(id));
        }

        [HttpGet("closest/{id}")]
        public async Task<IActionResult> GetClosestByEmployeeId(int id)
        {
            if (id <= 0) return BadRequest("Bad request made");
            return Ok(await _periodicEvaluationInterface.GetClosestByEmployeeId(id));
        }

        [HttpPut("send-email/{message}")]
        public async Task<IActionResult> SendEmail(string message, PeriodicEvaluation model)
        {
            if (model is null) return BadRequest("Bad request made");
            return Ok(await _periodicEvaluationInterface.SendEmail(message, model));
        }
    }
}
