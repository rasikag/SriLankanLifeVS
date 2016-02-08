'use strict'

srilankanlife.controller('districtController',
        function districtController($scope, districtData , $timeout) {

            $scope.districtStatusTrue = false;

            $scope.districtStatusFalse = false;

            var getAllDistrict = function () {
                districtData.getAllDistrictService()
                    .then(function (resp) {
                        //console.log(resp.data)
                        $scope.allDistrict = resp.data;
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
                var a = "#"+dist.DistrictName;
                console.log(a);
                var myEl = document.getElementById(a);
                console.log(myEl);
            }
                
            getAllDistrict();

        });