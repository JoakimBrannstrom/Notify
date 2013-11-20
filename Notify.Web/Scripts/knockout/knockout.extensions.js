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
		var doubleDigits = function (value) {
			value = "" + value;

			if (value.length === 1) {
				value = "0" + value;
			}

			return value;
		};

		try {
				var jsonDate = ko.utils.unwrapObservable(valueAccessor());
				var dateValue = parseJsonDateString(jsonDate);

				var strDate = doubleDigits(dateValue.getFullYear()) + '-' +
								doubleDigits(dateValue.getMonth() + 1) + '-' +
								doubleDigits(dateValue.getDate());

				var strTime = doubleDigits(dateValue.getHours()) + ':' +
								doubleDigits(dateValue.getMinutes()) + ':' +
								doubleDigits(dateValue.getSeconds());

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
