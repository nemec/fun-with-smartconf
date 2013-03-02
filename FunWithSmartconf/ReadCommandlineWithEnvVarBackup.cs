using System;
using CommandLine;
using SmartConf.Sources;
using SmartConf.Sources.Environment;

namespace FunWithSmartconf
{
    /// <summary>
    /// This example explores combining the CommandLineParser source
    /// with the Environment variable source so that any command line
    /// option that _isn't_ provided is pulled in from the process'
    /// environment variables.
    /// </summary>
    class ReadCommandlineWithEnvVarBackup
    {
        public class Options
        {
            [EnvironmentVariable("INPUT_FILE")]
            [Option('r', "read", HelpText = "Input file to be processed.")]
            public string InputFile { get; set; }
        }

        public static void RunExample()
        {
            // Make sure the env var is set.
            Environment.SetEnvironmentVariable("INPUT_FILE", "default.txt");

            // Create a new configuration manager where the input file
            // is specified on the command line.
            var withInput = new SmartConf.ConfigurationManager<Options>(
                new EnvironmentConfigurationSource<Options>
                    {
                        GlobalTarget = EnvironmentVariableTarget.Process,
                        PrimarySource = true
                    },
                new CommandLineConfigurationSource<Options>(new []{"-r", "input.txt"})
                );

            Console.Write("Should print the value specified in command line arguments: ");
            Console.WriteLine(withInput.Out.InputFile);

            Console.WriteLine();

            // Create a new configuration manager where the input file
            // is not specified, so it defaults to the environment variable.
            var withEnv = new SmartConf.ConfigurationManager<Options>(
                new EnvironmentConfigurationSource<Options>
                {
                    GlobalTarget = EnvironmentVariableTarget.Process,
                    PrimarySource = true
                },
                new CommandLineConfigurationSource<Options>(new string[0])
                );

            Console.Write("Should print the value specified in environment variable: ");
            Console.WriteLine(withEnv.Out.InputFile);
        }
    }
}
