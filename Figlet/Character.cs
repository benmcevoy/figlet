namespace Figlet
{
    internal class Character
    {
        public Character(int ascii, string[] lines)
        {
            Ascii = ascii;
            Lines = lines;
            Value = (char)ascii;
        }

        public int Ascii { get; }

        public char Value { get; }

        public string[] Lines { get; }
    }
}