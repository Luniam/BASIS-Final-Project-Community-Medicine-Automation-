using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Community_Medicine.Models {
    public class Center {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Display(Name = "District")]
        public int DistrictId { get; set; }

        [Display(Name = "Thana")]
        public int ThanaId { get; set; }

        //<add name="DefaultConnection" connectionString="Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-Community_Medicine-20150401022257.mdf;Initial Catalog=aspnet-Community_Medicine-20150401022257;Integrated Security=True" providerName="System.Data.SqlClient" />

    }
}