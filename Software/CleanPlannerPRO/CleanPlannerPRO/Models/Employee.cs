using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanPlannerPRO.Models {
    public class Employee : Person {

        public string OIB { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Spol { get; set; }

        public string Email { get; set; }

        public string Broj_mobitela { get; set; }

    }
}
