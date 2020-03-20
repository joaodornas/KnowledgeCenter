using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace removeDamaged
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileDamagedPath = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_DOCUMENT_DATABASES\RETINA_damaged\damaged.txt";

            string[] filesDamaged = File.ReadAllLines(fileDamagedPath);

            string fileUnknownPath = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_DOCUMENT_DATABASES\RETINA_damaged\unKnown.txt";

            string[] filesUnknown = File.ReadAllLines(fileUnknownPath);

            List<string> allFiles = new List<string>();

            allFiles.AddRange(filesDamaged);

            allFiles.AddRange(filesUnknown);
            
            for (int iFile = 0; iFile < allFiles.Count; iFile++)
            {
                string destination = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_DOCUMENT_DATABASES\RETINA_damaged";

                int idx = allFiles[iFile].LastIndexOf("\\");

                destination = destination + @"\" + allFiles[iFile].Substring(idx + 1);

                File.Move(allFiles[iFile], destination);
            }


        }
    }
}
