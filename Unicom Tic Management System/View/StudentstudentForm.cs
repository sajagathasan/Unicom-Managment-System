using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.View;

namespace Unicom_Tic_Management_System.View
{
    public partial class StudentstudentForm : Form
    {
        public StudentstudentForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Fullstakedevlopment fullstackForm = new Fullstakedevlopment();
            fullstackForm.FormClosed += (s, args) => this.Show();
            fullstackForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DatascienceForm datascienceForm = new DatascienceForm(); // updated class name
            datascienceForm.FormClosed += (s, args) => this.Show();
            datascienceForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AIForm aiForm = new AIForm();
            aiForm.FormClosed += (s, args) => this.Show();
            aiForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Show();
            MachinelearingForm machinelearingForm = new MachinelearingForm();
            machinelearingForm.FormClosed += (s, arge) => this.Show();
            machinelearingForm.Show();
        }
    }
}
