using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Http;
using System.Timers;
using System.Collections;
using System.Runtime.InteropServices;


namespace Hardverapro {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        readonly System.Timers.Timer timer = new System.Timers.Timer(10000);
        readonly ProcessPage page = new ProcessPage();
        int currentresults = 0;

        private void BtnStart_Click(object sender, EventArgs e) {
            this.Text = "0";
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;

            page.GetPages();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            timer.Stop();
            page.GetPages();

            Thread thread = new Thread(new ThreadStart(SetText));
            thread.Start();
            Thread.Sleep(1000);

            if (page.numberofresults > currentresults) {
                currentresults = page.numberofresults;

                //DialogResult msg = MessageBox.Show(Extensions.GetNewAds(page.numberofresults, ads), currentresults.ToString(), MessageBoxButtons.OK);
            } else if (page.numberofresults < currentresults) {
                //DialogResult msg = MessageBox.Show(Extensions.GetRemovedAds(ads, page.currentads), currentresults.ToString(), MessageBoxButtons.OK);
            }
            timer.Start();
        }

        private delegate void SafeCallDelegate(string text);
        private void WriteTextSafe(string text) {
            if (this.InvokeRequired) {
                var d = new SafeCallDelegate(WriteTextSafe);
                this.Invoke(d, new object[] { text });
            } else {
                this.Text = text;
            }
        }

        private void SetText() {
            WriteTextSafe(currentresults.ToString());
        }
    }

}
