copisteriaFenixApp.factory('insumosDataFactory', function ($resource) {
    return $resource('api/Insumos/:id',
           { id: '@id' },
           { 'update': { method: 'PUT' } }
        );
});