using CommonInterfaces.Models.BaseEntities;

namespace Repository.Repositories;

public abstract class BaseRepository<T> where T : class, IBaseEntity
{
    protected ChronoContext ChronoContext { get; }

    protected BaseRepository(ChronoContext chronoContext)
    {
        ChronoContext = chronoContext;
    }

    public T? GetById(Guid id)
    {
        return ChronoContext.Find<T>(id);
    }

    public List<T> GetItemsById(List<Guid> ids)
    {
        return ChronoContext.Set<T>().Where(arg => ids.Contains(arg.Id)).ToList();
    }

    public T New(T element)
    {
        return (T)ChronoContext.Add(new object()).Entity;
    }

    public T Save(T element)
    {
        return ChronoContext.Add(element).Entity;
    }

    public void Remove(T element)
    {
        ChronoContext.Remove(element);
    }
}