using System.Threading.Tasks;
using Moq;
using Xunit;
using SigmaTestTask.Repositories;
using SigmaTestTask.Services;

namespace CandidateAPI.Tests.Services
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _mockRepository;
        private readonly CandidateService _candidateService;

        public CandidateServiceTests()
        {
            _mockRepository = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_mockRepository.Object);
        }

        [Fact]
        public async Task AddOrUpdateCandidateAsync_AddsNewCandidate_WhenCandidateDoesNotExist()
        {
            // Arrange
            var candidate = new Candidate { Email = "test@example.com", FirstName = "John", LastName = "Doe" };
            _mockRepository.Setup(repo => repo.GetCandidateByEmailAsync(candidate.Email)).ReturnsAsync((Candidate)null);

            // Act
            await _candidateService.AddOrUpdateCandidateAsync(candidate);

            // Assert
            _mockRepository.Verify(repo => repo.AddCandidateAsync(candidate), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateCandidateAsync_UpdatesExistingCandidate_WhenCandidateExists()
        {
            // Arrange
            var existingCandidate = new Candidate { Email = "test@example.com", FirstName = "John", LastName = "Doe" };
            var updatedCandidate = new Candidate { Email = "test@example.com", FirstName = "Jane", LastName = "Doe" };
            _mockRepository.Setup(repo => repo.GetCandidateByEmailAsync(existingCandidate.Email)).ReturnsAsync(existingCandidate);

            // Act
            await _candidateService.AddOrUpdateCandidateAsync(updatedCandidate);

            // Assert
            Assert.Equal("Jane", existingCandidate.FirstName);
            _mockRepository.Verify(repo => repo.UpdateCandidateAsync(existingCandidate), Times.Once);
        }
    }
}
