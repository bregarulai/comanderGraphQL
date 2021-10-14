using ComanderGraphQl.Data;
using ComanderGraphQl.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComanderGraphQl.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Description("Represents any software or service that has a command line interface");
            descriptor
                .Field(f => f.LicenseKey)
                .Ignore();
            descriptor
                .Field(f => f.Commands)
                .ResolveWith<Resolvers>(f => f.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list for available commands for this platform.");
        }
        
        private class Resolvers
        {
            [UseProjection]
            public IQueryable<Command> GetCommands([Parent]Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands
                    .Where(c => c.PlatformId == platform.Id);
            }
        }
    }
}
