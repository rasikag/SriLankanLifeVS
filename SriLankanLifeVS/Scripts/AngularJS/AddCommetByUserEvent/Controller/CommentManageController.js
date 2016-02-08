'use strict'

srilankanlife.controller('AddCommentController',
        function AddCommentController($scope, CommentDataService, $timeout) {
            $scope.commnetSubmitSuccess = false;
            $scope.commnetSubmitFail = false;

            $scope.submitComment = function (comment, commentForm) {

                if (commentForm.$valid) {

                    //console.log(comment);

                    CommentDataService.SubmitComment(comment)
                        .then(function successCallback(data) {
                            $scope.commnetSubmitSuccess = true;
                            $scope.comment = "";
                            $timeout(function () {
                                $scope.commnetSubmitSuccess = false;
                            }, 3000);
                        }, function errorCallback(data) {
                            $scope.commnetSubmitFail = true;
                            $timeout(function () {
                                $scope.commnetSubmitFail = false;
                            }, 3000);
                        });

                }
            };
        });