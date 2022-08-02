namespace Common.Models
{
    public class BEP20TokenTransactionIgnoreAddress
    {
        /// <summary>
        /// Address to ignore
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// (optional) Name to identify address by
        /// </summary>
        public string? Name { get; set; }
    }
}