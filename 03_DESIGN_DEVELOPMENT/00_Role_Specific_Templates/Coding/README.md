# Coding Documentation Templates

## Overview
Coding templates optimized for both AI assistance and human workflow. These templates provide structured approaches to code development, code review, and coding standards while enabling AI to understand context and generate relevant code, documentation, and development practices.

## AI-Optimized Coding Prompts

### Primary Coding Prompt Template
```markdown
@copilot I need to implement [FEATURE/FUNCTION] for [SYSTEM/COMPONENT].

Context:
- Programming language: [LANGUAGE]
- Framework: [FRAMEWORK]
- Requirements: [FUNCTIONAL_REQUIREMENTS]
- Constraints: [TECHNICAL_CONSTRAINTS]
- Existing code: [RELEVANT_CODE_CONTEXT]

Please generate [DELIVERABLE_TYPE] following coding best practices.
```

### Specific Coding Prompt Templates

#### Code Generation
```markdown
@copilot Generate [COMPONENT_TYPE] for [FEATURE_NAME].

Context:
- Language/Framework: [TECH_STACK]
- Design patterns: [PREFERRED_PATTERNS]
- Requirements: [DETAILED_REQUIREMENTS]
- Integration points: [API_DATABASE_INTEGRATIONS]
- Error handling: [ERROR_HANDLING_STRATEGY]

Include: Complete implementation, unit tests, documentation, and error handling.
```

#### Code Review
```markdown
@copilot Review this code for [REVIEW_FOCUS].

Context:
- Code purpose: [WHAT_CODE_DOES]
- Quality criteria: [QUALITY_STANDARDS]
- Performance requirements: [PERFORMANCE_NEEDS]
- Security considerations: [SECURITY_REQUIREMENTS]
- Maintainability goals: [MAINTAINABILITY_STANDARDS]

Please provide: Issues found, improvement suggestions, and best practice recommendations.
```

#### Refactoring
```markdown
@copilot Refactor this code to improve [REFACTORING_GOAL].

Context:
- Current implementation: [EXISTING_CODE]
- Issues to address: [SPECIFIC_PROBLEMS]
- Constraints: [REFACTORING_CONSTRAINTS]
- Target architecture: [DESIRED_ARCHITECTURE]
- Testing requirements: [TEST_COVERAGE_NEEDS]

Include: Refactored code, migration plan, and updated tests.
```

## Template Documents

### 1. Coding Standards Template
Comprehensive coding standards documentation:

```markdown
# Coding Standards: [Project/Organization Name]

## Document Information
- **Version**: [Version number]
- **Author**: [Author name]
- **Date**: [Creation date]
- **Status**: [Draft/Review/Approved]
- **Scope**: [Applicable projects/teams]

## General Principles

### Code Quality Principles
- **Readability**: Code should be self-documenting and easy to understand
- **Maintainability**: Code should be easy to modify and extend
- **Reliability**: Code should be robust and handle errors gracefully
- **Performance**: Code should be efficient and scalable
- **Security**: Code should follow security best practices

### SOLID Principles
- **Single Responsibility**: Each class should have one reason to change
- **Open/Closed**: Open for extension, closed for modification
- **Liskov Substitution**: Derived classes must be substitutable for base classes
- **Interface Segregation**: Clients should not depend on unused interfaces
- **Dependency Inversion**: Depend on abstractions, not concretions

## Language-Specific Standards

### [Primary Language] Standards
#### Naming Conventions
- **Variables**: [Naming pattern and examples]
- **Functions**: [Naming pattern and examples]
- **Classes**: [Naming pattern and examples]
- **Constants**: [Naming pattern and examples]
- **Files**: [Naming pattern and examples]

#### Code Structure
- **File Organization**: [How to organize code within files]
- **Class Structure**: [Order of class members]
- **Function Length**: [Maximum function length]
- **Indentation**: [Indentation style and size]
- **Line Length**: [Maximum line length]

#### Comments and Documentation
- **Inline Comments**: [When and how to use]
- **Function Documentation**: [Documentation format]
- **Class Documentation**: [Documentation requirements]
- **API Documentation**: [API documentation standards]

### Example Code Standards

#### Variable Naming
```python
# Good examples
user_count = 10
is_active = True
max_retry_attempts = 3

# Bad examples
uc = 10
flag = True
n = 3
```

#### Function Structure
```python
def calculate_user_score(user_id: int, metrics: Dict[str, float]) -> float:
    """
    Calculate user score based on provided metrics.
    
    Args:
        user_id: Unique identifier for the user
        metrics: Dictionary of metric names and values
        
    Returns:
        Calculated score as float
        
    Raises:
        ValueError: If user_id is invalid
        KeyError: If required metrics are missing
    """
    if user_id <= 0:
        raise ValueError("User ID must be positive")
    
    required_metrics = ['engagement', 'performance', 'satisfaction']
    if not all(metric in metrics for metric in required_metrics):
        raise KeyError("Missing required metrics")
    
    # Calculate weighted score
    weights = {'engagement': 0.4, 'performance': 0.4, 'satisfaction': 0.2}
    score = sum(metrics[metric] * weights[metric] for metric in required_metrics)
    
    return min(max(score, 0.0), 100.0)  # Clamp between 0 and 100
```

#### Class Structure
```python
class UserService:
    """Service for managing user operations."""
    
    # Class constants
    MAX_USERS_PER_PAGE = 50
    
    def __init__(self, database: Database):
        """Initialize UserService with database connection."""
        self._database = database
        self._cache = {}
    
    # Public methods
    def get_user(self, user_id: int) -> Optional[User]:
        """Retrieve user by ID."""
        # Implementation
        pass
    
    def create_user(self, user_data: Dict[str, Any]) -> User:
        """Create new user."""
        # Implementation
        pass
    
    # Private methods
    def _validate_user_data(self, user_data: Dict[str, Any]) -> None:
        """Validate user data before creation."""
        # Implementation
        pass
```

## Code Review Standards

### Code Review Process
1. **Pre-Review**: [Self-review checklist]
2. **Review Assignment**: [How reviewers are assigned]
3. **Review Execution**: [Review procedures]
4. **Feedback Integration**: [How to handle feedback]
5. **Approval**: [Approval criteria]

### Review Checklist
#### Functionality
- [ ] Code meets requirements
- [ ] Edge cases handled
- [ ] Error conditions managed
- [ ] Performance considerations addressed

#### Code Quality
- [ ] Follows coding standards
- [ ] Proper naming conventions
- [ ] Appropriate comments
- [ ] No code duplication

#### Testing
- [ ] Unit tests included
- [ ] Test coverage adequate
- [ ] Tests are meaningful
- [ ] Mock objects used appropriately

#### Security
- [ ] Input validation present
- [ ] Authentication checks
- [ ] Authorization implemented
- [ ] No sensitive data exposed

### Review Template
```markdown
## Code Review: [Feature/PR Title]

### Summary
[Brief description of changes]

### Review Criteria
- **Functionality**: [Pass/Fail - Comments]
- **Code Quality**: [Pass/Fail - Comments]
- **Testing**: [Pass/Fail - Comments]
- **Security**: [Pass/Fail - Comments]
- **Performance**: [Pass/Fail - Comments]

### Detailed Comments
#### Major Issues
- [Issue 1]: [Description and suggestion]
- [Issue 2]: [Description and suggestion]

#### Minor Issues
- [Issue 1]: [Description and suggestion]
- [Issue 2]: [Description and suggestion]

#### Suggestions
- [Suggestion 1]: [Description and rationale]
- [Suggestion 2]: [Description and rationale]

### Approval Status
- [ ] Approved
- [ ] Approved with minor changes
- [ ] Requires major changes
- [ ] Rejected

### Reviewer: [Name]
### Date: [Review date]
```

## Testing Standards

### Unit Testing
#### Testing Framework
- **Framework**: [Testing framework choice]
- **Test Structure**: [Test organization]
- **Naming Convention**: [Test naming standards]
- **Coverage Target**: [Minimum coverage percentage]

#### Test Structure
```python
import unittest
from unittest.mock import Mock, patch

class TestUserService(unittest.TestCase):
    """Test cases for UserService class."""
    
    def setUp(self):
        """Set up test fixtures."""
        self.mock_database = Mock()
        self.user_service = UserService(self.mock_database)
    
    def test_get_user_success(self):
        """Test successful user retrieval."""
        # Arrange
        user_id = 1
        expected_user = User(id=1, name="John Doe")
        self.mock_database.get_user.return_value = expected_user
        
        # Act
        result = self.user_service.get_user(user_id)
        
        # Assert
        self.assertEqual(result, expected_user)
        self.mock_database.get_user.assert_called_once_with(user_id)
    
    def test_get_user_not_found(self):
        """Test user not found scenario."""
        # Arrange
        user_id = 999
        self.mock_database.get_user.return_value = None
        
        # Act
        result = self.user_service.get_user(user_id)
        
        # Assert
        self.assertIsNone(result)
    
    def test_create_user_invalid_data(self):
        """Test user creation with invalid data."""
        # Arrange
        invalid_data = {"name": ""}
        
        # Act & Assert
        with self.assertRaises(ValueError):
            self.user_service.create_user(invalid_data)
```

### Integration Testing
#### Integration Test Structure
- **Test Environment**: [Test environment setup]
- **Test Data**: [Test data management]
- **External Dependencies**: [Mock vs real dependencies]
- **Test Isolation**: [How to isolate tests]

## Error Handling Standards

### Error Handling Principles
- **Fail Fast**: Detect and report errors early
- **Graceful Degradation**: Provide fallback functionality
- **Logging**: Comprehensive error logging
- **User Experience**: User-friendly error messages

### Exception Handling
```python
import logging
from typing import Optional

logger = logging.getLogger(__name__)

class UserNotFoundError(Exception):
    """Raised when user is not found."""
    pass

class UserService:
    def get_user(self, user_id: int) -> Optional[User]:
        """
        Retrieve user by ID with proper error handling.
        
        Args:
            user_id: User identifier
            
        Returns:
            User object or None if not found
            
        Raises:
            ValueError: If user_id is invalid
            DatabaseError: If database operation fails
        """
        try:
            # Validate input
            if not isinstance(user_id, int) or user_id <= 0:
                raise ValueError(f"Invalid user ID: {user_id}")
            
            # Attempt database operation
            user = self._database.get_user(user_id)
            
            if user is None:
                logger.info(f"User not found: {user_id}")
                return None
            
            logger.debug(f"User retrieved successfully: {user_id}")
            return user
            
        except DatabaseConnectionError as e:
            logger.error(f"Database connection failed: {e}")
            raise DatabaseError("Unable to retrieve user") from e
        except Exception as e:
            logger.error(f"Unexpected error retrieving user {user_id}: {e}")
            raise
```

## Performance Standards

### Performance Guidelines
- **Algorithmic Complexity**: [Time/space complexity requirements]
- **Database Queries**: [Query optimization guidelines]
- **Caching**: [Caching strategies]
- **Memory Management**: [Memory usage guidelines]

### Performance Testing
```python
import time
import pytest

def test_user_service_performance():
    """Test UserService performance requirements."""
    user_service = UserService(database)
    
    # Test response time
    start_time = time.time()
    user = user_service.get_user(1)
    end_time = time.time()
    
    response_time = end_time - start_time
    assert response_time < 0.1, f"Response time {response_time} exceeds limit"
    
    # Test bulk operations
    user_ids = list(range(1, 1001))
    start_time = time.time()
    users = user_service.get_users_bulk(user_ids)
    end_time = time.time()
    
    bulk_time = end_time - start_time
    assert bulk_time < 1.0, f"Bulk operation time {bulk_time} exceeds limit"
```

## Security Standards

### Security Guidelines
- **Input Validation**: [Validation requirements]
- **Authentication**: [Authentication standards]
- **Authorization**: [Authorization patterns]
- **Data Protection**: [Data security measures]

### Security Implementation
```python
import hashlib
import secrets
from functools import wraps

class SecurityService:
    @staticmethod
    def hash_password(password: str) -> tuple[str, str]:
        """Hash password with salt."""
        salt = secrets.token_hex(16)
        password_hash = hashlib.pbkdf2_hmac(
            'sha256',
            password.encode('utf-8'),
            salt.encode('utf-8'),
            100000
        )
        return password_hash.hex(), salt
    
    @staticmethod
    def verify_password(password: str, hash_value: str, salt: str) -> bool:
        """Verify password against hash."""
        password_hash = hashlib.pbkdf2_hmac(
            'sha256',
            password.encode('utf-8'),
            salt.encode('utf-8'),
            100000
        )
        return password_hash.hex() == hash_value

def require_auth(f):
    """Decorator to require authentication."""
    @wraps(f)
    def decorated_function(*args, **kwargs):
        if not current_user.is_authenticated:
            raise AuthenticationError("Authentication required")
        return f(*args, **kwargs)
    return decorated_function

def require_permission(permission):
    """Decorator to require specific permission."""
    def decorator(f):
        @wraps(f)
        def decorated_function(*args, **kwargs):
            if not current_user.has_permission(permission):
                raise AuthorizationError(f"Permission required: {permission}")
            return f(*args, **kwargs)
        return decorated_function
    return decorator
```

## Documentation Standards

### Code Documentation
- **Module Documentation**: [Module-level documentation]
- **Class Documentation**: [Class-level documentation]
- **Function Documentation**: [Function-level documentation]
- **Inline Comments**: [When and how to comment]

### Documentation Templates
```python
"""
Module: user_service.py

This module provides user management functionality including user creation,
retrieval, and management operations.

Classes:
    UserService: Main service class for user operations
    User: User data model
    UserError: Base exception for user-related errors

Functions:
    create_user_service: Factory function for UserService

Examples:
    >>> user_service = create_user_service(database)
    >>> user = user_service.get_user(1)
    >>> print(user.name)
    "John Doe"
"""

class UserService:
    """
    Service class for managing user operations.
    
    This class provides methods for creating, retrieving, updating, and
    deleting users. It handles database operations and business logic
    related to user management.
    
    Attributes:
        database: Database connection instance
        cache: In-memory cache for user data
    
    Example:
        >>> database = Database("connection_string")
        >>> user_service = UserService(database)
        >>> user = user_service.get_user(1)
    """
    
    def get_user(self, user_id: int) -> Optional[User]:
        """
        Retrieve a user by their ID.
        
        Args:
            user_id: The unique identifier for the user
            
        Returns:
            User object if found, None otherwise
            
        Raises:
            ValueError: If user_id is not a positive integer
            DatabaseError: If database operation fails
            
        Example:
            >>> user = user_service.get_user(1)
            >>> print(user.name)
            "John Doe"
        """
        pass
```

## Code Quality Tools

### Static Analysis
- **Linting**: [Linting tools and configuration]
- **Type Checking**: [Type checking tools]
- **Code Complexity**: [Complexity analysis tools]
- **Security Scanning**: [Security analysis tools]

### Tool Configuration
```yaml
# .pre-commit-config.yaml
repos:
  - repo: https://github.com/psf/black
    rev: 22.3.0
    hooks:
      - id: black
        language_version: python3.9

  - repo: https://github.com/pycqa/flake8
    rev: 4.0.1
    hooks:
      - id: flake8
        args: [--max-line-length=88]

  - repo: https://github.com/pycqa/isort
    rev: 5.10.1
    hooks:
      - id: isort
        args: [--profile, black]

  - repo: https://github.com/pre-commit/mirrors-mypy
    rev: v0.942
    hooks:
      - id: mypy
        additional_dependencies: [types-requests]
```

## CI/CD Integration

### Pipeline Standards
- **Build Process**: [Build automation]
- **Testing**: [Automated testing]
- **Quality Gates**: [Quality checks]
- **Deployment**: [Deployment automation]

### Pipeline Configuration
```yaml
# .github/workflows/ci.yml
name: CI/CD Pipeline

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Set up Python
      uses: actions/setup-python@v4
      with:
        python-version: 3.9
    
    - name: Install dependencies
      run: |
        python -m pip install --upgrade pip
        pip install -r requirements.txt
        pip install -r requirements-dev.txt
    
    - name: Run linting
      run: |
        flake8 src/
        black --check src/
        isort --check-only src/
    
    - name: Run type checking
      run: mypy src/
    
    - name: Run tests
      run: |
        pytest tests/ --cov=src/ --cov-report=xml
    
    - name: Upload coverage
      uses: codecov/codecov-action@v3
      with:
        file: ./coverage.xml
```

## Appendices

### Appendix A: Language-Specific Guidelines
- **Python**: [Python-specific standards]
- **JavaScript**: [JavaScript-specific standards]
- **Java**: [Java-specific standards]
- **C#**: [C#-specific standards]

### Appendix B: Tool Configurations
- **IDE Settings**: [IDE configuration files]
- **Linter Configs**: [Linter configuration files]
- **Formatter Configs**: [Code formatter settings]

### Appendix C: Best Practices
- **Design Patterns**: [Recommended patterns]
- **Anti-Patterns**: [Patterns to avoid]
- **Performance Tips**: [Performance optimization]
- **Security Best Practices**: [Security guidelines]

### Appendix D: Checklists
- **Pre-Commit Checklist**: [Before committing code]
- **Code Review Checklist**: [Review verification]
- **Release Checklist**: [Before releasing]
```

### 2. Pull Request Template
Standard pull request format:

```markdown
# Pull Request: [Title]

## Description
[Brief description of changes]

## Type of Change
- [ ] Bug fix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change which adds functionality)
- [ ] Breaking change (fix or feature that would cause existing functionality to not work as expected)
- [ ] Documentation update
- [ ] Refactoring (no functional changes)
- [ ] Performance improvement
- [ ] Test improvement

## Related Issues
- Closes #[issue number]
- Relates to #[issue number]

## Changes Made
- [Change 1]: [Description]
- [Change 2]: [Description]
- [Change 3]: [Description]

## Testing
### Test Coverage
- [ ] Unit tests added/updated
- [ ] Integration tests added/updated
- [ ] Manual testing completed
- [ ] Performance testing completed

### Test Results
- **Unit Tests**: [Pass/Fail - Details]
- **Integration Tests**: [Pass/Fail - Details]
- **Coverage**: [Coverage percentage]

## Code Quality
### Self-Review Checklist
- [ ] Code follows project style guidelines
- [ ] Self-review of code completed
- [ ] Comments added for complex logic
- [ ] Documentation updated
- [ ] No debug code or console logs

### Static Analysis
- [ ] Linting passes
- [ ] Type checking passes
- [ ] Security scan passes
- [ ] Code complexity within limits

## Deployment
### Deployment Checklist
- [ ] Database migrations included
- [ ] Environment variables documented
- [ ] Configuration changes documented
- [ ] Deployment instructions provided

### Rollback Plan
[Description of how to rollback if needed]

## Screenshots/Media
[Add screenshots or media if applicable]

## Additional Notes
[Any additional information for reviewers]

## Reviewer Checklist
- [ ] Code review completed
- [ ] Tests reviewed and adequate
- [ ] Documentation reviewed
- [ ] Security considerations reviewed
- [ ] Performance impact assessed

## Approval
- [ ] Ready for review
- [ ] Ready for merge
- [ ] Requires additional changes
```

### 3. Code Review Template
Structured code review format:

```markdown
# Code Review: [Feature/PR Title]

## Review Overview
- **Reviewer**: [Reviewer name]
- **Date**: [Review date]
- **PR/Branch**: [PR number or branch name]
- **Author**: [Code author]

## Summary
[Brief summary of changes and overall assessment]

## Detailed Review

### Functionality Review
#### Requirements Alignment
- [ ] Code meets functional requirements
- [ ] Edge cases properly handled
- [ ] Error scenarios addressed
- [ ] Business logic correct

#### Implementation Quality
- [ ] Algorithm efficiency appropriate
- [ ] Data structures well-chosen
- [ ] Integration points correct
- [ ] API contracts followed

### Code Quality Review
#### Structure and Organization
- [ ] Code well-organized and modular
- [ ] Proper separation of concerns
- [ ] Appropriate abstraction levels
- [ ] Dependencies well-managed

#### Naming and Conventions
- [ ] Naming conventions followed
- [ ] Variables and functions well-named
- [ ] Constants appropriately defined
- [ ] File organization logical

#### Comments and Documentation
- [ ] Code is self-documenting
- [ ] Complex logic explained
- [ ] API documentation updated
- [ ] README files updated

### Testing Review
#### Test Coverage
- [ ] Unit tests comprehensive
- [ ] Integration tests included
- [ ] Edge cases tested
- [ ] Error scenarios tested

#### Test Quality
- [ ] Tests are meaningful
- [ ] Tests are maintainable
- [ ] Mock objects used appropriately
- [ ] Test data well-structured

### Security Review
#### Security Implementation
- [ ] Input validation present
- [ ] Authentication implemented
- [ ] Authorization checked
- [ ] Data sanitization done

#### Security Best Practices
- [ ] No hardcoded credentials
- [ ] Secure coding practices followed
- [ ] Error messages don't leak info
- [ ] Logging doesn't expose sensitive data

### Performance Review
#### Performance Considerations
- [ ] Algorithm complexity acceptable
- [ ] Database queries optimized
- [ ] Memory usage reasonable
- [ ] Caching implemented where needed

#### Scalability
- [ ] Code scales with load
- [ ] Resource usage reasonable
- [ ] No performance bottlenecks
- [ ] Monitoring considerations

## Issues Found

### Critical Issues
#### Issue 1: [Title]
**File**: [File path]  
**Line**: [Line number]  
**Description**: [Detailed description]  
**Impact**: [High/Medium/Low]  
**Recommendation**: [Suggested fix]

#### Issue 2: [Title]
[Repeat structure...]

### Major Issues
#### Issue 1: [Title]
**File**: [File path]  
**Line**: [Line number]  
**Description**: [Detailed description]  
**Impact**: [High/Medium/Low]  
**Recommendation**: [Suggested fix]

### Minor Issues
#### Issue 1: [Title]
**File**: [File path]  
**Line**: [Line number]  
**Description**: [Detailed description]  
**Impact**: [High/Medium/Low]  
**Recommendation**: [Suggested fix]

## Suggestions for Improvement

### Code Quality Improvements
- [Suggestion 1]: [Description and rationale]
- [Suggestion 2]: [Description and rationale]

### Performance Improvements
- [Suggestion 1]: [Description and rationale]
- [Suggestion 2]: [Description and rationale]

### Maintainability Improvements
- [Suggestion 1]: [Description and rationale]
- [Suggestion 2]: [Description and rationale]

## Positive Feedback
- [Good practice 1]: [Description]
- [Good practice 2]: [Description]
- [Good practice 3]: [Description]

## Overall Assessment
### Strengths
- [Strength 1]: [Description]
- [Strength 2]: [Description]

### Areas for Improvement
- [Area 1]: [Description]
- [Area 2]: [Description]

### Recommendation
- [ ] Approve
- [ ] Approve with minor changes
- [ ] Requires changes before approval
- [ ] Reject (with explanation)

## Follow-up Actions
- [ ] Author to address critical issues
- [ ] Author to consider suggestions
- [ ] Re-review required after changes
- [ ] Documentation updates needed
- [ ] Additional testing required

## Comments
[Additional comments or context]
```

## Process Workflow

### 1. Development Process
```markdown
## Development Process Checklist
- [ ] Requirements understood
- [ ] Design approach planned
- [ ] Code structure designed
- [ ] Implementation completed
- [ ] Unit tests written
- [ ] Code self-reviewed
```

### 2. Code Review Process
```markdown
## Code Review Process Checklist
- [ ] Pull request created
- [ ] Automated checks passed
- [ ] Code review assigned
- [ ] Review completed
- [ ] Feedback addressed
- [ ] Approval received
```

### 3. Quality Assurance
```markdown
## Quality Assurance Checklist
- [ ] Code standards followed
- [ ] Test coverage adequate
- [ ] Performance acceptable
- [ ] Security review passed
- [ ] Documentation updated
- [ ] Deployment ready
```

## Integration with Development

### Development Workflow
```markdown
## Development Workflow Best Practices
- Follow branching strategy
- Write tests before code
- Commit early and often
- Use meaningful commit messages
- Request reviews early
- Address feedback promptly
```

### Collaboration with AI
```markdown
## AI Collaboration Best Practices
- Provide complete code context
- Specify coding standards
- Include test requirements
- Reference design patterns
- Request documentation
- Validate generated code
```

## Tools and Resources

### Development Tools
- **IDE**: [Recommended IDEs and settings]
- **Version Control**: [Git workflows and practices]
- **Code Formatting**: [Automated formatting tools]

### Quality Tools
- **Linting**: [Code linting tools]
- **Testing**: [Testing frameworks]
- **Coverage**: [Code coverage tools]

### CI/CD Tools
- **Build**: [Build automation tools]
- **Testing**: [Automated testing tools]
- **Deployment**: [Deployment automation]

---

**Note**: These templates are designed to work with GitHub Copilot and should be customized based on your specific programming languages, frameworks, and project requirements.
