using schedule_appointment_domain;

namespace schedule_appointment_infra
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context) =>
            _context = context;

        public async Task<bool> Commit()
        {
            var success = (await _context.SaveChangesAsync()) > 0;

            // Possibility to dispatch domain events, etc

            return success;
        }

        public Task Rollback()
        {
            // Rollback anything, if necessary
            return Task.CompletedTask;
        }

        ~UnitOfWork() =>
            Dispose();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
