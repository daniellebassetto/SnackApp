using Microsoft.AspNetCore.Identity;

namespace SnackApp.Services;

public class SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) : ISeedUserRoleInitial
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public void SeedRoles()
    {
        if(!_roleManager.RoleExistsAsync("Member").Result)
        {
            IdentityRole role = new()
            {
                Name = "Member",
                NormalizedName = "MEMBER"
            };
            _ = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            _ = _roleManager.CreateAsync(role).Result;
        }
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            IdentityUser user = new()
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Numsey#2024").Result;

            if(result.Succeeded)
                _userManager.AddToRoleAsync(user, "Member").Wait();
            
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            IdentityUser user = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "NumseyAdmin#2024").Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }
}