(function () {
    'use strict'

    angular.module("age.ranger").controller("errorSuccessPopupController", errorSuccessPopup);

    //errorSuccessPopup.$inject = ["$modalInstance", "popupInfo"];
    errorSuccessPopup.$inject = ["$uibModalInstance", "popupInfo", "$sce"];

    function errorSuccessPopup(modalInstance, popupInfo, sce) {
        var vm = this;
        vm.data = {};
        vm.func = {};

        function init() {
            vm.data.popupInfo = {};
            vm.data.popupInfo.status = popupInfo.status;
            vm.data.popupInfo.message = sce.trustAsHtml(popupInfo.message);
        }
        vm.func.ok = function () {
            modalInstance.close("Yes");
        }
        vm.func.cancel = function () {
            modalInstance.close("No");
        }

        init();
    }
})();