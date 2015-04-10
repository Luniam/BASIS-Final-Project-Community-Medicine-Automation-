using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Community_Medicine.Models;

namespace Community_Medicine.Data {
    public class DiseaseGateway : Gateway {

        public bool Save(Disease d) {
            connection = new SqlConnection(connectionString);

            string query = "INSERT INTO disease VALUES (@name, @description, @treatment, @drugs)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", d.Name);
            command.Parameters.AddWithValue("@description", d.Description);
            command.Parameters.AddWithValue("@treatment", d.Treatment);
            command.Parameters.AddWithValue("@drugs", d.Drugs);



            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected > 0;
        }

    }
}