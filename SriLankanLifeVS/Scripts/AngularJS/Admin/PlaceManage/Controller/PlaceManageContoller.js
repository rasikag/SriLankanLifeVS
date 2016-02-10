﻿'use strict';

srilankanlife.controller('placeController', function placeController($scope, dataPlace, $timeout) {

    $scope.messageDiv = true;


    $scope.getTownByName = function (val) {
        return dataPlace.getTownByName(val)
             .then(function (response) {
                 console.log(response);
                 return response.data.map(function (item) {
                     return item.TownName;
                 });
             });
    };

    $scope.addPlace = function (place, townForm) {
        dataPlace.addPlace(place)
            .then(function (respond) {

                $scope.messageDiv = false;
                $scope.messageStatment = respond.data;
                $scope.place = "";
                $timeout(function () {
                    $scope.messageDiv = true;
                }, 3000);

            }, function (respond) {


                $scope.messageDiv = false;
                $scope.messageStatment = respond.data.Message;

                $timeout(function () {
                    $scope.messageDiv = true;
                }, 3000);

            });
    };

    var getAllPlaces = function getAllPlace() {
        dataPlace.getAllPlaces()
            .then(function (respons) {
                //console.log(respons);
                var places = new Array();
                for (var i = 0 ; i < respons.data.length; i++) {

                    var obj = {
                        Id: respons.data[i].Id,
                        TownName: respons.data[i].TownName,
                        DistrictId: respons.data[i].DistrictId,
                        DName: respons.data[i].District.DistrictName,

                        TownTextId: 'T' + respons.data[i].Id,
                        TownInputId: 'I' + respons.data[i].Id,
                        DistrictTextId: 'T' + respons.data[i].Id + '_' + respons.data[i].DistrictId,
                        DistrictInputId: 'I' + respons.data[i].Id + '_' + respons.data[i].DistrictId,

                        EditButtonId: 'E_' + respons.data[i].Id,
                        SaveButtonId: 'S_' + respons.data[i].Id
                    };

                    twons.push(obj);
                }
                $scope.allTowns = twons;
                //console.log(twons);
            }, function (respons) {
                //console.log(respons);
            });
    };

    //$scope.editTown = function (towmE) {

    //    //console.log(towmE);

    //    var showInputTown = "#" + towmE.TownInputId;
    //    var hideTown = "#" + towmE.TownTextId;
    //    var showInputDistrict = "#" + towmE.DistrictInputId;
    //    var hideDistrict = "#" + towmE.DistrictTextId;
    //    var showSave = "#" + towmE.SaveButtonId;
    //    var hideEdit = "#" + towmE.EditButtonId;

    //    $(hideTown).addClass("ng-hide");
    //    $(showInputTown).removeClass("ng-hide");
    //    $(showInputDistrict).removeClass("ng-hide");
    //    $(hideDistrict).addClass("ng-hide");
    //    $(showSave).removeClass("ng-hide");
    //    $(hideEdit).addClass("ng-hide");

    //};

    //$scope.saveEditedTown = function (towmE) {
    //    //console.log(towmE);

    //    var obj = {
    //        TownId: towmE.Id,
    //        TownName: towmE.TownName,
    //        DistrictName: towmE.DName
    //    };

    //    dataTown.editTown(obj)
    //        .then(function (response) {
    //            getAll();
    //            //console.log(response);
    //        }, function (response) {
    //            //console.log(response);
    //        });

    //};

    //$scope.deleteTown = function (Id) {

    //    dataTown.deleteTown(Id).then(
    //        function (response) {
    //            getAll();
    //        }, function (response) {

    //        });
    //}

    getAllPlaces();

});