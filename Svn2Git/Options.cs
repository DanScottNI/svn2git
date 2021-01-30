using System.Collections.Generic;
using CommandLine;

namespace Svn2Git
{
    class Options
    {
        [Option("url", Required = true, HelpText = "Specify the URL of the SVN repository")]
        public string Url { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Be verbose in logging -- useful for debugging issues")]
        public bool Verbose { get; set; }

        [Option("rebase", Required = false, HelpText = "Instead of cloning a new project, rebase an existing one against SVN")]
        public bool Rebase { get; set; }

        [Option("username", Default = null, Required = false, HelpText = "Username for transports that needs it (http(s), svn)")]
        public string Username { get; set; }


        [Option("password", Default = null, Required = false, HelpText = "Password for transports that need it (http(s), svn)")]
        public string Password { get; set; }


        [Option("trunk", Required = false, HelpText = "Subpath to trunk from repository URL (default: trunk)")]
        public string Trunk { get; set; }

        [Option("branches", Required = false, HelpText = "Subpath to branches from repository URL (default: branches); can be used multiple times")]
        public IEnumerable<string> Branches { get; set; }

        [Option("tags", Required = false, HelpText = "Subpath to tags from repository URL (default: tags); can be used multiple times")]
        public IEnumerable<string> Tags { get; set; }

        [Option("rootistrunk", Required = false, HelpText = "Use this if the root level of the repo is equivalent to the trunk and there are no tags or branches")]
        public bool RootIsTrunk { get; set; }

        [Option("notrunk", Required = false, HelpText = "Do not import anything from trunk")]
        public bool NoTrunk { get; set; }

        [Option("nobranches", Required = false, HelpText = "Do not try to import any branches")]
        public bool NoBranches { get; set; }

        [Option("notags", Required = false, HelpText = "Do not try to import any tags")]
        public bool NoTags { get; set; }

        [Option("no-minimize-url", Required = false, HelpText = "Accept URLs as-is without attempting to connect to a higher level directory")]
        public bool NoMinimizeUrl { get; set; }

        [Option("revision", Required = false, HelpText = "Start importing from SVN revision START_REV; optionally end at END_REV")]
        public string Revision { get; set; }

        [Option('m', "metadata", Required = false, HelpText = "Include metadata in git logs (git-svn-id)")]
        public bool Metadata { get; set; }

        [Option("authors", Required = false, HelpText = "Path to file containing svn-to-git authors mapping")]
        public string Authors { get; set; }

        [Option("exclude", Required = false, HelpText = "Specify a regular expression to filter paths when fetching; can be used multiple times")]
        public string Exclude { get; set; }

        [Option("rebasebranch", Required = false, HelpText = "Rebase specified branch")]
        public string RebaseBranch { get; set; }


    }
}
