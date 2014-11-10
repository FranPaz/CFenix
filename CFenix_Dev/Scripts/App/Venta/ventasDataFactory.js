copisteriaFenixApp.factory('ventasDataFactory', function ($resource) {
    return $resource('api/Ventas/:id',
           { id: '@id' },
           { 'update': { method: 'PUT' } }
        );
});