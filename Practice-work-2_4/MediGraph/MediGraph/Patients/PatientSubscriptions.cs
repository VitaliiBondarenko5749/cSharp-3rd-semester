using HotChocolate;
using HotChocolate.Types;
using MediGraph.Data;
using MediGraph.DataLoader;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediGraph.Patients
{
#pragma warning disable
    [ExtendObjectType(Name = "Subscription")]
    public class PatientSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Patient> OnPatientScheduledAsync(
            [EventMessage] Guid patientId,
            PatientByIdDataLoader patientById,
            CancellationToken cancellationToken) =>
            patientById.LoadAsync(patientId, cancellationToken);
    }
}