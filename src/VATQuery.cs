/*

Copyright 2018 James Woodall (james@triggerme.io)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

 */

using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TriggerMe.VAT
{
    /// <summary>
    /// Implements the VAT Query operations
    /// </summary>
    public class VATQuery : IVATQuery
    {
        // Cache the Http Client
        static HttpClient DefaultClient = new HttpClient
        {
            BaseAddress = new Uri(Endpoint)
        };

        // This doesn't work as HTTPS (???)
        const string Endpoint = "http://ec.europa.eu/taxation_customs/vies/services/checkVatService";

        /// <summary>
        /// Checks a VAT number for a specified country code and VAT Number
        /// </summary>
        /// <param name="country">2 digit Country Code (i.e. GB/FR/IE ...)</param>
        /// <param name="vatNumber">VAT number to check (without country code)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A valid VatCheckerResult. Check "Valid" field for success/failure.</returns>
        public async Task<VATCheckerResult> CheckVATNumberAsync(string country, string vatNumber,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(country))
                throw new ArgumentNullException(nameof(country));

            if (string.IsNullOrEmpty(vatNumber))
                throw new ArgumentNullException(nameof(vatNumber));

            XNamespace rootNs = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace checkVatNs = "urn:ec.europa.eu:taxud:vies:services:checkVat:types";

            var envelope = new XElement(rootNs + "Envelope");
            var body = new XElement(rootNs + "Body");
            var checkVat = new XElement(checkVatNs + "checkVat");

            checkVat.Add(new XElement(checkVatNs + "countryCode", country));
            checkVat.Add(new XElement(checkVatNs + "vatNumber", vatNumber));

            body.Add(checkVat);
            envelope.Add(body);

            var strContent = new StringContent(envelope.ToString(), Encoding.UTF8, "text/xml");

            using (var resp = await DefaultClient.PostAsync("", strContent, cancellationToken))
            {
                var rStr = await resp.Content.ReadAsStringAsync();

                var returnElem = XElement.Parse(rStr);

                var checkVatResponse = returnElem?.Element(rootNs + "Body")?.Element(checkVatNs + "checkVatResponse");

                return new VATCheckerResult
                {
                    CountryCode = checkVatResponse?.Element(checkVatNs + "countryCode")?.Value,
                    VatNumber = checkVatResponse?.Element(checkVatNs + "vatNumber")?.Value,
                    Valid = bool.TryParse(checkVatResponse?.Element(checkVatNs + "valid")?.Value, out var b) && b,
                    Name = checkVatResponse?.Element(checkVatNs + "name")?.Value,
                    Address = checkVatResponse?.Element(checkVatNs + "address")?.Value
                };
            }
        }
    }
}
