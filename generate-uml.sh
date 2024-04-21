# Generate UML diagrams using puml-gen for a .NET project

dotnet puml-gen ./src ./uml -dir -createAssociation

# Command breakdown:
# dotnet puml-gen ./src ./uml -dir -public -createAssociation

# Invoke the puml-gen tool using the dotnet command (assuming it's a local tool)
# You can omit 'dotnet' if the tool is installed globally.

# Explanation of Flags:
# -dir: Specifies that the input and output paths are directories, not individual files
# -public: Filters to include only public classes in the UML diagram
# -createAssociation: Ensures associated classes (e.g., linked by relationships) are visualized

# Note: Adjust the input path (./src), output path (./uml), and flags based on your project's structure and requirements.
# This command can be executed in JetBrains Rider's terminal or integrated into an MSBUILD target.
# Alternatively, create a run configuration in JetBrains Rider using this command for easier execution.
# Ensure the working directory in Rider's run configuration is set to the root of your solution.
