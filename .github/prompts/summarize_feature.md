# Summarize Feature

## Purpose
Create comprehensive feature summaries for product managers, stakeholders, and technical teams.

## Usage
Use this prompt when you need to:
- Document new features for stakeholders
- Create release notes
- Prepare feature specifications
- Generate technical documentation

## Prompt Template

```
#prompt:summarize_feature

Please create a comprehensive feature summary for the following implementation:

[PASTE CODE/SPECIFICATION HERE]

Summary structure:
1. **Feature Overview**
   - Brief description of the feature
   - Business value and impact
   - Target users/personas

2. **Technical Implementation**
   - Key components and technologies used
   - Architecture decisions and rationale
   - Integration points and dependencies

3. **Functional Requirements**
   - Core functionality delivered
   - User workflows and use cases
   - Acceptance criteria met

4. **Non-Functional Requirements**
   - Performance characteristics
   - Security considerations
   - Scalability and reliability aspects

5. **Testing Coverage**
   - Types of tests implemented
   - Test coverage metrics
   - Quality assurance approach

6. **Deployment and Operations**
   - Deployment strategy
   - Monitoring and alerting
   - Rollback procedures

7. **Documentation and Training**
   - User documentation provided
   - Training materials created
   - Support procedures

8. **Future Considerations**
   - Potential enhancements
   - Known limitations
   - Maintenance requirements

Target audience: [Specify: Technical team, Product managers, Executives, etc.]
```

## Example Usage

```markdown
#prompt:summarize_feature

Please create a feature summary for the intelligent routing feature we just implemented:

[Include implementation details here]

Target audience: Product managers and executive stakeholders
Focus on business value and user impact.
```
