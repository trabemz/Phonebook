function selection(selectedItems) {
    var dataGridMicrodistrict = $("#microdistricts").dxDataGrid("instance");
    var districtId;
    if (!selectedItems.selectedRowKeys) {
        //select first district
        var dataGridDistrict = $("#districts").dxDataGrid("instance");
        dataGridDistrict.selectRowsByIndexes(0);
        //get Id of first district
        districtId = dataGridDistrict.getKeyByRowIndex(0);
    }
    else {
        //get Id of selected district
        districtId = selectedItems.selectedRowKeys[0];
    }
    //filter microdistricts by selected district
    dataGridMicrodistrict.filter(["DistrictId", "=", districtId]);
}

//checks uniqness of phone number
function asyncValidationName(params) {
    //create UniqueViewModel
    var model = {};
    model.id = params.data.ID;
    model.UniqueText = params.value;

    var data = JSON.stringify(model);
    return $.ajax({
        url: '@Url.Action("CheckUniqueName", "TerritorialUnitsWebApi")',
        type: "Post",
        contentType: "application/json",
        data: data,
        error: (error) => {
            console.log(JSON.stringify(error));
        }
    });
}

function saveDistrict() {
    var dataGrid = $("#districts").dxDataGrid("instance");
    dataGrid.saveEditData();
}

function saveMicrodistrict() {
    var dataGrid = $("#microdistricts").dxDataGrid("instance");
    dataGrid.saveEditData();
}

//add buttons is displayed with text
function customizeAddBtn(e) {
    e.toolbarOptions.items[0].showText = "Always";

}