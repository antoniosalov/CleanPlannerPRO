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
    public partial class FrmLogin : Form {
        public FrmLogin() {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e) {

            string usernameOwner = "vlasnik";
            Owner owner = OwnerRepository.GetOwner(usernameOwner);
            string passwordOwner = owner.Lozinka;

            if(txtUsername.Text != usernameOwner) {
                List<Employee> employees = EmployeeRepository.GetEmployees();
                foreach(Employee employee in employees) {
                    if (txtUsername.Text.Equals(employee.Korisnicko_ime)) {
                        if (txtPassword.Text.Equals(employee.Lozinka)) {
                            FrmEmployeesProtected frmEmployeesProtected = new FrmEmployeesProtected();
                            Hide();
                            frmEmployeesProtected.ShowDialog();
                            Close();
                        } else {
                            MessageBox.Show("Pogrešno korisničko ime ili lozinka", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } else {
                if (txtUsername.Text != usernameOwner || txtPassword.Text != passwordOwner) {
                    MessageBox.Show("Pogrešno korisničko ime ili lozinka", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else {
                    FrmEmployees frmEmployees = new FrmEmployees();
                    Hide();
                    frmEmployees.ShowDialog();
                    Close();
                }
            }
        }
    }
}
