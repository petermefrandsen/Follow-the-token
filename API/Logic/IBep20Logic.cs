using Common.Models;

namespace Logic
{
    public interface IBep20Logic
    {
        Task<Bep20TokenTransactionResponse> GetBep20TokenTransactions(Bep20TokenTransactionRequest request);
        Task<Bep20TokenTransactionResponseSimplified> GetBep20TokenTransactionsSimplified(Bep20TokenTransactionRequest request);
        Task<List<BscScanTokenTransfer>> GetTransactionsForAddress(string address, string contract);
    }
}
