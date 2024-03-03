namespace CommonInterfaces.Services;

public interface ITransactionService
{
    T Transactional<T>(Func<T> action);
}