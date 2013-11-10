// KnockoutJS ViewModel
var NotificationItem = function (notification) {
	this.Title = notification.Title;
	this.Message = notification.Message;
	this.Type = notification.Type;
	this.UtcTimestamp = notification.UtcTimestamp;
};

var NotificationViewModel = function (maxLength) {
	maxLength = typeof maxLength !== 'undefined' ? maxLength : 5;	// default value

	this.Notifications = ko.observableArray([]);

	this.Add = function (notification) {
		this.Notifications.push(new NotificationItem(notification));

		if (this.Notifications.visibleCount() > maxLength) {
			this.Notifications.splice(0, 1); // remove the oldest event
		}
	};
};
