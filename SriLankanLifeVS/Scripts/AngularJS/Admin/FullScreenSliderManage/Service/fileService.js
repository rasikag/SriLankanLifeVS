(function () {

    "use strict";

    // Service to do CRUD operations on photos
    srilankanlife.service("fileService", [
        "$http", "$q", "apiUrl", function ($http, $q, apiUrl) {

            //Get all photos saved on the server  
            function getAll() {

                var deferred = $q.defer();

                $http.get(apiUrl)
                  .success(function (result) {
                      //console.log(result);
                      deferred.resolve(result);
                  })
                  .error(function (error) {
                      deferred.reject(error);
                  });

                return deferred.promise;
            }

            //Get photo from server with given file name        
            function getPhoto(fileName) {

                var deferred = $q.defer();

                $http.get(apiUrl + fileName)
                  .success(function (result) {
                      deferred.resolve(result);
                  })
                  .error(function (error) {
                      deferred.reject(error);
                  });

                return deferred.promise;
            }

            // Delete photo on the server with given file name      
            function deletePhoto(fileName , Id) {

                var deferred = $q.defer();

                $http.delete(apiUrl, { params: { fileName: fileName , Id : Id  } })
                  .success(function (result) {
                      deferred.resolve(result);
                  }).error(function (error) {
                      deferred.reject(error);
                  });

                return deferred.promise;
            }

            function changeActiveImage(Id, Active) {

                //var deferred = $q.defer();

                $http.post("http://localhost:58115/api/change-active-image", { params: { Id: Id, Active: Active } })
                  .success(function (result) {
                      console.log(result);
                      //deferred.resolve(result);
                  }).error(function (error) {
                      console.log(error);
                      //deferred.reject(error);
                  });

                //return deferred.promise;
            }

            return {
                getAll: getAll,
                getPhoto: getPhoto,
                deletePhoto: deletePhoto,
                changeActiveImage: changeActiveImage
            };
        }
    ]);


})();