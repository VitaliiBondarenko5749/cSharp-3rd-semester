using System;
using System.Collections.Generic;

#pragma warning disable

namespace MediGraph.Data
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirsName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public ICollection<Appointment>? Appointments { get; set; }
    }
}