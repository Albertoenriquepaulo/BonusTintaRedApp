console.log("DataTable");
function DataTable() {
    $(document).ready(function () {
        $('#myTable').DataTable();
    });
}

window.test = {
    historyGo(value) {
        window.history.go(value);
    }
};