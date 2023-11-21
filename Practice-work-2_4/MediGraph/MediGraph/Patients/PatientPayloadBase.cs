using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Patients
{
    public class PatientPayloadBase : Payload
    {
        protected PatientPayloadBase(Patient patient)
        {
            Patient = patient;
        }

        protected PatientPayloadBase(IReadOnlyList<Common.Error> errors) : base(errors) { }

        public Patient? Patient { get; }
    }
}