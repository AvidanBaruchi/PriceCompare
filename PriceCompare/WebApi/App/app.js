(function (angular) {
    "use-strict";

    var app = angular.module("PricesApp",
    [
        "ngAnimate",
        "ui.router",
        "ui.bootstrap"
    ]);

    app.constant('baseUrl', 'http://localhost:49399');

    app.config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider.state('main',
        {
            template: '<div>I am Template</div>',
            controller: "MainController as vm",
            url: '/main'
        });

        $urlRouterProvider.otherwise('/main');
    });
})(angular);