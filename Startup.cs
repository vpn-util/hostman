using System;
using System.Linq;
using Hostman.Configuration;
using Hostman.Database;
using Hostman.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Hostman
{
    public class Startup
    {
        private readonly IConfiguration _Config;

        public Startup(IConfiguration config)
        {
            this._Config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuring the database connection

            services.AddDbContext<Context>(options =>
            {
                options.UseMySql(this._Config.GetConnectionString("Database"));
            });

            // Configuring the authentication

            var oidcOpt = new OpenIDConnectOptions();
            
            this._Config.GetSection("OIDC")
                .Bind(oidcOpt);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(5),
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuer = true
                    };

                    if (oidcOpt.ValidAudiences != null) {
                        opt.TokenValidationParameters.ValidAudiences =
                            oidcOpt.ValidAudiences;
                    }

                    if (oidcOpt.ValidIssuers != null) {
                        opt.TokenValidationParameters.ValidIssuers =
                            oidcOpt.ValidIssuers;
                    }

                    if (oidcOpt.IssuerSigningKeys != null) {
                        opt.TokenValidationParameters.IssuerSigningKeys =
                            oidcOpt.IssuerSigningKeys.Select(
                                k => new JsonWebKey(k));
                    }

                    if (oidcOpt.Configuration != null) {
                        opt.ConfigurationManager =
                            new ConfigurationManager<OpenIdConnectConfiguration>(
                                oidcOpt.Configuration,
                                new OpenIdConnectConfigurationRetriever());
                    }
            });

            // Configuring the endpoint controllers

            services.AddControllers(opt =>
            {
                // Any request to any controller shall require an authenticated
                // user. (The AllowAnonymousAttribute may override this.)

                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                opt.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            Context dbContext)
        {
            // TODO: Migration strategy

            dbContext.Database.EnsureCreated();

            // Setting up the pipeline

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<UserMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
