using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTransferController : GenericController<EmployeeTransfer>
    {
        private readonly IEmployeeTransferInterface _employeeTransferInterface;

        public EmployeeTransferController(IEmployeeTransferInterface employeeTransferInterface)
            : base(employeeTransferInterface)
        {
            _employeeTransferInterface = employeeTransferInterface;
        }

        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetByEmployeeId(int id)
        {
            if (id <= 0) return BadRequest("Bad request made");
            return Ok(await _employeeTransferInterface.GetByEmployeeId(id));
        }

        [HttpGet("all/branch/{id}")]
        public async Task<IActionResult> GetAllByBranchId(int id)
        {
            if (id <= 0) return BadRequest("Bad request made");
            return Ok(await _employeeTransferInterface.GetAllByBranchId(id));
        }

        [HttpGet("all/branches/shortage")]
        public async Task<IActionResult> GetBranchesWithStaffShortage()
        {
            return Ok(await _employeeTransferInterface.GetBranchesWithStaffShortage());
        }

        [HttpGet("all/branches/excess")]
        public async Task<IActionResult> GetBranchesWithExcssStaff()
        {
            return Ok(await _employeeTransferInterface.GetBranchesWithExcssStaff());
        }

        [HttpGet("all/branches/fully")]
        public async Task<IActionResult> GetBranchesWithStaffFully()
        {
            return Ok(await _employeeTransferInterface.GetBranchesWithStaffFully());
        }

        [HttpGet("all/employees/excess")]
        public async Task<IActionResult> GetAllExcssEmployee()
        {
            return Ok(await _employeeTransferInterface.GetAllExcssEmployee());
        }
    }
}
