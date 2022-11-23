using System;
using System.Threading.Tasks;

namespace Clays.App.Domain.Interfaces
{
	public interface ILoginService
	{
		Task<ApiResponse> UserLogin(LoginModel model);
    }
}

