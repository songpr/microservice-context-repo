# AI Usage Logs Directory

This directory contains weekly AI usage logs for productivity analysis and cost tracking.

## File Structure

```
logs/
├── README.md                      # This file
├── AI_USAGE_LOG_2025-07-03.md    # Week of July 3, 2025
├── AI_USAGE_LOG_2025-07-10.md    # Week of July 10, 2025
├── AI_USAGE_LOG_2025-07-17.md    # Week of July 17, 2025
└── ...                           # Additional weekly files
```

## Weekly File Naming Convention

- **Format**: `AI_USAGE_LOG_YYYY-MM-DD.md`
- **Date**: Start date of the week (Monday)
- **Example**: `AI_USAGE_LOG_2025-07-03.md` for week of July 3-9, 2025

## Usage Guidelines

### Daily Practice
1. **Log every AI interaction** in the current week's file
2. **Use the structured format** specified in `.github/copilot-instructions.md`
3. **Include all required fields**:
   - Timestamp and session information
   - Prompt details and context files
   - AI response summary and file changes
   - Productivity analysis (time savings, quality assessment)
   - Technical metrics (token counts)
   - Cost analysis
   - Learning and improvement notes

### Weekly Management
1. **Create new weekly file** every Monday
2. **Use the template** from previous weeks
3. **Update the week date** in the header
4. **Archive completed weeks** (keep for analysis)

### Monthly Analysis
1. **Review all weekly files** for trends
2. **Run analysis scripts** across multiple weeks
3. **Generate summary reports** for management
4. **Update processes** based on insights

## Analysis Scripts

The analysis scripts have been updated to handle weekly files:

```bash
# Analyze current week
./scripts/analyze_ai_usage.sh

# Analyze specific week
./scripts/analyze_ai_usage.sh weekly 2025-07-03

# Analyze multiple weeks
./scripts/analyze_ai_usage.sh monthly 2025-07
```

## Quick Start

1. **Find current week file** or create one using the naming convention
2. **Copy the log format** from `.github/copilot-instructions.md`
3. **Log your AI interaction** immediately after completion
4. **Use analysis scripts** to generate insights

## File Template

When creating a new weekly file, use this template:

```markdown
# AI Usage Log - Week of YYYY-MM-DD

This file contains detailed logs of all AI prompt interactions for the week of [Date].

**Important**: Every AI interaction must be logged here immediately after completion. Follow the format specified in `.github/copilot-instructions.md`.

## Log Format Reference

Each entry must include:
- Timestamp and session information
- Prompt details and context files
- AI response summary and file changes
- Productivity analysis (time savings, quality assessment)
- Technical metrics (token counts)
- Cost analysis
- Learning and improvement notes

## AI Interaction Log Entries

---

<!-- New log entries should be added below this line -->
```

## Automation

### Creating Weekly Files
```bash
# Create new weekly file with current date
./scripts/create_weekly_log.sh

# Create weekly file for specific date
./scripts/create_weekly_log.sh 2025-07-10
```

### Analysis and Reporting
```bash
# Generate weekly summary
./scripts/analyze_ai_usage.sh weekly

# Generate monthly report
./scripts/analyze_ai_usage.sh monthly

# Validate log format
./scripts/analyze_ai_usage.sh validate logs/AI_USAGE_LOG_2025-07-03.md
```

## Best Practices

1. **Immediate Logging**: Log interactions as soon as they complete
2. **Accurate Timing**: Be honest about time savings and manual effort
3. **Complete Context**: Include all relevant files and information
4. **Quality Assessment**: Provide realistic quality ratings
5. **Learning Focus**: Document what worked and what didn't
6. **Cost Awareness**: Track token usage and costs accurately

## Privacy and Security

- **No Sensitive Data**: Never log credentials, PII, or proprietary information
- **Code Snippets**: Only include non-sensitive code examples
- **Context Awareness**: Be mindful of what information is being logged
- **Access Control**: Ensure proper access controls on log files

## Troubleshooting

### Common Issues

1. **Missing log entries** - Check if logging immediately after AI interaction
2. **Incorrect format** - Refer to `.github/copilot-instructions.md` for proper format
3. **Analysis script errors** - Verify log file format and structure
4. **File permissions** - Ensure proper read/write permissions on log files

### Getting Help

1. Review the log format specification
2. Check existing log entries for examples
3. Use the analysis scripts for validation
4. Consult team members for guidance
