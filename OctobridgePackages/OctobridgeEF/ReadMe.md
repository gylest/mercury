# OctobridgeEF

This is a .NET `Class Library` project containing Entity Framework classes to access the `Octobridge` database.  
This project is for publishing as a NuGet package in GitHub.  

---

## Project Overview

**Purpose:**  
Entity Framework code for working with Octobridge database.  

**Technologies:**  
- `EF Core Power Tools` used to generate models from the database.  

**Setup & Usage:**  
- Project type: `Class Library (C#)`  
- NuGet packages:  
  - Microsoft.EntityFrameworkCore.SqlServer  
- Ensure the "Octobridge" database exists and know the connection string.  
- Right click a project in Solution Explorer, select EF Core Power Tools/Reverse Engineer or press Ctrl+Shift+A, select the Data folder, and select the EF Core Database First Wizard.  
- Connect to your existing database via the Add button, select the desired database objects, and complete the wizard to generate models.  


## Publish to GitHub Packages

One time operation to add GitHub Packages as a NuGet source:  

```cmd
dotnet nuget add source --username gylest --password YOUR_GITHUB_PAT --store-password-in-clear-text --name github "https://nuget.pkg.github.com/gylest/index.json"
```
Push the package to GitHub Packages:  

```cmd
dotnet nuget push bin/Release/OctobridgeEF.1.2.0.nupkg --source github
```
