# Application Source Code

This directory contains the complete application source code organized by architectural layers.

## Layered Architecture

### 🎨 CX Frontend Layer (`cx-frontend/`)
Customer-facing applications and user interfaces:
- **Web Portal**: Main customer web application
- **Mobile App**: Native or hybrid mobile application
- **Admin Dashboard**: Administrative interface for support agents
- **Shared Components**: Reusable UI components and libraries

### 🔧 Backend Services Layer (`backend-services/`)
Core business logic and API services:
- **Microservices**: Individual business domain services
- **API Gateway**: Request routing and authentication
- **Authentication Service**: User authentication and authorization
- **Notification Service**: Communication and messaging

### 📊 Data Layer (`data-layer/`)
Data management and persistence:
- **Repositories**: Data access patterns and implementations
- **Data Models**: Entity definitions and schemas
- **Data Processing**: ETL jobs and data transformation
- **Migrations**: Database schema changes

### 🔗 Integration Layer (`integration-layer/`)
External system integrations:
- **External APIs**: Third-party service integrations
- **Message Brokers**: Event-driven architecture components
- **Webhooks**: Inbound integration handlers
- **API Connectors**: Outbound integration clients

### 🔧 Shared (`shared/`)
Common libraries and utilities:
- **Common Libraries**: Shared business logic
- **Security Utilities**: Security helper functions
- **Monitoring**: Logging and telemetry utilities
- **Configuration**: Environment and configuration management

## Development Guidelines

1. **Layer Isolation**: Each layer should have clear boundaries and minimal coupling
2. **Dependency Direction**: Dependencies should flow from outer layers to inner layers
3. **Testing**: Each layer should have comprehensive unit and integration tests
4. **Documentation**: Each service/component should have clear README documentation

## Getting Started

1. Review the architecture documentation in `02_ARCHITECTURE/02_Layer_Architecture/`
2. Check the specific layer README files for detailed implementation guidance
3. Follow the coding standards in `03_DESIGN_DEVELOPMENT/03_Code_Standards_Guidelines.md`
4. Use the AI prompt templates in `.github/prompts/` for consistent development patterns
