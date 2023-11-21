using MediGraph.Data;
using MediGraph.DataLoader;

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