copisteriaFenixApp.factory('comprasDataFactory', function ($resource) {
    return $resource('api/Compras/:id',
           { id: '@id' },
           { 'update': { method: 'PUT' } }
        );
});