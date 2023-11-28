using HotChocolate.AspNetCore;
using HotChocolate.Types;
using MediGraph.Appointments;
using MediGraph.Data;
using MediGraph.DataLoader;
using MediGraph.Doctors;
using MediGraph.Patients;
using MediGraph.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

#pragma warning disable

namespace MediGraph
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            })
                // Add the DbContext
                .AddDbContextFactory<ApplicationDbContext>((serviceProvider, options) =>
                {
                    options.UseSqlite("Data Source=mediGraph.db")
                    .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
                })
                // Add the GraphQL server core
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

                .AddTypeExtension<DoctorByIdDataLoader>()
                .AddTypeExtension<PatientByIdDataLoader>()
                .AddTypeExtension<AppointmentByIdDataLoader>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
              
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/graphql", true);
                    return Task.CompletedTask;
                });
            });
        }
    }
}