'use strict'

srilankanlife.controller('AddVideoController',
        function AddCommentController($scope, VideoDataService, $timeout) {
            $scope.videoSubmitSuccess = false;
            $scope.videoSubmitFail = false;

            $scope.submitVideo = function (video, videoForm) {

                if (videoForm.$valid) {

                    //console.log(comment);

                    VideoDataService.SubmitVideo(video)
                        .then(function successCallback(data) {
                            $scope.videoSubmitSuccess = true;
                            $scope.video = "";
                            $timeout(function () {
                                $scope.videoSubmitSuccess = false;
                            }, 3000);
                        }, function errorCallback(data) {
                            $scope.videoSubmitFail = true;
                            $timeout(function () {
                                $scope.videoSubmitFail = false;
                            }, 3000);
                        });

                }
            };
        });