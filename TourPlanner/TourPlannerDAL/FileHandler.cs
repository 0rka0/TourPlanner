using System.IO;
using System.Threading.Tasks;
using TourPlannerModels;
using log4net;
using System.Reflection;

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

            File.Delete(path);
        }

        public static void CopyImage(string file1, string file2)
        {
            _logger.Info("Copying image");

            File.Copy(Configuration.ImagePath + file1, Configuration.ImagePath + file2);
        }
    }
}
