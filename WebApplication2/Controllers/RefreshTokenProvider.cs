using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2.Controllers
{
	public class RefreshTokenProvider:IAuthenticationTokenProvider
	{
		private readonly ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

		public void Create(AuthenticationTokenCreateContext context)
		{
			context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
			_refreshTokens[context.Token] = context.SerializeTicket();
		}
		public Task CreateAsync(AuthenticationTokenCreateContext context)
		{
			context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
			_refreshTokens[context.Token] = context.SerializeTicket();
			return Task.FromResult<object>(null);
		}
		public void Receive(AuthenticationTokenReceiveContext context)
		{
			string value;
			if (_refreshTokens.TryRemove(context.Token, out value))
			{
				context.DeserializeTicket(value);
			}
		}
		public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
		{
			string value;
			if (_refreshTokens.TryRemove(context.Token, out value))
			{
				context.DeserializeTicket(value);
			}
			return Task.FromResult<object>(null);
		}
	}
}