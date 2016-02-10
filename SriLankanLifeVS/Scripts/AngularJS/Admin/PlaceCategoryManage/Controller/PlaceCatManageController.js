'use strict'

srilankanlife.controller('categoryController',
        function categoryController($scope, placeCategoryData, $timeout) {

            $scope.catStatusTrue = false;

            $scope.catStatusFalse = false;

            var getAllCat = function () {
                placeCategoryData.getAllPlaceCategories()
                    .then(function (resp) {
                        var newArray = new Array();
                        for (var i = 0 ; i < resp.data.length ; i++) {
                            var obj = {
                                CategoryName: resp.data[i].CategoryName,
                                Id: resp.data[i].Id,
                                EditButtonId: "E" + resp.data[i].Id,
                                SaveButtonId: "S" + resp.data[i].Id,
                                TextId: "T" + resp.data[i].Id,
                                InputId: "I" + resp.data[i].Id
                            };
                            newArray.push(obj);
                        }
                        $scope.allCat = newArray;
                    });
            };

            $scope.addCategory = function (cat, catForm) {
                placeCategoryData.addPlaceCategory(cat)
                    .then(
                    function (data) {
                        getAllCat();
                        $scope.catStatusTrue = true;
                        $scope.cat = "";
                        $timeout(function () {

                            $scope.catStatusTrue = false;
                        }, 3000);
                    },
                    function (data) {
                        $scope.catStatusFalse = true;
                        $timeout(function () {
                            $scope.catStatusFalse = false;
                        }, 3000);
                    });
            };

            $scope.editCat = function (dist) {

                var a = "#" + dist.InputId;
                var b = "#" + dist.TextId;
                var c = "#" + dist.SaveButtonId;
                var d = "#" + dist.EditButtonId;

                $(b).addClass("ng-hide");
                $(a).removeClass("ng-hide");
                $(c).removeClass("ng-hide");
                $(d).addClass("ng-hide");
            };

            $scope.saveEditedCat = function (cat) {
                console.log(cat);
                var obj = {
                    Id: cat.Id,
                    CategoryName: cat.CategoryName
                };

                placeCategoryData.editCategory(obj).then(
                    function (data) {
                        getAllCat();
                        //console.log(dist)
                        $timeout(function () {
                        }, 3000);
                    },
                    function (data) {
                        //$scope.districtStatusFalse = true;
                        $timeout(function () {
                        }, 3000);
                    });

            };

            $scope.deleteCat = function (dist) {
                var obj = {
                    Id: dist.Id,
                    CategoryName: dist.CategoryName
                };


                placeCategoryData.deleteCategory(obj).then(
                    function (data) {
                        getAllCat();
                        $timeout(function () {
                        }, 3000);
                    },
                    function (data) {
                        //$scope.districtStatusFalse = true;
                        $timeout(function () {
                        }, 3000);
                    });
            };

            getAllCat();

        });