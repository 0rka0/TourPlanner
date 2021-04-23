using Npgsql;
using System;
using System.Collections.Generic;
using TourPlannerModels;

namespace TourPlannerDAL
{
    public class DatabaseHandler
    {
        private static DatabaseHandler _db;
        string connString = string.Empty;
        NpgsqlConnection conn;

        DatabaseHandler()
        {
            connString = "Host=localhost;Username=postgres;Password=postgres;Database=TourPlanner";
            conn = new NpgsqlConnection(connString);
        }

        public static DatabaseHandler GetInstance()
        {
            if (_db == null)
            {
                _db = new DatabaseHandler();
            }
            return _db;
        }

        public IEnumerable<Tour> SelectAllTours()
        {
            conn.Open();
            List<Tour> tourList = new List<Tour>();

            using(var cmd = new NpgsqlCommand("SELECT * FROM tours", conn))
            using (var reader = cmd.ExecuteReader())
                while(reader.Read())
                {
                    tourList.Add(new Tour((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                }
            conn.Close();
            return tourList;
        }

        public void InsertTour(Tour tour)
        {
            conn.Open();
            tour.Id = GetMaxId();

            using (var cmd = new NpgsqlCommand("INSERT INTO tours VALUES (@id, @tn, @desc, @inf, @dis)", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@id", tour.Id);
                cmd.Parameters.AddWithValue("@tn", tour.Name);
                cmd.Parameters.AddWithValue("@desc", tour.TourDescription);
                cmd.Parameters.AddWithValue("@inf", tour.RouteInformation);
                cmd.Parameters.AddWithValue("@dis", tour.Distance);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        public void InsertImage(string filename, int id)
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand("UPDATE tours SET image = @img WHERE id = @id", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@img", filename);
                cmd.Parameters.AddWithValue("@uid", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        int GetMaxId()
        {
            int maxId = 0;
            try
            {
                using (var cmd = new NpgsqlCommand("Select max(id) FROM tours", conn))
                {
                    cmd.Prepare();
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                            maxId = (int)reader[0];
                }
            }
            catch (Exception e)
            {
            }
            return maxId+1;
        }
    }
}
