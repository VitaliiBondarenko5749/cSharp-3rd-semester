using MediGraph.Common;
using MediGraph.Data;

#pragma warning disable

namespace MediGraph.Doctors
{
    public class AddDoctorPayload : Payload
    {
        public AddDoctorPayload(Doctor doctor)
        {
            Doctor = doctor;
        }

        public AddDoctorPayload(UserError error) : base(new[] { error }) 
        { }

        public Doctor? Doctor { get; init; }
    }
}