copisteriaFenixApp.controller('clientesCtrl', function ($scope, clientesDataFactory, $modal, $stateParams, $state, listadoClientes, cuentaCliente, ventaSvc, listdeudascliente, ngTableParams, $filter) {

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
        $scope.clientes = clientesDataFactory.query(); // lleno la tabla con el nuevo cliente agregado
        $scope.cliente = {};
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

    //#region Limpieza de campos
    $scope.clean = function () { //funcion para limpiar los campos de los datos del cliente en el alta de cliente
        $scope.cliente = {};
    }
    //#endregion
    
    //#region Busqueda Cliente Venta

    //fpaz: variable donde se va a guardar el clientes seleccionado en el dropdown de busqueda de cliente, todo se hace con los tags select del paquete select-ui
    $scope.clienteBuscado = {};    

    $scope.addClienteVta = function (item, model) { // fpaz: agrega el cliente al que se le va a hacer la venta al servicio compartido ventaSvc se activa con el evento on-select="addClienteVta($item, $model)"               
        ventaSvc.addClienteVta(model);
    };
    //#endregion

    //#region Paginacion y llenado y filtrado de la tabla dinamica de Clientes

    var data = $scope.clientes;

    $scope.tableParams = new ngTableParams({
        page: 1,            // show first page
        count: 10,          // count per page
        filter: {
            // filtros de la tabla, 
            Id: '', //por Id de Cliente
            Nombre_RazonSocial: ''// por nombre de Escuela
        }
    }, {
        total: data.length, // saco el Total de registros del listado de escuelas
        getData: function ($defer, params) {            
            var orderedData = params.filter() ?
                   $filter('filter')(data, params.filter()) :
                   data;
            $scope.clientes = orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count());

            params.total(orderedData.length); // set total for recalc pagination
            $defer.resolve($scope.clientes);
        }
    });
    //#endregion

    
});