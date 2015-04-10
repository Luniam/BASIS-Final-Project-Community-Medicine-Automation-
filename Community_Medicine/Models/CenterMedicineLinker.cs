using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Medicine.Models {
    public class CenterMedicineLinker {

        public int ?Id { get; set; }

        public int CenterId { get; set; }

        public int MedicineId { get; set; }

        public int  Qty { get; set; }

    }
}