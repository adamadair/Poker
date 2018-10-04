

using System;
using System.IO;

namespace TexasHoldemBot
{


    public static class BotIo
    {
        public static TextWriter Out { get; private set; }
        public static TextWriter Log { get; private set; }
        public static TextReader In { get; private set; }

        static BotIo()
        {
            SetOut(Console.Out);
            SetLog(Console.Error);
            SetIn(Console.In);
        }

        public static void SetOut(TextWriter w)
        {
            Out = w;            
        }

        public static void SetLog(TextWriter w)
        {
            Log = w;
        }

        public static void SetIn(TextReader r)
        {
            In = r;
        }
    }
}