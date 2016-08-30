angular.module('App', [])
    .controller('DirectoryCtrl', function ($scope, $http, $q) {

        server = '';
        $scope.currentDirectory = server;
        directoryUrl = "/api/directory";
        fileCountUrl = "api/directory/filecount";

        var isLoadFile;
        var cancel;

        $http.get(directoryUrl).success(function (data) {
            getDirectoryInfo(data);
            this.getFilesCount();
        });

        getDirectoryInfo = function (data) {
            $scope.directories = data.Directories;
            $scope.files = data.Files;
            $scope.parent = data.Parent;
        }

        getFileCount = function (data) {
            $scope.LessTenMb = data.LessTenMb;
            $scope.BetweenTenAndFiftyMb = data.BetweenTenAndFiftyMb;
            $scope.MoreOnehundredMb = data.MoreOnehundredMbMoreOnehundredMb;
        }

        fileCountLoading = function () {
            $scope.LessTenMb = "Loading...";
            $scope.BetweenTenAndFiftyMb = "Loading...";
            $scope.MoreOnehundredMb = "Loading...";
        }

        cancelRequst = function () {
            fileCountLoading();

            if (isLoadFile) {
                cancel.resolve(this.response);
            }
        }

        getFilesCountOfCurrentDirectory = function () {
            cancelRequst();

            cancel = $q.defer();
            isLoadFile = true;

            cancel.then($http({
                url: fileCountUrl,
                method: "Get",
                params: { path: $scope.currentDirectory },
                timeout: cancel.promise
            }).success(function (data) {
                getFileCount(data)
                isLoadFile = false;
            }));
        }

        getFilesCount = function () {
            cancelRequst();

            cancel = $q.defer();
            isLoadFile = true;

            cancel.then($http({
                url: fileCountUrl,
                method: "Get",
                timeout: cancel.promise
            }).success(function (data) {
                getFileCount(data)
                isLoadFile = false;
            }));
        }

        $scope.showDirectory = function (directory) {
            if ($scope.currentDirectory == server) {
                $scope.currentDirectory = directory;
            }
            else $scope.currentDirectory += directory + "\\";
            $http({
                url: directoryUrl,
                method: "GET",
                params: { path: $scope.currentDirectory }
            }).success(function (data) {
                getDirectoryInfo(data);
                this.getFilesCountOfCurrentDirectory();
            });
        }

        $scope.back = function () {
            if ($scope.parent == server) {
                $scope.currentDirectory = server;
                $http.get(directoryUrl).success(function (data) {
                    getDirectoryInfo(data);
                    this.getFilesCount();
                });
            }
            else {
                $scope.currentDirectory = $scope.parent + "\\";
                $http({
                    url: directoryUrl,
                    method: "GET",
                    params: { path: $scope.parent }
                }).success(function (data) {
                    getDirectoryInfo(data);
                    this.getFilesCountOfCurrentDirectory();
                });
            }
        }
    });
