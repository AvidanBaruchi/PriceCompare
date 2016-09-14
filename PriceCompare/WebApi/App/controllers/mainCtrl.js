(function (angular) {
    "use-strict";

    angular.module("PricesApp")
        .controller("MainController", MainController);

    function MainController(pricesService) {
        this.pricesService = pricesService;

        this.data = "tada";

        this.init();
    }

    MainController.prototype.init = function init() {
        //this.pricesService.getStores()
        //    .then(function(response) {
        //            debugger;
        //        },
        //        function(error) {
        //              debugger;
        //        });
    }

})(angular);