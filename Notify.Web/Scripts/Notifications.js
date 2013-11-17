// KnockoutJS ViewModel
var NotificationItem = function (notification) {
	var self = this;
	self.Id = notification.Id;
	self.Title = notification.Title;
	self.Message = notification.Message;
	self.Type = notification.Type;
	self.UtcTimestamp = notification.UtcTimestamp;
	self.Read = notification.Read;

	self.LabelClass = 'label-' + notification.Type.toLowerCase();
};

var SelectionItem = function (value, selected) {
	var self = this;
	self.Value = ko.observable(value);
	self.Selected = ko.observable(selected);
};

var NotificationViewModel = function (maxLength) {
	var self = this;
	maxLength = typeof maxLength !== 'undefined' ? maxLength : 5;	// default value

	self.Notifications = ko.observableArray([]);
	self.AllTypes = ko.observableArray([]);
	self.SelectedTypes = ko.observableArray([]);
	self.LabelClass = ko.observable('label-default');
	self.Listening = ko.observable(true);
	self.ToggleText = ko.computed(function () {
		if (self.Listening())
			return "On";
		return "Off";
	}, this);

	self.AddAllTypes = function (types) {
		types.forEach(function (type) {
			self.AllTypes.push(type);
			self.SelectedTypes.push(new SelectionItem(type, true));
		});
	};

	self.Add = function (notification) {
		self.Notifications.push(new NotificationItem(notification));

		if (self.Notifications().length > maxLength)
			self.Notifications.splice(0, 1); // remove the oldest event

		self.updateMaxTypeLevel();
	};
	
	self.Remove = function (notification) {
		// self.Notifications.remove(function (item) { return item.Id === notification.Id });
		self.Notifications.remove(notification);

		self.updateMaxTypeLevel();
	};

	self.updateMaxTypeLevel = function () {
		self.LabelClass('label-' + self.maxTypeLevel().toLowerCase());
	};

	self.maxTypeLevel = function () {
		var types = self.uniqueTypes();
		var registredTypes = self.AllTypes();

		for (var i = registredTypes.length - 1; i > 0; i--) {
			if (types.indexOf(registredTypes[i]) !== -1)
				return registredTypes[i];
		}

		return 'default';
	};

	self.uniqueTypes = function () {
		return ko.utils.arrayGetDistinctValues(self.existingTypes());
	};

	self.existingTypes = function () {
		return  ko.utils.arrayMap(self.Notifications(), function (item) { return item.Type; });
	};
};
