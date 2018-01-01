using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Controllers
{
	public class Client
	{
		public string Id { get; set; }
		public string Secret { get; set; }
		public string RedirectUrl { get; set; }
	}
	public  class ClientRepository
	{
		public static List<Client> Clients = new List<Client>()
		{
			new Client
			{
				Id="test1",
				RedirectUrl="http://localhost:52664",
				Secret="123456789"
			}
		};
	}
}