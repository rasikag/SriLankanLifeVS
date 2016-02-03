'use strict'

srilankanlife.controller('AddCommentController',
        function AddCommentController($scope, CommentDataService , $timeout) {
            $scope.commnetSubmit = false;
            $scope.submitComment = function (comment, commentForm) {

                if (commentForm.$valid) {
                    //commentData.postComment(comment);

                    commentData.save(comment)
                        .$promise.then(function (response) {
                            $scope.commnetSubmit = true;
                            $scope.comment = "";
                            $timeout(function () {
                                $scope.commnetSubmit = false;
                            }, 3000);
                        }, function (response) {
                            console.log('faild', response);
                        })
                }
            };
        });