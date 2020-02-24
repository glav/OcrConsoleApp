using System;

namespace OcrConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Text Recognition Console Utility.\n");
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
