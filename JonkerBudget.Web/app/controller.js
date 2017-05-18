(function () {
    'use strict';

    angular
        .module('app')
        .controller('Ctrl', Ctrl);

    Ctrl.$inject = ['$location', '$scope', 'authService', 'dataService'];

    function Ctrl($location, $scope, authService, dataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'controller';
        vm.data = [];

        $scope.login = function () {
            authService.login($scope.username, $scope.password).then(function (response) {
                alert('success');
            },function (err) {
                alert('error!');
          });
        };

        $scope.logout = function () {
            authService.logout().then(function (response) {                
            }, function (err) {                
            });
        }

        $scope.getData = function () {
            dataService.getProducts().then(function (response) {
                debugger;
                vm.data = angular.copy(response);
            }, function (err) {
                alert('error getting data!');
            });
        };

        activate();

        function activate() { }
    }
})();
