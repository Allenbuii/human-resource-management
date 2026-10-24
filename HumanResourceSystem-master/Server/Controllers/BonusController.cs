using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : GenericController<Bonus>
    {
        private readonly IBonusInterface _bonusInterface;

        public BonusController(IBonusInterface bonusInterface)
            : base(bonusInterface)
        {
            _bonusInterface = bonusInterface;
        }

        [HttpGet("all/employee/{id}")]
        public async Task<IActionResult> GetByEmployeeId(int id)
        {
            if (id <= 0) return BadRequest("Bad request made");
            return Ok(await _bonusInterface.GetAllByEmployeeId(id));
        }
    }
}
