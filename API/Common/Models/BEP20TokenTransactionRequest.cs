namespace Common.Models
{
    public class BEP20TokenTransactionRequest
    {
        /// <summary>
        /// (optional) BscScan.com API key, can be obtained at https://bscscan.com/apis
        /// </summary>
        //public string? BscScanAPIKey { get; set; }
        
        /// <summary>
        /// Token contract for exploration
        /// </summary>
        public string? BEP20TokenContract { get; set; }
        
        /// <summary>
        /// Address for intial lookup
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Addresses to ignore, e.g. liquitidy pools and alike
        /// </summary>
        public List<BEP20TokenTransactionIgnoreAddress>? IgnoreAddresses { get; set; }

        /// <summary>
        /// The depth of addresses explored, i.e. 
        /// - 0 only looks up the requested address
        /// - 1 looks up all transactions of those addresses who recieved tokens from the inital address
        /// - 2 looks up all transactions of those addresses found in (1)
        /// - n looks up all transaction of those found in n-1
        /// </summary>
        public int ExplorationDepth { get; set; }
    }
}