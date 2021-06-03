using System.IO;
using System.Threading.Tasks;
using TourPlannerModels;
using log4net;
using System.Reflection;
using System;

namespace TourPlannerDAL.Files
{
    public static class FileHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static async Task SaveImage(Stream ms, string path)
        {
            _logger.Info("Saving image to defined path");

            await using FileStream fs = File.Create(path);
            ms.Seek(0, SeekOrigin.Begin);
            ms.CopyTo(fs);
        }

        public static void DeleteImage(string path)
        {
            _logger.Info("Deleting image from defined path");

            try
            {
                File.Delete(path);
            }
            catch(Exception)
            {
                throw new Exception("File could not be deleted from defined path");
            }
        }

        public static void ClearImages()
        {
            _logger.Info("Clearing image folder");

            try
            {
                DirectoryInfo di = new DirectoryInfo(Configuration.ImagePath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            catch(Exception)
            {
                throw new Exception("Directory could not be cleared");
            }
        }

        public static void CopyImage(string file1, string file2)
        {
            _logger.Info("Copying image");

            try
            {
                File.Copy(Configuration.ImagePath + file1, Configuration.ImagePath + file2);
            }
            catch(Exception)
            {
                throw new Exception("File could not be copied in defined path");
            }
        }

        public static void ExportToFile(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch(Exception)
            {
                throw new Exception("Content could not be writte to file in defined path");
            }
        }

        public static string ImportFromFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch(Exception)
            {
                throw new Exception("Content of file could not be imported");
            }
        }
    }
}
