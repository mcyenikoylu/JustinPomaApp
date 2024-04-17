﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JustinPomaApp
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();

            barStaticItem2.Caption = "Version v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCalendar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CalendarForm form = new CalendarForm();
            form.ShowDialog();
        }

        private void btnTimeLine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GanttForm form = new GanttForm();
            form.ShowDialog();
        }
    }
}