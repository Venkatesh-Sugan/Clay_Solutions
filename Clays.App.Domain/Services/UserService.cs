using System;
using System.Threading.Tasks;
using Clays.app.DataAccess.Entities;
using Clays.App.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Clays.App.Domain.Services
{
	public class UserService : IUserService
	{
		private UserManager<ApplicationUser> _userManager;
		public UserService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<ApiResponse> AddUser(RegisterUserModel userModel)
		{
			try
			{
				var existingUser = _userManager.FindByEmailAsync(userModel.Email);
				if (await existingUser != null)
				{
					var defaultUser = new ApplicationUser
					{
						FirstName = userModel.FirstName,
						Email = userModel.Email,
						EmailConfirmed = true,
						PhoneNumberConfirmed = true,
						UserName = userModel.Email
					};

					var user = await _userManager.CreateAsync(defaultUser);
					await _userManager.AddPasswordAsync(defaultUser, userModel.Password);
					await _userManager.AddToRoleAsync(defaultUser, userModel.Role);
					return new ApiResponse(System.Net.HttpStatusCode.OK);
				}
				return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessage: "User already Existed");
			}
			catch(Exception ex)
			{
				return new ApiResponse(System.Net.HttpStatusCode.BadRequest,errorMessage:ex.Message);
			}
		}
	}
}

