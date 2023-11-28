using HotChocolate.Data;
using MediGraph.Data;

namespace MediGraph.Extensions
{
    public class UseApplicationDbContextAttribute : UseDbContextAttribute
    {
        public UseApplicationDbContextAttribute() : base(typeof(ApplicationDbContext))
        {
        }
    }
}