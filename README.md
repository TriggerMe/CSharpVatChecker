# CSharpVatChecker
A simple C# VAT Checker. Of a supplied Country Code and VAT Number, this class checks the VIES database (http://ec.europa.eu/taxation_customs/vies/). 

*I couldn't find a free REST implementation.*

# Usage
Compile the `VatQuery` file into your project and use the class as outlined below.

```csharp
var vatResult = await VatQuery.CheckVATNumberAsync("IE", "3041081MH"); // The Squarespace VAT Number

Console.WriteLine(vatResult.Valid); // Is the VAT Number valid?
Console.WriteLine(vatResult.Name);  // Name of the organisation
```

# Contribution
Feel free to suggest edits, features, issues and pull requests.
