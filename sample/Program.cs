using System;
using System.Threading.Tasks;
using TriggerMe.VAT;

namespace TriggerMe.VATChecker.Sample
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var vatQuery = new VATQuery();
            var vatResult = await vatQuery.CheckVATNumberAsync("IE", "3041081MH"); // The Squarespace VAT Number

            Console.WriteLine(vatResult.Valid); // Is the VAT Number valid?
            Console.WriteLine(vatResult.Name);  // Name of the organisation
        }
    }
}
