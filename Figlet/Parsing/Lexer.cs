using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Figlet.Parsing
{
    internal class Lexer
    {
        public Font Lex(string fontPath)
        {
            using (var fs = new FileStream(fontPath, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                return Lex(sr);
            }
        }

        public Font Lex(StreamReader sr)
        {
            if (sr.EndOfStream) throw new InvalidOperationException("file is empty?");

            var fontInfo = GetFontInfo(sr.ReadLine());
            var chars = new Dictionary<char, Character>(102);
            var i = 0;

            for (var j = 0; j < fontInfo.CommentLines; j++)
            {
                sr.ReadLine();
            }

            while (!sr.EndOfStream && i < 102)
            {
                var c = GetChar(sr, fontInfo, i);
                chars[c.Value] = c;
                i++;
            }

            return new Font(fontInfo, chars);
        }

        private static Character GetChar(StreamReader sr, FontInfo fontInfo, int index)
        {
            var lines = new string[fontInfo.Height];
            var lastIndex = fontInfo.Height - 1;

            for (var i = 0; i < fontInfo.Height; i++)
            {
                var line = sr.ReadLine();
                // you need the HardBlank for kerning/"smushing" rules
                line = line.Replace(fontInfo.HardBlank, Tokens.Space);

                line = (i == lastIndex)
                    ? line.Remove(line.Length - 2)
                    : line.Remove(line.Length - 1);

                lines[i] = line;
            }

            return new Character(32 + index, lines);
        }

        private static FontInfo GetFontInfo(string line)
        {
            var fi = new FontInfo();
            var buffer = new char[5];

            using (var sr = new StringReader(line))
            {
                sr.Read(buffer, 0, 5);

                if (new string(buffer) != Tokens.Signature) throw new InvalidOperationException("unrecognised font file");

                fi.HardBlank = (char)sr.Read();

                sr.Read();

                fi.Height = Convert.ToInt32(sr.ReadUntilOrEol(Tokens.Space));
                fi.BaseLine = Convert.ToInt32(sr.ReadUntilOrEol(Tokens.Space));
                fi.MaxLength = Convert.ToInt32(sr.ReadUntilOrEol(Tokens.Space));
                fi.OldLayout = Convert.ToInt32(sr.ReadUntilOrEol(Tokens.Space));
                fi.CommentLines = Convert.ToInt32(sr.ReadUntilOrEol(Tokens.Space));
                //fi.PrintDirection = Convert.ToInt32(sr.ReadUntilOrEol(Tokens.Space));
                //fi.FullLayout = Convert.ToInt32(sr.ReadUntilOrEol(Tokens.Space));
                //fi.CodeTagCount = Convert.ToInt32(sr.ReadUntilOrEol(Tokens.Space));

            }

            return fi;
        }
    }
}


