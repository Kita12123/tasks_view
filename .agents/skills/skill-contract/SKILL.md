---
name: skill-contract
description: This is a skill to manage your skills in a project for AI agents.
tags:
  - skill
---
# Skill Contract
This is skill to manage your skills in project.
*refer to [vercel-labs/skills](https://github.com/vercel-labs/skills)*

## When to use
- To install skills.
- To manage skills.
- To manage AI agents.

## Workflow: Adding a skill
1. Decide which skilll to add and find the repository url.
2. Run `npx skills add <repository-url> --agent github-copilot --skill <skill-name>` in the terminal.
3. Check if the skill is added successfully with run `npx skills list` in the terminal.

## Workflow: Removing a skill
1. Get skill name to remove with run `npx skills list` in the terminal.
2. Run `npx skills remove <skill-name>` in the terminal.
3. Check if the skill is removed successfully with run `npx skills list` in the terminal.