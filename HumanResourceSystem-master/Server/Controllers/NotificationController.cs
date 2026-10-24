using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(INotificationInterface notificationInterface) : ControllerBase
    {
        [HttpGet("all/employee/{id}")]
        public async Task<IActionResult> GetByEmployeeId(int id)
        {
            if (id <= 0) return BadRequest("Bad request made");
            return Ok(await notificationInterface.GetByEmployeeId(id));
        }

        [HttpPut("markasread/employee/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await notificationInterface.MarkAsRead(id);
            return NoContent();
        }
    }
}
