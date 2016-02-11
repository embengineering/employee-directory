'use strict';
app.factory('permissionSvc', function ($rootScope) {
    var userRoles;
    return {
        setPermissions: function (currentRoles) {
            userRoles = currentRoles;

            // broadcast an event that will be listened by our custom directive to re-render it if the permissions changed
            $rootScope.$broadcast('permissionsChanged');
        },
        hasPermission: function (permissionsAllowed) {
            permissionsAllowed = permissionsAllowed || [];
            return _.intersection(permissionsAllowed, userRoles).length > 0;
        }
    };
});
  