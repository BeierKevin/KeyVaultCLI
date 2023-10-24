# Key-Vault-CLI

Key Vault CLI (*kvc*) is a command line interface password manager developed in C#. This open-source project helps you
securely manage your passwords and sensitive information while providing a user-friendly and efficient CLI-based
interface. Below, you'll find information on how to run, build, the technologies used, and the features covered by Key
Vault CLI.

## Features

Key Vault CLI comes with a variety of features to help you manage your passwords and sensitive information. Here's a
list of features, marked with checkmarks (✔), crosses (❌) to indicate the status of each feature, and Work in Progress (
🚧) to indicate features that are currently being developed or planned for future releases:

- ❌ **Master Password**: Secure your Vault with a master password.
- ❌ **CRUD Operations**: Create, read, update, and delete password entries.
- ❌ **Password Generation**: Generate strong, random passwords.
- ❌ **Password Storage**: Safely store your passwords and sensitive information.
- ❌ **Encryption**: Passwords are securely encrypted before storage.
- ❌ **Search and Retrieval**: Easily search and retrieve stored passwords.
- ❌ **Multi-Platform Support**: Key Vault CLI can be installed and used on all the major platforms (Windows, MacOS and
  Linux).
- ❌ **Backup and Restore**: Backup and restore your password database.
- ❌ **Category Organization**: Organize your passwords into categories for better management.
- ❌ **Password Sharing**: Share passwords securely with other users.

## Technologies

Key Vault CLI is developed using the following technologies:

- .NET Core: The application is built on the .NET Core framework.
- C#: The primary programming language used.
- Console Application: The user interface is entirely CLI-based.

## Prerequisites

Key Vault CLI is built on the .NET Core framework. To run the application, you'll need to have the .NET Core SDK
installed on your local machine. You can find the latest version of the
SDK [here](https://dotnet.microsoft.com/download).

## How tos

For all the following **How tos** sections, you need to follow these steps first:

1. Clone this repository to your local machine.
2. Open a terminal or command prompt.
3. Navigate to the project directory.

### How to Install

*Tbd* (installing the CLI on your local machine)

### How to Run

Run the project using the following command:

```shell
dotnet run
```

You will be prompted to create a master password the first time you run the application.

### How to Build

Build the project using the following command:

```shell
dotnet build
```

### How to Test

*Tbd* (how to run the tests)

## License

Key Vault CLI is open-source software released under the [MIT License](LICENSE). You are free to use, modify, and
distribute this software as per the terms of the license.
