using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using ConferencePlanner.GraphQL.Sessions;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Tracks;
using ConferencePlanner.GraphQL.Types;
using Microsoft.EntityFrameworkCore;

#pragma warning disable

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

builder.Services
     .AddGraphQLServer()
   .AddQueryType(d => d.Name("Query"))
      .AddTypeExtension<SpeakerQueries>()
      .AddTypeExtension<SessionQueries>()
      .AddTypeExtension<TrackQueries>()
   .AddMutationType(d => d.Name("Mutation"))
      .AddTypeExtension<SessionMutations>()
      .AddTypeExtension<SpeakerMutations>()
      .AddTypeExtension<TrackMutations>()
   .AddType<AttendeeType>()
   .AddType<SessionType>()
   .AddType<SpeakerType>()
   .AddType<TrackType>()
   .EnableRelaySupport()
   .AddDataLoader<SpeakerByIdDataLoader>()
   .AddDataLoader<SessionByIdDataLoader>();

WebApplication app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.MapGet("/", () => "Hello World!");

app.Run();