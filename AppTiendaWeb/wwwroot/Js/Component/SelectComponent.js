var SelectComponent = {
    FillSelect: (idSelect, arrayGeneric) => {
        for (var index = 0; index < arrayGeneric.length; index++) {
            $(idSelect).append('<option value="' + arrayGeneric[index].value + '">' + arrayGeneric[index].name + '</option>');
        }
        $(idSelect).change();
    }
}