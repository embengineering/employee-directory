'use strict';
app.controller('employeeCtrl', ['$scope', 'employeeSvc', function ($scope, ordersService) {

    $scope.employees = [];

    ordersService.getEmployees().then(function (results) {

        $scope.employees = results.data;

    }, function (error) {
        //alert(error.data.message);
    });

}]);