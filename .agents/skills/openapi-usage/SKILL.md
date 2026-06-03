---
name: openapi-usage
description: This is a skill for using OpenAPI specification to define API contracts and generate code for backend and frontend projects. You can use this skill to maintain consistency in API contracts and automate code generation based on the defined API contracts.
tags:
  - skill
---
# OpenAPI Usage

## When to use
- To reflect the API contract defined in the `openapi.yml` file in the project root directory.
- To automatically generate code for backend projects using NSwag based on the API contract.
- To automatically generate code for frontend projects using ORVAL based on the API contract.

## Workflow: Adding or Modifying API Contract
1. Install the `writing-openapi-specs` skill from `speakeasy-api/skills`. *Only if it is not already installed.*
2. Update the `openapi.yml` file in the project root directory to add or update terms and definitions as needed.
3. Refer to the `openapi.yml` file in the project root directory when working with AI agents or when adding or updating documents and coding.
4. Use the updated `openapi.yml` file to generate code following:
   - [Backend Projects](#usage-with-nswag)
   - [Frontend Projects](#usage-with-orval)

## Usage with NSwag
1. Copy the `nswag.json` file from the `references` directory to the project root directory. *Only if the `nswag.json` file does not exist in the project root directory.*
2. Use the `nswag.json` file in the project root directory to generate code for the API contract as needed. See the [NSWAG script](scripts/NSWAG.md) for details on how to use NSwag for code generation.

## Usage with ORVAL
1. Copy the `orval.config.ts` file from the `references` directory to the project root directory. *Only if the `orval.config.ts` file does not exist in the project root directory.*
2. Use the `orval.config.ts` file in the project root directory to generate code for the API contract as needed. See the [ORVAL script](scripts/ORVAL.md) for details on how to use ORVAL for code generation.
