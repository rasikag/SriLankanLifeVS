(function () {

    "use strict";

    srilankanlife.controller("imageUploadController", ["$window",
        "fileService", "Upload", "apiUrl","$http", function ($window, fileService, Upload, apiUrl ,$http) {

            var vm = this;

            //Variables
            vm.photos = [];
            vm.files = [];
            vm.previewPhoto = {};
            vm.spinner = {
                active: true
            };

            //Functions
            function setPreviewPhoto(photo) {
                vm.previewPhoto = photo;
            }

            function activate() {
                vm.spinner.active = true;
                fileService.getAll()
                  .then(function (data) {
                      vm.photos = data.Photos;
                      vm.spinner.active = false;
                      setPreviewPhoto();
                  }, function (err) {
                      console.log("Error status: " + err.message);
                      vm.spinner.active = false;
                  });
            }

            function uploadFiles(files) {
                Upload.upload({
                    url: 'http://localhost:58115/api/add-image-to-main-slider',
                    data: { file: files }
                })
                  .then(function (response) {
                      console.log(response);
                      activate();
                      setPreviewPhoto();
                      uploadFiles();
                  }, function (err) {
                      console.log("Error status: " + err.message);
                      vm.spinner.active = false;
                  });

                //$window.location.reload();
            }

            function removePhoto(photo) {
                fileService.deletePhoto(photo.ImageName, photo.Id)
                  .then(function () {
                      activate();

                      setPreviewPhoto();
                  }, function () {
                      activate();
                  });
            }

            function changeActiveImage(photo) {

                //fileService.changeActiveImage(photo.Id, photo.Active);
                //.then(function () {
                //    activate();

                //    setPreviewPhoto();
                //}, function () {
                //    activate();
                //});
                //?Id=' + photo.Id+'&'+'Active='+photo.Active
                $http({
                    url: 'http://localhost:58115/api/changeactiveimage',
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
                    setPreviewPhoto();
                }, function () {
                    activate();
                    setPreviewPhoto();
                });

            }

            //Set scope 
            activate();
            vm.uploadFiles = uploadFiles;
            vm.remove = removePhoto;
            vm.setPreviewPhoto = setPreviewPhoto;
            vm.changeActiveImage = changeActiveImage;
        }
    ]);
})();