angular.module('tomou').factory('homeServices', ['$http',
    function ($http) {
        var service = {};
        service.ObtenerRegiones = function (callback) {
            $http.get(location.origin  + '/api/catalogos/ObtenerRegiones').then(
                function (response) {
                    callback(response.data);
                }, function (response) {
                    response = {
                        Message: "Error al consumir el API: " + response.statusText + "(" + response.status + ")",
                        Success: false
                    }
                    callback(response);
                });
        };

        service.EditarUsuario = function (data, callback) {
            $http.post(location.origin + '/api/usuarios/EditarUsuario',
                data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {
                    callback(response.data);
                }, function (response) {
                    response = {
                        Message: "Error al consumir el API: " + response.statusText + "(" + response.status + ")",
                        Success: false
                    }
                    callback(response);
                });
        };
        service.AgregarRegistro = function (data, callback) {
            $http.post(location.origin + '/api/registro/AgregarRegistro',
                data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {
                    callback(response.data);
                }, function (response) {
                    response = {
                        Message: "Error al consumir el API: " + response.statusText + "(" + response.status + ")",
                        Success: false
                    }
                    callback(response);
                });
        };
        service.EnviarMensaje = function (data, callback) {
            $http.post(location.origin + '/api/registro/EnviarMensaje',
                data, { headers: { 'Content-Type': 'application/json' } }).then(
                function (response) {
                    callback(response.data);
                }, function (response) {
                    response = {
                        Message: "Error al consumir el API: " + response.statusText + "(" + response.status + ")",
                        Success: false
                    }
                    callback(response);
                });
        };
        return service;
    }]);