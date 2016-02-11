'use strict';
app.controller('manageEmployeeCtrl', ['$scope', '$http', '$location', '$routeParams', 'employeeSvc',
function ($scope, $http, $location, $routeParams, employeeSvc) {

    // define property for errors
    $scope.errors = null;

    // define model
    $scope.employee = employeeSvc.employeeModel;

    // cancel form handler
    $scope.cancel = function () {

        // TODO: ask confirmation if form is dirty

        // navigate to employee list
        $location.path('/employees');
    };

    // form submit handler
    $scope.submit = function(form) {

        // set submitted flag
        $scope.submitted = true;

        // if form is invalid, show errors and prevent continue
        if (form.$invalid) {
            return;
        }

        // set submitting flag
        $scope.submitting = true;

        // add or update
        if (!$scope.employee.id) {

            // perform AJAX request
            employeeSvc.addEmployee($scope.employee).success(function (data, status, headers, config) {
                $location.path('/employees');
            }).error(function (data, status, headers, config) {
                $scope.errors = _.map(data.modelState, function (error) { return error[0]; });
                console.log(data);
                $scope.submitting = false;
            });

        } else {

            // perform AJAX request
            employeeSvc.updateEmployee($scope.employee).success(function (data, status, headers, config) {
                $location.path('/employees');
            }).error(function (data, status, headers, config) {
                $scope.errors = _.map(data.modelState, function (error) { return error[0]; });
                console.log(data);
                $scope.submitting = false;
            });
        }
    };

    // $routeParams && $routeParams.id ? $routeParams.id : ''
    // retrieve model information
    if ($routeParams && $routeParams.id) {
        employeeSvc.getEmployeeById($routeParams.id).success(function (data, status, headers, config) {
            $scope.employee = data;
        }).error(function (data, status, headers, config) {
            $scope.errors = ['Employee not found!'];
        });
    }

}]);