copisteriaFenixApp.controller('deudasCtrl', function ($scope, $stateParams, $state,deudasDataFactory,listDeudasCliente,listDetalleDeuda) {
    $scope.isCollapsed = false;
    $scope.deudasClientes = listDeudasCliente; //trae todas las deudas del cliente
    $scope.detallesDeuda = listDetalleDeuda; //trae los detalles de la deuda
});