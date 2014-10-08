var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeCtrl",
        templateUrl: "app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginCtrl",
        templateUrl: "app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupCtrl",
        templateUrl: "app/views/signup.html"
    });

    $routeProvider.when("/employees", {
        controller: "employeeCtrl",
        templateUrl: "app/views/employees.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

app.config(function ($httpProvider) {
    // http custom interceptor
    $httpProvider.interceptors.push('authInterceptorSvc');
});

app.run(['authSvc', function (authService) {
    authService.fillAuthData();
}]);