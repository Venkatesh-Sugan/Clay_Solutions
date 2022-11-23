using System;
using Clay.App.Api.Controllers;
using Clays.app.DataAccess.Entities;
using Clays.App.Domain;
using Clays.App.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Clay.App.Test
{
	public class UserControllerTest
	{
		private readonly UserController userController;
		private readonly Mock<IUserService> mockUserService;
		public UserControllerTest()
		{
			mockUserService = new Mock<IUserService>();
			userController = new UserController(mockUserService.Object);
        }

		[Fact]
		public void CreateUserOkResult()
		{
            var defaultUser = new RegisterUserModel { FirstName = "userManager",LastName="Test",Password="Password1,",Role="Admin", Email = "userManager@abc.com" };
			var apiResponse = new ApiResponse(System.Net.HttpStatusCode.OK);
			mockUserService.Setup(x => x.AddUser(defaultUser)).ReturnsAsync(apiResponse);
			var result = userController.AddUser(defaultUser);
			var data = result.Result;
			Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void CreateUserBadRequestResult()
        {
            var defaultUser = new RegisterUserModel { FirstName = "userManager", LastName = "Test", Password = "Password1,", Role = "Admin", Email = "userManager@abc.com" };
            var apiResponse = new ApiResponse(System.Net.HttpStatusCode.BadRequest);
            mockUserService.Setup(x => x.AddUser(defaultUser)).ReturnsAsync(apiResponse);
            var result = userController.AddUser(defaultUser);
            var data = result.Result;
            Assert.IsType<BadRequestObjectResult>(data);
        }
    }
}

