(function () {
    'use strict'
    angular.module("age.ranger", ["ui.router"
                                , "ui.grid"
                                ,'ui.grid.pagination'
                                //, "ngAnimate"
                                , "blockUI"
                                , "ui.bootstrap"
    ]).run(["$state", "$stateParams", "$rootScope", function ($state, $stateParams, $rootScope) {
        $rootScope.uiRouteScope = { state: $state, stateParams: $stateParams };
    }]).config(["$stateProvider", "$urlRouterProvider", "blockUIConfig", "$compileProvider", function (stateProvider, urlRouterProvider, blockUIConfig, compileProvider) {
        blockUIConfig.autoBlock = false;

        urlRouterProvider.otherwise("person-maintenance-list");
        stateProvider
            .state("person-maintenance-list", {
                url: "/person-maintenance-list",
                templateUrl: "person-maintenance-list/person-maintenance-list.html",
                controller: "personMaintenanceListController",
                controllerAs: "vm"
            })
            .state("error-not-authorized", {
                url: "/error-not-authorized",
                templateUrl: "error-not-authorized/error.html",
                controller: "errorNotAuthorizedController",
                controllerAs: "vm"
            })

    }]);


})();