using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Appointments
{
    public class AddAppointmentPayload : Payload
    {
        public AddAppointmentPayload(Appointment appointment)
        {
            Appointment = appointment;
        }

        public AddAppointmentPayload(Common.Error error) : base(new[] { error }) 
        { }

        public Appointment? Appointment { get; init; }
    }
}