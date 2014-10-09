﻿using System;
using System.Web.Http;
using EmployeeDirectory.API.Contexts;
using EmployeeDirectory.API.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using EmployeeDirectory.API;
using EmployeeDirectory.API.Providers;

[assembly: OwinStartup(typeof(Startup))]
namespace EmployeeDirectory.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // set http route configuration
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            // set oauth configuration
            ConfigureOAuth(app);

            // enable CORS
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and role manager to use a single instance per request
            app.CreatePerOwinContext(AppContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // token generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}