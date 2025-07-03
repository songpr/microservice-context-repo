# Diagram Source Files

This directory contains source files for all architectural diagrams organized by layer and tool.

## Structure

```
DIAGRAMS_SOURCE/
├── architecture/              # Architecture diagrams by layer
│   ├── cx-frontend/          # Frontend layer diagrams
│   ├── backend-services/     # Backend services diagrams
│   ├── data-layer/           # Data layer diagrams
│   └── integration-layer/    # Integration layer diagrams
├── excalidraw/               # Excalidraw source files (.excalidraw)
├── drawio/                   # Draw.io source files (.drawio)
├── plantuml/                 # PlantUML source files (.puml)
└── README.md
```

## Diagram Types

### Architecture Diagrams
- **System Architecture**: High-level system overview
- **Layer Diagrams**: Detailed layer-specific architectures
- **Component Diagrams**: Service and component relationships
- **Deployment Diagrams**: Infrastructure and deployment views

### Process Diagrams
- **Data Flow**: Data movement through the system
- **User Journeys**: Customer experience flows
- **Integration Flows**: External system interactions
- **Workflow Diagrams**: Business process flows

## Tools and Formats

### Excalidraw
- **Purpose**: Quick sketches and collaborative diagrams
- **Format**: .excalidraw files
- **Export**: PNG, SVG for documentation

### Draw.io (diagrams.net)
- **Purpose**: Professional architectural diagrams
- **Format**: .drawio files
- **Export**: PNG, SVG, PDF for documentation

### PlantUML
- **Purpose**: Code-based diagrams and documentation
- **Format**: .puml text files
- **Export**: PNG, SVG for automated documentation

## Best Practices

1. **Version Control**: All diagram sources are version controlled
2. **Naming Conventions**: Clear, descriptive file names
3. **Documentation**: Embedded in markdown files
4. **Automation**: Automated diagram generation in CI/CD
5. **Consistency**: Use standard symbols and conventions

## Automation

- **CI/CD Integration**: Automated diagram rendering
- **Documentation Sync**: Keep diagrams in sync with code
- **Quality Checks**: Validate diagram consistency
- **Export Pipeline**: Automated export to documentation formats
