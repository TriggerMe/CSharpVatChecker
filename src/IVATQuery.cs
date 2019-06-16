using System.Threading;
using System.Threading.Tasks;

namespace TriggerMe.VAT
{
    /// <summary>
    /// Interface defining the basic VATQuery operations
    /// </summary>
    public interface IVATQuery
    {
        /// <summary>
        /// Checks a VAT number
        /// </summary>
        /// <param name="country">2 digit Country Code (i.e. GB/FR/IE ...)</param>
        /// <param name="vatNumber">VAT number to check (without country code)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>A VAT Checker Result</returns>
        Task<VATCheckerResult> CheckVATNumberAsync(string country, string vatNumber,
            CancellationToken cancellationToken = default);
    }
}