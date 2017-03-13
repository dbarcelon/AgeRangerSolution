(function () {
    'use strict'
    angular.module("age.ranger").controller("errorNotAuthorizedController", errorNotAuthorized);

    errorNotAuthorized.$inject = ["$scope", "$state", "$stateParams"];

    function errorNotAuthorized(scope, state, stateParams) {
        var vm = this;
        vm.data = {};
        vm.func = {};

        function init() {
            sessionStorage.setItem("AgeRanger-User-Profile", {});
            scope.mc.data.UserProfile = sessionStorage.getItem("AgeRanger-User-Profile");
        }

        init();
    }
})();