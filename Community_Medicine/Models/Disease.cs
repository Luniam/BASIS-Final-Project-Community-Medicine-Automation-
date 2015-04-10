using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Community_Medicine.Models {
    public class Disease {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        
        [Display(Name = "Treatment Procedure")]
        public string Treatment { get; set; }

        
        [Display(Name = "Preferred Drugs")]
        public string Drugs { get; set; }

    }
}