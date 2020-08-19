function DataTable() {
    $(document).ready(function () {

        $('#myTable').DataTable();
    });
}

function DataTablesRemove() {
    $(document).ready(function () {
        $('#myTable').DataTable().destroy();
    });
}

DataTablesRemove();

window.test = {
    historyGo(value) {
        window.history.go(value);
    }
};