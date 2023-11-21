using MediGraph.Data;

namespace MediGraph.Doctors
{
    public class ScheduleDoctorPayload : DoctorPayloadBase
    {
        public ScheduleDoctorPayload(Doctor doctor) : base(doctor) { }
        public ScheduleDoctorPayload(Common.Error error) : base(new[] { error }) 
        { }
    }
}