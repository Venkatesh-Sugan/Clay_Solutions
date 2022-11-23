using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Clays.app.DataAccess.Entities;

namespace Clays.app.DataAccess.DataContext
{
	public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Door> Door { get; set; }

        public DbSet<DoorType> DoorType { get; set; }

        public DbSet<DoorHistory> DoorHistory { get; set; }
    }
}

