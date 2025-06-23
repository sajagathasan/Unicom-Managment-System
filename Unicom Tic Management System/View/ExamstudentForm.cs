using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unicom_Tic_Management_System.View
{
    public partial class ExamstudentForm : Form
    {
        public ExamstudentForm()
        {
            InitializeComponent();
        }

        private Form previousForm;

        public ExamstudentForm(Form callingForm)
        {
            InitializeComponent();
            previousForm = callingForm;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            previousForm.Show();  
            this.Close();
        }
    }
}
