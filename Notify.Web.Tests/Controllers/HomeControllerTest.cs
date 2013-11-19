using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notify.Web.Controllers;

namespace Notify.Web.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			var controller = new HomeController();

			// Act
			var result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void About()
		{
			// Arrange
			var controller = new HomeController();

			// Act
			var result = controller.About() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Contact()
		{
			// Arrange
			var controller = new HomeController();

			// Act
			var result = controller.KnockoutNotifications() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
