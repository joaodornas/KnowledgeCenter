using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace pdfPlugin
{
    public class tools
    {
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

        public string lineStringToHex(string lineString)
        {
            byte[] word = Encoding.Default.GetBytes(lineString);

            var hexString = BitConverter.ToString(word);

            return hexString;
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

        public string getLineFromByteOffset(ref string stream, int offset, string LF)
        {
            string line;

            int idx = stream.IndexOf(LF, offset);

            line = stream.Substring(offset, idx - offset);

            return line;
        }

        public int identifyObject(ref Globals globals, ref string stream_hexa, int offset)
        {
            DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            int[] idx = new int[2];

            string stream_string = lineHexToString(stream_hexa);

            idx[0] = stream_hexa.IndexOf(white_space_characters.CARRIAGE_RETURN.Hexadecimal, offset);
            idx[1] = stream_hexa.IndexOf(white_space_characters.LINE_FEED.Hexadecimal, offset);

            int idx_min = idx.Min();

            string line = lineHexToString(stream_hexa.Substring(offset, idx_min - offset));

            string LP = delimiter_characters.LEFT_PARENTHESIS.Glyph;
            string LT = delimiter_characters.LESS_THAN_SIGN.Glyph;
            string SL = delimiter_characters.SOLIDUS.Glyph;
            string LS = delimiter_characters.LEFT_SQUARE_BRACKET.Glyph;

            char LP_c = LP[0];
            char LT_c = LT[0];
            char SL_c = SL[0];
            char LS_c = LS[0];

            string keyword_stream = "stream";
            string keyword_null = "null";
            string keyword_obj = "obj";

            int kindOfObject = -1;

            string[] keys = line.Split(' ');

            string firstKey = keys[0];

            Boolean isNumeric = float.TryParse(keys[0], out float nfloat);

            if ((isNumeric == false) && ((String.Compare(firstKey, "true") == 0) || (String.Compare(firstKey, "false") == 0)))
            {
                kindOfObject = globals.Kinds_of_Objects.Boolean; //7.3.2 Boolean Objects
            }
            else if (isNumeric)
            {
                kindOfObject = globals.Kinds_of_Objects.Numeric; //7.3.3 Numeric Objects
            }
            else if ((isNumeric == false) && (firstKey[0] == LP_c) || ((isNumeric == false) && (firstKey[0] == LT_c) && (firstKey[1] != LT_c)))
            {
                kindOfObject = globals.Kinds_of_Objects.String; //7.3.4 String Objects
            }
            else if ((isNumeric == false) && (firstKey[0] == SL_c))
            {
                kindOfObject = globals.Kinds_of_Objects.Name; //7.3.5 Name Objects
            }
            else if ((isNumeric == false) && (firstKey[0] == LS_c))
            {
                kindOfObject = globals.Kinds_of_Objects.Array; //7.3.6 Array Objects
            }
            else if ((isNumeric == false) && (firstKey[0] == LT_c) && (firstKey[1] == LT_c))
            {
                kindOfObject = globals.Kinds_of_Objects.Dictionary; //7.3.7 Dictionary Objects
            }
            else if ((isNumeric == false) && (String.Compare(firstKey, keyword_stream) == 0))
            {
                kindOfObject = globals.Kinds_of_Objects.Stream; //7.3.8 Stream Objects
            }
            else if ((isNumeric == false) && (String.Compare(firstKey, keyword_null) == 0))
            {
                kindOfObject = globals.Kinds_of_Objects.Null; //7.3.9 Null Object
            }
            else if ((isNumeric == true) && (String.Compare(keys[2], keyword_obj) == 0))
            {
                kindOfObject = globals.Kinds_of_Objects.Indirect; //7.3.10 Indirect Objects
            }

            if (kindOfObject == globals.Kinds_of_Objects.Dictionary)
            {
                int idx_lts = stream_string.IndexOf(LT_c);
                int idx_l_feed = stream_string.IndexOf(white_space_characters.SPACE.Glyph,idx_lts + 3);
                string tmp = stream_string.Substring(idx_lts + 3, idx_l_feed - idx_lts - 3);

                if (String.Compare(tmp,"/FunctionType") == 0)
                {
                    kindOfObject = globals.Kinds_of_Objects.Functions;
                }
            }

            return kindOfObject;
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
            idx[15] = stream.IndexOf(white_space_characters.NULL.Hexadecimal, offset);
            idx[16] = stream.IndexOf(white_space_characters.SPACE.Hexadecimal, offset);

            int remove = -1;

            idx = idx.Where(val => val != remove).ToArray();

            int my_idx = idx.Min();

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

            int my_idx = idx.Min();

            string my_keys_hexa = stream.Substring(offset, my_idx - offset);

            string my_keys_string = lineHexToString(my_keys_hexa);

            string[] keys = my_keys_string.Split(' ');

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

        public void getFunctionInDictionaryHex(ref PDF_Function value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
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

            PDF_Function pdf_functions = new PDF_Function();

            value = pdf_functions;

        }

        public void getFunctionArrayInDictionaryHex(ref List<PDF_Function> value, ref string stream, int offset, int keyLength, ref int ERROR_ID, ref string ERROR_MSG)
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

            List<PDF_Function> pdf_functions = new List<PDF_Function>();

            value = pdf_functions;

        }

        public string getOBJstream(ref string stream_hexa, int offset)
        {
            
            WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

            string begin_obj = lineStringToHex("obj");

            string end_obj = lineStringToHex("endobj");

            int begin_idx = stream_hexa.IndexOf(begin_obj, offset);

            int end_idx = stream_hexa.IndexOf(end_obj, offset);

            string obj_stream_hexa = stream_hexa.Substring(begin_idx + begin_obj.Length + 1, end_idx - (begin_idx + begin_obj.Length + 1));

            obj_stream_hexa = obj_stream_hexa.TrimStart(white_space_characters.LINE_FEED.Hexadecimal.ToCharArray());
            obj_stream_hexa = obj_stream_hexa.TrimEnd(white_space_characters.LINE_FEED.Hexadecimal.ToCharArray());

            obj_stream_hexa = obj_stream_hexa.TrimStart(white_space_characters.CARRIAGE_RETURN.Hexadecimal.ToCharArray());
            obj_stream_hexa = obj_stream_hexa.TrimEnd(white_space_characters.CARRIAGE_RETURN.Hexadecimal.ToCharArray());

            return obj_stream_hexa;
        }



    }

}
