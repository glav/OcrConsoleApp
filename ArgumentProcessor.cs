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
                Console.WriteLine("Please supply a filename");
                return null;
            }
            var inputFilename = args[0];
            if (!File.Exists(inputFilename))
            {
                Console.WriteLine("No such file exists: [{0}]", inputFilename);
                return null;
            }

            var outFile = string.Empty;
            if (args.Length >= 2)
            {
                outFile = args[1];
            }

            if (AppConfig.Configuration == null)
            {
                Console.WriteLine("Missing or invalid appsettings.json...exiting");
                return null;
            }

            var apiKey = AppConfig.Configuration["apiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("Missing or invalid ApiKey...exiting");
                return null;
            }


            //var filename = "c:\\temp\\equipment-list.jpg";
            Console.WriteLine("Processing file: [{0}]", inputFilename);
            Console.WriteLine("Output results to: [{0}]", string.IsNullOrEmpty(outFile) ? "Console" : outFile);

            return new CommandArgs(apiKey, inputFilename, outFile);
        }
    }
}
