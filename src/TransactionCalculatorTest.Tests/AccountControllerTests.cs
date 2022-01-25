using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using TransactionCalculatorTest.Controllers;
using TransactionCalculatorTest.BusinessLogic.Interfaces;
using TransactionCalculatorTest.Model.Models;
using Xunit;

namespace TransactionCalculatorTest.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void Calculate_ShouldReturnOkResult_WhenRecieveValidRequest()
        {
            var serviceMock = new Mock<IAccountService>();
            serviceMock.Setup(s => s.GetAccountsInfo(It.IsAny<CalculationRequest>())).Returns(new List<AccountInfo>());
            var loggerMock = new Mock<ILogger<AccountController>>();
            var controller = new AccountController(serviceMock.Object, loggerMock.Object);

            var result = controller.Calculate(new CalculationRequest());

            var okResult = result as OkObjectResult;
            var okResultValue = okResult?.Value as List<AccountInfo>;
         
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResultValue);
        }
    }
}
