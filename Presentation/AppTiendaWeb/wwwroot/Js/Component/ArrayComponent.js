var ArrayResult = [{ value: 0, name: "" }];
var ArrayComponent =
{
    DynamicArrayToSelectArray: (arrayDynamic, propertyId, propertyName) => {
        ArrayResult = [];
        arrayDynamic.map(item => {
            let newItem = { value: 0, name: "" };
            $.each(item, (key, value) => {
                switch (key) {
                    case propertyId:
                        newItem.value = value;
                        break;
                    case propertyName:
                        newItem.name = value;
                        break;
                }
            });
            ArrayResult.push(newItem);
        });
        return ArrayResult;
    }
};
