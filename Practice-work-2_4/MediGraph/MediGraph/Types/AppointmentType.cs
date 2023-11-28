using HotChocolate;
using HotChocolate.Types;
using MediGraph.Data;
using MediGraph.DataLoader;
using System.Threading;
using System.Threading.Tasks;

namespace MediGraph.Types
{
    public class AppointmentType : ObjectType<Appointment>
    {
        protected override void Configure(IObjectTypeDescriptor<Appointment> descriptor)
        {
#pragma warning disable
            descriptor
                .ImplementsNode()
                .IdField(p => p.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<AppointmentByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
#pragma warning enable

            descriptor
                .Field(a => a.Patient)
                .ResolveWith<AppointmentResolver>(ar => ar.GetPatientByIdAsync(default!, default!, default))
                .Name("Patients");

            descriptor
                .Field(a => a.Doctor)
                .ResolveWith<AppointmentResolver>(ar => ar.GetDoctorByIdAsync(default!, default!, default))
                .Name("Doctors");
        }

        private class AppointmentResolver 
        {
            public async Task<Patient> GetPatientByIdAsync([Parent]Appointment appointment, PatientByIdDataLoader patientById,
                CancellationToken cancellationToken)
                => await patientById.LoadAsync(appointment.PatientId, cancellationToken);

            public async Task<Doctor> GetDoctorByIdAsync([Parent]Appointment appointment, DoctorByIdDataLoader doctorById,
                CancellationToken cancellationToken)
                => await doctorById.LoadAsync(appointment.DoctorId, cancellationToken);
        }
    }
}