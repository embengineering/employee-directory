'use strict';
app.factory('authSvc', ['$http', '$q', 'localStorageService', 'globalProperties', 'permissionSvc', function ($http, $q, localStorageService, globalProperties, permissionSvc) {

    var authServiceFactory = {};

    var authentication = {
        isAuth: false,
        userName: '',
        userRoles: []
    };

    var saveRegistration = function (registration) {

        logOut();

        return $http.post(globalProperties.serviceBaseUrl + 'api/account/register', registration).then(function (response) {
            return response;
        });

    };

    var login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(globalProperties.serviceBaseUrl + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            var userRoles = JSON.parse(response.userRoles) || ['EMPLOYEE'];

            localStorageService.set('authorizationData', {
                token: response.access_token,
                userName: loginData.userName,
                userRoles: userRoles
            });

            // set authentication properties
            authentication.isAuth = true;
            authentication.userName = loginData.userName;
            authentication.userRoles = userRoles;

            // set permissions
            permissionSvc.setPermissions(userRoles);

            deferred.resolve(response);

        }).error(function (err, status) {
            logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var logOut = function () {

        localStorageService.remove('authorizationData');

        authentication.isAuth = false;
        authentication.userName = "";
        authentication.userRoles = [];

    };

    var fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            authentication.isAuth = true;
            authentication.userName = authData.userName;
            authentication.userRoles = authData.userRoles;
            permissionSvc.setPermissions(authData.userRoles);
        }

    }

    // public methods
    authServiceFactory.saveRegistration = saveRegistration;
    authServiceFactory.login = login;
    authServiceFactory.logOut = logOut;
    authServiceFactory.fillAuthData = fillAuthData;
    authServiceFactory.authentication = authentication;

    return authServiceFactory;
}]);