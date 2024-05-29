using Microsoft.AspNetCore.Mvc;
using SigmaTestTask.Services;
using System.Threading.Tasks;

namespace SigmaTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        [Route("AddOrUpdateCandidate")]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] Candidate candidate)
        {
            if (candidate == null)
            {
                return BadRequest("Candidate information is required.");
            }

            await _candidateService.AddOrUpdateCandidateAsync(candidate);
            return Ok(candidate);
        }

        [HttpGet]
        [Route("GetCandidate")]
        public async Task<IActionResult> GetCandidate(string email)
        {
            var candidate = await _candidateService.GetCandidateByEmailAsync(email);
            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }
    }
}
