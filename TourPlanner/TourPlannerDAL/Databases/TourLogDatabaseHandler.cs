using Npgsql;
using System;
using System.Collections.Generic;
using TourPlannerModels;
using TourPlannerModels.TourObject;


namespace TourPlannerDAL.Databases
{
    public class TourLogDatabaseHandler : BaseDatabaseHandler
    {
        private static TourLogDatabaseHandler _db;

        public TourLogDatabaseHandler() : base(Configuration.TourLogTable)
        {
            _logger.Info("TourLog Database initialized");
        }

        public static IDatabase GetInstance()
        {

            if (_db == null)
            {
                _db = new TourLogDatabaseHandler();
            }
            return _db;
        }

        public override IEnumerable<ITourObject> SelectEntries(int id = 0)
        {
            CheckConn();
            List<TourLog> tourLogList = new List<TourLog>();

            using (var cmd = new NpgsqlCommand($"SELECT * FROM tourlogs WHERE tid=@tid", conn))
            {
                cmd.Parameters.Add("@tid", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[0].Value = id;
                cmd.Prepare();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        tourLogList.Add(new TourLog((int)reader[0], (DateTime)reader[1], (float)reader[2], reader[3].ToString(), (float)reader[4], (int)reader[5],
                            (int)reader[6], (int)reader[7], (int)reader[8], (int)reader[9], (int)reader[10], (int)reader[11]));
                    }
            }

            return tourLogList;
        }

        public override void UpdateEntry(ITourObject tourObj)
        {
            CheckConn();
            TourLog tourLog = (TourLog)tourObj;

            using (var cmd = new NpgsqlCommand("UPDATE tourlogs SET date = @date, distance = @dis, report = @rep, totaltime = @tt, rating = @rating, avgspeed = @avgspeed, " +
                "weather = @weather, traffic = @traffic, breaks = @breaks, groupsize = @groupsize WHERE id = @id", conn))
            {      //adding parameters
                cmd.Parameters.Add("@date", NpgsqlTypes.NpgsqlDbType.Timestamp);
                cmd.Parameters[0].Value = tourLog.Date;
                cmd.Parameters.Add("@dis", NpgsqlTypes.NpgsqlDbType.Real);
                cmd.Parameters[1].Value = tourLog.Distance;
                cmd.Parameters.AddWithValue("@rep", tourLog.Report);
                cmd.Parameters.Add("@tt", NpgsqlTypes.NpgsqlDbType.Real);
                cmd.Parameters[3].Value = tourLog.TotalTime;
                cmd.Parameters.Add("@rating", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[4].Value = (int)tourLog.Rating;
                cmd.Parameters.Add("@avgspeed", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[5].Value = tourLog.AvgSpeed;
                cmd.Parameters.Add("@weather", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[6].Value = (int)tourLog.Weather;
                cmd.Parameters.Add("@traffic", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[7].Value = (int)tourLog.Traffic;
                cmd.Parameters.Add("@breaks", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[8].Value = tourLog.Breaks;
                cmd.Parameters.Add("@groupsize", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[9].Value = tourLog.GroupSize;
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer);
                cmd.Parameters[10].Value = tourLog.Id;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public override void InsertEntry(ITourObject tourObj)
        {
            CheckConn();
            TourLog tourLog = (TourLog)tourObj;

            using (var cmd = new NpgsqlCommand("INSERT INTO tourlogs VALUES (@id, @date, @dis, @rep, @tt, @rat, @avgspeed, @weather, @traffic, @breaks, @groupsize, @tid)", conn))
            {      //adding parameters
                cmd.Parameters.AddWithValue("@id", tourLog.Id);
                cmd.Parameters.AddWithValue("@date", tourLog.Date);
                cmd.Parameters.AddWithValue("@dis", tourLog.Distance);
                cmd.Parameters.AddWithValue("@rep", tourLog.Report);
                cmd.Parameters.AddWithValue("@tt", tourLog.TotalTime);
                cmd.Parameters.AddWithValue("@rat", (int)tourLog.Rating);
                cmd.Parameters.AddWithValue("@avgspeed", tourLog.AvgSpeed);
                cmd.Parameters.AddWithValue("@weather", (int)tourLog.Weather);
                cmd.Parameters.AddWithValue("@traffic", (int)tourLog.Traffic);
                cmd.Parameters.AddWithValue("@breaks", tourLog.Breaks);
                cmd.Parameters.AddWithValue("@Groupsize", tourLog.GroupSize);
                cmd.Parameters.AddWithValue("@tid", tourLog.TourId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
