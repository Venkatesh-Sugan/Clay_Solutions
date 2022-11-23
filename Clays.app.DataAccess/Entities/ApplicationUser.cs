using System;
using Microsoft.AspNetCore.Identity;

namespace Clays.app.DataAccess.Entities
{
	public class ApplicationUser : IdentityUser
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

