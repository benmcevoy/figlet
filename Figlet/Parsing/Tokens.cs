namespace Figlet.Parsing
{
    internal class Tokens
    {
        public static string Signature = "flf2a";
        public static char Space = ' ';
        public static int NewLine = 10;
        public static char[] EndOfLine = { '@' };
        public static char[] EndOfLineAlt = { '#' };
        public static char[] EndOfCharacter = { '@', '@' };
        public static char[] EndOfCharacterAlt = { '#', '#' };
    }
}