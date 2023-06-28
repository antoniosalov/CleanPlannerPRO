using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanPlannerPRO.Models;
using DBLayer;

namespace CleanPlannerPRO.Repositories {
    public class EmployeeRepository {

        public static Employee GetEmployee(string OIB) {

            Employee employee = null;
            string sql = $"SELECT * FROM Employees WHERE OIB = {OIB}";
            DB.OpenConnection();
            SqlDataReader reader = DB.GetDataReader(sql);
            if (reader.HasRows) {
                reader.Read();
                employee = CreateObject(reader);
                reader.Close();
            }
            DB.CloseConnection();
            return employee;
        }

        public static List<Employee> GetEmployees() {

            List<Employee> employees = new List<Employee>();
            string sql = "SELECT * FROM Employees";
            DB.OpenConnection();
            SqlDataReader reader = DB.GetDataReader(sql);

            while (reader.Read()) {
                Employee employee = CreateObject(reader);
                employees.Add(employee);
            }

            reader.Close();
            DB.CloseConnection();

            return employees;
        }

        private static Employee CreateObject(SqlDataReader reader) {

            string OIB = reader["OIB"].ToString();
            string name = reader["Ime"].ToString();
            string surname = reader["Prezime"].ToString();
            string gender = reader["Spol"].ToString();
            string email = reader["Email"].ToString();
            string phoneNumber = reader["Broj_mobitela"].ToString();
            string username = reader["Korisnicko_ime"].ToString();
            string password = reader["Lozinka"].ToString();

            var employee = new Employee {
                OIB = OIB,
                Ime = name,
                Prezime = surname,
                Spol = gender,
                Email = email,
                Broj_mobitela = phoneNumber,
                Korisnicko_ime = username,
                Lozinka = password
            };

            return employee;
        }

        public static void CreateEmployee(string OIB, string name, string surname, string gender, string email, string phoneNumber) {

            string username = GenerateUsername(name, surname);

            string password = GeneratePassword();

            string sql = $"INSERT INTO Employees (OIB, Ime, Prezime, Spol, Email, Broj_mobitela, Korisnicko_ime, Lozinka) VALUES ('{OIB}', '{name}', '{surname}', '{gender}', '{email}', '{phoneNumber}', '{username}', '{password}')";
            DB.OpenConnection();
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }

        public static void DeleteEmployee(string OIB) {

            string sql = $"DELETE FROM Employees WHERE OIB = '{OIB}'";
            DB.OpenConnection();
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }

        public static void UpdateEmployee(string OIB, string name, string surname, string gender, string email, string phoneNumber) {

            string username = GenerateUsername(name, surname);
            
            string sql = $"UPDATE Employees SET Ime = '{name}', Prezime = '{surname}', Spol = '{gender}', Email = '{email}', Broj_mobitela = '{phoneNumber}', Korisnicko_ime = '{username}' WHERE OIB = '{OIB}'";
            DB.OpenConnection();
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }

        private static string GenerateUsername(string name, string surname) {

            List<Employee> employees = EmployeeRepository.GetEmployees();
            string username = name.ToLower()[0] + surname.ToLower();
            username = username.Replace("š", "s").Replace("đ", "d").Replace("č", "c").Replace("ć", "c").Replace("ž", "z");
            int counter = 0;
            foreach (Employee employee in employees) {
                if (employee.Korisnicko_ime.Contains(username)) counter++;
            }
            if (counter > 0) username += counter.ToString();
            foreach(Employee employee in employees) {
                if (employee.Korisnicko_ime.Contains(username)) {
                    username = username.Substring(0,username.Length-1) + (int.Parse(username.Substring(username.Length - 1))+1).ToString();
                }
            }

            return username;
        }

        private static string GeneratePassword() {

            string allLettersNumbers = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder pass = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < 8; i++) {
                pass.Append(allLettersNumbers[rand.Next(allLettersNumbers.Length)]);
            }
            string password = pass.ToString();

            return password;
        }
    }
}
