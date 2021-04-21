namespace MyBank.Services
{
    public interface ITransactionService
    {
        void AddToAccount(int accountId, string currencyType, float amount);
        float ConvertToCurrency(float amount, string currencyConstant);
        void DedudctFromAccount(int accountId, string currencyType, float amount);
        bool validBalanceAmount(int accountId, float amount, string currency);
        bool validUsername(string username);
    }
}