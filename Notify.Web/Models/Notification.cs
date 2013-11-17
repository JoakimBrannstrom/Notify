using System;

namespace Notify.Web.Models
{
	public class Notification
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public string Type { get; set; }
		public DateTime UtcTimestamp { get; set; }
		public bool Read { get; set; }

		public static readonly string[] AllTypes =	{
														"Default",
														"Primary",
														"Success",
														"Info",
														"Warning",
														"Danger"
													};

		public Notification(Guid id)
		{
			Id = id;
		}
	}
}
