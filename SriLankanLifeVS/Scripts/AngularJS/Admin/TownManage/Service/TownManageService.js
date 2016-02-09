'use strict';

srilankanlife.factory('dataTown', function ($http) {

    return {

        getDistrictByName: function (dist) {
            return $http({
                url: 'http://localhost:58115/api/get-by-name',
                method: 'GET',
                params: {
                    Name: dist
                }
            });

        },

        addTown: function (town) {
            return $http({
                url: 'http://localhost:58115/api/add-town',
                method: 'POST',
                data: town
            });
        },

        getAllTown: function () {
            return $http({
                url: 'http://localhost:58115/api/get-all',
                method : 'GET'
            });
        },

        editTown: function (obj) {
            return $http({
                url: 'http://localhost:58115/api/edit-town',
                method: 'POST',
                data : obj
            });
        },

        deleteTown: function (Id) {
            return $http({
                url: 'http://localhost:58115/api/delete-town',
                method: 'POST',
                data : Id
            });
        }

    }

});