using System;
using System.IO;
using System.Reflection;
using Figlet.Parsing;
using Figlet.Rendering;

namespace Figlet
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args.Length == 0) args = new [] { Console.ReadLine() };

            //if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            //{
            //    new Renderer(new Lexer().Lex(args[1])).Render(Console.Out, Console.BufferWidth, args[0]);
            //    return;
            //}

            var sr = Assembly.GetExecutingAssembly().GetManifestResourceStream("Figlet.Fonts.standard.flf");

            new Renderer(new Lexer().Lex(new StreamReader(sr))).Render(Console.Out, Console.BufferWidth, args[0]);
        }
    }
}
