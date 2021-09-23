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
    public class RepositoryEsalazione : IRepositoryEsalazione
    {
        const string connectionString = @"Data Source = (localdb)\mssqllocaldb;" +
                                    "Initial Catalog = AcademyI;" +
                                    "Integrated Security = true;";

        public void Add(Esalazione item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO EsalazioniTossiche(ConcentrazionePpm, DataMisurazione, OraMisurazione) VALUES(@ppm, @data, @ora)";
                    command.Parameters.AddWithValue("@ppm", item.ConcentrazionePpm);
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

        public List<Esalazione> GetItemsWithOutState()
        {
            List<Esalazione> esalazioni = new List<Esalazione>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM EsalazioniTossiche WHERE Stato IS NULL";

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Esalazione esalazione = new Esalazione();
                        esalazione.Id = (int)reader["Id"];
                        esalazione.DataMisurazione = (DateTime)reader["DataMisurazione"]; // =Convert.ToDateTime(...)
                        esalazione.OraMisurazione = (TimeSpan)reader["OraMisurazione"];
                        esalazione.ConcentrazionePpm = (double)reader["ConcentrazionePpm"];

                        esalazioni.Add(esalazione);
                    }

                    return esalazioni;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Update(Esalazione item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = connection;

                    command.CommandText = "UPDATE EsalazioniTossiche SET Stato = @state WHERE Id=@id";
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
