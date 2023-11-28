using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediGraph.Data;
using MediGraph.DataLoader;
using MediGraph.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediGraph.Doctors
{
#pragma warning disable
    [ExtendObjectType(Name = "Query")]
#pragma warning enable
    public class DoctorQueries
    {
        [UseApplicationDbContext]
        [UseFiltering]
        public IQueryable<Doctor> GetDoctors([ScopedService] ApplicationDbContext context)
            => context.Doctors;

        public Task<Doctor> GetDoctorByIdAsync([ID(nameof(Doctor))] Guid id, DoctorByIdDataLoader doctorById,
            CancellationToken cancellationToken) 
            => doctorById.LoadAsync(id, cancellationToken);

        [UseApplicationDbContext]
        public IQueryable<Doctor> GetDoctorsByFullName(string fullName, 
            [ScopedService] ApplicationDbContext context, CancellationToken cancellationToken)
        {
            string[] splitedFullName = fullName.Split(' ');
       
            if (!splitedFullName.Length.Equals(2))
            {
                throw new ArgumentException("Wrong entered full name!");
            }

            string firstName = splitedFullName[0], lastName = splitedFullName[1];

            return context.Doctors
                .Where(d => d.FirsName.Equals(firstName) && d.LastName.Equals(lastName));
        }
    }
}