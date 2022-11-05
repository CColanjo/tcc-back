using schedule_appointment_domain.Repositories;

namespace schedule_appointment_infra.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _context;
    protected RepositoryBase(ApplicationDbContext context) => _context = context;

    public async Task CreateAsync(TEntity obj) =>
        await _context.Set<TEntity>().AddAsync(obj);

    public void Update(TEntity obj) =>
        _context.Set<TEntity>().Update(obj); 
}