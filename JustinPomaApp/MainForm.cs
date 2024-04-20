using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTabbedMdi;

namespace JustinPomaApp
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //string InventoryCategoryFormOpenedMessageBox = "You cannot open a new marketing search because another marketing search page is open.";

        public MainForm()
        {
            InitializeComponent();

            //UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
            barStaticItem2.Caption = "Version v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always; //ribbon barı masterForm ribbon üzerine aktarmak için gerekiyor.
            this.ribbonControl1.AutoSizeItems = true; //alt formlardaki ribbon üzerindeki BarItems ların düzgün hizalamak için gerekiyor.
        }

        private void btnCalendar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CalendarForm form = new CalendarForm();
            MenumItemClickMethod(form, e, btnCalendar.Tag.ToString());
        }

        private void btnTimeLine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CalendarGanttForm frm = new CalendarGanttForm();
            MenumItemClickMethod(frm, e, btnTimeLine.Tag.ToString());
        }
   
        #region MenumItemClickMethod
        private void MenumItemClickMethod(RibbonForm frm, ItemClickEventArgs e, string tag)
        {
            try
            {
                RibbonForm f = FormFind(frm.Name);
                if (mdiFormOpened(f))
                {
                    XtraMdiTabPage page = FindPageByText(f.Text);
                    xtraTabbedMdiManager1.SelectedPage = page;
                    ribbonControl1.HideApplicationButtonContentControl();
                    return;
                }
                f.Ribbon.HideApplicationButtonContentControl();
                f.Ribbon.MdiMergeStyle = RibbonMdiMergeStyle.Always;
                f.MdiParent = MainForm.ActiveForm;
                f.Show();
                f.Ribbon.SelectedPage = f.Ribbon.Pages[0];
                f.Ribbon.SelectedPage.Tag = tag;
            }
            catch (Exception ex)
            {

            }
        }

        private RibbonForm FormFind(string FormName)
        {
            try
            {
                if (FormName == "") return null;
                string FormTypeFullName = string.Format("{0}.{1}", this.GetType().Namespace, FormName);
                Type type = Type.GetType(FormTypeFullName, true);

                RibbonForm frm;

                frm = (RibbonForm)Activator.CreateInstance(type);
               
                return frm;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool mdiFormOpened(RibbonForm frm)
        {
            try
            {
                Form OpenForms = Application.OpenForms[frm.Name];
                if (OpenForms == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private XtraMdiTabPage FindPageByText(string pageText)
        {
            foreach (XtraMdiTabPage page in xtraTabbedMdiManager1.Pages)
            {
                if (page.MdiChild.Text == pageText)
                    return page;
            }
            return null;
        }
        #endregion

        private void xtraTabbedMdiManager1_SelectedPageChanged(object sender, EventArgs e)
        {
            try
            {
                if (xtraTabbedMdiManager1.SelectedPage == null)
                {
                    ribbonControl1.SelectedPage = this.Ribbon.Pages[0];
                    return;
                }

                //string formCaption = xtraTabbedMdiManager1.SelectedPage.Text;

                //var formList = Classes.Functions.db.S_Form(-1).ToList();
                string formName = xtraTabbedMdiManager1.SelectedPage.MdiChild.Name; // formList.Where(c => c.FormCaption == formCaption).FirstOrDefault().FormName.ToString();

                string FormTypeFullName = string.Format("{0}.{1}", this.GetType().Namespace, formName);
                Type type = Type.GetType(FormTypeFullName.Trim(), true);

                RibbonForm frm;
                //int formId = formList.Where(c => c.FormCaption == formCaption).FirstOrDefault().ID;
                //if (formId == 21)
                //    frm = (RibbonForm)Activator.CreateInstance(type, 3, "Store Definitions");//parametresi olan bir formu açmak için bu şekilde kullanıyorum.
                //else if (formId == 24 || formId == 25 || formId == 26 //dynamic inventory dispacth form
                //    || formId == 31 || formId == 32 || formId == 33 || formId == 27//)//dynamic inventory search 
                //|| formId == 35 || formId == 37 || formId == 38//) // dynamic inventory transfers
                //|| formId == 36 || formId == 39 || formId == 40//) //dynamic inventory transfer search
                //|| formId == 42 || formId == 43 || formId == 44)//sales
                //{
                //    frm = (RibbonForm)Activator.CreateInstance(type, -1, "");
                //}
                //else if (formId == 50 || formId == 51)
                //{
                //    frm = (RibbonForm)Activator.CreateInstance(type, true, "");
                //}
                //else if (formId == 6 || formId == 7)
                //{
                //    frm = (RibbonForm)Activator.CreateInstance(type, false, "");
                //}
                //else if (formId == 52)
                //{
                //    frm = (RibbonForm)Activator.CreateInstance(type, "");
                //}
                //else if (formId == 53 || formId == 45 || formId == 46 || formId == 47 || formId == 48)
                //{
                //    frm = (RibbonForm)Activator.CreateInstance(type, -1, "", true);
                //}
                //else
                    frm = (RibbonForm)Activator.CreateInstance(type);

                if (xtraTabbedMdiManager1.SelectedPage.MdiChild.Name == frm.Name)
                {
                    ribbonControl1.SelectedPage = frm.Ribbon.Pages[0];
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}