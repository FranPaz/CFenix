copisteriaFenixApp.controller('altaModalCtrl', function ($scope, insumosDataFactory, $modalInstance, $stateParams, $state, insumoMod, edit) {

    $scope.insumo = insumoMod;
    $scope.edit = edit;

    //#region iafar: funciones para ALTA de un insumo que tendra como cabecera un trabajo con tipoTrabajo = Producto   

    $scope.altaInsumo = function (insumo) {
        var trabajo = new Object; //Objeto trabajo

        trabajo.Insumos = [];
        trabajo.Insumos.push(insumo); //iafar: en este caso siempre sera uno
        trabajo.Nombre = insumo.Nombre;
        trabajo.UMedida = insumo.UMedida;
        trabajo.PrecioUnitario = insumo.PrecioVenta;

        //iafar: se guardara como un tipo trabajo producto, se resuelve en la web API
        insumosDataFactory.save(trabajo).$promise.then(
            function () {
                alert('Nuevo insumo Guardado');
               
                $modalInstance.close();
            },
            function (response) {
                $scope.errors = response.data;
                alert("Error en alta de insumos");
            });
    };

    //#endregion


    //#region iafar:  funciones para MODIFICAR un insumo y su trabajo asociado

    $scope.modInsumo = function (insumo) {
        //iafar: se guardara como un tipo trabajo producto, se resuelve en la web API
        insumosDataFactory.update({ id: insumo.Id}, insumo ).$promise.then(
            function () {
                alert('Insumo Modificado');
                $modalInstance.close();
            },
            function (response) {
                $scope.errors = response.data;
                alert("Error en la modificacion de insumo");
            });
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    //#endregion

});

