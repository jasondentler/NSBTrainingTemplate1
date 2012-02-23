/// <reference path="../lib/jquery.js" />
/// <reference path="../lib/knockout.js" />
/// <reference path="../lib/knockout-mapping.js" />
/// <reference path="../app/hospital.js" />
hubInitializers.push(function () {

    function Patient(PatientId, FirstName, LastName, BedAssignment, IsAdmitted, IsDischarged) {
        this.PatientId = PatientId;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.BedAssignment = BedAssignment;
        this.IsAdmitted = IsAdmitted;
        this.IsDischarged = IsDischarged;
    }

    var viewModel = ko.mapping.fromJS(page.model);

    var makePatientObservable = function (idx, patient) {
        patient.LastFirst = ko.computed(function () {
            return this.LastName() + ', ' + this.FirstName();
        }, patient);

        patient.Status = ko.computed(function () {
            if (this.IsDischarged()) return "Discharged";
            if (!this.IsAdmitted()) return "Not Admitted";
            if (this.BedAssignment() != null) return "Bed #" + this.BedAssignment();
            return "Admitted";
        }, patient);

        patient.EditUrl = ko.computed(function () {
            return page.baseEditUrl + '/' + this.PatientId();
        }, patient);
    };

    var findPatient = function (patientId) {
        var patients = viewModel.model();
        for (var i = 0; i < patients.length; i++) {
            if (patients[i].PatientId() == patientId)
                return patients[0];
        }
        throw "Patient Id " + patientId + " wasn't in the view model.";
    };

    $.each(viewModel.model(), makePatientObservable);

    ko.applyBindings(viewModel, $('#view')[0]);

    var patientsHub = $.connection.patientsHub;

    patientsHub.patientCreated = function (e) {
        console.log('patientsHub.patientAdmitted');
        console.log(e);
        var patient = new Patient(e.PatientId, e.FirstName, e.LastName, null, false, false);
        patient = ko.mapping.fromJS(patient);
        console.log(patient);
        makePatientObservable(0, patient);
        console.log(patient);
        viewModel.model().push(patient);
        console.log("" + viewModel.model().length + " patients");
    };

    patientsHub.patientAdmitted = function (e) {
        console.log('patientsHub.patientAdmitted');
        console.log(e);
        var patient = findPatient(e.PatientId);
        console.log(patient);
        patient.IsAdmitted(true);
    };

    patientsHub.bedAssigned = function (e) {
        console.log('patientsHub.bedAssigned');
        console.log(e);
        var patient = findPatient(e.PatientId);
        console.log(patient);
        patient.BedAssignment(e.Bed);
    };

    patientsHub.patientMoved = function (e) {
        console.log('patientsHub.patientMoved');
        console.log(e);
        var patient = findPatient(e.PatientId);
        console.log(patient);
        patient.BedAssignment(e.ToBed);
    };

    patientsHub.patientDischarged = function (e) {
        console.log('patientsHub.patientDischarged');
        console.log(e);
        var patient = findPatient(e.PatientId);
        patient.BedAssignment(null);
        console.log(patient);
        patient.IsDischarged(true);
    };

});
