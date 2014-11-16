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

    $scope.onEnd = function () {
        setTimeout(function () {
            callback();
            alert('all done');
        }, 1);
    };

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
                scope.$eval(attrs.repeatEnd);
            }
        }
    };
});

function callback() {
    console.log('hello');
    $('h3.ng-binding').on('click', function () {
        var $article = $(this).parent().parent().parent();
        var content = '<h2>' + $article.attr('data-title') + '</h2>'
        + '<p>' + $article.attr('data-content') + '</p>'
        $("#myModalContent").html(content);
        $("#myModal").modal('show');
    });
}
