'use strict'

travelSite.factory('CommentDataService', function ($http) {


    
    return {
        SubmitComment : function (event) {
            $http({
                method: 'POST',
                url: 'http://localhost:58115/api/'
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });
        }
    }
});