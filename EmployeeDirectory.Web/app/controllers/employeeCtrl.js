'use strict';
app.controller('employeeCtrl', ['$scope', '$http', '$location', 'globalProperties', 'employeeSvc',
function ($scope, $http, $location, globalProperties, employeeSvc) {

    // set template
    $scope.listViewTemplate = $("#employeeListTemplate").html();

    // set remote data source
    $scope.source = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                var odataParams = kendo.data.transports['odata'].parameterMap(options.data, 'read');

                delete odataParams.$inlinecount; // <-- remove inlinecount parameter.
                delete odataParams.$format; // <-- remove format parameter.

                $http({
                    url: globalProperties.serviceBaseUrl + 'api/employees',
                    method: 'GET',
                    data: '',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    params: odataParams
                })
                .success(function (result) {
                    options.success(result);
                });
            }
        },
        schema: {
            data: 'items',
            total: 'count'
        },
        sort: [],
        pageSize: 6,
        serverFiltering: true, // <-- Do filtering server-side
        serverPaging: true, // <-- Do paging server-side
        serverSorting: true, // <-- Do sorting server-side
        type: 'odata' // <-- Include OData style params on query string.
    });

    // edit handler
    $scope.editEmployee = function (employeeId) {
        $location.path('/employees/edit/' + employeeId);
    };

    // delete handler
    $scope.deleteEmployee = function (employeeId) {
        event.preventDefault();
        employeeSvc
            .deleteEmployee(employeeId)
            .success(function () {
                $scope.source.read();
            })
            .error(function () { });
    };

    // add handler
    $scope.addEmployee = function () {
        $location.path('/employees/add');
    };

    // add new employee handler
    $scope.addEmployee = function () {
        $location.path('/employees/add');
    };

    /*
     $($0).data('kendoListView').dataSource.query({
        sort: { field: "Role", dir: "desc" },
        page: 1,
        pageSize: 20
    });
     */

}]);