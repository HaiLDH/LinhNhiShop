(function (app) {
    app.controller('loginAdminController', loginAdminController);

    loginAdminController.$inject = ['$scope','$state']

    function loginAdminController($scope, $state) {
        $scope.loginSubmit = function () {
            $state.go('home');
        }
    }
})(angular.module('linhnhishop'));