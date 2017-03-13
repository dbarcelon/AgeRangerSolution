(function () {
    'use strict'
    angular.module("age.ranger").controller("mainController", main);
    main.$inject = ["$state", "httpCallService", "popupService", "$http", "$scope", "$uibModal"];
    function main(state, httpCallService, popupService, $http, scope, uibModal) {
        var vm = this;
        vm.data = {};
        vm.func = {};

        //vm.data.serviceRootUrl = "http://localhost/AgeRanger.API/api/";

        function init() {
            httpCallService.httpCall("agegroupinformation/getagegroup",null, true, false, false).then(function (response) {
                if (response.data && response.data.length > 0) {
                    vm.data.ageGroupData = response.data;
                }
                else {
                    vm.data.ageGroupData = [];
                }
            }, function onError(reason) {
                popupService.successErrorPopUp("Error Occured: " + reason.data && reason.data.ExceptionMessage + ". Please try again later or contact your system administrator.", "ERROR");
            });

            state.go("person-maintenance-list");
        }

        vm.func.getAgeGroup = function (age) {
            var ageGroup="";
            for (var i = 0; i < vm.data.ageGroupData.length; i++) {
                if ((vm.data.ageGroupData[i].MinAge===null || age>=vm.data.ageGroupData[i].MinAge) && (vm.data.ageGroupData[i].MaxAge===null || age<=vm.data.ageGroupData[i].MaxAge)) {
                    ageGroup=vm.data.ageGroupData[i].Description;
                    break;
                }
            }
            return ageGroup;
        }


        init();
    }
})();