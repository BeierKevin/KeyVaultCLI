# KeyVaultCLI

Key Vault CLI (*kvc*) is a command line interface password manager developed in C#. This open-source project helps you
securely manage your passwords and sensitive information while providing a user-friendly and efficient CLI-based
interface. Below, you'll find information on how to run, build, the technologies used, and the features covered by Key
Vault CLI.

## Features

Key Vault CLI comes with a variety of features to help you manage your passwords and sensitive information. Here's a
list of features, marked with checkmarks (✔), crosses (❌) to indicate the status of each feature, and Work in Progress (
🚧) to indicate features that are currently being developed or planned for future releases:

- ✔ **Master Password**: Secure your Vault with a master password.
- ✔ **CRUD Operations**: Create, read, update, and delete password entries.
- ✔ **Password Generation**: Generate strong, random passwords.
- 🚧 **Password Storage**: Safely store your passwords and sensitive information.
- 🚧 **Encryption**: Passwords are securely encrypted before storage.
- ✔ **Search and Retrieval**: Easily search and retrieve stored passwords.
- ✔ **Multi-Platform Support**: Key Vault CLI can be installed and used on all the major platforms (Windows, MacOS and
  Linux).
- ✔ **Backup and Restore**: Backup and restore your password database.
- ✔ **Category Organization**: Organize your passwords into categories for better management.
- ✔ **Password Health Check**: Check the strength and security of your passwords.
- ❌ **Password Sharing**: Share passwords securely with other users.

## Prerequisites

Key Vault CLI is built on the .NET SDK. To run the application, you'll need to have the .NET SDK installed on your
local machine. You can find the latest version of the
SDK [here](https://dotnet.microsoft.com/download). The application uses.NET 8.0, so make sure you have the correct
version.

## Technologies

Key Vault CLI is developed using the following technologies:

- [.NET](https://dotnet.microsoft.com/en-us/): The application is built on the .NET SDK.
- [C#](https://learn.microsoft.com/de-de/dotnet/csharp/): The primary programming language used.
- [Console Application](https://learn.microsoft.com/de-de/dotnet/core/tutorials/with-visual-studio?pivots=dotnet-8-0):
  The user interface is entirely CLI-based.

## How tos

For all the following **How tos** sections, you need to follow these steps first:

1. Clone this repository to your local machine.
2. Open a terminal or command prompt.
3. Navigate to the project directory.

### How to Run

Run the project using the following command:

```shell
dotnet run
```

You will be prompted to create a master password the first time you run the application.

### How to Run with Docker

To run the application using Docker, execute the following commands:

```shell
docker build . -t keyvaultcli
docker run -it --name KeyVaultCli keyvaultcli
```

### How to Build

Build the project using the following command:

```shell
dotnet build
```

### How to Test

To run the tests, use the following command:

```shell
dotnet test
```

To run a coverage report inside the cli and not the IDE run this

```shell
dotnet test /p:CollectCoverage=true --% /p:CoverletOutputFormat=\"opencover,lcov\"
```

### Counting Lines of Code

Count the lines of code using the following command in the root of the project:

```shell
chmod +x countlines.sh && ./countlines.sh
```

### Installing the CLI

1. **Package Generation:** To package the project, execute the command below:

````shell
dotnet pack
````

This will create a new folder named `nupkg` within the `KeyVaultCli.Presentation` project.

2.**Installation from Local Feed:** Install the package from a local NuGet feed with the subsequent command:

```shell
dotnet tool install --global --add-source ./src/KeyVaultCli.Presentation/nupkg KeyVaultCli.Presentation
```

3.**Verification of Installation:** Confirm the installed packages on your machine using:

```shell
dotnet tool list --global
```

You should observe the presence of the KeyVaultCli.Presentation package. Run your application by invoking:

```shell
kvc
```

#### Uninstalling the CLI

To remove the globally installed tool, execute the following command:

```shell
dotnet tool uninstall KeyVaultCli.Presentation
```

## Use Cases

In the following there are simple use cases for the Key Vault CLI, and why you would use it.

### Use Case 1: Managing Personal Passwords

#### Actors:

- **User**: A person who wants to securely manage their passwords and sensitive information.

#### Preconditions:

- The user has installed Key Vault CLI on their local machine.
- The user has set up a master password for their vault.

#### Main Flow:

1. **Run the Application**: The user runs the Key Vault CLI application by executing the command `dotnet run`.
2. **Authenticate**: The application prompts the user to enter their master password to unlock the vault.
3. **Access Passwords**: Once authenticated, the user can perform CRUD operations to manage their passwords and
   sensitive information.
    - *Create*: The user can add new password entries to the vault.
    - *Read*: The user can view existing password entries.
    - *Update*: The user can modify existing password entries if needed.
    - *Delete*: The user can remove password entries from the vault.
4. **Generate Strong Passwords**: The user can utilize the password generation feature to create strong, random
   passwords for their accounts.
5. **Search and Retrieval**: The user can easily search for specific passwords or information stored in the vault using
   the search feature.
6. **Logout**: Once the user has finished managing their passwords, they can log out of the application, via a 
   command or by closing the terminal.

#### Post conditions:

- The user's passwords and sensitive information are securely stored in the vault.
- The user can safely exit the application knowing that their data is protected by the master password.

### Use Case 2: Securely Storing Work Credentials

#### Actors:

- **Employee**: An individual working in an organization who needs to manage various work-related credentials securely.

#### Preconditions:

- The employee has access to the Key Vault CLI application.
- The employee has set up a master password for their vault.
- The employee has access to the work-related systems and services that require credentials.

#### Main Flow:

1. **Run the Application**: The employee executes the command `dotnet run` to start the Key Vault CLI application.
2. **Authenticate**: The application prompts the employee to enter their master password to unlock the vault.
3. **Access Work Credentials**: Once authenticated, the employee can perform CRUD operations to manage their
   work-related credentials.
    - *Create*: The employee adds entries for various work-related systems, such as company email, internal databases,
      or cloud services, along with their respective usernames and passwords.
    - *Read*: The employee can view the stored work credentials whenever needed.
    - *Update*: If the employee's work password changes or needs updating, they can modify the corresponding entry in
      the vault.
    - *Delete*: If access to a particular system or service is revoked or no longer required, the employee can delete
      the corresponding credential entry from the vault.
4. **Search and Retrieve**: The employee can quickly search for specific work-related credentials stored in the vault
   using the search feature, making it easy to retrieve the necessary information when logging into different systems or
   services.
5. **Logout**: Once the employee has finished accessing or updating their work credentials, they securely log out of the
   Key Vault CLI application.

#### Post conditions:

- The employee's work-related credentials are securely stored in the vault, ensuring access to critical systems and
  services while maintaining security standards set by the organization.

## UML Diagrams Generation

To generate UML diagrams for your .NET projects using PlantUML and JetBrains Rider, follow these simple steps:

### Installation Guide

Refer to the detailed installation guide by Khalid Abuhakmeh for setting up PlantUML with JetBrains Rider 
[here](https://khalidabuhakmeh.com/dotnet-class-diagrams-in-jetbrains-rider-with-plantuml)

### Generating UML Diagrams

Run the script to generate UML diagrams based on your project's code:

````shell
chmod +x generate-uml.sh && ./generate-uml.sh
````

After running the script, navigate to the output (`puml`) directory to find the generated UML diagrams.

## License

Key Vault CLI is open-source software released under the [MIT License](LICENSE). You are free to use, modify, and
distribute this software as per the terms of the license.
