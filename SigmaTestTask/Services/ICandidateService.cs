using System.Threading.Tasks;

namespace SigmaTestTask.Services
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidateAsync(Candidate candidate);
        Task<Candidate> GetCandidateByEmailAsync(string email);
        void ClearCache(string email);
    }
}

