using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Medicine.Models {
    public class MedAmountDictionary {

        public Dictionary<string, int> medicineAmount; 

        public MedAmountDictionary()
        {
            medicineAmount = new Dictionary<string, int>();
        }

       

    }
}