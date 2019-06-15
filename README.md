[![NuGet](https://img.shields.io/nuget/v/TriggerMe.VATChecker.svg)](https://www.nuget.org/packages/TriggerMe.VATChecker)

# CSharpVatChecker
A simple C# VAT Checker. Of a supplied Country Code and VAT Number, this library checks the VIES database (http://ec.europa.eu/taxation_customs/vies/). 

*I couldn't find a free REST implementation.*

# Installing
TriggerMe.VATChecker is available from [NuGet](https://www.nuget.org/packages/TriggerMe.VATChecker).

`dotnet add package TriggerMe.VATChecker`

# Usage

```csharp
var vatQuery = new VATQuery();
var vatResult = await vatQuery.CheckVATNumberAsync("IE", "3041081MH"); // The Squarespace VAT Number

Console.WriteLine(vatResult.Valid); // Is the VAT Number valid?
Console.WriteLine(vatResult.Name);  // Name of the organisation
```

# Building
Compile the library using `dotnet build`. For reference, check the sample project.

# Contribution
Feel free to suggest edits, features, issues and pull requests.
