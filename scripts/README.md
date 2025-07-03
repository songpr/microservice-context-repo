# AI Usage Analysis Scripts

This directory contains automation scripts for analyzing AI usage logs and generating productivity reports.

## Scripts

### `analyze_ai_usage.sh`
Main script for analyzing AI usage logs and generating reports.

**Usage:**
```bash
./scripts/analyze_ai_usage.sh [command] [options]
```

**Commands:**
- `analyze` - Analyze current week's AI usage logs (default)
- `weekly` - Generate weekly summary report
- `validate` - Validate log file format
- `help` - Show help message

**Options:**
- `weekly YYYY-MM-DD` - Analyze specific week (e.g., `weekly 2025-07-03`)

**Examples:**
```bash
# Analyze current week
./scripts/analyze_ai_usage.sh

# Generate current week's report
./scripts/analyze_ai_usage.sh weekly

# Analyze specific week
./scripts/analyze_ai_usage.sh weekly 2025-07-03

# Validate current week's log format
./scripts/analyze_ai_usage.sh validate
```

### `create_weekly_log.sh`
Script for creating new weekly log files with proper template.

**Usage:**
```bash
./scripts/create_weekly_log.sh [date]
```

**Examples:**
```bash
# Create log for current week
./scripts/create_weekly_log.sh

# Create log for specific week
./scripts/create_weekly_log.sh 2025-07-10
```

### `ai_usage_analyzer.py`
Python script that provides the core analysis functionality.

**Features:**
- Parse AI usage log entries
- Calculate time savings and costs
- Generate weekly summaries
- Export reports to markdown

**Requirements:**
- Python 3.6+
- No external dependencies (uses standard library)

## Setup

1. Ensure Python 3 is installed
2. Make the script executable:
   ```bash
   chmod +x scripts/analyze_ai_usage.sh
   ```
3. Run the analyzer:
   ```bash
   ./scripts/analyze_ai_usage.sh
   ```

## Log File Format

The analyzer expects log entries in the format specified in `.github/copilot-instructions.md`. Each entry should include:

- Timestamp
- AI Engine used
- Time savings information
- Token counts
- Quality assessment
- Cost information

## Output Files

- `weekly_summary_generated.md` - Auto-generated weekly summary
- Console output with key metrics and statistics

## Customization

To modify cost rates or add new AI engines:

1. Edit the `COST_RATES` dictionary in `ai_usage_analyzer.py`
2. Add new parsing logic for additional AI engines
3. Update the analysis functions as needed

## Troubleshooting

**Common Issues:**

1. **"Permission denied"** - Run `chmod +x scripts/analyze_ai_usage.sh`
2. **"Python not found"** - Ensure Python 3 is installed and in PATH
3. **"Log file not found"** - Script will create empty log file automatically
4. **"No entries found"** - Check log file format against specification

**Getting Help:**
```bash
./scripts/analyze_ai_usage.sh help
```
