Feature: Intelligent Customer Routing

  As a customer service manager
  I want the system to intelligently route customers to the most appropriate agent
  So that customers receive the best possible support experience

  Background:
    Given the customer routing system is operational
    And there are available agents with different skill sets
    And customer profiles are accessible

  @priority-high @routing @ai
  Scenario: Route VIP customer to specialized agent
    Given a VIP customer "John Doe" with customer ID "123e4567-e89b-12d3-a456-426614174000"
    And the customer has a "Premium" segment classification
    And there are agents available with "VIP Support" skills
    When the customer initiates a chat session
    Then the system should route the customer to an agent with "VIP Support" skills
    And the routing should complete within 5 seconds
    And the agent should receive the customer's interaction history
    And the agent should be notified of the customer's VIP status

  @priority-high @routing @context
  Scenario: Route customer based on interaction history
    Given a customer "Jane Smith" with customer ID "456e7890-e89b-12d3-a456-426614174000"
    And the customer has previous interactions about "billing issues"
    And there are agents available with "Billing Support" skills
    When the customer initiates a phone call
    Then the system should route the customer to an agent with "Billing Support" skills
    And the agent should receive the customer's previous billing-related interactions
    And the estimated wait time should be provided to the customer

  @priority-medium @routing @language
  Scenario: Route customer based on language preference
    Given a customer "Carlos Rodriguez" with customer ID "789e1234-e89b-12d3-a456-426614174000"
    And the customer's preferred language is "Spanish"
    And there are agents available who speak "Spanish"
    When the customer initiates a chat session
    Then the system should route the customer to a Spanish-speaking agent
    And the chat interface should be displayed in Spanish
    And the agent should be notified of the customer's language preference

  @priority-medium @routing @overflow
  Scenario: Route to general queue when specialized agents unavailable
    Given a customer "Lisa Johnson" with customer ID "abc1234-e89b-12d3-a456-426614174000"
    And the customer has a technical support inquiry
    And there are no agents available with "Technical Support" skills
    And there are agents available in the general support queue
    When the customer initiates a chat session
    Then the system should route the customer to the general support queue
    And the customer should be informed about the expected wait time
    And the system should notify the customer when a technical specialist becomes available

  @priority-high @routing @ai-sentiment
  Scenario: Priority routing for frustrated customers
    Given a customer "Michael Brown" with customer ID "def5678-e89b-12d3-a456-426614174000"
    And the customer's previous interaction ended with a low satisfaction score
    And the customer's message contains negative sentiment indicators
    And there are senior agents available
    When the customer initiates a chat session
    Then the system should route the customer to a senior agent with priority
    And the agent should receive context about the customer's previous negative experience
    And the routing should bypass the normal queue

  @priority-medium @routing @multi-channel
  Scenario: Route customer transferring between channels
    Given a customer "Sarah Davis" with customer ID "ghi9012-e89b-12d3-a456-426614174000"
    And the customer started a chat session 10 minutes ago
    And the customer now initiates a phone call
    When the system processes the phone call
    Then the system should recognize the customer's ongoing chat session
    And route the call to the same agent if available
    And transfer the chat context to the phone conversation
    And mark the chat session as "transferred to voice"

  @priority-high @routing @business-hours
  Scenario: Route to after-hours support during off-hours
    Given a customer "David Wilson" with customer ID "jkl3456-e89b-12d3-a456-426614174000"
    And the current time is outside business hours
    And there are no regular agents available
    And there are after-hours support agents available
    When the customer initiates a chat session
    Then the system should route the customer to after-hours support
    And inform the customer about limited support capabilities
    And offer to schedule a callback during business hours

  @priority-high @routing @performance
  Scenario: Route customer to agent with shortest queue
    Given multiple customers are waiting for support
    And there are agents available with similar skill sets
    And agent "Agent A" has 2 customers in queue
    And agent "Agent B" has 0 customers in queue
    When a new customer initiates a chat session
    Then the system should route the customer to "Agent B"
    And the queue balance should be maintained
    And the routing decision should be logged for analysis

  @priority-low @routing @fallback
  Scenario: Provide self-service options when all agents busy
    Given a customer "Emma Taylor" with customer ID "mno7890-e89b-12d3-a456-426614174000"
    And all agents are currently busy
    And the estimated wait time is longer than 15 minutes
    When the customer initiates a chat session
    Then the system should offer self-service options
    And provide links to knowledge base articles
    And offer to schedule a callback
    And allow the customer to leave a message

  @priority-high @routing @error-handling
  Scenario: Handle routing system failure gracefully
    Given a customer "Robert Anderson" with customer ID "pqr1234-e89b-12d3-a456-426614174000"
    And the intelligent routing system is experiencing technical difficulties
    And there are agents available in the general queue
    When the customer initiates a chat session
    Then the system should route the customer to the general queue
    And log the routing system failure
    And notify system administrators
    And continue providing customer service with fallback routing
