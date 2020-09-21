

//displays the phone number in table in the correct form and adds click event
function customizePhoneNumber($cell, cellInfo) {
    rowIndex = cellInfo.rowIndex;
    number = cellInfo.text;
    formatedNumber = number ? "+7(" + number.slice(0, 3) + ")" + number.slice(3, 6) + "-" + number.slice(6, 8) + "-" + number.slice(8, 10) : "";
    $cell.append("<a href='#' class='phonebook__link' onclick='clickEditBtn(" + rowIndex + ")'>" + formatedNumber + "</a>");
}

//trigger click Edit button by link on Phone Number
function clickEditBtn(rowIndex) {
    var dataGrid = $("#PhoneNumbers").dxDataGrid("instance");
    dataGrid.editRow(rowIndex);
    //focused row need to get correct index of row to delete 
    dataGrid.option("focusedRowIndex", rowIndex);
}

//for cascading lookup district-microdistrict
function onEditorPreparing(e) {
    if (e.parentType === "dataRow" && e.dataField === "MicrodistrictId") {
        e.editorOptions.disabled = (typeof e.row.data.DistrictId !== "number");
    }
}

function setDistrictValue(rowData, value) {
    rowData.DistrictId = value;
    rowData.MicrodistrictId = null;
}

//checks uniqness of phone number
function asyncValidationNumber(params) {
    //pass all data to a viewmodel
    var model = {};
    model.ID = params.data.ID;
    model.UniqueText = params.value;

    var data = JSON.stringify(model);
    return $.ajax({
        url: '@Url.Action("CheckUniquePhoneNumber", "PhoneNumbersWebApi")',
        type: "Post",
        contentType: "application/json",
        data: data,
        error: (error) => {
            console.log(JSON.stringify(error));
        }
    });
}


function savePhoneNumber() {
    var dataGrid = $("#PhoneNumbers").dxDataGrid("instance");
    console.log("here")
    dataGrid.saveEditData();
}
function deleteFocusedPhoneNumberFromPopup() {
    var dataGrid = $("#PhoneNumbers").dxDataGrid("instance");
    console.log("there");
    var popup = $("#PhoneNumbersPopup");
    deleteFocusedRowFromPopup(dataGrid, popup);
}
function customizeAddBtn(e) {
    //add buttons is displayed with text
    e.toolbarOptions.items[0].showText = "Always";
    //unfocuse rows when click  
    var dataGrid = $("#PhoneNumbers").dxDataGrid("instance");
    e.toolbarOptions.items[0].onClick = function () {
        console.log("unfocuse");
        dataGrid.option("focusedRowIndex", -1);
    }
}
function setActiveNav() {
    $("#IndexNavItem")
}