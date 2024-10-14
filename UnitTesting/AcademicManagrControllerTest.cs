using ContractMonthlyClaimSystem.Controllers;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    public class AcademicManagrControllerTest
    {
        [Fact]
        public void ManagerReview_ReturnsViewWithPendingClaims()
        {
            // Arrange
            var mockClaimService = new Mock<ClaimService>();
            var pendingClaims = new List<Claim>
            {
                new Claim { LecturerName = "Lecturer1", Status = "Approved by Coordinator" },
                new Claim { LecturerName = "Lecturer2", Status = "Approved by Coordinator" }
            };

            mockClaimService.Setup(s => s.GetClaimsByStatus("Approved by Coordinator")).Returns(pendingClaims);
            var controller = new AcademicManagerController(mockClaimService.Object);

            // Act
            var result = controller.ManagerReview() as ViewResult;
            var model = result?.Model as List<Claim>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
            Assert.All(model, claim => Assert.Equal("Approved by Coordinator", claim.Status));
        }

        [Fact]
        public void ApproveClaim_ChangesClaimStatusToApprovedByManager()
        {
            // Arrange
            var mockClaimService = new Mock<ClaimService>();
            var claim = new Claim { Id = 1, Status = "Approved by Coordinator" };

            mockClaimService.Setup(s => s.GetAllClaims()).Returns(new List<Claim> { claim });
            var controller = new AcademicManagerController(mockClaimService.Object);

            // Act
            var result = controller.ApproveClaim(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ManagerReview", result.ActionName);
            Assert.Equal("Approved by Manager", claim.Status);
        }

        [Fact]
        public void RejectClaim_ChangesClaimStatusToRejectedByManager()
        {
            // Arrange
            var mockClaimService = new Mock<ClaimService>();
            var claim = new Claim { Id = 1, Status = "Approved by Coordinator" };

            mockClaimService.Setup(s => s.GetAllClaims()).Returns(new List<Claim> { claim });
            var controller = new AcademicManagerController(mockClaimService.Object);

            // Act
            var result = controller.RejectClaim(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ManagerReview", result.ActionName);
            Assert.Equal("Rejected by Manager", claim.Status);
        }
    }
}