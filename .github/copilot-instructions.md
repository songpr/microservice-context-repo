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
