var urlBase = 'http://cfenix.azurewebsites.net/'
//copisteriaFenixApp.factory('clientesDataFactory', function ($resource) {
//    return $resource( urlBase +
//           'api/Clientes/:id',
//           { id: '@id' },
//           { 'update': { method: 'PUT' } }
//        );
//});
copisteriaFenixApp.factory('deudasDataFactory', function ($resource) {
    return $resource('api/Deudas/:id',
           { id: '@id' },
           { 'update': { method: 'PUT' } }
        );
});