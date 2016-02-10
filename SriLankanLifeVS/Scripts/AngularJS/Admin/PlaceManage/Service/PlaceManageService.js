'use strict';

srilankanlife.factory('dataPlace', function ($http) {

    return {

        getTownByName: function (twn) {
            console.log(twn);
            return $http({
                url: 'http://localhost:58115/api/get-town-by-name-place',
                method: 'GET',
                params: {
                    Name: twn
                }
            });

        },

        addPlace: function (place) {
            return $http({
                url: 'http://localhost:58115/api/add-place',
                method: 'POST',
                data: place
            });
        },

        getAllPlaces: function () {
            return $http({
                url: 'http://localhost:58115/api/get-all-places',
                method : 'GET'
            });
        },

        //editTown: function (obj) {
        //    return $http({
        //        url: 'http://localhost:58115/api/edit-town',
        //        method: 'POST',
        //        data : obj
        //    });
        //},

        //deleteTown: function (Id) {
        //    return $http({
        //        url: 'http://localhost:58115/api/delete-town',
        //        method: 'POST',
        //        data : Id
        //    });
        //}

    }

});