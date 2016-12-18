Feature: Incoming NodeInfo messages
	In order to communicate various data about itself
	As a home gateway node
	I want to send a NodeInfoReport message

Scenario: Unknown node gets created
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
