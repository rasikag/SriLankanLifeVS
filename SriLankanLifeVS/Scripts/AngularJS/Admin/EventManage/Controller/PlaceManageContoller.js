'use strict';

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
                getAllPlaces();
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
                        PlaceName: respons.data[i].PlaceName,
                        Longitude: respons.data[i].Longitude,
                        Latitude: respons.data[i].Latitude,
                        Address: respons.data[i].Address,
                        Discription: respons.data[i].Description,
                        QuickFacts: respons.data[i].QuickFacts,
                        TownName: respons.data[i].TownName[0],

                        PlaceNameTextId: 'PlaceNameTextId' + i,
                        PlaceNameInputId: 'PlaceNameInputId' + i,
                        LongitudeTextId: 'LongitudeTextId' + i,
                        LongitudeInputId: 'LongitudeInputId' + i,

                        LatitudeTextId: 'LatitudeTextId' + i,
                        LatitudeInputId: 'LatitudeInputId' + i,
                        AddressTextId: 'AddressTextId' + i,
                        AddressInputId: 'AddressInputId' + i,

                        DiscriptionTextId: 'DiscriptionTextId' + i,
                        DiscriptionInputId: 'DiscriptionInputId' + i,
                        QuickFactsTextId: 'QuickFactsTextId' + i,
                        QuickFactsInputId: 'QuickFactsInputId' + i,

                        TownNameTextId: 'TownNameTextId' + i,
                        TownNameInputId: 'TownNameInputId' + i,


                        EditButtonId: 'E_' + i,
                        SaveButtonId: 'S_' + i
                    };

                    places.push(obj);
                }
                $scope.allPlaces = places;
                //console.log(twons);
            }, function (respons) {
                //console.log(respons);
            });
    };

    $scope.editPlace = function (placeE) {

        //console.log(towmE);

        var PlaceNameTextId = '#' + placeE.PlaceNameTextId;
        var PlaceNameInputId = '#' + placeE.PlaceNameInputId;
        var LongitudeTextId = '#' + placeE.LongitudeTextId;
        var LongitudeInputId = '#' + placeE.LongitudeInputId;

        var LatitudeTextId = '#' + placeE.LatitudeTextId;
        var LatitudeInputId = '#' + placeE.LatitudeInputId;
        var AddressTextId = '#' + placeE.AddressTextId;
        var AddressInputId = '#' + placeE.AddressInputId;

        var DiscriptionTextId = '#' + placeE.DiscriptionTextId;
        var DiscriptionInputId = '#' + placeE.DiscriptionInputId;
        var QuickFactsTextId = '#' + placeE.QuickFactsTextId;
        var QuickFactsInputId = '#' + placeE.QuickFactsInputId;

        var TownNameTextId = '#' + placeE.TownNameTextId;
        var TownNameInputId = '#' + placeE.TownNameInputId;

        var EditButtonId = '#' + placeE.EditButtonId;
        var SaveButtonId = '#' + placeE.SaveButtonId;

        $(PlaceNameTextId).addClass("ng-hide");
        $(PlaceNameInputId).removeClass("ng-hide");
        $(LongitudeTextId).addClass("ng-hide");
        $(LongitudeInputId).removeClass("ng-hide");

        $(LatitudeTextId).addClass("ng-hide");
        $(LatitudeInputId).removeClass("ng-hide");
        $(AddressTextId).addClass("ng-hide");
        $(AddressInputId).removeClass("ng-hide");

        $(DiscriptionTextId).addClass("ng-hide");
        $(DiscriptionInputId).removeClass("ng-hide");
        $(QuickFactsTextId).addClass("ng-hide");
        $(QuickFactsInputId).removeClass("ng-hide");

        $(TownNameTextId).addClass("ng-hide");
        $(TownNameInputId).removeClass("ng-hide");

        $(EditButtonId).addClass("ng-hide");
        $(SaveButtonId).removeClass("ng-hide");



    };

    $scope.saveEditedPlace = function (placeE) {
        console.log(placeE);

        var obj = {
            Id: placeE.Id,
            PlaceName: placeE.PlaceName,
            Longitude: placeE.Longitude,
            Latitude: placeE.Latitude,
            Address: placeE.Address,
            Discription: placeE.Discription,
            QFacts: placeE.QuickFacts,
            TownName: placeE.TownName,
        };
        console.log(obj);
        dataPlace.editPlace(obj)
            .then(function (response) {
                getAllPlaces();
                //console.log(response);
            }, function (response) {
                //console.log(response);
            });

    };

    $scope.deletePlace = function (Id) {

        var obj = {Id : Id};

        dataPlace.deletePlace(obj).then(
            function (response) {
                getAllPlaces();
            }, function (response) {

            });
    }

    getAllPlaces();

});