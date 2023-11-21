namespace MediGraph.Data
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public Patient Patient { get; set; } = default!;
        public Guid PatientId { get; set; }

        public Doctor Doctor { get; set; } = default!;
        public Guid DoctorId { get; set; }

        public DateTime DateTime { get; set; }
    }
}