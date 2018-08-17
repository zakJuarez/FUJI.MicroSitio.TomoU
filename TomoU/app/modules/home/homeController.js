angular.module('tomou').controller('homeController', ['$scope', '$rootScope', '$mdDialog', '$location', 'homeServices',
    function ($scope, $rootScope, $mdDialog, $location, homeServices) {
        $scope.loading = false;
        $scope.currentNavItem = 'page1';
        $scope.Pagina = 2;
        $scope.regiones_origen = [];
        $scope.registro = null;
        $scope.contacto = null;
        function load() {
            $scope.regiones_origen = [];
            $scope.loading = true;
            homeServices.ObtenerRegiones(function (response) {
                $scope.loading = false;
                $scope.regiones_origen = [{ intEstadoID: 0, vchEstado: 'EXTRANJERO' }];
                for (i = 0; i < response.length; i++)
                    $scope.regiones_origen.push(response[i]);
            });
            $scope.registro = null;
            $scope.contacto = null;
            $scope.loading = false;
        }
        load();
        
        $scope.AgregarRegistro = function (registro, ev) {
            $scope.loading = true;
            var Request = {
                Registro: registro
            };
            homeServices.AgregarRegistro(Request, function (response) {
                $scope.loading = false;
                if (response.Success) {
                    var alert = $mdDialog.alert({
                        title: 'Correcto',
                        textContent: response.Message,
                        ok: 'Aceptar',
                        skipHide: true
                    });
                    $mdDialog.show(alert).then(function () {
                        load();
                    });
                }
                else {
                    var alert = $mdDialog.alert({
                        title: 'Advertencia',
                        textContent: response.Message,
                        ok: 'Aceptar',
                        skipHide: true
                    });
                    $mdDialog.show(alert);
                }
            });
        };

        $scope.EnviarMensaje = function (contacto, ev) {
            $scope.loading = true;
            var Request = {
                Nombre: contacto.Nombre,
                Correo: contacto.Correo,
                Mensaje: contacto.Mensaje
            };
            homeServices.EnviarMensaje(Request, function (response) {
                $scope.loading = false;
                if (response.Success) {
                    var alert = $mdDialog.alert({
                        title: 'Correcto',
                        textContent: response.Message,
                        ok: 'Aceptar',
                        skipHide: true
                    });
                    $mdDialog.show(alert).then(function () {
                        load();
                    });
                }
                else {
                    var alert = $mdDialog.alert({
                        title: 'Advertencia',
                        textContent: response.Message,
                        ok: 'Aceptar',
                        skipHide: true
                    });
                    $mdDialog.show(alert);
                }
            });
        };

        showConfirm = function (ev) {
            // Appending dialog to document.body to cover sidenav in docs app
            var confirm = $mdDialog.confirm()
                .title('Aviso importante')
                .textContent('La información contenida en la página que usted está por ingresar, de FUJIFILM DE MÉXICO S.A. DE C.V., es exclusiva para los profesionales de la salud de conformidad con los Artículos 79° y 310° de la Ley General de Salud y de más a fines, por lo cual está estrictamente prohibido el acceso o intento de acceso no autorizado a toda persona que no se encuentre bajo dicho supuesto.')
                .ariaLabel('Acceso a profesionales de la salud.')
                .targetEvent(ev)
                .ok('Sí soy profesional de la salud')
                .cancel('No soy profesional de la salud');

            $mdDialog.show(confirm).then(function () {
                //$scope.status = 'Eres profesional de la salud, permaneces en la página.';
            }, function () {
                //$scope.status = 'Salir, se debe redirigir a la pagina anterior.';
                window.location.href = "http://mastologia.org.mx/index.html";
            });
        };

        showConfirm(this);

        $scope.openMenu = function ($mdOpenMenu, ev) {
            $mdOpenMenu(ev);
        };
        $scope.EditarUsuario = function (ev) {
            $mdDialog.show({
                controller: 'editarUsuarioController',
                templateUrl: 'app/modules/home/user/update/editarUsuarioView.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: true,
                skipHide: true
            }).then(function (response) {   
                var alert = $mdDialog.alert({
                    title: 'Mensaje del sistema',
                    textContent: response.Message,
                    ok: 'Aceptar',
                    skipHide: true
                });
                $mdDialog.show(alert);
            });
        };
        $scope.AdministrarUsuarios = function (ev) {
            $location.path('/usuarios');
        };
        $scope.goto = function (page) {
            //$scope.status = "Ir a " + page;
            switch (page) {
                case "Registro":
                    $scope.Pagina = 1;
                    break;
                case "Programa":
                    $scope.Pagina = 2;
                    break;
                case "Sede":
                    $scope.Pagina = 3;
                    break;
                case "Contacto":
                    $scope.Pagina = 4;
                    break;
                case "Casos":
                    $scope.Pagina = 5;
                    break;
                case "Encuesta":
                    $scope.Pagina = 6;
                    break;
            }
        };
    }]);