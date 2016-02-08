'use strict'

srilankanlife.factory('VideoDataService', function ($http) {


    
    return {
        SubmitVideo: function (event) {

            
          return  $http({
                method: 'POST',
                data : event,
                url: 'http://localhost:58115/api/add-event-video-by-user'
            });
        }
    }
});