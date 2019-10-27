# From Scripting to Framework course on TestAutomationU.com

## Chapter 1 - Machine Setup

1. Requirements
    - .NET Core version 2.2 (latest at time of recording) or greater
    - VS Code

2. Make a new project called `scripting-to-framework` and open it in VS Code

3. Install Extensions in VS Code
    - C# by Microsoft
    - PackSharp by Carlos Kidman

4. Open the Command Palette for each of these commands
    - `PackSharp: Create New Project` > select `Class Library` > call the Project "Framework"
    - `PackSharp: Create New Project` > select `Class Library` > call the Project "Royale"
    - `PackSharp: Create New Project` > select `NUnit 3 Test Project` > call the Project "Royale.Tests"
    - `PackSharp: Create New Project` > select `Solution File` > call the Project "StatsRoyale"

> NOTE: The Solution (.sln) file will manage the Project (.csproj) files while the Project files handle their own packages and dependencies. As you add things, this is all handled for you! Very cool.

5. Add the Projects to the Solution. Run these commands in the Terminal:
    - `$ dotnet sln add Framework`
    - `$ dotnet sln add Royale`
    - `$ dotnet sln add Royale.Tests`

6. Build the Solution so we know everything is working
    - `$ dotnet build`

7. Open the `UnitTest1.cs` file in the Royale.Tests project
    - C# will "spin up" so you can start coding in C# and get helpful completions, hints, etc.
    - VS Code will ask if you want "Required assets to build and debug". Add them by clicking the "Yes" button.
        - If you do not get this immediately, try closing VS Code and reopening the project.
        - This will add a `.vscode` folder to your solution, but this is required to run and debug the tests.

8. Run the Tests
    - `$ dotnet test`
    - This will run all the tests, but you only have one right now. It should pass.
