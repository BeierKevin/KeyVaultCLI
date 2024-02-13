# Infrastructure Project

This project represents the Infrastructure layer in our clean architecture based solution. This layer provides concrete implementations for interfaces defined in the domain layer, and handles all system interactions.

## References

This project **should reference the Domain project** to provide concrete implementations for its interfaces. In certain cases, it might also need to **reference the Application project**.

## Key Components

1. `Repository Implementations`: These are the concrete implementations of the repositories defined in the domain layer.

2. `Persistence`: This sub-layer handles data access and storage concerns.

3. `Services`: External services or utilities like Email Sender, File Provider etc. are defined here.

Ensure infrastructure-specific business rules or logic are not defined in this project.