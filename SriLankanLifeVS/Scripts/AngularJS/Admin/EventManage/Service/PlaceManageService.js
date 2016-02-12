'use strict';

srilankanlife.factory('dataPlace', function ($http) {

    return {

        getTownByName: function (twn) {
            console.log(twn);
            return $http({
                url: 'http://localhost:58115/api/get-town-by-name-event',
                method: 'GET',
                params: {
                    Name: twn
                }
            });

        },

        addPlace: function (place) {
            return $http({
                url: 'http://localhost:58115/api/add-event',
                method: 'POST',
                data: place
            });
        },

        getAllPlaces: function () {
            return $http({
                url: 'http://localhost:58115/api/get-all-evnts',
                method : 'GET'
            });
        },

        editPlace: function (obj) {
            
            return $http({
                url: 'http://localhost:58115/api/edit-event',
                method: 'POST',
                data : obj
            });
        },

        deletePlace: function (Id) {
            return $http({
                url: 'http://localhost:58115/api/delete-event',
                method: 'POST',
                data : Id
            });
        }

    }

});