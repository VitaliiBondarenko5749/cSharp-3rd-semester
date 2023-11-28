using HotChocolate;
using HotChocolate.Types;
using MediGraph.Data;
using MediGraph.DataLoader;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediGraph.Doctors
{
#pragma warning disable
    [ExtendObjectType(Name = "Subscription")]
    public class DoctorSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Doctor> OnDoctorScheduledAsync(
            [EventMessage] Guid doctorId,
            DoctorByIdDataLoader doctorById,
            CancellationToken cancellationToken) =>
            doctorById.LoadAsync(doctorId, cancellationToken);
    }
}