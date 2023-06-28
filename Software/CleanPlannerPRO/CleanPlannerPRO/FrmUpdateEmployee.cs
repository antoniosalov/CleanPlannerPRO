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
    public partial class FrmUpdateEmployee : Form {
        public FrmUpdateEmployee() {
            InitializeComponent();
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e) {

            if(cboEmployee.SelectedItem == null) {
               MessageBox.Show("Odabir zaposlenika je obavezan", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (txtName.Text == "" || txtSurname.Text == "" || txtEmail.Text == "" || txtPhoneNumber.Text == "") {
                MessageBox.Show("Sva polja moraju biti popunjena", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                string selectedEmployee = cboEmployee.GetItemText(cboEmployee.SelectedItem);
                string OIB = selectedEmployee.Substring(selectedEmployee.Length - 12);
                OIB = OIB.Substring(0, OIB.Length - 1);

                if (rdbMale.Checked) {
                    EmployeeRepository.UpdateEmployee(OIB, txtName.Text, txtSurname.Text, rdbMale.Text, txtEmail.Text, txtPhoneNumber.Text);
                } else if (rdbFemale.Checked) {
                    EmployeeRepository.UpdateEmployee(OIB, txtName.Text, txtSurname.Text, rdbFemale.Text, txtEmail.Text, txtPhoneNumber.Text);
                }

                FrmEmployees frmEmployees = new FrmEmployees();
                Hide();
                frmEmployees.ShowDialog();
                Close();
            }
        }

        private void FrmUpdateEmpolyee_Load(object sender, EventArgs e) {

            List<Employee> employees = EmployeeRepository.GetEmployees();
            foreach (Employee employee in employees) {
                cboEmployee.Items.Add($"{employee.Ime} {employee.Prezime} (OIB: {employee.OIB})");
            }
        }

        private void cboEmployee_SelectedIndexChanged(object sender, EventArgs e) {

            string selectedEmployee = cboEmployee.GetItemText(cboEmployee.SelectedItem);
            string OIB = selectedEmployee.Substring(selectedEmployee.Length - 12);
            OIB = OIB.Substring(0, OIB.Length - 1);

            txtOIB.Enabled = true;
            txtOIB.Text = OIB;
            txtOIB.Enabled = false;

            List<Employee> employees = EmployeeRepository.GetEmployees();
            foreach (Employee employee in employees) {
                if (employee.OIB.Equals(OIB)) {
                    txtName.Text = employee.Ime;
                    txtSurname.Text = employee.Prezime;
                    txtEmail.Text = employee.Email;
                    txtPhoneNumber.Text = employee.Broj_mobitela;

                    if (employee.Spol.Equals("M")) {
                        rdbMale.Checked = true;
                    } else {
                        rdbFemale.Checked = true;
                    }

                }
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
