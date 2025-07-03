#!/usr/bin/env python3
"""
AI Usage Log Analyzer
Automated tool for analyzing AI usage logs and calculating costs.
"""

import re
import json
from datetime import datetime, timedelta
from typing import Dict, List, Tuple

class AIUsageAnalyzer:
    """Analyzes AI usage logs and calculates productivity metrics."""
    
    # Cost rates as of July 2025 (per million tokens)
    COST_RATES = {
        'github_copilot': 0.0,  # Subscription-based
        'claude_3_5_sonnet': {'input': 3.00, 'output': 15.00},
        'gpt_4_turbo': {'input': 10.00, 'output': 30.00},
        'gpt_4': {'input': 30.00, 'output': 60.00}
    }
    
    def __init__(self, log_file_path: str = None):
        if log_file_path is None:
            # Use current week's log file
            from datetime import datetime
            current_date = datetime.now().strftime('%Y-%m-%d')
            self.log_file_path = f'logs/AI_USAGE_LOG_{current_date}.md'
        else:
            self.log_file_path = log_file_path
        self.entries = []
    
    def parse_log_file(self) -> List[Dict]:
        """Parse the AI usage log file and extract entries."""
        try:
            with open(self.log_file_path, 'r', encoding='utf-8') as f:
                content = f.read()
            
            # Split by log entry markers
            entries = re.split(r'---\s*\n', content)
            parsed_entries = []
            
            for entry in entries:
                if 'AI Interaction Log Entry' in entry:
                    parsed_entry = self._parse_single_entry(entry)
                    if parsed_entry:
                        parsed_entries.append(parsed_entry)
            
            self.entries = parsed_entries
            return parsed_entries
        
        except FileNotFoundError:
            print(f"Log file {self.log_file_path} not found.")
            return []
    
    def _parse_single_entry(self, entry: str) -> Dict:
        """Parse a single log entry."""
        entry_data = {}
        
        # Extract timestamp
        timestamp_match = re.search(r'\*\*Timestamp\*\*: (.+)', entry)
        if timestamp_match:
            entry_data['timestamp'] = timestamp_match.group(1).strip()
        
        # Extract AI engine
        engine_match = re.search(r'\*\*AI Engine\*\*: (.+)', entry)
        if engine_match:
            entry_data['ai_engine'] = engine_match.group(1).strip()
        
        # Extract time savings
        time_savings_match = re.search(r'\*\*Time Savings\*\*: (.+)', entry)
        if time_savings_match:
            entry_data['time_savings'] = time_savings_match.group(1).strip()
        
        # Extract token counts
        input_tokens_match = re.search(r'\*\*Input Token Count\*\*: [~]?(\d+)', entry)
        output_tokens_match = re.search(r'\*\*Output Token Count\*\*: [~]?(\d+)', entry)
        
        if input_tokens_match:
            entry_data['input_tokens'] = int(input_tokens_match.group(1))
        if output_tokens_match:
            entry_data['output_tokens'] = int(output_tokens_match.group(1))
        
        # Extract quality assessment
        quality_match = re.search(r'\*\*Quality Assessment\*\*: (.+)', entry)
        if quality_match:
            entry_data['quality'] = quality_match.group(1).strip()
        
        return entry_data
    
    def calculate_costs(self) -> Dict:
        """Calculate costs for all entries."""
        total_cost = 0.0
        cost_breakdown = {}
        
        for entry in self.entries:
            ai_engine = entry.get('ai_engine', '').lower()
            input_tokens = entry.get('input_tokens', 0)
            output_tokens = entry.get('output_tokens', 0)
            
            entry_cost = 0.0
            
            if 'github copilot' in ai_engine:
                entry_cost = 0.0  # Subscription-based
            elif 'claude' in ai_engine:
                rates = self.COST_RATES['claude_3_5_sonnet']
                entry_cost = (input_tokens * rates['input'] + output_tokens * rates['output']) / 1_000_000
            elif 'gpt-4 turbo' in ai_engine:
                rates = self.COST_RATES['gpt_4_turbo']
                entry_cost = (input_tokens * rates['input'] + output_tokens * rates['output']) / 1_000_000
            elif 'gpt-4' in ai_engine:
                rates = self.COST_RATES['gpt_4']
                entry_cost = (input_tokens * rates['input'] + output_tokens * rates['output']) / 1_000_000
            
            total_cost += entry_cost
            
            engine_key = ai_engine.replace(' ', '_').lower()
            if engine_key not in cost_breakdown:
                cost_breakdown[engine_key] = 0.0
            cost_breakdown[engine_key] += entry_cost
        
        return {
            'total_cost': total_cost,
            'cost_breakdown': cost_breakdown
        }
    
    def calculate_time_savings(self) -> Dict:
        """Calculate total time savings."""
        total_minutes = 0
        
        for entry in self.entries:
            time_savings = entry.get('time_savings', '')
            
            # Parse time savings (e.g., "4 hours 5 minutes", "45 minutes")
            hours_match = re.search(r'(\d+)\s*hours?', time_savings)
            minutes_match = re.search(r'(\d+)\s*minutes?', time_savings)
            
            entry_minutes = 0
            if hours_match:
                entry_minutes += int(hours_match.group(1)) * 60
            if minutes_match:
                entry_minutes += int(minutes_match.group(1))
            
            total_minutes += entry_minutes
        
        return {
            'total_minutes': total_minutes,
            'total_hours': total_minutes / 60,
            'formatted': f"{total_minutes // 60} hours {total_minutes % 60} minutes"
        }
    
    def generate_weekly_summary(self, start_date: str) -> Dict:
        """Generate weekly summary report."""
        # Parse start date
        start_dt = datetime.fromisoformat(start_date.replace('Z', '+00:00'))
        end_dt = start_dt + timedelta(days=7)
        
        # Filter entries for the week
        week_entries = []
        for entry in self.entries:
            if 'timestamp' in entry:
                try:
                    entry_dt = datetime.fromisoformat(entry['timestamp'].replace('Z', '+00:00'))
                    if start_dt <= entry_dt < end_dt:
                        week_entries.append(entry)
                except:
                    continue
        
        # Calculate metrics
        total_interactions = len(week_entries)
        time_savings = self.calculate_time_savings()
        costs = self.calculate_costs()
        
        # Quality assessment
        quality_counts = {'excellent': 0, 'good': 0, 'fair': 0, 'needs_improvement': 0}
        for entry in week_entries:
            quality = entry.get('quality', '').lower()
            if 'excellent' in quality:
                quality_counts['excellent'] += 1
            elif 'good' in quality:
                quality_counts['good'] += 1
            elif 'fair' in quality:
                quality_counts['fair'] += 1
            elif 'needs' in quality or 'improvement' in quality:
                quality_counts['needs_improvement'] += 1
        
        return {
            'total_interactions': total_interactions,
            'time_savings': time_savings,
            'costs': costs,
            'quality_counts': quality_counts,
            'avg_time_savings': time_savings['total_minutes'] / max(total_interactions, 1),
        }
    
    def export_summary(self, output_file: str = 'weekly_summary_generated.md'):
        """Export weekly summary to markdown file."""
        summary = self.generate_weekly_summary(datetime.now().strftime('%Y-%m-%d'))
        
        md_content = f"""# Weekly AI Usage Summary: Week of {datetime.now().strftime('%Y-%m-%d')}

## Overview Statistics
- **Total AI Interactions**: {summary['total_interactions']}
- **Total Time Saved**: {summary['time_savings']['formatted']}
- **Total Estimated Cost**: ${summary['costs']['total_cost']:.2f}
- **Average Time Savings per Interaction**: {summary['avg_time_savings']:.0f} minutes

## Cost Analysis
- **Total Cost**: ${summary['costs']['total_cost']:.2f}
- **Cost per Hour Saved**: ${summary['costs']['total_cost'] / max(summary['time_savings']['total_hours'], 1):.2f}

## Quality Assessment
- **Excellent Results**: {summary['quality_counts']['excellent']} ({summary['quality_counts']['excellent']/max(summary['total_interactions'], 1)*100:.0f}%)
- **Good Results**: {summary['quality_counts']['good']} ({summary['quality_counts']['good']/max(summary['total_interactions'], 1)*100:.0f}%)
- **Fair Results**: {summary['quality_counts']['fair']} ({summary['quality_counts']['fair']/max(summary['total_interactions'], 1)*100:.0f}%)
- **Needs Improvement**: {summary['quality_counts']['needs_improvement']} ({summary['quality_counts']['needs_improvement']/max(summary['total_interactions'], 1)*100:.0f}%)

## Cost Breakdown
"""
        
        for engine, cost in summary['costs']['cost_breakdown'].items():
            md_content += f"- **{engine.replace('_', ' ').title()}**: ${cost:.2f}\n"
        
        with open(output_file, 'w', encoding='utf-8') as f:
            f.write(md_content)
        
        print(f"Weekly summary exported to {output_file}")

def main():
    """Main function for command-line usage."""
    import sys
    
    log_file = None
    if len(sys.argv) > 1:
        if sys.argv[1] == 'weekly' and len(sys.argv) > 2:
            # Use specific weekly file
            week_date = sys.argv[2]
            log_file = f'logs/AI_USAGE_LOG_{week_date}.md'
        elif sys.argv[1] == 'current':
            # Use current week (default behavior)
            pass
        else:
            # Use provided file path
            log_file = sys.argv[1]
    
    analyzer = AIUsageAnalyzer(log_file)
    analyzer.parse_log_file()
    
    if analyzer.entries:
        print(f"Parsed {len(analyzer.entries)} log entries from {analyzer.log_file_path}")
        
        # Calculate and display metrics
        costs = analyzer.calculate_costs()
        time_savings = analyzer.calculate_time_savings()
        
        print(f"Total cost: ${costs['total_cost']:.2f}")
        print(f"Total time saved: {time_savings['formatted']}")
        print(f"Cost per hour saved: ${costs['total_cost'] / max(time_savings['total_hours'], 1):.2f}")
        
        # Export weekly summary
        analyzer.export_summary()
    else:
        print(f"No log entries found in {analyzer.log_file_path}")
        print("Make sure you're logging AI interactions in the correct weekly file.")

if __name__ == "__main__":
    main()
