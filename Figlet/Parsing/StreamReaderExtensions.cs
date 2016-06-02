using System.IO;

namespace Figlet.Parsing
{
    internal static class StreamReaderExtensions
    {
        public static string ReadUntilOrEol(this StringReader sr, char value)
        {
            var buffer = new char[8];
            var bufferIndex = 0;

            while (sr.Peek() != -1)
            {
                var temp = (char)sr.Read();

                if (temp == value) break;
                if (temp == Tokens.NewLine) break;

                buffer[bufferIndex++] = temp;
            }

            return new string(buffer, 0, bufferIndex);
        }

        public static char ReadChar(this StreamReader sr)
        {
            if (!sr.EndOfStream)
            {
                return (char)sr.Read();
            }

            return '\0';
        }
    }
}