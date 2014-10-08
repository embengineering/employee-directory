'use strict';
app.factory('employeeSvc', ['$http', function ($http) {

    var serviceBase = 'http://localhost/employees.svc/';
    var employeeServiceFactory = {};

    var getEmployees = function () {

        return $http.get(serviceBase + 'api/employee/all').then(function (results) {
            return results;
        });
    };

    // public methods
    employeeServiceFactory.getEmployees = getEmployees;

    return employeeServiceFactory;

}]);