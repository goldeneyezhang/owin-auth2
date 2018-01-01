using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
	public class AuthorizationCodeProvider:IAuthenticationTokenProvider
	{
		private readonly ConcurrentDictionary<string, string> _authorizationCode =
			new ConcurrentDictionary<string, string>(StringComparer.Ordinal);
		

		public void Create(AuthenticationTokenCreateContext context)
		{
			context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
			_authorizationCode[context.Token] = context.SerializeTicket();
		}

		public Task CreateAsync(AuthenticationTokenCreateContext context)
		{
			context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
			_authorizationCode[context.Token] = context.SerializeTicket();
			return Task.FromResult<object>(null);
		}

		public void Receive(AuthenticationTokenReceiveContext context)
		{
			string value;
			if(_authorizationCode.TryRemove(context.Token,out value))
			{
				context.DeserializeTicket(value);
			}
		}

		public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
		{
			string value;
			if (_authorizationCode.TryRemove(context.Token, out value))
			{
				context.DeserializeTicket(value);
			}
			return Task.FromResult<object>(null);
		}
	}
}