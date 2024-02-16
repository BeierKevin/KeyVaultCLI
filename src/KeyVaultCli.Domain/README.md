# Domain Project

This project represents the Domain layer of our clean architecture based solution. It contains all our business entities along with their rules and behaviors. All core business logic is defined within this layer.

## References

Remember, this is an independent project and **should not have any references to other projects** in the solution.

## Key Components

1. `Entities`: These are the core objects of our business domain. They include essential attributes and behaviors.

2. `Value Objects`: These are immutable objects that validate and represent particular values in our system.

3. `Common/Interfaces`: These are used to create a contract for services, which will be used by our 
   entities.

Please do not add any infrastructure-related code or any external package references to this project.