var dt = $(".operation-params tr td:contains('date-time')");

$.each(dt, function (key, value) {
	$("input.parameter", value.parentElement)[0].type = 'datetime-local';
});