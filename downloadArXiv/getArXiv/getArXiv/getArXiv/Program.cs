using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace getArXiv
{
    class Program
    {
        static void Main(string[] args)
        {

            string folder = "";

            int sd = 01;
            int sm = 01;
            int sy = 2007;

            int ed = 31;
            int em = 12;
            int ey = 2019;


            for (int year = sy; year <= ey; year++)
            {
                for (int month = sm; month <= em; month++)
                {
                    for (int day = sd; day <= ed; day++)
                    {
                        string yy = Convert.ToString(year);
                        string mm = Convert.ToString(month);
                        string dd = Convert.ToString(day);

                        if (month < 10)
                        {
                            mm = "0" + mm;
                        }

                        if (day < 10)
                        {
                            dd = "0" + dd;
                        }

                        if (!File.Exists(folder + "ArXiv - " + yy + " - " + mm + " - " + dd + ".xml"))
                        {

                            string url = @"http://export.arxiv.org/oai2?verb=ListRecords&from=" + yy + "-" + mm + "-" + dd + "&until=" + yy + "-" + mm + "-" + dd + "&set=cs&metadataPrefix=arXiv";

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            request.AutomaticDecompression = DecompressionMethods.GZip;

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string html = reader.ReadToEnd();

                                Console.WriteLine(html);

                                File.WriteAllText(folder + "ArXiv - " + yy + " - " + mm + " - " + dd + ".xml", html);
                            }

                            Thread.Sleep(1000 * 60 * 5);

                        }

                    }
                }
            }

        }
    }
}
