'use strict';
app.factory('permissionSvc', function ($rootScope) {
    var permissionList;
    return {
        setPermissions: function (permissions) {
            permissionList = permissions;

            // broadcast an event that will be listened by our custom directive to re-render it if the permissions changed
            $rootScope.$broadcast('permissionsChanged');
        },
        hasPermission: function (permission) {
            permission = permission.trim();
            return _.some(permissionList, function (item) {
                if (_.isString(item)) {
                    return item.trim() === permission;
                }
            });
        }
    };
});
  