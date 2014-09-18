//Funciones de Login

copisteriaFenixApp.controller('HeaderCtrl', ['$scope', 'User', function ($scope, User) {
    $scope.user = User.getUserData();
}]);

copisteriaFenixApp.controller('LogoutCtrl', ['$state', 'User', function ($state, User) {
    User.removeAuthentication();
    $state.go('login');
}]);

copisteriaFenixApp.controller('signupController', ['$scope', '$location', '$timeout', 'User', function ($scope, $location, $timeout, User) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };

    $scope.signUp = function () {

        User.saveRegistration($scope.registration).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
            startTimer();

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}]);

copisteriaFenixApp.controller('LoginCtrl', ['$scope', '$state', 'User', function ($scope, $state, User) {
    $scope.username = '';
    $scope.password = '';
    $scope.persist = true;//rsanch true para no desloguearse cuando actualiza
    $scope.errors = [];
    var nextState = null;

    try {
        nextState = User.getNextState();
    } catch (e) {
        nextState = null;
    }

    if(nextState !== null) {
        var nameBuffer = nextState.name + '';
        var errorBuffer = nextState.error + '';
        User.clearNextState();
        nextState = {
            name: nameBuffer,
            error: errorBuffer
        };
        if (typeof nextState.error === 'string' && nextState.error !== '' && $scope.errors.indexOf(nextState.error) === -1) {
            $scope.errors.push(nextState.error);
        } else {
            $scope.errors.push('Debes iniciar sesion para ver esta pagina');
        }
    }

    function disableLoginButton(message) {
        if (typeof message !== 'string') {
            message = 'Iniciando Sesion...';
        }
        jQuery('#login-form-submit-button').prop('disabled', true).prop('value', message);
    }

    function enableLoginButton(message) {
        if (typeof message !== 'string') {
            message = 'Submit';
        }
        jQuery('#login-form-submit-button').prop('disabled', false).prop('value', message);
    }

    function onSuccessfulLogin() {
        if(nextState !== null && typeof nextState.name === 'string' && nextState.name !== '') {
            $state.go(nextState.name, nextState.params);
        } else {
            $state.go('cuentasCorrientes');//ir al state Main
        }
    }

    function onFailedLogin(error) {
        if (typeof error === 'string' && $scope.errors.indexOf(error) === -1) {
            $scope.errors.push(error);
        }
        enableLoginButton();
    }

    $scope.login = function() {
        disableLoginButton();
        User.authenticate($scope.username, $scope.password, onSuccessfulLogin, onFailedLogin, $scope.persist);
    };
}]);
