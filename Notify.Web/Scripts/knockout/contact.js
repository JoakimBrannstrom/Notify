
var webSocket = new XSockets.WebSocket('ws://127.0.0.1:4502/Notification');

$().ready(function () {
	$(window).on('beforeunload', function () {
		try {
			webSocket.close(1000, 'Window unload');
		} catch (e) {
			console.log(e);
		}
	});
});

$(function () {
	var viewModel = new NotificationViewModel(webSocket, 10);

	ko.applyBindings(viewModel);

	webSocket.on(XSockets.Events.open, function () {
		console.log("XSockets open");
	});

	webSocket.bind(XSockets.Events.onError, function (error) {
		console.log("XSockets error: ", error);
	});

	webSocket.bind(XSockets.Events.close, function () {
		// This event will be fired when server disconnects / closes the current connection
		console.log("Connection closed.");
	});
});

