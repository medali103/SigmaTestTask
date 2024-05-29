using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using SigmaTestTask.Controllers;
using SigmaTestTask.Services;

namespace SigmaTestTask.Tests.Controllers
{
    public class CandidateControllerTests
    {
        private readonly Mock<ICandidateService> _mockCandidateService;
        private readonly CandidatesController _controller;

        public CandidateControllerTests()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _controller = new CandidatesController(_mockCandidateService.Object);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ReturnsBadRequest_WhenCandidateIsNull()
        {
            // Act
            var result = await _controller.AddOrUpdateCandidate(null);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ReturnsOk_WhenCandidateIsValid()
        {
            // Arrange
            var candidate = new Candidate { Email = "test@example.com", FirstName = "John", LastName = "Doe" };

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidate);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCandidateService.Verify(service => service.AddOrUpdateCandidateAsync(candidate), Times.Once);
        }

        [Fact]
        public async Task GetCandidate_ReturnsNotFound_WhenCandidateDoesNotExist()
        {
            // Arrange
            var email = "test@example.com";
            _mockCandidateService.Setup(service => service.GetCandidateByEmailAsync(email)).ReturnsAsync((Candidate)null);

            // Act
            var result = await _controller.GetCandidate(email);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCandidate_ReturnsOk_WhenCandidateExists()
        {
            // Arrange
            var candidate = new Candidate { Email = "test@example.com", FirstName = "John", LastName = "Doe" };
            _mockCandidateService.Setup(service => service.GetCandidateByEmailAsync(candidate.Email)).ReturnsAsync(candidate);

            // Act
            var result = await _controller.GetCandidate(candidate.Email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal(candidate.Email, returnValue.Email);
        }
    }
}
