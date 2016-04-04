﻿using IdentityModel.AspNetCore.ScopeValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tests
{
    public class MultipleAuthenticationStartup
    {
        private readonly bool _automaticAuthenticate;
        private readonly ClaimsPrincipal _principal1;
        private readonly ClaimsPrincipal _principal2;
        private readonly ScopeValidationOptions _scopeOptions;

        public MultipleAuthenticationStartup(ClaimsPrincipal principal1, ClaimsPrincipal principal2, bool automaticAuthenticate, ScopeValidationOptions options)
        {
            _principal1 = principal1;
            _principal2 = principal2;
            _scopeOptions = options;
            _automaticAuthenticate = automaticAuthenticate;
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_principal1 != null)
            {
                app.UseMiddleware<TestAuthenticationMiddleware>(new TestAuthenticationOptions
                {
                    AuthenticationScheme = "scheme1",
                    User = _principal1,

                    AutomaticAuthenticate = _automaticAuthenticate
                });
            }

            if (_principal2 != null)
            {
                app.UseMiddleware<TestAuthenticationMiddleware>(new TestAuthenticationOptions
                {
                    AuthenticationScheme = "scheme2",
                    User = _principal2,

                    AutomaticAuthenticate = _automaticAuthenticate
                });
            }

            app.AllowScopes(_scopeOptions);

            app.Run(ctx =>
            {
                ctx.Response.StatusCode = 200;
                return Task.FromResult(0);
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();
        }
    }
}