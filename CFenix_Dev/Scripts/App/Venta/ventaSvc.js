copisteriaFenixApp.service('ventaSvc', function () {
    
    //#region Cliente de la Venta
    var clienteVta = []; //variable global donde voy a guardar el cliente que encuentre en la busqueda y al que le estoy haciendo la venta
    var addClienteVta = function (newObj) {
        clienteVta.push(newObj);
          } 
    //#endregion

    var totalVta = 0;
    var addTotalVta = function (prmSubTotal) {
        totalVta = prmSubTotal;
    }

    //#region Productos Venta    
    var detallesFacturaVta = []; //variable global donde voy a guardar los detalles de la factura de la venta
    var addDetallesFacturaVta = function (newObj) {        
        detallesFacturaVta.push(newObj);
    };
    
    var limpiarVta = function () {
        clienteVta = [];
        detallesFacturaVta = null;
        totalVta = 0;
    }

    return {
        getCliente: function () {
            return clienteVta[0];
        },
        getTotalVta: function () {
            return totalVta;
        },
        listaDetallesFacturaVta: function () {//devuelve el array de detalles de factura de la venta
            return detallesFacturaVta;
        },
        limpiarVta: limpiarVta,

        addClienteVta: addClienteVta,
        addTotalVta:addTotalVta,
        addDetallesFacturaVta: addDetallesFacturaVta, //llama a la funcion de agregar un producto al array de detalle de factura
        
    };
});