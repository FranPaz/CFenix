copisteriaFenixApp.factory('pagosDataFactory', function ($resource) {
    return $resource('api/Pagos/:id',
           { id: '@id' },
           { 'update': { method: 'PUT' } }
        );
});