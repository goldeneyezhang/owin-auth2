using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
	public class DemoOAuthAuthorizationServerProvider:OAuthAuthorizationServerProvider
	{
		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			string clientId;
			string clientSecret;
			if(!context.TryGetBasicCredentials(out clientId,out clientSecret))
			{
				context.TryGetFormCredentials(out clientId, out clientSecret);
			}
			if(context.ClientId==null)
			{
				context.SetError("invalid_clientId","client Id is not set");
				return Task.FromResult<object>(null);
			}
			if(!string.IsNullOrEmpty(clientSecret))
			{
				context.OwinContext.Set("clientSecret", clientSecret);
			}
			var client = ClientRepository.Clients.Where(c => c.Id == clientId).FirstOrDefault();
			if(client!=null)
			{
				context.Validated();
			}
			else
			{
				context.SetError("invalid_clientid",$"Invalid client_id {context.ClientId}");
				return Task.FromResult<object>(null);
			}
			return Task.FromResult<object>(null);
		}
		public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
		{
			var client = ClientRepository.Clients.Where(c => c.Id == context.ClientId).FirstOrDefault();
			if (client != null)
			{
				context.Validated(client.RedirectUrl);
			}
			return base.ValidateClientRedirectUri(context);
		}
	}
}