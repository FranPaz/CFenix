var copisteriaFenixApp = angular.module('copisteriaFenixApp', ['ngRoute', 'ngResource', 'ui.router', 'ngCookies','ui.bootstrap',,'chieffancypants.loadingBar', 'ngAnimate',
  'ngSanitize']) //fpaz: defino el modulo con las librerias para routing (ui.router), usar apis rest y para interfaz de usuarios con angular(ui.bootstrap)
    .config(function ($stateProvider, $urlRouterProvider, cfpLoadingBarProvider) { //fpaz: configuro el routing de los states usando los servicios $stateProvider y $urlRouteProvider
        
        cfpLoadingBarProvider.includeSpinner = true;
        // fpaz:para cualquier caso que no este definido se va al estado del home mostrando grupos
        $urlRouterProvider.otherwise("/Cuentas")

        $stateProvider //fpaz: defino los states que van a guiar el ruteo de las vistas parciales de la app
        
        //#region Cuentas Corrientes
          .state('cuentasCorrientes', {
              url: "/Cuentas",
              views: {
                  'headerAdmin': {
                      templateUrl: '/Scripts/App/Partials/Header.html',
                      controller: ''
                  },
                  'menuAdmin': {
                      templateUrl: '/Scripts/App/Partials/Menu.html',
                      controller: ''
                  },
                  'dashboard': {
                      templateUrl: '/Scripts/App/Partials/Dashboard.html',
                      controller: '',
                      resolve: {
                          user: 'User',
                          authenticationRequired: function (user) {
                              user.isAuthenticated();
                          }
                      }
                  }
              }
          })
        //#endregion     

        //#region Trabajos e insumos
        .state('Insumos', {
            url: "/Insumos",
            views: {
                'headerAdmin': {
                    templateUrl: '/Scripts/App/Partials/Header.html',
                    controller: ''
                },
                'menuAdmin': {
                    templateUrl: '/Scripts/App/Partials/Menu.html',
                    controller: ''
                },
                'dashboard': {
                    templateUrl: '/Scripts/App/Insumos/Partials/AltaInsumo.html',
                    controller: 'insumosCtrl',
                    resolve: {
                        user: 'User',
                        authenticationRequired: function (user) {
                            user.isAuthenticated();
                        }
                    }
                }
            }
        })

            .state('Trabajos', {
                url: "/Trabajos",
                views: {
                    'headerAdmin': {
                        templateUrl: '/Scripts/App/Partials/Header.html',
                        controller: ''
                    },
                    'menuAdmin': {
                        templateUrl: '/Scripts/App/Partials/Menu.html',
                        controller: ''
                    },
                    'dashboard': {
                        templateUrl: '/Scripts/App/Trabajos/Partials/AltaTrabajo.html',
                        controller: '',
                        resolve: {
                            user: 'User',
                            authenticationRequired: function (user) {
                                user.isAuthenticated();
                            }
                        }
                    }
                }
            })


        //#endregion

        //#region Clientes
            .state('clientes', {
                url: "/Clientes",
                views: {
                    'headerAdmin': {
                        templateUrl: '/Scripts/App/Partials/Header.html',
                        controller: ''
                    },
                    'menuAdmin': {
                        templateUrl: '/Scripts/App/Partials/Menu.html',
                        controller: ''
                    },
                    'dashboard': {
                        templateUrl: '/Scripts/App/Clientes/Partials/Clientes_List.html',                        
                        resolve: {
                            user: 'User',                            
                            authenticationRequired: function (user) {
                                user.isAuthenticated();
                            },
                            clientesDataFactory: 'clientesDataFactory',
                            listadoClientes: function (clientesDataFactory) {
                                return clientesDataFactory.query();
                            },
                            cuentaCliente: function () {
                                return { value: [] };
                            }
                        },
                        controller: 'clientesCtrl'
                    }
                }
            })

            //fpaz: routing para los Tabs de Detalle Clientes
            .state('cliente_detail', {
                url: "/Cliente/:clienteId/Detail",                
                views: {
                    'headerAdmin': {
                        templateUrl: '/Scripts/App/Partials/Header.html',
                        controller: ''
                    },
                    'menuAdmin': {
                        templateUrl: '/Scripts/App/Partials/Menu.html',
                        controller: ''
                    },
                    'dashboard': {
                        templateUrl: '/Scripts/App/Clientes/Partials/Clientes_Detail.html',
                        controller: 'clientesCtrl',
                        resolve: {
                            user: 'User',
                            authenticationRequired: function (user) {
                                user.isAuthenticated();
                            },
                            listadoClientes: function () {
                                return { value: [] };
                            },
                            cuentaCliente: function () {
                                return { value: [] };
                            }
                        }
                    }
                }
            })
                .state('cliente_detail.info', {
                    url: "/Info",                    
                    views: {
                        'infoCuenta': {
                            templateUrl: '/Scripts/App/Clientes/Partials/Clientes_InfoCuenta.html',
                            controller: 'clientesCtrl',
                            resolve: {
                                user: 'User',
                                authenticationRequired: function (user) {
                                    user.isAuthenticated();
                                },
                                listadoClientes: function () {
                                    return { value: [] };
                                },
                                clientesDataFactory: 'clientesDataFactory',
                                cuentaCliente: function (clientesDataFactory, $stateParams) {
                                    //fpaz: trae los datos de un cliente en particular
                                    var clienteId = $stateParams.clienteId;
                                    return clientesDataFactory.get({ id: clienteId }).$promise;
                                }
                            }
                        }
                    }                    
                })

                .state('cliente_detail.movimientos', {
                    url: "/MovCC",
                    views: {
                        'movCuenta': {
                            templateUrl: '/Scripts/App/Clientes/Partials/Clientes_MovCuenta.html',
                            controller: '',
                            resolve: {
                                user: 'User',
                                authenticationRequired: function (user) {
                                    user.isAuthenticated();
                                }
                            }
                        }
                    }               
                })
            
                .state('cliente_detail.pagos', {
                    url: "/Pagos",
                    views: {
                        'pagosCuenta': {
                            templateUrl: '/Scripts/App/Clientes/Partials/Clientes_PagosCuenta.html',
                            controller: 'deudasCtrl',
                            resolve: {
                                user: 'User',
                                authenticationRequired: function (user) {
                                    user.isAuthenticated();
                                }
                            }
                        }
                    }               
                })
            
            //fpaz: Fin routing para los Tabs de Detalle Clientes

        //#endregion

        //#region Venta
             .state('venta', {
                 url: "/Venta",
                 views: {
                     'headerAdmin': {
                         templateUrl: '/Scripts/App/Partials/Header.html',
                         controller: ''
                     },
                     'menuAdmin': {
                         templateUrl: '/Scripts/App/Partials/Menu.html',
                         controller: ''
                     },
                     'dashboard': {
                         templateUrl: '/Scripts/App/Venta/Partials/Venta_Main.html',
                         controller: '',
                         resolve: {
                             user: 'User',
                             authenticationRequired: function (user) {
                                 user.isAuthenticated();
                             }
                         }
                     }
                 }
             })
            .state('cliente_detail.pagos.deudaVenta', {
                url: "/Detalle/Deuda",
                views: {                    
                    'detalleDeuda': {
                        templateUrl: '/Scripts/App/Venta/Partials/Venta_Deuda.html',
                        controller: '',
                        resolve: {
                            user: 'User',
                            authenticationRequired: function (user) {
                                user.isAuthenticated();
                            }
                        }
                    }
                }
            })
        //#endregion

        //#region Notificaciones
             .state('notificaciones', {
                 url: "/Notificaciones",
                 views: {
                     'headerAdmin': {
                         templateUrl: '/Scripts/App/Partials/Header.html',
                         controller: ''
                     },
                     'menuAdmin': {
                         templateUrl: '/Scripts/App/Partials/Menu.html',
                         controller: ''
                     },
                     'dashboard': {
                         templateUrl: '/Scripts/App/Notificaciones/Partials/page_messages.html',
                         controller: '',
                         resolve: {
                             user: 'User',
                             authenticationRequired: function (user) {
                                 user.isAuthenticated();
                             }
                         }
                     }
                 }
             })
        //#endregion

        //#region Usuarios y Login

          .state('usuarios', {
              url: "/Usuarios",
              views: {
                  'headerAdmin': {
                      templateUrl: '/Scripts/App/Partials/Header.html',
                      controller: ''
                  },
                  'menuAdmin': {
                      templateUrl: '/Scripts/App/Partials/Menu.html',
                      controller: ''
                  },
                  'dashboard': {
                      templateUrl: '/Scripts/App/Usuarios/Partials/List.html',
                      controller: '',
                      resolve: {
                          user: 'User',
                          authenticationRequired: function (user) {
                              user.isAuthenticated();
                          }
                      }
                  }
              }
          })
        .state('login', {
            url: "/login",
            views: {
                'dashboard': {
                    templateUrl: '/Scripts/App/Partials/Login.html',
                    controller: 'LoginCtrl',
                    resolve: {}
                }
            }
        }).state('signup', {
            url: "/signup",
            views: {
                'dashboard': {
                    templateUrl: '/Scripts/App/Partials/Signup.html',
                    controller: 'signupController',
                    resolve: {}
                }
            }
        }).state('logout', {
            templateUrl: '/Scripts/App/Partials/Login.html',
            controller: 'LogoutCtrl'
        })

        //#endregion
    })

   

    //rsanch Login antes de ver el state de ingresos.
    //.run(function ($rootScope, $state, User) {
    //    try {
    //        User.isAuthenticated();
    //    } catch (e) {
    //        // do nothing with this error
    //    }
    //    $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
    //        if (error.name === 'AuthenticationRequired') {
    //            User.setNextState(toState.name, 'Debes Iniciar Sesion para ver esta pagina');
    //            $state.go('login', {}, { reload: true });
    //        }
    //    });
    //});