using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraScheduler;

namespace JustinPomaApp
{
    public partial class CalendarGanttForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private BindingList<CustomResource> CustomResourceList = new BindingList<CustomResource>();
        private BindingList<CustomAppointment> CustomAppointmentList = new BindingList<CustomAppointment>();
        private BindingList<CustomDependency> CustomDependencyList = new BindingList<CustomDependency>();
        private int nextId;

        public CalendarGanttForm()
        {
            InitializeComponent();

            schedulerControl1.Start = System.DateTime.Now;
            schedulerDataStorage1.Resources.ColorSaving = DXColorSavingType.Auto;
            schedulerDataStorage1.AppointmentInserting += schedulerStorage_AppointmentInserting;
        }

        void schedulerStorage_AppointmentInserting(object sender, PersistentObjectCancelEventArgs e)
        {
            this.schedulerControl1.Storage.SetAppointmentId(((Appointment)e.Object), nextId++);
        }

        private void CalendarGanttForm_Load(object sender, EventArgs e)
        {
            InitResources();
            InitAppointments();
            InitDependencies();
        }

        private void InitDependencies()
        {
            AppointmentDependencyMappingInfo mappings = schedulerDataStorage1.AppointmentDependencies.Mappings;
            mappings.DependentId = "DependentId";
            mappings.ParentId = "ParentId";
            mappings.Type = "Type";

            CustomDependencyList.Add(new CustomDependency(1, 2, 0));
            CustomDependencyList.Add(new CustomDependency(1, 3, 0));
            CustomDependencyList.Add(new CustomDependency(3, 4, 0));

            schedulerDataStorage1.AppointmentDependencies.DataSource = CustomDependencyList;

        }

        private void InitResources()
        {
            ResourceMappingInfo mappings = this.schedulerDataStorage1.Resources.Mappings;
            mappings.Id = "ResID";
            mappings.Color = "ResColor";
            mappings.Caption = "Name";
            mappings.ParentId = "ParentId";

            CustomResourceList.Add(new CustomResource(1, "Project Deployment", Color.PowderBlue, 0));
            CustomResourceList.Add(new CustomResource(2, "Specifications", Color.PaleVioletRed, 1));
            CustomResourceList.Add(new CustomResource(3, "Spike Solution", Color.PeachPuff, 1));
            CustomResourceList.Add(new CustomResource(4, "Demos and Docs", Color.AliceBlue, 0));
            CustomResourceList.Add(new CustomResource(5, "Demos", Color.FloralWhite, 4));
            CustomResourceList.Add(new CustomResource(6, "Docs", Color.Honeydew, 4));
            this.schedulerDataStorage1.Resources.DataSource = CustomResourceList;
        }

        private void InitAppointments()
        {
            AppointmentMappingInfo mappings = this.schedulerDataStorage1.Appointments.Mappings;
            mappings.AppointmentId = "Id";
            mappings.Start = "StartTime";
            mappings.End = "EndTime";
            mappings.Subject = "Subject";
            mappings.AllDay = "AllDay";
            mappings.Description = "Description";
            mappings.Label = "Label";
            mappings.Location = "Location";
            mappings.RecurrenceInfo = "RecurrenceInfo";
            mappings.ReminderInfo = "ReminderInfo";
            mappings.ResourceId = "OwnerId";
            mappings.Status = "Status";
            mappings.Type = "EventType";
            mappings.PercentComplete = "PercentComplete";

            DateTime date = DateTime.Today.AddDays(2);
            CustomAppointmentList.Add(new CustomAppointment(1, "Spec", 2, 0, 1, date, date.AddDays(3), 100));
            CustomAppointmentList.Add(new CustomAppointment(2, "Spike", 3, 0, 2, date.AddDays(3), date.AddDays(5), 100));
            CustomAppointmentList.Add(new CustomAppointment(3, "Documentation-Fundamentals", 4, 0, 3, date.AddDays(2), date.AddDays(6), 100));
            CustomAppointmentList.Add(new CustomAppointment(4, "Documentation-Public API", 6, 0, 6, date.AddDays(7), date.AddDays(10), 0));
            this.nextId = (CustomAppointmentList.Max(a => a.Id)) + 1;
            this.schedulerDataStorage1.Appointments.DataSource = CustomAppointmentList;
        }
    }
}