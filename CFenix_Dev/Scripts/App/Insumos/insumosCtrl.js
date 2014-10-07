copisteriaFenixApp.controller('insumosCtrl', function ($scope, insumosDataFactory, $modal, $stateParams, $state) {
    //iafar: trae todos los insumos
    $scope.insumos = insumosDataFactory.query();

    //iafar: trae los datos de un insumo en particular
    $scope.insumoId = $stateParams.Id;
    $scope.trabajo = insumosDataFactory.get({ id: $scope.insumoId });

    //funcion para agregar un nuevo insumo y mostrarlo en el listado
    $scope.addinsumo = function (insumo) {
        $scope.insumos.push(insumo);
    };




    //#region llamadas a partials de tabs en Dashboard de Insumos
    $scope.InsumosAdd = function () {
        $state.go('Insumos.add');
    };
    //deberia pasar como parametro el id?
    $scope.InsumosMod = function () {
        $state.go('Insumos.mod');
    };

    $scope.InsumosDetail = function () {
        $state.go('Insumos.detail');
    };
    //#endregion


});
