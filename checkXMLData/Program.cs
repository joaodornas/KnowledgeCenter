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

            //countFiles();

            getParents();

            //getUniqueFieldsFile("pubmed18n0928");

            //getUniqueFields();

            //getUniqueYears();

            //int start = Convert.ToInt16(args[1]);
            //int end = Convert.ToInt16(args[2]);
            //int group = Convert.ToInt16(args[0]);

            //splitXML(72, 72, 3);

        }

        public static void getUniqueFieldsFile(string filename)
        {
            string xmldata = File.ReadAllText(@"H:\PUBMED_tmp\" + filename + ".xml", Encoding.UTF8);

            List<string> fields = new List<string>();

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

                        System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\fields.txt", true);

                        file.WriteLine(node);

                        file.Close();
                    }

                }
            }
        }

        public static void countFiles()
        {
            string folder = @"H:\PUBMED_split\";

            string[] sub = Directory.GetDirectories(folder);

            for (int i = 0; i < sub.Length; i++)
            {
                string subfolder = sub[i];

                string[] files = Directory.GetFiles(subfolder);

                int count = files.Length;

                if (count < 30001)
                {
                    Console.WriteLine(subfolder);
                }

            }

            int x = 0;
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

        public static void splitXML(int start, int end, int group)
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

                Boolean exist = Directory.Exists(@"H:\" + Convert.ToString(group) + @"\pubmed" + iiString + "n" + iString);

                if (!exist)
                {
                    Console.WriteLine(@"H:\PUBMED_tmp\pubmed" + iiString + "n" + iString + ".xml");

                    string xmldata = File.ReadAllText(@"H:\PUBMED_tmp\pubmed" + iiString + "n" + iString + ".xml", Encoding.UTF8);

                    DirectoryInfo di = Directory.CreateDirectory(@"H:\" + Convert.ToString(group) + @"\pubmed" + iiString + "n" + iString);

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

                            //System.IO.StreamWriter file = new System.IO.StreamWriter(@"H:\PUBMED_split\pubmed" + iiString + "n" + iString + @"\pubmed" + iiString + "n" + iString + "-" + Convert.ToString(count) + ".xml", true, Encoding.UTF8);

                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"H:\" + Convert.ToString(group) + @"\pubmed" + iiString + "n" + iString + @"\pubmed" + iiString + "n" + iString + "-" + Convert.ToString(count) + ".xml", true, Encoding.UTF8);

                            file.WriteLine(article);

                            file.Close();

                        }

                    }

                    Console.WriteLine(PMIDs.Count);

                    //System.IO.StreamWriter filePMID = new System.IO.StreamWriter(@"H:\PUBMED_split\pubmed" + iiString + "n" + iString + @"\PMIDs.txt", true);

                    System.IO.StreamWriter filePMID = new System.IO.StreamWriter(@"H:\" + Convert.ToString(group) + @"\pubmed" + iiString + "n" + iString + @"\PMIDs.txt", true);

                    for (int j = 0; j < PMIDs.Count; j++)
                    {
                        filePMID.WriteLine(PMIDs[j]);

                    }

                    filePMID.Close();

                }
            }

        }

        public static void getParents()
        {
            int Total = 972;
            string iString = string.Empty;
            string iiString = string.Empty;

            List<string> tree = new List<string>();

            for (int i = 1; i <= Total; i++)
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

                string folder = @"E:\PUBMED_split\" + "pubmed" + iiString + "n" + iString + @"\";

                Console.WriteLine(folder);

                string[] files = Directory.GetFiles(folder);

                int nFiles = files.Length;
                //nFiles = 2;
                for (int j = 1; j < nFiles; j++)
                {
                    string filePath = files[j];

                    //Console.WriteLine(filePath);
                    
                    string xmldata = File.ReadAllText(filePath);

                    XDocument doc = XDocument.Load(new System.IO.StringReader(xmldata));

                    IEnumerable<XElement> allElements = doc.Descendants();

                    foreach (XElement element in allElements)
                    {
                        string path = element.Name.LocalName;

                        foreach (XElement parents in element.Ancestors())
                        {
                            path = parents.Name.LocalName + "\\" + path;

                        }

                        if (!tree.Contains(path))
                        {
                            tree.Add(path);

                            Console.WriteLine(path);

                            System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\unique_tree.txt", true);
                            file.WriteLine(path);
                            file.Close();
                        }
                    }

                }


            }
        }
    }

}

       