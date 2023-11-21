using MediGraph.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace MediGraph.DataLoader
{
    public class AppointmentByIdDataLoader : BatchDataLoader<Guid, Appointment>
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

        public AppointmentByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Appointment>> LoadBatchAsync(IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();

            return await dbContext.Appointments
                .Where(d => keys.Contains(d.Id))
                .ToDictionaryAsync(d => d.Id, cancellationToken);
        }
    }
}