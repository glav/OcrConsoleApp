using Glav.CognitiveServices.FluentApi.ComputerVision;
using Glav.CognitiveServices.FluentApi.Core;
using System;
using System.Diagnostics;
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

            Console.WriteLine("Job submitted for precessing...now awaiting result...");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = initialCallResult
                    .WaitForOperationToCompleteAsync()
                    .Result;

            stopwatch.Stop();

            if (result.Any(a => !a.ActionSubmittedSuccessfully))
            {
                Console.WriteLine("One or more items not processed successfully");
                return;
            }
            Console.WriteLine("Job completed precessing - Time taken: [{0}h {1}m {1}s {2}ms]",
                stopwatch.Elapsed.Hours,
                stopwatch.Elapsed.Minutes,
                stopwatch.Elapsed.Seconds,
                stopwatch.Elapsed.Milliseconds);

            var builder = new StringBuilder();
            result.ToList().ForEach(r =>
            {
                r.ResponseData.recognitionResult.lines.ToList().ForEach(l => NoiseFilter.AddToBuffer(l.text,builder));
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
