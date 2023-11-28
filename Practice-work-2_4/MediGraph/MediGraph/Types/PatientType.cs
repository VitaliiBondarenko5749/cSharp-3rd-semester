using HotChocolate.Types;
using MediGraph.Data;
using MediGraph.DataLoader;

namespace MediGraph.Types
{
    public class PatientType : ObjectType<Patient>
    {
        protected override void Configure(IObjectTypeDescriptor<Patient> descriptor)
        {
#pragma warning disable
            descriptor
                .ImplementsNode()
                .IdField(p => p.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<PatientByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
#pragma warning enable

            descriptor.Field(p => p.Appointments)
                .Ignore();
        }
    }
}