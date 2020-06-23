using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms.VisualStyles;
using System.Collections;

namespace Hardverapro {
    class GetPage {
        private static readonly HttpClient httpClient;

        public int numberofresults = 0;
        public List<string> currentads = new List<string>();

        static GetPage() {
            httpClient = new HttpClient();
        }

        public void RunTask() {
            Task.Run(Main).Wait();
        }

        async Task Main() {
            try {
                HttpResponseMessage response = await httpClient.GetAsync("https://hardverapro.hu/aprok/hardver/videokartya/keres.php?stext=1660&county=&stcid=&settlement=&stmid=&minprice=&maxprice=&company=&cmpid=&user=&usrid=&selling=1&stext_none=");
                response.EnsureSuccessStatusCode();
                string responsebody = await response.Content.ReadAsStringAsync();
                numberofresults = responsebody.GetNumber();
                currentads = responsebody.GetAds();

                Console.WriteLine(numberofresults);


            } catch (HttpRequestException e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }


    }
}
