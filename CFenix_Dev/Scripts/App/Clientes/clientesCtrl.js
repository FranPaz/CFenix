copisteriaFenixApp.controller('clientesCtrl', function ($scope, clientesDataFactory, $modal, $stateParams, $state) {
    
    //fpaz: trae todos los clientes
    $scope.clientes = clientesDataFactory.query();

    //fpaz: trae los datos de un cliente en particular
    $scope.clienteId = $stateParams.Id;
    $scope.cliente = clientesDataFactory.get({ id: $scope.clienteId });

    //funcion para agregar un nuevo Cliente y mostrarlo en el listado
    $scope.addCliente = function (cliente) {
        $scope.clientes.push(cliente);
    };

    //funcion para eliminar un Cliente y dejar de mostrarlo en el listado
    $scope.deleteCliente = function (cliente) {
        var i = $scope.clientes.indexOf(cliente);
        $scope.clientes.splice(i, 1);
    };

    //fpaz: funciones para ALTA en una ventana modal 
    $scope.open = function () {

        var modalInstance = $modal.open({// $modal.open() sirve para abrir una ventana modal que seria modalInstance
            templateUrl: '/Scripts/App/Clientes/Partials/Clientes_Add.html',
            controller: ModalInstanceCtrl,
            resolve: {
                cliente: function () {
                    return $scope.cliente;
                }
            }
        });

        modalInstance.result.then(function (cliente) {
            $scope.resultado = 'Alta Correcta'; 
            $scope.addCliente(cliente);
        }, function () {
            $scope.resultado = 'Alta Incorrecta';
        });
    };

    var ModalInstanceCtrl = function ($scope, $modalInstance, clientesDataFactory, cliente) {
        $scope.cliente = cliente;
        $scope.errors = [];
        $scope.alerts = [];
        $scope.ok = function (cliente) {
            //grupoEmpresario.IdHash = md5.createHash(grupoEmpresario.Cuit_Cuil);
            clientesDataFactory.save(cliente).$promise.then(
                function () {
                    $modalInstance.close(cliente);
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
    
});