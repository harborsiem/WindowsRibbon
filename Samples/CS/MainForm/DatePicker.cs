using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class DatePicker : Form
    {
        public DatePicker()
        {
            InitializeComponent();
            monthCalendar.DateSelected += MonthCalendar_DateChanged;
            //monthCalendar.DateChanged += MonthCalendar_DateChanged;
            //DateTime now = DateTime.Now;
            //monthCalendar.SelectionRange = new SelectionRange(now, now.AddDays(7));
            //monthCalendar.MinDate = now;
            //monthCalendar.MaxDate = now.AddDays(7);
        }

        public string Label
        {
            get;
            private set;
        }

        private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            Label = e.Start.ToShortDateString();
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
