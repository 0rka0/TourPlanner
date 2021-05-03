using Npgsql;
using System;
using System.Data;
using TourPlannerModels.TourObject;
using TourPlannerModels;
using log4net;
using System.Reflection;
using System.Collections.Generic;

namespace TourPlannerDAL.Databases
{
    public abstract class BaseDatabaseHandler : IDatabase
    {
        protected static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected string connString = string.Empty;
        protected NpgsqlConnection conn;
        protected string _table { get; set; }

        public BaseDatabaseHandler(string table)
        {
            _logger.Info("Database initialized");

            _table = table;

            connString = Configuration.ConnectionString;
            conn = new NpgsqlConnection(connString);
            conn.Open();
        }

        protected void CheckConn()
        {
            if (conn.State != ConnectionState.Open)
            {
                _logger.Warn("Connection restarted");
                conn.Close();
                conn.Open();
            }
        }

        public int GetMaxId()
        {
            int maxId = 0;
            try
            {
                using (var cmd = new NpgsqlCommand($"SELECT max(id) FROM {_table}", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        maxId = (int)reader[0];
            }
            catch (Exception e)
            {
            }
            return maxId + 1;
        }

        public void DeleteEntry(int id)
        {
            CheckConn();

            using (var cmd = new NpgsqlCommand($"DELETE FROM {_table} WHERE (id = @id)", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public abstract IEnumerable<ITourObject> SelectEntries(int id);

        public abstract void UpdateEntry(ITourObject tourObject);
        public abstract void InsertEntry(ITourObject tourObject);
    }
}
