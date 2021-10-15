using ComanderGraphQl.Data;
using ComanderGraphQl.GraphQL.Commands;
using ComanderGraphQl.GraphQL.Platforms;
using ComanderGraphQl.Models;
using HotChocolate;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComanderGraphQl.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync (AddplatformInput input,
            [ScopedService] AppDbContext context)
        {
            var platform = new Platform
            {
                Name = input.Name
            };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync (AddcommandInput input,
            [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };
            context.Commands.Add(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}
