using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace splitPubmed
{
    class Program
    {
        static void Main(string[] args)
        {

            string folder = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_REFERENCES_DATABASES\_RETINA";
            string folder_split = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_REFERENCES_DATABASES\_RETINA_split";

            string[] files = Directory.GetFiles(folder, "*.xml");

            int id_article = 0;

            foreach (string file in files)
            {
                string data = File.ReadAllText(file);

                int idx_Pubmed = data.IndexOf("<PubmedArticle>");

                while(idx_Pubmed != -1)
                {
                    id_article = id_article + 1;

                    Console.WriteLine(Convert.ToString(id_article));

                    int idx_Pubmed_end = data.IndexOf("</PubmedArticle>", idx_Pubmed);

                    string article = data.Substring(idx_Pubmed, idx_Pubmed_end + "</PubmedArticle>".Length - idx_Pubmed);

                    File.WriteAllText(folder_split + "\\" + Convert.ToString(id_article) + ".xml", article);

                    idx_Pubmed = data.IndexOf("<PubmedArticle>",idx_Pubmed + 1);
                }

            }

        }
    }
}
