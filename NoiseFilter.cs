using System;
using System.Collections.Generic;
using System.Text;

namespace OcrConsoleApp
{
    static class NoiseFilter
    {
        public static void AddToBuffer(string input, StringBuilder buffer)
        {
            // This part helps to  cleanup portions of text when people use bullet points. These are often converted to 
            // periods on an empty line and are thus useless.
            var normalised = input.ToLowerInvariant().Trim().Replace(".", string.Empty);
            if (!string.IsNullOrWhiteSpace(normalised))
            {
                buffer.AppendLine(input);
            }
        }
    }
}
