//var urlBase = 'http://cfenix.azurewebsites.net/'
//urlBase +

copisteriaFenixApp.factory('trabajosDataFactory', function ($resource) {
    return $resource('api/Trabajos/:id',
           { id: '@id' },
           { 'update': { method: 'PUT' } }
        );
});