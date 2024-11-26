Feature: GetValidationFeature

As a ReportPortal user
I want to have my dashboard protected
So that I want to call a single endpoint that will return my dashboard only for me

Scenario Outline: Check Get Dashboard With Invalid Id
	Given a request with authorization
	And the request has path params:
	| name | value      |
	| id   | <id_value> |
	When the 'Get' request is sent to '/api/v1/{user}/dashboard/{id}' endpoint
	Then  the response status code is <status_code>
	And the response body is equal to '<error_message>'
	Examples: 
	| id_value | status_code | error_message                     |
	| invalid  | BadRequest  | Bad Request                       |
	| id       | BadRequest  | Bad Request                       |
	| 17       | NotFound    | Did you use correct project name? |
	| 7&??123  | BadRequest  | Bad Request                       |
	| __       | BadRequest  | Bad Request                       |


	Scenario Outline: Check Get Board With Invalid Auth
	Given a request without authorization
	And the request has path params:
	| name | value                    |
	| id   | 670f8a5af3ffd8c7569ebc75 |
	And the request has header:
	| name          | value |
	| Authorization | <key> |
	When the 'Get' request is sent to '/api/v1/{user}/dashboard/{id}' endpoint
	Then the response status code is Unauthorized
	And the response body is equal to '<error_message>'
	Examples: 
	| key                                       | error_message |
	| Bearer invalid key                        | invalid_token |
	| Bearer 12345                              | invalid_token |
	|                                           | unauthorized  |
	| Bearer ReportPortalKey_WYzjrJxLSqafC66glg | invalid_token |
	| Bearer                                    | invalid_token |
