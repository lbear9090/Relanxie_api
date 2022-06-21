using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

using Avigma.Repository.Lib;

[assembly: OwinStartup(typeof(API.Startup))]

namespace API
{
    public class Startup
    {
        //public void Configuration(IAppBuilder app)
        //{
        //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

        //}
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {   // Token Generation
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(Convert.ToDouble(ConfigurationManager.AppSettings["TokenValidityDays"])),
                Provider = new CustomAuthorizationServerProvider()
            };


            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}