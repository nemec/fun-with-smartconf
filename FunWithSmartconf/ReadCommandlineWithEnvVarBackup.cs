using System;
using CommandLine;
using SmartConf.Sources;
using SmartConf.Sources.Environment;

namespace FunWithSmartconf
{
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
            Environment.SetEnvironmentVariable("INPUT_FILE", "default.txt");

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
