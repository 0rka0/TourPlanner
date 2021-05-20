﻿using TourPlannerBL.Mapquest;
using TourPlannerDAL.Files;
using TourPlannerDAL.Databases;
using TourPlannerModels;
using log4net;
using Newtonsoft.Json;
using System.Reflection;
using System;
using TourPlannerBL.StringPrep;
using TourPlannerModels.TourObject;
using System.Collections.Generic;
using System.IO;

namespace TourPlannerBL.TourObjectHandling
{
    static public class TourHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static public void AddTour(string start, string goal, string desc, string inf)
        {
            _logger.Info("Attempting to add Tour");

            try
            {
                TourInformationResponseObject information = MapQuestHandler.GetTourInformation(start, goal);
                Tour tour = CreateTourObject(information, StringPreparer.BuildName(start, goal), desc, inf);

                if (information.route.routeError.errorCode >= 0)
                {
                    throw new Exception("Request returned invalid error code");
                }

                InsertTour(tour, true);

                MapQuestHandler.GetImage(information, tour.Image);

                _logger.Info("Add success");
            }
            catch (Exception e)
            {
                _logger.Error("Adding process led to following error: " + e.Message);
            }
        }

        static public void DeleteTour(Tour tour)
        {
            _logger.Info("Attempting to delete Tour");

            try
            {
                IDatabase db = TourDatabaseHandler.GetInstance();
                db.DeleteEntry(tour.Id);
                FileHandler.DeleteImage(Configuration.ImagePath + tour.Image);

                _logger.Info("Deletion success");
            }
            catch (Exception e)
            {
                _logger.Error("Deletion process led to following error: " + e.Message);
            }
        }

        static public void EditTour(string name, string description, string information, Tour tour)
        {
            _logger.Info("Attempting to edit Tour");

            try
            {
                tour.SetEditData(name, description, information);

                IDatabase db = TourDatabaseHandler.GetInstance();
                db.UpdateEntry(tour);

                _logger.Info("Editing success");
            }
            catch (Exception e)
            {
                _logger.Error("Editing process led to following error: " + e.Message);
            }
        }

        static public void CopyTour(Tour tour)
        {
            _logger.Info("Attempting to copy Tour");

            try
            {
                Tour copy = tour.Clone();

                InsertTour(copy, true);

                FileHandler.CopyImage(tour.Image, copy.Image);

                _logger.Info("Copying success");
            }
            catch (Exception e)
            {
                _logger.Error("Copying process led to following error: " + e.Message);
            }
        }

        static Tour CreateTourObject(TourInformationResponseObject information, string name, string desc, string inf)
        {
            return new Tour(name, desc, inf, information.route.distance.ToString());
        }

        static private void InsertTour(Tour tour, bool newTour)
        {
            IDatabase db = TourDatabaseHandler.GetInstance();
            if (newTour)
            {
                tour.Id = db.GetMaxId();
                tour.Image = StringPreparer.BuildFilename(tour.Id, tour.Name);
            }
            db.InsertEntry(tour);
        }

        private static void ClearData()
        {
            IDatabase db = TourDatabaseHandler.GetInstance();
            db.ClearDb();
            db = TourLogDatabaseHandler.GetInstance();
            db.ClearDb();

            DirectoryInfo di = new DirectoryInfo(Configuration.ImagePath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        static private void RequestImportedTourImage(Tour tour)
        {
            _logger.Info("Attempting to request image");

            try
            {
                Tuple<string, string> locationTuple = StringPreparer.ExtractLocationFromFilename(tour.Image);
                TourInformationResponseObject information = MapQuestHandler.GetTourInformation(locationTuple.Item1, locationTuple.Item2);

                if (information.route.routeError.errorCode >= 0)
                {
                    throw new Exception("Request returned invalid error code");
                }

                MapQuestHandler.GetImage(information, tour.Image);

                _logger.Info("Image request success");
            }
            catch (Exception e)
            {
                _logger.Error("Requesting process led to following error: " + e.Message);
            }
        }

        public static void ImportTours(string path)
        {
            try
            {
                string jsonTourContent = FileHandler.ImportFromFile(path);
                List<Tour> importedTours = JsonConvert.DeserializeObject<List<Tour>>(jsonTourContent);

                ClearData();

                foreach (Tour tour in importedTours)
                {
                    RequestImportedTourImage(tour);
                    InsertTour(tour, false);

                    foreach (TourLog log in tour.LogList)
                    {
                        TourLogHandler.AddImportedTourLog(log);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("Importing process led to following error: " + e.Message);
            }
        }

        public static void ExportTours(string path)
        {
            try
            {
                string jsonTourContent = JsonConvert.SerializeObject(TourSelector.GetTours());

                FileHandler.ExportToFile(path, jsonTourContent);
            } 
            catch (Exception e)
            {
                _logger.Error("Exporting process led to following error: " + e.Message);
            }
        }
    }
}
