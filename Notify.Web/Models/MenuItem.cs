using System;

namespace Notify.Web.Models
{
	public class MenuItem
	{
		public string Name { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public bool Active { get; set; }

		public bool Match(string controller, string action)
		{
			if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
				return false;

			if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(Action))
				return false;

			if (!string.Equals(Controller, controller, StringComparison.InvariantCultureIgnoreCase))
				return false;

			if (!string.Equals(Action, action, StringComparison.InvariantCultureIgnoreCase))
				return false;

			return true;
		}
	}
}
