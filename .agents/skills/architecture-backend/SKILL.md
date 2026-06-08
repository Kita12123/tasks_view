---
name: architecture-backend
description: Design and maintain the architecture of a backend API, following principles of clean architecture. Best practices include layered architecture, REST/GraphQL hybrid approach, Attribute-Based Access Control (ABAC), Fail Fast, and more.
---
# Architecture - Backend

## When to use
- To design and maintain the architecture of a backend API.
- To create or edit documentation related to backend architecture.
- To implement architectural improvements or refactor existing backend systems.
- To optimize performance or enhance security in a backend architecture.
- To review architectural decisions or provide feedback on backend design practices.

## Core Principles
- **Layered Architecture**: Separate this project into layers: Application, Domain, Infrastructure, and Presentation. 
- **REST/GraphQL Hybrid**: A hybrid approach is adopted, where REST APIs handle commands (write operations) and GraphQL handles queries (read operations)
- **Attribute-Based Access Control (ABAC)**: Implement Attribute-Based Access Control (ABAC) to manage permissions based on user context and resource attributes by using expression filters to code database queries.
- **Fail Fast**: Implement a fail-fast approach to quickly identify and handle errors, ensuring that issues are detected early in the development process and can be addressed promptly.

See the "References" section below for details.

## Getting Started: .NET project setup
1. Setup the project structure using the provided [setup script](scripts/setup-dotnet.bat).
2. Run `dotnet build` to build the solution and ensure all projects are correctly referenced and there are no build errors.

## References

### Layered Architecture
- **Dependency Inversion Principle**: Domain layer should not depend on any other layer. Application layer can depend on Domain, but not on Infrastructure. Infrastructure layer can depend on both Application and Domain, but not the other way around. Presentation layer can depend on Application and Domain, but not on Infrastructure.
- **Domain-Driven Design (DDD)**: Emphasizes the importance of the domain model and its logic and value objects, ensuring that the domain layer is independent of other layers and can evolve independently, Including the use of entities, value objects, aggregates, repositories, and domain services to model complex business logic.
- **Mediator Pattern**: Use the Mediator pattern to decouple the components of the application layer, allowing for better separation of concerns and easier maintenance.

### REST/GraphQL Hybrid Approach
- **GraphQL for Queries**: Use HotChocolate to handle read operations, and designed by SourceCode-First approach. Implement all entities with Projected, Paging, Sorting and Filtering, and support GraphQL best practices such as avoiding N+1 query problems and optimizing data fetching strategies. Also, implement access control at the GraphQL level using expression filters to ensure that users can only access data they are authorized to see.
- **REST for Commands**: RESTful APIs are based on Document-First (`openapi.yml`) design using NSwag. Use REST APIs to handle write operations (commands), following standard HTTP methods (POST, PUT, DELETE) and ensuring that commands are executed with proper validation and error handling.

### Attribute-Based Access Control (ABAC)
- **Expression Filters**: Define Entity Framework Core's expression filters in the domain layer to implement ABAC. These filters will be applied to database queries to enforce access control based on user attributes and resource attributes, ensuring that users can only access data they are authorized to see.

### Fail Fast
- **Exception-based Error Handling**: Implement a fail-fast approach by using global exception handling middleware to catch and handle exceptions. Command pipelines should validate input and business rules before executing commands, and if any validation fails, an exception should be thrown immediately to roll back the transaction and return an appropriate error response to the client. 