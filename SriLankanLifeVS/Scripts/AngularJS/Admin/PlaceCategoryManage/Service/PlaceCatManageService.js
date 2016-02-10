'use strict'

srilankanlife.factory('placeCategoryData', function ($http) {

    return {
        addPlaceCategory : function (cat) {

           return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/add-place-categoty',
                data: cat
            });
        },
        getAllPlaceCategories : function () {
            return $http(
                {
                    method: 'GET',
                    url: 'http://localhost:58115/api/get-all-place-category'
                });
        },

        editCategory: function (cat) {
            return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/edit-place-category',
                data: cat
            });
        },
        deleteCategory: function (cat) {

            return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/delete-place-category',
                data: cat
            });

        }
    }
});