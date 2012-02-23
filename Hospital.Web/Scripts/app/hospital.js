/// <reference path="../lib/jquery.js" />
/// <reference path="../lib/jquery.signalR.js" />
/// <reference path="../lib/knockout.js" />
/// <reference path="../lib/knockout-mapping.js" />
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