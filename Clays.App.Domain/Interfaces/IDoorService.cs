using System;
using System.Threading.Tasks;
using Clays.app.DataAccess.Entities;

namespace Clays.App.Domain.Interfaces
{
	public interface IDoorService
	{
        Task<ApiResponse> GetAllDoor();
        Task<ApiResponse> GetDoor(int doorId);
        Task<ApiResponse> AddDoor(Door door);
        bool UpdateDoor(Door door);
		bool DeleteDoor(int Id);
        Task<ApiResponse> OpenDoor(int doorId);
        Task<ApiResponse> GetDoorHistory();
    }
}

