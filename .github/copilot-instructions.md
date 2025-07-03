# GitHub Copilot Instructions for Microservice Context Repository

## Repository Context
This repository contains the complete documentation and implementation for an enterprise customer experience (CX) solution built on Azure and AWS cloud platforms. The solution includes:
- Modern contact center implementation
- AI-powered virtual assistants
- Customer data platform (CDP) integration
- Omnichannel communication services

## Coding Standards & Preferences

### General Guidelines
- **Code Quality**: Follow clean code principles with meaningful naming conventions
- **Documentation**: Include comprehensive inline documentation and README files
- **Testing**: Implement unit tests, integration tests, and end-to-end tests
- **Security**: Use managed identity for Azure services, never hardcode credentials
- **Error Handling**: Implement proper error handling with retry logic and exponential backoff

### Azure Services Preferences
- **Authentication**: Use Azure Managed Identity whenever possible
- **Compute**: Prefer Azure Container Apps for containerized workloads
- **Storage**: Use Azure Blob Storage with appropriate access tiers
- **Messaging**: Implement Azure Service Bus for reliable messaging
- **Monitoring**: Include Azure Application Insights for observability
- **Security**: Store secrets in Azure Key Vault

### AWS Services Preferences
- **Contact Center**: Use Amazon Connect for contact center solutions
- **AI/ML**: Prefer Amazon Lex for conversational AI
- **Compute**: Use AWS Lambda for serverless functions
- **Storage**: Use Amazon S3 with appropriate storage classes
- **Monitoring**: Include CloudWatch for logging and metrics

### Infrastructure as Code
- **Azure**:  Use Terraform for Azure resources
- **AWS**: Use Terraform for AWS resources
- **Organization**: Structure IaC in modules for reusability
- **Naming**: Use consistent naming conventions with environment prefixes

### API Design
- **Standards**: Follow OpenAPI 3.0 specification
- **Versioning**: Use semantic versioning for APIs
- **Authentication**: Implement OAuth 2.0 / OpenID Connect
- **Documentation**: Include comprehensive API documentation

### Response Style
- **Detail Level**: Provide comprehensive explanations with examples
- **Code Comments**: Include explanatory comments for complex logic
- **Best Practices**: Suggest improvements and best practices
- **Security**: Always highlight security considerations

## File Structure Guidelines
- Use consistent directory structure as defined in the repository
- Include README.md files in each major directory
- Use Mermaid diagrams for architectural visualizations
- Store API specifications in OpenAPI/Swagger format
- Include feature files in Gherkin format for BDD testing

## Prompt Templates
Use the templates in `.github/prompts/` for common tasks:
- `#prompt:generate_unit_tests` - Generate comprehensive unit tests
- `#prompt:refactor_api_endpoint` - Refactor API endpoints following best practices
- `#prompt:summarize_feature` - Create feature summaries for stakeholders
- `#prompt:debug_build_error` - Debug build and deployment issues

## AI Usage Logging and Analysis

### Mandatory Logging Requirements
**IMPORTANT**: After every AI prompt usage, log the interaction details in the current week's log file in the `logs/` directory for productivity analysis and cost tracking.

**File Format**: `logs/AI_USAGE_LOG_YYYY-MM-DD.md` (where YYYY-MM-DD is the Monday of the current week)

**Quick Setup**: Use `./scripts/create_weekly_log.sh` to create new weekly files

### Required Log Entry Format
Each AI interaction must be logged with the following structure:

```markdown
## AI Interaction Log Entry

**Timestamp**: [ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ]
**Session ID**: [Unique identifier for the session]
**User**: [Developer name/identifier]
**AI Engine**: [GitHub Copilot Chat | GitHub Copilot Code Completion | Claude | GPT-4 | Other]

### Prompt Details
**Prompt Type**: [#prompt:template_name | Custom | Code Completion | Chat]
**Prompt Template Used**: [If applicable: generate_unit_tests | refactor_api_endpoint | summarize_feature | debug_build_error | custom]
**Original Prompt**: 
```
[Full prompt text here]
```

**Context Files Referenced**: 
- [List of files that were referenced or provided as context]

### AI Response Summary
**Response Type**: [Code Generation | Documentation | Explanation | Debugging | Refactoring | Analysis]
**Files Modified/Created**: 
- [List of files that were created or modified]
- [Include line count changes: +XX lines, -YY lines]

**Key Changes Summary**:
- [Bullet point summary of what was generated/modified]
- [Focus on functional changes, not just syntax]

### Productivity Analysis
**Estimated Manual Work Time**: [Hours/Minutes that would have been required manually]
**Actual AI Interaction Time**: [Time spent interacting with AI]
**Time Savings**: [Manual time - AI time = Savings]
**Quality Assessment**: [Excellent | Good | Fair | Needs Improvement]
**Manual Corrections Required**: [None | Minor | Moderate | Significant]

### Technical Metrics
**Input Token Count**: [Estimated tokens in prompt + context]
**Output Token Count**: [Estimated tokens in response]
**Total Token Count**: [Input + Output]

### Cost Analysis (Current Rates as of July 2025)
**AI Service Cost Calculation**:
- **GitHub Copilot**: $19/month (unlimited usage - pro-rated daily: ~$0.63/day)
- **Claude 3.5 Sonnet**: $3.00 per 1M input tokens, $15.00 per 1M output tokens
- **GPT-4 Turbo**: $10.00 per 1M input tokens, $30.00 per 1M output tokens
- **GPT-4**: $30.00 per 1M input tokens, $60.00 per 1M output tokens

**Estimated Cost for This Interaction**: $[Calculate based on tokens and rate]
**Cost per Hour Saved**: $[Cost / Time Savings]

### Learning and Improvement
**What Worked Well**: [Aspects of the prompt/interaction that were effective]
**What Could Be Improved**: [Areas for improvement in prompting or process]
**Suggested Prompt Refinements**: [How to improve similar future prompts]
**Knowledge Gained**: [New insights or patterns learned]

---
```

### Example Log Entry
```markdown
## AI Interaction Log Entry

**Timestamp**: 2025-07-03T14:30:45Z
**Session ID**: copilot-session-20250703-001
**User**: john.developer
**AI Engine**: GitHub Copilot Chat

### Prompt Details
**Prompt Type**: #prompt:generate_unit_tests
**Prompt Template Used**: generate_unit_tests
**Original Prompt**: 
```
#prompt:generate_unit_tests

Please generate comprehensive unit tests for the CustomerService class:
[CustomerService.cs code provided]

Requirements:
- Test coverage: 90%+
- Include happy path, edge cases, and error conditions
- Use xUnit with Moq for mocking
```

**Context Files Referenced**: 
- /CODE/services/customer_profile_api/src/Services/CustomerService.cs
- /CODE/services/customer_profile_api/src/Models/Customer.cs

### AI Response Summary
**Response Type**: Code Generation
**Files Modified/Created**: 
- /CODE/services/customer_profile_api/tests/Unit/Services/CustomerServiceTests.cs (+245 lines)

**Key Changes Summary**:
- Generated 15 comprehensive unit test methods
- Implemented proper mocking for repository dependencies
- Added test data fixtures and builders
- Included edge cases for null inputs and invalid data
- Added async test methods with proper cancellation token handling

### Productivity Analysis
**Estimated Manual Work Time**: 4.5 hours
**Actual AI Interaction Time**: 25 minutes
**Time Savings**: 4 hours 5 minutes
**Quality Assessment**: Excellent
**Manual Corrections Required**: Minor (adjusted 2 test assertions)

### Technical Metrics
**Input Token Count**: ~3,500 (prompt + context files)
**Output Token Count**: ~4,200 (generated test code + explanations)
**Total Token Count**: 7,700

### Cost Analysis
**AI Service Cost Calculation**: GitHub Copilot Chat (included in subscription)
**Estimated Cost for This Interaction**: $0.00 (subscription-based)
**Cost per Hour Saved**: $0.00

### Learning and Improvement
**What Worked Well**: 
- Providing complete context with service and model classes
- Specifying exact testing framework and mocking library
- Clear coverage requirements

**What Could Be Improved**: 
- Could have specified performance test requirements
- Should have mentioned specific edge cases to prioritize

**Suggested Prompt Refinements**: 
- Add request for performance benchmarks
- Include specific business rule validation tests

**Knowledge Gained**: 
- AI excels at generating boilerplate test code
- Providing model context significantly improves test quality
---
```

### Aggregated Reporting Requirements

#### Weekly Summary Report
Create a weekly summary in `AI_USAGE_WEEKLY_SUMMARY.md`:

```markdown
# Weekly AI Usage Summary: [Week of YYYY-MM-DD]

## Overview Statistics
- **Total AI Interactions**: [Number]
- **Total Time Saved**: [Hours:Minutes]
- **Total Estimated Cost**: $[Amount]
- **Average Time Savings per Interaction**: [Minutes]
- **Most Used Prompt Template**: [Template name]

## Productivity Metrics
- **Code Generation**: [Number of interactions] - [Time saved]
- **Documentation**: [Number of interactions] - [Time saved] 
- **Debugging**: [Number of interactions] - [Time saved]
- **Refactoring**: [Number of interactions] - [Time saved]

## Quality Assessment
- **Excellent Results**: [Number] ([Percentage]%)
- **Good Results**: [Number] ([Percentage]%)
- **Fair Results**: [Number] ([Percentage]%)
- **Needs Improvement**: [Number] ([Percentage]%)

## Cost Analysis
- **Total Subscription Cost**: $[Amount]
- **Per-token API Costs**: $[Amount]
- **Cost per Hour Saved**: $[Amount]
- **ROI Calculation**: [Time saved in dollars] / [AI costs] = [ROI ratio]

## Top Time-Saving Activities
1. [Activity] - [Hours saved]
2. [Activity] - [Hours saved]
3. [Activity] - [Hours saved]

## Improvement Opportunities
- [Identified areas for better prompting]
- [Process improvements]
- [Training needs]

## Recommendations
- [Actionable recommendations for next week]
```

### Implementation Guidelines

1. **Immediate Logging**: Log interactions immediately after completion
2. **Honest Assessment**: Provide realistic time estimates and quality assessments
3. **Detailed Context**: Include enough detail for future analysis
4. **Regular Review**: Weekly team review of AI usage patterns
5. **Continuous Improvement**: Refine prompts based on logged insights

### Cost Tracking Automation
Consider implementing automated tracking tools:
- Token counting utilities for API-based services
- Time tracking integration with development tools
- Automated cost calculation scripts
- Dashboard creation for real-time metrics

### Privacy and Security Notes
- **No Sensitive Data**: Never log credentials, PII, or proprietary business data
- **Code Snippets**: Only log non-sensitive code examples
- **Access Control**: Restrict access to usage logs appropriately
- **Retention Policy**: Define log retention and archival policies
