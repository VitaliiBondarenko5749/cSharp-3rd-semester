using MediGraph.Common;
using MediGraph.Data;
using System.Collections.Generic;

#pragma warning disable

namespace MediGraph.Doctors
{
    public class DoctorPayloadBase : Payload
    {
        protected DoctorPayloadBase(Doctor doctor)
        {
            Doctor = doctor;
        }

        protected DoctorPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }

        public Doctor? Doctor { get; }
    }
}