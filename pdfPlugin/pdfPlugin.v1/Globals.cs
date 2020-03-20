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

    public class custom_value<T>
    {
        // the value inside the pdf data
        public T value;

        // the version in which this feature was available
        public float pdf_version_f;

        // whether it is required or not
        public Boolean required;

        // whether is was found or not
        public Boolean found;

        public int kind_of_object_found;

        public int kind_of_object_expected;

        public custom_value()
        {
            value = default(T);

            pdf_version_f = 0.0f;

            required = false;

            found = false;

            kind_of_object_found = -1;

            kind_of_object_expected = -1;

        }
        
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

    //7.2.2 Character Set 
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
            SPACE.Glyph = "SP";

            EOF.Hexadecimal = "25-25-45-4F-46";
            EOF.Name = "EOF";
            EOF.Glyph = "%%EOF";

        }

    }

    //7.2.2 Character Set
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

        public int Boolean = 732; //7.3.2 Boolean Objects
           
        public int Numeric = 733; //7.3.3 Numeric Objects
           
        public int String = 734; //7.3.4 String Objects
            
        public int Name = 735; //7.3.5 Name Objects

        public int Array = 736; //7.3.6 Array Objects
            
        public int Dictionary = 737; //7.3.7 Dictionary Objects
            
        public int Stream = 738; //7.3.8 Stream Objects
            
        public int Null = 739; //7.3.9 Null Object

        public int Indirect = 7310; //7.3.10 Indirect Objects

        public int Functions = 710; //7.10 Functions

        public int Dictionary_Wrapper = 0; //0 Dictionary in stream mode

    }
    
    //7.5.4 Cross-Reference Table
    public struct PDF_Cross_Reference_Table
    {
        // subsection number
        public int subsection_idx;

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

        public custom_value<int> byte_offset_of_last_cross_reference_section;

        public PDF_Trailer(Boolean is_True = true)
        {
            PDF_Versions pdf_versions = new PDF_Versions();

            Size.pdf_version_f = pdf_versions.v10;
            Size.required = true;
            Size.value = 0;
            Size.found = false;
            Size.key = "/Size";

            Prev.pdf_version_f = pdf_versions.v10;
            Prev.required = false;
            Prev.value = 0;
            Prev.found = false;
            Prev.key = "/Prev";

            Root.pdf_version_f = pdf_versions.v10;
            Root.required = true;
            Root.value = new string[3];
            Root.found = false;
            Root.key = "/Root";

            Encrypt.pdf_version_f = pdf_versions.v11;
            Encrypt.required = false;
            Encrypt.value = new string[3];
            Encrypt.found = false;
            Encrypt.key = "/Encrypt";

            Info.pdf_version_f = pdf_versions.v10;
            Info.required = false;
            Info.value = new string[3];
            Info.found = false;
            Info.key = "/Info";

            ID.pdf_version_f = pdf_versions.v11;
            ID.required = false;
            ID.value = new string[3];
            ID.found = false;
            ID.key = "/ID";

            byte_offset_of_last_cross_reference_section.pdf_version_f = pdf_versions.v10;
            byte_offset_of_last_cross_reference_section.required = true;
            byte_offset_of_last_cross_reference_section.value = 0;
            byte_offset_of_last_cross_reference_section.found = false;
            byte_offset_of_last_cross_reference_section.key = string.Empty;

        }

    }

  
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// GLOBAL Classes Instantiation
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    
    public class Globals
    {

        public Kinds_of_Objects Kinds_of_Objects { get; }

        public PDF_Plugin_Properties PDF_Plugin_Properties;

        public PDF_File PDF_File;

        public PDF_Versions PDF_Versions;

        public PDF_Properties PDF_Properties = new PDF_Properties();

        public PDF_Trailer PDF_Trailer;
        public PDF_Cross_Reference_Table[] PDF_Cross_Reference_Table;

        public Globals()
        {
            Boolean is_true = true;

            PDF_Versions = new PDF_Versions();

            Kinds_of_Objects = new Kinds_of_Objects();

            PDF_Plugin_Properties = new PDF_Plugin_Properties();

            PDF_File = new PDF_File();

            PDF_Trailer = new PDF_Trailer(is_true);

        }

    }
}
