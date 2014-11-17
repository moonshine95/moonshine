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
        else if (myObjProperty == 4) {
            return "Culture";
        }
        else if (myObjProperty == 5) {
            return "Sciences";
        }
        else if (myObjProperty == 6) {
            return "Technologies";
        }
        else if (myObjProperty == 7) {
            return "World";
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
        console.log(content);

        // get couleur
        $("#myModalLabel").html($article.attr('data-title'));
        $("#myModalContent").append(content)
        $('#image').width($('#image').height()).attr('href', $article.attr('data-Image'));
        $('#myModalContent>h1').removeAttr('style');
        $('#myModalContent>h2').removeAttr('style');
        $('#myModalContent>p').removeAttr('style');
        $('aside').replaceWith($('<p>' + this.innerHTML + '</p>'));
        $('#myModalContent>h2').replaceWith($('<h3>' + this.innerHTML + '</h3>'));
        $('a#myModalContent>h3').replaceWith($('<h3>' + this.innerHTML + '</h3>'));
        $('a#myModalContent>h4').replaceWith($('<h3>' + this.innerHTML + '</h3>'));



        $('#description').html($article.attr('data-description'));
        $("#myModal").modal('show');
        setTimeout(function () {
            $('.modal-content').height($('#myModalContent').height() + 100)
        }, 500);
    });
}