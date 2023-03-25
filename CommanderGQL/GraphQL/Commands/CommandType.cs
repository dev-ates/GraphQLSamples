namespace CommanderGQL.GraphQL.Commands;

using CommanderGQL.Data;
using CommanderGQL.Models;

public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command.");

        descriptor
            .Field(c => c.Platform)
            .ResolveWith<Resolver>(c => c.GetPlatform(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is platform to which the command belongs.");
    }

    private class Resolver
    {
        public Platform? GetPlatform([Parent]Command command, [ScopedService] AppDbContext context)
        {
            return context.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
        }
    }
}
