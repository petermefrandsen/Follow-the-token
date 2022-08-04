using Common.Models;

namespace Logic
{
    public interface IBEP20Logic
    {
        Task<BEP20TokenTransactionResponse> GetBEP20TokenTransactions(BEP20TokenTransactionRequest request);
        Task<BEP20TokenTransactionResponseSimplified> GetBEP20TokenTransactionsSimplified(BEP20TokenTransactionRequest request);
        Task<List<BscScanTokenTransfer>> GetTransactionsForAddress(string address, string contract);
    }
}
