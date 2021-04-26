using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
            connString = Configuration.ConnectionString;
            conn = new NpgsqlConnection(connString);
            conn.Open();
        }

        public static DatabaseHandler GetInstance()
        {
            if (_db == null)
            {
                _db = new DatabaseHandler();
            }
            return _db;
        }

        public IEnumerable<Tour> SelectAllTourEntries()
        {
            CheckConn();
            List<Tour> tourList = new List<Tour>();

            using(var cmd = new NpgsqlCommand("SELECT * FROM tours", conn))
            using (var reader = cmd.ExecuteReader())
                while(reader.Read())
                {
                    tourList.Add(new Tour((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                }

            return tourList;
        }

        public void UpdateTourEntry(Tour tour)
        {
            CheckConn();

            using (var cmd = new NpgsqlCommand("UPDATE tours SET tourname = @name, description = @desc, information = @inf WHERE id = @id", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@name", tour.Name);
                cmd.Parameters.AddWithValue("@desc", tour.TourDescription);
                cmd.Parameters.AddWithValue("@inf", tour.RouteInformation);
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[3].Value = tour.Id;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertTourEntry(Tour tour)
        {
            CheckConn();

            using (var cmd = new NpgsqlCommand("INSERT INTO tours VALUES (@id, @tn, @desc, @inf, @dis, @img)", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@id", tour.Id);
                cmd.Parameters.AddWithValue("@tn", tour.Name);
                cmd.Parameters.AddWithValue("@desc", tour.TourDescription);
                cmd.Parameters.AddWithValue("@inf", tour.RouteInformation);
                cmd.Parameters.AddWithValue("@dis", tour.Distance);
                cmd.Parameters.AddWithValue("@img", tour.Image);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTourEntry(int id)
        {
            CheckConn();

            using (var cmd = new NpgsqlCommand("DELETE FROM tours WHERE (id = @id)", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetMaxId()
        {
            int maxId = 0;
            try
            {
                using (var cmd = new NpgsqlCommand("SELECT max(id) FROM tours", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        maxId = (int)reader[0];
            }
            catch (Exception e)
            {
            }
            return maxId+1;
        }

        void CheckConn()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
        }
    }
}
