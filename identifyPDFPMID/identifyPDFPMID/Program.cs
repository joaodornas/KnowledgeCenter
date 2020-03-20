using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace identifyPDFPMID
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileName = @"C:\Users\Dornas\Downloads\My Collection.bib";

            string info = File.ReadAllText(fileName);

            int idx_doi = info.IndexOf("doi");

            List<string> results = new List<string>();

            while (idx_doi != -1)
            {
                int idx_doi_left = info.IndexOf("{", idx_doi);
                int idx_doi_right = info.IndexOf("}", idx_doi);

                string DOI = info.Substring(idx_doi_left + 1, idx_doi_right - idx_doi_left - 1);

                int idx_file = info.IndexOf("file",idx_doi);

                int idx_pdf = info.IndexOf(":pdf", idx_file);

                string filePDF = info.Substring(idx_file, idx_pdf - idx_file);

                int idx_slash = filePDF.LastIndexOf("/");

                filePDF = filePDF.Substring(idx_slash + 1);

                string URL = "https://www.ncbi.nlm.nih.gov/pubmed/?term=" + DOI;

                var request = (HttpWebRequest)WebRequest.Create(URL);

                var response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                int idx_PMID = responseString.IndexOf("PMID:");

                if (idx_PMID != -1)
                {

                    int idx_span = responseString.IndexOf("</span>", idx_PMID);

                    string PMID = responseString.Substring(idx_PMID + "PMID:".Length, idx_span - idx_PMID - "PMID:".Length);

                    results.Add(filePDF + "," + DOI + "," + PMID);

                    idx_doi = info.IndexOf("doi", idx_doi + 1);

                }

            }

            File.WriteAllLines(@"C:\Users\Dornas\Downloads\PMIDs.txt", results.ToArray());


        }
    }
}
