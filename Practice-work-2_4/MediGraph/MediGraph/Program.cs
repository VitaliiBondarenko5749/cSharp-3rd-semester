using MediGraph.Appointments;
using MediGraph.Data;
using MediGraph.DataLoader;
using MediGraph.Doctors;
using MediGraph.Patients;
using MediGraph.Types;
using Microsoft.EntityFrameworkCore;

#pragma warning disable

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=mediGraph.db");
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<DoctorQueries>()
    .AddTypeExtension<PatientQueries>()
    .AddTypeExtension<AppointmentQueries>()
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<AppointmentMutations>()
    .AddTypeExtension<DoctorMutations>()
    .AddTypeExtension<PatientMutations>()
    .AddSubscriptionType(d => d.Name("Subscription"))
    .AddTypeExtension<AppointmentSubscriptions>()
    .AddTypeExtension<DoctorSubscriptions>()
    .AddTypeExtension<PatientSubscriptions>()
    .AddType<DoctorType>()
    .AddType<PatientType>()
    .AddType<AppointmentType>()
    .EnableRelaySupport()
    .AddFiltering()
    .AddDataLoader<DoctorByIdDataLoader>()
    .AddDataLoader<PatientByIdDataLoader>()
    .AddDataLoader<AppointmentByIdDataLoader>()
    .RegisterService<ApplicationDbContext>(ServiceKind.Pooled)
    .AddInMemorySubscriptions();

WebApplication app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();