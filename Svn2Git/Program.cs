using System.Collections.Generic;
using CommandLine;

namespace Svn2Git
{

    class Program
    {

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed(RunOptions)
              .WithNotParsed(HandleParseError);
        }

        static void RunOptions(Options opts)
        {
            var svn = new Svn2Git(opts);
            svn.run();
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
    }
}
