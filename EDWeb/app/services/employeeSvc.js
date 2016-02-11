'use strict';
app.factory('employeeSvc', ['$http', 'globalProperties', function ($http, globalProperties) {

    var employeeServiceFactory = {},
        employeeModel = {
            id: '',
            email: '',
            userName: '',
            firstName: '',
            middleInitial: '',
            lastName: '',
            secondLastName: '',
            jobTitle: '',
            location: '',
            phoneNumber: '',
            role: 'EMPLOYEE',
            password: '',
            confirmPassword: ''
        };

    // get employee by id
    var getEmployeeById = function(employeeId) {
        return $http({
            method: 'GET',
            url: globalProperties.serviceBaseUrl + 'api/employees/' + employeeId,
            headers: {
                'Content-Type': 'application/json'
            }
        });
    };

    // add employee handler
    var addEmployee = function (employee) {

        // make sure to copy email as user name before saving
        employee.userName = employee.email;

        return $http({
            method: 'POST',
            url: globalProperties.serviceBaseUrl + 'api/account/register',
            data: employee
        });
    };

    // save employee handler
    var updateEmployee = function (employee) {
        return $http({
            method: 'PUT',
            url: globalProperties.serviceBaseUrl + 'api/employees/' + employee.id,
            data: employee
        });
    };

    // delete employee handler
    var deleteEmployee = function(employeeId) {
        return $http({
            method: 'DELETE',
            url: globalProperties.serviceBaseUrl + 'api/employees/' + employeeId,
            headers: {
                'Content-Type': 'application/json'
            }
        });
    };


    // public methods
    employeeServiceFactory.employeeModel = employeeModel;
    employeeServiceFactory.getEmployeeById = getEmployeeById;
    employeeServiceFactory.updateEmployee = updateEmployee;
    employeeServiceFactory.addEmployee = addEmployee;
    employeeServiceFactory.deleteEmployee = deleteEmployee;

    return employeeServiceFactory;
}]);
  