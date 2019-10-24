using MainProgram.Libraries;
using MainProgram.Utils;

namespace MainProgram.Extensions
{
    public static class EnumsExtensions
    {
        public static string GetName(this LibraryLanguage language)
        {
            switch (language)
            {
                case LibraryLanguage.Assembly:
                    return "Assembly";
                case LibraryLanguage.CSharp:
                    return "C#";
                default:
                    return string.Empty;
            }
        }

        public static string GetName(this Threads threads)
        {
            switch (threads)
            {
                case Threads.One:
                    return "1";
                case Threads.Two:
                    return "2";
                case Threads.Four:
                    return "4";
                case Threads.Eight:
                    return "8";
                default:
                    return string.Empty;
            }
        }
    }
}