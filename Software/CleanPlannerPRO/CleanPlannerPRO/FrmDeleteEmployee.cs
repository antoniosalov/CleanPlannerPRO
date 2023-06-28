using CleanPlannerPRO.Models;
using CleanPlannerPRO.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanPlannerPRO {
    public partial class FrmDeleteEmployee : Form {
        public FrmDeleteEmployee() {
            InitializeComponent();
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e) {

            if(cboEmployee.SelectedItem == null) {
                MessageBox.Show("Odabir zaposlenika je obavezan", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                string selectedEmployee = cboEmployee.GetItemText(cboEmployee.SelectedItem);
                string OIB = selectedEmployee.Substring(selectedEmployee.Length - 12);
                OIB = OIB.Substring(0, OIB.Length - 1);

                EmployeeRepository.DeleteEmployee(OIB);

                FrmEmployees frmEmployees = new FrmEmployees();
                Hide();
                frmEmployees.ShowDialog();
                Close();
            }
        }

        private void FrmDeleteEmployee_Load(object sender, EventArgs e) {
            List<Employee> employees = EmployeeRepository.GetEmployees();
            foreach(Employee employee in employees) {
                cboEmployee.Items.Add($"{employee.Ime} {employee.Prezime} (OIB: {employee.OIB})");
            }
        }

        private void pcbBack_Click(object sender, EventArgs e) {
            FrmEmployees frmEmployees = new FrmEmployees();
            Hide();
            frmEmployees.ShowDialog();
            Close();
        }
    }
}
