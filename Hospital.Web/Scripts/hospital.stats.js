/// <reference path="jquery-1.6.2.js" />
/// <reference path="jquery.signalR.js" />
/// <reference path="knockout-2.0.0.js" />
/// <reference path="knockout-mapping-2.0.0.js" />
/// <reference path="hospital.js"/>

hubInitializers.push(function () {

    $("#admittedPatientCount").attr('data-bind', 'text: AdmittedPatients()');
    $("#waitingForBedCount").attr('data-bind', 'text: WaitingForBeds()');
    $("#availableBedsCount").attr('data-bind', 'text: AvailableBeds()');

    var viewModel = ko.mapping.fromJS(statsModel);

    ko.applyBindings(viewModel, $('#stats')[0]);

    var statsHub = $.connection.statsHub;

    statsHub.patientAdmitted = function () {
        console.log('statsHub.patientAdmitted');
        viewModel.AdmittedPatients(viewModel.AdmittedPatients() + 1);
        viewModel.WaitingForBeds(viewModel.WaitingForBeds() + 1);
    };

    statsHub.bedAssigned = function () {
        console.log('statsHub.bedAssigned');
        viewModel.WaitingForBeds(viewModel.WaitingForBeds() - 1);
        viewModel.AvailableBeds(viewModel.AvailableBeds() - 1);
    };

    statsHub.patientDischarged = function () {
        console.log('statsHub.patientDischarged');
        viewModel.AdmittedPatients(viewModel.AdmittedPatients() - 1);
        viewModel.AvailableBeds(viewModel.AvailableBeds() + 1);
    };

});
