using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Doctors
{
    public class ScheduleDoctorPayload : DoctorPayloadBase
    {
        public ScheduleDoctorPayload(Doctor doctor) : base(doctor) { }
        public ScheduleDoctorPayload(UserError error) : base(new[] { error }) 
        { }
    }
}