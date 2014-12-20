copisteriaFenixApp.controller('pagosCtrl', function ($scope, pagosDataFactory,ventasDataFactory, ventaSvc) {

    $scope.infoCliente = ventaSvc.getCliente(); // var para mostrar la info del cliente
    $scope.detallesFactura = ventaSvc.listaDetallesFacturaVta(); //var para mostrar en la Pantalla de pagos el detalle de la compra que se esta haciendo, viene de la funcion addVenta de la pantalla de Venta
    $scope.totalVta = ventaSvc.getTotalVta(); // traigo el total de la venta

    $scope.edicionDetallesVta = false;//habilita o no las opciones de edicion de la fila del detalle de la venta
    
    //#region Manejo de check box
    $scope.formasPago = [ // array con los tipos de forma de pago y si estan habilitados o no
        { id: 1, forma: 'Pago en Efectivo', seleccionado:true },
        { id: 2, forma: 'Pago con Cuenta Corriente', seleccionado: false },
        { id: 3, forma: 'Pago con Cheque', seleccionado: false }
    ];
    //#endregion
    
    //#region Alta Venta
    $scope.ventaId;
    $scope.addVenta = function () { // registra la venta        
        var venta = new Object;
        venta.Cliente = ventaSvc.getCliente();
        venta.DetallesVta = $scope.detallesFactura;
        ventasDataFactory.save(venta).$promise.then(
            function (response) {
                ventaSvc.limpiarVta();
                $scope.detallesFactura = detalles = [];
                alert("Venta Registrada Correctamente");
                $scope.venta = response;
                $scope.registrar();
            },
            function (response) {
                //$scope.errors = response.data;
                alert("Error al Registrar la Venta");
            });
    };
    //#endregion

    //#region Alta Pagos

    $scope.registrar = function () {        
        var pago = new Object;
        pago.MontoPago = 0;
        pago.Venta = $scope.venta;
        
        

        if ($scope.formasPago[0].seleccionado) {
            //alert("Pago con Efectivo");
            pago.MontoPago += parseFloat($scope.pagoEfectivo.Monto);            
        }
        if ($scope.formasPago[1].seleccionado) {
            //alert("Pago con CC");
            pago.MontoPago += parseFloat($scope.pagoCC.Monto);
            $scope.pagoCC.CuentaCorrienteId = 123;
            //pago.DetallesPagos.push($scope.pagoCC);
        }
        if ($scope.formasPago[2].seleccionado) {
            //alert("Pago con Cheque");
            pago.MontoPago += parseFloat($scope.pagoCheque.Monto);            
        }

        $scope.resultPago = pago;

        var pagoCustom = new Object; //instancio un objeto donde voy a pasar como parametros todos los tipo de pagos
        pagoCustom.pago = pago;
        pagoCustom.pagoEfectivo = $scope.pagoEfectivo;
        pagoCustom.pagoCC = $scope.pagoCC;
        pagoCustom.pagoCheque = $scope.pagoCheque;
        

        pagosDataFactory.save(pagoCustom).$promise.then(
            function (response) {                
                alert("Pago Registrado Correctamente");                
            },
            function (response) {
                //$scope.errors = response.data;
                alert("Error al Registrar el Pago");
            });
    }
    //endregion
});