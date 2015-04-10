using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Community_Medicine.Data {
    public class Gateway {

        protected string connectionString = WebConfigurationManager.ConnectionStrings["med"].ConnectionString;
        protected SqlConnection connection;

    }
}