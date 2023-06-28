using CleanPlannerPRO.Models;
using DBLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanPlannerPRO.Repositories {
    public class OwnerRepository {

        public static Owner GetOwner(string username) {
            
            Owner owner = null;
            string sql = $"SELECT * FROM Owner WHERE Korisnicko_ime='{username}'";
            DB.OpenConnection();
            SqlDataReader reader = DB.GetDataReader(sql);
            if (reader.HasRows) {
                reader.Read();
                owner = CreateObject(reader);
                reader.Close();
            }
            DB.CloseConnection();
            return owner;
        }

        private static Owner CreateObject(SqlDataReader reader) {

            string username = reader["Korisnicko_ime"].ToString();
            string password = reader["Lozinka"].ToString();

            var owner = new Owner {
                Korisnicko_ime = username,
                Lozinka = password
            };

            return owner;
        }
    }
}
