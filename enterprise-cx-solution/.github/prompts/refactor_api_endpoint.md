# Refactor API Endpoint

## Purpose
Refactor existing API endpoints to follow best practices for performance, security, and maintainability.

## Usage
Use this prompt when you need to:
- Improve API performance
- Enhance security measures
- Implement better error handling
- Follow RESTful design principles
- Add proper validation and sanitization

## Prompt Template

```
#prompt:refactor_api_endpoint

Please refactor the following API endpoint to follow best practices:

[PASTE CODE HERE]

Refactoring goals:
- Performance: Optimize database queries and caching
- Security: Implement proper authentication and authorization
- Validation: Add comprehensive input validation
- Error handling: Implement proper error responses with appropriate HTTP status codes
- Documentation: Add OpenAPI/Swagger documentation
- Logging: Include structured logging for monitoring
- Testing: Ensure testability with dependency injection

Architecture patterns to consider:
- Repository pattern for data access
- Service layer for business logic
- DTO/Model separation
- Async/await for I/O operations
- Circuit breaker pattern for external calls

Standards to follow:
- RESTful API design principles
- HTTP status codes best practices
- JSON response formatting
- Rate limiting and throttling
- CORS configuration
- API versioning strategy
```

## Example Usage

```markdown
#prompt:refactor_api_endpoint

Please refactor this customer profile API endpoint to improve performance and security:

[Include your code here]

Focus on implementing caching, proper error handling, and input validation.
```
