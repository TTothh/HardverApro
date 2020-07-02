using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hardverapro {
    static class ProcessAds {
        public static string GetTitle(this string fragment) {

        }

        public static List<string> GetOffsets(this string fragment) {
            List<string> offsetlist = new List<string>();

            string pattern = @"<a class=" + "\"" + "dropdown - item" + "\"" + "href=.*</a>";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(pattern);

            for (int i = 0; i < (matches.Count - 2); i++) {
                string final = "https://hardverapro.hu" + matches[i].Value.Substring(matches[i].Value.IndexOf("href =") + 4, ((matches[i].Value.IndexOf(">") - 2) - matches[i].Value.IndexOf("href=") + 4));
                offsetlist.Add(final);
            }

            return offsetlist;
        }

        public static int GetPrice(this string fragment) {

        }

        public static string GetName(this string fragment) {

        }
        
        public static int GetReputation(this string fragment) {

        }

        public static int GetResults(this string fragment) {
            string pattern = @"offset = (\d{ 1,3})" + "\"" + @">\d{1,4} - \d{1,4}</a>";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            string match = regex.Match(fragment).Value;
            string final = match.Substring(match.IndexOf('-') + 1, (match.IndexOf("</a>") - match.IndexOf('-') + 1));

            return int.Parse(final);
        }
    }
}
