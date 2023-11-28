using System;

namespace MediGraph.Patients
{
    public record SchedulePatientInput(Guid id, string firstName, string lastName);
}