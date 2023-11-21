using MediGraph.Data;
using MediGraph.DataLoader;
using MediGraph.Extensions;

namespace MediGraph.Appointments
{
#pragma warning disable
    [ExtendObjectType(Name = "Query")]
#pragma warning enable
    public class AppointmentQueries 
    {
        [UseApplicationDbContext]
        [UseFiltering]
        public IQueryable<Appointment> GetAppointments([ScopedService] ApplicationDbContext context)
            => context.Appointments;

        public Task<Appointment> GetAppointmentByIdAsync([ID(nameof(Appointment))] Guid id,
            AppointmentByIdDataLoader appointmentById, CancellationToken cancellationToken)
            => appointmentById.LoadAsync(id, cancellationToken);

        [UseApplicationDbContext]
        public IQueryable<Appointment> GetAppointmentsByDateTimeAsync(DateTime dateTime,
            [ScopedService] ApplicationDbContext context, CancellationToken cancellationToken)
            => context.Appointments
            .Where(a => a.DateTime.Equals(dateTime));

        [UseApplicationDbContext]
        public IQueryable<Appointment> GetAppointmentsForPatientId(Guid patientId,
            [ScopedService] ApplicationDbContext context, CancellationToken cancellationToken)
            => context.Appointments
            .Where(a => a.PatientId.Equals(patientId));

        [UseApplicationDbContext]
        public IQueryable<Appointment> GetAppointmentsForDoctorId(Guid doctorId,
            [ScopedService] ApplicationDbContext context, CancellationToken cancellationToken)
            => context.Appointments
            .Where(a => a.DoctorId.Equals(doctorId));
    }
}