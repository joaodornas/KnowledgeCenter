using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfPlugin
{

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// GENERAL INFORMATION
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    public struct custom_value<T>
    {
        // the value inside the pdf data
        public T value;

        // the version in which this feature was available
        public float pdf_version_f;

        // whether it is required or not
        public Boolean required;

        public Boolean found;

        public string key;

        public int kind_of_object_expected;

        public int kind_of_object_found;

    }

    public class PDF_Plugin_Properties
    {
        public float max_pdf_file_size { get; set; }
        public float free_host_memory { get; set; }
        public int total_number_of_simultaneous_files { get; set; }

        public Boolean debug { get; set; }
    }

    public class PDF_File
    {
        public string fileNamePath { get; set; }
        public string directoryPath { get; set; }
        public long fileSize { get; set; }

        public string pdf_stream_hexa;


        public string pdf_stream_string;

        public List<string> pdf_lines_hexa;
        public List<string> pdf_lines_string;

    }

    public class PDF_Properties
    {
        public float pdf_version_f;

        public int nLines;

        public int nObjects;

        public int nPages;

        public int byte_offset_of_last_cross_reference_section;

        public List<int> byte_offset_xref;

        public List<int> byte_offset_xref_kind;

        public PDF_Properties(Boolean is_true = true)
        {
            List<int> byte_offset_xref = new List<int>();

            List<int> byte_offset_xref_kind = new List<int>();
        }
    }


    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// SYNTAX INFORMATION (clause 7)
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    //7.2.2 Character Set
    public class Character_Set
    {
        public string Decimal { get; set; }
        public string Hexadecimal { get; set; }
        public string Octal { get; set; }
        public string Name { get; set; }
        public string Glyph { get; set; }
        public string Sequence { get; set; }
    }

    //Table 1 - White-Space Characters
    public class WHITE_SPACE_CHARACTERS
    {
       
        public WHITE_SPACE_CHARACTERS()
        {
            this.Set();
        }


        public Character_Set NULL = new Character_Set();
        public Character_Set HORIZONTAL_TAB = new Character_Set();
        public Character_Set LINE_FEED = new Character_Set(); //newline character, end-of-line (EOL) marker
        public Character_Set FORM_FEED = new Character_Set();
        public Character_Set CARRIAGE_RETURN = new Character_Set(); //newline character, end-of-line (EOL) marker
        public Character_Set SPACE = new Character_Set();

        public Character_Set EOF = new Character_Set();

        public void Set()
        {
            NULL.Decimal = "0";
            NULL.Hexadecimal = "00";
            NULL.Octal = "000";
            NULL.Name = "Null";
            NULL.Glyph = "NUL";

            HORIZONTAL_TAB.Decimal = "9";
            HORIZONTAL_TAB.Hexadecimal = "09";
            HORIZONTAL_TAB.Octal = "011";
            HORIZONTAL_TAB.Name = "HORIZONTAL TAB";
            HORIZONTAL_TAB.Glyph = "HT";

            LINE_FEED.Decimal = "10";
            LINE_FEED.Hexadecimal = "0A";
            LINE_FEED.Octal = "012";
            LINE_FEED.Name = "Line Feed";
            LINE_FEED.Glyph = "\n";

            FORM_FEED.Decimal = "12";
            FORM_FEED.Hexadecimal = "0C";
            FORM_FEED.Octal = "014";
            FORM_FEED.Name = "FORM FEED";
            FORM_FEED.Glyph = "FF";

            CARRIAGE_RETURN.Decimal = "13";
            CARRIAGE_RETURN.Hexadecimal = "0D";
            CARRIAGE_RETURN.Octal = "015";
            CARRIAGE_RETURN.Name = "CARRIAGE RETURN";
            CARRIAGE_RETURN.Glyph = "CR";

            SPACE.Decimal = "32";
            SPACE.Hexadecimal = "20";
            SPACE.Octal = "040";
            SPACE.Name = "Space";
            SPACE.Glyph = " ";

            EOF.Hexadecimal = "25-25-45-4F-46";
            EOF.Name = "EOF";
            EOF.Glyph = "%%EOF";

        }

    }

    //Table 2 - Delimiter Characters
    public class DELIMITER_CHARACTERS
    {
       
        public DELIMITER_CHARACTERS()
        {
            this.Set();
        }

        public Character_Set LEFT_PARENTHESIS = new Character_Set();
        public Character_Set RIGHT_PARENTHESIS = new Character_Set();
        public Character_Set LESS_THAN_SIGN = new Character_Set();
        public Character_Set GREATER_THAN_SIGN = new Character_Set();
        public Character_Set LEFT_SQUARE_BRACKET = new Character_Set();
        public Character_Set RIGHT_SQUARE_BRACKET = new Character_Set();
        public Character_Set LEFT_CURLY_BRACKET = new Character_Set();
        public Character_Set RIGHT_CURLY_BRACKET = new Character_Set();
        public Character_Set SOLIDUS = new Character_Set();
        public Character_Set PERCENT_SIGN = new Character_Set();

        public void Set()
        {
            LEFT_PARENTHESIS.Decimal = "40";
            LEFT_PARENTHESIS.Hexadecimal = "28";
            LEFT_PARENTHESIS.Octal = "50";
            LEFT_PARENTHESIS.Name = "LEFT PARENTHESIS";
            LEFT_PARENTHESIS.Glyph = "(";

            RIGHT_PARENTHESIS.Decimal = "41";
            RIGHT_PARENTHESIS.Hexadecimal = "29";
            RIGHT_PARENTHESIS.Octal = "51";
            RIGHT_PARENTHESIS.Name = "RIGHT PARENTHESIS";
            RIGHT_PARENTHESIS.Glyph = ")";

            LESS_THAN_SIGN.Decimal = "60";
            LESS_THAN_SIGN.Hexadecimal = "3C";
            LESS_THAN_SIGN.Octal = "60";
            LESS_THAN_SIGN.Name = "LESS_THAN_SIGN";
            LESS_THAN_SIGN.Glyph = "<";

            GREATER_THAN_SIGN.Decimal = "62";
            GREATER_THAN_SIGN.Hexadecimal = "3E";
            GREATER_THAN_SIGN.Octal = "62";
            GREATER_THAN_SIGN.Name = "GREATER_THAN_SIGN";
            GREATER_THAN_SIGN.Glyph = ">";

            LEFT_SQUARE_BRACKET.Decimal = "91";
            LEFT_SQUARE_BRACKET.Hexadecimal = "5B";
            LEFT_SQUARE_BRACKET.Octal = "133";
            LEFT_SQUARE_BRACKET.Name = "LEFT_SQUARE_BRACKET";
            LEFT_SQUARE_BRACKET.Glyph = "[";

            RIGHT_SQUARE_BRACKET.Decimal = "93";
            RIGHT_SQUARE_BRACKET.Hexadecimal = "5D";
            RIGHT_SQUARE_BRACKET.Octal = "135";
            RIGHT_SQUARE_BRACKET.Name = "RIGHT_SQUARE_BRACKET";
            RIGHT_SQUARE_BRACKET.Glyph = "]";

            LEFT_CURLY_BRACKET.Decimal = "123";
            LEFT_CURLY_BRACKET.Hexadecimal = "7B";
            LEFT_CURLY_BRACKET.Octal = "173";
            LEFT_CURLY_BRACKET.Name = "LEFT_CURLY_BRACKET";
            LEFT_CURLY_BRACKET.Glyph = "{";

            RIGHT_CURLY_BRACKET.Decimal = "125";
            RIGHT_CURLY_BRACKET.Hexadecimal = "7D";
            RIGHT_CURLY_BRACKET.Octal = "175";
            RIGHT_CURLY_BRACKET.Name = "RIGHT_CURLY_BRACKET";
            RIGHT_CURLY_BRACKET.Glyph = "}";

            SOLIDUS.Decimal = "47";
            SOLIDUS.Hexadecimal = "2F";
            SOLIDUS.Octal = "57";
            SOLIDUS.Name = "SOLIDUS";
            SOLIDUS.Glyph = "/";

            PERCENT_SIGN.Decimal = "37";
            PERCENT_SIGN.Hexadecimal = "25";
            PERCENT_SIGN.Octal = "45";
            PERCENT_SIGN.Name = "PERCENT_SIGN";
            PERCENT_SIGN.Glyph = "%";

        }

    }

    //7.3.4.2 Literal Strings 
    //Table 3 - Escape Sequences in Literal Strings
    public class Escape_Sequences_in_Literal_Strings
    {
        public Escape_Sequences_in_Literal_Strings()
        {
            this.Set();
        }

        public Character_Set LINE_FEED = new Character_Set();
        public Character_Set CARRIAGE_RETURN = new Character_Set();
        public Character_Set HORIZONTAL_TAB = new Character_Set();
        public Character_Set BACKSPACE = new Character_Set();
        public Character_Set FORM_FEED = new Character_Set();
        public Character_Set LEFT_PARENTHESIS = new Character_Set();
        public Character_Set RIGHT_PARENTHESIS = new Character_Set();
        public Character_Set REVERSE_SOLIDUS = new Character_Set();
        public Character_Set Character_code_ddd = new Character_Set();

        public void Set()
        {
            LINE_FEED.Decimal = "10";
            LINE_FEED.Hexadecimal = "0A";
            LINE_FEED.Octal = "012";
            LINE_FEED.Name = "Line Feed";
            LINE_FEED.Glyph = "LF";
            LINE_FEED.Sequence = "\n";

            CARRIAGE_RETURN.Decimal = "13";
            CARRIAGE_RETURN.Hexadecimal = "0D";
            CARRIAGE_RETURN.Octal = "015";
            CARRIAGE_RETURN.Name = "CARRIAGE RETURN";
            CARRIAGE_RETURN.Glyph = "CR";
            CARRIAGE_RETURN.Sequence = "\r";

            HORIZONTAL_TAB.Decimal = "9";
            HORIZONTAL_TAB.Hexadecimal = "09";
            HORIZONTAL_TAB.Octal = "011";
            HORIZONTAL_TAB.Name = "HORIZONTAL TAB";
            HORIZONTAL_TAB.Glyph = "HT";
            HORIZONTAL_TAB.Sequence = "\t";

            BACKSPACE.Decimal = "8";
            BACKSPACE.Hexadecimal = "08";
            BACKSPACE.Octal = "";
            BACKSPACE.Name = "BACKSPACE";
            BACKSPACE.Glyph = "BS";
            BACKSPACE.Sequence = "\b";

            FORM_FEED.Decimal = "12";
            FORM_FEED.Hexadecimal = "0C";
            FORM_FEED.Octal = "014";
            FORM_FEED.Name = "FORM FEED";
            FORM_FEED.Glyph = "FF";
            FORM_FEED.Sequence = "\f";

            LEFT_PARENTHESIS.Decimal = "40";
            LEFT_PARENTHESIS.Hexadecimal = "28";
            LEFT_PARENTHESIS.Octal = "50";
            LEFT_PARENTHESIS.Name = "LEFT PARENTHESIS";
            LEFT_PARENTHESIS.Glyph = "(";
            LEFT_PARENTHESIS.Sequence = "\\("; //"\("

            RIGHT_PARENTHESIS.Decimal = "41";
            RIGHT_PARENTHESIS.Hexadecimal = "29";
            RIGHT_PARENTHESIS.Octal = "51";
            RIGHT_PARENTHESIS.Name = "RIGHT PARENTHESIS";
            RIGHT_PARENTHESIS.Glyph = ")";
            RIGHT_PARENTHESIS.Sequence = "\\)"; //"\)"

            REVERSE_SOLIDUS.Decimal = "";
            REVERSE_SOLIDUS.Hexadecimal = "5C";
            REVERSE_SOLIDUS.Octal = "";
            REVERSE_SOLIDUS.Name = "REVERSE SOLIDUS";
            REVERSE_SOLIDUS.Glyph = "";
            REVERSE_SOLIDUS.Sequence = "\\";

            Character_code_ddd.Decimal = "";
            Character_code_ddd.Hexadecimal = "";
            Character_code_ddd.Octal = "";
            Character_code_ddd.Name = "Character code ddd";
            Character_code_ddd.Glyph = "";
            Character_code_ddd.Sequence = "\\ddd"; //"\ddd"
        }

    }

    //7.5.2 File Header (PDF Versions)
    public class PDF_Versions
    {
        public float v10 = 1.0f;
        public float v11 = 1.1f;
        public float v12 = 1.2f;
        public float v13 = 1.3f;
        public float v14 = 1.4f;
        public float v15 = 1.5f;
        public float v16 = 1.6f;
        public float v17 = 1.7f;

        public float iso32000 = 32000f;
    }

    //7.3 Objects
    public class Kinds_of_Objects
    {
         public const int Date = 000; //0.0.0 Date Objects

        public const int Rectangle = 001; //0.0.1 Rectangle Objects

        public const int Boolean = 732; //7.3.2 Boolean Objects
           
         public const int Numeric = 733; //7.3.3 Numeric Objects
           
         public const int String = 734; //7.3.4 String Objects
            
         public const int Name = 735; //7.3.5 Name Objects

         public const int Array = 736; //7.3.6 Array Objects
            
         public const int Dictionary = 737; //7.3.7 Dictionary Objects
            
         public const int Stream = 738; //7.3.8 Stream Objects
            
         public const int Null = 739; //7.3.9 Null Object

         public const int Indirect = 7310; //7.3.10 Indirect Objects

         public const int Functions = 710; //7.10 Functions

         public const int Undefined = -1; //No Definition

    }
    
    public class Kinds_of_XRef
    {
        public const int Table = 1;

        public const int Stream = 2;
    }

    //7.4 Filters
    public class FlatDecode_Predictors
    {
        public int No_Prediction = 1;

        public int TIFF_Predictor_2 = 2;

        public int PNG_None_on_all_rows = 10;

        public int PNG_Sub_on_all_rows = 11;

        public int PNG_Up_on_all_rows = 12;

        public int PNG_Average_on_all_rows = 13;

        public int PNG_Paeth_on_all_rows = 14;

        public int PNG_optimum = 15;
    }

    //7.5.4 Cross-Reference Table
    public struct PDF_Cross_Reference_Table
    {
   
        // global object number
        public int global_object_idx;

        // local object number
        public int local_object_idx;

        //  nnnnnnnnnn  shall be a 10-digit byte offset in the decoded stream
        public int byte_offset;

        //  ggggg  shall be a 5-digit generation number
        public int generation_number;

        //  n  shall be a keyword identifying this as an in-use entry
        public Boolean in_use;

    }

    //7.5.5 File Trailer
    //Table 15 - Entries in the file trailer dictionary
    public struct PDF_Trailer
    {

        public custom_value<int> Size;

        public custom_value<int> Prev;

        public custom_value<string[]> Root;

        public custom_value<string[]> Encrypt;

        public custom_value<string[]> Info;

        public custom_value<string[]> ID;

        public custom_value<int> XRefStm;

        public PDF_Trailer(Boolean is_True = true)
        {
            PDF_Versions pdf_versions = new PDF_Versions();

            Size.pdf_version_f = pdf_versions.v10;
            Size.required = true;
            Size.value = 0;
            Size.found = false;
            Size.key = "/Size";
            Size.kind_of_object_expected = Kinds_of_Objects.Numeric;
            Size.kind_of_object_found = -1;

            Prev.pdf_version_f = pdf_versions.v10;
            Prev.required = false;
            Prev.value = 0;
            Prev.found = false;
            Prev.key = "/Prev";
            Prev.kind_of_object_expected = Kinds_of_Objects.Numeric;
            Prev.kind_of_object_found = -1;

            XRefStm.pdf_version_f = pdf_versions.v15;
            XRefStm.required = false;
            XRefStm.value = 0;
            XRefStm.found = false;
            XRefStm.key = "/XRefStm";
            XRefStm.kind_of_object_expected = Kinds_of_Objects.Numeric;
            XRefStm.kind_of_object_found = -1;

            Root.pdf_version_f = pdf_versions.v10;
            Root.required = true;
            Root.value = new string[3];
            Root.found = false;
            Root.key = "/Root";
            Root.kind_of_object_expected = Kinds_of_Objects.Array;
            Root.kind_of_object_found = -1;

            Encrypt.pdf_version_f = pdf_versions.v11;
            Encrypt.required = false;
            Encrypt.value = new string[3];
            Encrypt.found = false;
            Encrypt.key = "/Encrypt";
            Encrypt.kind_of_object_expected = Kinds_of_Objects.Array;
            Encrypt.kind_of_object_found = -1;

            Info.pdf_version_f = pdf_versions.v10;
            Info.required = false;
            Info.value = new string[3];
            Info.found = false;
            Info.key = "/Info";
            Info.kind_of_object_expected = Kinds_of_Objects.Array;
            Info.kind_of_object_found = -1;

            ID.pdf_version_f = pdf_versions.v11;
            ID.required = false;
            ID.value = new string[3];
            ID.found = false;
            ID.key = "/ID";
            ID.kind_of_object_expected = Kinds_of_Objects.Array;
            ID.kind_of_object_found = -1;

        }

    }
 
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// GLOBAL Classes Instantiation
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    
    public class Globals
    {
        
        public PDF_Plugin_Properties PDF_Plugin_Properties;

        public PDF_File PDF_File;

        public PDF_Versions PDF_Versions;

        public PDF_Properties PDF_Properties = new PDF_Properties();

        public List<PDF_Trailer> PDF_Trailer;

        public List<PDF_Cross_Reference_Table> PDF_Cross_Reference_Table;

        public Dictionary<string, object> PDF_Document_Catalog;

        public Dictionary<string, object> PDF_Page_Tree;

        public List<Dictionary<string, object>> PDF_Pages;

        public List<string> PDF_Pages_Contents;

        public Dictionary<string, object> PDF_Pages_Resources;
        
        public Globals()
        {
            PDF_Properties = new PDF_Properties(true);

            PDF_Plugin_Properties = new PDF_Plugin_Properties();

            PDF_File = new PDF_File();

            PDF_Trailer = new List<PDF_Trailer>();

            PDF_Document_Catalog = new Dictionary<string, object>();

            PDF_Page_Tree = new Dictionary<string, object>();

            PDF_Pages = new List<Dictionary<string, object>>();

            PDF_Pages_Contents = new List<string>();

            PDF_Pages_Resources = new Dictionary<string, object>();
            
        }

    }
}
