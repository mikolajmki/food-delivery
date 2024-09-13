﻿using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DbInitializer: IDbInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(
            RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager, 
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    //_context.Database.Migrate();
                }
            } 
            catch (Exception ex)
            {
                throw;
            }
            if (_context.Roles.Any(x => x.Name == "Manager")) return;
            _roleManager.CreateAsync(new IdentityRole("Manager"))
                .GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Admin"))
                .GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Customer"))
                .GetAwaiter().GetResult();

            var user= new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Name = "Admin",
                City = "XYZ",
                Address = "XYZ",
                PostalCode = "12345"
            };

            _userManager.CreateAsync(user, "Admin@123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
