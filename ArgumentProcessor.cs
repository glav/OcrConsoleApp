using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OcrConsoleApp
{
    class ArgumentProcessor
    {
        public CommandArgs ProcessArguments(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Error!: Please supply a filename");
                ShowArgumentFormat();
                return null;
            }
            var inputFilename = args[0];
            if (!File.Exists(inputFilename))
            {
                Console.WriteLine("Error!: No such file exists: [{0}]", inputFilename);
                return null;
            }

            var outFile = string.Empty;
            if (args.Length >= 2)
            {
                outFile = args[1];
            }

            if (AppConfig.Configuration == null)
            {
                Console.WriteLine("Error!: Missing or invalid appsettings.json...exiting");
                return null;
            }

            var apiKey = AppConfig.Configuration["apiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("Error!: Missing or invalid ApiKey...exiting");
                return null;
            }

            Console.WriteLine("Processing file: [{0}]", inputFilename);
            Console.WriteLine("Output results to: [{0}]", string.IsNullOrEmpty(outFile) ? "Console" : outFile);

            return new CommandArgs(apiKey, inputFilename, outFile);
        }

        static void ShowArgumentFormat()
        {
            Console.WriteLine("\nFormat: OcrConsoleApp {InputFilename} [OutputFile]");
            Console.WriteLine("\twhere {InputFilename} is mandatory and the image to use as input.");
            Console.WriteLine("\twhere [OutputFile] is optional and the file to write the results to. If none is specified, results are output to console");

        }
    }
}
