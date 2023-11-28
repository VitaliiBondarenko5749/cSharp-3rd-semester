using HotChocolate.Types;
using MediGraph.Data;
using MediGraph.DataLoader;

namespace MediGraph.Types
{
    public class DoctorType : ObjectType<Doctor>
    {
        protected override void Configure(IObjectTypeDescriptor<Doctor> descriptor)
        {
#pragma warning disable
            descriptor
                .ImplementsNode()
                .IdField(d => d.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<DoctorByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
#pragma warning enable

            descriptor.Field(d => d.Appointments)
                .Ignore();
        }
    }
}