using ContractMonthlyClaimSystem.Controllers;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class LecturerControllerTests
    {
        private LecturerController _controller;
        private Mock<IWebHostEnvironment> _mockEnvironment;
        private Mock<ClaimService> _mockClaimService;
        private Mock<IFormFile> _mockFile;
        private Mock<ISession> _mockSession;

        [SetUp]
        public void Setup()
        {
            // Mock Environment and ClaimService
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockEnvironment.Setup(env => env.WebRootPath).Returns("wwwroot");

            _mockClaimService = new Mock<ClaimService>();

            // Mock IFormFile
            _mockFile = new Mock<IFormFile>();

            // Mock session and set up a default value for "Name"
            _mockSession = new Mock<ISession>();
            byte[] value = Encoding.UTF8.GetBytes("TestLecturer");
            _mockSession.Setup(s => s.TryGetValue("Name", out value)).Returns(true);

            // Assign HttpContext to Controller
            var httpContext = new DefaultHttpContext { Session = _mockSession.Object };
            _controller = new LecturerController(_mockEnvironment.Object, _mockClaimService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };
        }

        [Test]
        public async Task SubmitClaimPost_ValidClaimWithFile_ShouldRedirectToSubmitClaims()
        {
            // Arrange
            var model = new LecturerViewModel
            {
                NewClaim = new Claim
                {
                    SupportingDocument = _mockFile.Object
                }
            };

            // Set up the mock file for testing
            _mockFile.Setup(f => f.FileName).Returns("test.docx");
            _mockFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.SubmitClaimPost(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual("SubmitClaims", result.ActionName, "Should redirect to SubmitClaims action.");
            _mockClaimService.Verify(service => service.AddClaim(It.IsAny<Claim>()), Times.Once, "AddClaim should be called exactly once.");
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}
