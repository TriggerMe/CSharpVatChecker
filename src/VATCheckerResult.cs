namespace TriggerMe.VAT
{
    /// <summary>
    /// Represents a Checker Result
    /// </summary>
    public class VATCheckerResult
    {
        /// <summary>
        /// Name of the organisation
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Country Code the organisation belongs to
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Registered VAT of the organisation
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// If the VAT Number valid?
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// VAT Number of the organisation
        /// </summary>
        public string VatNumber { get; set; }
    }
}