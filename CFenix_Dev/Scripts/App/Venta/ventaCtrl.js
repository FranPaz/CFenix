copisteriaFenixApp.controller('ventaCtrl', function ($scope, ventasDataFactory, $stateParams, $state, ventaSvc) {

    var detalles = [];
    $scope.detallesFactura = detalles; //lleno el scope de detallesFactura con los detalles que se van cargando o los que ya me devuelve al llamar a la pantalla

    //#region Agregar producto al listado de Detalle de Factura
    $scope.agregarProducto = function (productoVta) {

        productoVta.Subtotal = productoVta.Cantidad * productoVta.PrecioUnitario;
        //ventaSvc.addDetallesFacturaVta(productoVta);
        detalles.push(productoVta);

        $scope.productoVta = null;
    }

    
    
    $scope.addVenta = function () {
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
    }


});