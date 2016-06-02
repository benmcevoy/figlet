using System;
using Figlet.Parsing;
using Figlet.Rendering;

namespace Figlet
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Lexer();
            var fi = t.Lex(@"fonts\doom.flf");
            var r = new Renderer(fi);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            r.Render(Console.Out, Console.BufferWidth, "The quick brown fox jumped over the slow lazy dog");
            
            Console.ReadKey();
        }
    }
}
