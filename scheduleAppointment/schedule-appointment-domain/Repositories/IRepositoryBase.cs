namespace schedule_appointment_domain.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task CreateAsync(TEntity obj);
    void Update(TEntity obj);
 
}