using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

[assembly: OwinStartup(typeof(webApiTokenAuthentication.Startup))]

namespace webApiTokenAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //To enable cross origin request.
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //Reference to provider.
            var myProvider = new MyAuthServerProvider();
            OAuthAuthorizationServerOptions myServerOptions = new OAuthAuthorizationServerOptions {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = myProvider
            };

            app.UseOAuthAuthorizationServer(myServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
