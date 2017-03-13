(function () {
    'use strict'
    angular.module('age.ranger').service('popupService', popupService);

    //popupService.$inject = ["$modal"];
    popupService.$inject = ["$uibModal"];

    function popupService(uibModal) {

        var successErrorPopUp = function (alertMessage, status, okCallBack) {
            var modalInstance = uibModal.open({
                animation: true,
                templateUrl: "common-template/error-success-pop-up.html",
                controller: "errorSuccessPopupController",
                controllerAs: "vm",
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    popupInfo: function () {
                        return {
                            message: alertMessage,
                            status: status
                        }
                    }
                }
            });
            modalInstance.result.then(function (actionName) {
                if (actionName === "Yes") {
                    if (okCallBack && typeof (okCallBack) === 'function') { okCallBack(); }
                }
            }, function () { })
        }

        var confirmPopUp = function (alertMessage, callbackParameter, yesCallback, noCallback) {
            var modalInstance = uibModal.open({
                animation: true,
                templateUrl: "common-template/confirm-pop-up.html",
                controller: "confirmPopupController",
                controllerAs: "vm",
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    popupInfo: function () {
                        return {
                            message: alertMessage,
                            callbackParameter: callbackParameter
                        }
                    }
                }
            });
            modalInstance.result.then(function (popUpInfo) {
                if (popUpInfo.actionName === "Yes") {
                    if (yesCallback && typeof (yesCallback) === 'function') { yesCallback(popUpInfo.callbackParameter); }
                }
            }, function () { })
        }

        var addUpdatePersonInformation = function (personDetails, mainController, saveCallback, closeCallback) {
            var modalInstance = uibModal.open({
                templateUrl: "common-template/add-update-person-information-pop-up.html",
                controller: "addUpdatePersonInformationPopupController",
                controllerAs: "vm",
                size: 'lg',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    popupInfo: function () {
                        return {
                            personDetails: personDetails,
                            mainController: mainController
                        }
                    }
                }
            });

            modalInstance.result.then(function (popupInfo) {
                if (popupInfo.action === "save") {
                    if (saveCallback && typeof (saveCallback) === 'function') { saveCallback(popupInfo); }
                }
                else if (popupInfo.action === "close") {
                    if (closeCallback && typeof (closeCallback) === 'function') { closeCallback(popupInfo); }
                }
            }, function () {
                //No Operation
            });
        }


        return {
            successErrorPopUp: successErrorPopUp
            , confirmPopUp: confirmPopUp
            , addUpdatePersonInformation: addUpdatePersonInformation
        };
    }
})();