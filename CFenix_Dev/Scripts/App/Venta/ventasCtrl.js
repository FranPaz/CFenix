copisteriaFenixApp.controller('ventasCtrl', function ($scope, $stateParams, $state) {
    var uid = 1

    $scope.detallesFactura; //fpaz: recive array de detalles de una factura en particular o se usar para ir cargando con los productos que se van agregando a la venta
    
    //iafar: array de detalle del producto a agregar a la factura
    $scope.productos = [];

    //iafar: Funcion para cargar el scope de detalle de productos
    $scope.addProducto = function () {
            alert('a ver');
            if ($scope.producto.id == null) {
                //if this is new product, add it in products array
                $scope.producto.id = uid++;
                $scope.productos.push($scope.producto);
            } else {
                //for existing product, find this contact using id
                //and update it.
                for (i in $scope.productos) {
                    if ($scope.productos[i].id == $scope.producto.id) {
                        $scope.productos[i] = $scope.producto;
                    }
                }
            }

        //clear the add contact form
            $scope.producto = {};
    };

    //iafar: borrar un producto del array de productos
    $scope.delete = function (id) {

        //search product with given id and delete it
        for (i in $scope.productos) {
            if ($scope.productos[i].id == id) {
                $scope.productos.splice(i, 1);
                $scope.producto = {};
            }
        }

    }

    //iafar: editar un producto del array de productos
    $scope.edit = function (id) {
        //search contact with given id and update it
        for (i in $scope.productos) {
            if ($scope.productos[i].id == id) {
                //we use angular.copy() method to create 
                //copy of original object in products
                $scope.producto = angular.copy($scope.productos[i]);
            }
        }
    }
    
    
    //iafar: controlador para el DTPicker
   
    $scope.today = function() {
        $scope.dt = new Date();
    };

    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
    $scope.disabled = function(date, mode) {
        return ( mode === 'day' && ( date.getDay() === 0 || date.getDay() === 6 ) );
    };

    $scope.toggleMin = function() {
        $scope.minDate = $scope.minDate ? null : new Date();
    };
    $scope.toggleMin();

    $scope.open = function($event) {
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
});

