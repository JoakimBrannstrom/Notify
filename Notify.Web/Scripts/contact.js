var viewModel = null;
var webSocket = null;

$(function () {
	viewModel = new NotificationViewModel(10);

	ko.applyBindings(viewModel);

	webSocket = new XSockets.WebSocket('ws://127.0.0.1:4502/Notification');

	webSocket.on(XSockets.Events.open, function () {
		// When a notification arrives, add it to the viewModel
		webSocket.on('notify', function (notification) {
			viewModel.Add(notification);
		});

		// Listen for all types (published at bottom of open event)
		webSocket.on('allTypes', function (types) {
			viewModel.AddAllTypes(types);
		});
	});

	webSocket.bind(XSockets.Events.onError, function (error) {
		console.log("XSockets error: ", error);
	});

	webSocket.bind(XSockets.Events.close, function () {
		// This event will be fired when server disconnects / closes the current connection
		console.log("Connection closed.");
	});
});

var toggleListening = function () {
	viewModel.Listening(!viewModel.Listening());

	// Tell XSockets about the change
	// Note that this will update the actual property on the controller without any method decalred being called
	webSocket.publish('set_Listening', { value: viewModel.Listening() });
};

var markAsRead = function (notification) {
	notification.Read = !notification.Read;

	// Tell XSockets about the change
	var json = { id: notification.Id, value: notification.Read };
	webSocket.trigger('MarkAsRead', json);

	viewModel.Remove(notification);
};

toggleSelection = function (item) {
	var selected = item.Selected();
	var value = item.Value();

	if (selected === true)
		console.log("Selecting '" + value + "'");
	else
		console.log("Deselecting '" + value + "'");

	ko.utils.arrayMap(viewModel.SelectedTypes(), function (type) {
		if (type.Value() === value)
			type.Selected(item.Selected());
	});

	var selectedTypes = ko.utils.arrayMap(ko.utils.arrayFilter(viewModel.SelectedTypes(), function (type) {
		return type.Selected() === true;
	}), function (type) {
		return type.Value();
	});

	// Tell XSockets about the change
	webSocket.publish('set_SelectedTypes', selectedTypes);
	return true;
};
