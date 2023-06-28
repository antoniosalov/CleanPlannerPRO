using CleanPlannerPRO.Models;
using CleanPlannerPRO.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CleanPlannerPRO {
    public partial class FrmCreateEmployee : Form {
        public FrmCreateEmployee() {
            InitializeComponent();
        }

        private void btnCreateEmployee_Click_1(object sender, EventArgs e) {

            bool duplicateValue = false;
            List<Employee> employees = EmployeeRepository.GetEmployees();
            foreach(Employee employee in employees) {
                if (employee.OIB.Equals(txtOIB.Text)) {
                    duplicateValue = true;
                    break;
                }
            }

            if (txtOIB.Text.Length != 11) {
                MessageBox.Show("OIB mora sadržavati 11 znakova", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if(duplicateValue){
                MessageBox.Show("Već postoji zaposlenik čiji OIB odgovara upisanom. OIB mora biti jedinstven.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (!rdbMale.Checked && !rdbFemale.Checked) {
                MessageBox.Show("Odabir spola je obavezan", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (txtName.Text == "" || txtSurname.Text == "" || txtEmail.Text == "" || txtPhoneNumber.Text == "") {
                MessageBox.Show("Sva polja moraju biti popunjena", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (rdbMale.Checked) {
                EmployeeRepository.CreateEmployee(txtOIB.Text, txtName.Text, txtSurname.Text, rdbMale.Text, txtEmail.Text, txtPhoneNumber.Text);
                FrmEmployees frmEmployees = new FrmEmployees();
                Hide();
                frmEmployees.ShowDialog();
                Close();
            } else if (rdbFemale.Checked) {
                EmployeeRepository.CreateEmployee(txtOIB.Text, txtName.Text, txtSurname.Text, rdbFemale.Text, txtEmail.Text, txtPhoneNumber.Text);
                FrmEmployees frmEmployees = new FrmEmployees();
                Hide();
                frmEmployees.ShowDialog();
                Close();
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
