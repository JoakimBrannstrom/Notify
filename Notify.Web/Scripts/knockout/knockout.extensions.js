$(function () {
	var jsonDateRe = /^\/Date\((-?\d+)(\+|-)?(\d+)?\)\/$/;
	var parseJsonDateString = function (dateCandidate) {
		var arr = dateCandidate && jsonDateRe.exec(dateCandidate);
		if (arr) {
			return new Date(parseInt(arr[1]));
		}
		return dateCandidate;
	};

	ko.bindingHandlers.date = {
		init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			try {
				var jsonDate = ko.utils.unwrapObservable(valueAccessor());
				var dateValue = parseJsonDateString(jsonDate);
				var strDate = dateValue.getFullYear() + '-' + (dateValue.getMonth() + 1) + '-' + dateValue.getDate();
				var strTime = dateValue.getHours() + ':' + dateValue.getMinutes() + ':' + dateValue.getSeconds();

				$(element).html(strDate + ' ' + strTime);
			}
			catch (exc) {
			}

			$(element).change(function () {
				var valAcc = valueAccessor;
				valAcc(element.getAttribute('value'));
			});
		},
		update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
			var valAcc = valueAccessor;
			valAcc(element.getAttribute('value'));
		}
	};
});
