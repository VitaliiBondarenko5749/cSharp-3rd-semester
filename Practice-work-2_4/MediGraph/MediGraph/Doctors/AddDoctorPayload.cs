using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Doctors
{
    public class AddDoctorPayload : Payload
    {
        public AddDoctorPayload(Doctor doctor)
        {
            Doctor = doctor;
        }

        public AddDoctorPayload(Common.Error error) : base(new[] { error }) 
        { }

        public Doctor? Doctor { get; init; }
    }
}