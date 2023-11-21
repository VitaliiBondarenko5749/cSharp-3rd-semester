using MediGraph.Data;
using Microsoft.EntityFrameworkCore;

namespace MediGraph.DataLoader
{
    public class PatientByIdDataLoader : BatchDataLoader<Guid, Patient>
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

        public PatientByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(batchScheduler)
        {
            this.dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<Guid, Patient>> LoadBatchAsync(IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext = dbContextFactory.CreateDbContext();

            return await dbContext.Patients
                .Where(d => keys.Contains(d.Id))
                .ToDictionaryAsync(d => d.Id, cancellationToken);
        }
    }
}