using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Patients
{
    public class AddPatientPayload : Payload
    {
        public AddPatientPayload(Patient patient) 
        {
            Patient = patient;
        }

        public AddPatientPayload(Common.Error error) : base(new[] { error }) 
        { }

        public Patient? Patient { get; init; }
    }
}