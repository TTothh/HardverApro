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

        public static List<Item> GetAds(this string response) {
            List<Item> ads = new List<Item>();
            List<int> adtitles = response.AllIndexesOf("</a> <small class=");

            //kell: 
            string featured = @"<li class=" + "\"" + "media featured" + "\" " + "data-.*</li>.*<li";



            //itt volt a sok regexes szar. jó hogy elveszett mert legaláb töölni nem kell mert újra kell írni az egészet a faszba nulláról más logikával.

            //rework
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
