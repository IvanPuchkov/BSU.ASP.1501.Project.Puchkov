function RemoveRowById(rowid) {
    var row = document.getElementById(rowid);
    if (row)
        row.parentNode.removeChild(row);
    else {
        alert("Can't remove row from the table");
    }
}
