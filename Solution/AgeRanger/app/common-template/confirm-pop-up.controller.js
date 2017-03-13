(function () {
    'use strict'

    angular.module("age.ranger").controller("confirmPopupController", confirmPopup);

    //confirmPopup.$inject = ["$modalInstance", "popupInfo"];
    confirmPopup.$inject = ["$uibModalInstance", "popupInfo"];

    function confirmPopup(modalInstance, popupInfo) {
        var vm = this;
        vm.data = {};
        vm.func = {};
        vm.data.popupInfo = popupInfo;
        vm.func.ok = function () {
            vm.data.popupInfo.actionName = "Yes";
            modalInstance.close(vm.data.popupInfo);
        }
        vm.func.cancel = function () {
            modalInstance.close("No");
        }
    }
})();