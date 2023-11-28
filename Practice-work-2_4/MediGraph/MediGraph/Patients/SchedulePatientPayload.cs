using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Patients
{
    public class SchedulePatientPayload : PatientPayloadBase
    {
        public SchedulePatientPayload(Patient patient) : base(patient) { }
        public SchedulePatientPayload(UserError error) : base(new[] { error }) 
        { }
    }
}