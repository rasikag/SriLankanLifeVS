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
        }
    }
});