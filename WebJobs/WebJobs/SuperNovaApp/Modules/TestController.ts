/// <reference path="../_references.ts" />
module TestApp {
    'use strict';

    interface TestControllerScope extends ng.IScope {
        sets: Array<Supernova.Set>;
        selectedSet: Supernova.Set;
        getCard: getCardFunc;
    }

    interface getCardFunc {
        (cardName: string)
    }

    export class TestController {
        constructor(private $scope: TestControllerScope, private $http: ng.IHttpService) {
            $scope.sets = [];
            $scope.selectedSet = null;

            $http.get('/api/set/Get').then(function (result: ng.IHttpPromiseCallbackArg<Array<Supernova.Set>>) {
                $scope.sets = result.data.sort((a, b) => { return a.Name < b.Name ? -1 : 1; });
                $scope.selectedSet = $scope.sets[0];
                console.log($scope.sets);
            })

            $scope.getCard = function (cardName: string) {
                $http.get('/api/card/GetEntity/' + cardName + ',' + $scope.selectedSet.Name).then(function (result) {
                    console.log(result);
                });
            }
        }
    }
}