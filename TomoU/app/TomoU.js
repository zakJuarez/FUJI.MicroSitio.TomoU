angular.module('login', ['ngMaterial', 'ngMessages']);
angular.module('tomou', ['ngMaterial', 'ngMessages', 'ngRoute', 'ngCookies', 'ngSanitize', 'ui.bootstrap', 'ngIdle', 'login', 'ui.bootstrap'])
    .config(function ($mdThemingProvider) {
        var fujiTheme = $mdThemingProvider.extendPalette('blue-grey', {
            //Poner el tema de la empresa
        });
        $mdThemingProvider.definePalette('fujiTheme', fujiTheme);
        $mdThemingProvider.theme('default')
            .backgroundPalette('grey', {
                'default': '200'
            })
            .primaryPalette('blue-grey');
    })
    .config(function ($mdDateLocaleProvider) {
        $mdDateLocaleProvider.months = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
            'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
        $mdDateLocaleProvider.shortMonths = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
            'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];
        $mdDateLocaleProvider.days = ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sábado'];
        $mdDateLocaleProvider.shortDays = ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'];
        $mdDateLocaleProvider.firstDayOfWeek = 1;
        $mdDateLocaleProvider.weekNumberFormatter = function (weekNumber) {
            return 'Semana ' + weekNumber;
        };
        $mdDateLocaleProvider.msgCalendar = 'Calendario';
        $mdDateLocaleProvider.msgOpenCalendar = 'Abrir calendario';
        $mdDateLocaleProvider.formatDate = function (date) {
            var m = moment(date);
            return m.isValid() ? m.format('DD/MM/YYYY') : '';
        };
        $mdDateLocaleProvider.parseDate = function (dateString) {
            var m = moment(dateString, 'DD/MM/YYYY', true);
            return m.isValid() ? m.toDate() : new Date(NaN);
        };
    })
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'app/modules/home/homeView.html',
                controller: 'homeController'
            })
    }])
    .config(function (IdleProvider, KeepaliveProvider) {
        IdleProvider.idle(299); // 5 min
        IdleProvider.timeout(1);
    })
    .run(['$rootScope', '$location', '$cookieStore', '$http', '$mdDialog', '$templateCache', 'Idle',
        function ($rootScope, $location, $cookieStore, $http, $mdDialog, $templateCache, Idle) {
            $rootScope.$on('IdleTimeout', function () {
                var alert = $mdDialog.alert({
                    title: 'Advertencia',
                    textContent: 'se ha terminado su sesión',
                    ok: 'Aceptar'
                });
                $mdDialog.show(alert).then(function () {
                    //$location.path('/login');
                });
                $location.path('/login');
            });
            $rootScope.logeado = false;
            $rootScope.Globals = $cookieStore.get('Globals') || {};
            if ($rootScope.Globals.CurrentUser) {
                $rootScope.logeado = true;
            }
            $rootScope.$on('$locationChangeStart', function (event, next, current) {
                /*
                    Generar método para control de accesos
                */
                var location = $location.path();
                switch (location) {
                    default:
                            $location.path('/');//$location.path('/login');
                        break;
                }
            })
        }]);