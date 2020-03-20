using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace findDuplicated
{
    class Program
    {
        static void Main(string[] args)
        {

            string folder = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_DOCUMENT_DATABASES\RETINA";
            string destination = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_DOCUMENT_DATABASES\duplicated\";

            string[] files = Directory.GetFiles(folder, "*.pdf");

            List<string> allFiles = new List<string>();

            allFiles.AddRange(files);

            List<string> uniqueFiles = new List<string>();

            List<string> duplicated = new List<string>();

            for (int i = 0; i < allFiles.Count; i++)
            {

                Console.WriteLine(Convert.ToString(i));

                int idx = allFiles[i].LastIndexOf("_");
                int idx_slash = allFiles[i].LastIndexOf(@"\");

                if (idx == -1)
                {
                    uniqueFiles.Add(allFiles[i]);
                }
                else
                {
                    string file = allFiles[i].Substring(idx_slash + 1, idx - idx_slash - 1);

                    if (uniqueFiles.Contains(file))
                    {
                        duplicated.Add(file);

                        File.Move(allFiles[i], destination + file + "-" + Convert.ToString(i) + ".pdf");

                    }
                    else
                    {
                        uniqueFiles.Add(file);
                    }
                }
            }

            //File.WriteAllLines(folder + @"\duplicated.txt", duplicated.ToArray());

            //File.WriteAllLines(folder + @"\uniqueFiles.txt", uniqueFiles.ToArray());
            
        }
    }
}
