//sets russian localization
DevExpress.localization.locale("ru");

function deleteFocusedRowFromPopup(dataGrid, popup) {
    var index = dataGrid.option("focusedRowIndex");
    popup.dxPopup("hide");
    dataGrid.deleteRow(index);
}

//create context menu
function parseContextMenu(e) {
    if (e.row.rowType === "data") {
        e.items = [{
            text: "Редактировать",
            onItemClick: function () {
                e.component.option("focusedRowIndex", -1);
                e.component.option("focusedRowIndex", e.row.rowIndex);
                e.component.editRow(e.row.rowIndex);
            }
        },
        {
            text: "Удалить",
            onItemClick: function () {
                e.component.deleteRow(e.row.rowIndex);
            }
            }
        ];
    }
}
