using MediGraph.Data;
using Microsoft.EntityFrameworkCore;

namespace MediGraph.DataLoader
{
    public class DoctorByIdDataLoader : BatchDataLoader<Guid, Doctor>
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

        public DoctorByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<Guid, Doctor>> LoadBatchAsync(IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();

            return await dbContext.Doctors
                .Where(d => keys.Contains(d.Id))
                .ToDictionaryAsync(d => d.Id, cancellationToken);
        }
    }
}