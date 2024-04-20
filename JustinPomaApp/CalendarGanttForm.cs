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
            //schedulerDataStorage1.CreateAppointment = new myDataSource().dbAppointments;

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

    //public class myDataSource
    //{
    //    public List<dbTaskDependencies> dbTaskDependencies;
    //    public List<dbResources> dbResources;
    //    public List<dbAppointments> dbAppointments;
    //}

    //public class dbTaskDependencies
    //{
    //    public int Id { get; set; }
    //    public int ParentId { get; set; }
    //    public int DependentId { get; set; }
    //    public int Type { get; set; }

    //    public dbTaskDependencies(int Id_, int ParentId_, int DependentId_, int Type_)
    //    {
    //        this.Id = Id_;
    //        this.ParentId = ParentId_;
    //        this.DependentId = DependentId_;
    //        this.Type = Type_;
    //    }
    //}

    //public class dbResources
    //{
    //    public int Id { get; set; }
    //    public int IdSort { get; set; }
    //    public int ParentId { get; set; }
    //    public string Description { get; set; }
    //    public int Color { get; set; }
    //    public byte[] image { get; set; }
    //    public string CustomField1 { get; set; }

    //    public dbResources(int Id_, int IdSort_, int ParentId_, string Description_, int Color_, byte[] image_, string CustomField1_)
    //    {
    //        this.Id = Id_;
    //        this.IdSort = IdSort_;
    //        this.ParentId = ParentId_;
    //        this.Description = Description_;
    //        this.Color = Color_;
    //        this. = _;
    //        this.CustomField1 = CustomField1_;
    //    }
    //}

    //public class dbAppointments
    //{
    //    public int UniqueId { get; set; }
    //    public int Type { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public bool AllDay { get; set; }
    //    public string Subject { get; set; }
    //    public string Location { get; set; }
    //    public string Description { get; set; }
    //    public int Status { get; set; }
    //    public int Label { get; set; }
    //    public int ResourceId { get; set; }
    //    public string ResourceIds { get; set; }
    //    public string ReminderInfo { get; set; }
    //    public string RecurrenceInfo { get; set; }
    //    public int PercentComplete { get; set; }
    //    public string TimeZoneId { get; set; }
    //    public string CustomField1 { get; set; }

    //    public dbAppointments(int UniqueId_, int Type_, DateTime StartDate_, DateTime EndDate_, bool AllDay_, string Subject_, string Location_, string Description_, int Status_, int Label_, int ResourceId_, string ResourceIds_, string ReminderInfo_, string RecurrenceInfo_, int PercentComplete_, string TimeZoneId_, string CustomField1_)
    //    {
    //        this.UniqueId = UniqueId_;
    //        this.Type = Type_;
    //        this.StartDate = StartDate_;
    //        this.EndDate = EndDate_;
    //        this.AllDay = AllDay_;
    //        this.Subject = Subject_;
    //        this.Location = Location_;
    //        this.Description = Description_;
    //        this.Status = Status_;
    //        this.Label = Label_;
    //        this.ResourceId = ResourceId_;
    //        this.ResourceIds = ResourceIds_;
    //        this.ReminderInfo = ReminderInfo_;
    //        this.RecurrenceInfo = RecurrenceInfo_;
    //        this.PercentComplete = PercentComplete_;
    //        this.TimeZoneId = TimeZoneId_;
    //        this.CustomField1 = CustomField1_;
    //    }
    //}
}