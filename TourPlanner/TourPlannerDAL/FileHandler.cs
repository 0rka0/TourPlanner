using System.IO;
using System.Threading.Tasks;
using TourPlannerModels;

namespace TourPlannerDAL
{
    public static class FileHandler
    {
        public static async Task SaveImage(Stream ms, string path)
        {
            await using FileStream fs = File.Create(path);
            ms.Seek(0, SeekOrigin.Begin);
            ms.CopyTo(fs);
        }

        public static void DeleteImage(string path)
        {
            File.Delete(path);
        }

        public static void CopyImage(string file1, string file2)
        {
            File.Copy(Configuration.ImagePath + file1, Configuration.ImagePath + file2);
        }
    }
}
