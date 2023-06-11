using SimApi.Data.Context;

namespace SimApi.Data.Repository;

public class EfTransactionRepository : IEfTransactionRepository
{
    private readonly SimEfDbContext dbContext;

    public EfTransactionRepository(SimEfDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<TransactionView> GetAll()
    {
        return dbContext.Set<TransactionView>().ToList();
    }

    public List<TransactionView> GetByAccountId(int accountId)
    {
        return dbContext.Set<TransactionView>().Where(x => x.AccountId == accountId).ToList();
    }

    public List<TransactionView> GetByCustomerId(int customerId)
    {
        return dbContext.Set<TransactionView>().Where(x => x.CustomerId == customerId).ToList();
    }

    public IQueryable<TransactionView> GetById(int id)
    {
        return dbContext.Set<TransactionView>().Where(x => x.Id == id);
    }

    public List<TransactionView> GetByReferenceNumber(string referenceNumber)
    {
        return dbContext.Set<TransactionView>().Where(x => x.ReferenceNumber == referenceNumber).ToList();
    }
}
