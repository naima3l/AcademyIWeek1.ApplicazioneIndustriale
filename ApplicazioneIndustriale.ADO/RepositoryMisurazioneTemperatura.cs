using ApplicazioneIndustriale.CORE.Entities;
using ApplicazioneIndustriale.CORE.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicazioneIndustriale.ADO
{
    public class RepositoryMisurazioneTemperatura : IRepositoryMisurazioneTemperatura
    {
        const string connectionString = @"Data Source = (localdb)\mssqllocaldb;" +
                                    "Initial Catalog = AcademyI;" +
                                    "Integrated Security = true;";

        public void Add(MisurazioneTemperatura item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO Temperatura(Temperatura, DataMisurazione, OraMisurazione) VALUES(@temp, @data, @ora)";
                    command.Parameters.AddWithValue("@temp", item.Temperatura);
                    command.Parameters.AddWithValue("@data", item.DataMisurazione);
                    command.Parameters.AddWithValue("@ora", item.OraMisurazione);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MisurazioneTemperatura> GetItemsWithOutState()
        {
            List<MisurazioneTemperatura> temperature = new List<MisurazioneTemperatura>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM Temperatura WHERE Stato IS NULL";

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MisurazioneTemperatura temperatura = new MisurazioneTemperatura();
                        temperatura.Id = (int)reader["Id"];
                        temperatura.DataMisurazione = (DateTime)reader["DataMisurazione"]; // =Convert.ToDateTime(...)
                        temperatura.OraMisurazione = (TimeSpan)reader["OraMisurazione"];
                        temperatura.Temperatura = (double)reader["Temperatura"];

                        temperature.Add(temperatura);
                    }

                    return temperature;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Update(MisurazioneTemperatura item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.CommandText = "UPDATE Temperatura SET Stato = @state WHERE Id=@id";
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.Parameters.AddWithValue("@state", item.Stato);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
