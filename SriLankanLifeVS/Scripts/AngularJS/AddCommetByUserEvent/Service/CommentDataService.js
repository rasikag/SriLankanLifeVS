'use strict'

srilankanlife.factory('CommentDataService', function ($http) {


    
    return {
        SubmitComment: function (event) {

            
          return  $http({
                method: 'POST',
                data : event,
                url: 'http://localhost:58115/api/submit-comment-for-event'
            });
        }
    }
});