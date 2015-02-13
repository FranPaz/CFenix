copisteriaFenixApp.controller('insumosCtrl', function ($scope, insumosDataFactory, $modal, $stateParams, $state) {
    //#region iafar:Operaciones basicas

    //iafar: trae todos los insumos
    $scope.insumos = insumosDataFactory.query();

    //iafar: trae los datos de un insumo en particular
    $scope.insumoId = $stateParams.Id;

    //#region iafar:funcion para agregar un nuevo insumo al listado mostrado

    $scope.addInsumo = function (insumo) {
        $scope.insumos.push(insumo);
        $scope.insumo = null;
    };

    //#endregion
   

    //#region iafar: ventana modal para el alta de un insumo

    $scope.openAltaInsumo = function () {

        var modalInstance = $modal.open({
            templateUrl: '/Scripts/App/Insumos/Partials/Insumos_Add.html',
            controller: 'altaModalCtrl',
            size: 'lg',
            windowClass: 'bs-example-modal-lg',
            resolve: {
                insumoMod: function () {
                    return { value: [] };
                },
                edit: function () {
                    return false    
                }
            }
        });

        modalInstance.result.then(function () {
            //iafar:Agrego a la lista el nuevo producto 
            $scope.insumos = insumosDataFactory.query();
        });

    };
    //#endregion

    //#region iafar: llamada a ventana modal para modificar un insumo
    $scope.openModInsumo = function (insumoMod) {
        
        var modalInstance = $modal.open({
            templateUrl: '/Scripts/App/Insumos/Partials/Insumos_Add.html',
            controller: 'altaModalCtrl',
            size: 'lg',
            windowClass: 'bs-example-modal-lg',
            resolve: {
                insumoMod: function () {
                    return insumoMod;
                },
                edit: function() {
                    return true
                }
            }
        });
        
        modalInstance.result.then(function () {
            //iafar:Agrego a la lista el nuevo producto 
            $scope.insumos = insumosDataFactory.query();
        });

    };
    //#endregion

});
