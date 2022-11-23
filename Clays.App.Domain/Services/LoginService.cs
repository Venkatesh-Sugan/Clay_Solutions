using System;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Clays.app.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Clays.App.Domain.Interfaces;
using System.Linq;
using Clays.app.DataAccess.DataContext;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Principal;

namespace Clays.App.Domain.Services
{
	public class LoginService : ILoginService
	{
		private IConfiguration configuration;
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext _context;
        private IHttpContextAccessor httpAccessor;

        public LoginService(IConfiguration config,UserManager<ApplicationUser> manager,ApplicationDbContext context,
            IHttpContextAccessor http)
		{
			configuration = config;
            userManager = manager;
            _context = context;
            httpAccessor = http;
        }

        public async Task<ApiResponse> UserLogin(LoginModel model)
        {

            var user = await userManager.FindByEmailAsync(model.UserName);
            if (user != null)
            {
                bool authenticated = await userManager.CheckPasswordAsync(user, model.Password);
                if (authenticated)
                {
                    string token = await GenerateSecurityToken(user);
                    return new ApiResponse(System.Net.HttpStatusCode.OK, result: new { token = token });
                }
            }
            return new ApiResponse(System.Net.HttpStatusCode.Unauthorized, result: null, errorMessage: "Invalid User");
        }


        private async Task<string> GenerateSecurityToken(ApplicationUser user)
        {
            string _secret = configuration.GetSection("AppSettings").GetValue(typeof(string), "secret").ToString();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var userRole = await userManager.GetRolesAsync(user);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(ClaimTypes.Role, userRole[0]),
                    new Claim(ClaimTypes.Name,string.Concat(user.FirstName,user.LastName))
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));

        }

    }
}

