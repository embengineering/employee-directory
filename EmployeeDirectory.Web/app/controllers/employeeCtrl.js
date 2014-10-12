'use strict';
app.controller('employeeCtrl', ['$scope', '$http', '$location', 'globalProperties', 'employeeSvc',
function ($scope, $http, $location, globalProperties, employeeSvc) {

    // helper method
    // TODO: move again in a separate file
    $.fn.fixItemHeight = function (itemSelector) {
        var oCanvas = typeof itemSelector === 'undefined' ? $(this).parent() : $(this);
        var oItems = typeof itemSelector === 'undefined' ? $(this) : oCanvas.find(itemSelector);
        var fnSetSameHeight = function (aStack) {

            var tallest = 0;
            $.each(aStack, function (iIndex, oItem) {
                var thisHeight = $(oItem).height();
                if (thisHeight > tallest) {
                    tallest = thisHeight;
                }
            });

            // Set final height
            $.each(aStack, function (iIndex, oItem) {
                if (tallest == 0) {
                    $(oItem).css('height', 'auto');
                } else {
                    $(oItem).css('min-height', tallest);
                }
            });
        };

        // Remove previous height assigned for items in canvas
        oItems.css('height', 'auto').css('min-height', 'initial');

        var aStack = [], oTmpItem = null, oItem = null, iPosPrev = 0, iPosNext = 0;
        oItems.each(function (i, item) {

            // Set webpart item as a jQuery object
            oItem = $(item);

            // Determine if first webpart and add to stack automatically
            if (!oTmpItem) {
                oTmpItem = oItem;
                aStack.push(oTmpItem);
            } else {

                iPosPrev = oTmpItem.offset().top;
                iPosNext = oItem.offset().top;

                // Determine if previous webpart is in the same line (row) as next webpart
                if (iPosNext == iPosPrev) {
                    aStack.push(oTmpItem);
                    aStack.push(oItem);
                } else {

                    // Format using the highest
                    fnSetSameHeight(aStack);

                    // Empty stack
                    aStack = [];
                    oTmpItem = null;
                }

                // Set current webpart as next temporary for comparation
                oTmpItem = oItem;
            }
        });

        // Determine if there are more than one web part in the stack
        if (aStack.length > 1) {

            // Format using the highest
            fnSetSameHeight(aStack);

            // Empty stack
            aStack = [];
            oTmpItem = null;
        }

    };

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
            model: {
                fields: {
                    FirstName: { type: 'string' },
                    LastName: { type: 'string' },
                    MiddleInitial: { type: 'string' },
                    SecondLastName: { type: 'string' },
                    JobTitle: { type: 'string' },
                    Location: { type: 'string' },
                    PhoneNumber: { type: 'string' },
                    Email: { type: 'string' }
                }
            },
            data: 'items',
            total: 'count'
        },
        sort: [{ field: 'FirstName', dir: 'asc' }],
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

    // set default sorting direction
    $scope.sortDir = 'asc';

    // handle sorting
    $scope.sortList = function (dir) {
        // toggle sorting direction
        $scope.sortDir = $scope.sortDir === 'asc' ? 'desc' : 'asc';
        $scope.source.query({
            sort: {
                field: "FirstName", dir: $scope.sortDir
            },
            page: 1,
            pageSize: 6
        });
    };

    // set property for filtering pattern
    $scope.filterPattern = '';

    // handle filtering
    $scope.filterList = function (value) {
        // toggle sorting direction
        $scope.source.query({
            sort: {
                field: 'FirstName', dir: $scope.sortDir
            },
            filter: [
                {
                    logic: 'or',
                    filters: [
                        { field: 'FullName', operator: 'contains', value: value },
                        { field: 'Email', operator: 'contains', value: value },
                        { field: 'PhoneNumber', operator: 'contains', value: value },
                        { field: 'JobTitle', operator: 'contains', value: value },
                        { field: 'Location', operator: 'contains', value: value }
                    ]
                }
            ],
            page: 1,
            pageSize: 6
        });
    };

    // kendo listview Events
    $scope.onDataBound = function (event) {
        // TODO: fixing height of items in not working because angular databound is happening after listview is drawn
        //event.sender.items().fixItemHeight();
    };

}]);