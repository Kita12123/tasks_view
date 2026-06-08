---
name: redmine-api
description: A skill for interacting with the Redmine API to manage projects, track issues, and collaborate with team members.
tags:
  - skill
env:
  - REDMINE_API_KEY: "Your Redmine API key for authentication"
  - REDMINE_URL: "The base URL of your Redmine instance (e.g., https://your-redmine-instance.com)"
  - REDMINE_PROJECT_ID: "The identifier of the Redmine project you want to interact with"
---
# Redmine API

## When to use
- To manage projects and tasks efficiently.
- To track issues and bugs in software development.
- To collaborate with team members on project activities.

## Getting Started
1. Sign up for a Redmine account.
2. Setting up Redmine to enable API access as follows: Admin > Settings > Synchronization > Enable REST API ON.
3. Issue API access key from My Account > API access key.
4. Store the API key, Redmine URL, and project ID in the `.env` file in the root directory of the project or in your environment variables.

## API Endpoints
Please refer to the official Redmine API documentation for detailed infomation on available endpoints and their usage: [Redmine API Documentation](https://www.redmine.org/projects/redmine/wiki/Rest_api)

## Workflow
1. Get enviroment variables: REDMINE_API_KEY, REDMINE_URL, and REDMINE_PROJECT_ID from `.env` file in the root directory of the project or from your environment variables.
2. Request the Redmine API using the provided API key and URL to perform desired operations (e.g., creating an issue, updating a project, etc.).
