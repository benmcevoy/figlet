using System.Collections.Generic;

namespace Figlet
{
    internal class Font
    {
        public Font(FontInfo fontInfo, Dictionary<char, Character> characters)
        {
            FontInfo = fontInfo;
            Characters = characters;
        }

        public FontInfo FontInfo { get; }

        public Dictionary<char, Character> Characters { get; }
    }
}