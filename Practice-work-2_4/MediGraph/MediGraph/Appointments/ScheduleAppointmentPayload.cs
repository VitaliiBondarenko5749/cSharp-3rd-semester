using MediGraph.Data;

namespace MediGraph.Appointments
{
    public class ScheduleAppointmentPayload : AppointmentPayloadBase
    {
        public ScheduleAppointmentPayload(Appointment appointment) : base(appointment) { }
        public ScheduleAppointmentPayload(Common.Error error) : base(new[] { error }) { }
    }
}