var hubInitializers = [];

$(function () {
    $.each(hubInitializers, function (idx, callback) {
        console.log('Calling hubInitializer callback #' + idx);
        callback();
    });

    // Start the connection
    $.connection.hub.start(function () {
        console.log('Connected');
    });
    console.log('Connecting');
});