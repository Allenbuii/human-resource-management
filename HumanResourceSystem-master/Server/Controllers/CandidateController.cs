using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : GenericController<Candidate>
    {
        private readonly ICandidateInterface _candidateRepository;

        public CandidateController(ICandidateInterface candidateRepository)
            : base(candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        [HttpPut("send-email")]
        public async Task<IActionResult> SendEmail(Candidate model)
        {
            if (model is null) return BadRequest("Bad request made");
            return Ok(await _candidateRepository.SendEmail(model));
        }
    }
}
