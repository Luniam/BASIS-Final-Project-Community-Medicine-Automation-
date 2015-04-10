using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Community_Medicine.Models;

namespace Community_Medicine.Data {
    public class DistrictGateway : Gateway {


        public List<District> GetAll()
        {
            List<District> d = new List<District>();
            
            connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM district";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                District aDistrict = new District()
                {
                    Name = reader["name"].ToString(),
                    Id = int.Parse(reader["id"].ToString())
                };

                d.Add(aDistrict);
            }

            reader.Close();
            connection.Close();

            return d;
        } 

    }
}