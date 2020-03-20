using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace pdfPlugin
{
    internal class plugin
    {
        private static void Main(String[] args)
        {
            string[] arg = new string[4];
            arg[0] = Convert.ToString(1000);

            //arg[1] = "file";
            arg[1] = "directory";

            //arg[2] = "C:\\Users\\Dornas\\Dropbox\\__ D - BE-HAPPY\\3. CEO\\1. ONTOLOGY\\HQ - Projects (IN)\\_KNOWLEDGE_CENTER\\_DOC\\pdf_test.pdf";
            //arg[2] = "C:\\Users\\Dornas\\Dropbox\\__ D - BE-HAPPY\\y. HARD-QUALE\\_PROJECT (IN)\\_KNOWLEDGE-CENTER\\_DOC\\_ADOBE\\pdf_example.pdf";
            arg[2] = "C:\\Users\\Dornas\\Dropbox\\__ XX - HARD-QUALE\\_KNOWLEDGE-CENTER\\_DOCUMENT_DATABASES\\RETINA";
            //arg[2] = "C:\\Users\\Dornas\\Dropbox\\__ XX - HARD-QUALE\\_KNOWLEDGE-CENTER\\_DOCUMENT_DATABASES\\RETINA\\27928387_pmc.pdf";
            
            arg[3] = "no_debug";
            //arg[3] = "debug";
            
            

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// GLOBAL VARIABLES, CONSTANTS AND TOOLS FUNCTIONS
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            tools Tools = new tools();

            Globals Globals = new Globals();
            Syntax Syntax = new Syntax();
            
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();

            string LF = white_space_characters.LINE_FEED.Hexadecimal;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// PARSE ARGUMENTS 
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Tools.parse(arg, ref Globals);

            List<string> filesNames = new List<string>();

            if (String.Compare(arg[1], "file") == 0)
            {
                filesNames.Add(Globals.PDF_File.fileNamePath); 
            }
            else if (String.Compare(arg[1], "directory") == 0)
            {
                filesNames.AddRange(Directory.GetFiles(Globals.PDF_File.directoryPath, "*.pdf"));
            }

            int iFile = 0;
            foreach (string fileNamePath in filesNames)
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// LOAD FILE
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                iFile = iFile + 1;

                Globals.PDF_File.fileNamePath = fileNamePath;

                Globals.PDF_Properties.byte_offset_xref = new List<int>();
                Globals.PDF_Properties.byte_offset_xref_kind = new List<int>();

                if (Globals.PDF_Plugin_Properties.debug) { Console.WriteLine(Convert.ToString(iFile)); };
                Console.WriteLine(Convert.ToString(iFile));

                if (iFile >= 28869)
                //if (iFile >= 0)
                {
                    Tools.loadFile(ref Globals);

                    string html = "!DOCTYPE html";

                    int idx = Globals.PDF_File.pdf_stream_hexa.IndexOf(Tools.lineStringToHex(html));

                    if (idx == 0)
                    {
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /// FILE HEADER
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        Syntax.file_structure.File_Header(ref Globals);

                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /// FILE TRAILER
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        Syntax.file_structure.File_Trailer(ref Globals);

                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /// CROSS REFERENCE TABLE
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        PDF_Cross_Reference_Table[] cross = new PDF_Cross_Reference_Table[Globals.PDF_Properties.nObjects];

                        Globals.PDF_Cross_Reference_Table = new List<PDF_Cross_Reference_Table>(Globals.PDF_Properties.nObjects);

                        Globals.PDF_Cross_Reference_Table.AddRange(cross);

                        Syntax.file_structure.Cross_Reference_Table_or_Stream(ref Globals);

                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /// DOCUMENT CATALOG
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        int iroot = 0;

                        for (int i = 0; i < Globals.PDF_Trailer.Count; i++)
                        {
                            if (Convert.ToInt16(Globals.PDF_Trailer[i].Root.value[0]) > 0)
                            {
                                iroot = Convert.ToInt16(Globals.PDF_Trailer[i].Root.value[0]);
                            }
                        }

                        int root_offset = Globals.PDF_Cross_Reference_Table[iroot].byte_offset * 3;

                        Syntax.document_structure.getDocumentCatalog(ref Globals, root_offset);


                    }
                    else if (idx != -1)
                    {
                        Tools.saveDebugInfo(ref Globals, "html", "HTML");
                    }

                    Globals.PDF_Properties.byte_offset_xref.Clear();
                    Globals.PDF_Properties.byte_offset_xref_kind.Clear();
                    Globals.PDF_Trailer.Clear();
                }
                /*
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// PAGE TREE
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Syntax.document_structure.getPageTree(ref Globals);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// PAGES
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                custom_value<object> kids_obj = (custom_value<object>)Globals.PDF_Page_Tree["/Kids"];

                List<string[]> kids = (List<string[]>)kids_obj.value;

                Globals.PDF_Properties.nPages = kids.Count;

                int nKids = kids.Count;

                for (int iPage = 0; iPage < nKids; iPage++)
                {
                    string[] obj = kids[iPage];

                    int iPageObj = Convert.ToInt16(obj[0]);

                    int offset = Globals.PDF_Cross_Reference_Table[iPageObj].byte_offset * 3;

                    Syntax.document_structure.getPages(ref Globals, offset);
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// PAGES - CONTENTS
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Syntax.content_streams_and_resources.getPagesContents(ref Globals);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// PAGES - RESOURCES - EXTGSTATE
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                for (int iPage = 0; iPage < Globals.PDF_Properties.nPages; iPage++)
                {
                    Syntax.content_streams_and_resources.getResourceExtGState(ref Globals, iPage);
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// PAGES - RESOURCES - FONT
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                for (int iPage = 0; iPage < Globals.PDF_Properties.nPages; iPage++)
                {
                    Syntax.content_streams_and_resources.getResourceFont(ref Globals, iPage);
                }

                int x = 0;
                */

            }
        }
        
    }
}
