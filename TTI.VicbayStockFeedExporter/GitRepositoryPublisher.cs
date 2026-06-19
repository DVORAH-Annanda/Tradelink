using System;
using System.Diagnostics;
using System.IO;

namespace TTI.VicbayStockFeedExporter
{
    internal sealed class GitRepositoryPublisher
    {
        private readonly string _gitExecutablePath;
        private readonly string _repositoryPath;
        private readonly string _branch;

        public GitRepositoryPublisher(
            string gitExecutablePath,
            string repositoryPath,
            string branch)
        {
            _gitExecutablePath = gitExecutablePath;
            _repositoryPath = repositoryPath;
            _branch = branch;
        }

        public void PrepareForExport()
        {
            EnsureRepositoryIsClean();

            RunGitOrThrow(
                "pull --ff-only origin " + Quote(_branch),
                "Unable to pull the latest version of the Vicbay stock repository.");
        }

        public void CommitAndPush(string outputFilePath, DateTimeOffset generatedAt)
        {
            string relativeFilePath = GetRepositoryRelativePath(outputFilePath);

            RunGitOrThrow(
                "add -- " + Quote(relativeFilePath),
                "Unable to stage the Vicbay stock JSON file.");

            GitCommandResult diffResult = RunGit(
                "diff --cached --quiet -- " + Quote(relativeFilePath));

            // Exit code 0 = no changes.
            if (diffResult.ExitCode == 0)
            {
                Console.WriteLine("GitHub publish skipped: stock file has not changed.");
                return;
            }

            // Exit code 1 = changes are staged, which is expected.
            if (diffResult.ExitCode != 1)
            {
                throw new InvalidOperationException(
                    "Unable to determine whether the stock file changed."
                    + Environment.NewLine
                    + diffResult.Output);
            }

            string commitMessage =
                "Update Vicbay stock feed "
                + generatedAt.ToString("yyyy-MM-dd HH:mm:ss zzz");

            RunGitOrThrow(
                "commit -m " + Quote(commitMessage),
                "Unable to commit the Vicbay stock JSON file.");

            RunGitOrThrow(
                "push origin " + Quote(_branch),
                "Unable to push the Vicbay stock JSON file to GitHub.");

            Console.WriteLine("GitHub publish completed successfully.");
        }

        private void EnsureRepositoryIsClean()
        {
            GitCommandResult statusResult = RunGit("status --porcelain");

            if (!string.IsNullOrWhiteSpace(statusResult.Output))
            {
                throw new InvalidOperationException(
                    "The local Vicbay stock repository has uncommitted changes."
                    + Environment.NewLine
                    + "Please resolve them before running the exporter."
                    + Environment.NewLine
                    + statusResult.Output);
            }
        }

        private string GetRepositoryRelativePath(string outputFilePath)
        {
            string repositoryFullPath = Path.GetFullPath(_repositoryPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            string outputFullPath = Path.GetFullPath(outputFilePath);

            string repositoryPrefix =
                repositoryFullPath + Path.DirectorySeparatorChar;

            if (!outputFullPath.StartsWith(
                    repositoryPrefix,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "The JSON output file must be inside the local Git repository."
                    + Environment.NewLine
                    + "Repository: " + repositoryFullPath
                    + Environment.NewLine
                    + "Output file: " + outputFullPath);
            }

            return outputFullPath
                .Substring(repositoryPrefix.Length)
                .Replace(Path.DirectorySeparatorChar, '/');
        }

        private void RunGitOrThrow(string arguments, string failureMessage)
        {
            GitCommandResult result = RunGit(arguments);

            if (result.ExitCode != 0)
            {
                throw new InvalidOperationException(
                    failureMessage
                    + Environment.NewLine
                    + "Git command: git " + arguments
                    + Environment.NewLine
                    + result.Output);
            }
        }

        private GitCommandResult RunGit(string arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _gitExecutablePath,
                Arguments = arguments,
                WorkingDirectory = _repositoryPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                string standardOutput = process.StandardOutput.ReadToEnd();
                string standardError = process.StandardError.ReadToEnd();

                process.WaitForExit();

                return new GitCommandResult
                {
                    ExitCode = process.ExitCode,
                    Output = (standardOutput + Environment.NewLine + standardError).Trim()
                };
            }
        }

        private static string Quote(string value)
        {
            return "\"" + value.Replace("\"", "\\\"") + "\"";
        }

        private sealed class GitCommandResult
        {
            public int ExitCode { get; set; }
            public string Output { get; set; }
        }
    }
}
