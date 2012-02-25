# @reference ~/Scripts/lib/jquery.js
# @reference ~/Scripts/lib/knockout.js
# @reference ~/Scripts/lib/knockout-mapping.js
# @reference ~/Scripts/lib/jquery.signalR.js
# @reference ~/signalrhubs
# @reference ~/Scripts/app/hospital.js

hubInitializers.push -> 
	$("#admittedPatientCount").attr "data-bind", "text: AdmittedPatients()"
	$("#waitingForBedCount").attr "data-bind", "text: WaitingForBeds()"
	$("#availableBedsCount").attr "data-bind", "text: AvailableBeds()"

	viewModel = ko.mapping.fromJS stats.model

	ko.applyBindings viewModel, $('#stats')[0]

	statsHub = $.connection.statsHub

	statsHub.patientAdmitted = -> 
		console.log "patient admitted"
		return

	statsHub.bedAssigned = ->
		console.log "bed assigned"
		return

	statsHub.patientDischarged = ->
		console.log "patient discharged"
		return

	return