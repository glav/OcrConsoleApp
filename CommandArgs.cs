using System;
using System.Collections.Generic;
using System.Text;

namespace OcrConsoleApp
{
    class CommandArgs
    {
        public CommandArgs(string apiKey, string inputFilename, string outputFilename)
        {
            ApiKey = apiKey;
            InputFilename = inputFilename;
            OutputFilename = outputFilename;
        }
        public string InputFilename { get; }
        public string OutputFilename { get; }
        public string ApiKey { get; }

        public bool IsApiKeyValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ApiKey);
            }
        }

        public bool OutputToFilename
        { 
            get
            {
                return !string.IsNullOrWhiteSpace(OutputFilename);
            }
        }

    }
}
