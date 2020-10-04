using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace library.FileManager
{
   public static class FileManager
    {
        public static string GetFile(string url)
        {
            WebRequest request = WebRequest.Create(url);
            //execute the request
            WebResponse response = request.GetResponse();
            //read data via response stream
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[60];

            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }

            } while (count > 0);

            return sb.ToString();
        }
    }
}
