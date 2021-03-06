var copisteriaFenixApp = angular.module('copisteriaFenixApp', ['ngRoute', 'ngResource', 'ui.router', 'ngCookies','ui.bootstrap',,'chieffancypants.loadingBar', 'ngAnimate', 'ngTable',
  'ngSanitize', 'ui.select']) //fpaz: defino el modulo con las librerias para routing (ui.router), usar apis rest y para interfaz de usuarios con angular(ui.bootstrap)
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

            //Dashboard para insumos
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
                    templateUrl: '/Scripts/App/Insumos/Partials/ListInsumos.html',
                    controller: 'insumosCtrl',
                    resolve: {
                        user: 'User',
                        authenticationRequired: function (user) {
                            user.isAuthenticated();
                        }
                    }
                }//iafar:Esto inserta la vista parcial(insumos_add) en un marco(datosInsumo) dentro de un estado(Insumos)
                //'datosInsumo@Insumos': {
                //    templateUrl: '/Scripts/App/Insumos/Partials/Insumos_Add.html',
                //    controller: '',
                //    resolve: {
                //        user: 'User',
                //        authenticationRequired: function (user) {
                //            user.isAuthenticated();
                //        }
                //    }
                //}

            }
        })
            //Alta de un nuevo Insumo
            .state('Insumos.add', {
                url: "/Insumos",
                views: {
                    'datosInsumo@Insumos': {
                        templateUrl: '/Scripts/App/Insumos/Partials/Insumos_Add.html',
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
             //Fin Dashboard de insumos

            //Dashboard Para Trabajos
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
                        templateUrl: '/Scripts/App/Trabajos/Partials/ListTrabajos.html',
                        controller: 'trabajosCtrl',
                        resolve: {
                            user: 'User',
                            authenticationRequired: function (user) {
                                user.isAuthenticated();
                            }
                        }
                    }
                }
            })

            //iafar:Routing para los tabs de Trabajo

            //Alta de un nuevo trabajo
            .state('Trabajos.add', {
                url: "/Add",
                views: {
                    'AddTrabajos': {
                        templateUrl: '/Scripts/App/Trabajos/Partials/Trabajos_Add.html',
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

            //Modificacion de un trabajo
            .state('Trabajos.mod', {
                url: "/Mod",
                views: {
                    'ModTrabajos': {
                        templateUrl: '/Scripts/App/Trabajos/Partials/Trabajos_Mod.html',
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

            //Detalle de un trabajo
        //"/Trabajos/Detail"
            .state('Trabajos.detail', {
                url: "/Detail",
                views: {
                    'DetailTrabajos': {
                        templateUrl: '/Scripts/App/Trabajos/Partials/Trabajos_Detail.html',
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
            //Fin de routing de Tabs para Trabajo

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
                            },
                            listdeudascliente: function () {
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
                            },
                            listdeudascliente: function () {
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
                                },
                                listdeudascliente: function () {
                                    return { value: [] };
                                }
                            }
                        }
                    }                    
                })

                .state('cliente_detail.movimientos', {
                    url: "/MovCC",
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
                                },
                                listdeudascliente: function () {
                                    return { value: [] };
                                }
                            }
                        },
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

                .state('cliente_detail.historial', {
                    url: "/HistorialCompras",
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
                                },
                                listdeudascliente: function () {
                                    return { value: [] };
                                }
                            }
                        },
                        'historial': {
                            templateUrl: '/Scripts/App/Clientes/Partials/Cliente_HistorialCompras.html',
                            controller: 'comprasCtrl',
                            resolve: {
                                user: 'User',
                                authenticationRequired: function (user) {
                                    user.isAuthenticated();
                                },
                                comprasDataFactory: 'comprasDataFactory',
                                listComprasCliente: function (comprasDataFactory, $stateParams) {
                                    //fpaz: trae las compras de un cliente en particular
                                    var clienteId = $stateParams.clienteId;
                                    return comprasDataFactory.query({ id: clienteId });
                                }
                            }
                        }
                    }
                })
                    .state('cliente_detail.historial.detalleCompra', {
                        url: "/Factura/:ventaId/Detalle",
                        views: {
                            'detalleCompra': {
                                templateUrl: '/Scripts/App/Deudas/Partials/Deuda_DetalleDeuda.html',
                                controller: 'deudasCtrl',
                                resolve: {
                                    user: 'User',
                                    authenticationRequired: function (user) {
                                        user.isAuthenticated();
                                    },
                                    deudasDataFactory: 'deudasDataFactory',
                                    listDetalleDeuda: function (deudasDataFactory, $stateParams) {
                                        //fpaz: trae los detalles de una deuda en particular
                                        var clienteId = $stateParams.clienteId;
                                        var ventaId = $stateParams.ventaId;
                                        return deudasDataFactory.query({ prmIdCliente: clienteId, prmIdVenta: ventaId });
                                    },
                                    listDeudasCliente: function () {
                                        return { value: [] };
                                    }
                                }
                            }
                        }
                    })
                    .state('cliente_detail.historial.detallePagosCompra', {
                        url: "/Factura/:ventaId/Pagos",
                        views: {
                            'detallePagosCompra': {
                                templateUrl: '/Scripts/App/Compras/Partials/Compra_DetallePagos.html',
                                controller: 'pagosCtrl',
                                resolve: {
                                    user: 'User',
                                    authenticationRequired: function (user) {
                                        user.isAuthenticated();
                                    },
                                    pagosDataFactory: 'pagosDataFactory',
                                    listPagosCompra: function (pagosDataFactory, $stateParams) {
                                        //fpaz: trae los detalles de una deuda en particular
                                        var clienteId = $stateParams.clienteId;
                                        var ventaId = $stateParams.ventaId;
                                        return pagosDataFactory.query({id: ventaId });
                                    }
                                }
                            }
                        }
                    })
            
                // routing para pagos pendientes del cliente
                .state('cliente_detail.pagos', {
                    url: "/Deudas",
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
                                },
                                listdeudascliente: function () {
                                    return { value: [] };
                                }
                            }
                        },
                        'pagosCuenta': {
                            templateUrl: '/Scripts/App/Clientes/Partials/Clientes_PagosCuenta.html',                            
                            controller: 'deudasCtrl',
                            resolve: {
                                user: 'User',
                                authenticationRequired: function (user) {
                                    user.isAuthenticated();
                                },                                
                                deudasDataFactory: 'deudasDataFactory',
                                listDeudasCliente: function (deudasDataFactory, $stateParams) {
                                    //fpaz: trae los datos de un cliente en particular
                                    var clienteId = $stateParams.clienteId;
                                    return deudasDataFactory.query({ id: clienteId });
                                },
                                listDetalleDeuda: function () {
                                    return { value: [] };
                                }
                            }
                        }
                    }               
                })
                    .state('cliente_detail.pagos.deudaVenta', {
                        url: "/Factura/:ventaId",
                        views: {
                            'detalleDeuda': {
                                templateUrl: '/Scripts/App/Deudas/Partials/Deuda_DetalleDeuda.html',
                                controller: 'deudasCtrl',
                                resolve: {
                                    user: 'User',
                                    authenticationRequired: function (user) {
                                        user.isAuthenticated();
                                    },
                                    deudasDataFactory: 'deudasDataFactory',
                                    listDetalleDeuda: function (deudasDataFactory, $stateParams) {
                                        //fpaz: trae los detalles de una deuda en particular
                                        var clienteId = $stateParams.clienteId;
                                        var ventaId = $stateParams.ventaId;
                                        return deudasDataFactory.query({ prmIdCliente: clienteId, prmIdVenta: ventaId });
                                    },
                                    listDeudasCliente: function () {                                        
                                        return { value: [] };
                                    }
                                }
                            }
                        }
                    })
            //fpaz: Fin routing para los Tabs de Detalle Clientes             
        //#endregion

        //#region Venta             
            .state('ventaBase', {
                abstract: true,                
                templateUrl: '/Scripts/App/Venta/Partials/Venta_Main.html',
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
                        controller: 'ventaCtrl',
                        resolve: {
                            user: 'User',
                            authenticationRequired: function (user) {
                                user.isAuthenticated();
                            }
                        }
                    }
                }
            })

            .state('ventaBase.venta', {
                url: "/Venta",
                templateUrl: '/Scripts/App/Clientes/Partials/Cliente_Busqueda.html',
                views: {
                    'busquedaCliente': {
                        templateUrl: '/Scripts/App/Clientes/Partials/Cliente_Busqueda.html',
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
                            },
                            listdeudascliente: function () {
                                return { value: [] };
                            }
                        },
                        controller: 'clientesCtrl'
                    },
                    'tablaDetalle': {
                        templateUrl: '/Scripts/App/Venta/Partials/Venta_DetalleFactura.html',                        
                        controller: ''
                    }
                }
            })

            .state('pagosBase.registracionVenta', { // estado donde se registra la venta y se hace el pago de la misma
                url: "/Venta/Registracion/:detalles",                
                views: {                    
                    'clienteVta': {
                        templateUrl: '/Scripts/App/Pagos/Partials/Pagos_InfoCliente.html',
                        controller: 'pagosCtrl',
                        resolve: {
                                user: 'User',
                                authenticationRequired: function (user) {
                                    user.isAuthenticated();
                                },
                                listPagosCompra: function () {
                                    return { value: [] };
                                }
                            }                        
                    },
                    'tablaDetalle': {
                        templateUrl: '/Scripts/App/Venta/Partials/Venta_DetalleFactura.html',
                        controller: 'pagosCtrl',
                        resolve: {
                                user: 'User',
                                authenticationRequired: function (user) {
                                    user.isAuthenticated();
                                },
                                listPagosCompra: function () {
                                    return { value: [] };
                                }
                            }                        
                    },
                    'formasPago': {
                        templateUrl: '/Scripts/App/Pagos/Partials/Pagos_FormaPago.html',
                        controller: 'pagosCtrl',
                        resolve: {
                                user: 'User',
                                authenticationRequired: function (user) {
                                    user.isAuthenticated();
                                },
                                listPagosCompra: function () {
                                    return { value: [] };
                                }
                            }                        
                    }
                }
            })
        //#endregion

        //#region Pagos
            .state('pagosBase', {
                abstract: true,                
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
                        templateUrl: '/Scripts/App/Pagos/Partials/Pagos_ProcVenta.html',
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

        //#region Cajas
             .state('Cajas', {
                 url: "/Cajas",
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
                         templateUrl: '/Scripts/App/Cajas/Partials/Cajas_List.html',
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
