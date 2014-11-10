var urlBase = 'http://cfenix.azurewebsites.net/'
//copisteriaFenixApp.factory('clientesDataFactory', function ($resource) {
//    return $resource( urlBase +
//           'api/Clientes/:id',
//           { id: '@id' },
//           { 'update': { method: 'PUT' } }
//        );
//});
copisteriaFenixApp.factory('ventasDataFactory', function ($resource) {
    return $resource('api/Ventas/:id',
           { id: '@id' },
           { 'update': { method: 'PUT' } }
        );
});