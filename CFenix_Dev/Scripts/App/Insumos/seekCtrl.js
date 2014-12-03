copisteriaFenixApp.controller('seekCtrl', function ($scope, insumosDataFactory, $modal, $modalInstance, $stateParams, $state) {
    //iafar: trae todos los insumos
    $scope.insumos = insumosDataFactory.query();

    //#region iafar: funciones modales para aceptar o cancelar
    $scope.productoVta;
    $scope.seleccionar = function (insumo) {
        $scope.productoVta = insumo;
    };

    $scope.ok = function () {
        $modalInstance.close($scope.productoVta);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
    //#endregion

});