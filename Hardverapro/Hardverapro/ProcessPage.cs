using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms.VisualStyles;
using System.Collections;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Hardverapro {
    class ProcessPage {
        private HttpClient httpClient = new HttpClient();
        public int numberofresults { get; set; }
        public List<Item> ads { get; set; }
        public List<string> offsets { get; set; }

        public void GetPages() {
            ads.Clear();
            Task.Run(() => GetPage("https://hardverapro.hu/aprok/hardver/alaplap/index.html?offset=0"));

            for (int i = 1; i < offsets.Count; i++) {
                Task.Run(() => GetPage(offsets[i]));
            }
        }

        async private void GetPage(string uri) {
            try {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responsebody = await response.Content.ReadAsStringAsync();

                ads.AddRange(responsebody.GetAds());
                numberofresults = responsebody.GetResults();
                offsets = responsebody.GetOffsets();

            } catch (HttpRequestException e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                //return new List<string>();
            }
        }
    }
}
