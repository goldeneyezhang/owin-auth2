using Microsoft.Owin;
using Owin;
using WebApplication2.Controllers;

[assembly: OwinStartupAttribute(typeof(WebApplication2.Startup))]
namespace WebApplication2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			app.UseOAuthAuthorizationServer(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				AuthorizeEndpointPath = new PathString("/oauth2/authorize"),
				TokenEndpointPath=new PathString("/oauth2/token"),
				Provider=new DemoOAuthAuthorizationServerProvider(),
				AuthorizationCodeProvider=new AuthorizationCodeProvider(),
				RefreshTokenProvider=new RefreshTokenProvider()
			});
			app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions()
			{
			});
        }
    }
}
