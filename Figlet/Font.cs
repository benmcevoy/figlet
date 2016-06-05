using System.Collections.Generic;

namespace Figlet
{
    internal class Font
    {
        public Font(FontInfo fontInfo, Dictionary<char, Character> characters)
        {
            FontInfo = fontInfo;
            Characters = characters;

            var empty = Empty(fontInfo.Height);

            Characters.Add('\r', new Character(13, empty));
            Characters.Add('\n', new Character(10, empty));
            Characters.Add('\t', new Character(9, empty));
        }

        private static string[] _empty;
        private static string[] Empty(int length)
        {
            if (_empty != null) return _empty;

            _empty = new string[length];

            for (var i = 0; i < length; i++)
            {
                _empty[i] = string.Empty;
            }

            return _empty;
        }

        public FontInfo FontInfo { get; }

        public Dictionary<char, Character> Characters { get; }
    }
}