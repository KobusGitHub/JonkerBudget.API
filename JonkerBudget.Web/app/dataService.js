(function () {
    'use strict';

    angular
        .module('app')
        .factory('dataService', dataService);

    dataService.$inject = ['$http','$q'];

    function dataService($http, $q) {

        var address = "http://localhost:6567/";

        var service = {
            getProducts: getProducts
        };

        return service;

        function getProducts() {
            var deferred = $q.defer();

            $http.get(address + 'api/Products').success(function (response) {                

                deferred.resolve(response);

            }).error(function (err, status) {
                deferred.reject(err);
            });

            return deferred.promise;
        };
    }
})();