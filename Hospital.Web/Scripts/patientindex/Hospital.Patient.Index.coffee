# @reference ~/Scripts/lib/jquery.js
# @reference ~/Scripts/lib/jquery-ui.js
# @reference ~/Scripts/lib/knockout.js
# @reference ~/Scripts/lib/knockout-mapping.js
# @reference ~/Scripts/lib/jquery.signalR.js
# @reference ~/signalrhubs
# @reference ~/Scripts/app/hospital.js
# @reference ~/Scripts/commands/hospital.commands.coffee

hubInitializers.push -> 
	Patient = (PatientId, FirstName = "", LastName = "", BedAssignment = null, IsAdmitted = false, IsDischarged = false) ->
		this.PatientId = PatientId
		this.FirstName = FirstName
		this.LastName = LastName
		this.BedAssignment = BedAssignment
		this.IsAdmitted = IsAdmitted
		this.IsDischarged = IsDischarged

	viewModel = ko.mapping.fromJS page.model

	makePatientObservable = (idx, patient) ->
		lastFirst = ->
			return this.LastName() + ', ' + this.FirstName()

		status = ->
			return "Discharged" if this.IsDischarged()
			return "Not Admitted" if !this.IsAdmitted()
			return "Bed #" + this.BedAssignment() if this.BedAssignment() != null
			return "Admitted"

		editUrl = ->
			return page.baseEditUrl + "/" + this.PatientId()

		patient.LastFirst = ko.computed lastFirst, patient
		patient.Status = ko.computed status, patient
		patient.EditUrl = ko.computed editUrl, patient

	findPatient = (patientId) ->
		patients = viewModel.model()
		foundPatient = (patient for patient in patients when patient.PatientId() == patientId)
		return foundPatient[0] if foundPatient? && foundPatient.length
		throw "Patient Id " + patientId + " wasn't in the view model."

	$.each viewModel.model(), makePatientObservable

	ko.applyBindings viewModel, $('#view')[0]
	
	patientsHub = $.connection.patientsHub;

	patientsHub.patientCreated = (e) ->
		console.log "patientsHub.patientCreated"
		return

	patientsHub.patientAdmitted = (e) ->
		console.log "patientsHub.patientAdmitted"
		return

	patientsHub.bedAssigned = (e) ->
		console.log "patientsHub.bedAssigned"
		return

	patientsHub.patientMoved = (e) ->
		console.log "patientsHub.patientMoved"
		return
	
	patientsHub.patientDischarged = (e) ->
		console.log "patientsHub.patientDischarged"
		return

	$("#createPatientLink").click ->
		$("#createPatientDialog").dialog
			modal: true
			resizable: false
			buttons: 
				"Create Patient": ->
					window.commandHub.createPatient createUUID(), $("#firstName").val(), $("#lastName").val()
					$(this).dialog "close"
				"Cancel": ->
					$(this).dialog "close"
		return
	return