using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Models.Auth
{
    public static class AuthPolicy
    {
        public static void AddPolicies(IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("MustBeVia", a =>
                    a.RequireAuthenticatedUser().RequireClaim("Domain", "via"));

            });
        }
    }
}
