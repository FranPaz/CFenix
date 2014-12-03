copisteriaFenixApp.controller('ventaCtrl', function ($scope, ventasDataFactory, $stateParams, $state, ventaSvc, $modal, $log) {

    var detalles = [];
    $scope.detallesFactura = detalles; //lleno el scope de detallesFactura con los detalles que se van cargando o los que ya me devuelve al llamar a la pantalla
    $scope.totalVta = 0;
    $scope.edicionDetallesVta = true;//habilita o no las opciones de edicion de la fila del detalle de la venta

    //#region Agregar producto al listado de Detalle de Factura y Registrar Venta
    $scope.agregarProducto = function (productoVta) {

        productoVta.Subtotal = productoVta.Cantidad * productoVta.PrecioUnitario;
        $scope.totalVta += parseFloat(productoVta.Subtotal);
        ventaSvc.addTotalVta($scope.totalVta);
        ventaSvc.addDetallesFacturaVta(productoVta);
        detalles.push(productoVta);

        $scope.productoVta = null;
    };
    $scope.addVenta = function () { // registra la venta
        ventaSvc.addDetallesFacturaVta(detalles);        
        var venta = new Object;
        venta.Cliente = ventaSvc.getCliente();
        venta.DetallesVta = ventaSvc.listaDetallesFacturaVta();
        ventasDataFactory.save(venta).$promise.then(
            function () {
                ventaSvc.limpiarVta();
                $scope.detallesFactura = detalles = [];
                alert("Venta Registrada Correctamente");
            },
            function (response) {
                //$scope.errors = response.data;
                alert("Error al Registrar la Venta");
            });
    };
    //#endregion

    //#region Date picker de Seleccion de producto en pantalla de venta
    //iafar: controlador para el DTPicker

    $scope.today = function () {
        $scope.dt = new Date();
    };

    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.toggleMin = function () {
        $scope.minDate = $scope.minDate ? null : new Date();
    };
    $scope.toggleMin();

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];

    //#endregion

});