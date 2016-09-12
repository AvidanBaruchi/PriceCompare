(function (angualar) {
    var app = angular.module("PricesApp",
    [
        "ngAnimate",
        "ui.router",
        "ui.bootstrap"
    ]);

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