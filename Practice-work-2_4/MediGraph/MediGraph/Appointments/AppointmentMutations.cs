using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using MediGraph.Common;
using MediGraph.Data;
using MediGraph.DataLoader;
using MediGraph.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediGraph.Appointments
{
#pragma warning disable
    [ExtendObjectType(Name="Mutation")]
#pragma warning enable
    public class AppointmentMutations
    {
        [UseApplicationDbContext]
        public async Task<AddAppointmentPayload> AddAppointmentAsync(AddAppointmentInput input,
            [ScopedService] ApplicationDbContext context, DoctorByIdDataLoader doctorById,
            PatientByIdDataLoader patientById, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorById.LoadAsync(input.doctorId, cancellationToken);

            if (doctor is null)
            {
                return new AddAppointmentPayload(new UserError("Doctor's Id was not found in the database!", "NOT_FOUND"));
            }

            Patient? patient = await patientById.LoadAsync(input.patientId, cancellationToken);

            if (patient is null)
            {
                return new AddAppointmentPayload(new UserError("Patient's Id was not found in the database!", "NOT_FOUND"));
            }

            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                DoctorId = input.doctorId,
                PatientId = input.patientId
            };

            await context.Appointments.AddAsync(appointment);

            await context.SaveChangesAsync(cancellationToken);

            return new AddAppointmentPayload(appointment);
        }

        [UseApplicationDbContext]
        public async Task<ScheduleAppointmentPayload> UpdateAppointmentAsync(ScheduleAppointmentInput input,
            [ScopedService] ApplicationDbContext context, AppointmentByIdDataLoader appointmentById,
            CancellationToken cancellationToken, [Service]ITopicEventSender eventSender)
        {
            Appointment? appointment = await appointmentById.LoadAsync(input.id, cancellationToken);

            if (appointment is null)
            {
                return new ScheduleAppointmentPayload(new UserError("Appointment's id was not found in the database!",
                    "NOT_FOUND"));
            }

            appointment.DateTime = input.dateTime;

            context.Appointments.Update(appointment);
            await context.SaveChangesAsync(cancellationToken);
            await eventSender.SendAsync(nameof(AppointmentSubscriptions.OnAppointmentScheduledAsync), appointment.Id);

            return new ScheduleAppointmentPayload(appointment);
        }

        [UseApplicationDbContext]
        public async Task<string> DeleteAppointmentByIdAsync(Guid id, [ScopedService] ApplicationDbContext context,
            AppointmentByIdDataLoader appointmentById, CancellationToken cancellationToken)
        {
            Appointment? appointment = await appointmentById.LoadAsync(id, cancellationToken);

            if (appointment is null)
            {
                return "Appointment's id was not found in the database!";
            }

            context.Appointments.Remove(appointment);

            await context.SaveChangesAsync(cancellationToken);

            return "Appointment has been removed!";
        }

        [UseApplicationDbContext]
        public async Task<string> DeleteAppointmentsByIdAsync(ICollection<Guid> ids, 
            [ScopedService] ApplicationDbContext context, AppointmentByIdDataLoader appointmentById,
            CancellationToken cancellationToken)
        {
            int counter = 0;

            foreach (Guid id in ids)
            {
                Appointment? appointment = await appointmentById.LoadAsync(id, cancellationToken);

                if (appointment is null)
                {
                    continue;
                }

                context.Appointments.Remove(appointment);

                counter++;
            }

            await context.SaveChangesAsync(cancellationToken);

            return $"Deleted appointments: {counter}.";
        }
    }
}