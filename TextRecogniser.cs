using Glav.CognitiveServices.FluentApi.ComputerVision;
using Glav.CognitiveServices.FluentApi.Core;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace OcrConsoleApp
{
    class TextRecogniser
    {
        public void ProcessImage(CommandArgs config)
        {
            var initialCallResult = ComputerVisionConfigurationSettings.CreateUsingConfigurationKeys(config.ApiKey, LocationKeyIdentifier.SouthEastAsia)
                    .AddConsoleAndTraceLogging()
                    .UsingHttpCommunication()
                    .WithComputerVisionAnalysisActions()
                    .AddFileForRecognizeTextAnalysis(config.InputFilename, Glav.CognitiveServices.FluentApi.ComputerVision.Domain.RecognizeTextMode.Printed)
                    .AnalyseAllAsync()
                    .Result;
            if (!initialCallResult.RecognizeTextAnalysis.AnalysisResult.ActionSubmittedSuccessfully)
            {
                Console.WriteLine("Problem submitting the request for text recognition - [{0}]", initialCallResult.RecognizeTextAnalysis.AnalysisResult.ApiCallResult.ErrorMessage);
                return;
            }

            var result = initialCallResult
                    .WaitForOperationToCompleteAsync()
                    .Result;

            if (result.Any(a => !a.ActionSubmittedSuccessfully))
            {
                Console.WriteLine("One or more items not processed successfully");
                return;
            }

            var builder = new StringBuilder();
            result.ToList().ForEach(r =>
            {
                r.ResponseData.recognitionResult.lines.ToList().ForEach(l => builder.AppendLine(l.text));
            });

            if (!config.OutputToFilename)
            {
                Console.WriteLine("Results:\n");
                Console.WriteLine(builder.ToString());
                return;
            }

            File.WriteAllText(config.OutputFilename, builder.ToString());
            Console.WriteLine("Done.");

        }
    }
}
