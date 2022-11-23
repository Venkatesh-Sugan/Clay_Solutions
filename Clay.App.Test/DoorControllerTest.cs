using System;
using Moq;
using Xunit;
using Clays.App.Domain;
using Clays.App.Domain.Interfaces;
using Clay.App.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Clays.app.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Clay.App.Test
{
	public class DoorControllerTest
	{

		private Mock<IDoorService> mockDoorService;
		private Mock<IHttpContextAccessor> _httpContextAccessor;
		private DoorController doorController;

		public DoorControllerTest()
		{
            mockDoorService = new Mock<IDoorService>();
			_httpContextAccessor = new Mock<IHttpContextAccessor>();
			doorController = new DoorController(_httpContextAccessor.Object, mockDoorService.Object);
		}

		[Fact]
		public void AddDoor()
		{
			Door door = new Door { DoorId = 1, DoorName = "101", DoorNumber = "101", DoorType = 1 };
            var apiResponse = new ApiResponse(System.Net.HttpStatusCode.OK);
            mockDoorService.Setup(x => x.AddDoor(door)).ReturnsAsync(apiResponse);
			var result = doorController.AddDoor(door);
            var data = result.Result;
            Assert.IsType<OkObjectResult>(data);
        }
	}
}

