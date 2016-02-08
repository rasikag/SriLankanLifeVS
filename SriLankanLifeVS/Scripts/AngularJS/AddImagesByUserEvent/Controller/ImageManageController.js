'use strict'

srilankanlife.controller('AddImageController',
        ['$scope', 'Upload', '$http', '$timeout', function ($scope, Upload, $http, $timeout) {

            $scope.ImageSubmitSuccess = false;
            $scope.ImageSubmitFail = false;

            $scope.submitImage = function (img, file, imageForm) {

                $scope.upload(file, img);


            };

            // upload on file select or drop
            $scope.upload = function (file, img) {
                Upload.upload({
                    url: "http://localhost:58115/api/add-event-image-by-user",
                    data: { file: file }
                }).then(function (resp) {
                    console.log(resp);
                    $http({
                        method: 'POST',
                        url: 'http://localhost:58115/api/update-user-data-event',
                        data: {
                            'Name': img.Name,
                            'Email': img.Email,
                            'Id': resp.data
                        }
                    }).then(function () {
                        $scope.ImageSubmitSuccess = true;
                        $scope.img = "";
                        $timeout(function () {
                            $scope.ImageSubmitSuccess = false;
                        }, 3000);
                    }, function () {
                        $scope.ImageSubmitFail = true;
                        $scope.img = "";
                        $timeout(function () {
                            $scope.ImageSubmitFail = false;
                        }, 3000);
                    });


                }, function (resp) {
                    console.log('Error status: ' + resp);
                });
            };





        }]);