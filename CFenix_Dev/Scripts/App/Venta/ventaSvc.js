copisteriaFenixApp.service('ventaSvc', function () {
    
    //#region Cliente de la Venta
    var clienteVta = []; //variable global donde voy a guardar el cliente que encuentre en la busqueda y al que le estoy haciendo la venta
    var addClienteVta = function (newObj) {
        clienteVta.push(newObj);
          } 
    //#endregion

    //#region Productos Venta
    var detallesFacturaVta = []; //variable global donde voy a guardar los detalles de la factura de la venta
    var addDetallesFacturaVta = function (newObj) {
        detallesFacturaVta = newObj;
    }; //variable global donde voy a guardar el cliente que encuentre en la busqueda y al que le estoy haciendo la venta
    
    var limpiarVta = function () {
        clienteVta = [];
        detallesFacturaVta = [];
    }

    return {
        getCliente: function () {
            return clienteVta[0];
        },
        first: function () {
            return clienteVta[0];
        },
        addClienteVta: addClienteVta,
        addDetallesFacturaVta: addDetallesFacturaVta, //llama a la funcion de agregar un producto al array de detalle de factura
        listaDetallesFacturaVta: function () {//devuelve el array de detalles de factura de la venta
            return detallesFacturaVta;
        },
        limpiarVta: limpiarVta
    };
});