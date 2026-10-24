using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : GenericController<Branch>
    {
        private readonly IBranchInterface _branchInterface;

        public BranchController(IBranchInterface branchInterface)
            : base(branchInterface)
        {
            _branchInterface = branchInterface;
        }

        [HttpGet("all/employee/{id}")]
        public async Task<IActionResult> GetAllEmployeeInBranch(int id)
        {
            if (id <= 0) return BadRequest("Bad request made");
            return Ok(await _branchInterface.GetAllEmployeeInBranch(id));
        }

        [HttpGet("all/leader/{id}")]
        public async Task<IActionResult> GetAllNonLeader(int id)
        {
            if (id <= 0) return BadRequest("Bad request made");
            return Ok(await _branchInterface.GetAllNonLeader(id));
        }
    }
}
