using Npgsql;
using System;
using System.Collections.Generic;
using TourPlannerModels;
using TourPlannerModels.TourObject;

namespace TourPlannerDAL.Databases
{
    public class AttractionDatabaseHandler : BaseDatabaseHandler
    {
        private static AttractionDatabaseHandler _db;

        AttractionDatabaseHandler() : base(Configuration.AttractionTable)
        {
            _logger.Info("Tour Database initialized");
        }

        public static IDatabase GetInstance()
        {

            if (_db == null)
            {
                _db = new AttractionDatabaseHandler();
            }
            return _db;
        }

        public override void InsertEntry(ITourObject tourObject)
        {
            CheckConn();
            Attraction attraction = (Attraction)tourObject;

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO attractions VALUES (@id, @pid, @name, @rating, @total_ratings, @address, @tid)", conn))
                {      //adding parameters
                    cmd.Parameters.AddWithValue("@id", attraction.Id);
                    cmd.Parameters.AddWithValue("@pid", attraction.Pid);
                    cmd.Parameters.AddWithValue("@name", attraction.Name);
                    cmd.Parameters.AddWithValue("@rating", attraction.Rating);
                    cmd.Parameters.AddWithValue("@total_ratings", attraction.TotalRatings);
                    cmd.Parameters.AddWithValue("@address", attraction.Address);
                    cmd.Parameters.AddWithValue("@tid", attraction.TourId);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Attraction entry could not be inserted into Database");
            }
        }

        public override IEnumerable<ITourObject> SelectEntries(int id)
        {
            CheckConn();
            List<Attraction> attractionList = new List<Attraction>();

            try
            {
                using (var cmd = new NpgsqlCommand($"Select * FROM attractions WHERE tid=@tid", conn))
                {
                    cmd.Parameters.Add("@tid", NpgsqlTypes.NpgsqlDbType.Integer);
                    cmd.Parameters[0].Value = id;
                    cmd.Prepare();
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            attractionList.Add(new Attraction((int)reader[0], reader[1].ToString(), reader[2].ToString(), (float)reader[3], (int)reader[4], reader[5].ToString(), (int)reader[6]));
                        }
                }
            }
            catch (Exception)
            {
                throw new Exception("Attraction entries could not be selected from Database");
            }

            return attractionList;
        }

        public override void UpdateEntry(ITourObject tourObject)
        {
            throw new NotImplementedException("Data can not be updated");
        }
    }
}
