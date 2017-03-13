/// <reference path="master-sheet-all.controller.js" />
(function () {
    'use strict'
    angular.module("age.ranger").controller("personMaintenanceListController", personMaintenanceList);

    personMaintenanceList.$inject = ["$scope", "$state", "httpCallService", "$http", "popupService", "$filter", "$stateParams"];

    function personMaintenanceList(scope, state, httpCallService, $http, popupService, filter, stateParams) {
        var vm = this;
        vm.data = {};
        vm.func = {};

        vm.data.gridOptions = {
            data: [],
            enablePinning: true,
            columnDefs: [{ field: "Id", width: 80, enablePinning: true, enableFiltering: false },
                        { field: "FirstName", enablePinning: true},
                        { field: "LastName", enablePinning: true},
                        { field: "Age", width: 120, type: 'number', enableFiltering: false},
                        { field: "AgeGroup", width: 120, enableFiltering: true },
                        {name: 'Action', 
                        cellEditableCondition: false,
                        enableFiltering: false,
                        cellTemplate: '<button class="btn-primary" ng-click="grid.appScope.vm.func.editPerson(row)" title="Click to Edit Person Detail">Edit</button><button class="btn-danger" ng-click="grid.appScope.vm.func.deletePerson(row)" title="Click to Delete Person Detail">Delete</button>'
                        }
                ],
        enableSorting: true,
        multiSelect: false,
        enableFiltering: true,
        paginationPageSizes: [25, 50, 75],
        paginationPageSize: 25
    };

        function init() {


            setTimeout(function () {
                vm.func.searchPersonList();
            }, 200);
        }

        vm.func.searchPersonList = function () {
            httpCallService.httpCall("personinformation/getpersonlist", null,true, false, false).then(function (response) {
                if (response.data && response.data.length > 0) {
                    vm.data.personData = response.data;
                }
                else {
                    vm.data.personData = [];
                }
                vm.data.gridOptions.data = vm.data.personData;

            }, function onError(reason) {
                popupService.successErrorPopUp("Error Occured: " + reason.data && reason.data.ExceptionMessage + ". Please try again later or contact your system administrator.", "ERROR");
            });
        }

        vm.func.addPerson = function () {
            vm.data.personDetails = {
                Id: 0,
                FirstName: '',
                LastName: '',
                Age: 0,
                AgeGroup: scope.mc.func.getAgeGroup(0),
                isNew: true 
            };
            popupService.addUpdatePersonInformation(vm.data.personDetails, scope.mc, vm.func.saveSuccessOrFailure);
        }
        vm.func.editPerson = function (row) {
            vm.data.personDetails = {
                Id: row.entity.Id,
                FirstName: row.entity.FirstName,
                LastName: row.entity.LastName,
                Age: row.entity.Age,
                AgeGroup: row.entity.AgeGroup,
                isNew: false
            };
            popupService.addUpdatePersonInformation(vm.data.personDetails, scope.mc, vm.func.saveSuccessOrFailure);
        }
        vm.func.deletePerson = function (row) {
            var confirmMessage = "Are you sure you want to delete the person with details below:\n";
            confirmMessage += "Id: " + row.entity.Id + "\n";
            confirmMessage += "Firstname: " + row.entity.FirstName + "\n";
            confirmMessage += "Lastname: " + row.entity.LastName + "\n";
            confirmMessage += "Age: " + row.entity.Age + "\n";
            confirmMessage += "AgeGroup: " + row.entity.AgeGroup + "\n";
            popupService.confirmPopUp(confirmMessage, row.entity, vm.func.confirmDeletion);
        }
        vm.func.saveSuccessOrFailure = function (popInfoData) {
            vm.data.personDetails = popInfoData.personDetails;
            if (popInfoData.saveStatus === "SUCCESS") {
                popupService.successErrorPopUp("Data Saved Successfully!", "SUCCESS", init);
                if (popInfoData.personDetails.isNew) {
                    vm.data.personData.push({
                        Id: popInfoData.personDetails.Id,
                        FirstName: popInfoData.personDetails.FirstName,
                        LastName: popInfoData.personDetails.LastName,
                        Age: popInfoData.personDetails.Age,
                        AgeGroup: popInfoData.personDetails.AgeGroup,
                    });
                }
                else {
                    for (var i = 0; i < vm.data.personData.length; i++) {
                        if (popInfoData.personDetails.Id === vm.data.personData[i].Id) {
                            vm.data.personData[i].FirstName = popInfoData.personDetails.FirstName;
                            vm.data.personData[i].LastName = popInfoData.personDetails.LastName;
                            vm.data.personData[i].Age = popInfoData.personDetails.Age;
                            vm.data.personData[i].AgeGroup = popInfoData.personDetails.AgeGroup;
                            break;
                        }
                    }
                }
                vm.data.gridOptions.data = vm.data.personData;
            }
            else {
                popupService.successErrorPopUp(popInfoData.ErrorMessage, "ERROR", vm.func.openPopAgainOnError);
            }
        }
        vm.func.confirmDeletion = function(personDetails) {
            httpCallService.httpCall("personinformation/deletepersoninformation", { Id: personDetails.Id }, true, false, false).then(function (response) {
                if (response.data) {
                    for (var i = 0; vm.data.personData.length; i++) {
                        if (personDetails.Id === vm.data.personData[i].Id) {
                            vm.data.personData.splice(i, i);
                            popupService.successErrorPopUp("Person Detail Successfully deleted", "SUCCESS");
                            break;
                        }
                    }
                }
                vm.data.gridOptions.data = vm.data.personData;

            }, function onError(reason) {
                popupService.successErrorPopUp("Error Occured: " + reason.data && reason.data.ExceptionMessage + ". Please try again later or contact your system administrator.", "ERROR");
            });

        }

        init();
    }
})();