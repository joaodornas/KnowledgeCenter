using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace checkXMLData
{
    class Program
    {
        static void Main(string[] args)
        {

            //getUniqueFields();

            //getUniqueYears();

            //splitXML();

            List<Task> tasksList = new List<Task>();

            Task split1 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(18, 72));
            tasksList.Add(split1);
            
            Task split2 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(72, 172));
            tasksList.Add(split2);

            Task split3 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(173, 272));
            tasksList.Add(split3);

            Task split4 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(273, 372));
            tasksList.Add(split4);

            //Task split5 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(373, 472));
            //tasksList.Add(split5);

            //Task split6 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(473, 572));
            //tasksList.Add(split6);

            //Task split7 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(573, 672));
            //tasksList.Add(split7);

            //Task split8 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(673, 772));
            //tasksList.Add(split8);

            //Task split9 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(773, 872));
            //tasksList.Add(split9);

            //Task split10 = System.Threading.Tasks.Task.Factory.StartNew(() => splitXML(873, 972));
            //tasksList.Add(split10);

            Task.WaitAll(tasksList.ToArray());

        }

        public static void getUniqueFields()
        {
            int Total = 972;
            string iString = string.Empty;
            string iiString = string.Empty;

            List<string> fields = new List<string>();

            for (int i = 828; i <= Total; i++)
            {

                if (i < 10)
                {
                    iString = "000" + Convert.ToString(i);
                }
                else if (i < 100)
                {
                    iString = "00" + Convert.ToString(i);
                }
                else if (i < 1000)
                {
                    iString = "0" + Convert.ToString(i);
                }

                if (i <= 928)
                {
                    iiString = "18";
                }
                else
                {
                    iiString = "19";
                }

                Console.WriteLine(@"E:\PUBMED_tmp\pubmed" + iiString + "n" + iString + ".xml");

                string xmldata = File.ReadAllText(@"E:\PUBMED_tmp\pubmed" + iiString + "n" + iString + ".xml", Encoding.UTF8);

                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.DtdProcessing = System.Xml.DtdProcessing.Parse;

                XmlReader rdr = XmlReader.Create(new System.IO.StringReader(xmldata), readerSettings);

                while (rdr.Read())
                {
                    if (rdr.NodeType == XmlNodeType.Element)
                    {
                        string node = rdr.LocalName;

                        if (!fields.Contains(node))
                        {
                            fields.Add(node);

                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\PUBMED_fields\fields.txt", true);

                            file.WriteLine(node);

                            file.Close();
                        }

                    }
                }


            }


        }

        public static void getUniqueYears()
        {
            int Total = 972;
            string iString = string.Empty;
            string iiString = string.Empty;

            List<string> fields = new List<string>();

            for (int i = 644; i <= Total; i++)
            {

                if (i < 10)
                {
                    iString = "000" + Convert.ToString(i);
                }
                else if (i < 100)
                {
                    iString = "00" + Convert.ToString(i);
                }
                else if (i < 1000)
                {
                    iString = "0" + Convert.ToString(i);
                }

                if (i <= 928)
                {
                    iiString = "18";
                }
                else
                {
                    iiString = "19";
                }

                Console.WriteLine(@"E:\PUBMED_tmp\pubmed" + iiString + "n" + iString + ".xml");

                string xmldata = File.ReadAllText(@"E:\PUBMED_tmp\pubmed" + iiString + "n" + iString + ".xml", Encoding.UTF8);

                XmlDocument doc = new XmlDocument();
                doc.Load(new System.IO.StringReader(xmldata));

                XmlNodeList elemList = doc.GetElementsByTagName("Year");

                for (int j = 0; j < elemList.Count; j++)
                {
                    string node = elemList[j].InnerXml;

                    if (!fields.Contains(node))
                    {
                        fields.Add(node);

                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\years.txt", true);

                        file.WriteLine(node);

                        file.Close();
                    }

                }

            }


        }

        public static void splitXML(int start, int end)
        {   

            int Total = 972;
            string iString = string.Empty;
            string iiString = string.Empty;

            List<string> fields = new List<string>();

            for (int i = start; i <= end; i++)
            {

                if (i < 10)
                {
                    iString = "000" + Convert.ToString(i);
                }
                else if (i < 100)
                {
                    iString = "00" + Convert.ToString(i);
                }
                else if (i < 1000)
                {
                    iString = "0" + Convert.ToString(i);
                }

                if (i <= 928)
                {
                    iiString = "18";
                }
                else
                {
                    iiString = "19";
                }

                Console.WriteLine(@"E:\PUBMED_tmp\pubmed" + iiString + "n" + iString + ".xml");

                string xmldata = File.ReadAllText(@"E:\PUBMED_tmp\pubmed" + iiString + "n" + iString + ".xml", Encoding.UTF8);

                DirectoryInfo di = Directory.CreateDirectory(@"E:\PUBMED_split\pubmed" + iiString + "n" + iString);

                int idx_pubmedArticle = 0;
                int idx_endPubmed = 0;

                string start_field = "<PubmedArticle>";
                string end_field = "</PubmedArticle>";

                string begin_PMID_field = "<PMID";
                string end_PMID_field = "</PMID>";
                string delimiter = ">";
                

                int offset = 0;
                int count = 0;

                List<string> PMIDs = new List<string>();

                while (idx_pubmedArticle != -1)
                {
                    idx_pubmedArticle = xmldata.IndexOf(start_field, offset);
                    idx_endPubmed = xmldata.IndexOf(end_field, offset);

                    offset = idx_endPubmed + 1;

                    if ((idx_pubmedArticle != -1) & (idx_endPubmed != -1))
                    {
                        string article = xmldata.Substring(idx_pubmedArticle, idx_endPubmed - idx_pubmedArticle + end_field.Length);

                        int idx_i = article.IndexOf(begin_PMID_field, 0);
                        int idx_j = article.IndexOf(delimiter, idx_i);
                        int idx_z = article.IndexOf(end_PMID_field, idx_i);

                        string PMID = article.Substring(idx_j + 1, idx_z - idx_j - 1);

                        PMIDs.Add(PMID);

                        count = count + 1;

                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\PUBMED_split\pubmed" + iiString + "n" + iString + @"\pubmed" + iiString + "n" + iString + "-" + Convert.ToString(count) + ".xml", true, Encoding.UTF8);

                        file.WriteLine(article);

                        file.Close();

                    }
                    
                }

                Console.WriteLine(PMIDs.Count);

                System.IO.StreamWriter filePMID = new System.IO.StreamWriter(@"E:\PUBMED_split\pubmed" + iiString + "n" + iString + @"\PMIDs.txt", true);

                for (int j = 0; j < PMIDs.Count; j++)
                {
                    filePMID.WriteLine(PMIDs[j]);
                    
                }

                filePMID.Close();
            }

        }

    }

}

       