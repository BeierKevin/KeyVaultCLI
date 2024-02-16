# Infrastructure Project

This project represents the Infrastructure layer in our clean architecture based solution. This layer provides concrete implementations for interfaces defined in the domain layer, and handles all system interactions.

## References

This project **should reference the Domain project** to provide concrete implementations for its interfaces. In certain cases, it might also need to **reference the Application project**.

## Key Components

1. `Services`: External services or utilities like File Provider, Encryption, Generation, Commands and Console are 
   defined here.

Ensure infrastructure-specific business rules or logic are not defined in this project.