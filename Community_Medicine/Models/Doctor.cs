using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Medicine.Models {
    public class Doctor {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Degree { get; set; }

        public string Specialization { get; set; }

        public int CenterId { get; set; }
    }
}