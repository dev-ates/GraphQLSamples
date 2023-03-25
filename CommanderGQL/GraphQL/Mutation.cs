namespace CommanderGQL.GraphQL;

using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate.Subscriptions;

public class Mutation
{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
    {
        var platform = new Platform
        {
            Name = input.Name,
            LicenseKey = new Random().Next(0, 999999).ToString()
        };

        context.Platforms.Add(platform);
        await context.SaveChangesAsync(cancellationToken);

        await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

        return new AddPlatformPayload(platform);
    }

    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
    {
        var command = new Command
        {
            CommandLine = input.CommandLine,
            HowTo = input.HowTo,
            PlatformId = input.PlatformId
        };

        context.Commands.Add(entity: command);
        await context.SaveChangesAsync();

        return new AddCommandPayload(command);
    }
}
