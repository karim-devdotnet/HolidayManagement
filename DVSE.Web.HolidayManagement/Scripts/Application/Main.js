
$.validator.addMethod('date', function (value, element) {
    if (this.optional(element)) {
        return true;
    }

    try {
        $.datepicker.parseDate('dd-mm-yy', value);
    }
    catch (err) {
        return false;
    }

    return true;
});

String.prototype.parseInt = function () {
    return parseInt(this);
};