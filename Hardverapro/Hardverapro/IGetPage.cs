using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hardverapro {
    interface IGetPage {
        void GetPage();
        List<string> RunTask();
        Task<List<string>> Main();

        int numberofresults { get; set; }
        List<string> currentads { get; set; }

    }
}
