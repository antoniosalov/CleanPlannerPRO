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
    public partial class FrmEmployees : Form {
        public FrmEmployees() {
            InitializeComponent();
        }

        private void FrmEmployees_Load(object sender, EventArgs e) {

            List<Employee> employees = new List<Employee>();
            foreach (Employee employee in EmployeeRepository.GetEmployees()) {
                employee.Korisnicko_ime = "";
                employee.Lozinka = "";
                employees.Add(employee);
            }
            dgvEmployees.DataSource = employees;
        }

        private void btnCreateEmployee_Click(object sender, EventArgs e) {
            FrmCreateEmployee frmCreateEmployee = new FrmCreateEmployee();
            Hide();
            frmCreateEmployee.ShowDialog();
            Close();

        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e) {
            FrmDeleteEmployee frmDeleteEmployee = new FrmDeleteEmployee();
            Hide();
            frmDeleteEmployee.ShowDialog();
            Close();
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e) {
            FrmUpdateEmployee frmUpdateEmployee = new FrmUpdateEmployee();
            Hide();
            frmUpdateEmployee.ShowDialog();
            Close();
        }

        private void pcbSearch_Click(object sender, EventArgs e) {

            List<Employee> employees = new List<Employee>();
            foreach(Employee employee in EmployeeRepository.GetEmployees()) {
                employee.Korisnicko_ime = "";
                employee.Lozinka = "";
                if (employee.Ime.ToLower().Contains(txtSearch.Text.ToLower()) || employee.Prezime.ToLower().Contains(txtSearch.Text.ToLower())) {
                    employees.Add(employee);
                }
            }

            dgvEmployees.DataSource = employees;
            if (employees.Count == 0) {
                MessageBox.Show("Nije pronađen niti jedan zaposlenik", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Employee> employeesAll = new List<Employee>();
                foreach (Employee employee in EmployeeRepository.GetEmployees()) {
                    employee.Korisnicko_ime = "";
                    employee.Lozinka = "";
                    employeesAll.Add(employee);
                }
                dgvEmployees.DataSource = employeesAll;
            } else {
                dgvEmployees.DataSource = employees;
            }

        }

        private void btnShowHide_Click(object sender, EventArgs e) {

            if (btnShowHide.Text.Contains("Prikaži")) {
                List<Employee> employees = EmployeeRepository.GetEmployees();
                dgvEmployees.DataSource = employees;
                btnShowHide.Text = "Sakrij podatke za prijavu";
            } else {
                List<Employee> employees = new List<Employee>();
                foreach (Employee employee in EmployeeRepository.GetEmployees()) {
                    employee.Korisnicko_ime = "";
                    employee.Lozinka = "";
                    employees.Add(employee);
                }
                btnShowHide.Text = "Prikaži podatke za prijavu";

                dgvEmployees.DataSource = employees;
            }
        }

        private void pcbLogout_Click(object sender, EventArgs e) {
            FrmLogin frmLogin = new FrmLogin();
            Hide();
            frmLogin.ShowDialog();
            Close();
        }
    }
}
