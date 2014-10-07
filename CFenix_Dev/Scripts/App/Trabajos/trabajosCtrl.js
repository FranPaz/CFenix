copisteriaFenixApp.controller('trabajosCtrl', function ($scope, trabajosDataFactory, $modal, $stateParams, $state) {
    //iafar: trae todos los trabajos
    $scope.trabajos = trabajosDataFactory.query();

    //iafar: trae los datos de un trabajo en particular
    $scope.trabajoId = $stateParams.Id;
    $scope.trabajo = trabajosDataFactory.get({ id: $scope.clienteId });

    //funcion para agregar un nuevo trabajo y mostrarlo en el listado
    $scope.addTrabajo = function (trabajo) {
        $scope.trabajos.push(trabajo);
    };




    //#region llamadas a partials de tabs en Dashboard de Trabajos
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




