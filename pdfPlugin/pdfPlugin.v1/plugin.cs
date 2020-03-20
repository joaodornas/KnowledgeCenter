using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace pdfPlugin
{
    internal class plugin
    {
        private static void Main(String[] args)
        {
            string[] arg = new string[3];
            arg[0] = Convert.ToString(1000);

            //arg[1] = "C:\\Users\\Dornas\\Dropbox\\__ D - BE-HAPPY\\3. CEO\\1. ONTOLOGY\\HQ - Projects (IN)\\_KNOWLEDGE_CENTER\\_DOC\\pdf_test.pdf";
            arg[1] = "C:\\Users\\Dornas\\Dropbox\\__ D - BE-HAPPY\\y. HARD-QUALE\\_PROJECT (IN)\\_KNOWLEDGE-CENTER\\_DOC\\_ADOBE\\pdf_example.pdf";

            //arg[2] = "no_debug";
            arg[2] = "debug";

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// GLOBAL VARIABLES, CONSTANTS AND TOOLS FUNCTIONS
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            Globals globals = new Globals();
            Syntax syntax = new Syntax();

            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();

            string LF = white_space_characters.LINE_FEED.Hexadecimal;
            
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// PARSE ARGUMENTS 
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            parse(arg, ref globals);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// LOAD FILE
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            loadFile(ref globals);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// FILE HEADER
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            syntax.file_structure.File_Header(ref globals);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// FILE TRAILER
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            syntax.file_structure.File_Trailer(ref globals);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// CROSS REFERENCE TABLE
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            globals.PDF_Cross_Reference_Table = new PDF_Cross_Reference_Table[globals.PDF_Properties.nObjects];

            syntax.file_structure.Cross_Reference_Table(ref globals);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// CROSS REFERENCE STREAM 
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*
            if (globals.PDF_Properties.pdf_version_f > globals.PDF_Versions.v15)
            {
                globals.PDF_Cross_Reference_Stream = new PDF_Cross_Reference_Stream[globals.PDF_Properties.nObjects];

                syntax.file_structure.Cross_Reference_Streams(ref globals);
            }
            */
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// DOCUMENT CATALOG
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            syntax.document_structure.Document_Catalog(ref globals);

            //readObjects(ref globals, LF);




        }

        private static void parse(String[] args, ref Globals globals)
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //DEFINE PROPRIEDADES DO ARQUIVO E DE USO DA MEMORIA
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            globals.PDF_Plugin_Properties.free_host_memory = (float)0.0;
            using (Process proc = Process.GetCurrentProcess())
            {

                var memory = proc.PrivateMemorySize64;
                globals.PDF_Plugin_Properties.free_host_memory = (float)memory;

            }

            globals.PDF_Plugin_Properties.total_number_of_simultaneous_files = Convert.ToInt32(args[0]);

            globals.PDF_Plugin_Properties.max_pdf_file_size = globals.PDF_Plugin_Properties.free_host_memory / globals.PDF_Plugin_Properties.total_number_of_simultaneous_files;

            globals.PDF_File.fileNamePath = args[1];

            globals.PDF_File.fileSize = new System.IO.FileInfo(globals.PDF_File.fileNamePath).Length;

            if (String.Compare(args[2],"no_debug") == 0)
            {
                globals.PDF_Plugin_Properties.debug = false;
            }
            else if (String.Compare(args[2], "debug") == 0)
            {
                globals.PDF_Plugin_Properties.debug = true;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
        }

        private static void loadFile(ref Globals globals)
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //CARREGA O ARQUIVO DO DISCO
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            tools Tools = new tools();

            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();

            byte[] pdf_stream = new byte[(int)Math.Ceiling(globals.PDF_Plugin_Properties.max_pdf_file_size)];

            globals.PDF_File.pdf_stream_hexa = new string('-', (int)Math.Ceiling(globals.PDF_Plugin_Properties.max_pdf_file_size));

            if (File.Exists(globals.PDF_File.fileNamePath))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(globals.PDF_File.fileNamePath, FileMode.Open)))
                {
                    pdf_stream = reader.ReadBytes((int)globals.PDF_File.fileSize);

                    globals.PDF_File.pdf_stream_hexa = BitConverter.ToString(pdf_stream, 0);

                }

                if (globals.PDF_Plugin_Properties.debug)
                { 
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //ESSA PARTE DO CODIGO EH APENAS PARA TESTES
                //DEVO USAR APENAS O STREAM HEXA
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    globals.PDF_File.pdf_stream_string = new string('-', (int)Math.Ceiling(globals.PDF_Plugin_Properties.max_pdf_file_size));

                    globals.PDF_File.pdf_stream_string = Tools.lineHexToString(globals.PDF_File.pdf_stream_hexa);

                    Syntax syntax = new Syntax();

                    string CR = white_space_characters.CARRIAGE_RETURN.Hexadecimal;
                    string LF = white_space_characters.LINE_FEED.Hexadecimal;

                    string CR_s = Tools.lineHexToString(CR);

                    string LF_s = Tools.lineHexToString(LF);

                    string[] CR_separator = new string[] { CR_s };

                    string[] LF_separator = new string[] { LF_s };

                    string[] _lines_string = globals.PDF_File.pdf_stream_string.Split(CR_separator, StringSplitOptions.None);

                    List<string> allLines = new List<string>();

                    for (int i = 0; i < _lines_string.Length; i++)
                    {
                        string[] new_lines_string = _lines_string[i].Split(LF_separator, StringSplitOptions.None);

                        for (int j = 0; j < new_lines_string.Length; j++)
                        {
                            if (String.IsNullOrEmpty(new_lines_string[j]) != true)
                            {
                                allLines.Add(new_lines_string[j]);
                            }
                        }
                    }

                    globals.PDF_File.pdf_lines_string = new List<string>();
                    globals.PDF_File.pdf_lines_hexa = new List<string>();

                    for (int i = 0; i < allLines.Count; i++)
                    {
                        string line_string = allLines[i];

                        string line_hexa = Tools.lineStringToHex(line_string);

                        globals.PDF_File.pdf_lines_hexa.Add(line_hexa);

                        globals.PDF_File.pdf_lines_string.Add(line_string);

                    }

                    globals.PDF_Properties.nLines = globals.PDF_File.pdf_lines_string.Count;

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                }

            }

        }

        private static void readObjects(ref Globals globals, string LF)
        {
            tools Tools = new tools();

            int nObjs = globals.PDF_Properties.nObjects;

            for (int i = 0; i < nObjs; i++)
            {
                if (globals.PDF_Cross_Reference_Table[i].in_use)
                {
                    int offset = globals.PDF_Cross_Reference_Table[i].byte_offset * 3;

                    string line_hexa = Tools.getLineFromByteOffset(ref globals.PDF_File.pdf_stream_hexa, offset, LF);
                    string line_string = Tools.lineHexToString(line_hexa);
                    string[] keys = line_string.Split(' ');
                    int obj_idx = Convert.ToInt32(keys[0]);

                    if (obj_idx == Convert.ToInt32(globals.PDF_Trailer.Root.value[0])) //ROOT CATALOG
                    {
                        while (true)
                        {
                            offset = offset + line_hexa.Length + 3;

                            line_hexa = Tools.getLineFromByteOffset(ref globals.PDF_File.pdf_stream_hexa, offset, LF);
                            line_string = Tools.lineHexToString(line_hexa);

                           
                        }
                    }
                    else
                    {
                        offset = offset + line_hexa.Length + 3;

                        line_hexa = Tools.getLineFromByteOffset(ref globals.PDF_File.pdf_stream_hexa, offset, LF);
                        line_string = Tools.lineHexToString(line_hexa);
                    }

                  
                }
            }

        }


    }
}
