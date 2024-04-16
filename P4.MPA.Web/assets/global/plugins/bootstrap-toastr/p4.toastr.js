var p4 = p4 || {};
(function () {

    if (!toastr) {
        return;
    }

    /* DEFAULTS *************************************************/

    //toastr.options.positionClass = 'toast-bottom-right';

    /* NOTIFICATION *********************************************/

    var showNotification = function (type, message, title) {
        toastr[type](message, title);
    };

    NotifySuccess = function (message, title) {
        showNotification('success', message, title);
    };

    NotifyInfo = function (message, title) {
        showNotification('info', message, title);
    };

    NotifyWarn = function (message, title) {
        showNotification('warning', message, title);
    };

    NotifyError = function (message, title) {
        showNotification('error', message, title);
    };

})();