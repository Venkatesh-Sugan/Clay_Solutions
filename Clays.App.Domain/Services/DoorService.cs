using System;
using System.Linq;
using System.Threading.Tasks;
using Clays.app.DataAccess.DataContext;
using Clays.app.DataAccess.Entities;
using System.Net;
using Clays.App.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Clays.App.Domain.Services
{
	public class DoorService : IDoorService
	{
		private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoorService(ApplicationDbContext dbContext,IHttpContextAccessor httpContextAccessor)
		{
			this.dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
		}

        public async Task<ApiResponse> GetAllDoor()
        {
            try
            {
                var response = new ApiResponse();
                var door = dbContext.Door.AsQueryable();
                if (door.Any())
                {
                    response.Result = door.ToList();
                    response.StatusCode = (int)HttpStatusCode.OK;
                    return response;
                }
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ErrorMessage = "Invalid Request";
                return response;
            }
            catch(Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, result: null, errorMessage: ex.Message);
            }
        }

        public async Task<ApiResponse> GetDoor(int doorId)
		{
            try
            {
                var response = new ApiResponse();
                var door = await dbContext.Door.FindAsync(doorId);
                if (door != null)
                {
                    response.Result = door;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    return response;
                }
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ErrorMessage = "Invalid Request";
                return response;
            }
            catch(Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, result: null, errorMessage: ex.Message);
            }
        }

		public async Task<ApiResponse> AddDoor(Door door)
		{
            try
            {
                var checkDoorAlreadyAdded = dbContext.Door.Any(x => x.DoorName == door.DoorName || x.DoorNumber == door.DoorNumber);
                if (checkDoorAlreadyAdded)
                {
                    return await Task.FromResult<ApiResponse>(new ApiResponse(status: HttpStatusCode.BadRequest, errorMessage: "Door Already Added"));
                }
                await dbContext.Door.AddAsync(door);
                return await Task.FromResult<ApiResponse>(new ApiResponse(status: HttpStatusCode.OK, errorMessage: "Door Added Successfully"));
            }
            catch(Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, result: null, errorMessage: ex.Message);
            }
		}

        private async Task AddDoorHistory(DoorHistory doorHistory)
        {

            await dbContext.DoorHistory.AddAsync(doorHistory);
            await dbContext.SaveChangesAsync();

        }

        public async Task<ApiResponse> GetDoorHistory()
        {
            try
            {
                var doorHistory = dbContext.DoorHistory.ToListAsync();
                return new ApiResponse(HttpStatusCode.OK, result: await doorHistory);
            }
            catch(Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, result: null, errorMessage: ex.Message);
            }
        }

		public async Task<ApiResponse> OpenDoor(int doorId)
		{
            try
            {
                var user = _httpContextAccessor.HttpContext.User;
                bool isAdmin = user.IsInRole("Admin");
                var door = await GetDoor(doorId);
                var doorHistory = new DoorHistory();
                doorHistory.IsDoorAccessed = "Entry Restrcited";
                if (door.StatusCode == (int)HttpStatusCode.OK)
                {
                    var data = door.Result as Door;
                    doorHistory.DoorId = data.DoorId;
                    doorHistory.DoorName = data.DoorName;
                    doorHistory.UserId = user.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Sid).Value;
                    doorHistory.UserName = user.Claims.FirstOrDefault(x=>x.Type == System.Security.Claims.ClaimTypes.Name).Value;
                    doorHistory.DoorAccessedTime = DateTime.Now;
                    if (data.DoorType != 1 && isAdmin)
                    {
                        doorHistory.IsDoorAccessed = "door Opened";
                        await AddDoorHistory(doorHistory);
                        return new ApiResponse(HttpStatusCode.OK,result: "Door Opened Successfully");
                    }
                    else if (data.DoorType == 1)
                    {
                        doorHistory.IsDoorAccessed = "door Opened";
                        await AddDoorHistory(doorHistory);
                        return new ApiResponse(HttpStatusCode.OK, result: "Door Opened Successfully");
                    }
                    else
                    {
                        await AddDoorHistory(doorHistory);
                        return new ApiResponse(HttpStatusCode.Unauthorized, errorMessage: "Restricted Entry");
                    }
                }                
                return new ApiResponse(HttpStatusCode.BadRequest, errorMessage: "InvalidData");
            }
            catch(Exception ex)
            {
                return new ApiResponse(HttpStatusCode.InternalServerError, result: null, errorMessage: ex.Message);
            }
		}

		public bool UpdateDoor(Door door)
		{
			//implementation for Updating door Config
			return false;
		}

		public bool DeleteDoor(int Id)
		{
			//implementation for Delete door Config
			return false;
        }
    }
}

