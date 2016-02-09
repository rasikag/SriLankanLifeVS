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
        }

    }

});