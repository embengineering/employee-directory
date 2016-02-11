var app = angular.module('EmployeeDirectoryApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ngAnimate', 'kendo.directives', 'underscore']);

app.config(function ($routeProvider, $httpProvider) {

    $routeProvider.when("/login", {
        controller: "loginCtrl",
        templateUrl: "app/views/login.html"
    });

    //$routeProvider.when("/signup", {
    //    controller: "signupCtrl",
    //    templateUrl: "app/views/signup.html"
    //});

    $routeProvider.when("/employees", {
        controller: "employeeCtrl",
        templateUrl: "app/views/employees.html",
        permission: ['HR', 'EMPLOYEE']
    });

    $routeProvider.when('/employees/add', {
        templateUrl: 'app/views/addEmployee.html',
        controller: 'manageEmployeeCtrl',
        permission: ['HR']
    });

    $routeProvider.when('/employees/edit/:id', {
        templateUrl: 'app/views/manageEmployee.html',
        controller: 'manageEmployeeCtrl',
        permission: ['HR']
    });

    $routeProvider.otherwise({ redirectTo: "/employees" });

    // http custom interceptor when using $http
    $httpProvider.interceptors.push('authInterceptorSvc');
});

app.run(['authSvc', function (authSvc) {
    authSvc.fillAuthData();
}]);

// set some global properties
app.value('globalProperties', {
    serviceBaseUrl: '/'
});