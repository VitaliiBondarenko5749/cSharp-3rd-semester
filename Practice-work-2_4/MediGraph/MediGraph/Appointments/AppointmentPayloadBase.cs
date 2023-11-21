using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Appointments
{
    public class AppointmentPayloadBase : Payload
    {
        protected AppointmentPayloadBase(Appointment? appointment)
        {
            Appointment = appointment;
        }

        protected AppointmentPayloadBase(IReadOnlyList<Common.Error> errors) : base(errors) { } 

        public Appointment? Appointment { get; }
    }
}