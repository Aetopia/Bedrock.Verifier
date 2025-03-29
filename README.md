# Bedrock Verifier

A tool to verify the licensing status & package integrity of Minecraft: Bedrock Edition.

# Usage

- Download from [GitHub Releases](https://github.com/Aetopia/Bedrock.Verifier/releases)

- Run the program.

- Verify the licensing status & package integrity of Minecraft: Bedrock Edition.

    |Property|Description|
    |-|-|
    |License|Determines if an account has a license for Minecraft: Bedrock Edition.|
    |Release|Determines if Minecraft's app package is untampered.|
    |Preview|Determines if Minecraft: Preview's app package is untampered.|

## Build
1. Download the following:

    - [.NET SDK](https://dotnet.microsoft.com/en-us/download)

2. Run the following command to compile:

    ```cmd
    dotnet publish "src\Bedrock.Verifier.csproj"
    ```