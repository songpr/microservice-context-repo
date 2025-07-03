# UX Design Documentation Templates

## Overview
User Experience (UX) design templates optimized for both AI assistance and human workflow. These templates provide structured approaches to creating user-centered designs while enabling AI to understand context and generate relevant UX deliverables.

## AI-Optimized UX Prompts

### Primary UX Prompt Template
```markdown
@copilot I need to create UX documentation for [FEATURE/COMPONENT].

Context:
- Target users: [USER_PERSONAS]
- Use cases: [PRIMARY_USE_CASES]
- Constraints: [TECHNICAL_BUSINESS_CONSTRAINTS]
- Success metrics: [KPIs_AND_METRICS]

Please generate [DELIVERABLE_TYPE] following UX best practices.
```

### Specific UX Prompt Templates

#### User Journey Mapping
```markdown
@copilot Create a user journey map for [SPECIFIC_FEATURE].

Context:
- User persona: [PERSONA_NAME_AND_DETAILS]
- Starting point: [ENTRY_POINT]
- End goal: [DESIRED_OUTCOME]
- Pain points: [KNOWN_ISSUES]
- Touchpoints: [SYSTEM_INTERACTIONS]

Include: Steps, emotions, pain points, opportunities, and success metrics.
```

#### Wireframe Creation
```markdown
@copilot Generate wireframe specifications for [SCREEN/COMPONENT].

Context:
- Screen type: [MOBILE/DESKTOP/TABLET]
- User flow: [PREVIOUS_NEXT_SCREENS]
- Key elements: [REQUIRED_COMPONENTS]
- Constraints: [TECHNICAL_BRAND_CONSTRAINTS]

Include: Layout structure, component hierarchy, interaction patterns, and responsive behavior.
```

#### Persona Development
```markdown
@copilot Create a user persona for [TARGET_USER_TYPE].

Context:
- Product: [PRODUCT_NAME_AND_PURPOSE]
- Research data: [AVAILABLE_USER_RESEARCH]
- Business goals: [BUSINESS_OBJECTIVES]
- User goals: [USER_OBJECTIVES]

Include: Demographics, behaviors, motivations, pain points, and goals.
```

## Template Documents

### 1. User Journey Template
Use this structure for mapping user interactions:

```markdown
# User Journey: [Journey Name]

## Journey Overview
- **Persona**: [Primary user persona]
- **Scenario**: [Specific use case]
- **Goal**: [What user wants to achieve]
- **Duration**: [Expected time to complete]

## Journey Steps

### Step 1: [Step Name]
- **Action**: [What user does]
- **Touchpoint**: [Where interaction happens]
- **Emotion**: [User feeling: 😊 😐 😞]
- **Pain Points**: [Issues encountered]
- **Opportunities**: [Improvement possibilities]

### Step 2: [Step Name]
[Repeat structure...]

## Success Metrics
- **Completion Rate**: [Target percentage]
- **Time to Complete**: [Target duration]
- **User Satisfaction**: [Measurement method]
- **Error Rate**: [Acceptable threshold]

## Recommendations
- [Prioritized improvements]
- [Technical requirements]
- [Business impact]
```

### 2. Persona Template
Structure for user personas:

```markdown
# User Persona: [Persona Name]

## Basic Information
- **Age**: [Age range]
- **Role**: [Job title/role]
- **Location**: [Geographic location]
- **Tech Savviness**: [Low/Medium/High]

## Background
[Brief description of user's context and background]

## Goals
### Primary Goals
- [Main objective 1]
- [Main objective 2]
- [Main objective 3]

### Secondary Goals
- [Supporting objective 1]
- [Supporting objective 2]

## Frustrations & Pain Points
- [Major frustration 1]
- [Major frustration 2]
- [Major frustration 3]

## Behaviors & Preferences
- **Communication Style**: [Preferred methods]
- **Technology Usage**: [Devices and platforms]
- **Decision Making**: [Process and criteria]
- **Work Patterns**: [When and how they work]

## Needs & Expectations
- [Must-have requirement 1]
- [Must-have requirement 2]
- [Nice-to-have feature 1]

## Quote
> "[Representative quote that captures their mindset]"

## Persona Photo
[Image placeholder or description]
```

### 3. Wireframe Template
Structure for wireframe documentation:

```markdown
# Wireframe: [Screen/Component Name]

## Screen Overview
- **Screen Type**: [Mobile/Desktop/Tablet]
- **User Flow Position**: [Where in flow]
- **Primary Purpose**: [Main function]
- **Success Criteria**: [How to measure success]

## Layout Structure

### Header Section
- **Logo/Brand**: [Position and size]
- **Navigation**: [Menu items and structure]
- **User Actions**: [Login, profile, etc.]

### Main Content Area
- **Primary Content**: [Main information/functionality]
- **Secondary Content**: [Supporting information]
- **Call-to-Action**: [Primary buttons/links]

### Footer Section
- **Links**: [Footer navigation]
- **Legal**: [Terms, privacy, etc.]
- **Contact**: [Support information]

## Component Specifications

### Interactive Elements
| Component | Type | Behavior | State |
|-----------|------|----------|-------|
| [Button name] | Button | [Click action] | [Default/hover/active] |
| [Input field] | Input | [Validation rules] | [Empty/filled/error] |
| [Link] | Link | [Navigation target] | [Default/visited/hover] |

### Content Blocks
| Block | Content Type | Priority | Responsive Behavior |
|-------|-------------|----------|-------------------|
| [Block name] | [Text/Image/Video] | [High/Medium/Low] | [Desktop/Mobile behavior] |

## Interaction Patterns
- **Navigation**: [How users move between sections]
- **Data Entry**: [Form patterns and validation]
- **Feedback**: [Success/error messaging]
- **Loading States**: [Progressive disclosure]

## Responsive Design
- **Desktop**: [Layout description]
- **Tablet**: [Layout changes]
- **Mobile**: [Mobile-specific adaptations]

## Accessibility Considerations
- **Screen Reader**: [Support requirements]
- **Keyboard Navigation**: [Tab order and shortcuts]
- **Color Contrast**: [Contrast ratios]
- **Font Sizes**: [Minimum readable sizes]

## Technical Notes
- **Performance**: [Loading requirements]
- **Browser Support**: [Compatibility requirements]
- **Integration**: [API or backend requirements]
```

## UX Process Workflow

### 1. Discovery Phase
```markdown
## Discovery Checklist
- [ ] User research completed
- [ ] Competitive analysis done
- [ ] Stakeholder interviews conducted
- [ ] Business requirements gathered
- [ ] Technical constraints identified
- [ ] Success metrics defined
```

### 2. Design Phase
```markdown
## Design Checklist
- [ ] User personas created
- [ ] User journeys mapped
- [ ] Information architecture defined
- [ ] Wireframes created
- [ ] Prototypes built
- [ ] Usability testing planned
```

### 3. Validation Phase
```markdown
## Validation Checklist
- [ ] Usability testing completed
- [ ] Accessibility review done
- [ ] Stakeholder approval received
- [ ] Technical feasibility confirmed
- [ ] Performance requirements met
- [ ] Analytics implementation planned
```

## Integration with Development

### Handoff to Development
```markdown
## Design Handoff Checklist
- [ ] Final designs exported
- [ ] Design system components documented
- [ ] Interaction specifications provided
- [ ] Responsive breakpoints defined
- [ ] Animation specifications included
- [ ] Asset optimization completed
```

### Collaboration with AI
```markdown
## AI Collaboration Best Practices
- Provide complete context in prompts
- Reference specific personas and journeys
- Include technical constraints
- Specify success metrics
- Request structured outputs
- Iterate based on feedback
```

## Tools and Resources

### Design Tools
- **Figma**: [Link to team workspace]
- **Sketch**: [Link to shared libraries]
- **Adobe XD**: [Link to design system]

### Research Tools
- **User Testing Platforms**: [Links to accounts]
- **Analytics Tools**: [Dashboard links]
- **Survey Tools**: [Form templates]

### Documentation Tools
- **Confluence**: [Link to UX space]
- **Notion**: [Link to UX database]
- **Miro**: [Link to workshop boards]

## Examples and Case Studies

### Example 1: Login Flow
[Link to actual login flow documentation]

### Example 2: Dashboard Design
[Link to actual dashboard documentation]

### Example 3: Mobile App Onboarding
[Link to actual onboarding documentation]

---

**Note**: These templates are designed to work with GitHub Copilot and should be customized based on your specific project needs and team workflow.
