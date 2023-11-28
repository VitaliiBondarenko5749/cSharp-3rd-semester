using MediGraph.Common;
using MediGraph.Data;

#pragma warning disable

namespace MediGraph.Patients
{
    public class AddPatientPayload : Payload
    {
        public AddPatientPayload(Patient patient) 
        {
            Patient = patient;
        }

        public AddPatientPayload(UserError error) : base(new[] { error }) 
        { }

        public Patient? Patient { get; init; }
    }
}