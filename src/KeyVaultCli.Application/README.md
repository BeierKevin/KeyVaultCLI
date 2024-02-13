# Application Project

This project represents the Application layer in our clean architecture based solution. This layer is responsible for orchestrating the domain layer and serves as a bridge between the domain and the external concerns (UI, Tests, Persistence etc.)

## References

This project **should only reference the Domain project** to use the entities and interfaces defined there.

## Key Components

1. `Commands`: These encapsulate actions users want to perform on our system. Commands are handled by Command Handlers.

2. `Services`: Services which ar tightly coupled to the domain layer and are used to perform operations that do not fit 
   into the domain layer.

Please avoid defining any business rules or domain logic in this layer.