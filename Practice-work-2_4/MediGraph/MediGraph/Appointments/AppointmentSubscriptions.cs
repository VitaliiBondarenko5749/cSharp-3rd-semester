using MediGraph.Data;
using MediGraph.DataLoader;

namespace MediGraph.Appointments
{
#pragma warning disable
    [ExtendObjectType(Name = "Subscription")]
    public class AppointmentSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Appointment> OnAppointmentScheduledAsync(
            [EventMessage] Guid appointmentId,
            AppointmentByIdDataLoader appointmentById,
            CancellationToken cancellationToken) =>
            appointmentById.LoadAsync(appointmentId, cancellationToken);
    }
}