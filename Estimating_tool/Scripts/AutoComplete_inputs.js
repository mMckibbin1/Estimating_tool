function inputAutoComplete(path, inputID, dataType) {
    var names;
    $.ajax({
        url: path,
        type: "Get",
        dataType: "JSON",
        data: { type: dataType },
        success: function (result) {
            names = result;
            var option = {
                data: names,
                list: {
                    match: {
                        enabled: true
                    }
                }

            };
            $(inputID).easyAutocomplete(option);
        }
    });
}
function inputAutoCompleteOne(path, inputID) {
    var names;
    $.ajax({
        url: path,
        type: "Get",
        dataType: "JSON",
        success: function (result) {
            names = result;
            var option = {
                data: names,
                list: {
                    match: {
                        enabled: true
                    }
                }

            };
            $(inputID).easyAutocomplete(option);
        }
    });
}
