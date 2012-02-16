Feature: Track patients
	In order to avoid silly mistakes
	As a nurse
	I want to track patients

@domain
Scenario: Create a patient
	When I create a patient
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	Then the patient is created
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	And nothing else happens

