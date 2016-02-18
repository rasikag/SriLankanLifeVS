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
                method: 'GET'
            });
        },

        editPlace: function (obj) {

            return $http({
                url: 'http://localhost:58115/api/edit-place',
                method: 'POST',
                data: obj
            });
        },

        deletePlace: function (Id) {
            return $http({
                url: 'http://localhost:58115/api/delete-place',
                method: 'POST',
                data: Id
            });
        },


        getPlaceCategories: function (cat) {
            return $http({
                url: 'http://localhost:58115/api/get-place-categories-in-place',
                method: 'GET',
                params: {
                    Name: cat
                }
            });
        }

    }

});