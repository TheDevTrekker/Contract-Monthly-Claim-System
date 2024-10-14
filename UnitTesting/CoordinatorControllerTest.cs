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
    public class CoordinatorControllerTest
    {
        [Fact]
        public void ApproveClaim_ChangesClaimStatusToApproved()
        {
            // Arrange
            var mockClaimService = new Mock<ClaimService>();
            var claim = new Claim { Id = 1, Status = "Pending" };

            mockClaimService.Setup(s => s.GetAllClaims()).Returns(new List<Claim> { claim });
            var controller = new CoordinatorController(mockClaimService.Object);

            // Act
            var result = controller.ApproveClaim(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("CoReview", result.ActionName);
            Assert.Equal("Approved by Coordinator", claim.Status);
        }

        [Fact]
        public void RejectClaim_ChangesClaimStatusToRejected()
        {
            // Arrange
            var mockClaimService = new Mock<ClaimService>();
            var claim = new Claim { Id = 1, Status = "Pending" };

            mockClaimService.Setup(s => s.GetAllClaims()).Returns(new List<Claim> { claim });
            var controller = new CoordinatorController(mockClaimService.Object);

            // Act
            var result = controller.RejectClaim(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("CoReview", result.ActionName);
            Assert.Equal("Rejected by Coordinator", claim.Status);
        }

    }
}
