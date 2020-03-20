using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using ImageProcessor;
using System.Drawing;

namespace pdfPlugin
{
    public class tools
    {
      
        // PARSE ARRGUMENTS
        public void parse(String[] args, ref Globals globals)
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

            if (String.Compare(args[1], "file") == 0)
            {
                globals.PDF_File.fileNamePath = args[2];
            }
            else if (String.Compare(args[1], "directory") == 0)
            {
                globals.PDF_File.directoryPath = args[2];
            }
            
            if (String.Compare(args[3], "no_debug") == 0)
            {
                globals.PDF_Plugin_Properties.debug = false;
            }
            else if (String.Compare(args[3], "debug") == 0)
            {
                globals.PDF_Plugin_Properties.debug = true;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }

        //LOAD PDF FILE
        public void loadFile(ref Globals globals)
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //CARREGA O ARQUIVO DO DISCO
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            tools Tools = new tools();

            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();

            byte[] pdf_stream = new byte[(int)Math.Ceiling(globals.PDF_Plugin_Properties.max_pdf_file_size)];

            globals.PDF_File.pdf_stream_hexa = new string('-', (int)Math.Ceiling(globals.PDF_Plugin_Properties.max_pdf_file_size));

            bool teste = File.Exists(globals.PDF_File.fileNamePath);

            if (File.Exists(globals.PDF_File.fileNamePath))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(globals.PDF_File.fileNamePath, FileMode.Open)))
                {
                    globals.PDF_File.fileSize = new System.IO.FileInfo(globals.PDF_File.fileNamePath).Length;

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

        //RETRIEVE DICTIONARY INFORMATION
        public static (List<string> keys,List<Boolean> required,List<float> version, List<int> kind) getDictionaryBluePrintInformation(string dicName)
        {
            PDF_Versions pdf_versions = new PDF_Versions();

            string[] allDicNames = new string[] { "Document_Catalog", "Page_Tree", "Page_Object", "Resources", "ExtGState", "FontType0", "FontType1", "MMFontType1", "FontType3", "FontTrueType", "CIDFontType0", "CIDFontType2" };

            int index = Array.IndexOf(allDicNames, dicName);

            List<string> keys = new List<string>();
            List<Boolean> required = new List<Boolean>();
            List<float> version = new List<float>();
            List<int> kind = new List<int>();

            switch (index)
            {
                case 0:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    //keys.Add("/Version");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.Name);

                    //keys.Add("/Extensions");
                    //required.Add(false);
                    //version.Add(pdf_versions.iso32000);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Pages");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/PageLabels");
                    //required.Add(false);
                    //version.Add(pdf_versions.v13);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Names");
                    //required.Add(false);
                    //version.Add(pdf_versions.v12);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Dests");
                    //required.Add(false);
                    //version.Add(pdf_versions.v11);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/ViewerPreferences");
                    //required.Add(false);
                    //version.Add(pdf_versions.v12);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/PageLayout");
                    //required.Add(false);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Name);

                    //keys.Add("/PageMode");
                    //required.Add(false);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Name);

                    //keys.Add("/Outlines");
                    //required.Add(false);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Threads");
                    //required.Add(false);
                    //version.Add(pdf_versions.v11);
                    //kind.Add(Kinds_of_Objects.Array);

                    //keys.Add("/OpenAction");
                    //required.Add(false);
                    //version.Add(pdf_versions.v11);
                    //kind.Add(Kinds_of_Objects.Undefined);

                    //keys.Add("/AA");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/URI");
                    //required.Add(false);
                    //version.Add(pdf_versions.v11);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/AcroForm");
                    //required.Add(false);
                    //version.Add(pdf_versions.v12);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Metadata");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.Stream);

                    //keys.Add("/StructTreeRoot");
                    //required.Add(false);
                    //version.Add(pdf_versions.v13);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/MarkInfo");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Lang");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.String);

                    //keys.Add("/SpiderInfo");
                    //required.Add(false);
                    //version.Add(pdf_versions.v13);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/OutputIntents");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.Array);

                    //keys.Add("/PieceInfo");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/OCProperties");
                    //required.Add(false);
                    //version.Add(pdf_versions.v15);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Perms");
                    //required.Add(false);
                    //version.Add(pdf_versions.v15);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Legal");
                    //required.Add(false);
                    //version.Add(pdf_versions.v15);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Requirements");
                    //required.Add(false);
                    //version.Add(pdf_versions.v17);
                    //kind.Add(Kinds_of_Objects.Array);

                    //keys.Add("/Collection");
                    //required.Add(false);
                    //version.Add(pdf_versions.v17);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/NeedsRendering");
                    //required.Add(false);
                    //version.Add(pdf_versions.v17);
                    //kind.Add(Kinds_of_Objects.Boolean);

                    break;

                case 1:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Parents");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Kids");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/Count");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);


                    break;

                case 2:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Parent");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Indirect);

                    keys.Add("/LastModified");
                    required.Add(true);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Date);

                    keys.Add("/Resources");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/MediaBox");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Rectangle);

                    //keys.Add("/CropBox");
                    //required.Add(false);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Rectangle);

                    //keys.Add("/BleedBox");
                    //required.Add(false);
                    //version.Add(pdf_versions.v13);
                    //kind.Add(Kinds_of_Objects.Rectangle);

                    //keys.Add("/TrimBox");
                    //required.Add(false);
                    //version.Add(pdf_versions.v13);
                    //kind.Add(Kinds_of_Objects.Rectangle);

                    //keys.Add("/ArtBox");
                    //required.Add(false);
                    //version.Add(pdf_versions.v13);
                    //kind.Add(Kinds_of_Objects.Rectangle);
                    
                    //keys.Add("/BoxColorInfo");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Contents");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined); //stream or array

                    //keys.Add("/Rotate");
                    //required.Add(false);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    //keys.Add("/Group");
                    //required.Add(false);
                    //version.Add(pdf_versions.v14);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/B");
                    required.Add(false);
                    version.Add(pdf_versions.v11);
                    kind.Add(Kinds_of_Objects.Array);

                    //keys.Add("/Dur");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    //keys.Add("/Trans");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    //keys.Add("/Annots");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    //keys.Add("/AA");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/Metadata");
                    required.Add(false);
                    version.Add(pdf_versions.v14);
                    kind.Add(Kinds_of_Objects.Stream);

                    //keys.Add("/PieceInfo");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/StructParents");
                    required.Add(true);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Numeric);

                    //keys.Add("/ID");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    //keys.Add("/PZ");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    //keys.Add("/SeparationInfo");
                    //required.Add(false);
                    //version.Add(pdf_versions.v13);
                    //kind.Add(Kinds_of_Objects.Dictionary);

                    //keys.Add("/Tabs");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/TemplateInstantiated");
                    required.Add(true);
                    version.Add(pdf_versions.v15);
                    kind.Add(Kinds_of_Objects.Name);

                    //keys.Add("/PresSteps");
                    //required.Add(true);
                    //version.Add(pdf_versions.v10);
                    //kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/UserUnit");
                    required.Add(false);
                    version.Add(pdf_versions.v16);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/VP");
                    required.Add(false);
                    version.Add(pdf_versions.v16);
                    kind.Add(Kinds_of_Objects.Dictionary);


                    break;

                case 3:

                    keys.Add("/ExtGState");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/ColorSpace");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Pattern");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Shading");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/XObject");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Font");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/ProcSet");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    break;

                case 4:

                    keys.Add("/Type");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Name");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/LW");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/LC");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/LJ");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/ML");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/D");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/RI");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/OP");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Boolean);

                    keys.Add("/op");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Boolean);

                    keys.Add("/OPM");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/Font");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/BG");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Functions);

                    keys.Add("/BG2");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/UCR");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Functions);

                    keys.Add("/UCR2");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/TR");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/TR2");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/HT");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/FL");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/SM");
                    required.Add(false);
                    version.Add(pdf_versions.v13);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/SA");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Boolean);

                    keys.Add("/BM");
                    required.Add(false);
                    version.Add(pdf_versions.v14);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/SMask");
                    required.Add(false);
                    version.Add(pdf_versions.v14);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/CA");
                    required.Add(false);
                    version.Add(pdf_versions.v14);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/ca");
                    required.Add(false);
                    version.Add(pdf_versions.v14);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/AIS");
                    required.Add(false);
                    version.Add(pdf_versions.v14);
                    kind.Add(Kinds_of_Objects.Boolean);

                    keys.Add("/TK");
                    required.Add(false);
                    version.Add(pdf_versions.v14);
                    kind.Add(Kinds_of_Objects.Boolean);
                    
                    break;

                case 5:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Subtype");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/BaseFont");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/CIDSystemInfo");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/FontDescriptor");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/DW");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/W");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/DW2");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/W2");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/CIDToGIDMap");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);

                    break;

                case 6:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Subtype");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Name");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/BaseFont");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/FirstChar");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/LastChar");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/Widths");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/FontDescriptor");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Encoding");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/ToUnicode");
                    required.Add(false);
                    version.Add(pdf_versions.v12);
                    kind.Add(Kinds_of_Objects.Stream);

                    break;

                case 7:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Subtype");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Name");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/BaseFont");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/FirstChar");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/LastChar");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/Widths");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/FontDescriptor");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Encoding");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/ToUnicode");
                    required.Add(false);
                    version.Add(pdf_versions.v12);
                    kind.Add(Kinds_of_Objects.Stream);

                    break;

                case 8:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Subtype");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Name");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/FontBBox");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Rectangle);

                    keys.Add("/FontMatrix");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/CharProcs");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Encoding");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/FirstChar");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/LastChar");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/Widths");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/FontDescriptor");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Resource");
                    required.Add(false);
                    version.Add(pdf_versions.v12);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/ToUnicode");
                    required.Add(false);
                    version.Add(pdf_versions.v12);
                    kind.Add(Kinds_of_Objects.Stream);

                    break;

                case 9:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Subtype");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Name");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/BaseFont");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/FirstChar");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/LastChar");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/Widths");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/FontDescriptor");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/Encoding");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);

                    keys.Add("/ToUnicode");
                    required.Add(false);
                    version.Add(pdf_versions.v12);
                    kind.Add(Kinds_of_Objects.Stream);

                    break;

                case 10:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Subtype");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/BaseFont");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/CIDSystemInfo");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/FontDescriptor");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/DW");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/W");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/DW2");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/W2");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/CIDToGIDMap");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);

                    break;

                case 11:

                    keys.Add("/Type");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/Subtype");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/BaseFont");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Name);

                    keys.Add("/CIDSystemInfo");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/FontDescriptor");
                    required.Add(true);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Dictionary);

                    keys.Add("/DW");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Numeric);

                    keys.Add("/W");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/DW2");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/W2");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Array);

                    keys.Add("/CIDToGIDMap");
                    required.Add(false);
                    version.Add(pdf_versions.v10);
                    kind.Add(Kinds_of_Objects.Undefined);
                    kind.Add(Kinds_of_Objects.Name);

                    break;

            }

            return (keys, required, version, kind);

        }

        //RETRIEVE DICTIONARY BLUPRINT
        public Dictionary<string,object> getDictionaryBluePrint(string dicName)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            var info = getDictionaryBluePrintInformation(dicName);

            int nValues = info.keys.Count;

            for (int i = 0; i < nValues; i++)
            {
                insertNewObjectInDictionary<object>(ref dic, info.keys[i], false, info.required[i], info.version[i], info.kind[i], -1);
            }
            
            return dic;
        }

        //INSERT NEW OBJECT IN DICTIONARY
        public void insertNewObjectInDictionary<T>(ref Dictionary<string, object> dic, string key, Boolean found, Boolean required, float version, int kind_of_object, int kind_of_object_found)
        {
            custom_value<T> custom = new custom_value<T>();
            
            custom.value = default(T);
            custom.key = key;
            custom.found = found;
            custom.required = required;
            custom.pdf_version_f = version;
            custom.kind_of_object_expected = kind_of_object;
            custom.kind_of_object_found = kind_of_object_found;
            
            dic.Add(key, custom);
        }

        public int getNextDelimiter(ref string stream_hexa, int offset, string which)
        {

            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            string teste = lineHexToString(stream_hexa.Substring(offset, stream_hexa.Length - offset - 1));

            string[] kind = new string[] {"both", "delimiter", "white_space" };

            int which_idx = Array.IndexOf(kind,which);

            int min = -1;

            switch (which_idx)
            {
                case 0:

                    int[] idxb = new int[17];

                    idxb[0] = stream_hexa.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
                    idxb[1] = stream_hexa.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
                    idxb[2] = stream_hexa.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
                    idxb[3] = stream_hexa.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
                    idxb[4] = stream_hexa.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
                    idxb[5] = stream_hexa.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
                    idxb[6] = stream_hexa.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
                    idxb[7] = stream_hexa.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
                    idxb[8] = stream_hexa.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
                    idxb[9] = stream_hexa.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset + 3);
                    idxb[10] = stream_hexa.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
                    idxb[11] = stream_hexa.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
                    idxb[12] = stream_hexa.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
                    idxb[13] = stream_hexa.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
                    idxb[14] = stream_hexa.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
                    idxb[15] = stream_hexa.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
                    idxb[16] = stream_hexa.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

                    idxb = idxb.Where(val => val != -1).ToArray();

                    min = idxb.Min();

                    break;

                case 1:

                    int[] idxd = new int[10];

                    idxd[0] = stream_hexa.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
                    idxd[1] = stream_hexa.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
                    idxd[2] = stream_hexa.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
                    idxd[3] = stream_hexa.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
                    idxd[4] = stream_hexa.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
                    idxd[5] = stream_hexa.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
                    idxd[6] = stream_hexa.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
                    idxd[7] = stream_hexa.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
                    idxd[8] = stream_hexa.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
                    idxd[9] = stream_hexa.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset + 3);
                    
                    idxd = idxd.Where(val => val != -1).ToArray();

                    min = idxd.Min();

                    break;

                case 2:

                    int[] idxw = new int[7];

                    idxw[0] = stream_hexa.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
                    idxw[1] = stream_hexa.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
                    idxw[2] = stream_hexa.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
                    idxw[3] = stream_hexa.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
                    idxw[4] = stream_hexa.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
                    idxw[5] = stream_hexa.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
                    idxw[6] = stream_hexa.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

                    idxw = idxw.Where(val => val != -1).ToArray();

                    min = idxw.Min();

                    break;
            }

            return min;

        }

        public string getObject(ref Globals globals, int offset)
        {
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            string nbeginObj = lineStringToHex("\nobj");
            string nendObj = lineStringToHex("\nendobj");

            string rbeginObj = lineStringToHex("\robj");
            string rendObj = lineStringToHex("\rendobj");

            string sbeginObj = lineStringToHex(" obj");
            string sendObj = lineStringToHex(" endobj");

            int[] idx_beginObj = new int[3];
            int[] idx_endObj = new int[3];

            idx_beginObj[0] = globals.PDF_File.pdf_stream_hexa.IndexOf(nbeginObj, offset);
            idx_endObj[0] = globals.PDF_File.pdf_stream_hexa.IndexOf(nendObj, offset);

            idx_beginObj[1] = globals.PDF_File.pdf_stream_hexa.IndexOf(rbeginObj, offset);
            idx_endObj[1] = globals.PDF_File.pdf_stream_hexa.IndexOf(rendObj, offset);

            idx_beginObj[2] = globals.PDF_File.pdf_stream_hexa.IndexOf(sbeginObj, offset);
            idx_endObj[2] = globals.PDF_File.pdf_stream_hexa.IndexOf(sendObj, offset);

            int[] idx_begin = idx_beginObj.Where(val => val != -1).ToArray();
            int[] idx_end = idx_endObj.Where(val => val != -1).ToArray();
            
            string obj_stream_hexa = globals.PDF_File.pdf_stream_hexa.Substring(idx_begin.Min(), idx_end[0] - idx_begin.Min() + nendObj.Length);

            return obj_stream_hexa;
        }

        //CONVERT HEXADECIMAL TO STRING
        public string lineHexToString(string lineHex)
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

        public byte[] lineHexToByte(string lineHex)
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

            return bytes;
        }

        //CONVERT STRING TO HEXADECIMAL
        public string lineStringToHex(string lineString)
        {
            byte[] word = Encoding.Default.GetBytes(lineString);

            var hexString = BitConverter.ToString(word);

            return hexString;
        }

        //IDENTIFY KIND OF OBJECT
        public int identifyObject(ref Globals globals, ref string stream_hexa, ref int offset, string key)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            //string teste = lineHexToString(stream_hexa.Substring(offset, 35)); //remove

            offset = offset + lineStringToHex(key).Length + 1;

            while (String.Compare(stream_hexa.Substring(offset,2), lineStringToHex(" ")) == 0)
            {
                offset = offset + 3;
            }
            
            string LP = delimiter_characters.LEFT_PARENTHESIS.Hexadecimal;
            string LT = delimiter_characters.LESS_THAN_SIGN.Hexadecimal;
            string SL = delimiter_characters.SOLIDUS.Hexadecimal;
            string LS = delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal;
            
            string keyword_stream = lineStringToHex("stream");
            string keyword_null = lineStringToHex("null");
            string keyword_obj = lineStringToHex("obj");
            string keyword_true = lineStringToHex("true");
            string keyword_false = lineStringToHex("false");

            int kindOfObject = -1;

            string teste = lineHexToString(stream_hexa.Substring(offset, stream_hexa.Length - offset));

            string firstKey = stream_hexa.Substring(offset, 2);
            string secondKey = stream_hexa.Substring(offset + 3, 2);
            string thirdKey = stream_hexa.Substring(offset + 6, 2);
            string fourthKey = stream_hexa.Substring(offset + 9, 2);

            int idx_next = getNextDelimiter(ref stream_hexa, offset + 3, "delimiter");
            string keys_hexa = stream_hexa.Substring(offset, idx_next - offset);
            string keys_string = lineHexToString(keys_hexa);
            string[] keys = keys_string.Split(white_space_characters.SPACE.Glyph.ToCharArray());

            string R = keys[keys.Length-1];

            R = R.Trim(white_space_characters.LINE_FEED.Glyph.ToCharArray());

            Boolean FirstisNumeric = float.TryParse(lineHexToString(firstKey), out float nfloat);
            
            if ((FirstisNumeric == false) && ((String.Compare(firstKey, keyword_true) == 0) || (String.Compare(firstKey, keyword_false) == 0)))
            {
                kindOfObject = Kinds_of_Objects.Boolean; //7.3.2 Boolean Objects
            }
            else if ( (FirstisNumeric) && (String.Compare(R, "R") != 0))
            {
                kindOfObject = Kinds_of_Objects.Numeric; //7.3.3 Numeric Objects
            }
            else if ((FirstisNumeric == false) && ((String.Compare(firstKey, LP) == 0)) || ((FirstisNumeric == false) && ((String.Compare(firstKey, LT) == 0)) && ((String.Compare(secondKey, LT) != 0))))
            {
                kindOfObject = Kinds_of_Objects.String; //7.3.4 String Objects
            }
            else if ((FirstisNumeric == false) && ((String.Compare(firstKey, SL) == 0)))
            {
                kindOfObject = Kinds_of_Objects.Name; //7.3.5 Name Objects
            }
            else if ((FirstisNumeric == false) && ((String.Compare(firstKey, LS) == 0)))
            {
                kindOfObject = Kinds_of_Objects.Array; //7.3.6 Array Objects
            }
            else if ((FirstisNumeric == false) && (String.Compare(firstKey, LT) == 0) && ((String.Compare(secondKey, LT) == 0)))
            {
                kindOfObject = Kinds_of_Objects.Dictionary; //7.3.7 Dictionary Objects
            }
            else if ((FirstisNumeric == false) && (String.Compare(firstKey, keyword_stream) == 0))
            {
                kindOfObject = Kinds_of_Objects.Stream; //7.3.8 Stream Objects
            }
            else if ((FirstisNumeric == false) && (String.Compare(firstKey, keyword_null) == 0))
            {
                kindOfObject = Kinds_of_Objects.Null; //7.3.9 Null Object
            }
            else if ((FirstisNumeric == true) && (String.Compare(R, "R") == 0) )
            {
                kindOfObject = Kinds_of_Objects.Indirect; //7.3.10 Indirect Objects
            }

            if (kindOfObject == Kinds_of_Objects.Dictionary)
            {
                int idx_lts = stream_hexa.IndexOf(LT);
                int idx_l_feed = stream_hexa.IndexOf(white_space_characters.SPACE.Hexadecimal, idx_lts + 3);
                string tmp = stream_hexa.Substring(idx_lts + 3, idx_l_feed - idx_lts - 3);

                if (String.Compare(tmp, lineStringToHex("/FunctionType")) == 0)
                {
                    kindOfObject = Kinds_of_Objects.Functions;
                }
            }

            return kindOfObject;
        }

        public string getSubDictionary(ref string stream_hexa, int offset)
        {
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();

            string beginDic = delimiter_characters.LESS_THAN_SIGN.Hexadecimal + '-' + delimiter_characters.LESS_THAN_SIGN.Hexadecimal;
            string endDic = delimiter_characters.GREATER_THAN_SIGN.Hexadecimal + '-' + delimiter_characters.GREATER_THAN_SIGN.Hexadecimal;

            int idx_beginObj = stream_hexa.IndexOf(beginDic, offset);
            int idx_endObj = stream_hexa.IndexOf(endDic, offset);

            int idx_offset = idx_beginObj;
            idx_offset = stream_hexa.IndexOf(beginDic, idx_beginObj + 1, idx_endObj - idx_beginObj - 1);
            while ( idx_offset != -1)
            {
                idx_endObj = stream_hexa.IndexOf(endDic, idx_endObj + 1);

                idx_offset = stream_hexa.IndexOf(beginDic, idx_offset + 1, idx_endObj - idx_offset);
            }

            string obj_stream_hexa = stream_hexa.Substring(idx_beginObj, idx_endObj - idx_beginObj + endDic.Length);

            return obj_stream_hexa;
        }

        public string getSubArray(ref string stream_hexa, int offset)
        {
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();

            string beginArray = delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal;
            string endArray = delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal;

            int idx_beginObj = stream_hexa.IndexOf(beginArray, offset);
            int idx_endObj = stream_hexa.IndexOf(endArray, offset);

            string obj_stream_hexa = stream_hexa.Substring(idx_beginObj, idx_endObj - idx_beginObj);

            obj_stream_hexa = obj_stream_hexa.Trim(beginArray.ToCharArray());
            obj_stream_hexa = obj_stream_hexa.Trim(white_space_characters.LINE_FEED.Hexadecimal.ToCharArray());

            return obj_stream_hexa;
        }

        public string getSubStream(ref string stream, int offset)
        {
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            string beginStream = lineStringToHex("stream");
            string endStream = lineStringToHex("endstream");

            int idx_beginObj = stream.IndexOf(beginStream,offset);
            int idx_endObj = stream.IndexOf(endStream,offset);

            string obj_stream_hexa = stream.Substring(idx_beginObj + beginStream.Length + 3, idx_endObj - idx_beginObj - 3 - beginStream.Length);

            obj_stream_hexa = obj_stream_hexa.Trim('-');
            obj_stream_hexa = obj_stream_hexa.Trim(lineStringToHex("\n").ToCharArray());

            return obj_stream_hexa;
        }

        public string decodeStream(ref string stream_content_hexa, string which_filter, int len, Dictionary<string,int> parameters)
        {

            string decodedStream = string.Empty;

            string[] kinds_of_filters = new string[] { "/FlateDecode" };

            int idx_filter = Array.IndexOf(kinds_of_filters, which_filter);

            switch (idx_filter)
            {
                case 0:

                    byte[] byteArray = lineHexToByte(stream_content_hexa);

                    if (parameters.Count < 1)
                    {
                    
                        if (byteArray.Length == len)
                        {

                            byte[] cutByteArray = new byte[byteArray.Length - 2];
                            Array.Copy(byteArray, 2, cutByteArray, 0, cutByteArray.Length);

                            MemoryStream ms_hexa_comp = new MemoryStream(cutByteArray);
                            MemoryStream ms_hexa = new MemoryStream();

                            DeflateStream d_Stream = new DeflateStream(ms_hexa_comp, CompressionMode.Decompress);

                            d_Stream.CopyTo(ms_hexa);
                            d_Stream.Close();
                            ms_hexa.Position = 0;

                            byte[] byteArrayDecomp = ms_hexa.ToArray();

                            ms_hexa.Close();

                            decodedStream = BitConverter.ToString(byteArrayDecomp);

                        }
                        else
                        {
                            //throw error
                        }

                    }
                    else
                    {
                        byte[] cutByteArray = new byte[byteArray.Length - 2];
                        Array.Copy(byteArray, 2, cutByteArray, 0, cutByteArray.Length);

                        MemoryStream ms_hexa_comp = new MemoryStream(cutByteArray);
                        
                        ImageProcessor.Imaging.Formats.PngFormat png = new ImageProcessor.Imaging.Formats.PngFormat();

                        var imgFactory = new ImageFactory();
                        imgFactory.Load(ms_hexa_comp).Format(png);
                        
                        //byte[] byteArrayDecomp = ms_hexa.ToArray();

                        ms_hexa_comp.Close();

                        //decodedStream = BitConverter.ToString(byteArrayDecomp);

                    }

                    
                    break;

            }

            return decodedStream;

        }

        private MemoryStream DecodeFilter(MemoryStream stream, int columns, int predictor)
        {

            predictor = 11;

            //if (predictor == 1)
            //{
            //    // no prediction algorithm, decode only

            //    // BUG: buffer size matters, if larger decoding fails, this is a bug in ZlibStream?
            //    return stream.ToMemoryStream(1024);
            //}

            //if (predictor == 2)
            //{
            //    throw new PdfParseException(Resources.UnsupportedTiff2Predictor);
            //}

            //var colors = this.Parameters.Get<int>("Colors", 1);
            var colors = 1;

            //var bitsPerComponent = this.Parameters.Get<int>("BitsPerComponent", 8);
            var bitsPerComponent = 8;

            //var columns = this.Parameters.Get<int>("Columns", 1);

            var bpp = (colors * bitsPerComponent + 7) / 8;
            var rowLength = (columns * colors * bitsPerComponent + 7) / 8;

            var row = new byte[rowLength];
            var priorRow = new byte[rowLength];
            var output = new MemoryStream();
            
            //predictor = stream.ReadByte();

            //if (predictor == -1)
            //{
            //    break;  // end of stream
            //}

            //if (stream.Read(row, 0, rowLength) != rowLength)
            //{
            //    throw new PdfParseException(Resources.UnexpectedEndOfStream);
            //}

            if (predictor == 0)
            {
                // PNG None
                // nothing here, in == out
            }
            else if (predictor == 11)
            {
                // PNG Sub
                for (var x = 0; x < rowLength; x++)
                {
                    var left = (x - bpp >= 0) ? row[x - bpp] : 0;
                    row[x] = (byte)((row[x] + left) % 0x100);
                }
            }
            else if (predictor == 12)
            {
                // PNG Up
                for (int x = 0; x < rowLength; x++)
                {
                    var prior = priorRow[x];
                    row[x] = (byte)((row[x] + prior) % 0x100);
                }
            }
            else if (predictor == 13)
            {
                // PNG Average
                for (int x = 0; x < rowLength; x++)
                {
                    var left = (x - bpp >= 0) ? row[x - bpp] : 0;
                    var prior = priorRow[x];
                    row[x] = (byte)((row[x] + Math.Floor((double)(left + prior)) / 2) % 0x100);
                }
            }
            else if (predictor == 14)
            {
                // PNG Paeth
                for (int x = 0; x < rowLength; x++)
                {
                    byte left = 0;
                    byte priorLeft = 0;
                    if (x - bpp >= 0)
                    {
                        left = row[x - bpp];
                        priorLeft = priorRow[x - bpp];
                    }
                    byte prior = priorRow[x];

                    row[x] = (byte)((row[x] + PaethPredictor(left, prior, priorLeft)) % 0x100);
                }
            }
            //else
            //{
            //    throw new PdfParseException(Resources.UnknownPredictor, predictor);
            //}

            // copy prior row
            for (var i = 0; i < rowLength; i++)
            {
                priorRow[i] = row[i];
            }
            output.Write(row, 0, row.Length);
            
            output.Seek(0, SeekOrigin.Begin);

            return output;
        }

        /// <summary>
        /// The Paeth predictor function.
        /// </summary>
        /// <param name="a">Left.</param>
        /// <param name="b">Above.</param>
        /// <param name="c">Upper left.</param>
        /// <returns></returns>
        private static int PaethPredictor(byte a, byte b, byte c)
        {
            var p = a + b - c;              // initial estimate
            var pa = Math.Abs(p - a);       // distances to a, b, c
            var pb = Math.Abs(p - b);
            var pc = Math.Abs(p - c);
            // return nearest of a,b,c
            // breaking ties in order a,b,c
            if (pa <= pb && pa <= pc)
            {
                return a;
            }
            if (pb <= pc)
            {
                return b;
            }
            return c;
        }

        public void saveDebugInfo(ref Globals globals, string data, string dictionary)
        {
            if (globals.PDF_Plugin_Properties.debug)
            {
                StreamWriter file = File.AppendText(globals.PDF_File.directoryPath + @"\" + dictionary + ".txt");

                string[] Separators = new string[] { "\n", "\r" };

                string[] tmp = data.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                data = String.Join(" ",tmp);

                file.WriteLine(globals.PDF_File.fileNamePath + ";" + Convert.ToString(globals.PDF_Properties.pdf_version_f) + ";" + data);

                file.Close();
            }

        }












        public int endOfLinePosition(ref string stream, string CR, string LF, int start)
        {
            int my_idx = 0;

            int cr_idx = stream.IndexOf(CR, start);

            int lf_idx = stream.IndexOf(LF, start);

            if (cr_idx < lf_idx)
            {
                my_idx = cr_idx;
            }
            else
            {
                my_idx = lf_idx;
            }

            return my_idx;
        }

        public string getLineFromByteOffset(ref string stream, int offset)
        {
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            string LF = white_space_characters.LINE_FEED.Hexadecimal;
            string CR = white_space_characters.CARRIAGE_RETURN.Hexadecimal;

            string line = string.Empty;

            int[] idx = new int[2] { -1, -1 };

            idx[0] = stream.IndexOf(LF, offset + 1);
            idx[1] = stream.IndexOf(CR, offset + 1);

            int diff = Math.Abs(idx[0] - idx[1]);

            int value = 0;

            if ( diff == 3)
            {
                int max_val = idx.Max();

                int[] idxs = new int[2] { -1, -1 };

                idxs[0] = stream.IndexOf(LF, max_val + 1);
                idxs[1] = stream.IndexOf(CR, max_val + 1);

                value = idxs.Min();
            }
            else
            {
                idx = idx.Where(val => val != -1).ToArray();

                int min_val = idx.Min();

                int[] idxs = new int[2] { -1, -1 };

                idxs[0] = stream.IndexOf(LF, min_val + 1);
                idxs[1] = stream.IndexOf(CR, min_val + 1);

                value = idxs.Min();
            }
            
            line = stream.Substring(offset, value - offset);

            string teste = lineHexToString(line);

            return line;
        }
        
        public string substringDelimiterRecurrent(string stream, int offset, string delimiter_left, string delimiter_right)
        {
            int idx_lp = stream.IndexOf(delimiter_left, offset);

            int idx_rp = stream.IndexOf(delimiter_right, offset);

            string tmp = stream.Substring(idx_lp, idx_rp - idx_lp);

            string step_tmp = tmp;

            int new_idx_lp = step_tmp.IndexOf(delimiter_left, 2);

            while (new_idx_lp != -1)
            {
                int new_idx_rp = stream.IndexOf(delimiter_right, idx_rp + 2);

                tmp = stream.Substring(idx_lp, new_idx_rp - idx_lp);

                step_tmp = stream.Substring(new_idx_lp, idx_rp - new_idx_lp);

                new_idx_lp = step_tmp.IndexOf(delimiter_left, 2);
            }

            return tmp;

        }

        public void getIntegerInDictionaryHex(ref int value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length + 2;

            int[] idx = new int[17];

            idx[0] = stream.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
            idx[1] = stream.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
            idx[2] = stream.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
            idx[3] = stream.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[4] = stream.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
            idx[5] = stream.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
            idx[6] = stream.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
            idx[7] = stream.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
            idx[8] = stream.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[9] = stream.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset);

            idx[10] = stream.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
            idx[11] = stream.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
            idx[12] = stream.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
            idx[13] = stream.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
            idx[14] = stream.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
            //idx[15] = stream.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
            //idx[16] = stream.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);
            
            int remove = -1;

            idx = idx.Where(val => val != remove).ToArray();

            remove = 0;

            idx = idx.Where(val => val != remove).ToArray();

            int my_idx = 0;

            if (idx.Count() > 0)
            {
                my_idx = idx.Min();
            }
            else
            {
                my_idx = stream.Length;
            }

            string my_integer = stream.Substring(offset, my_idx - offset);

            my_integer = my_integer.Trim('-');

            int integer = Convert.ToInt32(lineHexToString(my_integer));

            value = integer;

            // return error -1

        }

        public void getNumberInDictionaryHex(ref float value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length + 2;

            int[] idx = new int[17];

            idx[0] = stream.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
            idx[1] = stream.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
            idx[2] = stream.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
            idx[3] = stream.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[4] = stream.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
            idx[5] = stream.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
            idx[6] = stream.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
            idx[7] = stream.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
            idx[8] = stream.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[9] = stream.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset);

            idx[10] = stream.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
            idx[11] = stream.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
            idx[12] = stream.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
            idx[13] = stream.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
            idx[14] = stream.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
            idx[15] = stream.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
            idx[16] = stream.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

            int remove = -1;

            idx = idx.Where(val => val != remove).ToArray();

            int my_idx = idx.Min();

            string my_number = stream.Substring(offset, my_idx - offset);

            my_number = my_number.Trim('-');

            float number = (float)Convert.ToDouble(lineHexToString(my_number));

            value = number;

            // return error -1

        }

        public void getStringInDictionaryHex(ref string value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length;


            int idx_lts = stream.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
            int idx_lp = stream.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// 7.3.4.2 Literal Strings
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ((idx_lp < idx_lts) && idx_lts != -1 && idx_lp != -1)
            {

                string tmp = substringDelimiterRecurrent(stream, offset, delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal);

                /////////////////////////////////////////////////////////
                /// Table 3 - Escape Sequences in Literal Strings
                /////////////////////////////////////////////////////////
                /// it should convert to string
                /////////////////////////////////////////////////////////

                value = lineHexToString(tmp);
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// 7.3.4.3 Hexadecimal Strings
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if ((idx_lts < idx_lp) && idx_lp != -1 && idx_lts != -1) 
            {
                int idx_gts = stream.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);

                string tmp = stream.Substring(idx_lts, idx_gts - idx_lts);

                value = tmp;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// CHECK ERRORS
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (idx_lp == -1 && idx_lts == -1)
            {
                //throw error
            }

        }

        public void getIndirectDictionaryInDictionaryHex(ref string[] value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length + 2;

            int[] idx = new int[16];

            idx[0] = stream.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
            idx[1] = stream.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
            idx[2] = stream.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
            idx[3] = stream.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[4] = stream.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
            idx[5] = stream.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
            idx[6] = stream.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
            idx[7] = stream.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
            idx[8] = stream.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[9] = stream.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset);

            idx[10] = stream.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
            idx[11] = stream.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
            idx[12] = stream.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
            idx[13] = stream.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
            idx[14] = stream.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
            idx[15] = stream.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
            //idx[16] = stream.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

            int remove = -1;

            idx = idx.Where(val => val != remove).ToArray();

            int my_idx = 0;

            if (idx.Length > 0)
            {
                my_idx = idx.Min();
            }
            else
            {
                my_idx = stream.Length - 1;
            }

            string my_keys_hexa = stream.Substring(offset, my_idx - offset);

            string my_keys_string = lineHexToString(my_keys_hexa);

            string[] keys = my_keys_string.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            value = keys;

        }

        public void getNameInDictionaryHex(ref string value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length + 2;

            int[] idx = new int[17];

            idx[0] = stream.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
            idx[1] = stream.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
            idx[2] = stream.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
            idx[3] = stream.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[4] = stream.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
            idx[5] = stream.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
            idx[6] = stream.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
            idx[7] = stream.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
            idx[8] = stream.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[9] = stream.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset);

            idx[10] = stream.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
            idx[11] = stream.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
            idx[12] = stream.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
            idx[13] = stream.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
            idx[14] = stream.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
            idx[15] = stream.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
            idx[16] = stream.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

            int remove = -1;

            idx = idx.Where(val => val != remove).ToArray();

            int my_idx = idx.Min();

            string my_name_hexa = stream.Substring(offset, my_idx - offset);

            string my_name_string = lineHexToString(my_name_hexa);

            value = my_name_string;

        }

        public void getBooleanInDictionaryHex(ref Boolean value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new  DELIMITER_CHARACTERS();
             WHITE_SPACE_CHARACTERS white_space_characters = new  WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length + 2;

            int[] idx = new int[17];

            idx[0] = stream.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
            idx[1] = stream.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
            idx[2] = stream.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
            idx[3] = stream.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[4] = stream.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
            idx[5] = stream.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
            idx[6] = stream.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
            idx[7] = stream.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
            idx[8] = stream.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[9] = stream.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset);

            idx[10] = stream.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
            idx[11] = stream.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
            idx[12] = stream.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
            idx[13] = stream.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
            idx[14] = stream.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
            idx[15] = stream.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
            idx[16] = stream.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

            int remove = -1;

            idx = idx.Where(val => val != remove).ToArray();

            int my_idx = idx.Min();

            string my_boolean_hexa = stream.Substring(offset, my_idx - offset);

            string my_boolean_string = lineHexToString(my_boolean_hexa);

            Boolean result = false;

            if (String.Compare(my_boolean_string, "true") == 0)
            {
                result = true;
            }

            value = result;

        }

        public void getArrayInDictionaryHex(ref List<string> value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length;

            string tmp = substringDelimiterRecurrent(stream, offset, delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal);

            string substream = tmp.Trim('-');
            substream = substream.Trim(white_space_characters.SPACE.Hexadecimal.ToCharArray());
            substream = substream.Trim('-');

            substream = lineHexToString(substream);

            string[] result = substream.Split(white_space_characters.SPACE.Hexadecimal.ToCharArray());

            value.AddRange(result);

        }

        public void getIndirectDictionaryArrayInDictionaryHex(ref List<string[]> value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {

            //NEEDS TO IMPLEMENT RECURSSIVE ARRAYS
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length;

            string tmp = substringDelimiterRecurrent(stream, offset, delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal);

            string substream = tmp.Trim('-');
            substream = substream.Trim(white_space_characters.SPACE.Hexadecimal.ToCharArray());
            substream = substream.Trim('-');
            substream = substream.Trim(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal.ToCharArray());
            substream = substream.Trim(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal.ToCharArray());
            substream = substream.Trim('-');
            substream = substream.Trim(white_space_characters.LINE_FEED.Hexadecimal.ToCharArray());
            substream = substream.Trim('-');

            substream = lineHexToString(substream);

            string[] result = substream.Split(lineHexToString(white_space_characters.LINE_FEED.Hexadecimal).ToCharArray());

            for (int i = 0; i < result.Length; i++)
            {
                string tmp_result = result[i];

                string[] tmp2result = tmp_result.Split(lineHexToString(white_space_characters.SPACE.Hexadecimal).ToCharArray());
               
                value.Add(tmp2result);

            }

        }

        public void getIndirectDictionaryArrayWithNamesInDictionaryHex(ref Dictionary<string,string[]> value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();
            
            string LTS_double = delimiter_characters.LESS_THAN_SIGN.Hexadecimal + '-' + delimiter_characters.LESS_THAN_SIGN.Hexadecimal;
            string GTS_double = delimiter_characters.GREATER_THAN_SIGN.Hexadecimal + '-' + delimiter_characters.GREATER_THAN_SIGN.Hexadecimal;
            
            string tmp = substringDelimiterRecurrent(stream, offset, LTS_double, GTS_double);

            string substream = tmp.Trim('-');
            substream = substream.Trim(white_space_characters.SPACE.Hexadecimal.ToCharArray());
            substream = substream.Trim('-');
            substream = substream.Trim(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal.ToCharArray());
            substream = substream.Trim(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal.ToCharArray());
            substream = substream.Trim('-');
            substream = substream.Trim(white_space_characters.LINE_FEED.Hexadecimal.ToCharArray());
            substream = substream.Trim('-');
            substream = substream.Trim(LTS_double.ToCharArray());
            substream = substream.Trim(GTS_double.ToCharArray());

            substream = lineHexToString(substream);

            string[] _separator = new string[] { white_space_characters.LINE_FEED.Glyph};

            string[] result = substream.Split(_separator, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < result.Length; i = i + 2)
            {
                string tmp_result = result[i];

                string tmp2_result = result[i + 1];

                string[] tmp2result = tmp2_result.Split(lineHexToString(white_space_characters.SPACE.Hexadecimal).ToCharArray());

                value.Add(tmp_result,tmp2result);

            }

        }

        public void getDateInDictionaryHex(ref DateTime value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length + 2;

            int[] idx = new int[17];

            idx[0] = stream.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
            idx[1] = stream.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
            idx[2] = stream.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
            idx[3] = stream.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[4] = stream.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
            idx[5] = stream.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
            idx[6] = stream.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
            idx[7] = stream.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
            idx[8] = stream.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[9] = stream.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset);

            idx[10] = stream.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
            idx[11] = stream.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
            idx[12] = stream.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
            idx[13] = stream.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
            idx[14] = stream.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
            idx[15] = stream.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
            idx[16] = stream.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

            int remove = -1;

            idx = idx.Where(val => val != remove).ToArray();

            int my_idx = idx.Min();

            string my_date = stream.Substring(offset, my_idx - offset);

            my_date = my_date.Trim('-');

            string date_string = lineHexToString(my_date);

            DateTime time = Convert.ToDateTime(date_string);

            value = time;

        }

        public void getMatrixInDictionaryHex(ref List<List<float>> value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();


        }

        public void getStreamInDictionaryHex(ref string value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            offset = offset + keyLength + white_space_characters.SPACE.Hexadecimal.Length + 2;

            int[] idx = new int[17];

            idx[0] = stream.IndexOf(delimiter_characters.GREATER_THAN_SIGN.Hexadecimal, offset);
            idx[1] = stream.IndexOf(delimiter_characters.LEFT_CURLY_BRACKET.Hexadecimal, offset);
            idx[2] = stream.IndexOf(delimiter_characters.LEFT_PARENTHESIS.Hexadecimal, offset);
            idx[3] = stream.IndexOf(delimiter_characters.LEFT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[4] = stream.IndexOf(delimiter_characters.LESS_THAN_SIGN.Hexadecimal, offset);
            idx[5] = stream.IndexOf(delimiter_characters.PERCENT_SIGN.Hexadecimal, offset);
            idx[6] = stream.IndexOf(delimiter_characters.RIGHT_CURLY_BRACKET.Hexadecimal, offset);
            idx[7] = stream.IndexOf(delimiter_characters.RIGHT_PARENTHESIS.Hexadecimal, offset);
            idx[8] = stream.IndexOf(delimiter_characters.RIGHT_SQUARE_BRACKET.Hexadecimal, offset);
            idx[9] = stream.IndexOf(delimiter_characters.SOLIDUS.Hexadecimal, offset);

            idx[10] = stream.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
            idx[11] = stream.IndexOf(white_space_characters.EOF.Hexadecimal, offset);
            idx[12] = stream.IndexOf(white_space_characters.FORM_FEED.Hexadecimal, offset);
            idx[13] = stream.IndexOf(white_space_characters.HORIZONTAL_TAB.Hexadecimal, offset);
            idx[14] = stream.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);
            idx[15] = stream.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
            idx[16] = stream.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

            int remove = -1;

            idx = idx.Where(val => val != remove).ToArray();

            int my_idx = idx.Min();

            string result = stream.Substring(offset, my_idx - offset);

            value = result;

        }


    }

}
