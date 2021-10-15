using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComanderGraphQl.GraphQL.Commands
{
    public record AddcommandInput(string HowTo, string CommandLine, int PlatformId);
   
}
