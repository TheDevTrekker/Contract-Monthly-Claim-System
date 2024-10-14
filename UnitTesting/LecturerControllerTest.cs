using ContractMonthlyClaimSystem.Controllers;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTesting
{
    public class LecturerControllerTest
    {

        [Fact]
        public void SubmitClaims_ReturnsViewWithSubmittedClaims()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            var mockClaimService = new Mock<ClaimService>();
            var controller = new LecturerController(mockEnv.Object, mockClaimService.Object);

            // Use MockSession instead of mocking ISession
            var mockSession = new MockSession();
            mockSession.SetString("Name", "Lecturer1");

            var httpContext = new DefaultHttpContext
            {
                Session = mockSession
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var claims = new List<Claim>
            {
                new Claim { LecturerName = "Lecturer1", Status = "Pending" },
                new Claim { LecturerName = "Lecturer1", Status = "Approved" }
            };

            mockClaimService.Setup(s => s.GetAllClaims()).Returns(claims);

            // Act
            var result = controller.SubmitClaims() as ViewResult;
            var model = result?.Model as LecturerViewModel;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.SubmittedClaims.Count);

            // Compare the properties of each claim manually
            Assert.Equal(claims[0].LecturerName, model.SubmittedClaims[0].LecturerName);
            Assert.Equal(claims[0].Status, model.SubmittedClaims[0].Status);
            Assert.Equal(claims[1].LecturerName, model.SubmittedClaims[1].LecturerName);
            Assert.Equal(claims[1].Status, model.SubmittedClaims[1].Status);
        }

        [Fact]
        public async Task SubmitClaimPost_ValidClaimWithFile_RedirectsToSubmitClaims()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            var mockClaimService = new Mock<ClaimService>();
            var controller = new LecturerController(mockEnv.Object, mockClaimService.Object);

            // Mock the environment
            mockEnv.Setup(e => e.WebRootPath).Returns("wwwroot");

            // Use MockSession
            var mockSession = new MockSession();
            mockSession.SetString("Name", "Lecturer1");

            var httpContext = new DefaultHttpContext
            {
                Session = mockSession
            };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Create a valid claim with a file
            var mockFile = new Mock<IFormFile>();
            var content = "Fake File Content";
            var fileName = "testfile.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.Length).Returns(ms.Length);
            mockFile.Setup(f => f.OpenReadStream()).Returns(ms);
            mockFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

            var model = new LecturerViewModel
            {
                NewClaim = new Claim
                {
                    LecturerName = "Lecturer1",
                    HoursWorked = 10,
                    HourlyRate = 100,
                    SupportingDocument = mockFile.Object
                }
            };

            // Act
            var result = await controller.SubmitClaimPost(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SubmitClaims", result.ActionName);
            mockClaimService.Verify(s => s.AddClaim(It.IsAny<Claim>()), Times.Once);
        }



        [Fact]
        public async Task SubmitClaimPost_NoFileUploaded_RedirectsToSubmitClaims()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            var mockClaimService = new Mock<ClaimService>();
            var controller = new LecturerController(mockEnv.Object, mockClaimService.Object);

            var mockSession = new MockSession();
            mockSession.SetString("Name", "Lecturer1");

            var httpContext = new DefaultHttpContext
            {
                Session = mockSession
            };
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var model = new LecturerViewModel
            {
                NewClaim = new Claim
                {
                    LecturerName = "Lecturer1",
                    HoursWorked = 5,
                    HourlyRate = 50
                }
            };

            // Act
            var result = await controller.SubmitClaimPost(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SubmitClaims", result.ActionName);
            mockClaimService.Verify(s => s.AddClaim(It.IsAny<Claim>()), Times.Once);
        }


    }

    public class MockSession : ISession
    {
        private Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();

        public bool IsAvailable => true;

        public string Id => Guid.NewGuid().ToString();

        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public void Clear() => _sessionStorage.Clear();

        public void Remove(string key) => _sessionStorage.Remove(key);

        public void Set(string key, byte[] value) => _sessionStorage[key] = value;

        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);

        // Helper methods to work with string values
        public void SetString(string key, string value)
        {
            _sessionStorage[key] = System.Text.Encoding.UTF8.GetBytes(value);
        }

        public string GetString(string key)
        {
            if (_sessionStorage.TryGetValue(key, out byte[] value))
            {
                return System.Text.Encoding.UTF8.GetString(value);
            }

            return null;
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}