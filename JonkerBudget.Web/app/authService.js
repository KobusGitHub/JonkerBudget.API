(function () {
    'use strict';

    angular
        .module('app')
        .service('authService', authService);

    authService.$inject = ['$http', '$q', 'localStorageService'];

    function authService($http, $q, localStorageService) {

        var address = "http://localhost:6567/";      
        var service = {};

        var logout = function () {
            logOut();
        };

        var login = function (username, password) {

            logOut();

            var data = "grant_type=password&username=" + username + "&password=" + password;
            var deferred = $q.defer();

            $http.post(address + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                
                var authResult = {
                   expires: response.expires,
                   issued: response.issued,
                   access_token: response.access_token,
                   expires_in: response.expires_in,
                   roles: response.roles,
                   token_type: response.token_type,
                   username: response.username                
                };
                
                localStorageService.set('authorizationData', authResult);
                
                deferred.resolve(response);

            }).error(function (err, status) {                
                deferred.reject(err);
            });

            return deferred.promise;
        };

        var logOut = function () {

            localStorageService.remove('authorizationData');
        };

        var fillAuthData = function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                authentication.isAuth = true;
                authentication.userName = authData.userName;
                authentication.useRefreshTokens = authData.useRefreshTokens;
                authentication.navigation = authData.navigation;
            }
        };

        service.login = login;
        service.logout = logout;

        return service;
    }
})();