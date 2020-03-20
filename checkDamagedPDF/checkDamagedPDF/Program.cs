using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace checkDamagedPDF
{
    class Program
    {
        static void Main(string[] args)
        {

            string html = "!DOCTYPE html";

            string catalog = "/Catalog";

            string folder = "C:\\Users\\Dornas\\Dropbox\\__ XX - HARD-QUALE\\_KNOWLEDGE-CENTER\\_DOCUMENT_DATABASES\\RETINA";

            List<string> filesNames = new List<string>();

            filesNames.AddRange(Directory.GetFiles(folder, "*.pdf"));

            List<string> damaged = new List<string>();
            List<string> notDamaged = new List<string>();
            List<string> unKnown = new List<string>();

            for (int iFile = 20000; iFile < filesNames.Count; iFile++ )
            {

                string fileNamePath = filesNames[iFile];

                using (BinaryReader reader = new BinaryReader(File.Open(fileNamePath, FileMode.Open)))
                {
                    int fileSize = (int)new System.IO.FileInfo(fileNamePath).Length;

                    byte[] pdf_stream = reader.ReadBytes(fileSize);

                    string pdf_stream_hexa = BitConverter.ToString(pdf_stream, 0);

                    //string pdf_stream_string = lineHexToString(pdf_stream_hexa);

                    int idx_html = pdf_stream_hexa.IndexOf(lineStringToHex(html));

                    int idx_catalog = pdf_stream_hexa.IndexOf(lineStringToHex(catalog));

                    if ( (idx_html != -1) && (idx_catalog == -1) )
                    {
                        damaged.Add(fileNamePath);
                    }
                    else if ((idx_html == -1) && (idx_catalog != -1))
                    {
                        notDamaged.Add(fileNamePath);
                    }
                    else
                    {
                        unKnown.Add(fileNamePath);
                    }
                    
                }

            }


            File.AppendAllLines(folder + "\\damaged.txt", damaged.ToArray());
            File.AppendAllLines(folder + "\\notDamaged.txt", notDamaged.ToArray());
            File.AppendAllLines(folder + "\\unKnown.txt", unKnown.ToArray());

            int x = 0;

        }

        public static string lineHexToString(string lineHex)
        {

            lineHex = lineHex.Trim('-');
            string[] hex_ = lineHex.Split('-');
            string hex = string.Join(string.Empty, hex_);

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                string currentHex = hex.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(currentHex, 16);
            }

            string lineString = System.Text.Encoding.ASCII.GetString(bytes);

            return lineString;

        }

        public static string lineStringToHex(string lineString)
        {
            byte[] word = Encoding.Default.GetBytes(lineString);

            var hexString = BitConverter.ToString(word);

            return hexString;
        }

    }
}
