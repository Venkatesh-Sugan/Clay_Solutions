using System;
using System.Threading.Tasks;

namespace Clays.App.Domain.Interfaces
{
	public interface IUserService
	{
        Task<ApiResponse> AddUser(RegisterUserModel userModel);
    }
}

