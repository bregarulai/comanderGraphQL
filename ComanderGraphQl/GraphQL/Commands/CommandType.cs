using ComanderGraphQl.Data;
using ComanderGraphQl.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComanderGraphQl.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Description("Represents any executable command");
            descriptor
                .Field(p => p.Platform)
                .ResolveWith<Resolvers>(p => p.GetPlatforms(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the platform in which this command can be executed");
        }

        private class Resolvers
        {
            public IQueryable<Platform> GetPlatforms(Command command, [ScopedService] AppDbContext context)
            {
                return context.Platforms
                    .Where(p => p.Id == command.PlatformId);
            }
        }

    }
}
