namespace CommanderGQL.GraphQL;

using CommanderGQL.Models;

public class Subscription
{
    [Subscribe]
    [Topic]
    public Platform OnPlatformAdded([EventMessage] Platform platform)
    {
        return platform;
    }
}
