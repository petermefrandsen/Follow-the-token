using Common.Models;

namespace Logic
{
    public interface IBep20Logic
    {
        Task<Bep20TokenTransactionResponse> GetBEP20TokenTransactions(Bep20TokenTransactionRequest request);
        Task<Bep20TokenTransactionResponseSimplified> GetBEP20TokenTransactionsSimplified(Bep20TokenTransactionRequest request);
        Task<List<BscScanTokenTransfer>> GetTransactionsForAddress(string address, string contract);
    }
}
