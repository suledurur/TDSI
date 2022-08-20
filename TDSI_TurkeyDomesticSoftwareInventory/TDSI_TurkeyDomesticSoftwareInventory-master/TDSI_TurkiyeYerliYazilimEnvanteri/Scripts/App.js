var checksoftwareAddApp = angular.module('checksoftwareAddApp', []);

checkSoftwareAddApp.controller('softwareController', function ($scope, $http) {
    $http.get({
        method: "Get",
        url: "/UserController/GetSoftwares"
    }).then(function mySuccess(response) {
        $scope.softwares = response.data;
    }, function myError(response) {
        $scope.softwares = response.statusText;
    });

    //$http.get('/User/GetSoftwares')
    //    .success(function (result) {
    //        $scope.softwares = result;
    //    })
    //    .error(function (data) {
    //        console.log(data);
    //    });
});