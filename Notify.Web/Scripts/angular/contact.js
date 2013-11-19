



var webSocket = new XSockets.WebSocket('ws://127.0.0.1:4502/Notification');

var notificationApp = angular.module('notificationApp', ['XSockets']);

notificationApp.controller('NotificationController', function ($scope) {
	$scope.webSocket = webSocket;
	$scope.maxLength = 10;

	$scope.ctrl = new NotificationController($scope);
});

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
