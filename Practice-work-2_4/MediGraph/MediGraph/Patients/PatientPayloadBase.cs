using MediGraph.Common;
using MediGraph.Data;
using System.Collections.Generic;

#pragma warning disable

namespace MediGraph.Patients
{
    public class PatientPayloadBase : Payload
    {
        protected PatientPayloadBase(Patient patient)
        {
            Patient = patient;
        }

        protected PatientPayloadBase(IReadOnlyList<UserError> errors) : base(errors) { }

        public Patient? Patient { get; }
    }
}