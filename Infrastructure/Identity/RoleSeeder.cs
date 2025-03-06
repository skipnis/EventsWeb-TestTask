using Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class RoleSeeder
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleSeeder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SeedRolesAsync()
    {
        var roles = new[] { "Admin", "User", "EventOwner" };

        foreach (var role in roles)
        {
            var roleExists = await _unitOfWork.RoleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                var result = await _unitOfWork.RoleManager.CreateAsync(new IdentityRole<Guid>(role));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role {role}");
                }
            }
        }
    }
}