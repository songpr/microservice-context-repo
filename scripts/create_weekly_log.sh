#!/bin/bash

# Create Weekly AI Usage Log Script
# This script creates a new weekly log file with the proper template

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Function to get Monday of the week for a given date
get_monday() {
    local date_input="$1"
    if [ -z "$date_input" ]; then
        date_input=$(date +%Y-%m-%d)
    fi
    
    # Get the day of week (1=Monday, 7=Sunday)
    day_of_week=$(date -d "$date_input" +%u)
    
    # Calculate days to subtract to get to Monday
    days_to_subtract=$((day_of_week - 1))
    
    # Get Monday's date
    monday_date=$(date -d "$date_input - $days_to_subtract days" +%Y-%m-%d)
    echo "$monday_date"
}

# Parse command line arguments
if [ "$1" = "help" ] || [ "$1" = "--help" ] || [ "$1" = "-h" ]; then
    echo "Usage: $0 [date]"
    echo ""
    echo "Creates a new weekly AI usage log file."
    echo ""
    echo "Arguments:"
    echo "  date    Optional date in YYYY-MM-DD format (defaults to current week)"
    echo ""
    echo "Examples:"
    echo "  $0                    # Create log for current week"
    echo "  $0 2025-07-10        # Create log for week of July 10, 2025"
    echo ""
    echo "Note: The script will automatically determine the Monday of the specified week."
    exit 0
fi

# Determine the target date
if [ -n "$1" ]; then
    TARGET_DATE="$1"
    # Validate date format
    if ! date -d "$TARGET_DATE" +%Y-%m-%d >/dev/null 2>&1; then
        echo -e "${RED}Error: Invalid date format. Use YYYY-MM-DD.${NC}"
        exit 1
    fi
else
    TARGET_DATE=$(date +%Y-%m-%d)
fi

# Get Monday of the target week
WEEK_START=$(get_monday "$TARGET_DATE")
LOG_FILE="logs/AI_USAGE_LOG_${WEEK_START}.md"

echo -e "${GREEN}Creating weekly AI usage log...${NC}"
echo "Target date: $TARGET_DATE"
echo "Week start (Monday): $WEEK_START"
echo "Log file: $LOG_FILE"

# Create logs directory if it doesn't exist
mkdir -p logs

# Check if file already exists
if [ -f "$LOG_FILE" ]; then
    echo -e "${YELLOW}Warning: Log file already exists: $LOG_FILE${NC}"
    read -p "Do you want to overwrite it? (y/N): " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Yy]$ ]]; then
        echo "Cancelled."
        exit 0
    fi
fi

# Create the weekly log file
cat > "$LOG_FILE" << EOF
# AI Usage Log - Week of $WEEK_START

This file contains detailed logs of all AI prompt interactions for the week of $WEEK_START.

**Important**: Every AI interaction must be logged here immediately after completion. Follow the format specified in \`.github/copilot-instructions.md\`.

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
EOF

echo -e "${GREEN}✓ Weekly log file created successfully: $LOG_FILE${NC}"
echo ""
echo "Next steps:"
echo "1. Start logging your AI interactions in this file"
echo "2. Follow the format specified in .github/copilot-instructions.md"
echo "3. Use the analysis scripts to generate weekly reports"
echo ""
echo "Quick commands:"
echo "  ./scripts/analyze_ai_usage.sh                    # Analyze current week"
echo "  ./scripts/analyze_ai_usage.sh weekly $WEEK_START # Analyze this week"
echo "  ./scripts/analyze_ai_usage.sh validate $LOG_FILE # Validate format"
