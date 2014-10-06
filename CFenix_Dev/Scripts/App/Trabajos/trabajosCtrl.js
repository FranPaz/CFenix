copisteriaFenixApp.controller('trabajosCtrl', function ($scope, $modal, $stateParams, $state) {






    //#region llamadas a partials de tabs en Dashboard de Trabajos
    $scope.trabajosAdd = function () {
        $state.go('Trabajos.add');
    };

    $scope.trabajosMod = function () {
        $state.go('Trabajos.mod');
    };

    //$scope.trabajosDetail = function () {
    //    $state.go('Trabajos.detail');
    //};
    //#endregion


});




