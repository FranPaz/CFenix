copisteriaFenixApp.controller('comprasCtrl', function ($scope, $stateParams, $state, comprasDataFactory, listComprasCliente) {
    $scope.isCollapsed = false;
    $scope.comprasClientes = listComprasCliente; //trae todas las compras del cliente    
});