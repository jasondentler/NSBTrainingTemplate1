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

@domain
Scenario: Admit a patient
	Given I have created a patient 
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	When I admit the patient
	Then the patient is admitted
	And nothing else happens

@domain
Scenario: Admit an admitted patient
	Given I have created a patient 
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	And I have admitted the patient
	When I admit the patient
	Then nothing happens

@domain
Scenario: Assign a patient to a bed
	Given I have created a patient 
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	And I have admitted the patient
	When I assign the patient to a bed
	| Field | Value |
	| Bed   | 2     |
	Then the patient is assigned to a bed
	| Field | Value |
	| Bed   | 2     |
	And nothing else happens

@domain
Scenario: Move a patient to another bed
	Given I have created a patient 
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	And I have admitted the patient
	And I have assigned the patient to a bed
	| Field | Value |
	| Bed   | 2     |
	When I assign the patient to a bed
	| Field | Value |
	| Bed   | 3     |
	Then the patient is moved 
	| Field    | Value |
	| From Bed | 2     |
	| To Bed   | 3     |
	And nothing else happens

@domain
Scenario: Assign an unadmitted patient to a bed
	Given I have created a patient 
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	And I have admitted the patient
	When I assign the patient to a bed
	| Field | Value |
	| Bed   | 2     |
	Then error: The patient can't be assigned to a bed until admitted

@domain
Scenario: Discharge a patient
	Given I have created a patient 
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	And I have admitted the patient
	When I discharge the patient
	Then the patient is discharged
	And nothing else happens

@domain
Scenario: Discharge an unadmitted patient
	Given I have created a patient 
	| Field      | Value |
	| First Name | Red   |
	| Last Name  | Shirt |
	When I discharge the patient
	Then error: The patient can't be discharged without being admitted

