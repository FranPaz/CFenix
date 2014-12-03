copisteriaFenixApp.controller('insumosCtrl', function ($scope, insumosDataFactory, $modal, $stateParams, $state) {
    //iafar: trae todos los insumos
    $scope.insumos = insumosDataFactory.query();

    //iafar: trae los datos de un insumo en particular
    $scope.insumoId = $stateParams.Id;
    $scope.trabajo = insumosDataFactory.get({ id: $scope.insumoId });//iafar:es necesario esto aqui?

    //iafar:funcion para agregar un nuevo insumo al listado mostrado
    $scope.addInsumo = function (insumo) {
        $scope.insumos.push(insumo);
        $scope.insumo = null;
    };

    //#region iafar: funciones para ALTA de un insumo   

    $scope.altaInsumo = function (insumo) {
        
        insumosDataFactory.save(insumo).$promise.then(
            function () {
                //$scope.addInsumo(insumo);
                alert('Nuevo insumo Guardado');
                $scope.addInsumo(insumo);
            },
            function (response) {
                $scope.errors = response.data;
                alert('Error al guardar insumo Insumo');
            });
    };
    //#endregion

    //#region iafar: funciones modales para aceptar o cancelar
    $scope.seleccionar = function (insumo) {
        $scope.insumoMod = insumo;
    };

    $scope.Mod = function () {
       
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
    //#endregion

    //#region llamadas a partials de tabs en Dashboard de Insumos
    $scope.InsumosAdd = function () {
        $state.go('Insumos.add');
        $scope.insumoMod = null;
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
