using MediGraph.Common;
using MediGraph.Data;
using System.Collections.Generic;

#pragma warning disable

namespace MediGraph.Appointments
{
    public class AppointmentPayloadBase : Payload
    {
        protected AppointmentPayloadBase(Appointment? appointment)
        {
            Appointment = appointment;
        }

        protected AppointmentPayloadBase(IReadOnlyList<UserError> errors) : base(errors) { } 

        public Appointment? Appointment { get; }
    }
}