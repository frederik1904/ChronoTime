using System.Transactions;
using CommonInterfaces.Services;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Common.Services;

public class TransactionServiceService(ChronoContext context) : ITransactionService
{
    public T Transactional<T>(Func<T> action)
    {
        using var transactionScope = new TransactionScope();

        var result = action();

        context.SaveChanges();
        transactionScope.Complete();

        return result;
    }
}