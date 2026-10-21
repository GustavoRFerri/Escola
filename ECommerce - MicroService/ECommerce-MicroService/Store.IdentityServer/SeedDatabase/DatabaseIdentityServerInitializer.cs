using Microsoft.AspNetCore.Identity;
using Store.IdentityServer.Configuration;
using Store.IdentityServer.Data;

namespace Store.IdentityServer.SeedDatabase
{
    public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseIdentityServerInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeSeedRoles()
        {
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
            {
                IdentityRole roleAdmin = new IdentityRole
                {
                    Name = IdentityConfiguration.Admin,
                    NormalizedName = IdentityConfiguration.Admin.ToUpper(),                    
                };
                _roleManager.CreateAsync(roleAdmin).Wait();
            }


            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = IdentityConfiguration.Client,
                    NormalizedName = IdentityConfiguration.Client.ToUpper(),
                };
                _roleManager.CreateAsync(role).Wait();
            }
        }

        public void InitializeSeedUsers()
        {
            if (_userManager.FindByEmailAsync("").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "",
                    Email = "",
                    FirstName = "System",
                    LastName = "Admin",
                    EmailConfirmed = true,
                };
                IdentityResult result = _userManager.CreateAsync(user, "Admin123*").Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, IdentityConfiguration.Admin).Wait();
                }
            }
        }
    }
}
