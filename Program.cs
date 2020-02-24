using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Glav.CognitiveServices.FluentApi.ComputerVision;
using Glav.CognitiveServices.FluentApi.Core;
using System.Linq;
using System.Text;

namespace OcrConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Text Recognition Console Utility.");
            AppConfig.LoadAppSettings();
            var config = new ArgumentProcessor().ProcessArguments(args);

            if (config == null)
            {
                return;
            }

            new TextRecogniser().ProcessImage(config);
        }
    }

}
