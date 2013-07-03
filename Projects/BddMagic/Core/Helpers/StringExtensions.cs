using System.Collections.Generic;
using System.IO;

namespace BddMagic.Core.Helpers
{
    public static class StringExtensions
    {
        public static void WriteLines(this string value, TextWriter textWriter, bool trimLines = false)
        {
            Argument.MustNotBeNull(value, "value");
            Argument.MustNotBeNull(textWriter, "textWriter");

            foreach (var line in value.ToLines())
            {
                if (trimLines)
                {
                    textWriter.WriteLine(line.Trim());
                }
                else
                {
                    textWriter.WriteLine(line);
                }
            }
        }

        public static IEnumerable<string> ToLines(this string value)
        {
            Argument.MustNotBeNull(value, "value");

            using (var reader = new StringReader(value))
            {
                while (true)
                {
                    var line = reader.ReadLine();

                    if (line == null)
                    {
                        yield break;
                    }
                    yield return line;
                }
            }
        }

    }
}
