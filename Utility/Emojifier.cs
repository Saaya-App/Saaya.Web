using System.Text.RegularExpressions;

namespace Saaya.Web.Utility
{
    public static class Emojifier
    {
        public static string Emojify(this string content)
        {
            if (!File.Exists("emojis.txt"))
                File.Create("emojis.txt");

            var emojis = File.ReadAllLines("emojis.txt");
            foreach (var emoji in emojis)
            {
                var match = Regex.Match(content, $@"(?<=^|\s):{emoji.Split("|")[0]}:(?=\s|$)");
                if (match.Success)
                    content = content.Replace(match.Value, $"<img class=\"sp-liw__emoji\" alt=\"{emoji.Split("|")[0]}\" src=\"{emoji.Split("|")[1]}\" />");
            }

            return content;
        }
    }
}