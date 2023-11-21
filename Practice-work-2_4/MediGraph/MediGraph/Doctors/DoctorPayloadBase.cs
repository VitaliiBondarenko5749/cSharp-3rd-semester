using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Doctors
{
    public class DoctorPayloadBase : Payload
    {
        protected DoctorPayloadBase(Doctor doctor)
        {
            Doctor = doctor;
        }

        protected DoctorPayloadBase(IReadOnlyList<Common.Error> errors) : base(errors)
        {
        }

        public Doctor? Doctor { get; }
    }
}