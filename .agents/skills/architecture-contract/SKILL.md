---
name: architecture-contract
description: Define the contract of a directory structure for a project. You can use it to organize project files and folders by following a predefined directory structure.
tags:
  - skill
---
# When to use
- To define the contract of a directory structure for a project.
- To use a predefined directory structure to organize project files and folders.
- To adding or modifying directory names in the project.

# Architecture Contract: Backend Projects
Backend projects is based on DDD of clean architecture, and split into 4 layers: Application, Domain, Infrastructure and Presentation. Dependency rule is that Domain layer is independent of other layers, Application and Infrastructure layers depends on Domain layer, and Presentation layer depends on Application layer.

# Workflow: Adding or Modifying Directory
1. Refer to the `DIRECTORIES.txt` file from `references` directory to understand the predefined directory structure for the project.
2. When adding new directories, follow the structure defined in the `DIRECTORIES.txt` file to maintain consistency in the project organization.
