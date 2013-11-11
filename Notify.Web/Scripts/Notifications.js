// KnockoutJS ViewModel
var NotificationItem = function (notification) {
	this.Title = notification.Title;
	this.Message = notification.Message;
	this.Type = notification.Type;
	this.UtcTimestamp = notification.UtcTimestamp;

	this.LabelClass = 'label-' + notification.Type.toLowerCase();
};

var NotificationViewModel = function (maxLength) {
	var self = this;
	maxLength = typeof maxLength !== 'undefined' ? maxLength : 5;	// default value

	self.Notifications = ko.observableArray([]);
	self.Types = ko.observableArray([]);
	self.LabelClass = ko.observable('label-default');
	self.Listening = ko.observable(true);
	this.ToggleText = ko.computed(function () {
		if (self.Listening())
			return "On";
		return "Off";
	}, this);

	self.Add = function (notification) {
		this.Notifications.push(new NotificationItem(notification));

		if (this.Notifications().length > maxLength) {
			this.Notifications.splice(0, 1); // remove the oldest event
		}

		var max = this.maxTypeLevel();
		if (self.LabelClass.peek() !== 'label-' + max)
			self.LabelClass = 'label-' + max;
	};

	self.AddAllTypes = function (types) {
		types.forEach(function (type) {
			self.Types.push(type);
		});
	};

	// get a list of used categories
	self.existingTypes = function () {
		var types = ko.utils.arrayMap(self.Notifications, function (item) {
			return item.Type;
		});
		return types.sort();
	};
	
	self.uniqueTypes = function () {
		return ko.utils.arrayGetDistinctValues(self.existingTypes()).sort();
	};

	self.maxTypeLevel = function () {
		var types = self.uniqueTypes();
		var registredTypes = self.Types.reverse();
		for (var i = 0; i < registredTypes.length; i++) {
			if (types.indexOf(registredTypes[i]) !== -1)
				return registredTypes[i];
		}

		return 'default';
	};
};
