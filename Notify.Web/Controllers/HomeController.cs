﻿using System.Web.Mvc;

namespace Notify.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Notifications()
		{
			return View();
		}
	}
}
