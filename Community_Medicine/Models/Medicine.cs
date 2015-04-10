using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Community_Medicine.Models {
    public class Medicine {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Medicine Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "mg/mL")]
        public string Dose { get; set; }

    }
}