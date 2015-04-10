using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Community_Medicine.Models;

namespace Community_Medicine.Data {
    public class MedicineGateway : Gateway {

        public bool Save(Medicine m) {
            connection = new SqlConnection(connectionString);

            string query = "INSERT INTO medicine VALUES (@name, @dose)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", m.Name);
            command.Parameters.AddWithValue("@dose", m.Dose);

            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected > 0;
        }

        public List<Medicine> GetAll()
        {
            List<Medicine> meds = new List<Medicine>();

            connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM medicine";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                Medicine aMed = new Medicine()
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Name = reader["name"].ToString(),
                    Dose = reader["dose"].ToString()
                };
                meds.Add(aMed);
            }

            reader.Close();
            connection.Close();

            return meds;
        }


        //returns a MedAmountDictionary class with the medicine name as key and quantity as value
        public MedAmountDictionary GetMedicineAmount(List<CenterMedicineLinker> currentLinkers)
        {

            MedAmountDictionary medAmount = new MedAmountDictionary();

            connection = new SqlConnection(connectionString);

            

            foreach (CenterMedicineLinker linker in currentLinkers)
            {
                string query = "SELECT * FROM medicine WHERE id = @med_id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@med_id", linker.MedicineId);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        medAmount.medicineAmount[reader["name"].ToString()] =
                            medAmount.medicineAmount[reader["name"].ToString()] + linker.Qty;
                    }
                    catch (Exception)
                    {
                        medAmount.medicineAmount[reader["name"].ToString()] = linker.Qty;
                    }
                }

                reader.Close();
                connection.Close();
            
            }

            return medAmount;
        }
    }
}