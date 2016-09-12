(function (angular) {
    angular.module("PricesApp")
        .controller("MainController", MainController);

    function MainController() {
        this.data = "tada";
    }

})(angular);