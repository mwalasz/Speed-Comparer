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
    }
}