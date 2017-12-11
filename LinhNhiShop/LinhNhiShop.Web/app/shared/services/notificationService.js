(function (app) {
    app.factory('notificationService', notificationService);

    function notificationService() {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 2000,
            "extendedTimeOut": 1000
        };

        function displaySuccess(success) {
            toastr.success(success);
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.forEach(function (err) {
                    toastr.error(err)
                });
            }
            else {
                toastr.error(error);
            }
        }

        function displayWarning(warning) {
            toastr.warning(warning);
        }

        function displayInfo(info) {
            toastr.info(info);
        }

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }
    }

})(angular.module('linhnhishop.common'))