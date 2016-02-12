'use strict'

srilankanlife.factory('placeCategoryData', function ($http) {

    return {
        addPlaceCategory : function (cat) {

           return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/add-event-categoty',
                data: cat
            });
        },
        getAllPlaceCategories : function () {
            return $http(
                {
                    method: 'GET',
                    url: 'http://localhost:58115/api/get-all-event-category'
                });
        },

        editCategory: function (cat) {
            return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/edit-event-category',
                data: cat
            });
        },
        deleteCategory: function (cat) {

            return $http({
                method: 'POST',
                url: 'http://localhost:58115/api/delete-event-category',
                data: cat
            });

        }
    }
});