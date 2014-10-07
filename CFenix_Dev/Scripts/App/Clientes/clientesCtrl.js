copisteriaFenixApp.controller('clientesCtrl', function ($scope, clientesDataFactory, $modal, $stateParams, $state, listadoClientes, cuentaCliente) {
    
    //fpaz: trae todos los clientes
    $scope.clientes = listadoClientes;

    //fpaz: trae los datos de un cliente en particular    
    $scope.cuentaCliente = cuentaCliente;

    //funcion para agregar un nuevo Cliente y mostrarlo en el listado
    $scope.addCliente = function (cliente) {
        $scope.clientes.push(cliente);
    };

    //funcion para eliminar un Cliente y dejar de mostrarlo en el listado
    $scope.deleteCliente = function (cliente) {
        var i = $scope.clientes.indexOf(cliente);
        $scope.clientes.splice(i, 1);
    };

    //#region fpaz: funciones para ALTA de Cliente   
    $scope.altaCliente = function (cliente) {
        //grupoEmpresario.IdHash = md5.createHash(grupoEmpresario.Cuit_Cuil);
        clientesDataFactory.save(cliente).$promise.then(
            function () {
                $scope.addCliente(cliente);
                alert('Nuevo Cliente Guardado');
            },
            function (response) {
                $scope.errors = response.data;
                alert('Error al guardar cliente Cliente');
            });
    };
    //#endregion
    
    //#region llamadas a partials de tabs en detalle de cliente
    $scope.infoCuenta = function () {
        $state.go('cliente_detail.info');
    };
    $scope.movCuenta = function () {
        $state.go('cliente_detail.movimientos');
    };
    $scope.pagosCuenta = function () {
        $state.go('cliente_detail.pagos');
    };
    //#endregion
});