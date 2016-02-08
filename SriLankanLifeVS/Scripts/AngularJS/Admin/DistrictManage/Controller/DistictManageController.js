'use strict'

srilankanlife.controller('districtController',
        function districtController($scope, districtData, $timeout) {

            $scope.districtStatusTrue = false;

            $scope.districtStatusFalse = false;

            var getAllDistrict = function () {
                districtData.getAllDistrictService()
                    .then(function (resp) {
                        var newArray = new Array();
                        for (var i = 0 ; i < resp.data.length ; i++) {
                            var obj = {
                                DistrictName: resp.data[i].DistrictName,
                                Id: resp.data[i].Id,
                                EditButtonId: "EditButton" + resp.data[i].Id,
                                SaveButtonId: "SaveButton" + resp.data[i].Id,
                                TextId: "Div" + resp.data[i].Id,
                                InputId: "Input" + resp.data[i].Id
                            };
                            newArray.push(obj);
                        }

                        $scope.allDistrict = newArray;
                    });
            };

            $scope.addDistrict = function (district, districtForm) {
                districtData.addDistrictService(district)
                    .then(
                    function (data) {
                        getAllDistrict();
                        $scope.districtStatusTrue = true;
                        $scope.district = "";
                        $timeout(function () {

                            $scope.districtStatusTrue = false;
                        }, 3000);
                    },
                    function (data) {
                        $scope.districtStatusFalse = true;
                        $timeout(function () {
                            $scope.districtStatusFalse = false;
                        }, 3000);
                    });
            };

            $scope.editDistrict = function (dist) {

                var a = "#" + dist.InputId;
                var b = "#" + dist.TextId;
                var c = "#" + dist.SaveButtonId;
                var d = "#" + dist.EditButtonId;

                $(b).addClass("ng-hide");
                $(a).removeClass("ng-hide");
                $(c).removeClass("ng-hide");
                $(d).addClass("ng-hide");
            };

            $scope.saveEditedDistr = function (dist) {
                var obj = {
                    Id: dist.Id,
                    DistrictName: dist.DistrictName
                };

                districtData.editDistrict(obj).then(
                    function (data) {
                        getAllDistrict();
                        console.log(dist)
                        $timeout(function () {
                        }, 3000);
                    },
                    function (data) {
                        $scope.districtStatusFalse = true;
                        $timeout(function () {
                        }, 3000);
                    });

            };

            $scope.deleteDistrict = function (dist) {
                var obj = {
                    Id: dist.Id,
                    DistrictName: dist.DistrictName
                };


                districtData.deleteDistrict(obj).then(
                    function (data) {
                        getAllDistrict();
                        $timeout(function () {
                        }, 3000);
                    },
                    function (data) {
                        $scope.districtStatusFalse = true;
                        $timeout(function () {
                        }, 3000);
                    });
            };

            getAllDistrict();

        });