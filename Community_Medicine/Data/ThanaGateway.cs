using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Community_Medicine.Models;

namespace Community_Medicine.Data {
    public class ThanaGateway : Gateway {

        public List<Thana> GetAllThanas(int distId)
        {
            List<Thana> thanas = new List<Thana>();

            connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM thana WHERE dist_id = @distid";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@distid", distId);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                Thana aThana = new Thana()
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Name = reader["name"].ToString(),
                    DistrictId = int.Parse(reader["dist_id"].ToString())
                };
                

                thanas.Add(aThana);
            }

            reader.Close();
            connection.Close();

            return thanas;
        }

        public int GetThanaId(int distid, int thanaid)
        {
            connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM thana WHERE dist_id = @distid";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@distid", distid);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (thanaid == 0)
                thanaid++;

            int i = 0;

            Thana aThana = null;

            while (reader.Read() && i <= thanaid) {
                aThana = new Thana() {
                    Id = int.Parse(reader["id"].ToString()),
                    Name = reader["name"].ToString(),
                    DistrictId = int.Parse(reader["dist_id"].ToString())
                };

                i++;

            }

            reader.Close();
            connection.Close();

            return aThana.Id;
        }
    }
}