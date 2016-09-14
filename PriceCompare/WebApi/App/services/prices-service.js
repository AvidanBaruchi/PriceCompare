(function(angular) {
    "use-strict";

    angular.module("PricesApp")
        .service("pricesService", PricesService);

    function PricesService(baseUrl, $http, $q) {
        this.$http = $http;
        this.$q = $q;

        this.apiUrl = baseUrl + '/api';
    }

    PricesService.prototype.getStores = function getStores() {
        return this.$http.get(this.apiUrl + '/stores');
    }
})(angular);