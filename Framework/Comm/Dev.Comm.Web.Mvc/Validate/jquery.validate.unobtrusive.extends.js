jQuery.validator.addMethod("enforcetrue", function (value, element, param) {
    return element.checked;
});
jQuery.validator.unobtrusive.adapters.addBool("enforcetrue");


jQuery.validator.addMethod("cnlength", function (value, element, param) {
    //debugger;
    var len = $(element).val().replace(/[^\x00-\xff]/g, "rr").length;
    var min = param.min,
      max = param.max;

    if (min && max) {
        return len >= min && len <= max;
    }
    else if (min) {
        return len >= min;
    }
    else if (max) {
        return len <= max;
    }

    return false;

});
$.validator.unobtrusive.adapters.add("cnlength", ["min", "max"], function (options) {
    options.rules['cnlength'] = {
        min: options.params.min,
        max: options.params.max
    };
    options.messages['cnlength'] = options.message;
});


//jQuery.validator.addMethod("enforcetrue", function (value, element, param) {
//    return element.checked;
//});
//jQuery.validator.unobtrusive.adapters.addBool("enforcetrue");

function onSuccess(inputElement, msg) {



    var container = $("[data-valmsg-for='" + $(inputElement).attr('name') + "']");

    container.removeClass("field-validation-valid").addClass("field-validation-success");

    container.empty();
    msg = (msg == undefined) ? '' : msg;
    var msgobj = $('<span for="UserName" generated="true">' + msg + '</span>');

    msgobj.appendTo(container);
}

function onError(inputElement, msg) {

    var container = $("[data-valmsg-for='" + inputElement[0].name + "']");

    container.removeClass("field-validation-valid").addClass("field-validation-error");

    container.empty();

    msg = (msg == undefined) ? '' : msg;

    var msgobj = $('<span for="UserName" generated="true">' + msg + '</span>');

    msgobj.appendTo(container);
}
