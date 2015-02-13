copisteriaFenixApp.controller('trabajosCtrl', function ($scope, trabajosDataFactory, insumosDataFactory, $modal, $stateParams, $state) {

    //iafar: trae todos los trabajos
    $scope.trabajos = trabajosDataFactory.query();
    $scope.insumos = insumosDataFactory.query();
    $scope.listaInsumos = [];

    //iafar: trae los datos de un trabajo en particular
    $scope.trabajoId = $stateParams.Id;
    $scope.trabajo = trabajosDataFactory.get({ id: $scope.trabajoId });

    //#region iafar: funcion para agregar un nuevo trabajo y mostrarlo en el listado
    $scope.addTrabajo = function (trabajo) {
        $scope.trabajos.push(trabajo);
    };

    $scope.addInsumo= function (insumo) {
        $scope.listaInsumos.push(insumo);
        insumo = {};
    };
    //#endregion

    //#region iafar: funciones para ALTA de un insumo   
    $scope.altaTrabajo = function (trabajo) {
        trabajo.TipoTrabajoId = 1;
        var parametrosTrabajo = new Object;
        parametrosTrabajo.listInsumo = $scope.listaInsumos;
        parametrosTrabajo.trabajo = trabajo;
        trabajosDataFactory.save(parametrosTrabajo).$promise.then(
            function () {
                //$scope.addInsumo(insumo);
                alert('Nuevo trabajo Guardado');
            },
            function (response) {
                $scope.errors = response.data;
                alert('Error al guardar trabajo Trabajo');
            });
    };
    //#endregion
    
    //#region iafar: llamadas a partials de tabs en Dashboard de Trabajos
    $scope.TrabajosAdd = function () {
        $state.go('Trabajos.add');
    };
    //deberia pasar como parametro el id?
    $scope.TrabajosMod = function () {
        $state.go('Trabajos.mod');
    };

    $scope.TrabajosDetail = function () {
        $state.go('Trabajos.detail');
    };
    //#endregion


});




