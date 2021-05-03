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

            using (var cmd = new NpgsqlCommand($"SELECT * FROM tourlogs WHERE tid={id}", conn))
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    tourLogList.Add(new TourLog((int)reader[0], (DateTime)reader[1], reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), (int)reader[5], reader[6].ToString(), reader[7].ToString(), (int)reader[8]));
                }

            return tourLogList;
        }

        public override void UpdateEntry(ITourContent tourObj)
        {
            CheckConn();
            TourLog tourLog = (TourLog)tourObj;

            using (var cmd = new NpgsqlCommand("UPDATE tourlogs SET date = @date, report = @rep, distance = @dis, totaltime = @tt, rating = @rating, comm = @comment, avgspeed = @avgspeed, tid = @tid", conn))
            {      //adding parameters
                cmd.Parameters.Add("@date", NpgsqlTypes.NpgsqlDbType.Timestamp);
                cmd.Parameters[0].Value = tourLog.Date;
                cmd.Parameters.AddWithValue("@rep", tourLog.Report);
                cmd.Parameters.AddWithValue("@dis", tourLog.Distance);
                cmd.Parameters.AddWithValue("@tt", tourLog.TotalTime);
                cmd.Parameters.Add("@rating", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[4].Value = tourLog.Rating;
                cmd.Parameters.AddWithValue("@comment", tourLog.Comment);
                cmd.Parameters.AddWithValue("@avgspeed", tourLog.AvgSpeed);
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

            using (var cmd = new NpgsqlCommand("INSERT INTO tourlogs VALUES (@id, @date, @rep, @dis, @tt, @rat, @comm, @avgspeed, @tid)", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@id", tourLog.Id);
                cmd.Parameters.AddWithValue("@date", tourLog.Date);
                cmd.Parameters.AddWithValue("@rep", tourLog.Report);
                cmd.Parameters.AddWithValue("@dis", tourLog.Distance);
                cmd.Parameters.AddWithValue("@tt", tourLog.TotalTime);
                cmd.Parameters.AddWithValue("@rat", tourLog.Rating);
                cmd.Parameters.AddWithValue("@comm", tourLog.Comment);
                cmd.Parameters.AddWithValue("@avgspeed", tourLog.AvgSpeed);
                cmd.Parameters.AddWithValue("@tid", tourLog.TourId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
