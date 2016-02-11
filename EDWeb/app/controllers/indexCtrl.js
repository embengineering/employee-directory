'use strict';
app.controller('indexCtrl', ['$scope', '$rootScope', '$location', 'authSvc', 'permissionSvc',
    function ($scope, $rootScope, $location, authSvc, permissionSvc) {

    $scope.navbarExpanded = false;

    // expose logOut method to the scope
    $scope.logOut = function () {
        authSvc.logOut();
        $location.path('/login');
    }

    // expose authentication to the scope
    $scope.authentication = authSvc.authentication;

    // when defining a route a new extra parameter “permission” is supported for the permission that it requires 
    // then after that we will test in a routeChangeStart event to check if the user has permissions for that route
    $scope.$on('$routeChangeStart', function (scope, next, current) {
        if (next.$$route) {
            var permission = next.$$route.permission || [];
            if (!permission.length || !permissionSvc.hasPermission(permission)) {
                $location.path('/login');
            }
        }
    });

}]);