using System;
using Figlet.Parsing;
using Figlet.Rendering;

namespace Figlet
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) return;
            if (string.IsNullOrWhiteSpace(args[0])) return;

            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                new Renderer(new Lexer().Lex(args[1])).Render(Console.Out, Console.BufferWidth, args[0]);
                return;
            }

            new Renderer(new Lexer().Lex()).Render(Console.Out, Console.BufferWidth, args[0]);
        }
    }
}
