using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using MediGraph.Common;
using MediGraph.Data;
using MediGraph.DataLoader;
using MediGraph.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediGraph.Doctors
{
#pragma warning disable
    [ExtendObjectType(Name = "Mutation")]
#pragma warning enable
    public class DoctorMutations
    {
        [UseApplicationDbContext]
        public async Task<AddDoctorPayload> AddDoctorAsync(AddDoctorInput input,
            [ScopedService] ApplicationDbContext context, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(input.firstName) || string.IsNullOrEmpty(input.lastName))
            {
                return new AddDoctorPayload(new UserError("Full name is incorrect!", "ERROR_NAME_VALID"));
            }

            Doctor doctor = new()
            {
                Id = Guid.NewGuid(),
                FirsName = input.firstName,
                LastName = input.lastName
            };

            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync(cancellationToken);

            return new AddDoctorPayload(doctor);
        }

        [UseApplicationDbContext]
        public async Task<ScheduleDoctorPayload> UpdateDoctorByIdAsync(ScheduleDoctorInput input,
            [ScopedService] ApplicationDbContext context, DoctorByIdDataLoader doctorById,
            CancellationToken cancellationToken, [Service]ITopicEventSender eventSender)
        {
            Doctor? doctor = await doctorById.LoadAsync(input.id, cancellationToken);

            if(doctor is null)
            {
                return new ScheduleDoctorPayload(new UserError("Doctor's id was not found in the database", "NOT_FOUND"));
            }

            if (string.IsNullOrEmpty(input.firstName) || string.IsNullOrEmpty(input.lastName))
            {
                return new ScheduleDoctorPayload(new UserError("Full name is incorrect!", "ERROR_NAME_VALID"));
            }

            doctor.FirsName = input.firstName;
            doctor.LastName = input.lastName;

            context.Doctors.Update(doctor);

            await context.SaveChangesAsync(cancellationToken);
            await eventSender.SendAsync(nameof(DoctorSubscriptions.OnDoctorScheduledAsync), doctor.Id);

            return new ScheduleDoctorPayload(doctor);
        }

        [UseApplicationDbContext]
        public async Task<string> DeleteDoctorByIdAsync(Guid id, [ScopedService] ApplicationDbContext context,
            DoctorByIdDataLoader doctorById, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorById.LoadAsync(id, cancellationToken);

            if(doctor is null)
            {
                return "Doctor's id was not found in the database!";
            }

            context.Doctors.Remove(doctor);
            await context.SaveChangesAsync(cancellationToken);

            return "Doctor has been removed!";
        }
    }
}