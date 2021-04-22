using Npgsql;
using System;

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

        public void InsertTour(Tour tour)
        {
            conn.Open();
            tour.Id = GetMaxId();

            using (var cmd = new NpgsqlCommand("INSERT INTO tours VALUES (@id, @tn, @desc, @inf, @dis, @img)", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@id", tour.Id);
                cmd.Parameters.AddWithValue("@tn", tour.Name);
                cmd.Parameters.AddWithValue("@desc", tour.TourDescription);
                cmd.Parameters.AddWithValue("@inf", tour.RouteInformation);
                cmd.Parameters.AddWithValue("@dis", tour.Distance);
                cmd.Parameters.AddWithValue("@img", tour.Location);
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
                using (var cmd = new NpgsqlCommand("Select max(uid) FROM tours", conn))
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
            return maxId;
        }
    }
}
