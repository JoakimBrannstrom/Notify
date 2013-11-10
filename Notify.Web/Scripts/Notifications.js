// KnockoutJS ViewModel
var NotificationItem = function (notification) {
	var self = this;
	this.Title = notification.Title;
	this.Message = notification.Message;
	this.Type = notification.Type;
	this.UtcTimestamp = notification.UtcTimestamp;
};

var NotificationViewModel = function () {

	this.Notifications = ko.observableArray([]);

	this.NotificationSubscriptions = ko.observableArray([]);

	this.Add = function (notification) {
		this.Notifications.push(new NotificationItem(notification));
		this.NotificationSubscriptions.push(notification.Type);

		// Sort the array since it might be bad after adding
		this.Notifications.sort(function(left, right) {
			if (left.UtcTimestamp == right.UtcTimestamp)
				return 0;
			else if (left.UtcTimestamp < right.UtcTimestamp)
				return -1;
			else
				return 1;
		});
	};
};
