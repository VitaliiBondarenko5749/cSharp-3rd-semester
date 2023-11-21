using MediGraph.Data;
using MediGraph.DataLoader;
using MediGraph.Extensions;

namespace MediGraph.Patients
{
#pragma warning disable
    [ExtendObjectType(Name = "Query")]
#pragma warning enable
    public class PatientQueries
    {
        [UseApplicationDbContext]
        [UseFiltering]
        public IQueryable<Patient> GetPatients([ScopedService] ApplicationDbContext context)
            => context.Patients;

        public Task<Patient> GetPatientByIdAsync([ID(nameof(Patient))] Guid id, PatientByIdDataLoader patientById,
            CancellationToken cancellationToken)
            => patientById.LoadAsync(id, cancellationToken);
    }
}