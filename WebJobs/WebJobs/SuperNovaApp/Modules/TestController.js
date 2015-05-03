/// <reference path="../_app.ts" />
var TestApp;
(function (TestApp) {
    'use strict';
    var TestController = (function () {
        function TestController($scope, $http) {
            this.$scope = $scope;
            this.$http = $http;
            $scope.sets = [];
            $scope.selectedSet = null;
            $http.get('/api/set/Get').then(function (result) {
                $scope.sets = result.data.sort(function (a, b) {
                    return a.Name < b.Name ? -1 : 1;
                });
                $scope.selectedSet = $scope.sets[0];
                console.log($scope.sets.map(function (set) {
                    return set.Name;
                }));
            });
        }
        return TestController;
    })();
    TestApp.TestController = TestController;
})(TestApp || (TestApp = {}));
//# sourceMappingURL=TestController.js.map