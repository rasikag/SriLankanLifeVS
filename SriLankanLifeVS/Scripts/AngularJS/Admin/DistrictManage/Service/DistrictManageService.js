'use strict'

srilankanlife.factory('districtData', function ($http) {

    return {
        addDistrictService: function (district) {

           return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/add-district',
                data: district
            });
        },
        getAllDistrictService: function () {
            return $http(
                {
                    method: 'GET',
                    url: 'http://localhost:58115/api/get-all-district'
                });
        },

        editDistrict: function (dist) {
            return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/edit-district',
                data: dist
            });
        },
        deleteDistrict: function (dist) {

            return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/delete-district',
                data: dist
            });

        }
    }
});