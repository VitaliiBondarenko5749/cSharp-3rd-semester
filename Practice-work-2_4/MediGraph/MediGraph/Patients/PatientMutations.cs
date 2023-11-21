using HotChocolate.Subscriptions;
using MediGraph.Data;
using MediGraph.DataLoader;
using MediGraph.Doctors;
using MediGraph.Extensions;

namespace MediGraph.Patients
{
#pragma warning disable
    [ExtendObjectType(Name = "Mutation")]
#pragma warning enable
    public class PatientMutations
    {
        [UseApplicationDbContext]
        public async Task<AddPatientPayload> AddPatientAsync(AddPatientInput input,
            [ScopedService] ApplicationDbContext context, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.firstName) || string.IsNullOrEmpty(input.lastName))
            {
                return new AddPatientPayload(new Common.Error("Full name is incorrect!", "ERROR_NAME_VALID"));
            }

            Patient patient = new()
            {
                Id = Guid.NewGuid(),
                FirsName = input.firstName,
                LastName = input.lastName
            };

            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();

            return new AddPatientPayload(patient);
        }

        [UseApplicationDbContext]
        public async Task<SchedulePatientPayload> UpdatePatientByIdAsync(SchedulePatientInput input,
            [ScopedService] ApplicationDbContext context, PatientByIdDataLoader patientById,
            CancellationToken cancellationToken, [Service]ITopicEventSender eventSender)
        {
            Patient? patient = await patientById.LoadAsync(input.id, cancellationToken);

            if (patient is null)
            {
                return new SchedulePatientPayload(new Common.Error("Doctor's id was not found in the database", "NOT_FOUND"));
            }

            if (string.IsNullOrEmpty(input.firstName) || string.IsNullOrEmpty(input.lastName))
            {
                return new SchedulePatientPayload(new Common.Error("Full name is incorrect!", "ERROR_NAME_VALID"));
            }

            patient.FirsName = input.firstName;
            patient.LastName = input.lastName;

            context.Patients.Update(patient);

            await context.SaveChangesAsync(cancellationToken);
            await eventSender.SendAsync(nameof(PatientSubscriptions.OnPatientScheduledAsync), patient.Id);

            return new SchedulePatientPayload(patient);
        }

        [UseApplicationDbContext]
        public async Task<string> DeletePatientByIdAsync(Guid id, [ScopedService] ApplicationDbContext context,
            PatientByIdDataLoader patientById, CancellationToken cancellationToken)
        {
            Patient? patient = await patientById.LoadAsync(id, cancellationToken);

            if (patient is null)
            {
                return "Doctor's id was not found in the database!";
            }

            context.Patients.Remove(patient);
            await context.SaveChangesAsync(cancellationToken);

            return "Doctor has been removed!";
        }
    }
}