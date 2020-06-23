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
        readonly GetPage gp = new GetPage();
        int currentresults = 0;

        public ArrayList ads = new ArrayList();

        private void BtnStart_Click(object sender, EventArgs e) {
            this.Text = "Tock";
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            timer.Stop();
            List<string> previousads = gp.currentads;

            gp.RunTask();

            Thread thread = new Thread(new ThreadStart(SetText));
            thread.Start();
            Thread.Sleep(1000);

            if (gp.currentads.Count > previousads.Count) {
                currentresults = gp.numberofresults;

                DialogResult msg = MessageBox.Show(Extensions.GetNewAds(gp.currentads, previousads), currentresults.ToString(), MessageBoxButtons.OK);
            } else if (gp.numberofresults < currentresults) {
                DialogResult msg = MessageBox.Show(Extensions.GetRemovedAds(previousads, gp.currentads), currentresults.ToString(), MessageBoxButtons.OK);
            } else if (previousads.Count != 0) {

            }
            timer.Start();
        }

        //Code by Microsoft mert nem igazán fogom fel egyelőre hogy hogy csinálja amit csinál
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
