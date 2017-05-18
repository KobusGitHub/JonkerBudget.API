(function () {
    'use strict';

    angular.module('app', [
        // Angular modules 
        'ngRoute',        
        // Custom modules 

        // 3rd Party Modules
        'LocalStorageModule'
    ]);

    var app = angular.module('app');

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });
})();