using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using Community_Medicine.Models;

namespace Community_Medicine.Data {
    public class CenterGateway : Gateway {



        //saves a center in the database in the center table
        public bool Save(Center c)
        {
            connection = new SqlConnection(connectionString);

            string query = "INSERT INTO center VALUES (@name, @dist_id, @thana_id)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", c.Name);
            command.Parameters.AddWithValue("@dist_id", c.DistrictId);
            command.Parameters.AddWithValue("@thana_id", c.ThanaId);

            int rowAffected = 0;
            connection.Open();
            try
            {
                rowAffected = command.ExecuteNonQuery();
            } catch(Exception)
            {
                rowAffected = 0;
            }
            connection.Close();
            return rowAffected > 0;
        }


        //gets all the centers from the center table of the db
        public List<Center> GetAll()
        {
            List<Center> centers = new List<Center>();

            connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM center";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                Center aCenter = new Center()
                {
                    Name = reader["name"].ToString(),
                    DistrictId = int.Parse(reader["dist_id"].ToString()),
                    Id = int.Parse(reader["id"].ToString())
                };

                centers.Add(aCenter);
            }

            reader.Close();
            connection.Close();

            return centers;
        }


        //saves a CenterMedicineLinker in the database in the center_medicine_linker table
        public bool SaveCenterMedicineLinker(CenterMedicineLinker cml)
        {
            connection = new SqlConnection(connectionString);

            string query = "INSERT INTO center_medicine_linker VALUES (@cent_id, @med_id, @qty)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("cent_id", cml.CenterId);
            command.Parameters.AddWithValue("med_id", cml.MedicineId);
            command.Parameters.AddWithValue("qty", cml.Qty);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected > 0;
        }


        public Center GetCenter(string user)
        {

            Center aCenter = null;

            connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM center WHERE name = @name";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", user);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                aCenter = new Center() {
                    Name = reader["name"].ToString(),
                    DistrictId = int.Parse(reader["dist_id"].ToString()),
                    Id = int.Parse(reader["id"].ToString())
                };

                
            }

            reader.Close();
            connection.Close();

            return aCenter;

        }

        public List<CenterMedicineLinker> GetCenterMedicineLinker(int id)
        {
            List<CenterMedicineLinker> linkers = new List<CenterMedicineLinker>();

            connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM center_medicine_linker WHERE cent_id = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                
                CenterMedicineLinker aLinker = new CenterMedicineLinker()
                {
                    CenterId = int.Parse(reader["cent_id"].ToString()),
                    Id = int.Parse(reader["id"].ToString()),
                    MedicineId = int.Parse(reader["med_id"].ToString()),
                    Qty = int.Parse(reader["qty"].ToString())
                };

                linkers.Add(aLinker);
                
            }

            reader.Close();
            connection.Close();

            return linkers;
        }


        public List<Center> GetCenters(int distid, int thanaid) {
            List<Center> centers = new List<Center>();

            connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM center WHERE dist_id = @distid AND thana_id = @thanaid";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@distid", distid);
            command.Parameters.AddWithValue("@thanaid", thanaid);


            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                Center aCenter = new Center() {
                    Name = reader["name"].ToString(),
                    DistrictId = int.Parse(reader["dist_id"].ToString()),
                    Id = int.Parse(reader["id"].ToString())
                };

                centers.Add(aCenter);
            }

            reader.Close();
            connection.Close();

            return centers;
        } 
    }
}