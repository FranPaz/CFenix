/// <reference path="../Insumos/Partials/Insumos_Seek.html" />
/// <reference path="../Insumos/Partials/Insumos_Seek.html" />
copisteriaFenixApp.controller('ventaCtrl', function ($scope, ventasDataFactory, insumosDataFactory, $stateParams, $state, $modal, ventaSvc) {

    var detalles = [];
    $scope.detallesFactura = detalles; //lleno el scope de detallesFactura con los detalles que se van cargando o los que ya me devuelve al llamar a la pantalla

    //#region Agregar producto al listado de Detalle de Factura y Registrar Venta
    $scope.agregarProducto = function (productoVta) {

        productoVta.Subtotal = productoVta.Cantidad * productoVta.PrecioUnitario;
        //ventaSvc.addDetallesFacturaVta(productoVta);
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

    //#region ventana modal de Busqueda de insumos
    $scope.productoVta = [];
   
    $scope.openBusProducto = function () {

        var modalInstance = $modal.open({
            templateUrl: '/Scripts/App/Insumos/Partials/Insumos_Seek.html',
            controller: 'seekCtrl',
            size: 'lg',
            windowClass: 'bs-example-modal-lg',
            resolve: {
            }
        });

        modalInstance.result.then(function (insumoSelect) {
            //alert("lado de ventaCtrl");
            //$scope.productoVta = [1];
            $scope.productoVta.CodProducto = insumoSelect.Id;
            $scope.productoVta.Descripcion = insumoSelect.Nombre;
            $scope.productoVta.stock = insumoSelect.CantStock;
            $scope.productoVta.unidad = insumoSelect.UMedida;
            $scope.productoVta.PrecioUnitario = insumoSelect.PrecioVenta;
        });

    };
    

    //iafar:para buscar despues de perder el focus del codigo
    $scope.busInsumoId = function () {
        
        
        //$scope.insumoFound = insumosDataFactory.get({ id: insumoId });
        //alert($scope.insumoFound.Nombre);
        alert($scope.productoVta.CodProducto);
        resultado = insumosDataFactory.get({ id: $scope.productoVta.CodProducto });
        alert(resultado.Nombre);
        $scope.productoVta = [];
        //$scope.productoVta.CodProducto = resultado.Id;
        $scope.productoVta.Descripcion = resultado.Nombre;
        
        $scope.productoVta.stock = resultado.CantStock;
        $scope.productoVta.unidad = resultado.UMedida;
        $scope.productoVta.PrecioUnitario = resultado.PrecioVenta;

        //alert($scope.productoVta.Descripcion)
        };
    //#endregion



});