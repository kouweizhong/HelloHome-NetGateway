Feature: Node Startup
	In order to communicate with the gateway
	As a hello home node
	I need to be recognised and uniquely identified

Scenario: New node starting
	Given that my unique signature is 1234
	And that I start for the first time
	And that my rfId is 4
	And that my rfId is not yet known by the gateway
	When I start
	Then A new node is created with my signature and rfId 4
	And No new config message is sent back

Scenario: New node starting with no rfId
	Given that my unique signature is 1234
	And that I start for the first time
	And I need a new rfId
	When I start
	Then A new node is created with my signature
	And A new config message is sent back witht the new rfId

Scenario: Exisiting node restart
	Given that my unique signature is 1234 
	And that my rfId is 2
	And that I'm already known by the gateway
	When I start
	Then Last startup time is updated
	And No new config message is sent back
