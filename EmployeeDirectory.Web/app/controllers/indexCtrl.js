'use strict';
app.controller('indexCtrl', ['$scope', '$location', 'authSvc', function ($scope, $location, authService) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.authentication = authService.authentication;

}]);