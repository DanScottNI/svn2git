using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Medallion.Shell;

namespace Svn2Git
{
    class Svn2Git
    {
        private Options opts;
        private string gitConfigCommand;

        public Svn2Git(Options opts)
        {
            this.opts = opts;
        }

        public string checkout_svn_branch(string branch)
        {
            return $"git checkout -b \"{branch}\" \"remotes/svn/{branch}\"";
        }

        public void run()
        {
            if (this.opts.Rebase)
            {
                get_branches();
            }
            else if (!string.IsNullOrWhiteSpace(this.opts.RebaseBranch))
            {
                get_rebasebranch();
            }
            else
            {
                clone();
            }
            fix_branches();
            fix_tags();
            fix_trunk();
            optimize_repos();
        }

        private void optimize_repos()
        {
            run_command("git", "gc");
        }

        private void fix_tags()
        {
            throw new System.NotImplementedException();
        }

        private void fix_trunk()
        {
            throw new System.NotImplementedException();
        }

        private void fix_branches()
        {
            throw new System.NotImplementedException();
        }

        private void get_rebasebranch()
        {
            throw new System.NotImplementedException();
        }

        private void get_branches()
        {
            throw new System.NotImplementedException();
        }

        public void clone()
        {
            var trunk = this.opts.Trunk;
            var branches = new List<string>() { "branches" };
            var tags = new List<string>() { "tags" };

            if (opts.Tags?.Any() == true)
            {
                tags.AddRange(opts.Tags);
            }

            if (opts.Branches?.Any() == true)
            {
                branches.AddRange(opts.Branches);
            }

            var metadata = this.opts.Metadata;
            var nominimizeurl = this.opts.NoMinimizeUrl;
            var rootistrunk = this.opts.RootIsTrunk;
            var authors = this.opts.Authors;
            var exclude = this.opts.Exclude;
            var revision = this.opts.Revision;
            var username = this.opts.Username;
            var password = this.opts.Password;

            if (rootistrunk)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("svn init --prefix=svn/ ");

                if (!string.IsNullOrWhiteSpace(username))
                {
                    sb.Append("--username='").Append(username).Append("' ");
                }

                if (!string.IsNullOrWhiteSpace(password))
                {
                    sb.Append("--password='").Append(username).Append("' ");
                }

                if (!metadata)
                {
                    sb.Append("--no-metadata ");
                }

                if (nominimizeurl)
                {
                    sb.Append("--no-minimize-url ");
                }

                sb.Append("--trunk='").Append(this.opts.Url).Append('\'');

                run_command("git", sb.ToString(), true, true);
            }
            else
            {
                /*
                 
                 cmd = "git svn init --prefix=svn/ "

        # Add each component to the command that was passed as an argument.
        cmd += "--username='#{username}' " unless username.nil?
        cmd += "--password='#{password}' " unless password.nil?
        cmd += "--no-metadata " unless metadata
        if nominimizeurl
          cmd += "--no-minimize-url "
        end
        cmd += "--trunk='#{trunk}' " unless trunk.nil?
        unless tags.nil?
          # Fill default tags here so that they can be filtered later
          tags = ['tags'] if tags.empty?
          # Process default or user-supplied tags
          tags.each do |tag|
            cmd += "--tags='#{tag}' "
          end
        end
        unless branches.nil?
          # Fill default branches here so that they can be filtered later
          branches = ['branches'] if branches.empty?
          # Process default or user-supplied branches
          branches.each do |branch|
            cmd += "--branches='#{branch}' "
          end
        end

        cmd += @url

        run_command(cmd, true, true)
                 */

                StringBuilder sb = new StringBuilder();
                sb.Append("svn init --prefix=svn/ ");

                if (!string.IsNullOrWhiteSpace(username))
                {
                    sb.Append("--username='").Append(username).Append("' ");
                }

                if (!string.IsNullOrWhiteSpace(password))
                {
                    sb.Append("--password='").Append(username).Append("' ");
                }

                if (!metadata)
                {
                    sb.Append("--no-metadata ");
                }

                if (nominimizeurl)
                {
                    sb.Append("--no-minimize-url ");
                }

                if (!string.IsNullOrWhiteSpace(trunk))
                {
                    sb.Append("--trunk='").Append(trunk).Append('\'');
                }

                if (tags?.Any() == true)
                {
                    foreach (var t in tags)
                    {
                        sb.Append("--tags='").Append(t).Append("' ");
                    }
                }

                if (branches?.Any() == true)
                {
                    foreach (var b in branches)
                    {
                        sb.Append("--branches='").Append(b).Append("' ");
                    }
                }

                run_command("git", sb.ToString(), true, true);
            }

            if (!string.IsNullOrWhiteSpace(authors) && File.Exists(authors))
            {
                run_command("git", $"{git_config_command()} svn.authorsfile {authors}");
            }

            StringBuilder cmd = new StringBuilder();

            cmd.Append("svn fetch ");
            //      unless revision.nil?
            //        range = revision.split(":")
            //        range[1] = "HEAD" unless range[1]
            //        cmd += "-r #{range[0]}:#{range[1]} "
            //      end
            //      unless exclude.empty?
            //# Add exclude paths to the command line; some versions of git support
            //# this for fetch only, later also for init.
            //        regex = []
            //        unless rootistrunk
            //          regex << "#{trunk}[/]" unless trunk.nil?
            //          tags.each{| tag | regex << "#{tag}[/][^/]+[/]"}
            //            unless tags.nil? or tags.empty?
            //          branches.each{| branch | regex << "#{branch}[/][^/]+[/]"}
            //            unless branches.nil? or branches.empty?
            //end
            //        regex = '^(?:' + regex.join('|') + ')(?:' + exclude.join('|') + ')'
            //        cmd += "--ignore-paths='#{regex}' "
            //      end
            run_command("git", cmd.ToString(), true, true);

            get_branches();
        }

        public void run_command(string cmd, string args, bool exit_on_error = true, bool printout_output = false)
        {
            var command = Command.Run(cmd, args);

            var result = command.Result;

            if (!result.Success && exit_on_error)
            {
                throw new System.Exception("Error");

            }


        }

        public string git_config_command()
        {
            if (string.IsNullOrWhiteSpace(gitConfigCommand))
            {

            }

            return string.Empty;
        }
    }
}
