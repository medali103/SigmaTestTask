using Microsoft.Extensions.Caching.Memory;
using SigmaTestTask.Repositories;
using System;
using System.Threading.Tasks;

namespace SigmaTestTask.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public CandidateService(ICandidateRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30), // Cache duration
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
        }

        public async Task AddOrUpdateCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await _repository.GetCandidateByEmailAsync(candidate.Email);

            if (existingCandidate == null)
            {
                await _repository.AddCandidateAsync(candidate);
            }
            else
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTime = candidate.CallTime;
                existingCandidate.LinkedInProfile = candidate.LinkedInProfile;
                existingCandidate.GitHubProfile = candidate.GitHubProfile;
                existingCandidate.Comment = candidate.Comment;
                await _repository.UpdateCandidateAsync(existingCandidate);
            }

            // Clear cache after updating or adding a candidate
            ClearCache(candidate.Email);
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            if (!_cache.TryGetValue(email, out Candidate candidate))
            {
                candidate = await _repository.GetCandidateByEmailAsync(email);

                if (candidate != null)
                {
                    _cache.Set(email, candidate, _cacheOptions);
                }
            }

            return candidate;
        }

        public void ClearCache(string email)
        {
            _cache.Remove(email);
        }
    }
}
