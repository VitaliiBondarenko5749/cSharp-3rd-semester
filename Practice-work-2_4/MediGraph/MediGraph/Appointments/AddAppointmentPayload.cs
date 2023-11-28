using MediGraph.Common;
using MediGraph.Data;

#pragma warning disable

namespace MediGraph.Appointments
{
    public class AddAppointmentPayload : Payload
    {
        public AddAppointmentPayload(Appointment appointment)
        {
            Appointment = appointment;
        }

        public AddAppointmentPayload(UserError error) : base(new[] { error }) 
        { }

        public Appointment? Appointment { get; init; }
    }
}