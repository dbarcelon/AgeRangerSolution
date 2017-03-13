(function () {
    'use strict'

    angular.module("age.ranger").controller("addUpdatePersonInformationPopupController", addUpdatePersonInformation);

    addUpdatePersonInformation.$inject = ["$uibModalInstance", "popupInfo", "popupService", "httpCallService", "$scope", "$stateParams", "$http"];

    function addUpdatePersonInformation(modalInstance, popupInfo, popupService, httpCallService, scope, stateParams, $http) {
        var vm = this;
        vm.data = {};
        vm.func = {};
        vm.data.popupInfo = popupInfo;

        function init() {
            //if (vm.data.popupInfo.personDetails.Id === 0) {
            //    vm.data.popupInfo.personDetails.FirstName = "";
            //    vm.data.popupInfo.personDetails.LastName = "";
            //    vm.data.popupInfo.personDetails.Age = 0;
            //    vm.data.popupInfo.personDetails.AgeGroup = "";
            //}
        }

        vm.func.cancel = function () {
            modalInstance.close("close");
        }

        vm.func.Save = function () {
            var dScope = null;
            if (vm.data.popupInfo.personDetails.FirstName === "") {
                popupService.successErrorPopUp("Please enter FirstName", "ERROR");
            }
            else if (vm.data.popupInfo.personDetails.LastName === "") {
                popupService.successErrorPopUp("Please enter LastName", "ERROR");
            }
            else if (vm.data.popupInfo.personDetails.Age < 0) {
                popupService.successErrorPopUp("Age should not be less than zero", "ERROR");
            }
            else {
                httpCallService.httpCall("personinformation/addupdatepersoninformation", vm.data.popupInfo.personDetails, true, false, false).then(function (response) {
                    if (response.data && response.data.ExceptionMessage === "") {
                        vm.data.popupInfo.action = "save";
                        vm.data.popupInfo.saveStatus = "SUCCESS";
                        if (vm.data.popupInfo.personDetails.Id===0)
                            vm.data.popupInfo.personDetails.Id = response.data.personDTO.Id;
                        modalInstance.close(vm.data.popupInfo);
                    }
                    else {
                        vm.data.popupInfo.action = "save";
                        vm.data.popupInfo.saveStatus = "ERROR";
                        vm.data.popupInfo.ErrorMessage = response.data.ExceptionMessage + ". Please try again later or contact your system administrator."; 
                        modalInstance.close(vm.data.popupInfo);
                    }
                }, function onError(reason) {
                    vm.data.popupInfo.action = "save";
                    vm.data.popupInfo.saveStatus = "ERROR";
                    vm.data.popupInfo.ErrorMessage = "Error Occured: " + reason.data && reason.data.ExceptionMessage + ". Please try again later or contact your system administrator.";
                    modalInstance.close(vm.data.popupInfo);
                });
            }
        }

        vm.func.ageChanged = function () {
            vm.data.popupInfo.personDetails.AgeGroup = vm.data.popupInfo.mainController.func.getAgeGroup(vm.data.popupInfo.personDetails.Age);
        }

        init();
    }
})();