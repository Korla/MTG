/// <reference path="../_references.ts" />
var Supernova;
(function (Supernova) {
    var Set = (function () {
        function Set() {
        }
        return Set;
    })();
    Supernova.Set = Set;
    var Card = (function () {
        function Card() {
        }
        return Card;
    })();
    Supernova.Card = Card;
})(Supernova || (Supernova = {}));
/// <reference path="../_references.ts" />
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
                console.log($scope.sets);
            });
            $scope.getCard = function (cardName) {
                $http.get('/api/card/GetEntity/' + cardName + ',' + $scope.selectedSet.Name).then(function (result) {
                    console.log(result);
                });
            };
        }
        return TestController;
    })();
    TestApp.TestController = TestController;
})(TestApp || (TestApp = {}));
/// <reference path="index.ts" />
/// <reference path="Modules/Supernova.ts" />
/// <reference path="Modules/TestController.ts" /> 
/// <reference path="_references.ts" />
angular.module('SuperNova', []).controller('TestController', TestApp.TestController);
//# sourceMappingURL=app.js.map