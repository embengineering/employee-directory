'use strict';
app.controller('loginCtrl', ['$scope', '$location', 'authSvc', function ($scope, $location, authSvc) {

    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        authSvc.login($scope.loginData).then(function (response) {

            // navigate to employee list after login
            $location.path('/employees');
        }, function (err) {

            // display error message
            $scope.message = err.error_description;
        });
    };

}]);