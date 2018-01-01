using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
	[Authorize]
	public class ProtectedResourceController : Controller
    {
        // GET: ProtectedResource
        public ActionResult Index()
        {
			return Json(new { Context = "Hello World" });
        }
    }
}