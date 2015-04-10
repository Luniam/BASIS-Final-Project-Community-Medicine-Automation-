using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Community_Medicine.Models;

namespace Community_Medicine.Data {
    public class DoctorGateway : Gateway {

        public bool Save(Doctor d) {
            connection = new SqlConnection(connectionString);

            string query = "INSERT INTO doctor VALUES (@name, @degree, @spec, @centid)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", d.Name);
            command.Parameters.AddWithValue("@degree", d.Degree);
            command.Parameters.AddWithValue("@spec", d.Specialization);
            command.Parameters.AddWithValue("@centid", d.CenterId);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected > 0;
        }

    }
}