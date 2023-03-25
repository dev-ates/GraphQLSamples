using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("CommandConStr")))
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddSubscriptionType<Subscription>()
            .AddType<PlatformType>()
            .AddType<CommandType>()
            .AddFiltering()
            .AddSorting()
            .AddInMemorySubscriptions();

        var app = builder.Build();

        app.UseWebSockets();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });

        app.UseGraphQLVoyager("/graphql-voyager");

        app.Run();
    }
}