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

    $scope.appliedClass = function (myObjProperty) {
        if (myObjProperty == 0) {
            return "Economy";
        }
        else if (myObjProperty == 1) {
            return "Politic";
        }
        else if (myObjProperty == 2) {
            return "People";
        }
        else if (myObjProperty == 3) {
            return "Sport";
        }
    }

})


newsApp.directive("repeatEnd", function () {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            if (scope.$last) {
                callback()
            }
        }
    };
});

function callback() {
    $('h3.ng-binding').on('click', function () {
        var $article = $(this).parent().parent().parent();

        var content = $article.attr('data-content');
        $("#myModalLabel").html($article.attr('data-title'));
        $("#myModalContent").html(content);
        $("#myModal").modal('show');
    });
}