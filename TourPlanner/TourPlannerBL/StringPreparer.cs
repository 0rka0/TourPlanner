using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerBL
{
    static public class StringPreparer
    {
        static readonly string _urlDirections = "http://www.mapquestapi.com/directions/v2/route";
        static readonly string _urlStaticMap = "http://www.mapquestapi.com/staticmap/v5/map";
        static readonly string _key = "A1H6TsijwzAZ3cp7vu5cGAmVqEysE6gy"; //to be transfered into config file

        static public string BuildRequest(string start, string goal)
        {
            return String.Format("{0}?key={1}&from={2}&to={3}", _urlDirections, _key, start, goal);
        }

        static public string BuildRequest(string requestString)
        {
            return String.Format("{0}?key={1}&{2}", _urlStaticMap, _key, requestString);
        }

        static public string NameBuilder(string start, string goal)
        {
            return String.Format("{0}-{1}", start, goal);
        }

        static public Int64 GetInt64HashCode(string strText)
        {
            Int64 hashCode = 0;
            if (!string.IsNullOrEmpty(strText))
            {
                //Unicode Encode Covering all characterset
                byte[] byteContents = Encoding.Unicode.GetBytes(strText);
                System.Security.Cryptography.SHA256 hash =
                new System.Security.Cryptography.SHA256CryptoServiceProvider();
                byte[] hashText = hash.ComputeHash(byteContents);
                //32Byte hashText separate
                //hashCodeStart = 0~7  8Byte
                //hashCodeMedium = 8~23  8Byte
                //hashCodeEnd = 24~31  8Byte
                //and Fold
                Int64 hashCodeStart = BitConverter.ToInt64(hashText, 0);
                Int64 hashCodeMedium = BitConverter.ToInt64(hashText, 8);
                Int64 hashCodeEnd = BitConverter.ToInt64(hashText, 24);
                hashCode = hashCodeStart ^ hashCodeMedium ^ hashCodeEnd;
            }
            return (hashCode);
        }
    }
}
