using MediGraph.Common;
using MediGraph.Data;

namespace MediGraph.Appointments
{
    public class ScheduleAppointmentPayload : AppointmentPayloadBase
    {
        public ScheduleAppointmentPayload(Appointment appointment) : base(appointment) { }
        public ScheduleAppointmentPayload(UserError error) : base(new[] { error }) { }
    }
}