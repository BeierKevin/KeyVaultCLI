# Presentation Project

This project represents the Presentation layer in our clean architecture based solution. This layer is responsible for handling all user interface and presentation logic.

## References

This layer **should reference the Application project** for business operations, and may occasionally need to **reference the Infrastructure project** for utility operations.

## Key Components

1. `Program.cs`: Because this is a simple console application, this is the entry point of our application.
2. `Services`: This mainly contains the user interface logic, and is responsible for handling user input and output, 
   and interacting with the Console.
3. `UserInterface`: Mainly how to format and display data to the user in the console.

Remember not to define any business rules or domain logic in this layer.