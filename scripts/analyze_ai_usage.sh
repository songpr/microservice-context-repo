#!/bin/bash

# AI Usage Log Analyzer Script
# This script helps analyze AI usage logs and generate reports

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${GREEN}AI Usage Log Analyzer${NC}"
echo "================================"

# Check if Python is available
if ! command -v python3 &> /dev/null; then
    echo -e "${RED}Error: Python 3 is required but not installed.${NC}"
    exit 1
fi

# Check if the current week's log file exists
CURRENT_WEEK=$(date +%Y-%m-%d)
CURRENT_LOG_FILE="logs/AI_USAGE_LOG_${CURRENT_WEEK}.md"

if [ ! -f "$CURRENT_LOG_FILE" ]; then
    echo -e "${YELLOW}Warning: Current week's log file not found: $CURRENT_LOG_FILE${NC}"
    echo "Creating new weekly log file..."
    
    # Create logs directory if it doesn't exist
    mkdir -p logs
    
    # Create weekly log file from template
    cat > "$CURRENT_LOG_FILE" << 'EOF'
# AI Usage Log - Week of CURRENT_WEEK

This file contains detailed logs of all AI prompt interactions for the week of CURRENT_WEEK.

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
EOF
    
    # Replace placeholder with actual date
    sed -i "s/CURRENT_WEEK/$CURRENT_WEEK/g" "$CURRENT_LOG_FILE"
    echo -e "${GREEN}Created weekly log file: $CURRENT_LOG_FILE${NC}"
fi

# Check if the analyzer script exists
if [ ! -f "scripts/ai_usage_analyzer.py" ]; then
    echo -e "${RED}Error: scripts/ai_usage_analyzer.py not found.${NC}"
    exit 1
fi

# Parse command line arguments
case "${1:-analyze}" in
    "analyze"|"")
        echo "Analyzing current week's AI usage logs..."
        python3 scripts/ai_usage_analyzer.py current
        ;;
    "weekly")
        if [ -n "$2" ]; then
            echo "Analyzing weekly logs for week of $2..."
            python3 scripts/ai_usage_analyzer.py weekly "$2"
        else
            echo "Generating current week's summary..."
            python3 scripts/ai_usage_analyzer.py current
        fi
        
        if [ -f "weekly_summary_generated.md" ]; then
            echo -e "${GREEN}Weekly summary generated: weekly_summary_generated.md${NC}"
        fi
        ;;
    "help")
        echo "Usage: $0 [command] [options]"
        echo ""
        echo "Commands:"
        echo "  analyze     - Analyze current week's AI usage logs (default)"
        echo "  weekly      - Generate weekly summary report"
        echo "  help        - Show this help message"
        echo "  validate    - Validate log file format"
        echo ""
        echo "Options:"
        echo "  weekly YYYY-MM-DD  - Analyze specific week (e.g., weekly 2025-07-03)"
        echo ""
        echo "Examples:"
        echo "  $0                      # Analyze current week"
        echo "  $0 weekly              # Generate current week summary"
        echo "  $0 weekly 2025-07-03   # Analyze specific week"
        echo "  $0 validate            # Check current week's log format"
        ;;
    "validate")
        if [ -n "$2" ]; then
            LOG_FILE="$2"
        else
            LOG_FILE="$CURRENT_LOG_FILE"
        fi
        
        echo "Validating log file format: $LOG_FILE"
        
        if [ ! -f "$LOG_FILE" ]; then
            echo -e "${RED}Error: Log file not found: $LOG_FILE${NC}"
            exit 1
        fi
        
        # Check for required sections
        if grep -q "AI Interaction Log Entry" "$LOG_FILE"; then
            echo -e "${GREEN}✓ Found log entries${NC}"
        else
            echo -e "${YELLOW}⚠ No log entries found${NC}"
        fi
        
        # Check for required fields
        required_fields=(
            "Timestamp"
            "AI Engine"
            "Prompt Type"
            "Time Savings"
            "Quality Assessment"
        )
        
        for field in "${required_fields[@]}"; do
            if grep -q "**${field}**" "$LOG_FILE"; then
                echo -e "${GREEN}✓ Found field: ${field}${NC}"
            else
                echo -e "${YELLOW}⚠ Missing field: ${field}${NC}"
            fi
        done
        
        echo ""
        echo "Validation complete."
        ;;
    *)
        echo -e "${RED}Error: Unknown command '$1'${NC}"
        echo "Use '$0 help' for usage information."
        exit 1
        ;;
esac

echo ""
echo -e "${GREEN}Done!${NC}"
