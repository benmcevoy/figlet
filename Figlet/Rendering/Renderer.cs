using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Figlet.Rendering
{
    internal class Renderer
    {
        private readonly Font _font;

        public Renderer(Font font)
        {
            _font = font;
        }

        public void Render(TextWriter writer, int columns, string message)
        {
            var chars = new List<Character>(message.Length);

            chars.AddRange(message.Select(character => _font.Characters[character]));

            var length = GetMessageLineLength(chars);
            var index = 0;


            while (index <= chars.Count - 1)
            {
                var page = chars.Skip(index).ToList();
                var lineBreakIndex = GetLineBreakIndex(page, length, columns);
                index += lineBreakIndex;

                // need "paging" here based on lineBreakIndex
                // lineBreakIndex should be on the remaining
                // and you see why kerning and "smushing" make good idea

                var line = 0;

                for (var i = 0; i < _font.FontInfo.Height; i++)
                {
                    foreach (var character in page.Take(lineBreakIndex))
                    {
                        writer.Write(character.Lines[line]);
                    }

                    writer.WriteLine();

                    line++;
                }
            }
        }

        private static int GetLineBreakIndex(IList<Character> chars, int length, int columns)
        {
            var len = 0;
            var i = 0;

            foreach (var character in chars)
            {
                if (character.Ascii == 13) return i + 1;
               // if (character.Ascii == 10) return i + 1;

                len += character.Lines[0].Length;

                if (len >= columns) break;

                i++;
            }

            if (len < columns) return i;

            var x = i;
            // and then go back and find the first space - . , ! ? " ) ( /
            while (i > 0)
            {
                if (len < columns)
                {
                    if (Regex.IsMatch(chars[i].Value.ToString(), @"[ \.\^\*\+\?\(\)\[\{\\\|\-\]\t,!/]", RegexOptions.IgnoreCase))
                    {
                        return i + 1;
                    }
                }

                len -= chars[i].Lines[0].Length;
                i--;
            }

            return x;
        }

        private static int GetMessageLineLength(IEnumerable<Character> characters)
        {
            return characters.Sum(c => c.Lines[0].Length);
        }
    }
}
