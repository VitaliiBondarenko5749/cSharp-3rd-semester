namespace MediGraph.Appointments
{
    public record AddAppointmentInput(Guid patientId, Guid doctorId, DateTime dateTime);
}