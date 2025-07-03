# Generate Unit Tests

## Purpose
Generate comprehensive unit tests for new features or existing code that lacks test coverage.

## Usage
Use this prompt when you need to create unit tests for:
- New API endpoints
- Business logic functions
- Data access layer methods
- Service layer implementations

## Prompt Template

```
#prompt:generate_unit_tests

Please generate comprehensive unit tests for the following code:

[PASTE CODE HERE]

Requirements:
- Test coverage: Aim for 90%+ code coverage
- Test scenarios: Include happy path, edge cases, and error conditions
- Mocking: Mock external dependencies appropriately
- Assertions: Use descriptive assertions with clear error messages
- Test data: Include representative test data and fixtures
- Documentation: Add comments explaining complex test scenarios

Framework preferences:
- .NET: Use xUnit with Moq for mocking
- Python: Use pytest with unittest.mock
- Node.js: Use Jest with appropriate mocking libraries
- Java: Use JUnit 5 with Mockito

Additional considerations:
- Include integration tests where appropriate
- Test async operations properly
- Consider security implications in tests
- Include performance tests for critical paths
```

## Example Usage

```markdown
#prompt:generate_unit_tests

Please generate comprehensive unit tests for the following customer service API endpoint:

[Include your code here]

Focus on testing authentication, authorization, input validation, and error handling.
```
