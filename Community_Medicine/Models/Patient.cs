using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Medicine.Models {
    public class Patient {

        public int Id { get; set; }

        public int VoterId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string BirthDay { get; set; }

    }
}