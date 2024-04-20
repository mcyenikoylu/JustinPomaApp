using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace JustinPomaApp
{
    public partial class MainSplashScreen : SplashScreen
    {
        private static Timer tmr = new Timer();
        public MainSplashScreen()
        {
            InitializeComponent();

            //2024 Copyright © Poma Architectural Metals
            this.labelControl1.Text = "Copyright © 1974-" + DateTime.Now.Year.ToString() + " Poma Architectural Metals";
            this.labelControl3.Text = "Version v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            tmr.Interval = 5000;
            tmr.Enabled = true;
            tmr.Tick += new EventHandler(TimerEventProcessor);
            tmr.Start();
        }

        private void TimerEventProcessor(Object sender, EventArgs e)
        {
            tmr.Stop();
            tmr.Dispose();
            this.Close();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void MainSplashScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm frm = new MainForm();
            frm.ShowDialog();
        }
    }
}