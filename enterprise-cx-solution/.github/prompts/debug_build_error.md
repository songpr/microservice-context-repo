# Debug Build Error

## Purpose
Debug build and deployment errors using build logs and error context.

## Usage
Use this prompt when you encounter:
- Build failures in CI/CD pipelines
- Deployment errors
- Runtime errors after deployment
- Infrastructure provisioning issues

## Prompt Template

```
#prompt:debug_build_error

Please help debug the following build/deployment error:

**Error Context:**
- Environment: [Development/Staging/Production]
- Platform: [Azure/AWS/Local]
- Build tool: [Docker/dotnet/npm/gradle/etc.]
- Deployment method: [CI/CD pipeline/Manual/IaC]

**Error Output:**
```
[PASTE ERROR LOGS HERE]
```

**Recent Changes:**
- [List recent code changes]
- [Configuration changes]
- [Infrastructure modifications]

**Analysis Request:**
1. **Root Cause Analysis**
   - Identify the primary cause of the error
   - Explain why the error occurred
   - Identify contributing factors

2. **Resolution Steps**
   - Provide step-by-step fix instructions
   - Include commands to run
   - Specify configuration changes needed

3. **Prevention Measures**
   - Suggest improvements to prevent similar issues
   - Recommend additional testing or checks
   - Propose monitoring or alerting enhancements

4. **Verification Steps**
   - How to verify the fix works
   - What to test after applying the fix
   - Monitoring points to watch

Additional context:
- Include relevant configuration files
- Mention any environmental constraints
- Specify urgency level (Critical/High/Medium/Low)
```

## Example Usage

```markdown
#prompt:debug_build_error

Please help debug this Azure Container App deployment error:

**Error Context:**
- Environment: Production
- Platform: Azure Container Apps
- Build tool: Docker
- Deployment method: GitHub Actions CI/CD

**Error Output:**
```
[Include actual error logs here]
```

**Recent Changes:**
- Updated API authentication to use Managed Identity
- Modified container startup command
- Updated environment variables in deployment template
```
