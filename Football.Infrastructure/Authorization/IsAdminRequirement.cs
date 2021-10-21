using Football.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Football.Infrastructure.Authorization
{

    public class IsAdminRequirement : IAuthorizationRequirement
    {
    }

    public class IsAdminRequirementHandler : AuthorizationHandler<IsAdminRequirement>
    {
        private readonly FootballDbContext _dbContext;
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var user = _dbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(p => p.Id == userId);

            if(user.Role.Name == "Admin")
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
