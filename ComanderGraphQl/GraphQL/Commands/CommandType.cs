using ComanderGraphQl.Data;
using ComanderGraphQl.Models;
using HotChocolate;
using HotChocolate.Data;
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
                .ResolveWith<Resolvers>(p => p.GetPlatform(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the platform to which this platform can belong.");
        }

        private class Resolvers
        {
         
            [UseProjection]
            public Platform GetPlatform([Parent]Command command, [ScopedService] AppDbContext context)
            {
                return context.Platforms
                    .FirstOrDefault(p => p.Id == command.PlatformId);
                    
            }
        }

    }
}
