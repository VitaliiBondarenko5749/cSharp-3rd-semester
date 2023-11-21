using MediGraph.Data;

namespace MediGraph.Patients
{
    public class SchedulePatientPayload : PatientPayloadBase
    {
        public SchedulePatientPayload(Patient patient) : base(patient) { }
        public SchedulePatientPayload(Common.Error error) : base(new[] { error }) 
        { }
    }
}