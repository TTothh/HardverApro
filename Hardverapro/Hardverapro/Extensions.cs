using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Hardverapro {
    static class Extensions {
        public static List<int> AllIndexesOf(this string str, string value) {
            if (String.IsNullOrEmpty(value)) {
                throw new ArgumentException("the string to find may not be empty", "value");
            }

            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length) {
                index = str.IndexOf(value, index);
                if (index == -1) {
                    return indexes;
                }
                indexes.Add(index);
            }
        }

        /*public static int GetNumber(this string responsebody) {
            string searchtext = "Találatok száma: ";
            string number = responsebody.Substring(responsebody.IndexOf(searchtext) + searchtext.Length, 2);

            return int.Parse(number);
        }*/

        public static List<string> GetAds(this string response) {
            List<string> ads = new List<string>();
            List<int> adtitles = response.AllIndexesOf("</a> <small class=");

            for (int i = 0; i < adtitles.Count; i++) {
                string current = response.Substring(adtitles[i] - 200, 200);
                string final = current.Substring(current.LastIndexOf('>'));

                ads.Add(final);
            }

            return ads;
        }

        public static string GetNewAds(List<string> currentads, List<string> previousads) {
            return currentads.Except(previousads).ToList().First().ToString();
        }
        public static string GetRemovedAds(List<string> currentads, List<string> previousads) {
            return previousads.Except(currentads).ToList().First().ToString();
        }
    }
}
