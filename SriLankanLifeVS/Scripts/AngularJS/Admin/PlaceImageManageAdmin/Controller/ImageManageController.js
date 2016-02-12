'use strict'

srilankanlife.controller('PlaceImageAdminController',
        ['$scope', 'Upload', '$http', '$timeout',function ($scope, Upload, $http, $timeout) {

            $scope.ImageSubmitSuccess = false;
            $scope.ImageSubmitFail = false;

            $scope.submitImage = function (file) {
                $scope.upload(file);
            };

            $scope.getPlaceByName = function (twn) {
                return $http({
                    url: 'http://localhost:58115/api/get-places-by-name-admin-image',
                    method: 'GET',
                    params: {
                        Name: twn
                    }
                }).then(function (response) {
                         console.log(response);
                         return response.data.map(function (item) {
                             return item.PlaceName;
                         });
                     });
            };


            // upload on file select or drop
            $scope.upload = function (file) {
                Upload.upload({
                    url: "http://localhost:58115/api/add-place-image-by-admin",
                    data: { file: file, PlaceName: $scope.PlaceName }

                }).then(function (resp) {
                    activate();
                    $scope.ImageSubmitSuccess = true;
                    $scope.PlaceName = "";
                    $timeout(function () {

                        $scope.ImageSubmitSuccess = false;
                    }, 3000);


                }, function (resp) {
                    $scope.ImageSubmitFail = true;
                    $scope.PlaceName = "";
                    $timeout(function () {

                        $scope.ImageSubmitFail = false;
                    }, 3000);
                });
            };

            function activate() {

                $http(
                    {
                        method: 'GET',
                        url: 'http://localhost:58115/api/get-all-images-admin-palce'
                    }
                    ).then(function (resp) {
                        console.log(resp);
                        $scope.photos = resp.data;
                    }, function (resp) {
                        console.log(resp);
                    });

            };


            $scope.remove = function (photo) {
                $http(
                {
                    method: 'POST',
                    url: 'http://localhost:58115/api/delete-place-image-admin',
                    data: {
                        "fileName": photo.ImageName,
                        "Id": photo.Id
                    }
                }
                ).then(function () {
                    activate();
                }, function () {
                    activate();
                });
            };

            $scope.changeActiveImage = function changeActiveImage(photo) {

                $http({
                    url: 'http://localhost:58115/api/change-active-image',
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    data: {
                        Id: photo.Id,
                        Active: photo.Active
                    }
                }).then(function () {
                    activate();
                }, function () {
                    activate();
                });

            }

            activate();
        }]);