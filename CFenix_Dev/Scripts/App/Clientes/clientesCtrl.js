copisteriaFenixApp.controller('clientesCtrl', function ($scope, clientesDataFactory, $modal, $stateParams, $state, listadoClientes, cuentaCliente, ventaSvc, listdeudascliente) {

    $scope.deudasClientes = listdeudascliente;
    //fpaz: trae todos los clientes
    $scope.clientes = listadoClientes;

    //fpaz: trae los datos de un cliente en particular    
    $scope.cuentaCliente = cuentaCliente;    

    //funcion para eliminar un Cliente y dejar de mostrarlo en el listado
    $scope.deleteCliente = function (cliente) {
        var i = $scope.clientes.indexOf(cliente);
        $scope.clientes.splice(i, 1);
    };

    //#region funciones para ALTA de Cliente   

    //funcion para agregar un nuevo Cliente y mostrarlo en el listado
    $scope.addCliente = function (cliente) {
        $scope.clientes.push(cliente);
        $scope.cliente = null;
    };

    $scope.altaCliente = function (cliente) {        
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
    
    //#region Busqueda Cliente Venta

    //fpaz: variable donde se va a guardar el clientes seleccionado en el dropdown de busqueda de cliente, todo se hace con los tags select del paquete select-ui
    $scope.clienteBuscado = {};    

    $scope.addClienteVta = function (item, model) { // fpaz: agrega el cliente al que se le va a hacer la venta al servicio compartido ventaSvc se activa con el evento on-select="addClienteVta($item, $model)"               
        ventaSvc.addClienteVta(model);
    };
    //#endregion

    
});