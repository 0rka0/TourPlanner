using Npgsql;
using System;
using System.Collections.Generic;
using TourPlannerModels;


namespace TourPlannerDAL
{
    public class TourLogDatabaseHandler : BaseDatabaseHandler
    {
        private static TourLogDatabaseHandler _db;

        public TourLogDatabaseHandler() : base(Configuration.TourLogTable)
        {
        }

        public static IDatabase GetInstance()
        {
            _logger.Info("Database accessed");

            if (_db == null)
            {
                _db = new TourLogDatabaseHandler();
            }
            return _db;
        }

        public override IEnumerable<ITourContent> SelectEntries(int id = 0)
        {
            CheckConn();
            List<TourLog> tourLogList = new List<TourLog>();

            using (var cmd = new NpgsqlCommand("SELECT * FROM tours", conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    tourLogList.Add(new TourLog((int)reader[0], (DateTime)reader[1], reader[2].ToString(), (int)reader[3], (int)reader[4], (int)reader[5], reader[6].ToString(), (int)reader[7], (int)reader[8]));
                }

            return tourLogList;
        }

        public override void UpdateEntry(ITourContent tourObj)
        {
            CheckConn();
            TourLog tourLog = (TourLog)tourObj;

            using (var cmd = new NpgsqlCommand("UPDATE tourlogs SET date = @date, report = @rep, distance = @dis, totaltime = @tt, rating = @rating, comment = @comment, avgspeed = @avgspeed, tourid = @tid", conn))
            {      //adding parameters
                cmd.Parameters.Add("@date", NpgsqlTypes.NpgsqlDbType.Date);
                cmd.Parameters[0].Value = tourLog.Date;
                cmd.Parameters.AddWithValue("@rep", tourLog.Report);
                cmd.Parameters.Add("@dis", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[2].Value = tourLog.Distance;
                cmd.Parameters.Add("@tt", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[3].Value = tourLog.TotalTime;
                cmd.Parameters.Add("@rating", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[4].Value = tourLog.Rating;
                cmd.Parameters.AddWithValue("@comment", tourLog.Comment);
                cmd.Parameters.Add("@avgspeed", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[6].Value = tourLog.AvgSpeed;
                cmd.Parameters.Add("@tid", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[7].Value = tourLog.TourId;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public override void InsertEntry(ITourContent tourObj)
        {
            CheckConn();
            TourLog tourLog = (TourLog)tourObj;

            using (var cmd = new NpgsqlCommand("INSERT INTO tours(id, tourid) VALUES (@id, @tid)", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@id", tourLog.Id);
                cmd.Parameters.AddWithValue("@tid", tourLog.TourId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
