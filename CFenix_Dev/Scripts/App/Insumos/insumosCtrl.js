copisteriaFenixApp.controller('insumosCtrl', function ($scope, insumosDataFactory, $modal, $stateParams, $state) {
    //iafar: trae todos los insumos
    $scope.insumos = insumosDataFactory.query();

    //iafar: trae los datos de un insumo en particular
    $scope.insumoId = $stateParams.Id;
    $scope.trabajo = insumosDataFactory.get({ id: $scope.insumoId });

    //funcion para agregar un nuevo insumo y mostrarlo en el listado
    $scope.addInsumo = function (insumo) {
        alert('Prueba')
        $scope.insumos.push(insumo);
    };

    //#region iafar: funciones para ALTA de un insumo   
    $scope.altaInsumo = function (insumo) {
        
        insumosDataFactory.save(insumo).$promise.then(
            function () {
                //$scope.addInsumo(insumo);
                alert('Nuevo insumo Guardado');
            },
            function (response) {
                $scope.errors = response.data;
                alert('Error al guardar insumo Insumo');
            });
    };
    //#endregion





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
