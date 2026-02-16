using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityService.Service
{
    public class CustomProfileService(UserManager<ApplicationUser> userManager) : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);
            if (user is null)
                throw new InvalidOperationException("User not found");
            var existingClaims = await userManager.GetClaimsAsync(user);
            var newClaims = new Claim[]
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim("Name",user.UserName!),
            };
            context.IssuedClaims.AddRange(newClaims);
            context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
