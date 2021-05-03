using Npgsql;
using System.Collections.Generic;
using TourPlannerModels;
using TourPlannerModels.TourObject;

namespace TourPlannerDAL.Databases
{
    public class TourDatabaseHandler : BaseDatabaseHandler
    {
        private static TourDatabaseHandler _db;

        TourDatabaseHandler() : base(Configuration.TourTable)
        {
        }

        public static IDatabase GetInstance()
        {
            _logger.Info("Database accessed");

            if (_db == null)
            {
                _db = new TourDatabaseHandler();
            }
            return _db;
        }

        public override IEnumerable<ITourObject> SelectEntries(int id = 0)
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

        public override void UpdateEntry(ITourObject tourObj)
        {
            CheckConn();
            Tour tour = (Tour)tourObj;

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

        public override void InsertEntry(ITourObject tourObj)
        {
            CheckConn();
            Tour tour = (Tour)tourObj;

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
    }
}
