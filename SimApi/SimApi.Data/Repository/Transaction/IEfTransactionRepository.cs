namespace SimApi.Data.Repository;

public interface IEfTransactionRepository
{
    List<TransactionView> GetAll();
    IQueryable<TransactionView> GetById(int id);
    List<TransactionView> GetByReferenceNumber(string referenceNumber);
    List<TransactionView> GetByCustomerId(int customerId);
    List<TransactionView> GetByAccountId(int accountId);
}
