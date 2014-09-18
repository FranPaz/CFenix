ocBizApp.controller('grupoEmpCtrl', function ($scope, grupoEmpDataFactory, $location, $modal, $log, $stateParams, $state, md5) {

    // fpaz: trae todos los grupos empresarios    
    $scope.grupos = grupoEmpDataFactory.query();

    //fpaz: trae los datos de un grupo en particular
    $scope.grupoId = $stateParams.Id;
    $scope.grupoEmpresario = grupoEmpDataFactory.get({ id: $scope.grupoId });

    //funcion para agregar un nuevo Grupo y mostrarlo en el listado
    $scope.addGrupo = function (grupoEmpresario) {
        $scope.grupos.push(grupoEmpresario);
    };

    //funcion para eliminar un Grupo y dejar de mostrarlo en el listado
    $scope.deleteGrupo = function (grupoEmpresario) {
        var i = $scope.grupos.indexOf(grupoEmpresario);
        $scope.grupos.splice(i, 1);
    };

    //fpaz: funciones para ALTA en una ventana modal 
    $scope.open = function () {

        var modalInstance = $modal.open({// $modal.open() sirve para abrir una ventana modal que seria modalInstance
            templateUrl: '/Scripts/App/GrupoEmpresario/Partials/AltaGrupoEmp.html',
            controller: ModalInstanceCtrl,
            resolve: {
                grupoEmpresario: function () {
                    return $scope.grupoEmpresario;
                }
            }
        });

        modalInstance.result.then(function (grupoEmpresario) {// esta parte funciona como un promise, es decir se ejecuta una funcion si se aprieta el btn ok y otra con el btn cancel
            $scope.resultado = 'Alta Correcta'; //se carga un resultado en el scope de la pagina "padre"            
            $scope.addGrupo(grupoEmpresario);
        }, function () {
            $scope.resultado = 'Alta Incorrecta';
        });
    };

    var ModalInstanceCtrl = function ($scope, $modalInstance, grupoEmpDataFactory, grupoEmpresario) {// esta funcion sirve de controlador para la ventana modal $modalInstance es una libreria que se usa para hacer ref a la instancia que se levanto para el modal       
        $scope.grupoEmpresario = grupoEmpresario;
        $scope.errors = [];
        $scope.alerts = [];
        $scope.ok = function (grupoEmpresario) {
            grupoEmpresario.IdHash = md5.createHash(grupoEmpresario.Cuit_Cuil);
            grupoEmpDataFactory.save(grupoEmpresario).$promise.then(
                function () {
                    $modalInstance.close(grupoEmpresario);// $modalInstance.close es una funcion por defecto de angular que cierra el modal y como parametro puede devolver un resultado, y el promise de arriba sale por el si                   
                },
                function (response) {
                    $scope.errors = response.data;
                });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };
    //Fin fpaz: funciones para ALTA en una ventana modal 

    //fpaz: funciones para MODIFICACION en una ventana modal 
    $scope.openUpdate = function (grupo) {

        var modalInstance = $modal.open({// $modal.open() sirve para abrir una ventana modal que seria modalInstance
            templateUrl: '/Scripts/App/GrupoEmpresario/Partials/ModificacionGrupoEmp.html',
            controller: ModalUpdateCtrl,
            resolve: {
                grupoEmpresario: function () {
                    return grupo;
                }
            }
        });

        modalInstance.result.then(function (grupoEmpresario) {// esta parte funciona como un promise, es decir se ejecuta una funcion si se aprieta el btn ok y otra con el btn cancel
            $scope.resultado = 'Modificacion Correcta'; //se carga un resultado en el scope de la pagina "padre"           
        }, function () {
            $scope.resultado = 'Modificacion Incorrecta';
        });
    };

    var ModalUpdateCtrl = function ($scope, $modalInstance, grupoEmpDataFactory, grupoEmpresario) {// esta funcion sirve de controlador para la ventana modal $modalInstance es una libreria que se usa para hacer ref a la instancia que se levanto para el modal       
        $scope.grupoEmpresario = grupoEmpresario;

        $scope.update = function (grupoEmpresario) {

            //grupoEmpDataFactory.update({ id: grupoEmpresario.Id }, grupoEmpresario);

            grupoEmpDataFactory.update({ id: grupoEmpresario.Id }, grupoEmpresario).$promise.then(
                function () {
                    $modalInstance.close('aceptar');// $modalInstance.close es una funcion por defecto de angular que cierra el modal y como parametro puede devolver un resultado, y el promise de arriba sale por el si
                },
                function myfunction() {
                    $modalInstance.dismiss('cancel');// $modalInstance.dismiss es una funcion por defecto de angular que se dispara cuando se cancela el form     
                });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };
    //Fin fpaz: funciones para MODIFICACION en una ventana modal 

    //fpaz: funciones para ELIMINACION
    $scope.delete = function (grupo) {
        grupoEmpDataFactory.delete(grupo).$promise.then(
            function () {
                $scope.deleteGrupo(grupo);
                //$scope.resultado = 'Eliminacion exitosa';
                $state.go('gruposEmp'); //fpaz:$state.go sirve para ir a otra vista usando los states, el nombre del state que se pasa como parametro tiene q estar definido en el config del modulo
            },
            function () {
                $scope.resultado = 'Error Eliminacion';
            });
    };
    //Fin fpaz: funciones para ELIMINACION

    //#region Ventana Modal para invitar nuevos gerentes a un grupo empresario    
    $scope.openInvitacionGerente = function () {

        var modalInstance = $modal.open({
            templateUrl: '/Scripts/App/Gerentes/Partials/InvitacionGerentes.html',
            controller: ModalInvitacionGerenteCtrl,
            resolve: {
                idHash: function () {
                    return $scope.grupoEmpresario.IdHash;
                }
            }
        });

        modalInstance.result.then(function () {
            $scope.resultInvitacion = 'Invitacion Correcta';
        }, function () {
            $scope.resultInvitacion = 'Invitacion Incorrecta';
        });
    };

    var ModalInvitacionGerenteCtrl = function ($scope, $modalInstance, idHash) {
        $scope.hash = idHash;
        $scope.ok = function () {
            $modalInstance.close();
        };
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };
    //#endregion
});









