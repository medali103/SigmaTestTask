﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigmaTestTask.Services;

namespace SigmaTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _service;

        public CandidatesController(ICandidateService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.AddOrUpdateCandidateAsync(candidate);
            return Ok();
        }
    }
}
