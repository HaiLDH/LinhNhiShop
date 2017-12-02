
(function (app) {
    app.filter("statusFilter", function () {
        return function (input) {
            if (status = true)
                return "Kích hoạt"
            else
                return "Khóa"
        }
    });
})(angular.module("linhnhishop.common"));