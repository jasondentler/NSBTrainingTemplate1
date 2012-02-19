/// <reference path="jquery-1.6.2.js" />
/// <reference path="jquery.signalR.js" />
/// <reference path="knockout-2.0.0.js" />
/// <reference path="knockout-mapping-2.0.0.js" />

hubInitializers.push(function () {

    $("#admittedPatientCount").attr('data-bind', 'text: AdmittedPatients()');
    $("#waitingForBedCount").attr('data-bind', 'text: WaitingForBeds()');
    $("#availableBedsCount").attr('data-bind', 'text: AvailableBeds()');
    console.log('Added data-bind attributes');

    var viewModel = ko.mapping.fromJS(statsModel);
    console.log('Created observable view model');

    ko.applyBindings(viewModel, $('#stats')[0]);
    console.log('Bound');

    var statsHub = $.connection.statsHub;
    console.log('Got hub');

    statsHub.patientAdmitted = function () {
        console.log('patientAdmitted');
        viewModel.AdmittedPatients(viewModel.AdmittedPatients() + 1);
        viewModel.WaitingForBeds(viewModel.WaitingForBeds() + 1);
    };

    statsHub.bedAssigned = function () {
        console.log('bedAssigned');
        viewModel.WaitingForBeds(viewModel.WaitingForBeds() - 1);
        viewModel.AvailableBeds(viewModel.AvailableBeds() - 1);
    };

    statsHub.patientDischarged = function () {
        console.log('patientDischarged');
        viewModel.AdmittedPatients(viewModel.AdmittedPatients() - 1);
        viewModel.AvailableBeds(viewModel.AvailableBeds() + 1);
    };

    console.log('Set up hub event handlers');

});
