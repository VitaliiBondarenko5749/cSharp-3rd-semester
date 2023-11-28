using HotChocolate.Types.Descriptors;
using HotChocolate.Types;
using System.Reflection;

namespace MediGraph.Extensions
{
    public class UseUpperCaseAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
            => descriptor.UseUpperCase();
    }
}