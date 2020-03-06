function cascadingDropDown(parentID, ChildID, ChildTitle, URLPath, ParmName) {
    var parentValue = $(parentID).val();
    var controllerParm = ParmName;
    var data = JSON.parse('{"' + controllerParm + '":"' + parentValue + '"}'); 
        $(ChildID).selectpicker({ title: ChildTitle }).selectpicker('render');
    $.ajax({
        url: URLPath,
        type: 'Get',
        dataType: 'JSON',
        data: data,
            success: function (result) {
                var childList = $(ChildID);
                childList.find('option').remove().end();
                var counter = 0;
                $.each(result, function () {
                    childList.append('<option Value =' + result[counter].Value + ' >' + result[counter].Text + '</option > ');
                    counter++;
                });
                $(ChildID).selectpicker('refresh');
        }  });
}
