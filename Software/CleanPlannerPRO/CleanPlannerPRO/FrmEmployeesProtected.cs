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
    public partial class FrmEmployeesProtected : Form {
        public FrmEmployeesProtected() {
            InitializeComponent();
        }

        private void FrmEmployeesProtected_Load(object sender, EventArgs e) {

            List<Employee> employees = EmployeeRepository.GetEmployees();
            dgvEmployees.DataSource = employees;
            dgvEmployees.Columns["OIB"].Visible = false;
            dgvEmployees.Columns["Broj_mobitela"].Visible = false;
            dgvEmployees.Columns["Korisnicko_ime"].Visible = false;
            dgvEmployees.Columns["Lozinka"].Visible = false;
        }

        private void pcbSearch_Click(object sender, EventArgs e) {

            List<Employee> employees = new List<Employee>();
            foreach (Employee employee in EmployeeRepository.GetEmployees()) {
                if (employee.Ime.ToLower().Contains(txtSearch.Text.ToLower()) || employee.Prezime.ToLower().Contains(txtSearch.Text.ToLower())) {
                    employees.Add(employee);
                }
            }
            dgvEmployees.DataSource = employees;
            dgvEmployees.Columns["OIB"].Visible = false;
            dgvEmployees.Columns["Broj_mobitela"].Visible = false;
            dgvEmployees.Columns["Korisnicko_ime"].Visible = false;
            dgvEmployees.Columns["Lozinka"].Visible = false;

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

        private void pcbLogout_Click(object sender, EventArgs e) {

            FrmLogin frmLogin = new FrmLogin();
            Hide();
            frmLogin.ShowDialog();
            Close();
        }
    }
}
