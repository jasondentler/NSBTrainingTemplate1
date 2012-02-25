/// <reference path="~/Scripts/lib/jquery.js" />
/// <reference path="~/Scripts/lib/knockout.js" />
/// <reference path="~/Scripts/lib/knockout-mapping.js" />
/// <reference path="~/Scripts/lib/jquery.signalR.js" />
/// <reference path="~/signalrhubs" />
/// <reference path="~/Scripts/app/hospital.js" />
hubInitializers.push(function () {

    $("#admittedPatientCount").attr('data-bind', 'text: AdmittedPatients()');
    $("#waitingForBedCount").attr('data-bind', 'text: WaitingForBeds()');
    $("#availableBedsCount").attr('data-bind', 'text: AvailableBeds()');

    var viewModel = ko.mapping.fromJS(stats.model);

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
