var newsApp = angular.module("newsApp", [])

newsApp.factory('newsFactory', function ($http) {
    return {
        getFormData: function (callback) {
            $http.get('/api/news').success(callback);
        }
    }
})

newsApp.controller("newsController", function ($scope, newsFactory) {
    getFormData();
    function getFormData() {
        newsFactory.getFormData(function (results) {
            $scope.news = results.news;
        })
    }
    $scope.Save = function () {
        $scope.message = "News Data Submitted"
    }
})