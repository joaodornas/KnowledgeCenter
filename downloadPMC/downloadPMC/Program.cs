using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;

namespace downloadPMC
{
    class Program
    {
        static void Main(string[] args)
        {

            string csvfile = @"oa_file_list.csv";
            string pdfspmcfile = @"pdfs_pmc.txt";

            List<string> pdfspmc = File.ReadLines(pdfspmcfile).ToList<string>();

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();

            using (var reader = new StreamReader(csvfile))
            {
               int i = 1;

               while (!reader.EndOfStream)
                {
                    if (i > 1)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        listA.Add(values[0]);
                        listB.Add(values[4]);
                    }
                    else
                    {
                        string line = reader.ReadLine();

                        i = i + 1;
                    }
                }
            }

            for (int i = 0; i < pdfspmc.Count; i++)
            {

                string PMID = pdfspmc[i];
                int idx_under = PMID.IndexOf("_");
                PMID = PMID.Substring(0, idx_under);

                if (!File.Exists(PMID + "_pmc.tar.gz"))
                {
                    int idx_PMID = listB.IndexOf(PMID);

                    if (idx_PMID != -1)
                    {
                        string filePath = listA[idx_PMID];

                        // Get the object used to communicate with the server.
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.ncbi.nlm.nih.gov/pub/pmc/" + filePath);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        // This example assumes the FTP site uses anonymous logon.
                        request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                        Stream responseStream = response.GetResponseStream();

                        Console.WriteLine($"Download Complete, status {response.StatusDescription}");

                        var fileStream = File.Create(PMID + "_pmc.tar.gz");
                        responseStream.CopyTo(fileStream);

                        fileStream.Close();

                        response.Close();

                        Thread.Sleep(1000 * 60 * 5);
                    }
                }

            }



        }
    }
}
