'use strict';
app.directive('hasPermission', ['permissionSvc', function (permissionSvc) {
    return {
        link: function (scope, element, attrs) {
            if (!_.isString(attrs.hasPermission)) {
                throw "hasPermission value must be a string of roles separated by comma";
            }

            var permissionsAllowed = attrs.hasPermission.split(/\,/).map(function (item) { return item.trim();  });

            function toggleVisibilityBasedOnPermission() {
                var hasPermission = permissionSvc.hasPermission(permissionsAllowed);

                if (hasPermission) {
                    element.show();
                } else {
                    element.hide();
                }
            }

            toggleVisibilityBasedOnPermission();

            scope.$on('permissionsChanged', toggleVisibilityBasedOnPermission);
        }
    };
}]);