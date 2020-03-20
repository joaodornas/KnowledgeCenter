using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfPlugin
{
    //14 Document Interchange
    public class Document_Interchange
    {

        /*14.1 General
        *
        *The features described in this clause do not affect the final appearance of a document. 
        *Rather, these features enable a document to include higher-level information that is useful for the interchange of documents among conforming products:
        *
        *  •   Procedure sets(14.2, “Procedure Sets”) that define the implementation of PDF operators
        *
        *  •   Metadata(14.3, “Metadata”) consisting of general information about a document or a component of a document, such as its title, author, and creation and modification dates
        *
        *  •   File identifiers(14.4, “File Identifiers”) for reliable reference from one PDF file to another
        *
        *  •   Page-piece dictionaries(14.5, “Page-Piece Dictionaries”) allowing a conforming product to embed private data in a PDF document for its own use
        *
        *  •   Marked-content operators(14.6, “Marked Content”) for identifying portions of a content stream and associating them with additional properties or externally specified objects
        *
        *  •   Logical structure facilities(14.7, “Logical Structure”) for imposing a hierarchical organization on the content of a document
        *
        *  •   Tagged PDF(14.8, “Tagged PDF”), a set of conventions for using the marked content and logical structure facilities to facilitate the extraction and reuse of a document’s content for other purposes
        *
        *  •   Various ways of increasing the accessibility of a document to users with disabilities(14.9, “Accessibility Support”), including the identification of the natural language in which it is written(such as English or Spanish) for the benefit of a text-to-speech engine
        *
        *  •   The Web Capture extension (14.10, “ Web Capture”), which creates PDF files from Internet-based or locally resident HTML, PDF, GIF, JPEG, and ASCII text files
        *
        *  •   Facilities supporting prepress production workflows(14.11, “Prepress Support”), such as the specification of page boundaries and the generation of printer’s marks, colour separations, output intents, traps, and low-resolution proxies for high-resolution images
        */

        //14.2 Procedure_Sets
        public class Procedure_Sets
        {
            /*14.2 Procedure_Sets
            *The PDF operators used in content streams are grouped into categories of related operators called procedure sets (see Table 314). 
            *Each procedure set corresponds to a named resource containing the implementations of the operators in that procedure set. 
            *The ProcSet entry in a content stream’s resource dictionary (see 7.8.3, “Resource Dictionaries”) shall hold an array consisting of the names of the procedure sets used in that content stream. 
            *These procedure sets shall be used only when the content stream is printed to a PostScript output device. 
            *The names identify PostScript procedure sets that shall be sent to the device to interpret the PDF operators in the content stream. Each element of this array shall be one of the predefined names shown in Table 314.
            *
            *Table 314 - Predefined procedure set
            *
            *          [Name]              [Category of operators]
            *
            *          PDF                 Painting and graphics state
            *
            *          Text                Text
            *
            *          ImageB              Grayscale images or image masks
            *
            *          ImageC              Colour images
            *
            *          ImageI              Indexed (colour-table) images
            *
            *Beginning with PDF 1.4, this feature is considered obsolete. For compatibility with existing conforming readers, conforming writers should continue to specify procedure sets (preferably, all of those listed in Table 314 unless it is known that fewer are needed). 
            *However, conforming readers should not depend on the correctness of this information.
            *
            */

        }

        //14.3 Metadata
        public class Metadata
        {
            /*14.3.1 General
            *
            *A PDF document may include general information, such as the document’s title, author, and creation and modification dates.
            *Such global information about the document (as opposed to its content or structure) is called metadata and is intended to assist in cataloguing and searching for documents in external databases.Beginning with PDF 1.4, metadata may also be specified for individual components of a document.
            *
            *Metadata may be stored in a PDF document in either of the following ways:
            *
            *  •   In a metadata stream (PDF 1.4) associated with the document or a component of the document(14.3.2, “Metadata Streams”)
            *
            *  •   In a document information dictionary associated with the document(14.3.3, “Document Information Dictionary”)
            *
            *NOTE  Document information dictionaries is the original way that metadata was included in a PDF file.
            *      Metadata streams were introduced in PDF 1.4 and is now the preferred method to include metadata.
            */

            /*14.3.2 Metadata Streams
            *
                *
                *Metadata, both for an entire document and for components within a document, may be stored in PDF streams called metadata streams(PDF 1.4).
                *
                *NOTE 1    Metadata streams have the following advantages over the document information dictionary:
                *
                *  •   PDF - based workflows often embed metadata - bearing artwork as components within larger documents.Metadata streams provide a standard way of preserving the metadata of these components for examination downstream. 
                *      PDF - aware conforming products should be able to derive a list of all metadata - bearing document components from the PDF document itself.
                *
                *  •   PDF documents are often made available on the Web or in other environments, where many tools routinely examine, catalogue, and classify documents.
                *      These tools should be able to understand the self - contained description of the document even if they do not understand PDF.
                *
                *Besides the usual entries common to all stream dictionaries(see Table 5), the metadata stream dictionary shall contain the additional entries listed in Table 315.
                *
                *The contents of a metadata stream shall be the metadata represented in Extensible Markup Language(XML).
                *
                *NOTE 2    This information is visible as plain text to tools that are not PDF - aware only if the metadata stream is both unfiltered and unencrypted.
                *
                *Table 315 - Additional entries in a metadata stream dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Required) The type of PDF object that this dictionary describes; shall be Metadata for a metadata stream.
                *
                *          Subtype             name                (Required) The type of metadata stream that this dictionary describes; shall be XML.
                *
                *NOTE 3        The format of the XML representing the metadata is defined as part of a framework called the Extensible Metadata Platform (XMP) and described in the Adobe document XMP: Extensible Metadata Platform (see the Bibliography). 
                *              This framework provides a way to use XML to represent metadata describing documents and their components and is intended to be adopted by a wider class of products than just those that process PDF. 
                *              It includes a method to embed XML data within non-XML data files in a platform-independent format that can be easily located and accessed by simple scanning rather than requiring the document file to be parsed.
                *
                *A metadata stream may be attached to a document through the Metadata entry in the document catalogue (see 7.7.2, “Document Catalog”). 
                *The metadata framework provides a date stamp for metadata expressed in the framework. 
                *If this date stamp is equal to or later than the document modification date recorded in the document information dictionary, the metadata stream shall be taken as authoritative. 
                *If, however, the document modification date recorded in the document information dictionary is later than the metadata stream’s date stamp, the document has likely been saved by a writer that is not aware of metadata streams. 
                *In this case, information stored in the document information dictionary shall be taken to override any semantically equivalent items in the metadata stream. 
                *In addition, PDF document components represented as a stream or dictionary may have a Metadata entry (see Table 316).
                *
                *Table 316 - Additional entry for components having metadata
                *
                *          [Key]               [Type]              [Value]
                *
                *          Metadata            stream              (Optional; PDF 1.4) A metadata stream containing metadata for the component.
                *
                *In general, any PDF stream or dictionary may have metadata attached to it as long as the stream or dictionary represents an actual information resource, as opposed to serving as an implementation artifact. 
                *Some PDF constructs are considered implementational, and hence may not have associated metadata.
                *
                *When there is ambiguity about exactly which stream or dictionary may bear the Metadata entry, the metadata shall be attached as close as possible to the object that actually stores the data resource described.
                *
                *NOTE 4        Metadata describing a tiling pattern should be attached to the pattern stream’s dictionary, but a shading should have metadata attached to the shading dictionary rather than to the shading pattern dictionary that refers to it.
                *              Similarly, metadata describing an ICCBased colour space should be attached to the ICC profile stream describing it, and metadata for fonts should be attached to font file streams rather than to font dictionaries.
                *
                *NOTE 5        In tables describing document components in this specification, the Metadata entry is listed only for those in which it is most likely to be used.
                *              Keep in mind, however, that this entry may appear in other components represented as streams or dictionaries.
                *
                *  •   In addition, metadata may also be associated with marked content within a content stream.
                *      This association shall be created by including an entry in the property list dictionary whose key shall beMetadata and whose value shall be the metadata stream dictionary.Because this construct refers to an object outside the content stream, the property list is referred to indirectly as a named resource(see 14.6.2, “Property Lists”).
                *
                */

            /*14.3.3 Document Information Dictionary
            *
                *The optional Info entry in the trailer of a PDF file (see 7.5.5, “File Trailer”) shall hold a document information dictionary containing metadata for the document; Table 317 shows its contents. 
                *Any entry whose value is not known should be omitted from the dictionary rather than included with an empty string as its value.
                *
                *Some conforming readers may choose to permit searches on the contents of the document information dictionary. 
                *To facilitate browsing and editing, all keys in the dictionary shall be fully spelled out, not abbreviated. 
                *New keys should be chosen with care so that they make sense to users.
                *
                *The value associated with any key not specifically mentioned in Table 317 shall be a text string.
                *
                *Although conforming readers may store custom metadata in the document information dictionary, they may not store private content or structural information there.
                *Such information shall be stored in the document catalogue instead(see 7.7.2, “Document Catalog”).
                *
                *Table 317 - Entries in the document information dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Title               text string         (Optional; PDF 1.1) The document’s title.
                *
                *          Author              text string         (Optional) The name of the person who created the document.
                *
                *          Subject             text string         (Optional; PDF 1.1) The subject of the document.
                *
                *          Keywords            text string         (Optional; PDF 1.1) Keywords associated with the document.
                *
                *          Creator             text string         (Optional) If the document was converted to PDF from another format, the name of the conforming product that created the original document from which it was converted.
                *
                *          Producer            text string         (Optional) If the document was converted to PDF from another format, the name of the conforming product that converted it to PDF.
                *
                *          CreationDate        date                (Optional) The date and time the document was created, in human-readable form (see 7.9.4, “Dates”).
                *
                *          ModDate             date                (Required if PieceInfo is present in the document catalogue; otherwise optional; PDF 1.1) 
                *                                                  The date and time the document was most recently modified, in human-readable form (see 7.9.4, “Dates”).
                *
                *          Trapped             name                (Optional; PDF 1.3) A name object indicating whether the document has been modified to include trapping information (see 14.11.6, “Trapping Support”):
                *
                *                                                  True            The document has been fully trapped; no further trapping shall be needed. 
                *                                                                  This shall be the name True, not the boolean value true.
                *
                *                                                  False           The document has not yet been trapped.
                *                                                                  This shall be the name False, not the boolean value false.
                *
                *                                                  Unknown         Either it is unknown whether the document has been trapped or it has been partly but not yet fully trapped; some additional trapping may still be needed.
                *
                *                                                  Default value: Unknown.
                *      
                *                                                  NOTE            The value of this entry may be set automatically by the software creating the document’s trapping information, or it may be known only to a human operator and entered manually.
                *
                *
                *EXAMPLE       This example shows a typical document information dictionary.
                *
                *              1 0 obj
                *                  << / Title(PostScript Language Reference, Third Edition)
                *                     / Author(Adobe Systems Incorporated)
                *                     / Creator(Adobe FrameMaker 5.5.3 for Power Macintosh®)
                *                     / Producer(Acrobat Distiller 3.01 for Power Macintosh)
                *                     / CreationDate(D: 19970915110347 - 08'00')
                *                     / ModDate(D: 19990209153925 - 08'00')
                *                  >>
                *              endobj
                */


        }

        //14.4 File Identifiers
        public class File_Identifiers
        {
            /*14.4 File Identifiers
            *PDF files may contain references to other PDF files (see 7.11, “File Specifications”). 
            *Simply storing a file name, however, even in a platform-independent format, does not guarantee that the file can be found. 
            *Even if the file still exists and its name has not been changed, different server software applications may identify it in different ways. 
            *Servers running on DOS platforms convert all file names to 8 characters and a 3-character extension. 
            *Different servers may use different strategies for converting longer file names to this format.
            *
            *External file references may be made more reliable by including a file identifier(PDF 1.1) in the file and using it in addition to the normal platform-based file designation.
            *Matching the identifier in the file reference with the one in the file confirms whether the correct file was found.
            *
            *File identifiers shall be defined by the optional ID entry in a PDF file’s trailer dictionary(see 7.5.5, “File Trailer”). 
            *The ID entry is optional but should be used.The value of this entry shall be an array of two byte strings. 
            *The first byte string shall be a permanent identifier based on the contents of the file at the time it was originally created and shall not change when the file is incrementally updated. 
            *The second byte string shall be a changing identifier based on the file’s contents at the time it was last updated.When a file is first written, both identifiers shall be set to the same value.
            *If both identifiers match when a file reference is resolved, it is very likely that the correct and unchanged file has been found.
            *If only the first identifier matches, a different version of the correct file has been found.
            *
            *To help ensure the uniqueness of file identifiers, they should be computed by means of a message digest algorithm such as MD5 (described in Internet RFC 1321, The MD5 Message-Digest Algorithm; see the Bibliography), using the following information:
            *
            *  •   The current time
            *
            *  •   A string representation of the file’s location, usually a pathname
            *
            *  •   The size of the file in bytes
            *
            *  •   The values of all entries in the file’s document information dictionary(see 14.3.3, “Document Information Dictionary”)
            *
            *NOTE      The calculation of the file identifier need not be reproducible; all that matters is that the identifier is likely to be unique.
            *          For example, two implementations of the preceding algorithm might use different formats for the current time, causing them to produce different file identifiers for the same file created at the same time, but the uniqueness of the identifier is not affected.
            */
        }

        //14.5 Page-Piece Dictionaries
        public class Page_Piece_Dictionaries
        {
            /* 14.5 Page-Piece Dictionaries
            *A page-piece dictionary (PDF 1.3) may be used to hold private conforming product data. 
            *The data may be associated with a page or form XObject by means of the optional PieceInfo entry in the page object (see Table 30) or form dictionary (see Table 95). 
            *Beginning with PDF 1.4, private data may also be associated with the PDF document by means of the PieceInfo entry in the document catalogue (see Table 28).
            *
            *NOTE 1    Conforming products may use this dictionary as a place to store private data in connection with that document, page, or form.
            *          Such private data can convey information meaningful to the conforming product that produces it(such as information on object grouping for a graphics editor or the layer information used by Adobe Photoshop®) but may be ignored by general-purpose conforming readers.
            *
            *As Table 318 shows, a page-piece dictionary may contain any number of entries, each keyed by the name of a distinct conforming product or of a well-known data type recognized by a family of conforming products.
            *The value associated with each key shall be a data dictionary containing the private data that shall be used by the conforming product.
            *The Private entry may have a value of any data type, but typically it is a dictionary containing all of the private data needed by the conforming product other than the actual content of the document, page, or form.
            *
            *Table 318 - Entries in a page-piece dictionary
            *
            *          [Key]                               [Type]              [Value]
            *
            *          any conforming                      dictionary          A data dictionary (see Table 319)
            *          product name or well-
            *          known data type
            *
            *
            *Table 319 - Entries in an data dictionary
            *
            *          [Key]                               [Type]              [Value]
            *
            *          LastModified                        date                (Required) The date and time when the contents of the document, page, or form were most recently modified by this conforming product.
            *
            *          Private                             (any)               (Optional) Any private data appropriate to the conforming product, typically in the form of a dictionary.
            *
            *
            *The LastModified entry indicates when this conforming product last altered the content of the page or form. 
            *If the page-piece dictionary contains several data dictionaries, their modification dates can be compared with those in the corresponding entry of the page object or form dictionary (see Table 30 and Table 95), or the ModDate entry of the document information dictionary (see Table 317), to ascertain which data dictionary corresponds to the current content of the page or form. 
            *Because some platforms may use only an approximate value for the date and time or may not deal correctly with differing time zones, modification dates shall becompared only for equality and not for sequential ordering.
            *
            *NOTE 2        It is possible for two or more data dictionaries to have the same modification date.
            *              Conforming products can use this capability to define multiple or extended versions of the same data format.
            *              For example, suppose that earlier versions of a conforming product use an data dictionary named PictureEdit, and later versions of the same conforming product extend the data to include additional items not previously used. 
            *              The original data could continue to be kept in the PictureEdit dictionary and the additional items placed in a new dictionary named PictureEditExtended.
            *              This allows the earlier versions of the conforming product to continue to work as before, and later versions are able to locate and use the extended data items.
            */

           
        }

        //14.6 Marked Content
        public class Marked_Content
        {
            /*14.6.1 General
            *
            *Marked-content operators(PDF 1.2) may identify a portion of a PDF content stream as a marked-content element of interest to a particular conforming product.
            *Marked-content elements and the operators that mark them shall fall into two categories:
            *
            *  •   The MP and DP operators shall designate a single marked-content point in the content stream.
            *
            *  •   The BMC, BDC, and EMC operators shall bracket a marked-content sequence of objects within the content stream.
            *
            *NOTE 1    This is a sequence not simply of bytes in the content stream but of complete graphics objects. 
            *          Each object is fully qualified by the parameters of the graphics state in which it is rendered.
            *
            *NOTE 2    A graphics application, for example, might use marked content to identify a set of related objects as a group to be processed as a single unit.
            *          A text-processing application might use it to maintain a connection between a footnote marker in the body of a document and the corresponding footnote text at the bottom of the page. 
            *          The PDF logical structure facilities use marked-content sequences to associate graphical content with structure elements (see 14.7.4, “Structure Content”). 
            *          Table 320 summarizes the marked-content operators.
            *
            *All marked-content operators except EMC shall take a tag operand indicating the role or significance of the marked-content element to the conforming reader. 
            *All such tags shall be registered with Adobe Systems (see Annex E) to avoid conflicts between different applications marking the same content stream. 
            *In addition to the tag operand, the DP and BDC operators shall specify a property list containing further information associated with the marked content. 
            *Property lists are discussed further in 14.6.2, “Property Lists.”
            *
            *Marked-content operators may appear only between graphics objects in the content stream.
            *They may not occur within a graphics object or between a graphics state operator and its operands.
            *Marked-content sequences may be nested one within another, but each sequence shall be entirely contained within a single content stream.
            *
            *NOTE 3        A marked-content sequence may not cross page boundaries.
            *
            *NOTE 4        The Contents entry of a page object (see 7.7.3.3, “Page Objects”), which may be either a single stream or an array of streams, is considered a single stream with respect to marked-content sequences.
            *
            *Table 320 - Marked-content operators
            *
            *          [Operands]              [Operator]                  [Description]
            *
            *          tag                     MP                          Designate a marked-content point. tag shall be a name object indicating the role or significance of the point.
            *
            *          tag properties          DP                          Designate a marked-content point with an associated property list. 
            *                                                              tagshall be a name object indicating the role or significance of the point. 
            *                                                              properties shall be either an inline dictionary containing the property list or a name object associated with it in the Properties subdictionary of the current resource dictionary (see 14.6.2, “Property Lists”).
            *
            *          tag                     BMC                         Begin a marked-content sequence terminated by a balancing EMCoperator. 
            *                                                              tag shall be a name object indicating the role or significance of the sequence.
            *
            *          tag properties          BDC                         Begin a marked-content sequence with an associated property list, terminated by a balancing EMC operator. 
            *                                                              tag shall be a name object indicating the role or significance of the sequence. 
            *                                                              properties shall be either an inline dictionary containing the property list or a name object associated with it in the Properties subdictionary of the current resource dictionary (see 14.6.2, “Property Lists”).
            *
            *          --                      EMC                         End a marked-content sequence begun by a BMC or BDC operator.
            *
            *When the marked-content operators BMC, BDC, and EMC are combined with the text object operators BT and ET (see 9.4, “Text Objects”), each pair of matching operators (BMC…EMC, BDC…EMC, or BT…ET) shall be properly (separately) nested. 
            *Therefore, the sequences
            *
            *                      BMC                         BT
            *                          BT                          BMC
            *                                      …and…
            *                          ET                          EMC
            *                      EMC                         ET
            *
            *are valid, but
            *
            *                      BMC                         BT
            *                          BT                          BMC
            *                                      …and…
            *                          EMC                     ET
            *                      BT                          EMC
            *
            *are not valid.
            */

            /*14.6.2 Property Lists
            *
                *The marked-content operators DP and BDC associate a property list with a marked-content element within a content stream. 
                *The property list is a dictionary containing private information meaningful to the conforming writer creating the marked content. 
                *Conforming products should use the dictionary entries in a consistent way; the values associated with a given key should always be of the same type (or small set of types).
                *If all of the values in a property list dictionary are direct objects, the dictionary may be written inline in the content stream as a direct object.
                *If any of the values are indirect references to objects outside the content stream, the property list dictionary shall be defined as a named resource in the Properties subdictionary of the current resource dictionary(see 7.8.3, “Resource Dictionaries”) and referenced by name as the propertiesoperand of the DP or BDC operator.
            */

            /*14.6.3 Marked Content and Clipping
            *
                *Some PDF path and text objects are defined purely for their effect on the current clipping path, without the objects actually being painted on the page. 
                *This occurs when a path object is defined using the operator sequence W n or W* n (see 8.5.4, “Clipping Path Operators”) or when a text object is painted in text rendering mode 7 (see 9.3.6, “Text Rendering Mode”). 
                *Such clipped, unpainted path or text objects are called clipping objects. When a clipping object falls within a marked-content sequence, it shall not be considered part of the sequence unless the entire sequence consists only of clipping objects. 
                *In Example 1, for instance, the marked-content sequence tagged Clip includes the text string (Clip me) but not the rectangular path that defines the clipping boundary.
                *
                *EXAMPLE 1             / Clip BMC
                *                          100 100 10 10 re W n                        % Clipping path
                *                          (Clip me) Tj                                % Object to be clipped
                *                      EMC
                *
                *Only when a marked-content sequence consists entirely of clipping objects shall the clipping objects be considered part of the sequence. 
                *In this case, the sequence is known as a marked clipping sequence.Such sequences may be nested.
                *In Example 2, for instance, multiple lines of text are used to clip a subsequent graphics object(in this case, a filled path).Each line of text shall be bracketed within a separate marked clipping sequence, tagged Pgf.
                *The entire series shall be bracketed in turn by an outer marked clipping sequence, tagged Clip.
                *
                *NOTE      The marked - content sequence tagged ClippedText is not a marked clipping sequence, since it contains a filled rectangular path that is not a clipping object.
                *          The clipping objects belonging to the Clip and Pgf sequences are therefore not considered part of the ClippedText sequence.
                *
                *EXAMPLE 2             /ClippedText BMC
                *                          / Clip <<…>>
                *                              BDC
                *                                  BT
                *                                      7 Tr                            % Begin text clip mode
                *                                      / Pgf BMC
                *                                          (Line 1) Tj
                *                                      EMC
                *                                      / Pgf BMC
                *                                          (Line) '
                *                                          (2) Tj
                *                                      EMC
                *                                  ET                                  % Set current text clip
                *                              EMC
                *                          100 100 10 10 re f% Filled path
                *                      EMC
                *
                *The precise rules governing marked clipping sequences shall be as follows:
                *
                *  •   A clipping object shall be a path object ended by the operator sequence W n or W* n or a text object painted in text rendering mode 7.
                *
                *  •   An invisible graphics object shall be a path object ended by the operator n only (with no preceding W or W*) or a text object painted in text rendering mode 3.
                *
                *  •   A visible graphics object shall be a path object ended by any operator other than n, a text object painted in any text rendering mode other than 3 or 7, or any XObject invoked by the Do operator.
                *
                *  •   An empty marked - content element shall be a marked - content point or a marked-content sequence that encloses no graphics objects.
                *
                *  •   A marked clipping sequence shall be a marked-content sequence that contains at least one clipping object and no visible graphics objects.
                *
                *  •   Clipping objects and marked clipping sequences shall be considered part of an enclosing marked-content sequence only if it is a marked clipping sequence.
                *
                *  •   Invisible graphics objects and empty marked-content elements shall always be considered part of an enclosing marked-content sequence, regardless of whether it is a marked clipping sequence.
                *
                *  •   The q(save) and Q(restore) operators may not occur within a marked clipping sequence.
                *
                *Example 3 illustrates the application of these rules. Marked-content sequence S4 is a marked clipping sequence because it contains a clipping object (clipping path 2) and no visible graphics objects. 
                *Clipping path 2 is therefore considered part of sequence S4. 
                *Marked -content sequences S1, S2, and S3 are not marked clipping sequences, since they each include at least one visible graphics object. 
                *Thus, clipping paths 1 and 2 are not part of any of these three sequences.
                *
                *EXAMPLE 3         / S1 BMC
                *                      / S2 BMC
                *                          / S3 BMC
                *                              0 0 m
                *                              100 100 l
                *                              0 100 l W n                             % Clipping path 1
                *                              0 0 m
                *                              200 200 l
                *                              0 100 l f                               % Filled path
                *                          EMC
                *                          / S4 BMC
                *                              0 0 m
                *                              300 300 l
                *                              0 100 l W n                             % Clipping path 2
                *                          EMC
                *                      EMC
                *                      100 100 10 10 re f                              % Filled path
                *                  EMC
                *
                *In Example 4 marked-content sequence S1 is a marked clipping sequence because the only graphics object it contains is a clipping path. 
                *Thus, the empty marked-content sequence S3 and the marked-content point P1 are both part of sequence S2, and S2, S3, and P1 are all part of sequence S1.
                *
                *EXAMPLE 4         / S1 BMC
                *                      …Clipping path…
                *                      / S2 BMC
                *                          / S3 BMC
                *                          EMC
                *                          / P1 DP
                *                      EMC
                *                  EMC
                *
                *In Example 5 marked-content sequences S1 and S4 are marked clipping sequences because the only object they contain is a clipping path. 
                *Hence the clipping path is part of sequences S1 and S4; S3 is part of S2; and S2, S3, and S4 are all part of S1.
                *
                *EXAMPLE 5         / S1 BMC
                *                      / S2 BMC
                *                         / S3 BMC
                *                         EMC
                *                      EMC
                *                      / S4 BMC
                *                          …Clipping path…
                *                      EMC
                *                  EMC
                */
            
      

        }

        //14.7 Logical Structure
        public class Logical_Structure
        {

            /*14.7.1 General
            *
            *PDF’s logical structure facilities(PDF 1.3) shall provide a mechanism for incorporating structural information about a document’s content into a PDF file.
            *Such information may include the organization of the document into chapters and sections or the identification of special elements such as figures, tables, and footnotes. 
            *The logical structure facilities shall be extensible, allowing conforming writers to choose what structural information to include and how to represent it, while enabling conforming readers to navigate a file without knowing the producer’s structural conventions.
            *
            *PDF logical structure shares basic features with standard document markup languages such as HTML, SGML, and XML. 
            *A document’s logical structure shall be expressed as a hierarchy of structure elements, each represented by a dictionary object. 
            *Like their counterparts in other markup languages, PDF structure elements may have content and attributes. 
            *In PDF, rendered document content takes over the role occupied by text in HTML, SGML, and XML.
            *
            *A PDF document’s logical structure shall be stored separately from its visible content, with pointers from each to the other.
            *This separation allows the ordering and nesting of logical elements to be entirely independent of the order and location of graphics objects on the document’s pages.
            *
            *The Markings entry in the document catalogue (see 7.7.2, “Document Catalog”) shall specify a mark information dictionary, whose entries are shown in Table 321. 
            *It provides additional information relevant to specialized uses of structured PDF documents.
            *
            *Table 321 - Entries in the mark information dictionary
            *
            *          [Key]               [Type]              [Value]
            *
            *          Marked              boolean             (Optional) A flag indicating whether the document conforms to Tagged PDF conventions (see 14.8, “Tagged PDF”). 
            *                                                  Default value: false.
            *                                                  If Suspects is true, the document may not completely conform to Tagged PDF conventions.
            *
            *          UserProperties      boolean             (Optional; PDF 1.6) A flag indicating the presence of structure elements that contain user properties attributes (see 14.7.5.4, “User Properties”). 
            *                                                  Default value: false.
            *
            *          Suspects            boolean             (Optional; PDF 1.6) A flag indicating the presence of tag suspects (see 14.8.2.3, “Page Content Order”). 
            *                                                  Default value: false.
            *
            */

            /*14.7.2 Structure Hierarchy
            *
                *The logical structure of a document shall be described by a hierarchy of objects called the structure hierarchyor structure tree. 
                *At the root of the hierarchy shall be a dictionary object called the structure tree root, located by means of the StructTreeRoot entry in the document catalogue (see 7.7.2, “Document Catalog”). 
                *Table 322 shows the entries in the structure tree root dictionary. 
                *The K entry shall specify the immediate children of the structure tree root, which shall be structure elements.
                *
                *Structure elements shall be represented by a dictionary, whose entries are shown in Table 323.The K entry shall specify the children of the structure element, which may be zero or more items of the following kinds:
                *
                *  •   Other structure elements
                *
                *  •   References to content items, which are either marked - content sequences(see 14.6, “Marked Content”) or complete PDF objects such as XObjects and annotations. 
                *These content items represent the graphical content, if any, associated with a structure element.Content items are discussed in detail in 14.7.4, “Structure Content.”
                *
                *Table 322 - Entries in the structure tree root
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Required) The type of PDF object that this dictionary describes; shall be StructTreeRoot for a structure tree root.
                *
                *          K                   dictionary          (Optional) The immediate child or children of the structure tree root in the structure hierarchy. 
                *                              or array            The value may be either a dictionary representing a single structure element or an array of such dictionaries.
                *      
                *          IDTree              name tree           (Required if any structure elements have element identifiers) A name tree that maps element identifiers (see Table 323) to the structure elements they denote.
                *
                *          ParentTree          number tree         (Required if any structure element contains content items) A number tree (see 7.9.7, “Number Trees”) used in finding the structure elements to which content items belong. 
                *                                                  Each integer key in the number tree shall correspond to a single page of the document or to an individual object (such as an annotation or an XObject) that is a content item in its own right. 
                *                                                  The integer key shall be the value of the StructParent or StructParents entry in that object (see 14.7.4.4, “Finding Structure Elements from Content Items”). 
                *                                                  The form of the associated value shall depend on the nature of the object:
                *                                                  For an object that is a content item in its own right, the value shall be an indirect reference to the object’s parent element(the structure element that contains it as a content item).
                *                                                  For a page object or content stream containing marked-content sequences that are content items, the value shall be an array of references to the parent elements of those marked - content sequences.
                *                                                  See 14.7.4.4, “Finding Structure Elements from Content Items” for further discussion.
                *
                *          ParentTreeNextKey   integer             (Optional) An integer greater than any key in the parent tree, shall be used as a key for the next entry added to the tree.
                *
                *          RoleMap             dictionary          (Optional) A dictionary that shall map the names of structure types used in the document to their approximate equivalents in the set of standard structure types (see 14.8.4, “Standard Structure Types”).
                *
                *          ClassMap            dictionary          (Optional) A dictionary that shall map name objects designating attribute classes to the corresponding attribute objects or arrays of attribute objects (see 14.7.5.2, “Attribute Classes”).
                *
                *
                *Table 323 - Entries in a structure element dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be StructElem for a structure element.
                *
                *          S                   name                (Required) The structure type, a name object identifying the nature of the structure element and its role within the document, such as a chapter, paragraph, or footnote (see 14.7.3, “Structure Types”). 
                *                                                  Names of structure types shall conform to the guidelines described in Annex E.
                *
                *          P                   dictionary          (Required; shall be an indirect reference) The structure element that is the immediate parent of this one in the structure hierarchy.
                *
                *          ID                  byte string         (Optional) The element identifier, a byte string designating this structure element. 
                *                                                  The string shall be unique among all elements in the document’s structure hierarchy. 
                *                                                  The IDTree entry in the structure tree root (see Table 322) defines the correspondence between element identifiers and the structure elements they denote.
                *
                *          Pg                  dictionary          (Optional; shall be an indirect reference) A page object representing a page on which some or all of the content items designated by the K entry shall be rendered.
                *
                *          K                   (various)           (Optional) The children of this structure element. 
                *                                                  The value of this entry may be one of the following objects or an array consisting of one or more of the following objects:
                *
                *                                                  •   A structure element dictionary denoting another structure element
                *
                *                                                  •   An integer marked - content identifier denoting a marked-content sequence
                *                          
                *                                                  •   A marked-content reference dictionary denoting a marked - content sequence
                *
                *                                                  •   An object reference dictionary denoting a PDF object
                *
                *                                                  Each of these objects other than the first(structure element dictionary) shall be considered to be a content item; see 14.7.4, “Structure Content” for further discussion of each of these forms of representation.
                *                                                  If the value of K is a dictionary containing no Type entry, it shall be assumed to be a structure element dictionary.
                *
                *          A                   (various)           (Optional) A single attribute object or array of attribute objects associated with this structure element. 
                *                                                  Each attribute object shall be either a dictionary or a stream. 
                *                                                  If the value of this entry is an array, each attribute object in the array may be followed by an integer representing its revision number (see 14.7.5, “Structure Attributes,” and 14.7.5.3, “Attribute Revision Numbers”).
                *
                *          C                   name or array       (Optional) An attribute class name or array of class names associated with this structure element. 
                *                                                  If the value of this entry is an array, each class name in the array may be followed by an integer representing its revision number (see 14.7.5.2, “Attribute Classes,” and 14.7.5.3, “Attribute Revision Numbers”).
                *                                                  If both the A and C entries are present and a given attribute is specified by both, the one specified by the A entry shall take precedence.
                *
                *          R                   integer             (Optional) The current revision number of this structure element (see 14.7.5.3, “Attribute Revision Numbers”). 
                *                                                  The value shall be a non-negative integer. 
                *                                                  Default value: 0.
                *
                *          T                   text string         (Optional) The title of the structure element, a text string representing it in human-readable form. 
                *                                                  The title should characterize the specific structure element, such as Chapter 1, rather than merely a generic element type, such as Chapter.
                *
                *          Lang                text string         (Optional; PDF 1.4) A language identifier specifying the natural language for all text in the structure element except where overridden by language specifications for nested structure elements or marked content (see 14.9.2, “Natural Language Specification”). 
                *                                                  If this entry is absent, the language (if any) specified in the document catalogue applies.
                *
                *          Alt                 text string         (Optional) An alternate description of the structure element and its children in human-readable form, which is useful when extracting the document’s contents in support of accessibility to users with disabilities or for other purposes (see 14.9.3, “Alternate Descriptions”).
                *
                *          E                   text string         (Optional; PDF 1.5) The expanded form of an abbreviation.
                *
                *          ActualText          text string         (Optional; PDF 1.4) Text that is an exact replacement for the structure element and its children. 
                *                                                  This replacement text (which should apply to as small a piece of content as possible) is useful when extracting the document’s contents in support of accessibility to users with disabilities or for other purposes (see 14.9.4, “Replacement Text”).
                *
                */

             
            /*14.7.3 Structure Types
            *
                *Every structure element shall have a structure type, a name object that identifies the nature of the structure element and its role within the document (such as a chapter, paragraph, or footnote). 
                *To facilitate the interchange of content among conforming products, PDF defines a set of standard structure types; see 14.8.4, “Standard Structure Types.” 
                *Conforming products are not required to adopt them, however, and may use any names for their structure types.
                *
                *Where names other than the standard ones are used, a role map may be provided in the structure tree root, mapping the structure types used in the document to their nearest equivalents in the standard set.
                *
                *NOTE 1        A structure type named Section used in the document might be mapped to the standard type Sect. 
                *              The equivalence need not be exact; the role map merely indicates an approximate analogy between types, allowing conforming products to share nonstandard structure elements in a reasonable way.
                *
                *NOTE 2        The same structure type may occur as both a key and a value in the role map, and circular chains of association are explicitly permitted. 
                *              Therefore, a single role map may define a bidirectional mapping.A conforming reader using the role map should follow the chain of associations until it either finds a structure type it recognizes or returns to one it has already encountered.
                *
                *NOTE 3        In PDF versions earlier than 1.5, standard element types were never remapped. 
                *              Beginning with PDF 1.5, an element name shall always be mapped to its corresponding name in the role map, if there is one, even if the original name is one of the standard types.
                *              This shall be done to allow the element, for example, to represent a tag with the same name as a standard role, even though its use differs from the standard role.
                */

            /*14.7.4 Structure Content
            */

                /*14.7.4.1 General
            *
            *Any structure element may have associated graphical content, consisting of one or more content items. Content items shall be graphical objects that exist in the document independently of the structure tree but are associated with structure elements as described in the following sub - clauses.
            *Content items are of two kinds:
            *
            *  •   Marked - content sequences within content streams(see 14.7.4.2, “Marked - Content Sequences as Content Items”)
            *
            *  •   Complete PDF objects such as annotations and XObjects(see 14.7.4.3, “PDF Objects as Content Items”)
            *
            *The K entry in a structure element dictionary(see Table 323) shall specify the children of the structure element, which may include any number of content items, as well as child structure elements that may in turn have content items of their own.
            *
            *Content items shall be leaf nodes of the structure tree; that is, they may not have other content items nested within them for purposes of logical structure.
            *The hierarchical relationship among structure elements shall berepresented entirely by the K entries of the structure element dictionaries, not by nesting of the associated content items.
            *Therefore, the following restrictions shall apply:
            *
            *  •   A marked - content sequence delimiting a structure content item may not have another marked - content sequence for a content item nested within it though non - structural marked content shall be allowed.
            *
            *  •   A structure content item shall not invoke(with the Do operator) an XObject that is itself a structure content item.
            */

                /*14.7.4.2 Marked-Content Sequences as Content Items
            *
            *A sequence of graphics operators in a content stream may be specified as a content item of a structure element in the following way:
            *
            *  •   The operators shall be bracketed as a marked - content sequence between BDC and EMC operators(see 14.6, “Marked Content”). 
            *      Although the tag associated with a marked - content sequence is not directly related to the document’s logical structure, it should be the same as the structure type of the associated structure element.
            *
            *  •   The marked-content sequence shall have a property list(see 14.6.2, “Property Lists”) containing an MCIDentry, which i shall be an integer marked - content identifier that uniquely identifies the marked-content sequence within its content stream, as shown in the following example:
            *
            *EXAMPLE 1                 2 0 obj                                 % Page object
            *                              << / Type / Page
            *                                 / Contents 3 0 R % Content stream
            *                                  …
            *                              >>
            *                          endobj
            *                          3 0 obj                                 % Page's content stream
            *                              << / Length … >>
            *                          stream
            *                              …
            *                              / P << / MCID 0 >>                  % Start of marked-content sequence
            *                                  BDC
            *                                      …
            *                                      (Here is some text) Tj
            *                                      …
            *                                  EMC                             % End of marked-content sequence
            *                              …
            *                          endstream
            *                          endobj
            *
            *NOTE      This example and the following examples omit required StructParents entries in the objects used as content items (see 14.7.4.4, “Finding Structure Elements from Content Items”).
            *
            *A structure element dictionary may include one or more marked-content sequences as content items by referring to them in its K entry(see Table 323).
            *This reference may have two forms:
            *
            *  •   A dictionary object called a marked-content reference.Table 324 shows the contents of this type of dictionary, which shall specify the marked - content identifier, as well other information identifying the stream in which the sequence is contained.
            *      Example 2 illustrates the use of a marked-content reference to the marked-content sequence shown in Example 3.
            *
            *  •   An integer that specifies the marked-content identifier.
            *      This may be done in the common case where the marked - content sequence is contained in the content stream of the page that is specified in the Pg entry of the structure element dictionary. 
            *      Example 3 shows a structure element that has three children: a marked-content sequence specified by a marked - content identifier, as well as two other structure elements.
            *
            *EXAMPLE 2                 1 0 obj                                 % Structure element
            *                              << / Type / StructElem
            *                                 / S / P                          % Structure type
            *                                 / P …                            % Parent in structure hierarchy
            *                                 / K << / Type / MCR
            *                                      / Pg 2 0 R                  % Page containing marked-content sequence
            *                                      / MCID 0                    % Marked - content identifier
            *                                      >>
            *                              >>
            *                          endobj
            *
            *Table 324 - Entries in a marked-content reference dictionary
            *
            *              [Key]               [Type]              [Value]
            *
            *              Type                name                (Required) The type of PDF object that this dictionary describes; shall be MCR for a marked-content reference.
            *
            *              Pg                  dictionary          (Optional; shall be an indirect reference) The page object representing the page on which the graphics objects in the marked-content sequence shall be rendered. 
            *                                                      This entry overrides any Pg entry in the structure element containing the marked-content reference; it shall be required if the structure element has no such entry.
            *
            *              Stm                 stream              (Optional; shall be an indirect reference) The content stream containing the marked-content sequence. 
            *                                                      This entry should be present only if the marked-content sequence resides in a content stream other than the content stream for the page (see 8.10, “Form XObjects” and 12.5.5, “Appearance Streams”).
            *                                                      If this entry is absent, the marked-content sequence shall be contained in the content stream of the page identified by Pg(either in the marked - content reference dictionary or in the parent structure element).
            *
            *              StmOwn              (any)               (Optional; shall be an indirect reference) The PDF object owning the stream identified by Stems annotation to which an appearance stream belongs.
            *
            *              MCID                integer             (Required) The marked-content identifier of the marked-content sequence within its content stream.
            *
            *
            *EXAMPLE 3                 1 0 obj                                 % Containing structure element
            *                              << / Type / StructElem
            *                                 / S / MixedContainer             % Structure type
            *                                 / P …                            % Parent in structure hierarchy
            *                                 /Pg 2 0 R                        % Page containing marked-content sequence
            *                                 / K[4 0 R                        % Three children: a structure element
            *                                      0                           % a marked - content identifier
            *                                      5 0 R                       % another structure element
            *                                    ]
            *                              >>
            *                          endobj
            *                          2 0 obj                                 % Page object
            *                              << / Type / Page
            *                                 / Contents 3 0 R % Content stream
            *                                  …
            *                              >>
            *                          endobj
            *                          3 0 obj                                 % Page's content stream
            *                              << / Length … >>
            *                          stream
            *                              …
            *                              / P << / MCID 0 >>                  % Start of marked-content sequence
            *                                  BDC
            *                                      (Here is some text) Tj
            *                                      …
            *                                  EMC % End of marked-content sequence
            *                              …
            *                          endstream
            *                          endobj
            *
            *Content streams other than page contents may also contain marked content sequences that are content items of structure elements. 
            *The content of form XObjects may be incorporated into structure elements in one of the following ways:
            *
            *  •   A Do operator that paints a form XObject may be part of a marked - content sequence that shall beassociated with a structure element(see Example 4). 
            *In this case, the entire form XObject shall beconsidered to be part of the structure element’s content, as if it were inserted into the marked - content sequence at the point of the Do operator. 
            *The form XObject shall not in turn contain any marked-content sequences associated with this or other structure elements.
            *
            *  •   The content stream of a form XObject may contain one or more marked - content sequences that shall beassociated with structure elements(see Example 5).
            *The form XObject may have arbitrary substructure, containing any number of marked-content sequences associated with logical structure elements. 
            *However, any Do operator that paints the form XObject should not be part of a logical structure content item.
            *
            *A form XObject that is painted with multiple invocations of the Do operator may be incorporated into the document’s logical structure only by the first method, with each invocation of Do individually associated with a structure element.
            *
            *EXAMPLE 4                     1 0 obj                                                                 % Structure element
            *                                  << / Type / StructElem
            *                                     / S / P                                                          % Structure type
            *                                     / P …                                                            % Parent in structure hierarchy
            *                                     / Pg 2 0 R                                                       % Page containing marked-content sequence
            *                                     / K 0                                                            % Marked - content identifier
            *                                  >>
            *                              endobj
            *                              2 0 obj                                                                 % Page object
            *                                  << / Type / Page
            *                                     / Resources << / XObject << / Fm4 4 0 R >>                       % Resource dictionary
            *                                                  >>                                                  % containing form XObject
            *                                     / Contents 3 0 R                                                 % Content stream
            *                                      …
            *                                  >>
            *                              endobj
            *                              3 0 obj                                                                 % Page's content stream
            *                                  << / Length … >>
            *                              stream
            *                                  …
            *                                  / P << / MCID 0 >>                                                  % Start of marked-content sequence
            *                                      BDC
            *                                          / Fm4 Do                                                    % Paint form XObject
            *                                      EMC                                                             % End of marked-content sequence
            *                                  …
            *                              endstream
            *                              endobj
            *                                  4 0 obj                                                             % Form XObject
            *                                      << / Type / XObject
            *                                         / Subtype / Form
            *                                         / Length …
            *                                      >>
            *                              stream
            *                                  …
            *                                  (Here is some text) Tj
            *                                  …
            *                              endstream
            *                              endobj
            *
            *
            *EXAMPLE 5                     1 0 obj                                                                 % Structure element
            *                                  << / Type / StructElem
            *                                     / S / P                                                          % Structure type
            *                                     / P …                                                            % Parent in structure hierarchy
            *                                     / K << / Type / MCR
            *                                            / Pg 2 0 R                                                % Page containing marked-content sequence
            *                                            / Stm 4 0 R                                               % Stream containing marked-content sequence
            *                                            / MCID 0                                                  % Marked - content identifier
            *                                         >>
            *                                  >>
            *                              endobj
            *                              2 0 obj                                                                 % Page object
            *                                  << / Type / Page
            *                                     / Resources << / XObject << / Fm4 4 0 R >>                       % Resource dictionary
            *                                                 >>                                                   % containing form XObject
            *                                     / Contents 3 0 R                                                 % Content stream
            *                                      …
            *                                  >>
            *                              endobj
            *                              3 0 obj                                                                 % Page's content stream
            *                                  << / Length … >>
            *                              stream
            *                                  …
            *                                  / Fm4 Do                                                            % Paint form XObject
            *                                  …
            *                              endstream
            *                              endobj
            *                              4 0 obj                                                                 % Form XObject
            *                                  << / Type / XObject
            *                                     / Subtype / Form
            *                                     / Length …
            *                                  >>
            *                              stream
            *                                  …
            *                                  / P << / MCID 0 >>                                                  % Start of marked-content sequence
            *                                      BDC
            *                                          …
            *                                          (Here is some text) Tj
            *                                          …
            *                                      EMC                                                             % End of marked-content sequence
            *                                          …
            *                              endstream
            *                              endobj
            *
            */

                /*14.7.4.3 PDF Objects as Content Items
            *
            *When a structure element’s content includes an entire PDF object, such as an XObject or an annotation, that is associated with a page but not directly included in the page’s content stream, the object shall be identified in the structure element’s K entry by an object reference dictionary (see Table 325).
            *
            *NOTE 1            This form of reference is used only for entire objects. 
            *                  If the referenced content forms only part of the object’s content stream, it is instead handled as a marked - content sequence, as described in the preceding sub - clause.
            *
            *Table 325 - Entries in an object reference dictionary
            *
            *          [Key]           [Type]              [Value]
            *
            *          Type            name                (Required) The type of PDF object that this dictionary describes; shall be OBJRfor an object reference.
            *
            *          Pg              dictionary          (Optional; shall be an indirect reference) The page object of the page on which the object shall be rendered. 
            *                                              This entry overrides any Pg entry in the structure element containing the object reference; it shall be used if the structure element has no such entry.
            *
            *          Obj             (any)               (Required; shall be an indirect reference) The referenced object.
            *
            *NOTE 2        If the referenced object is rendered on multiple pages, each rendering requires a separate object reference. 
            *              However, if it is rendered multiple times on the same page, just a single object reference suffices to identify all of them. 
            *              (If it is important to distinguish between multiple renditions of the same XObject on the same page, they should be accessed by means of marked-content sequences enclosing particular invocations of the Dooperator rather than through object references.)
            */

                /*14.7.4.4 Finding Structure Elements from Content Items
            *
            *Because a stream may not contain object references, there is no way for content items that are marked - content sequences to refer directly back to their parent structure elements(the ones to which they belong as content items).
            *Instead, a different mechanism, the structural parent tree, shall be provided for this purpose.
            *For consistency, content items that are entire PDF objects, such as XObjects, also shall use the parent tree to refer to their parent structure elements.
            *
            *The parent tree is a number tree(see 7.9.7, “Number Trees”), accessed from the ParentTree entry in a document’s structure tree root(Table 322).
            *The tree shall contain an entry for each object that is a content item of at least one structure element and for each content stream containing at least one marked - content sequence that is a content item.
            *The key for each entry shall be an integer given as the value of the StructParent or StructParents entry in the object(see Table 326).
            *The values of these entries shall be as follows:
            *
            *  •   For an object identified as a content item by means of an object reference(see 14.7.4.3, “PDF Objects as Content Items”), the value shall be an indirect reference to the parent structure element.
            *
            *  •   For a content stream containing marked - content sequences that are content items, the value shall be an array of indirect references to the sequences’ parent structure elements.
            *      The array element corresponding to each sequence shall be found by using the sequence’s marked-content identifier as a zero - based index into the array.
            *
            *NOTE      Because marked-content identifiers serve as indices into an array in the structural parent tree, their assigned values should be as small as possible to conserve space in the array.
            *
            *The ParentTreeNextKey entry in the structure tree root shall hold an integer value greater than any that is currently in use as a key in the structural parent tree. 
            *Whenever a new entry is added to the parent tree, the current value of ParentTreeNextKey shall be used as its key.
            *The value shall be then incremented to prepare for the next new entry to be added.
            *
            *To locate the relevant parent tree entry, each object or content stream that is represented in the tree shallcontain a special dictionary entry, StructParent or StructParents(see Table 326).
            *Depending on the type of content item, this entry may appear in the page object of a page containing marked - content sequences, in the stream dictionary of a form or image XObject, in an annotation dictionary, or in any other type of object dictionary that is included as a content item in a structure element.
            *Its value shall be the integer key under which the entry corresponding to the object shall be found in the structural parent tree.
            *
            *Table 326 - Additional dictionary entries for structure element access
            *
            *          [Key]               [Type]              [Value]
            *
            *          StructParent        integer             (Required for all objects that are structural content items; PDF 1.3) The integer key of this object’s entry in the structural parent tree.
            *
            *          StructParents       integer             (Required for all content streams containing marked-content sequences that are structural content items; PDF 1.3) 
            *                                                  The integer key of this object’s entry in the structural parent tree.
            *                                                  At most one of these two entries shall be present in a given object.
            *                                                  An object may be either a content item in its entirety or a container for marked - content sequences that are content items, but not both.
            *
            *For a content item identified by an object reference, the parent structure element may be found by using the value of the StructParent entry in the item’s object dictionary as a retrieval key in the structural parent tree (found in the ParentTree entry of the structure tree root). 
            *The corresponding value in the parent tree shall be a reference to the parent structure element (see Example 1).
            *
            *EXAMPLE 1             1 0 obj                                                 % Parent structure element
            *                          << / Type / StructElem
            *                              …
            *                             / K << / Type / OBJR                             % Object reference
            *                             / Pg 2 0 R                                       % Page containing form XObject
            *                             / Obj 4 0 R                                      % Reference to form XObject
            *                                 >>
            *                          >>
            *                      endobj
            *                      2 0 obj                                                 % Page object
            *                          << / Type / Page
            *                             / Resources << / XObject << / Fm4 4 0 R >>       % Resource dictionary
            *                                         >>                                   % containing form XObject
            *                             / Contents 3 0 R                                 % Content stream
            *                              …
            *                          >>
            *                      endobj
            *                      3 0 obj                                                 % Page's content stream
            *                          << / Length … >>
            *                      stream
            *                          …
            *                          / Fm4 Do                                            % Paint form XObject
            *                          …
            *                      endstream
            *                      endobj
            *                      4 0 obj                                                 % Form XObject
            *                          << / Type / XObject
            *                             / Subtype / Form
            *                             / Length …
            *                             / StructParent 6                                 % Parent tree key
            *                          >>
            *                      stream
            *                          …
            *                      endstream
            *                      endobj
            *                      100 0 obj                                               % Parent tree(accessed from structure tree root)
            *                          << / Nums[0 101 0 R
            *                                    1 102 0 R
            *                                      …
            *                                    6 1 0 R                                   % Entry for page object 2; points back
            *                                      …                                       % to parent structure element
            *                                    ]
            *                          >>
            *                      endobj
            *
            *For a content item that is a marked-content sequence, the retrieval method is similar but slightly more complicated. 
            *Because a marked-content sequence is not an object in its own right, its parent tree key shall be found in the StructParents entry of the page object or other content stream in which the sequence resides. 
            *The value retrieved from the parent tree shall not be a reference to the parent structure element itself but to an array of such references—one for each marked-content sequence contained within that content stream. 
            *The parent structure element for the given sequence shall be found by using the sequence’s marked-content identifier as an index into this array (see Example 2).
            *
            *EXAMPLE 2                 1 0 obj                                             % Parent structure element
            *                              << / Type / StructElem
            *                                  …
            *                                 / Pg 2 0 R                                   % Page containing marked-content sequence
            *                                 / K 0                                        % Marked - content identifier
            *                              >>
            *                          endobj
            *                          2 0 obj                                             % Page object
            *                              << / Type / Page
            *                                 / Contents 3 0 R                             % Content stream
            *                                 / StructParents 6                            % Parent tree key
            *                                  …
            *                              >>
            *                          endobj
            *                          3 0 obj                                             % Page's content stream
            *                              << / Length … >>
            *                          stream
            *                              …
            *                              / P << / MCID 0 >>                              % Start of marked-content sequence
            *                                  BDC
            *                                      (Here is some text) TJ
            *                                      …
            *                                  EMC                                         % End of marked-content sequence
            *                              …
            *                          endstream
            *                          endobj
            *                          100 0 obj                                           % Parent tree (accessed from structure tree root)
            *                              << / Nums[0 101 0 R
            *                                        1 102 0 R
            *                                          …
            *                                        6 [1 0 R]                             % Entry for page object 2; array element at index 0
            *                                          …                                   % points back to parent structure element
            *                                        ]
            *                              >>
            *                          endobj
            */

            /*14.7.5 Structure Attributes
            */

                /*14.7.5.1 General
            *
            *A conforming product that processes logical structure may attach additional information, called attributes, to any structure element.
            *The attribute information shall be held in one or more attribute objects associated with the structure element. 
            *An attribute object shall be a dictionary or stream that includes an O entry(see Table 327) identifying the conforming product that owns the attribute information.
            *Other entries shall represent the attributes: the keys shall be attribute names, and values shall be the corresponding attribute values.
            *To facilitate the interchange of content among conforming products, PDF defines a set of standard structure attributes identified by specific standard owners; see 14.8.5, “Standard Structure Attributes.” 
            *In addition, (PDF 1.6) attributes may be used to represent user properties(see 14.7.5.4, “User Properties”).
            *
            *Table 327 - Entry common to all attribute object dictionaries
            *
            *          [Key]               [Type]              [Value]
            *
            *          O                   name                (Required) The name of the conforming product owning the attribute data. The name shall conform to the guidelines described in Annex E.
            *
            *Any conforming product may attach attributes to any structure element, even one created by another conforming product. 
            *Multiple conforming products may attach attributes to the same structure element. 
            *The Aentry in the structure element dictionary (see Table 323) shall hold either a single attribute object or an array of such objects, together with revision numbers for coordinating attributes created by different conforming products (see 14.7.5.3, “Attribute Revision Numbers”). 
            *A conforming product creating or destroying the second attribute object for a structure element shall be responsible for converting the value of the A entry from a single object to an array or vice versa, as well as for maintaining the integrity of the revision numbers. 
            *No inherent order shall be defined for the attribute objects in an A array, but new objects should be added at the end of the array so that the first array element is the one belonging to the conforming product that originally created the structure element.
            */

                /*14.7.5.2 Attribute Classes
            *
            *If many structure elements share the same set of attribute values, they may be defined as an attribute classsharing the identical attribute object.
            *Structure elements shall refer to the class by name.
            *The association between class names and attribute objects shall be defined by a dictionary called the class map, that shall be kept in the ClassMap entry of the structure tree root(see Table 322). 
            *Each key in the class map shall be a name object denoting the name of a class. 
            *The corresponding value shall be an attribute object or an array of such objects.
            *
            *NOTE      PDF attribute classes are unrelated to the concept of a class in object-oriented programming languages such as Java and C++. 
            *          Attribute classes are strictly a mechanism for storing attribute information in a more compact form; they have no inheritance properties like those of true object-oriented classes.
            *
            *The C entry in a structure element dictionary (see Table 323) shall contain a class name or an array of class names (typically accompanied by revision numbers as well; see 14.7.5.3, “Attribute Revision Numbers”). 
            *For each class named in the C entry, the corresponding attribute object or objects shall be considered to be attached to the given structure element, along with those identified in the element’s A entry.
            *If both the A and Centries are present and a given attribute is specified by both, the one specified by the A entry shall take precedence.
            */

                /*14.7.5.3 Attribute Revision Numbers
            *
            *When a conforming product modifies a structure element or its contents, the change may affect the validity of attribute information attached to that structure element by other conforming products. 
            *A system of revision numbers shall allow conforming products to detect such changes and update their own attribute information accordingly, as described in this sub - clause.
            *
            *A structure element shall have a revision number, that shall be stored in the R entry in the structure element dictionary(see Table 323) or default to 0 if no R entry is present.
            *Initially, the revision number shall be 0.When a conforming product modifies the structure element or any of its content items, it may signal the change by incrementing the revision number.
            *
            *NOTE 1    The revision number is unrelated to the generation number associated with an indirect object(see 7.3.10, “Indirect Objects”).
            *
            *NOTE 2    If their is no R entry and the revision number is to be incremented from the default value of 0 to 1, an R entry must be created in the structure element dictionary in order to record the 1.
            *
            *Each attribute object attached to a structure element shall have an associated revision number. 
            *The revision number shall be stored in the array that associates the attribute object with the structure element or if not stored in the array that associates the attribute object with the structure element shall default to 0.
            *
            *  •   Each attribute object in a structure element’s A array shall be represented by a single or a pair of array elements, the first or only element shall contain the attribute object itself and the second (when present) shall contain the integer revision number associated with it in this structure element.
            *
            *  •   The structure element’s C array shall contain a single or a pair of elements for each attribute class, the first or only shall contain the class name and the second(when present) shall contain the associated revision number.
            *
            *The revision numbers are optional in both the A and C arrays. 
            *An attribute object or class name that is not followed by an integer array element shall have a revision number of 0 and is represented by a single entry in the array.
            *
            *NOTE 3        The revision number is not stored directly in the attribute object because a single attribute object may be associated with more than one structure element (whose revision numbers may differ). 
            *              Since an attribute object reference is distinct from an integer, that distinction is used to determine whether the attribute object is represented in the array by a single or a pair of entries.
            *
            *NOTE 4        When an attribute object is created or modified, its revision number is set to the current value of the structure element’s R entry.By comparing the attribute object’s revision number with that of the structure element, an application can determine whether the contents of the attribute object are still current or whether they have been outdated by more recent changes in the underlying structure element.
            *
            *Changes in an attribute object shall not change the revision number of the associated structure element, which shall change only when the structure element itself or any of its content items is modified.
            *
            *Occasionally, a conforming product may make extensive changes to a structure element that are likely to invalidate all previous attribute information associated with it. 
            *In this case, instead of incrementing the structure element’s revision number, the conforming product may choose to delete all unknown attribute objects from its A and C arrays. 
            *These two actions shall be mutually exclusive: the conforming product should either increment the structure element’s revision number or remove its attribute objects, but not both.
            *
            *NOTE 5        Any conforming product creating attribute objects needs to be prepared for the possibility that they can be deleted at any time by another conforming product.
            */

                /*14.7.5.4 User Properties
            *
            *Most structure attributes(see 14.8.5, “Standard Structure Attributes”) specify information that is reflected in the element’s appearance; for example, BackgroundColor or BorderStyle.
            *
            *Some conforming writers, such as CAD applications, may use objects that have a standardized appearance, each of which contains non-graphical information that distinguishes the objects from one another. 
            *For example, several transistors might have the same appearance but different attributes such as type and part number.
            *
            *User properties(PDF 1.6) may be used to contain such information.
            *Any graphical object that corresponds to a structure element may have associated user properties, specified by means of an attribute object dictionary that shall have a value of UserProperties for the O entry(see Table 328).
            *
            *Table 328 - Additional entries in an attribute object dictionary for user properties
            *
            *          [Key]               [Type]              [Value]
            *
            *          O                   name                (Required) The attribute owner. Shall be UserProperties.
            *
            *          P                   array               (Required) An array of dictionaries, each of which represents a user property (see Table 329).
            *
            *The P entry shall be an array specifying the user properties. Each element in the array shall be a user property dictionary representing an individual property (see Table 329). 
            *The order of the array elements shall specify attributes in order of importance.
            *
            *Table 329 - Entries in a user property dictionary
            *
            *          [Key]               [Type]              [Value]
            *
            *          N                   text                (Required) The name of the user property.
            *
            *          V                   any                 (Required) The value of the user property.
            *                                                  While the value of this entry shall be any type of PDF object, conforming writers should use only text string, number, and boolean values. 
            *                                                  Conforming readers should display text, number and boolean values to users but need not display values of other types; however, they should not treat other values as errors.
            *
            *          F                   text string         (Optional) A formatted representation of the value of V, that shall be used for special formatting; for example “($123.45)” for the number -123.45. 
            *                                                  If this entry is absent, conforming readers should use a default format.
            *
            *          H                   boolean             (Optional) If true, the attribute shall be hidden; that is, it shall not be shown in any user interface element that presents the attributes of an object. 
            *                                                  Default value: false.
            *
            *PDF documents that contain user properties shall provide a UserProperties entry with a value of true in the document’s mark information dictionary (see Table 321). 
            *This entry allows conforming readers to quickly determine whether it is necessary to search the structure tree for elements containing user properties.
            *
            *EXAMPLE       The following example shows a structure element containing user properties called Part Name, Part Number, Supplier, and Price.
            *
            *              100 0 obj
            *                  << / Type / StructElem
            *                     / S / Figure                                                         % Structure type
            *                     / P 50 0 R                                                           % Parent in structure tree
            *                     / A << / O / UserProperties                                          % Attribute object
            *                           / P[                                                           % Array of user properties
            *                              << / N(Part Name) / V(Framostat) >>
            *                              << / N(Part Number) / V 11603 >>
            *                              << / N(Supplier) / V(Just Framostats) / H true >>           % Hidden attribute
            *                              << / N(Price) / V - 37.99 / F($37.99) >> % Formatted value
            *                              ]
            *                          >>
            *                  >>
            *              endobj
            */

            /*14.7.6 Example of Logical Structure
            *
                *The next Example shows portions of a PDF file with a simple document structure. 
                *The structure tree root (object 300) contains elements with structure types Chap (object 301) and Para (object 304). 
                *The Chapelement, titled Chapter 1, contains elements with types Head1 (object 302) and Para (object 303).
                *
                *These elements are mapped to the standard structure types specified in Tagged PDF(see 14.8.4, “Standard Structure Types”) by means of the role map specified in the structure tree root. 
                *Objects 302 through 304 have attached attributes(see 14.7.5, “Structure Attributes,” and 14.8.5, “Standard Structure Attributes”).
                *
                *The example also illustrates the structure of a parent tree(object 400) that maps content items back to their parent structure elements and an ID tree(object 403) that maps element identifiers to the structure elements they denote.
                *
                *EXAMPLE                   1 0 obj                                             % Document catalog
                *                              << / Type / Catalog
                *                                 / Pages 100 0 R                              % Page tree
                *                                 / StructTreeRoot 300 0 R                     % Structure tree root
                *                              >>
                *                          endobj
                *                          100 0 obj                                           % Page tree
                *                              << / Type / Pages
                *                                 / Kids[101 1 R                               % First page object
                *                                        102 0 R                               % Second page object
                *                                       ]
                *                                 / Count 2                                    % Page count
                *                              >>
                *                          endobj
                *                          101 1 obj                                           % First page object
                *                              << / Type / Page
                *                                 / Parent 100 0 R                             % Parent is the page tree
                *                                 / Resources << / Font << / F1 6 0 R          % Font resources
                *                                                          / F12 7 0 R
                *                                                       >>
                *                                                / ProcSet[/ PDF / Text]       % Procedure sets
                *                                              >>
                *                                 / MediaBox[0 0 612 792]                      % Media box
                *                                 / Contents 201 0 R                           % Content stream
                *                                 / StructParents 0                            % Parent tree key
                *                              >>
                *                          endobj
                *                          201 0 obj                                           % Content stream for first page
                *                              << / Length … >>
                *                          stream
                *                              1 1 1 rg
                *                              0 0 612 792 re f
                *                              BT                                              % Start of text object
                *                                  / Head1 << / MCID 0 >>                      % Start of marked-content sequence 0
                *                                      BDC
                *                                          0 0 0 rg
                *                                          / F1 1 Tf
                *                                          30 0 0 30 18 732 Tm
                *                                          (This is a first level heading.Hello world: ) Tj
                *                                          1.1333 TL
                *                                          T *
                *                                          (goodbye universe.) Tj
                *                                      EMC                                     % End of marked-content sequence 0
                *                                  / Para << / MCID 1 >>                       % Start of marked-content sequence 1
                *                              BDC
                *                                  / F12 1 Tf
                *                                  14 0 0 14 18 660.8 Tm
                *                                  (This is the first paragraph, which spans pages.It has four fairly short and \
                *                                  concise sentences.This is the next to last) Tj
                *                              EMC                                             % End of marked-content sequence 1
                *                              ET                                              % End of text object
                *                          endstream
                *                          endobj
                *                          102 0 obj                                           % Second page object
                *                              << / Type / Page
                *                                 / Parent 100 0 R                             % Parent is the page tree
                *                                 / Resources << / Font << / F1 6 0 R          % Font resources
                *                                                / F12 7 0 R
                *                                                       >>
                *                                                / ProcSet[/ PDF / Text]       % Procedure sets
                *                                             >>
                *                                 / MediaBox[0 0 612 792]                      % Media box
                *                                 / Contents 202 0 R                           % Content stream
                *                                 / StructParents 1                            % Parent tree key
                *                              >>
                *                          endobj
                *                          202 0 obj                                           % Content stream for second page
                *                              << / Length … >>
                *                          stream
                *                              1 1 1 rg
                *                              0 0 612 792 re f
                *                              BT                                              % Start of text object
                *                                  / Para << / MCID 0 >>                       % Start of marked-content sequence 0
                *                                      BDC
                *                                          0 0 0 rg
                *                                              / F12 1 Tf
                *                                              14 0 0 14 18 732 Tm
                *                                              (sentence.This is the very last sentence of the first paragraph.) Tj
                *                                      EMC                                     % End of marked-content sequence 0
                *                                  / Para << / MCID 1 >>                       % Start of marked-content sequence 1
                *                                      BDC
                *                                          / F12 1 Tf
                *                                          14 0 0 14 18 570.8 Tm
                *                                          (This is the second paragraph.It has four fairly short and concise sentences. \ This is the next to last) Tj
                *                                          EMC                                 % End of marked-content sequence 1
                *                                  / Para << / MCID 2 >>                       % Start of marked-content sequence 2
                *                                      BDC
                *                                          1.1429 TL
                *                                          T *
                *                                          (sentence.This is the very last sentence of the second paragraph.) Tj
                *                                      EMC                                     % End of marked-content sequence 2
                *                              ET                                              % End of text object
                *                          endstream
                *                          endobj
                *                          300 0 obj                                           % Structure tree root
                *                              << / Type / StructTreeRoot
                *                                 / K[301 0 R                                  % Two children: a chapter
                *                          304 0 R                                             % and a paragraph
                *                                    ]
                *                                 /RoleMap << /Chap /Sect                      % Mapping to standard structure types
                *                                             / Head1 / H
                *                                             / Para / P
                *                                          >>
                *                                  / ClassMap << / Normal 305 0 R >>           % Class map containing one attribute class
                *                                  /ParentTree 400 0 R                         % Number tree for parent elements
                *                                  /ParentTreeNextKey 2                        % Next key to use in parent tree
                *                                  /IDTree 403 0 R                             % Name tree for element identifiers
                *                              >>
                *                          endobj
                *                          301 0 obj                                           % Structure element for a chapter
                *                              << /Type /StructElem
                *                                 /S /Chap
                *                                 /ID(Chap1)                                   % Element identifier
                *                                 /T(Chapter 1)                                % Human-readable title
                *                                 /P 300 0 R                                   % Parent is the structure tree root
                *                                 /K[302 0 R                                   % Two children: a section head
                *                                    303 0 R                                   % and a paragraph
                *                                   ]
                *                              >>
                *                          endobj
                *                          302 0 obj                                           % Structure element for a section head
                *                              << / Type / StructElem
                *                                 / S / Head1
                *                                 / ID(Sec1.1)                                 % Element identifier
                *                                 / T(Section 1.1)                             % Human - readable title
                *                                 / P 301 0 R                                  % Parent is the chapter
                *                                 / Pg 101 1 R                                 % Page containing content items
                *                                 / A << / O / Layout                          % Attribute owned by Layout
                *                                        / SpaceAfter 25
                *                                        / SpaceBefore 0
                *                                        / TextIndent 12.5
                *                                     >>
                *                                 / K 0                                        % Marked - content sequence 0
                *                              >>
                *                          endobj
                *                          303 0 obj                                           % Structure element for a paragraph
                *                              << / Type / StructElem
                *                                 / S / Para
                *                                 / ID(Para1)                                  % Element identifier
                *                                 / P 301 0 R                                  % Parent is the chapter
                *                                 / Pg 101 1 R                                 % Page containing first content item
                *                                 / C / Normal                                 % Class containing this element’s attributes
                *                                 / K[1                                        % Marked - content sequence 1
                *                                      << / Type / MCR                         % Marked - content reference to 2nd item
                *                                         / Pg 102 0 R                         % Page containing second item
                *                                         / MCID 0                             % Marked - content sequence 0
                *                                      >>
                *                                    ]
                *                              >>
                *                          endobj
                *                          304 0 obj                                           % Structure element for another paragraph
                *                              << / Type / StructElem
                *                                 / S / Para
                *                                 / ID(Para2)                                  % Element identifier
                *                                 / P 300 0 R                                  % Parent is the structure tree root
                *                                 / Pg 102 0 R                                 % Page containing content items
                *                                 / C / Normal                                 % Class containing this element’s attributes
                *                                 / A << / O / Layout
                *                                        / TextAlign / Justify                 % Overrides attribute provided by classmap
                *                                      >>
                *                                  / K[1 2]                                    % Marked - content sequences 1 and 2
                *                              >>
                *                          endobj
                *                          305 0 obj                                           % Attribute class
                *                              << /O /Layout                                   % Owned by Layout
                *                                 /EndIndent 0
                *                                 /StartIndent 0
                *                                 /WritingMode /LrTb
                *                                 /TextAlign /Start
                *                              >>
                *                          endobj
                *                          400 0 obj                                           % Parent tree
                *                              << /Nums[0 401 0 R                              % Parent elements for first page
                *                                       1 402 0 R                              % Parent elements for second page
                *                                      ]
                *                              >>
                *                          endobj
                *                          401 0 obj                                           % Array of parent elements for first page
                *                              [302 0 R                                        % Parent of marked - content sequence 0
                *                               303 0 R                                        % Parent of marked - content sequence 1
                *                              ]
                *                          endobj
                *                          402 0 obj                                           % Array of parent elements for second page
                *                              [303 0 R                                        % Parent of marked - content sequence 0
                *                               304 0 R                                        % Parent of marked - content sequence 1
                *                               304 0 R                                        % Parent of marked - content sequence 2
                *                              ]
                *                          endobj
                *                          403 0 obj                                           % ID tree root node
                *                              << /Kids[404 0 R] >>                            % Reference to leaf node
                *                          endobj
                *                          404 0 obj                                           % ID tree leaf node
                *                              << /Limits[(Chap1)(Sec1.3) ]                    % Least and greatest keys in tree
                *                                 /Names[(Chap1)301 0 R                        % Mapping from element identifiers
                *                                        (Sec1.1) 302 0 R                      % to structure elements
                *                                        (Sec1.2) 303 0 R
                *                                        (Sec1.3) 304 0 R
                *                                       ]
                *                              >>
                *                          endobj
                *
                */

       

        }

        //14.8 Tagged PDF
        public class Tagged_PDF
        {
            /*14.8.1 General
            *
            *Tagged PDF(PDF 1.4) is a stylized use of PDF that builds on the logical structure framework described in 14.7, “Logical Structure.” 
            *It defines a set of standard structure types and attributes that allow page content (text, graphics, and images) to be extracted and reused for other purposes. 
            *A tagged PDF document is one that conforms to the rules described in this sub-clause.A conforming writer is not required to produce tagged PDF documents; however, if it does, it shall conform to these rules.
            *
            *NOTE 1    It is intended for use by tools that perform the following types of operations:
            *
            *  •   Simple extraction of text and graphics for pasting into other applications
            *
            *  •   Automatic reflow of text and associated graphics to fit a page of a different size than was assumed for the original layout
            *
            *  •   Processing text for such purposes as searching, indexing, and spell-checking
            *
            *  •   Conversion to other common file formats(such as HTML, XML, and RTF) with document structure and basic styling information preserved
            *
            *  •   Making content accessible to users with visual impairments(see 14.9, “Accessibility Support”)
            *
            *A tagged PDF document shall conform to the following rules:
            *
            *  •   Page content(14.8.2, “Tagged PDF and Page Content”). Tagged PDF defines a set of rules for representing text in the page content so that characters, words, and text order can be determined reliably.
            *      All text shall be represented in a form that can be converted to Unicode. Word breaks shall be represented explicitly. 
            *      Actual content shall be distinguished from artifacts of layout and pagination.
            *      Content shall begiven in an order related to its appearance on the page, as determined by the conforming writer.
            *
            *  •   A basic layout model (14.8.3, “Basic Layout Model”). A set of rules for describing the arrangement of structure elements on the page.
            *
            *  •   Structure types(14.8.4, “Standard Structure Types”). A set of standard structure types define the meaning of structure elements, such as paragraphs, headings, articles, and tables.
            *
            *  •   Structure attributes (14.8.5, “Standard Structure Attributes”). Standard structure attributes preserve styling information used by the conforming writer in laying out content on the page.
            *
            *A Tagged PDF document shall also contain a mark information dictionary (see Table 321) with a value of truefor the Marked entry.
            *
            *NOTE 2    The types and attributes defined for Tagged PDF are intended to provide a set of standard fallback roles and minimum guaranteed attributes to enable conforming readers to perform operations such as those mentioned previously.
            *          Conforming writers are free to define additional structure types as long as they also provide a role mapping to the nearest equivalent standard types, as described in 14.7.3, “Structure Types.” 
            *          Likewise, conforming writers can define additional structure attributes using any of the available extension mechanisms.
            */

            /*14.8.2 Tagged PDF and Page Content
            */
                
                /*14.8.2.1 General
                *
                *Like all PDF documents, a Tagged PDF document consists of a sequence of self - contained pages, each of which shall be described by one or more page content streams(including any subsidiary streams such as form XObjects and annotation appearances).
                *Tagged PDF defines some further rules for organizing and marking content streams so that additional information can be derived from them:
                *
                *  •   Distinguishing between the author’s original content and artifacts of the layout process(see 14.8.2.2, “Real Content and Artifacts”).
                *
                *  •   Specifying a content order to guide the layout process if the conforming reader reflows the page content(see 14.8.2.3, “Page Content Order”).
                *
                *  •   Representing text in a form from which a Unicode representation and information about font characteristics can be unambiguously derived(see 14.8.2.4, “Extraction of Character Properties”).
                *
                *  •   Representing word breaks unambiguously(see 14.8.2.5, “Identifying Word Breaks”).
                *
                *  •   Marking text with information for making it accessible to users with visual impairments(see 14.9, “Accessibility Support”).
                */

                /*14.8.2.2 Real Content and Artifacts
                */

                    /*14.8.2.2.1 General
                *
                *The graphics objects in a document can be divided into two classes:
                *
                *  •   The real content of a document comprises objects representing material originally introduced by the document’s author.
                *
                *  •   Artifacts are graphics objects that are not part of the author’s original content but rather are generated by the conforming writer in the course of pagination, layout, or other strictly mechanical processes.
                *
                *NOTE      Artifacts may also be used to describe areas of the document where the author uses a graphical background, with the goal of enhancing the visual experience.
                *          In such a case, the background is not required for understanding the content.
                *
                *The document’s logical structure encompasses all graphics objects making up the real content and describes how those objects relate to one another.
                *It does not include graphics objects that are mere artifacts of the layout and production process.
                *
                *A document’s real content includes not only the page content stream and subsidiary form XObjects but also associated annotations that meet all of the following conditions:
                *
                *  •   The annotation has an appearance stream(see 12.5.5, “Appearance Streams”) containing a normal(N) appearance.
                *
                *  •   The annotation’s Hidden flag(see 12.5.3, “Annotation Flags”) is not set.
                *
                *  •   The annotation is included in the document’s logical structure(see 14.7, “Logical Structure”).
                */

                    /*14.8.2.2.2 Specification of Artifacts
                *
                *An artifact shall be explicitly distinguished from real content by enclosing it in a marked-content sequence with the tag Artifact:
                *
                *          / Artifact                  / Artifact propertyList
                *              BMCBDC
                *                          …or…
                *              EMC                     EMC
                *
                *The first form shall be used to identify a generic artifact; the second shall be used for those that have an associated property list.
                *Table 330 shows the properties that can be included in such a property list.
                *
                *NOTE 1    To aid in text reflow, artifacts should be defined with property lists whenever possible.
                *          Artifacts lacking a specified bounding box are likely to be discarded during reflow.
                *
                *Table 330 - Property list entries for artifacts
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Optional) The type of artifact that this property list describes; if present, shall be one of the names Pagination, Layout, Page, or (PDF 1.7)Background.
                *
                *          BBox                rectangle               (Optional; required for background artifacts) An array of four numbers in default user space units giving the coordinates of the left, bottom, right, and top edges, respectively, of the artifact’s bounding box (the rectangle that completely encloses its visible extent).
                *
                *          Attached            array                   (Optional; pagination and full-page background artifacts only) 
                *                                                      An array of name objects containing one to four of the names Top, Bottom, Left, and Right, specifying the edges of the page, if any, to which the artifact is logically attached. 
                *                                                      Page edges shall be defined by the page’s crop box (see 14.11.2, “Page Boundaries”). 
                *                                                      The ordering of names within the array is immaterial. Including both Left and Right or both Top and Bottom indicates a full-width or full-height artifact, respectively.
                *                                                      Use of this entry for background artifacts shall be limited to full - page artifacts.
                *                                                      Background artifacts that are not full - page take their dimensions from their parent structural element.
                *
                *          Subtype             name                    (Optional; PDF 1.7) The subtype of the artifact. 
                *                                                      This entry should appear only when the Type entry has a value of Pagination. 
                *                                                      Standard values are Header, Footer, and Watermark.
                *                                                      Additional values may be specified for this entry, provided they comply with the naming conventions described in Annex E.
                *
                *The following types of artifacts can be specified by the Type entry:
                *
                *  •   Pagination artifacts. Ancillary page features such as running heads and folios(page numbers).
                *
                *  •   Layout artifacts. Purely cosmetic typographical or design elements such as footnote rules or background screens.
                *
                *  •   Page artifacts. Production aids extraneous to the document itself, such as cut marks and colour bars.
                *
                *  •   Background artifacts. Images, patterns or coloured blocks that either run the entire length and / or width of the page or the entire dimensions of a structural element. Background artifacts typically serve as a background for content shown either on top of or placed adjacent to that background.
                *
                *A background artifact can further be classified as visual content that serves to enhance the user experience, that lies under the actual content, and that is not required except to retain visual fidelity.
                *
                *NOTE 2        Examples of this include a coloured background, pattern, blend, or image that resides under main body text.
                *              In the case of white text on a black background, the black background is absolutely necessary to be able to read the white text; however, the background itself is merely there to enhance the visual experience.
                *              However, a draft or other identifying watermark is classified as a pagination artifact because it does not serve to enhance the experience; rather, it serves as a running artifact typically used on every page in the document. 
                *              As a further example, a Figure differs from a background artifact in that removal of the graphics objects from a Figure would detract from the overall contextual understanding of the Figure as an entity.
                *
                *  •   Tagged conforming readers may have their own ideas about what page content to consider relevant.A text-to - speech engine, for instance, probably should not speak running heads or page numbers when the page is turned.
                *      In general, conforming readers can do any of the following:
                *
                *  •   Disregard elements of page content(for example, specific types of artifacts) that are not of interest
                *
                *  •   Treat some page elements as terminals that are not to be examined further(for example, to treat an illustration as a unit for reflow purposes)
                *
                *  •   Replace an element with alternate text (see 14.9.3, “Alternate Descriptions”)
                *
                *NOTE 3        Depending on their goals, different conforming readers can make different decisions in this regard.
                *              The purpose of Tagged PDF is not to prescribe what the conforming reader should do, but to provide sufficient declarative and descriptive information to allow it to make appropriate choices about how to process the content.
                *
                *To support conforming readers in providing accessibility to users with disabilities, Tagged PDF documents should use the natural language specification(Lang), alternate description(Alt), replacement text(ActualText), and abbreviation expansion text(E) facilities described in 14.9, “Accessibility Support.”
                */

                    /*14.8.2.2.3 Incidental Artifacts
                *
                *In addition to objects that are explicitly marked as artifacts and excluded from the document’s logical structure, the running text of a page may contain other elements and relationships that are not logically part of the document’s real content, but merely incidental results of the process of laying out that content into a document.
                *They may include the following elements:
                *
                *  •   Hyphenation.Among the artifacts introduced by text layout is the hyphen marking the incidental division of a word at the end of a line.
                *      In Tagged PDF, such an incidental word division shall be represented by a soft hyphen character, which the Unicode mapping algorithm(see “Unicode Mapping in Tagged PDF” in 14.8.2.4, “Extraction of Character Properties”) translates to the Unicode value U+00AD. 
                *      (This character is distinct from an ordinary hard hyphen, whose Unicode value is U + 002D.) The producer of a Tagged PDF document shall distinguish explicitly between soft and hard hyphens so that the consumer does not have to guess which type a given character represents.
                *
                *NOTE 1        In some languages, the situation is more complicated: there may be multiple hyphen characters, and hyphenation may change the spelling of words.
                *              See the Example in 14.9.4, “Replacement Text.”
                *
                *  •   Text discontinuities. The running text of a page, as expressed in page content order(see 14.8.2.3, “Page Content Order”), may contain places where the normal progression of text suffers a discontinuity. 
                *      Conforming readers may recognize such discontinuities by examining the document’s logical structure.
                *
                *NOTE 2        For example, the page may contain the beginnings of two separate articles(see 12.4.3, “Articles”), each of which is continued onto a later page of the document. 
                *              The last words of the first article appearing on the page should not be run together with the first words of the second article.
                *
                *  •   Hidden page elements. For a variety of reasons, elements of a document’s logical content may be invisible on the page: they may be clipped, their colour may match the background, or they may be obscured by other, overlapping objects. 
                *      For the purposes of Tagged PDF, page content shall be considered to include all text and illustrations in their entirety, regardless of whether they are visible when the document is displayed or printed.
                *
                *NOTE 3        For example, formerly invisible elements may become visible when a page is reflowed, or a text - to - speech engine may choose to speak text that is not visible to a sighted reader.
                */

                /*14.8.2.3 Page Content Order
                */

                    /*14.8.2.3.1 General
                *
                *When dealing with material on a page-by - page basis, some Tagged PDF conforming readers may choose to process elements in page content order, determined by the sequencing of graphics objects within a page’s content stream and of characters within a text object, rather than in the logical structure order defined by a depth-first traversal of the page’s logical structure hierarchy. 
                *The two orderings are logically distinct and may or may not coincide. 
                *In particular, any artifacts the page may contain shall be included in the page content order but not in the logical structure order, since they are not considered part of the document’s logical structure.
                *Theconforming writer is responsible for establishing both an appropriate page content order for each page and an appropriate logical structure hierarchy for the entire document.
                *
                *Because the primary requirement for page content order is to enable reflow to maintain elements in proper reading sequence, it should normally(for Western writing systems) proceed from top to bottom(and, in a multiple - column layout, from column to column), with artifacts in their correct relative places. 
                *In general, all parts of an article that appear on a given page should be kept together, even if the article flows to scattered locations on the page. 
                *Illustrations or footnotes may be interspersed with the text of the associated article or may appear at the end of its content(or, in the case of footnotes, at the end of the entire page’s logical content).
                *
                *In some situations, conforming writer may be unable to determine correct page content order for part of a document’s contents.
                *In such cases, tag suspects(PDF 1.6) can be used.The conforming writer shall identify suspect content by using marked content (see 14.6, “Marked Content”) with a tag of TagSuspect, as shown in next Example. 
                *The marked content shall have a properties dictionary with an entry whose name is TagSuspect and whose value is Ordering, which indicates that the ordering of the enclosed marked content does not meet Tagged PDF specifications.
                *
                *NOTE      This can occur, for example, if content was extracted from another application, or if there are ambiguities or missing information in text output.
                *
                *EXAMPLE           / TagSuspect <</ TagSuspect / Ordering >>
                *                      BDC
                *                           ....                                            % Problem page contents
                *                      EMC
                *
                *Documents containing tag suspects shall contain a Suspects entry with a value of true in the mark information dictionary(see Table 321).
                */

                    /*14.8.2.3.2 Sequencing of Annotations
                *
                *Annotations associated with a page are not interleaved within the page’s content stream but shall be placed in the Annots array in its page object(see 7.7.3.3, “Page Objects”).
                *Consequently, the correct position of an annotation in the page content order is not readily apparent but shall be determined from the document’s logical structure.
                *
                *Both page content(marked - content sequences) and annotations may be treated as content items that are referenced from structure elements (see 14.7.4, “Structure Content”). 
                *Structure elements of type Annot(PDF 1.5), Link, or Form(see 14.8.4.4, “Inline - Level Structure Elements,” and 14.8.4.5, “Illustration Elements”) explicitly specify the association between a marked - content sequence and a corresponding annotation.
                *In other cases, if the structure element corresponding to an annotation immediately precedes or follows(in the logical structure order) a structure element corresponding to a marked - content sequence, the annotation is considered to precede or follow the marked - content sequence, respectively, in the page content order.
                *
                *NOTE      If necessary, a conforming writer may introduce an empty marked - content sequence solely to serve as a structure element for the purpose of positioning adjacent annotations in the page content order.
                */

                    /*14.8.2.3.3 Reverse - Order Show Strings
                *
                *NOTE 1        In writing systems that are read from right to left (such as Arabic or Hebrew), one might expect that the glyphs in a font would have their origins at the lower right and their widths (rightward horizontal displacements) specified as negative. 
                *              For various technical and historical reasons, however, many such fonts follow the same conventions as those designed for Western writing systems, with glyph origins at the lower left and positive widths, as shown in Figure 39. 
                *              Consequently, showing text in such right-to-left writing systems requires either positioning each glyph individually (which is tedious and costly) or representing text with show strings (see 9.2, “Organization and Use of Fonts”) whose character codes are given in reverse order. When the latter method is used, the character codes’ correct page content order is the reverse of their order within the show string.
                *
                *The marked-content tag ReversedChars informs the conforming reader that show strings within a marked-content sequence contain characters in the reverse of page content order. 
                *If the sequence encompasses multiple show strings, only the individual characters within each string shall be reversed; the strings themselves shall be in natural reading order.
                *
                *EXAMPLE           The sequence
                *
                *                  / ReversedChars
                *                      BMC
                *                          (olleH) Tj
                *                          −200 0 Td
                *                          (.dlrow) Tj
                *                      EMC
                *
                *                  represents the text
                *
                *                  Hello world.
                *
                *The show strings may have a SPACE (U+0020) character at the beginning or end to indicate a word break (see 14.8.2.5, “Identifying Word Breaks”) but shall not contain interior SPACEs.
                *
                *NOTE 2        This limitation is not serious, since a SPACE provides an opportunity to realign the typography without visible effect, and it serves the valuable purpose of limiting the scope of reversals for word - processing conforming readers.
                */

                /*14.8.2.4  Extraction of Character Properties
                */

                    /*14.8.2.4.1 General
                *
                *Tagged PDF enables character codes to be unambiguously converted to Unicode values representing the information content of the text.
                *There are several methods for doing this; a Tagged PDF document shallconform to at least one of them(see “Unicode Mapping in Tagged PDF” in 14.8.2.4, “Extraction of Character Properties”).
                *In addition, Tagged PDF enables some characteristics of the associated fonts to be deduced(see “Font Characteristics” in 14.8.2.4, “Extraction of Character Properties”).
                *
                *NOTE      These Unicode values and font characteristics can then be used for such operations as cut - and - paste editing, searching, text - to - speech conversion, and exporting to other applications or file formats.
                */

                    /*14.8.2.4.2 Unicode Mapping in Tagged PDF
                *
                *Tagged PDF requires that every character code in a document can be mapped to a corresponding Unicode value.
                *
                *NOTE 1        Unicode defines scalar values for most of the characters used in the world’s languages and writing systems, as well as providing a private use area for application-specific characters.
                *              Information about Unicode can be found in the Unicode Standard, by the Unicode Consortium (see the Bibliography).
                *
                *The methods for mapping a character code to a Unicode value are described in 9.10.2, “Mapping Character Codes to Unicode Values.” 
                *A conforming writer shall ensure that the PDF file contains enough information to map all character codes to Unicode by one of the methods described there.
                *
                *NOTE 2        An Alt, ActualText, or E entry specified in a structure element dictionary or a marked-content property list (see 14.9.3, “Alternate Descriptions,” 14.9.4, “Replacement Text,” and 14.9.5, “Expansion of Abbreviations and Acronyms”) may affect the character stream that some conforming readers actually use.
                *              For example, some conforming readers may choose to use the Alt or ActualText value and ignore all text and other content associated with the structure element and its descendants.
                *
                *NOTE 3        Some uses of Tagged PDF require characters that may not be available in all fonts, such as the soft hyphen (see 14.8.2.2.3, “Incidental Artifacts”). Such characters may be represented either by adding them to the font’s encoding or CMap and using ToUnicode to map them to appropriate Unicode values, or by using an ActualText entry in the associated structure element to provide substitute characters.
                */

                    /*14.8.2.4.3 Font Characteristics
                *
                *In addition to a Unicode value, each character code in a content stream has an associated set of font characteristics. 
                *These characteristics are not specified explicitly in the PDF file.
                *Instead, the conforming reader derives the characteristics from the font descriptor for the font that is set in the text state at the time the character is shown.
                *
                *NOTE      These characteristics are useful when exporting text to another application or file format that has a limited repertoire of available fonts.
                *
                *Table 331 lists a common set of font characteristics corresponding to those used in CSS and XSL; the W3C document Extensible Stylesheet Language(XSL) 1.0 provides more information(see the Bibliography).
                *Each of the characteristics shall be derived from information available in the font descriptor’s Flags entry(see 9.8.2, “Font Descriptor Flags”).
                *
                *Table 331 - Derivation of font characteristics
                *
                *          [Characteristic]            [Type]              [Derivation]
                *
                *          Serifed                     boolean             The value of the Serif flag in the font descriptor’s Flags entry
                *
                *          Proportional                boolean             The complement of the FixedPitch flag in the font descriptor’s Flagsentry
                *
                *          Italic                      boolean             The value of the Italic flag in the font descriptor’s Flags entry
                *
                *          Smallcap                    boolean             The value of the SmallCap flag in the font descriptor’s Flags entry
                *
                *The characteristics shown in the table apply only to character codes contained in show strings within content streams. 
                *They do not exist for alternate description text (Alt), replacement text (ActualText), or abbreviation expansion text (E).
                *
                *For the standard 14 Type 1 fonts, the font descriptor may be missing; the well-known values for those fonts shall be used.
                *
                *Tagged PDF in PDF 1.5 defines a wider set of font characteristics, which provide information needed when converting PDF to other files formats such as RTF, HTML, XML, and OEB, and also improve accessibility and reflow of tables.
                *Table 332 lists these font selector attributes and shows how their values shall be derived.
                *
                *If the FontFamily, FontWeight and FontStretch fields are not present in the font descriptor, these values shall be derived from the font name in a manner of the conforming reader’s choosing.
                *
                *Table 332 - Font selector attributes
                *
                *          [Attribute]                 [Description]
                *
                *          FontFamily                  A string specifying the preferred font family name. Derived from the FontFamilyentry in the font descriptor (see Table 122).
                *
                *          GenericFontFamily           A general font classification, used if FontFamily is not found. Derived from the font descriptor’s Flags entry as follows:
                *
                *                                      Serif           Chosen if the Serif flag is set and the FixedPitch and Script flags are not set
                *                                      SansSerif       Chosen if the FixedPitch, Script and Serif flags are all not set
                *                                      Cursive         Chosen if the Script flag is set and the FixedPitch flag is not set
                *                                      Monospace       Chosen if the FixedPitch flag is set
                *                                      
                *                                      NOTE            The values Decorative and Symbol cannot be derived
                *
                *          FontSize                    The size of the font: a positive number specifying the height of the typeface in points. Derived from the a, b, c, and d fields of the current text matrix.
                *
                *          FontStretch                 The stretch value of the font. Derived from FontStretch in the font descriptor (see Table 122).
                *
                *          FontStyle                   The italicization value of the font. It shall be Italic if the Italic flag is set in the Flags field of the font descriptor; otherwise, it shall be Normal.
                *
                *          FontVariant                 The small-caps value of the font. It shall be SmallCaps if the SmallCap flag is set in the Flags field of the font descriptor; otherwise, it shall be Normal.
                *
                *          FontWeight                  The weight (thickness) value of the font. Derived from FontWeight in the font descriptor (see Table 122).
                *                                      The ForceBold flag and the StemV field should not be used to set this attribute.
                *
                *14.8.2.5 Identifying Word Breaks
                *
                *NOTE 1        A document’s text stream defines not only the characters in a page’s text but also the words. 
                *              Unlike a character, the notion of a word is not precisely defined but depends on the purpose for which the text is being processed. 
                *              A reflow tool needs to determine where it can break the running text into lines; a text-to - speech engine needs to identify the words to be vocalized; spelling checkers and other applications all have their own ideas of what constitutes a word.
                *              It is not important for a Tagged PDF document to identify the words within the text stream according to a single, unambiguous definition that satisfies all of these clients.What is important is that there be enough information available for each client to make that determination for itself.
                *
                *A conforming reader of a Tagged PDF document may find words by sequentially examining the Unicode character stream, perhaps augmented by replacement text specified with ActualText(see 14.9.4, “Replacement Text”).
                *For this purpose the spacing characters that would be present to separate words in a pure text representation shall be present in the Tagged PDF representation of the text.
                *
                *NOTE 2        The conforming reader does not need to guess about word breaks based on information such as glyph positioning on the page, font changes, or glyph sizes.
                *
                *NOTE 3        The identification of what constitutes a word is unrelated to how the text happens to be grouped into show strings.
                *              The division into show strings has no semantic significance.In particular, a SPACE(U + 0020) or other word - breaking character is still needed even if a word break happens to fall at the end of a show string.
                *
                *NOTE 4        Some conforming readers may identify words by simply separating them at every SPACE character. 
                *              Others may be slightly more sophisticated and treat punctuation marks such as hyphens or em dashes as word separators as well.Still others may identify possible line -break opportunities by using an algorithm similar to the one in Unicode Standard Annex #29, Text Boundaries, available from the Unicode Consortium (see the Bibliography).
                */

            /*14.8.3 Basic Layout Model
            *
                *The basic layout model begins with the notion of a reference area. 
                *This is a rectangular region used as a frame or guide in which to place the document’s content. 
                *Some of the standard structure attributes, such as StartIndent and EndIndent (see 14.8.5.4.3, “Layout Attributes for BLSEs”), shall be measured from the boundaries of the reference area. 
                *Reference areas are not specified explicitly but are inferred from context. 
                *Those of interest are generally the column area or areas in a general text layout, the outer bounding box of a table and those of its component cells, and the bounding box of an illustration or other floating element.
                *
                *NOTE 1        Tagged PDF’s standard structure types and attributes shall be interpreted in the context of a basic layout model that describes the arrangement of structure elements on the page.
                *              This model is designed to capture the general intent of the document’s underlying structure and does not necessarily correspond to the one actually used for page layout by the application creating the document. 
                *              (The PDF content stream specifies the exact appearance.) The goal is to provide sufficient information for conforming readers to make their own layout decisions while preserving the authoring application’s intent as closely as their own layout models allow.
                *
                *NOTE 2        The Tagged PDF layout model resembles the ones used in markup languages such as HTML, CSS, XSL, and RTF, but does not correspond exactly to any of them. 
                *              The model is deliberately defined loosely to allow reasonable latitude in the interpretation of structure elements and attributes when converting to other document formats.
                *              Some degree of variation in the resulting layout from one format to another is to be expected.
                *
                *The standard structure types are divided into four main categories according to the roles they play in page layout:
                *
                *  •   Grouping elements(see 14.8.4.2, “Grouping Elements”) group other elements into sequences or hierarchies but hold no content directly and have no direct effect on layout.
                *
                *  •   Block - level structure elements(BLSEs) (see 14.8.4.3, “Block - Level Structure Elements”) describe the overall layout of content on the page, proceeding in the block-progression direction.
                *
                *  •   Inline-level structure elements (ILSEs) (see 14.8.4.4, “Inline-Level Structure Elements”) describe the layout of content within a BLSE, proceeding in the inline-progression direction.
                *
                *  •   Illustration elements (see 14.8.4.5, “Illustration Elements”) are compact sequences of content, in page content order, that are considered to be unitary objects with respect to page layout. An illustration can be treated as either a BLSE or an ILSE.
                *
                *The meaning of the terms block-progression direction and inline-progression direction depends on the writing system in use, as specified by the standard attribute WritingMode (see 14.8.5.4.2, “General Layout Attributes”). 
                *In Western writing systems, the block direction is from top to bottom and the inline direction is from left to right. 
                *Other writing systems use different directions for laying out content.
                *
                *Because the progression directions can vary depending on the writing system, edges of areas and directions on the page are identified by terms that are neutral with respect to the progression order rather than by familiar terms such as up, down, left, and right. 
                *Block layout proceeds from before to after, inline from start to end.
                *Thus, for example, in Western writing systems, the before and after edges of a reference area are at the top and bottom, respectively, and the start and end edges are at the left and right.
                *Another term, shift direction(the direction of shift for a superscript), refers to the direction opposite that for block progression—that is, from after to before(in Western writing systems, from bottom to top).
                *
                *BLSEs shall be stacked within a reference area in block - progression order.
                *In general, the first BLSE shall beplaced against the before edge of the reference area.Subsequent BLSEs shall be stacked against preceding ones, progressing toward the after edge, until no more BLSEs fit in the reference area.
                *If the overflowing BLSE allows itself to be split—such as a paragraph that can be split between lines of text—a portion of it may be included in the current reference area and the remainder carried over to a subsequent reference area(either elsewhere on the same page or on another page of the document).
                *Once the amount of content that fits in a reference area is determined, the placements of the individual BLSEs may be adjusted to bias the placement toward the before edge, the middle, or the after edge of the reference area, or the spacing within or between BLSEs may be adjusted to fill the full extent of the reference area.
                *
                *BLSEs may be nested, with child BLSEs stacked within a parent BLSE in the same manner as BLSEs within a reference area.
                *Except in a few instances noted(the BlockAlign and InlineAlign elements), such nesting of BLSEs does not result in the nesting of reference areas; a single reference area prevails for all levels of nested BLSEs.
                *
                *Within a BLSE, child ILSEs shall be packed into lines. Direct content items—those that are immediate children of a BLSE rather than contained within a child ILSE—shall be implicitly treated as ILSEs for packing purposes. 
                *Each line shall be treated as a synthesized BLSE and shall be stacked within the parent BLSE. 
                *Lines may be intermingled with other BLSEs within the parent area. 
                *This line-building process is analogous to the stacking of BLSEs within a reference area, except that it proceeds in the inline-progression rather than the block-progression direction: a line shall be packed with ILSEs beginning at the start edge of the containing BLSE and continuing until the end edge shall be reached and the line is full. 
                *The overflowing ILSE may allow itself to be broken at linguistically determined or explicitly marked break points (such as hyphenation points within a word), and the remaining fragment shall be carried over to the next line.
                *
                *Certain values of an element’s Placement attribute remove the element from the normal stacking or packing process and allow it instead to float to a specified edge of the enclosing reference area or parent BLSE; see “General Layout Attributes” in 14.8.5.4, “Layout Attributes,” for further discussion.
                *
                *Two enclosing rectangles shall be associated with each BLSE and ILSE(including direct content items that aretreated implicitly as ILSEs):
                *
                *  •   The content rectangle shall be derived from the shape of the enclosed content and defines the bounds used for the layout of any included child elements.
                *
                *  •   The allocation rectangle includes any additional borders or spacing surrounding the element, affecting how it shall be positioned with respect to adjacent elements and the enclosing content rectangle or reference area.
                *
                *The definitions of these rectangles shall be determined by layout attributes associated with the structure element; see 14.8.5.4.5, “Content and Allocation Rectangles” for further discussion.
                */
            
            /*14.8.4 Standard Structure Types
            */

                /*14.8.4.1 General
                *
                *Tagged PDF’s standard structure types characterize the role of a content element within the document and, in conjunction with the standard structure attributes(described in 14.8.5, “Standard Structure Attributes”), how that content is laid out on the page.
                *As discussed in 14.7.3, “Structure Types,” the structure type of a logical structure element shall be specified by the S entry in its structure element dictionary. 
                *To be considered a standard structure type, this value shall be either:
                *
                *  •   One of the standard structure type names described in 14.8.4.2, “Grouping Elements.”
                *
                *  •   An arbitrary name that shall be mapped to one of the standard names by the document’s role map(see 14.7.3, “Structure Types”), possibly through multiple levels of mapping.
                *
                *NOTE 1        Beginning with PDF 1.5, an element name is always mapped to its corresponding name in the role map, if there is one, even if the original name is one of the standard types.
                *              This is done to allow the element, for example, to represent a tag with the same name as a standard role, even though its use differs from the standard role.
                *
                *Ordinarily, structure elements having standard structure types shall be processed the same way whether the type is expressed directly or is determined indirectly from the role map.
                *However, some conforming readersmay ascribe additional semantics to nonstandard structure types, even though the role map associates them with standard ones.
                *
                *NOTE 2        For instance, the actual values of the S entries may be used when exporting to a tagged representation such as XML, and the corresponding role - mapped values shall be used when converting to presentation formats such as HTML or RTF, or for purposes such as reflow or accessibility to users with disabilities.
                *
                *NOTE 3        Most of the standard element types are designed primarily for laying out text; the terminology reflects this usage.
                *              However, a layout may in fact include any type of content, such as path or image objects.
                *
                *The content items associated with a structure element shall be laid out on the page as if they were blocks of text (for a BLSE) or characters within a line of text (for an ILSE).
                */

                /*14.8.4.2  Grouping Elements
                *
                *Grouping elements shall be used solely to group other structure elements; they are not directly associated with content items. 
                *Table 333 describes the standard structure types for elements in this category.H.8, “Structured Elements That Describe Hierarchical Lists” provides an example of nested table of content items.
                *
                *In a tagged PDF document, the structure tree shall contain a single top - level element; that is, the structure tree root(identified by the StructTreeRoot entry in the document catalogue) shall have only one child in its K(kids) array.
                *If the PDF file contains a complete document, the structure type Document should be used for this top - level element in the logical structure hierarchy.
                *If the file contains a well - formed document fragment, one of the structure types Part, Art, Sect, or Div may be used instead.
                *
                *Table 333 - Standard structure types for grouping elements
                *
                *              [Structure type]                [Description]
                *
                *              Document                        (Document) A complete document. This is the root element of any structure tree containing multiple parts or multiple articles.
                *
                *              Part                            (Part) A large-scale division of a document. This type of element is appropriate for grouping articles or sections.
                *
                *              Art                             (Article) A relatively self-contained body of text constituting a single narrative or exposition. 
                *                                              Articles should be disjoint; that is, they should not contain other articles as constituent elements.
                *
                *              Sect                            (Section) A container for grouping related content elements.
                *
                *                                              NOTE 1      For example, a section might contain a heading, several introductory paragraphs, and two or more other sections nested within it as subsections.
                *
                *              Div                             (Division) A generic block-level element or group of elements.
                *
                *              BlockQuote                      (Block quotation) A portion of text consisting of one or more paragraphs attributed to someone other than the author of the surrounding text.
                *
                *              Caption                         (Caption) A brief portion of text describing a table or figure.
                *
                *              TOC                             (Table of contents) A list made up of table of contents item entries (structure type TOCI) and/or other nested table of contents entries (TOC).
                *                                              A TOC entry that includes only TOCI entries represents a flat hierarchy. 
                *                                              A TOC entry that includes other nested TOC entries(and possibly TOCI entries) represents a more complex hierarchy.
                *                                              Ideally, the hierarchy of a top level TOC entry reflects the structure of the main body of the document.
                *                                              
                *                                              NOTE 2      Lists of figures and tables, as well as bibliographies, can be treated as tables of contents for purposes of the standard structure types.
                *
                *              TOCI                            (Table of contents item) An individual member of a table of contents. This entry’s children may be any of the following structure types:
                *                      
                *                                              Lbl             A label(see “List Elements” in 14.8.4.3, “Block - Level Structure Elements”)
                *
                *                                              Reference       A reference to the title and the page number(see “Inline - Level Structure Elements” in 14.8.4.4, “Inline - Level Structure Elements”)
                *
                *                                              NonStruct       Non - structure elements for wrapping a leader artifact(see “Grouping Elements” in 14.8.4.2, “Grouping Elements”).
                *
                *                                              P               Descriptive text(see “Paragraphlike Elements” 14.8.4.3, “Block - Level Structure Elements”)
                *
                *                                              TOC             Table of content elements for hierarchical tables of content, as described for the TOC entry
                *
                *              Index                           (Index) A sequence of entries containing identifying text accompanied by reference elements (structure type Reference; see 14.8.4.4, “Inline-Level Structure Elements”) that point out occurrences of the specified text in the main body of a document.
                *
                *              NonStruct                       (Nonstructural element) A grouping element having no inherent structural significance; it serves solely for grouping purposes. 
                *                                              This type of element differs from a division (structure type Div) in that it shall not be interpreted or exported to other document formats; however, its descendants shall be processed normally.
                *
                *              Private                         (Private element) A grouping element containing private content belonging to the application producing it. 
                *                                              The structural significance of this type of element is unspecified and shall be determined entirely by the conforming writer. 
                *                                              Neither the Private element nor any of its descendants shall be interpreted or exported to other document formats.
                */

                /*14.8.4.3 Block-Level Structure Elements
                */

                    /*14.8.4.3.1 General
                *
                *A block - level structure element(BLSE) is any region of text or other content that is laid out in the block-progression direction, such as a paragraph, heading, list item, or footnote.A structure element is a BLSE if its structure type(after role mapping, if any) is one of those listed in Table 334.
                *All other standard structure types shall be treated as ILSEs, with the following exceptions:
                *
                *  •   TR (Table row), TH (Table header), TD (Table data), THead (Table head), TBody (Table body), and TFoot (Table footer), which shall be used to group elements within a table and shall be considered neither BLSEs nor ILSEs
                *
                *  •   Elements with a Placement attribute(see “General Layout Attributes” in 14.8.5.4, “Layout Attributes”) other than the default value of Inline
                *
                *Table 334 - Block-level structure elements
                *
                *              [Category]                          [Structure types]
                *
                *              Paragraphlike elements              P       H1      H4
                *                                                  H       H2      H5
                *                                                          H3      H6
                *
                *              List elements                       L       Lbl
                *                                                  Ll      LBody
                *
                *              Table element                       Table
                *
                *In many cases, a BLSE may appear as one compact, contiguous piece of page content; in other cases, it may be discontiguous.
                *
                *NOTE      Examples of the latter include a BLSE that extends across a page boundary or is interrupted in the page content order by another, nested BLSE or a directly included footnote. 
                *          When necessary, Tagged conforming readers can recognize such fragmented BLSEs from the logical structure and use this information to reassemble them and properly lay them out.
                */

                    /*14.8.4.3.2 Paragraphlike Elements
                *
                *Table 335 describes structure types for paragraphlike elements that consist of running text and other content laid out in the form of conventional paragraphs(as opposed to more specialized layouts such as lists and tables).
                *
                *Table 335 - Standard structure types for paragraphlike elements
                *
                *              [Structure Type]                        [Description]
                *
                *              H                                       (Heading) A label for a subdivision of a document’s content. It should be the first child of the division that it heads.
                *
                *              H1-H6                                   Headings with specific levels, for use in conforming writers that cannot hierarchically nest their sections and thus cannot determine the level of a heading from its level of nesting.
                *
                *              P                                       (Paragraph) A low-level division of text.
                */

                    /*14.8.4.3.3 List Elements
                *
                *Table 336 describes structure types for organizing the content of lists.H.8, “Structured Elements That Describe Hierarchical Lists” provides an example of nested list entries.
                *
                *Table 336 - Standard structure types for list elements
                *
                *              [Structure Type]                        [Description]
                *
                *              L                                       (List) A sequence of items of like meaning and importance. Its immediate children should be an optional caption (structure type Caption; see 14.8.4.2, “Grouping Elements”) followed by one or more list items (structure type LI).
                *
                *              LI                                      (List item) An individual member of a list. Its children may be one or more labels, list bodies, or both (structure types Lbl or LBody).
                *
                *              Lbl                                     (Label) A name or number that distinguishes a given item from others in the same list or other group of like items.
                *
                *                                                      NOTE    In a dictionary list, for example, it contains the term being defined; in a bulleted or numbered list, it contains the bullet character or the number of the list item and associated punctuation.
                *
                *              LBody                                   (List body) The descriptive content of a list item. In a dictionary list, for example, it contains the definition of the term. 
                *                                                      It may either contain the content directly or have other BLSEs, perhaps including nested lists, as children.
                */

                    /*14.8.4.3.4 Table Elements
                *
                *The structure types described in Table 337 shall be used for organizing the content of tables.
                *
                *NOTE 1    Strictly speaking, the Table element is a BLSE; the others in this table are neither BLSEs or ILSEs.
                *
                *Table 337 - Standard structure types for table elements
                *
                *              [Structure Type]                        [Description]
                *
                *              Table                                   (Table) A two-dimensional layout of rectangular data cells, possibly having a complex substructure. 
                *                                                      It contains either one or more table rows (structure type TR) as children; or an optional table head (structure type THead) followed by one or more table body elements (structure type TBody) and an optional table footer (structure type TFoot). 
                *                                                      In addition, a table may have a caption (structure type Caption; see 14.8.4.2, “Grouping Elements”) as its first or last child.
                *
                *              TR                                      (Table row) A row of headings or data in a table. It may contain table header cells and table data cells (structure types TH and TD).
                *
                *              TH                                      (Table header cell) A table cell containing header text describing one or more rows or columns of the table.
                *
                *              TD                                      (Table data cell) A table cell containing data that is part of the table’s content.
                *
                *              THead                                   (Table header row group; PDF 1.5) A group of rows that constitute the header of a table. 
                *                                                      If the table is split across multiple pages, these rows may be redrawn at the top of each table fragment (although there is only one THead element).
                *
                *              TBody                                   (Table body row group; PDF 1.5) A group of rows that constitute the main body portion of a table. If the table is split across multiple pages, the body area may be broken apart on a row boundary. 
                *                                                      A table may have multiple TBody elements to allow for the drawing of a border or background for a set of rows.
                *
                *              TFoot                                   (Table footer row group; PDF 1.5) A group of rows that constitute the footer of a table. 
                *                                                      If the table is split across multiple pages, these rows may be redrawn at the bottom of each table fragment (although there is only one TFoot element.)
                *
                *NOTE 2        The association of headers with rows and columns of data is typically determined heuristically by applications. 
                *              Such heuristics may fail for complex tables; the standard attributes for tables shown in Table 348 can be used to make the association explicit.
                */

                    /*14.8.4.3.5 Usage Guidelines for Block-Level Structure
                *
                *Because different conforming readers use PDF’s logical structure facilities in different ways, Tagged PDF does not enforce any strict rules regarding the order and nesting of elements using the standard structure types. 
                *Furthermore, each export format has its own conventions for logical structure. 
                *However, adhering to certain general guidelines helps to achieve the most consistent and predictable interpretation among different Tagged PDF consumers.
                *
                *As described under 14.8.4.2, “Grouping Elements,” a Tagged PDF document may have one or more levels of grouping elements, such as Document, Part, Art (Article), Sect (Section), and Div (Division). 
                *The descendants of these should be BLSEs, such as H (Heading), P (Paragraph), and L (List), that hold the actual content. 
                *Their descendants, in turn, should be either content items or ILSEs that further describe the content.
                *
                *NOTE 1        As noted earlier, elements with structure types that would ordinarily be treated as ILSEs may have a Placement attribute(see “General Layout Attributes” in 14.8.5.4, “Layout Attributes”) that causes them to be treated as BLSEs instead.
                *              Such elements may be included as BLSEs in the same manner as headings and paragraphs.
                *
                *The block-level structure may follow one of two principal paradigms:
                *
                *  •   Strongly structured. The grouping elements nest to as many levels as necessary to reflect the organization of the material into articles, sections, subsections, and so on.
                *      At each level, the children of the grouping element should consist of a heading(H), one or more paragraphs(P) for content at that level, and perhaps one or more additional grouping elements for nested subsections.
                *
                *  •   Weakly structured.The document is relatively flat, having perhaps only one or two levels of grouping elements, with all the headings, paragraphs, and other BLSEs as their immediate children.
                *      In this case, the organization of the material is not reflected in the logical structure; however, it may be expressed by the use of headings with specific levels(H1–H6).
                *
                *NOTE 2        The strongly structured paradigm is used by some rich document models based on XML.
                *              The weakly structured paradigm is typical of documents represented in HTML.
                *
                *Lists and tables should be organized using the specific structure types described under “List Elements” in 14.8.4.3, “Block - Level Structure Elements,” and “Table Elements” in 14.8.4.3, “Block - Level Structure Elements”. 
                *Likewise, tables of contents and indexes should be structured as described for the TOC and Index structure types under “Grouping Elements” in 14.8.4.2, “Grouping Elements.”
                */

                /*14.8.4.4 Inline-Level Structure Elements
                */

                    /*14.8.4.4.1 General
                *
                *An inline - level structure element(ILSE) contains a portion of text or other content having specific styling characteristics or playing a specific role in the document. 
                *Within a paragraph or other block defined by a containing BLSE, consecutive ILSEs—possibly intermixed with other content items that are direct children of the parent BLSE—are laid out consecutively in the inline-progression direction(left to right in Western writing systems).
                *The resulting content may be broken into multiple lines, which in turn shall be stacked in the block-progression direction.
                *An ILSE may in turn contain a BLSE, which shall be treated as a unitary item of layout in the inline direction.Table 338 lists the standard structure types for ILSEs.
                *
                *Table 338 - Standard structure types for inline-level structure elements
                *
                *              [Structure Type]                [Description]
                *
                *              Span                            (Span) A generic inline portion of text having no particular inherent characteristics. 
                *                                              It can be used, for example, to delimit a range of text with a given set of styling attributes.
                *
                *                                              NOTE 1      Not all inline style changes need to be identified as a span.
                *                                                          Text colour and font changes(including modifiers such as bold, italic, and small caps) need not be so marked, since these can be derived from the PDF content(see “Font Characteristics” in 14.8.2.4, “Extraction of Character Properties”).
                *                                                          However, it is necessary to use a span to apply explicit layout attributes such as LineHeight, BaselineShift, or TextDecorationType (see “Layout Attributes for ILSEs” in 14.8.5.4, “Layout Attributes”).
                *
                *                                              NOTE 2      Marked-content sequences having the tag Span are also used to carry certain accessibility properties(Alt, ActualText, Lang, and E; see 14.9, “Accessibility Support”). 
                *                                                          Such sequences lack an MCIDproperty and are not associated with any structure element.
                *                                                          This use of the Span marked-content tag is distinct from its use as a structure type.
                *
                *              Quote                           (Quotation) An inline portion of text attributed to someone other than the author of the surrounding text.
                *
                *                                              The quoted text should be contained inline within a single paragraph.
                *                                              This differs from the block-level element BlockQuote(see 14.8.4.2, “Grouping Elements”), which consists of one or more complete paragraphs(or other elements presented as if they were complete paragraphs).
                *
                *              Note                            (Note) An item of explanatory text, such as a footnote or an endnote, that is referred to from within the body of the document. 
                *                                              It may have a label (structure type Lbl; see “List Elements” in 14.8.4.3, “Block-Level Structure Elements”) as a child. 
                *                                              The note may be included as a child of the structure element in the body text that refers to it, or it may be included elsewhere (such as in an endnotes section) and accessed by means of a reference (structure type Reference).
                *                                              Tagged PDF does not prescribe the placement of footnotes in the page content order. 
                *                                              They may be either inline or at the end of the page, at the discretion of theconforming writer.
                *
                *              Reference                       (Reference) A citation to content elsewhere in the document.
                *
                *              BibEntry                        (Bibliography entry) A reference identifying the external source of some cited content. 
                *                                              It may contain a label (structure type Lbl; see “List Elements” in 14.8.4.3, “Block-Level Structure Elements”) as a child.
                *                                              Although a bibliography entry is likely to include component parts identifying the cited content’s author, work, publisher, and so forth, no standard structure types are defined at this level of detail.
                *
                *              Code                            (Code) A fragment of computer program text.
                *
                *              Link                            (Link) An association between a portion of the ILSE’s content and a corresponding link annotation or annotations (see 12.5.6.5, “Link Annotations”). 
                *                                              Its children should be one or more content items or child ILSEs and one or more object references (see 14.7.4.3, “PDF Objects as Content Items”) identifying the associated link annotations. 
                *                                              See “Link Elements” in 14.8.4.3, “Block-Level Structure Elements,” for further discussion.
                *
                *              Annot                           (Annotation; PDF 1.5) An association between a portion of the ILSE’s content and a corresponding PDF annotation (see 12.5, “Annotations”). 
                *                                              Annot shall be used for all PDF annotations except link annotations (see the Link element) and widget annotations (see the Form element in Table 340). 
                *                                              See “Annotation Elements”14.8.4.4, “Inline-Level Structure Elements,” for further discussion.
                *
                *              Ruby                            (Ruby; PDF 1.5) A side-note (annotation) written in a smaller text size and placed adjacent to the base text to which it refers. 
                *                                              A Ruby element may also contain the RB, RT, and RP elements. 
                *                                              See “Ruby and Warichu Elements” in14.8.4.4, “Inline-Level Structure Elements,” for more details.
                *
                *              Warichu                         (Warichu; PDF 1.5) A comment or annotation in a smaller text size and formatted onto two smaller lines within the height of the containing text line and placed following (inline) the base text to which it refers. 
                *                                              A Warichu element may also contain the WT and WP elements. 
                *                                              See “Ruby and Warichu Elements” in14.8.4.4, “Inline-Level Structure Elements,” for more details.
                */

                    /*14.8.4.4.2 Link Elements
                *
                *NOTE 1    Link annotations(like all PDF annotations) are associated with a geometric region of the page rather than with a particular object in its content stream.
                *          Any connection between the link and the content is based solely on visual appearance rather than on an explicitly specified association. 
                *          For this reason, link annotations alone are not useful to users with visual impairments or to applications needing to determine which content can be activated to invoke a hypertext link.
                *
                *Tagged PDF link elements(structure type Link) use PDF’s logical structure facilities to establish the association between content items and link annotations, providing functionality comparable to HTML hypertext links. 
                *The following items may be children of a link element:
                *
                *  •   One or more content items or other ILSEs(except other links)
                *
                *  •   Object references(see 14.7.4.3, “PDF Objects as Content Items”) to one or more link annotations associated with the content
                *
                *When a Link structure element describes a span of text to be associated with a link annotation and that span wraps from the end of one line to the beginning of another, the Link structure element shall include a single object reference that associates the span with the associated link annotation.
                *Further, the link annotation shalluse the QuadPoint entry to denote the active areas on the page.
                *
                *EXAMPLE 1     The Link structure element references a link annotation that includes a QuadPoint entry that boxes the strings “with a” and “link”. 
                *              That is, the QuadPoint entry contains 16 numbers: the first 8 numbers describe a quadrilateral for “with a”, and the next 8 describe a quadrilateral for “link.”
                *
                *(see Example 1 image on page 589)
                *
                *NOTE 2    Beginning with PDF 1.7, use of the Link structure element to enclose multiple link annotations is deprecated.
                *
                *EXAMPLE 2     Consider the following fragment of HTML code, which produces a line of text containing a hypertext link:
                *
                *              < html >
                *                  < body >
                *                      < p >
                *                      Here is some text < a href = http:*www.adobe.com>with a link</a> inside.
                *                  </ body >
                *              </ html >
                *
                *              This code sample shows an equivalent fragment of PDF using a link element, whose text it displays in blue and underlined.
                *
                *              / P << / MCID 0 >>                              % Marked - content sequence 0(paragraph)
                *                  BDC                                         % Begin marked - content sequence
                *                      BT                                      % Begin text object
                *                          / T1_0 1 Tf                         % Set text font and size
                *                          14 0 0 14 10.000 753.976 Tm         % Set text matrix
                *                          0.0 0.0 0.0 rg                      % Set nonstroking colour to black
                *                          (Here is some text ) Tj             % Show text preceding link
                *                      ET                                      % End text object
                *                  EMC                                         % End marked - content sequence
                *              / Link << / MCID 1 >>                           % Marked - content sequence 1(link)
                *                  BDC                                         % Begin marked - content sequence
                *                      0.7 w                                   % Set line width
                *                      [] 0 d                                  % Solid dash pattern
                *                      111.094 751.8587 m                      % Move to beginning of underline
                *                      174.486 751.8587 l                      % Draw underline
                *                      0.0 0.0 1.0 RG                          % Set stroking colour to blue
                *                      S                                       % Stroke underline
                *                      BT                                      % Begin text object
                *                          14 0 0 14 111.094 753.976 Tm        % Set text matrix
                *                          0.0 0.0 1.0 rg                      % Set nonstroking colour to blue
                *                          (with a link) Tj                    % Show text of link
                *                      ET                                      % End text object
                *                  EMC                                         % End marked - content sequence
                *               /P << /MCID 2 >>                               % Marked-content sequence 2 (paragraph)
                *                  BDC                                         % Begin marked - content sequence
                *                      BT                                      % Begin text object
                *                          14 0 0 14 174.486 753.976 Tm        % Set text matrix
                *                          0.0 0.0 0.0 rg                      % Set nonstroking colour to black
                *                          (inside.) Tj                        % Show text following link
                *                      ET                                      % End text object
                *                  EMC                                         % End marked - content sequence
                *
                *EXAMPLE 3         This example shows an excerpt from the associated logical structure hierarchy.
                *
                *                  501 0 obj                                   % Structure element for paragraph
                *                      << / Type / StructElem
                *                         / S / P
                *                          . . .
                *                         / K[0                                % Three children: marked - content sequence 0
                *                             502 0 R                          % Link
                *                             2                                % Marked - content sequence 2
                *                            ]
                *                      >>
                *                  endobj
                *                  502 0 obj                                   % Structure element for link
                *                      << / Type / StructElem
                *                         / S / Link
                *                          . . .
                *                         / K[1                                % Two children: marked - content sequence 1
                *                             503 0 R                          % Object reference to link annotation
                *                            ]
                *                       >>
                *                  endobj
                *                  503 0 obj                                   % Object reference to link annotation
                *                      << / Type / OBJR
                *                         / Obj 600 0 R                        % Link annotation(not shown)
                *                      >>
                *                  endobj
                */

                    /*14.8.4.4.3 Annotation Elements
                *
                *Tagged PDF annotation elements(structure type Annot; PDF 1.5) use PDF’s logical structure facilities to establish the association between content items and PDF annotations. 
                *Annotation elements shall be used for all types of annotations other than links(see “Link Elements” in 14.8.4.3, “Block - Level Structure Elements”) and forms(see Table 340).
                *
                *The following items may be children of an annotation element:
                *
                *  •   Object references(see 14.7.4.3, “PDF Objects as Content Items”) to one or more annotation dictionaries
                *
                *  •   Optionally, one or more content items(such as marked - content sequences) or other ILSEs(except other annotations) associated with the annotations
                *
                *If an Annot element has no children other than object references, its rendering shall be defined by the appearance of the referenced annotations, and its text content shall be treated as if it were a Span element. 
                *It may have an optional BBox attribute; if supplied, this attribute overrides the rectangle specified by the annotation dictionary’s Rect entry.
                *
                *If the Annot element has children that are content items, those children represent the displayed form of the annotation, and the appearance of the associated annotation may also be applied(for example, with a Highlight annotation).
                *
                *There may be multiple children that are object references to different annotations, subject to the constraint that the annotations shall be the same except for their Rect entry.
                *This is much the same as is done for the Link element; it allows an annotation to be associated with discontiguous pieces of content, such as line - wrapped text.
                */

                    /*14.8.4.4.4 Ruby and Warichu Elements
                *
                *Ruby text is a side note, written in a smaller text size and placed adjacent to the base text to which it refers.
                *It is used in Japanese and Chinese to describe the pronunciation of unusual words or to describe such items as abbreviations and logos.
                *
                *Warichu text is a comment or annotation, written in a smaller text size and formatted onto two smaller lines within the height of the containing text line and placed following(inline) the base text to which it refers.
                *It is used in Japanese for descriptive comments and for ruby annotation text that is too long to be aesthetically formatted as a ruby.
                *
                *Table 339 - Standard structure types for Ruby and Warichu elements (PDF 1.5)
                *
                *          [Structure Type]                [Description]
                *
                *          Ruby                            (Ruby) The wrapper around the entire ruby assembly. 
                *                                          It shall contain one RB element followed by either an RT element or a three-element group consisting of RP, RT, and RP. 
                *                                          Ruby elements and their content elements shall not break across multiple lines.
                *
                *          RB                              (Ruby base text) The full-size text to which the ruby annotation is applied. 
                *                                          RB may contain text, other inline elements, or a mixture of both. 
                *                                          It may have the RubyAlignattribute.
                *
                *          RT                              (Ruby annotation text) The smaller-size text that shall be placed adjacent to the ruby base text. 
                *                                          It may contain text, other inline elements, or a mixture of both. 
                *                                          It may have the RubyAlign and RubyPosition attributes.
                *
                *          RP                              (Ruby punctuation) Punctuation surrounding the ruby annotation text. 
                *                                          It is used only when a ruby annotation cannot be properly formatted in a ruby style and instead is formatted as a normal comment, or when it is formatted as a warichu. 
                *                                          It contains text (usually a single LEFT or RIGHT PARENTHESIS or similar bracketing character).
                *
                *          Warichu                         (Warichu) The wrapper around the entire warichu assembly. 
                *                                          It may contain a three-element group consisting of WP, WT, and WP. 
                *                                          Warichu elements (and their content elements) may wrap across multiple lines, according to the warichu breaking rules described in the Japanese Industrial Standard (JIS) X 4051-1995.
                *
                *          WT                              (Warichu text) The smaller-size text of a warichu comment that is formatted into two lines and placed between surrounding WP elements.
                *
                *          WP                              (Warichu punctuation) The punctuation that surrounds the WT text. 
                *                                          It contains text (usually a single LEFT or RIGHT PARENTHESIS or similar bracketing character). 
                *                                          According to JIS X 4051-1995, the parentheses surrounding a warichu may be converted to a SPACE (nominally 1/4 EM in width) at the discretion of the formatter.
                */
        
                /*14.8.4.5 Illustration Elements
                *
                *Tagged PDF defines an illustration element as any structure element whose structure type(after role mapping, if any) is one of those listed in Table 340.
                *The illustration’s content shall consist of one or more complete graphics objects.
                *It shall not appear between the BT and ET operators delimiting a text object(see 9.4, “Text Objects”).
                *It may include clipping only in the form of a contained marked clipping sequence, as defined in 14.6.3, “Marked Content and Clipping.” 
                *In Tagged PDF, all such marked clipping sequences shall carry the marked - content tag Clip.
                *
                *Table 340 - Standard structure types for illustration elements
                *
                *          [Structure Type]                [Description]
                *
                *          Figure                          (Figure) An item of graphical content. Its placement may be specified with the Placement layout attribute (see “General Layout Attributes” in 14.8.5.4, “Layout Attributes”).
                *
                *          Formula                         (Formula) A mathematical formula.
                *                                          This structure type is useful only for identifying an entire content element as a formula.
                *                                          No standard structure types are defined for identifying individual components within the formula.
                *                                          From a formatting standpoint, the formula shall be treated similarly to a figure(structure type Figure).
                *
                *          Form                            (Form) A widget annotation representing an interactive form field (see 12.7, “Interactive Forms”). 
                *                                          If the element contains a Role attribute, it may contain content items that represent the value of the (non-interactive) form field. 
                *                                          If the element omits a Role attribute (see Table 348), it shall have only one child: an object reference (see 14.7.4.3, “PDF Objects as Content Items”) identifying the widget annotation. 
                *                                          The annotations’ appearance stream (see 12.5.5, “Appearance Streams”) shall describe the appearance of the form element.
                *
                *An illustration may have logical substructure, including other illustrations. 
                *For purposes of reflow, however, it shall be moved (and perhaps resized) as a unit, without examining its internal contents. 
                *To be useful for reflow, it shall have a BBox attribute. 
                *It may also have Placement, Width, Height, and BaselineShift attributes (see 14.8.5.4, “Layout Attributes”).
                *
                *Often an illustration is logically part of, or at least attached to, a paragraph or other element of a document. 
                *Any such containment or attachment shall be represented through the use of the Figure structure type. 
                *The Figure element indicates the point of attachment, and its Placement attribute describes the nature of the attachment. 
                *An illustration element without a Placement attribute shall be treated as an ILSE and laid out inline.
                *For accessibility to users with disabilities and other text extraction purposes, an illustration element should have an Alt entry or an ActualText entry(or both) in its structure element dictionary(see 14.9.3, “Alternate Descriptions,” and 14.9.4, “Replacement Text”). 
                *Alt is a description of the illustration, whereas ActualTextgives the exact text equivalent of a graphical illustration that has the appearance of text.
                */

            /*14.8.5 Standard Structure Attributes
                */

                /*14.8.5.1 General
                *
                *In addition to the standard structure types, Tagged PDF defines standard layout and styling attributes for structure elements of those types.
                *These attributes enable predictable formatting to be applied during operations such as reflow and export of PDF content to other document formats.
                *
                *As discussed in 14.7.5, “Structure Attributes,” attributes shall be defined in attribute objects, which are dictionaries or streams attached to a structure element in either of two ways:
                *
                *  •   The A entry in the structure element dictionary identifies an attribute object or an array of such objects.
                *
                *  •   The C entry in the structure element dictionary gives the name of an attribute class or an array of such names.
                *      The class name is in turn looked up in the class map, a dictionary identified by the ClassMap entry in the structure tree root, yielding an attribute object or array of objects corresponding to the class.
                *
                *In addition to the standard structure attributes described in 14.8.5.2, “Standard Attribute Owners,” there are several other optional entries—Lang, Alt, ActualText, and E—that are described in 14.9, “Accessibility Support,” but are useful to other PDF consumers as well.
                *They appear in the following places in a PDF file(rather than in attribute dictionaries) :
                *
                *  •   As entries in the structure element dictionary(see Table 323)
                *
                *  •   As entries in property lists attached to marked-content sequences with a Span tag(see 14.6, “Marked Content”)
                *
                *The Example in 14.7.6, “Example of Logical Structure,” illustrates the use of standard structure attributes.
                */

                /*14.8.5.2 Standard Attribute Owners
                *
                *Each attribute object has an owner, specified by the object’s O entry, which determines the interpretation of the attributes defined in the object’s dictionary. 
                *Multiple owners may define like - named attributes with different value types or interpretations.
                *Tagged PDF defines a set of standard attribute owners, shown in Table 341.
                *
                *Table 341 - Standard attribute owners
                *
                *          [Owner]                     [Description]
                *
                *          Layout                      Attributes governing the layout of content
                *
                *          List                        Attributes governing the numbering of lists
                *
                *          PrintField                  (PDF 1.7) Attributes governing Form structure elements for non-interactive form fields
                *
                *          Table                       Attributes governing the organization of cells in tables
                *
                *          XML-1.00                    Additional attributes governing translation to XML, version 1.00
                *
                *          HTML-3.20                   Additional attributes governing translation to HTML, version 3.20
                *
                *          HTML-4.01                   Additional attributes governing translation to HTML, version 4.01
                *
                *          OEB-1.00                    Additional attributes governing translation to OEB, version 1.00
                *
                *          RTF-1.05                    Additional attributes governing translation to Microsoft Rich Text Format, version 1.05
                *
                *          CSS-1.00                    Additional attributes governing translation to CSS, version 1.00
                *
                *          CSS-2.00                    Additional attributes governing translation to CSS, version 2.00
                *
                *
                *An attribute object owned by a specific export format, such as XML-1.00, shall be applied only when exporting PDF content to that format. 
                *Such format-specific attributes shall override any corresponding attributes owned by Layout, List, PrintField, or Table. 
                *There may also be additional format-specific attributes; the set of possible attributes is open-ended and is not explicitly specified or limited by Tagged PDF.
                */

                /*14.8.5.3 Attribute Values and Inheritance
                *
                *Some attributes are defined as inheritable.Inheritable attributes propagate down the structure tree; that is, an attribute that is specified for an element shall apply to all the descendants of the element in the structure tree unless a descendent element specifies an explicit value for the attribute.
                *
                *NOTE 1    The description of each of the standard attributes in this sub-clause specifies whether their values are inheritable.
                *
                *An inheritable attribute may be specified for an element for the purpose of propagating its value to child elements, even if the attribute is not meaningful for the parent element.
                *Non -inheritable attributes may be specified only for elements on which they would be meaningful.
                *
                *The following list shows the priority for determining attribute values.
                *A conforming reader determines an attribute’s value to be the first item in the following list that applies:
                *
                *  a)  The value of the attribute specified in the element’s A entry, owned by one of the export formats(such as XML, HTML-3.20, HTML-4.01, OEB-1.0, CSS-1.00, CSS-2.0, and RTF), if present, and if outputting to that format
                *
                *  b)  The value of the attribute specified in the element’s A entry, owned by Layout, PrintField, Table or List, if present
                *
                *  c)  The value of the attribute specified in a class map associated with the element’s C entry, if there is one
                *
                *  d)  The resolved value of the parent structure element, if the attribute is inheritable
                *
                *  e)  The default value for the attribute, if there is one
                *
                *NOTE 2        The attributes Lang, Alt, ActualText, and E do not appear in attribute dictionaries.
                *              The rules governing their application are discussed in 14.9, “Accessibility Support.”
                *
                *There is no semantic distinction between attributes that are specified explicitly and ones that are inherited. 
                *Logically, the structure tree has attributes fully bound to each element, even though some may be inherited from an ancestor element. 
                *This is consistent with the behaviour of properties (such as font characteristics) that are not specified by structure attributes but shall be derived from the content.
                */

                /*14.8.5.4 Layout Attributes
                */

                    /*14.8.5.4.1 General
                *
                *Layout attributes specify parameters of the layout process used to produce the appearance described by a document’s PDF content.
                *Attributes in this category shall be defined in attribute objects whose O(owner) entry has the value Layout(or is one of the format - specific owner names listed in Table 341).
                *
                *NOTE      The intent is that these parameters can be used to reflow the content or export it to some other document format with at least basic styling preserved.
                *
                *Table 342 summarizes the standard layout attributes and the structure elements to which they apply.
                *The following sub - clauses describe the meaning and usage of these attributes.
                *As described in 14.8.5.3, “Attribute Values and Inheritance,” an inheritable attribute may be specified for any element to propagate it to descendants, regardless of whether it is meaningful for that element.
                *
                *Table 342 - Standard layout attributes
                *
                *              [Structure Elements]                [Attributes]                [Inheritable]
                *
                *              Any structure element               Placement                   No
                *                                                  WritingMode                 Yes
                *                                                  BackgroundColor             No
                *                                                  BorderColor                 No
                *                                                  BorderStyle                 Yes
                *                                                  BorderThickness             Yes
                *                                                  Color                       No
                *                                                  Padding
                *
                *              Any BLSE                            SpaceBefore                 No
                *              ILSEs with Placement other          SpaceAfter                  No
                *              than Inline                         StartIndent                 Yes
                *                                                  EndIndent                   Yes
                *
                *              BLSEs containing text               TextIndent                  Yes
                *                                                  TextAlign                   Yes
                *
                *              Illustration elements               BBox                        No
                *              (Figure, Formula, Form)             Width                       No
                *              Table                               Height                      No
                *
                *              TH (Table header)                   Width                       No
                *              TD (Table data)                     Height                      No
                *                                                  BlockAlign                  Yes
                *                                                  InlineAlign                 Yes
                *                                                  TBorderStyle                Yes
                *                                                  TPadding                    Yes
                *
                *              Any ILSE                            LineHeight                  Yes
                *              BLSEs containing ILSEs or           BaselineShift               No
                *              containing direct or nested         TextDecorationType          No
                *              content items                       TextDecorationColor         Yes
                *                                                  TextDecorationThickness     Yes
                *
                *              Grouping elements Art, Sect,        ColumnCount                 No
                *              and Div                             ColumnWidths                No
                *                                                  ColumnGap                   No
                *
                *              Vertical text                       GlyphOrientationVertical    Yes
                *
                *              Ruby text                           RubyAlign                   Yes
                *                                                  RubyPosition                Yes
                */

                    /*14.8.5.4.2 General Layout Attributes
                *
                *The layout attributes described in Table 343 may apply to structure elements of any of the standard types at the block level(BLSEs) or the inline level(ILSEs).
                *
                *Table 343 - Standard layout attributes common to all standard structure types
                *
                *          [Key]               [Type]              [Value]
                *
                *          Placement           name                (Optional; not inheritable) The positioning of the element with respect to the enclosing reference area and other content:
                *
                *                                                  Block       Stacked in the block-progression direction within an enclosing reference area or parent BLSE.
                *
                *                                                  Inline      Packed in the inline-progression direction within an enclosing BLSE.
                *
                *                                                  Before      Placed so that the before edge of the element’s allocation rectangle(see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”) coincides with that of the nearest enclosing reference area.
                *                                                              The element may float, if necessary, to achieve the specified placement.
                *                                                              The element shall be treated as a block occupying the full extent of the enclosing reference area in the inline direction.
                *                                                              Other content shall be stacked so as to begin at the after edge of the element’s allocation rectangle.
                *
                *                                                  Start       Placed so that the start edge of the element’s allocation rectangle(see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”) coincides with that of the nearest enclosing reference area.
                *                                                              The element may float, if necessary, to achieve the specified placement.
                *                                                              Other content that would intrude into the element’s allocation rectangle shall be laid out as a runaround.
                *
                *                                                  End         Placed so that the end edge of the element’s allocation rectangle(see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”) coincides with that of the nearest enclosing reference area.
                *                                                              The element may float, if necessary, to achieve the specified placement.
                *                                                              Other content that would intrude into the element’s allocation rectangle shall be laid out as a runaround.
                *
                *                                                  When applied to an ILSE, any value except Inline shall cause the element to be treated as a BLSE instead. 
                *                                                  Default value: Inline.
                *                                                  Elements with Placement values of Before, Start, or End shall be removed from the normal stacking or packing process and allowed to float to the specified edge of the enclosing reference area or parent BLSE.
                *                                                  Multiple such floating elements may be positioned adjacent to one another against the specified edge of the reference area or placed serially against the edge, in the order encountered.
                *                                                  Complex cases such as floating elements that interfere with each other or do not fit on the same page may be handled differently by different conforming readers.
                *                                                  Tagged PDF merely identifies the elements as floating and indicates their desired placement.
                *
                *          WritingMode         name                (Optional; inheritable) The directions of layout progression for packing of ILSEs (inline progression) and stacking of BLSEs (block progression):
                *
                *                                                  LrTb        Inline progression from left to right; block progression from top to bottom.
                *                                                              This is the typical writing mode for Western writing systems.
                *
                *                                                  RlTb        Inline progression from right to left; block progression from top to bottom.
                *                                                              This is the typical writing mode for Arabic and Hebrew writing systems.
                *
                *                                                  TbRl        Inline progression from top to bottom; block progression from right to left.
                *                                                              This is the typical writing mode for Chinese and Japanese writing systems.
                *
                *                                                  The specified layout directions shall apply to the given structure element and all of its descendants to any level of nesting.
                *                                                  Default value: LrTb.
                *
                *                                                  For elements that produce multiple columns, the writing mode defines the direction of column progression within the reference area: the inline direction determines the stacking direction for columns and the default flow order of text from column to column.
                *                                                  For tables, the writing mode controls the layout of rows and columns: table rows(structure type TR) shall be stacked in the block direction, cells within a row(structure type TD) in the inline direction.
                *
                *                                                  The inline - progression direction specified by the writing mode is subject to local override within the text being laid out, as described in Unicode Standard Annex #9, The Bidirectional Algorithm, available from the Unicode Consortium (see the Bibliography).
                *
                *           BackgroundColor    array               (Optional; not inheritable; PDF 1.5) The colour to be used to fill the background of a table cell or any element’s content rectangle (possibly adjusted by the Padding attribute). 
                *                                                  The value shall be an array of three numbers in the range 0.0 to 1.0, representing the red, green, and blue values, respectively, of an RGB colour space. 
                *                                                  If this attribute is not specified, the element shall be treated as if it were transparent.
                *
                *           BorderColor        array               (Optional; inheritable; PDF 1.5) The colour of the border drawn on the edges of a table cell or any element’s content rectangle (possibly adjusted by the Padding attribute). 
                *                                                  The value of each edge shall be an array of three numbers in the range 0.0 to 1.0, representing the red, green, and blue values, respectively, of an RGB colour space. 
                *                                                  There are two forms:
                *
                *                                                  A single array of three numbers representing the RGB values to apply to all four edges.
                *
                *                                                  An array of four arrays, each specifying the RGB values for one edge of the border, in the order of the before, after, start, and end edges.
                *                                                  A value of null for any of the edges means that it shall not be drawn.
                *
                *                                                  If this attribute is not specified, the border colour for this element shall be the current text fill colour in effect at the start of its associated content.
                *
                *          BorderStyle         array or            (Optional; not inheritable; PDF 1.5) The style of an element’s border. 
                *                              name                Specifies the stroke pattern of each edge of a table cell or any element’s content rectangle (possibly adjusted by the Paddingattribute). 
                *                                                  There are two forms:
                *
                *                                                  •   A name from the list below representing the border style to apply to all four edges.
                *
                *                                                  •   An array of four entries, each entry specifying the style for one edge of the border in the order of the before, after, start, and end edges.
                *                                                      A value of null for any of the edges means that it shall not be drawn.
                *
                *                                                  None        No border.Forces the computed value of BorderThicknessto be 0.
                *
                *                                                  Hidden      Same as None, except in terms of border conflict resolution for table elements.
                *
                *                                                  Dotted      The border is a series of dots.
                *
                *                                                  Dashed      The border is a series of short line segments.
                *
                *                                                  Solid       The border is a single line segment.
                *
                *                                                  Double      The border is two solid lines.The sum of the two lines and the space between them equals the value of BorderThickness.
                *
                *                                                  Groove      The border looks as though it were carved into the canvas.
                *
                *                                                  Ridge       The border looks as though it were coming out of the canvas(the opposite of Groove).
                *
                *                                                  Inset       The border makes the entire box look as though it were embedded in the canvas.
                *
                *                                                  Outset      The border makes the entire box look as though it were coming out of the canvas(the opposite of Inset).
                *                              
                *                                                  Default value: None
                *                      
                *                                                  All borders shall be drawn on top of the box’s background. 
                *                                                  The colour of borders drawn for values of Groove, Ridge, Inset, and Outset shall depend on the structure element’s BorderColor attribute and the colour of the background over which the border is being drawn.
                *
                *                                                  NOTE        Conforming HTML applications may interpret Dotted, Dashed, Double, Groove, Ridge, Inset, and Outset to be Solid.
                *
                *          BorderThickness     number or           (Optional; inheritable; PDF 1.5) The thickness of the border drawn on the edges of a table cell or any element’s content rectangle (possibly adjusted by the Padding attribute). 
                *                              array               The value of each edge shall be a positive number in default user space units representing the border’s thickness (a value of 0 indicates that the border shall not be drawn). 
                *                                                  There are two forms:
                *
                *                                                  A number representing the border thickness for all four edges.
                *
                *                                                  An array of four entries, each entry specifying the thickness for one edge of the border, in the order of the before, after, start, and end edges.
                *                                                  A value of null for any of the edges means that it shall not be drawn.
                *                              
                *          Padding             number or           (Optional; not inheritable; PDF 1.5) Specifies an offset to account for the separation between the element’s content rectangle and the surrounding border (see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”). 
                *                              array               A positive value enlarges the background area; a negative value trims it, possibly allowing the border to overlap the element’s text or graphic.
                *
                *                                                  The value shall be either a single number representing the width of the padding, in default user space units, that applies to all four sides or a 4 - element array of numbers representing the padding width for the before, after, start, and end edge, respectively, of the content rectangle.
                *                                                  Default value: 0.
                *                              
                *          Color               array               (Optional; inheritable; PDF 1.5) The colour to be used for drawing text and the default value for the colour of table borders and text decorations. 
                *                                                  The value shall be an array of three numbers in the range 0.0 to 1.0, representing the red, green, and blue values, respectively, of an RGB colour space. 
                *                                                  If this attribute is not specified, the border colour for this element shall be the current text fill colour in effect at the start of its associated content.
                *
                */

                    /*14.8.5.4.3 Layout Attributes for BLSEs
                *
                *Table 344 describes layout attributes that shall apply only to block - level structure elements(BLSEs).
                *
                *Inline - level structure elements(ILSEs) with a Placement attribute other than the default value of Inline shall be treated as BLSEs and shall also be subject to the attributes described here.
                *
                *Table 344 - Additional standard layout attributes specific to block-level structure elements
                *
                *          [Key]               [Type]              [Value]
                *
                *          SpaceBefore         number              (Optional; not inheritable) The amount of extra space preceding the before edge of the BLSE, measured in default user space units in the block-progression direction. 
                *                                                  This value shall be added to any adjustments induced by the LineHeight attributes of ILSEs within the first line of the BLSE (see “Layout Attributes for ILSEs” in 14.8.5.4, “Layout Attributes”). 
                *                                                  If the preceding BLSE has a SpaceAfter attribute, the greater of the two attribute values shall be used. 
                *                                                  Default value: 0.
                *                                                  This attribute shall be disregarded for the first BLSE placed in a given reference area.
                *
                *          SpaceAfter          number              (Optional; not inheritable) The amount of extra space following the after edge of the BLSE, measured in default user space units in the block-progression direction. 
                *                                                  This value shall be added to any adjustments induced by the LineHeight attributes of ILSEs within the last line of the BLSE (see 14.8.5.4, “Layout Attributes”). 
                *                                                  If the following BLSE has a SpaceBefore attribute, the greater of the two attribute values shall be used. 
                *                                                  Default value: 0.
                *                                                  This attribute shall be disregarded for the last BLSE placed in a given reference area.
                *
                *          StartIndent         number              (Optional; inheritable) The distance from the start edge of the reference area to that of the BLSE, measured in default user space units in the inline-progression direction. 
                *                                                  This attribute shall apply only to structure elements with a Placement attribute of Block or Start (see “General Layout Attributes” in 14.8.5.4, “Layout Attributes”). 
                *                                                  The attribute shall be disregarded for elements with other Placementvalues. Default value: 0.
                *
                *                                                  A negative value for this attribute places the start edge of the BLSE outside that of the reference area.
                *                                                  The results are implementation - dependent and may not be supported by all conforming products that process Tagged PDF or by particular export formats.
                *
                *                                                  If a structure element with a StartIndent attribute is placed adjacent to a floating element with a Placement attribute of Start, the actual value used for the element’s starting indent shall be its own StartIndentattribute or the inline extent of the adjacent floating element, whichever is greater.
                *                                                  This value may be further adjusted by the element’s TextIndent attribute, if any.
                *
                *          EndIndent           number              (Optional; inheritable) The distance from the end edge of the BLSE to that of the reference area, measured in default user space units in the inline-progression direction. 
                *                                                  This attribute shall apply only to structure elements with a Placement attribute of Block or End (see “General Layout Attributes” in 14.8.5.4, “Layout Attributes”). 
                *                                                  The attribute shall be disregarded for elements with other Placement values. 
                *                                                  Default value: 0.
                *
                *                                                  A negative value for this attribute places the end edge of the BLSE outside that of the reference area.
                *                                                  The results are implementation - dependent and may not be supported by all conforming products that process Tagged PDF or by particular export formats.
                *
                *                                                  If a structure element with an EndIndent attribute is placed adjacent to a floating element with a Placement attribute of End, the actual value used for the element’s ending indent shall be its own EndIndentattribute or the inline extent of the adjacent floating element, whichever is greater.
                *
                *          TextIndent          number              (Optional; inheritable; applies only to some BLSEs) The additional distance, measured in default user space units in the inline-progression direction, from the start edge of the BLSE, as specified by StartIndent, to that of the first line of text. 
                *                                                  A negative value shall indicate a hanging indent. 
                *                                                  Default value: 0.
                *                                                  This attribute shall apply only to paragraphlike BLSEs and those of structure types Lbl(Label), LBody(List body), TH(Table header), and TD(Table data), provided that they contain content other than nested BLSEs.
                *
                *          TextAlign           name                (Optional; inheritable; applies only to BLSEs containing text) The alignment, in the inline-progression direction, of text and other content within lines of the BLSE:
                *
                *                                                  Start       Aligned with the start edge.
                *
                *                                                  Center      Centered between the start and end edges.
                *
                *                                                  End         Aligned with the end edge.
                *
                *                                                  Justify     Aligned with both the start and end edges, with internal spacing within each line expanded, if necessary, to achieve such alignment.
                *                                                              The last (or only) line shall be aligned with the start edge only.
                *
                *                                                  Default value: Start.
                *
                *          BBox                rectangle           (Optional for Annot; required for any figure or table appearing in its entirety on a single page; not inheritable) 
                *                                                  An array of four numbers in default user space units that shall give the coordinates of the left, bottom, right, and top edges, respectively, of the element’s bounding box (the rectangle that completely encloses its visible content). 
                *                                                  This attribute shall apply to any element that lies on a single page and occupies a single rectangle.
                *
                *          Width               number or           (Optional; not inheritable; illustrations, tables, table headers, and table cells only; should be used for table cells) 
                *                              name                The width of the element’s content rectangle (see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”), measured in default user space units in the inline-progression direction. 
                *                                                  This attribute shall apply only to elements of structure type Figure, Formula, Form, Table, TH (Table header), or TD (Table data).
                *                                                  The name Auto in place of a numeric value shall indicate that no specific width constraint is to be imposed; the element’s width shall be determined by the intrinsic width of its content. 
                *                                                  Default value: Auto.
                *
                *          Height              number or           (Optional; not inheritable; illustrations, tables, table headers, and table cells only) The height of the element’s content rectangle (see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”), measured in default user space units in the block-progression direction. 
                *                              name                This attribute shall apply only to elements of structure type Figure, Formula, Form, Table, TH (Table header), or TD (Table data).
                *                                                  The name Auto in place of a numeric value shall indicate that no specific height constraint is to be imposed; the element’s height shall be determined by the intrinsic height of its content. 
                *                                                  Default value: Auto.
                *
                *          BlockAlign          name                (Optional; inheritable; table cells only) The alignment, in the block-progression direction, of content within the table cell:
                *
                *                                                  Before      Before edge of the first child’s allocation rectangle aligned with that of the table cell’s content rectangle.
                *
                *                                                  Middle      Children centered within the table cell. The distance between the before edge of the first child’s allocation rectangle and that of the table cell’s content rectangle shall be the same as the distance between the after edge of the last child’s allocation rectangle and that of the table cell’s content rectangle.
                *
                *                                                  After       After edge of the last child’s allocation rectangle aligned with that of the table cell’s content rectangle.
                *
                *                                                  Justify     Children aligned with both the before and after edges of the table cell’s content rectangle.
                *                                                              The first child shall be placed as described for Before and the last child as described for After, with equal spacing between the children.
                *                                                              If there is only one child, it shall be aligned with the before edge only, as for Before.
                *
                *                                                  This attribute shall apply only to elements of structure type TH(Table header) or TD(Table data) and shall control the placement of all BLSEs that are children of the given element.
                *                                                  The table cell’s content rectangle(see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”) shall become the reference area for all of its descendants.
                *                                                  Default value: Before.
                *
                *          InlineAlign         name                (Optional; inheritable; table cells only) The alignment, in the inline-progression direction, of content within the table cell:
                *
                *                                                  Start       Start edge of each child’s allocation rectangle aligned with that of the table cell’s content rectangle.
                *
                *                                                  Center      Each child centered within the table cell.The distance between the start edges of the child’s allocation rectangle and the table cell’s content rectangle shall be the same as the distance between their end edges.
                *
                *                                                  End         End edge of each child’s allocation rectangle aligned with that of the table cell’s content rectangle.
                *
                *                                                  This attribute shall apply only to elements of structure type TH(Table header) or TD(Table data) and controls the placement of all BLSEs that are children of the given element. 
                *                                                  The table cell’s content rectangle(see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”) shall become the reference area for all of its descendants.
                *                                                  Default value: Start.
                *
                *          TBorderStyle        name or             (Optional; inheritable; PDF 1.5) The style of the border drawn on each edge of a table cell. 
                *                              array               Allowed values shall be the same as those specified for BorderStyle (see Table 343). 
                *                                                  If both TBorderStyle and BorderStyle apply to a given table cell, BorderStyle shall supersede TBorderStyle. 
                *                                                  Default value: None.
                *
                *          TPadding            integer or          (Optional; inheritable; PDF 1.5) Specifies an offset to account for the separation between the table cell’s content rectangle and the surrounding border (see “Content and Allocation Rectangles” in 14.8.5.4, “Layout Attributes”). 
                *                              array               If both TPadding and Padding apply to a given table cell, Padding shall supersede TPadding. 
                *                                                  A positive value shall enlarge the background area; a negative value shall trim it, and the border may overlap the element’s text or graphic. 
                *                                                  The value shall be either a single number representing the width of the padding, in default user space units, that applies to all four edges of the table cell or a 4-entry array representing the padding width for the before edge, after edge, start edge, and end edge, respectively, of the content rectangle. 
                *                                                  Default value: 0.
                *                              
                */

                    /*14.8.5.4.4 Layout Attributes for ILSEs
                *
                *The attributes described in Table 345 apply to inline - level structure elements(ILSEs). 
                *They may also be specified for a block-level element(BLSE) and may apply to any content items that are its immediate children.
                *
                *Table 345 - Standard layout attributes specific to inline-level structure elements
                *
                *          [Key]                   [Type]              [Value]
                *
                *          BaselineShift           number              (Optional; not inheritable) The distance, in default user space units, by which the element’s baseline shall be shifted relative to that of its parent element. 
                *                                                      The shift direction shall be the opposite of the block-progression direction specified by the prevailing WritingMode attribute (see “General Layout Attributes” in 14.8.5.4, “Layout Attributes”). Thus, positive values shall shift the baseline toward the before edge and negative values toward the after edge of the reference area (upward and downward, respectively, in Western writing systems). Default value: 0.
                *                                                  
                *                                                      The shifted element may be a superscript, a subscript, or an inline graphic.
                *                                                      The shift shall apply to the element, its content, and all of its descendants. Any further baseline shift applied to a child of this element shall be measured relative to the shifted baseline of this(parent) element.
                *
                *          LineHeight              number              (Optional; inheritable) The element’s preferred height, measured in default user space units in the block-progression direction. 
                *                                  or name             The height of a line shall be determined by the largest LineHeight value for any complete or partial ILSE that it contains.
                *
                *                                                      The name Normal or Auto in place of a numeric value shall indicate that no specific height constraint is to be imposed.
                *                                                      The element’s height shall be set to a reasonable value based on the content’s font size:
                *
                *                                                      Normal      Adjust the line height to include any nonzero value specified for BaselineShift.
                *
                *                                                      Auto        Adjustment for the value of BaselineShift shall not be made.
                *
                *                                                      Default value: Normal.
                *
                *                                                      This attribute applies to all ILSEs(including implicit ones) that are children of this element or of its nested ILSEs, if any.
                *                                                      It shall not apply to nested BLSEs.
                *
                *                                                      When translating to a specific export format, the values Normal and Auto, if specified, shall be used directly if they are available in the target format.
                *                                                      The meaning of the term “reasonable value” is left to the conforming reader to determine.
                *                                                      It should be approximately 1.2 times the font size, but this value can vary depending on the export format.
                *                              
                *                                                      NOTE 1      In the absence of a numeric value for LineHeight or an explicit value for the font size, a reasonable method of calculating the line height from the information in a Tagged PDF file is to find the difference between the associated font’s Ascent and Descent values (see 9.8, “Font Descriptors”), 
                *                                                                  map it from glyph space to default user space (see 9.4.4, “Text Space Details”), and use the maximum resulting value for any character in the line.
                *
                *          TextDecorationColor     array               (Optional; inheritable; PDF 1.5) The colour to be used for drawing text decorations. 
                *                                                      The value shall be an array of three numbers in the range 0.0 to 1.0, representing the red, green, and blue values, respectively, of an RGB colour space. 
                *                                                      If this attribute is not specified, the border colour for this element shall be the current fill colour in effect at the start of its associated content.
                *
                *          TextDecorationThickness number              (Optional; inheritable; PDF 1.5) The thickness of each line drawn as part of the text decoration. 
                *                                                      The value shall be a non-negative number in default user space units representing the thickness (0 is interpreted as the thinnest possible line). 
                *                                                      If this attribute is not specified, it shall be derived from the current stroke thickness in effect at the start of the element’s associated content, transformed into default user space units.
                *
                *          TextDecorationType      name                (Optional; not inheritable) The text decoration, if any, to be applied to the element’s text.
                *
                *                                                      None        No text decoration
                *      
                *                                                      Underline   A line below the text
                *
                *                                                      Overline    A line above the text
                *
                *                                                      LineThrough A line through the middle of the text
                *
                *                                                      Default value: None.
                *
                *                                                      This attribute shall apply to all text content items that are children of this element or of its nested ILSEs, if any.
                *                                                      The attribute shall not apply to nested BLSEs or to content items other than text.
                *
                *                                                      The colour, position, and thickness of the decoration shall be uniform across all children, regardless of changes in colour, font size, or other variations in the content’s text characteristics.
                *
                *          RubyAlign               name                (Optional; inheritable; PDF 1.5) The justification of the lines within a ruby assembly:
                *
                *                                                      Start       The content shall be aligned on the start edge in the inline-progression direction.
                *
                *                                                      Center      The content shall be centered in the inline-progression direction.
                *
                *                                                      End         The content shall be aligned on the end edge in the inline-progression direction.
                *
                *                                                      Justify     The content shall be expanded to fill the available width in the inline-progression direction.
                *
                *                                                      Distribute  The content shall be expanded to fill the available width in the inline-progression direction.
                *                                                                  However, space shall also be inserted at the start edge and end edge of the text.
                *                                                                  The spacing shall be distributed using a 1:2:1(start: infix:end) ratio.
                *                                                                  It shall be changed to a 0:1:1 ratio if the ruby appears at the start of a text line or to a 1:1:0 ratio if the ruby appears at the end of the text line.
                *
                *                                                      Default value: Distribute.
                *
                *                                                      This attribute may be specified on the RB and RT elements.When a ruby is formatted, the attribute shall be applied to the shorter line of these two elements. 
                *                                                      (If the RT element has a shorter width than the RB element, the RT element shall be aligned as specified in its RubyAlign attribute.)
                *
                *          RubyPosition                name            (Optional; inheritable; PDF 1.5) The placement of the RT structure element relative to the RB element in a ruby assembly:
                *
                *                                                      Before      The RT content shall be aligned along the before edge of the element.
                *
                *                                                      After       The RT content shall be aligned along the after edge of the element.
                *
                *                                                      Warichu     The RT and associated RP elements shall be formatted as a warichu, following the RB element.
                *
                *                                                      Inline      The RT and associated RP elements shall be formatted as a parenthesis comment, following the RB element.
                *
                *                                                      Default value: Before.
                *
                *          GlyphOrientationVertical    name            (Optional; inheritable; PDF 1.5) Specifies the orientation of glyphs when the inline-progression direction is top to bottom or bottom to top.
                *
                *                                                      This attribute may take one of the following values:
                *
                *                                                      angle       A number representing the clockwise rotation in degrees of the top of the glyphs relative to the top of the reference area.
                *                                                                  Shall be a multiple of 90 degrees between -180 and + 360.
                *                          
                *                                                      AutoSpecifies a default orientation for text, depending on whether it is fullwidth (as wide as it is high).
                *                                                      Fullwidth Latin and fullwidth ideographic text(excluding ideographic punctuation) shall be set with an angle of 0.
                *                                                      Ideographic punctuation and other ideographic characters having alternate horizontal and vertical forms shall use the vertical form of the glyph.
                *                                                      Non - fullwidth text shall be set with an angle of 90.
                *
                *                                                      Default value: Auto.
                *
                *                                                      NOTE 2      This attribute is used most commonly to differentiate between the preferred orientation of alphabetic(non - ideographic) text in vertically written Japanese documents(Auto or 90) and the orientation of the ideographic characters and / or alphabetic(non - ideographic) text in western signage and advertising(90).
                *
                *                                                      This attribute shall affect both the alignment and width of the glyphs.
                *                                                      If a glyph is perpendicular to the vertical baseline, its horizontal alignment point shall be aligned with the alignment baseline for the script to which the glyph belongs.
                *                                                      The width of the glyph area shall be determined from the horizontal width font characteristic for the glyph.
                *
                */

                    /*14.8.5.4.5 Content and Allocation Rectangles
                *
                *As defined in 14.8.3, “Basic Layout Model,” an element’s content rectangle is an enclosing rectangle derived from the shape of the element’s content, which shall define the bounds used for the layout of any included child elements.
                *The allocation rectangle includes any additional borders or spacing surrounding the element, affecting how it shall be positioned with respect to adjacent elements and the enclosing content rectangle or reference area.
                *
                *The exact definition of the content rectangle shall depend on the element’s structure type:
                *
                *  •   For a table cell(structure type TH or TD), the content rectangle shall be determined from the bounding box of all graphics objects in the cell’s content, taking into account any explicit bounding boxes (such as the BBox entry in a form XObject). 
                *      This implied size may be explicitly overridden by the cell’s Width and Height attributes.The cell’s height shall be adjusted to equal the maximum height of any cell in its row; its width shall be adjusted to the maximum width of any cell in its column.
                *
                *  •   For any other BLSE, the height of the content rectangle shall be the sum of the heights of all BLSEs it contains, plus any additional spacing adjustments between these elements.
                *
                *  •   For an ILSE that contains text, the height of the content rectangle shall be set by the LineHeight attribute. 
                *      The width shall be determined by summing the widths of the contained characters, adjusted for any indents, letter spacing, word spacing, or line - end conditions.
                *
                *  •   For an ILSE that contains an illustration or table, the content rectangle shall be determined from the bounding box of all graphics objects in the content, and shall take into account any explicit bounding boxes (such as the BBox entry in a form XObject). 
                *      This implied size may be explicitly overridden by the element’s Width and Height attributes.
                *
                *  •   For an ILSE that contains a mixture of elements, the height of the content rectangle shall be determined by aligning the child objects relative to one another based on their text baseline(for text ILSEs) or end edge(for non-text ILSEs), along with any applicable BaselineShift attribute(for all ILSEs), and finding the extreme top and bottom for all elements.
                *
                *NOTE      Some conforming readers may apply this process to all elements within the block; others may apply it on a line-by-line basis.
                *
                *The allocation rectangle shall be derived from the content rectangle in a way that also depends on the structure type:
                *
                *  •   For a BLSE, the allocation rectangle shall be equal to the content rectangle with its before and after edges adjusted by the element’s SpaceBefore and SpaceAfter attributes, if any, but with no changes to the start and end edges.
                *
                *  •   For an ILSE, the allocation rectangle is the same as the content rectangle.
                */

                    /*14.8.5.4.6 Illustration Attributes
                *
                *Particular uses of illustration elements(structure types Figure, Formula, or Form) shall have additional restrictions:
                *
                *  •   When an illustration element has a Placement attribute of Block, it shall have a Height attribute with an explicitly specified numerical value(not Auto).
                *      This value shall be the sole source of information about the illustration’s extent in the block-progression direction.
                *
                *  •   When an illustration element has a Placement attribute of Inline, it shall have a Width attribute with an explicitly specified numerical value(not Auto).
                *      This value shall be the sole source of information about the illustration’s extent in the inline-progression direction.
                *
                *  •   When an illustration element has a Placement attribute of Inline, Start, or End, the value of its BaselineShift attribute shall be used to determine the position of its after edge relative to the text baseline; BaselineShift shall be ignored for all other values of Placement. 
                *      (An illustration element with a Placement value of Start may be used to create a dropped capital; one with a Placement value of Inline may be used to create a raised capital.)
                */

                    /*14.8.5.4.7 Column Attributes
                *
                *The attributes described in Table 346 shall be present for the grouping elements Art, Sect, and Div(see 14.8.4.2, “Grouping Elements”).
                *They shall be used when the content in the grouping element is divided into columns.
                *
                *Table 346 - Standard column attributes
                *
                *          [Key]               [Type]              [Value]
                *
                *          ColumnCount         integer             (Optional; not inheritable; PDF 1.6) The number of columns in the content of the grouping element. 
                *                                                  Default value: 1.
                *
                *          ColumnGap           number or           (Optional; not inheritable; PDF 1.6) The desired space between adjacent columns, measured in default user space units in the inline-progression direction. 
                *                              array               If the value is a number, it specifies the space between all columns. 
                *                                                  If the value is an array, it should contain numbers, the first element specifying the space between the first and second columns, the second specifying the space between the second and third columns, and so on. 
                *                                                  If there are fewer than ColumnCount - 1 numbers, the last element shall specify all remaining spaces; if there are more than ColumnCount - 1 numbers, the excess array elements shall be ignored.
                *
                *          ColumnWidths        number or           (Optional; not inheritable; PDF 1.6) The desired width of the columns, measured in default user space units in the inline-progression direction. 
                *                              array               If the value is a number, it specifies the width of all columns. 
                *                                                  If the value is an array, it shall contain numbers, representing the width of each column, in order. 
                *                                                  If there are fewer than ColumnCount numbers, the last element shall specify all remaining widths; if there are more than ColumnCount numbers, the excess array elements shall be ignored.
                */
                    
                /*14.8.5.5 List Attribute
                *
                *If present, the ListNumbering attribute, described in Table 347, shall appear in an L(List) element.
                *It controls the interpretation of the Lbl(Label) elements within the list’s LI(List item) elements(see “List Elements” in 14.8.4.3, “Block - Level Structure Elements”).
                *This attribute may only be defined in attribute objects whose O(owner) entry has the value List or is one of the format-specific owner names listed in Table 341.
                *
                *Table 347 - Standard list attribute
                *
                *          [Key]               [Type]              [Value]
                *
                *          ListNumbering       name                (Optional; inheritable) The numbering system used to generate the content of the Lbl (Label) elements in an autonumbered list, or the symbol used to identify each item in an unnumbered list. 
                *                                                  The value of the ListNumbering shall be one of the following, and shall be applied as described here.
                *
                *                                                  None        No autonumbering; Lbl elements(if present) contain arbitrary text not subject to any numbering scheme
                *
                *                                                  Disc        Solid circular bullet
                *
                *                                                  Circle      Open circular bullet
                *
                *                                                  Square      Solid square bullet
                *
                *                                                  Decimal     Decimal arabic numerals(1–9, 10–99, …)
                *
                *                                                  UpperRoman  Uppercase roman numerals(I, II, III, IV, …)
                *
                *                                                  LowerRoman  Lowercase roman numerals(i, ii, iii, iv, …)
                *
                *                                                  UpperAlpha  Uppercase letters(A, B, C, …)
                *
                *                                                  LowerAlpha  Lowercase letters(a, b, c, …)
                *
                *                                                  Default value: None.
                *
                *                                                  The alphabet used for UpperAlpha and LowerAlpha shall be determined by the prevailing Lang entry(see 14.9.2, “Natural Language Specification”).
                *
                *                                                  The set of possible values may be expanded as Unicode identifies additional numbering systems.
                *                                                  A conforming reader shall ignore any value not listed in this table; it shall behave as though the value were None.
                *
                *
                *NOTE      This attribute is used to allow a content extraction tool to autonumber a list. 
                *          However, the Lbl elements within the table should nevertheless contain the resulting numbers explicitly, so that the document can be reflowed or printed without the need for autonumbering.
                */

                /*14.8.5.6 PrintField Attributes
                *
                *(PDF 1.7) The attributes described in Table 348 identify the role of fields in non - interactive PDF forms. 
                *Such forms may have originally contained interactive fields such as text fields and radio buttons but were then converted into non-interactive PDF files, or they may have been designed to be printed out and filled in manually.
                *Since the roles of the fields cannot be determined from interactive elements, the roles are defined using PrintField attributes.
                *
                *NOTE      PrintField attributes enable screen readers to identify page content that represents form fields(radio buttons, check boxes, push buttons, and text fields).
                *          These attributes enable the controls in print form fields to be represented in the logical structure tree and to be presented to assistive technology as if they were read-only interactive fields.
                *
                *
                *Table 348 - PrintField attributes
                *
                *          [Key]               [Type]              [Value]
                *
                *          Role                name                (Optional; not inheritable) The type of form field represented by this graphic. 
                *                                                  The value of Role shall be one of the following, and a conforming reader shall interpret its meaning as defined herein.
                *
                *                                                  rbRadio         button
                *
                *                                                  cbCheck         box
                *
                *                                                  pbPush          button
                *
                *                                                  tv              Text-value field
                *
                *                                                  The tv role shall be used for interactive fields whose values have been converted to text in the non - interactive document.
                *                                                  The text that is the value of the field shall be the content of the Form element(see Table 340).
                *
                *                                                  NOTE 1      Examples include text edit fields, numeric fields, password fields, digital signatures, and combo boxes.
                *
                *                                                  Default value: None specified.
                *
                *          checked             name                (Optional; not inheritable) The state of a radio button or check box field. 
                *                                                  The value shall be one of: on, off (default), or neutral.
                *                                                  NOTE 2      The case (capitalization)used for this key does not conform to the same conventions used elsewhere in this standard.
                *
                *          Desc                text string         (Optional; not inheritable) The alternate name of the field.
                *                                                  NOTE 3      Similar to the value supplied in the TU entry of the field dictionary for interactive fields (see Table 220).
                */

                /*14.8.5.7 Table Attributes
                *
                *The value of the O(owner) entry of a Table attributes element shall be Table or one of the format-specific owner names listed in Table 341.
                *
                *Table 349 - Standard table attributes
                *
                *          [Key]               [Type]              [Value]
                *
                *          RowSpan             integer             (Optional; not inheritable) The number of rows in the enclosing table that shall be spanned by the cell. 
                *                                                  The cell shall expand by adding rows in the block-progression direction specified by the table’s WritingModeattribute. 
                *                                                  If this entry is absent, a conforming reader shall assume a value of 1.
                *                                                  This entry shall only be used when the table cell has a structure type of TH or TD or one that is role mapped to structure type TH or TD(see Table 337).
                *
                *          ColSpan             integer             (Optional; not inheritable) The number of columns in the enclosing table that shall be spanned by the cell. 
                *                                                  The cell shall expand by adding columns in the inline-progression direction specified by the table’s WritingMode attribute. 
                *                                                  If this entry is absent, a conforming reader shall assume a value of 1
                *                                                  This entry shall only be used when the table cell has a structure type of TH or TD or one that is role mapped to structure types TH or TD(see Table 337).
                *
                *          Headers             array               (Optional; not inheritable; PDF 1.5) An array of byte strings, where each string shall be the element identifier (see the ID entry in Table 323) for a TH structure element that shall be used as a header associated with this cell.
                *                                                  This attribute may apply to header cells(TH) as well as data cells(TD)(see Table 337).
                *                                                  Therefore, the headers associated with any cell shall be those in its Headers array plus those in the Headers array of any THcells in that array, and so on recursively.
                *
                *          Scope               name                (Optional; not inheritable; PDF 1.5) A name whose value shall be one of the following: Row, Column, or Both. 
                *                                                  This attribute shall only be used when the structure type of the element is TH. (see Table 337). 
                *                                                  It shall reflect whether the header cell applies to the rest of the cells in the row that contains it, the column that contains it, or both the row and the column that contain it.
                *
                *          Summary             text string         (Optional; not inheritable; PDF 1.7) A summary of the table’s purpose and structure. 
                *                                                  This entry shall only be used within Table structure elements (see Table 337).
                *                                                  NOTE        For use in non - visual rendering such as speech or braille
                */


        }

        //14.9 Accessibility Support
        public class Accessibility_Support
        {
            /*14.9.1 General
            *
            *PDF includes several facilities in support of accessibility of documents to users with disabilities.In particular, many visually computer users with visual impairments use screen readers to read documents aloud.To enable proper vocalization, either through a screen reader or by some more direct invocation of a text-to-speech engine, PDF supports the following features:
            *
            *  •   Specifying the natural language used for text in a PDF document—for example, as English or Spanish, or used to hide or reveal optional content(see 14.9.2, “Natural Language Specification”)
            *
            *  •   Providing textual descriptions for images or other items that do not translate naturally into text(14.9.3, “Alternate Descriptions”), or replacement text for content that does translate into text but is represented in a nonstandard way(such as with a ligature or illuminated character; see 14.9.4, “Replacement Text”)
            *
            *  •   Specifying the expansion of abbreviations or acronyms(Section 14.9.5, “Expansion of Abbreviations and Acronyms”)
            *
            *The core of this support lies in the ability to determine the logical order of content in a PDF document, independently of the content’s appearance or layout, through logical structure and Tagged PDF, as described under 14.8.2.3, “Page Content Order.” 
            *An accessibility application can extract the content of a document for presentation to users with disabilities by traversing the structure hierarchy and presenting the contents of each node. 
            *For this reason, conforming writers ensure that all information in a document is reachable by means of the structure hierarchy, and they should use the facilities described in this sub-clause.
            *
            *NOTE 1        Text can be extracted from Tagged PDF documents and examined or reused for purposes other than accessibility; see 14.8, “Tagged PDF.”
            *
            *NOTE 2        Additional guidelines for accessibility support of content published on the Web can be found in the W3C document Web Content Accessibility Guidelines and the documents it points to (see the Bibliography).
            */

            /*14.9.2 Natural Language Specification
            */

            /*14.9.2.1 General
            *
            *Natural language may be specified for text in a document or for optional content.
            *
            *The natural language used for text in a document shall be determined in a hierarchical fashion, based on whether an optional Lang entry(PDF 1.4) is present in any of several possible locations.
            *At the highest level, the document’s default language(which applies to both text strings and text within content streams) may be specified by a Lang entry in the document catalogue(see 7.7.2, “Document Catalog”).
            *Below this, the language may be specified for the following items:
            *
            *  •   Structure elements of any type(see 14.7.2, “Structure Hierarchy”), through a Lang entry in the structure element dictionary.
            *
            *  •   Marked - content sequences that are not in the structure hierarchy(see 14.6, “Marked Content”), through a Lang entry in a property list attached to the marked - content sequence with a Span tag.
            *
            *NOTE 1            Although Span is also a standard structure type, as described under 14.8.4.4, “Inline - Level Structure Elements,” its use here is entirely independent of logical structure.
            *
            *NOTE 2            The natural language used for optional content allows content to be hidden or revealed, based on the Langentry(PDF 1.5) in the Language dictionary of an optional content usage dictionary.
            *
            *NOTE 3            The following sub - clauses provide details on the value of the Lang entry and the hierarchical manner in which the language for text in a document is determined.
            *
            *Text strings encoded in Unicode may include an escape sequence or language tag indicating the language of the text and overriding the prevailing Lang entry(see 7.9.2.2, “Text String Type”).
            */

            /*14.9.2.2 Language Identifiers
            *
            *Certain language-related dictionary entries are text strings that specify language identifiers.
            *Such text strings may appear as Lang entries in the following structures or dictionaries:
            *
            *  •   Document catalogue, structure element dictionary, or property list
            *
            *  •   Optional content usage dictionary’s Language dictionary, the hierarchical issues described in 14.9.2.3, “Language Specification Hierarchy,” shall not apply to this entry
            *
            *A language identifier shall either be the empty text string, to indicate that the language is unknown, or a Language - Tag as defined in RFC 3066, Tags for the Identification of Languages.
            *
            *Although language codes are commonly represented using lowercase letters and country codes are commonly represented using uppercase letters, all tags shall be treated as case insensitive.
            */

            /*14.9.2.3 Language Specification Hierarchy
            *
            *The Lang entry in the document catalogue shall specify the default natural language for all text in the document.
            *Language specifications may appear within structure elements, and they may appear within marked - content sequences that are not in the structure hierarchy.
            *If present, such language specifications override the default.
            *
            *Language specifications within the structure hierarchy apply in this order:
            *
            *  •   A structure element’s language specification.
            *      If a structure element does not have a Lang entry, the element shall inherit its language from any parent element that has one.
            *
            *  •   Within a structure element, a language specification for a nested structure element or marked-content sequence
            *
            *If only part of the page content is contained in the structure hierarchy, and the structured content is nested within nonstructured content for which a different language specification applies, the structure element’s language specification shall take precedence.
            *
            *A language identifier attached to a marked-content sequence with the Span tag specifies the language for all text in the sequence except for nested marked content that is contained in the structure hierarchy (in which case the structure element’s language applies) and except where overridden by language specifications for other nested marked content.
            *
            *NOTE      Examples in this sub-clause illustrate the hierarchical manner in which the language for text in a document is determined.
            *
            *EXAMPLE 1     This example shows how a language specified for the document as a whole could be overridden by one specified for a marked-content sequence within a page’s content stream, independent of any logical structure.
            *              In this case, the Lang entry in the document catalogue(not shown) has the value en - US, meaning U.S.English, and it is overridden by the Lang property attached(with the Span tag) to the marked - content sequence Hasta la vista.
            *              The Lang property identifies the language for this marked content sequence with the value es - MX, meaning Mexican Spanish.
            *
            *              2 0 obj                                                     % Page object
            *                  << / Type / Page
            *                     / Contents 3 0 R                                     % Content stream
            *                     …
            *                  >>
            *              endobj
            *              3 0 obj                                                     % Page's content stream
            *                  << / Length … >>
            *              stream
            *                  BT
            *                      (See you later, or as Arnold would say, ) Tj
            *                      / Span << / Lang(es - MX) >>                        % Start of marked - content sequence
            *                          BDC
            *                              (Hasta la vista.) Tj
            *                          EMC                                             % End of marked - content sequence
            *                  ET
            *              endstream
            *              endobj
            *
            *EXAMPLE 2     In the following example, the Lang entry in the structure element dictionary (specifying English) applies to the marked-content sequence having an MCID (marked-content identifier) value of 0 within the indicated page’s content stream. 
            *              However, nested within that marked-content sequence is another one in which the Lang property attached with the Span tag (specifying Spanish) overrides the structure element’s language specification.
            *
            *              This example omits required StructParents entries in the objects used as content items(see 14.7.4.4, “Finding Structure Elements from Content Items”).
            *
            *              1 0 obj                                                     % Structure element
            *                  << / Type / StructElem
            *                     / S / P                                              % Structure type
            *                     / P …                                                % Parent in structure hierarchy
            *                     / K << / Type / MCR
            *                            / Pg 2 0 R                                    % Page containing marked-content sequence
            *                            / MCID 0                                      % Marked - content identifier
            *                         >>
            *                    / Lang(en - US)                                       % Language specification for this element
            *                  >>
            *              endobj
            *              2 0 obj                                                     % Page object
            *                  << / Type / Page
            *                     / Contents 3 0 R                                     % Content stream
            *                      …
            *                  >>
            *              endobj
            *              3 0 obj                                                     % Page's content stream
            *                  << / Length … >>
            *              stream
            *                  BT
            *                      / P << / MCID 0 >>                                  % Start of marked - content sequence
            *                          BDC
            *                              (See you later, or in Spanish you would say, ) Tj
            *                              / Span << / Lang(es - MX) >>                % Start of nested marked - content sequence
            *                                  BDC
            *                                      (Hasta la vista.) Tj
            *                                  EMC                                     % End of nested marked - content sequence
            *                          EMC                                             % End of marked - content sequence
            *                  ET
            *              endstream
            *              endobj
            *
            *EXAMPLE 3         The page’s content stream consists of a marked-content sequence that specifies Spanish as its language by means of the Span tag with a Lang property. 
            *                  Nested within it is content that is part of a structure element (indicated by the MCID entry in that property list), and the language specification that applies to the latter content is that of the structure element, English.
            *
            *                  This example omits required StructParents entries in the objects used as content items(see 14.7.4.4, “Finding Structure Elements from Content Items”).
            *
            *                  1 0 obj                                 % Structure element
            *                      << / Type / StructElem
            *                         / S / P                          % Structure type
            *                         / P …                            % Parent in structure hierarchy
            *                         / K << / Type / MCR
            *                                / Pg 2 0 R                % Page containing marked-content sequence
            *                                / MCID 0                  % Marked - content identifier
            *                             >>
            *                         / Lang(en - US)                  % Language specification for this element
            *                      >>
            *                  endobj
            *                  2 0 obj                                 % Page object
            *                      << / Type / Page
            *                         / Contents 3 0 R                 % Content stream
            *                          …
            *                      >>
            *                  endobj
            *                  3 0 obj                                 % Page's content stream
            *                      << / Length … >>
            *                  stream
            *                      /Span << /Lang (es-MX) >>           % Start of marked-content sequence
            *                          BDC
            *                              (Hasta la vista, ) Tj
            *                              / P << / MCID 0 >>          % Start of structured marked - content sequence,
            *                                  BDC                     % to which structure element's language applies
            *                                      (as Arnold would say. ) Tj
            *                                  EMC                     % End of structured marked - content sequence
            *                          EMC                             % End of marked-content sequence
            *                  endstream
            *                  endobj
            *
            */

            /*14.9.2.4 Multi-language Text Arrays
            *
            *A multi-language text array(PDF 1.5) allows multiple text strings to be specified, each in association with a language identifier. 
            *(See the Alt entry in Tables 274 and 277 for examples of its use.)
            *
            *A multi-language text array shall contain pairs of strings.
            *The first string in each pair shall be a language identifier(14.9.2.2, “Language Identifiers”).
            *A language identifier shall not appear more than once in the array; any unrecognized language identifier shall be ignored.
            *An empty string specifies default text that may be used when no suitable language identifier is found in the array. 
            *The second string is text associated with the language.
            *
            *EXAMPLE       [(en - US)(My vacation)(fr)(mes vacances)()(default text)]
            *
            *When a conforming reader searches a multi - language text array to find text for a given language, it shall look for an exact (though case-insensitive) match between the given language’s identifier and the language identifiers in the array.
            *If no exact match is found, prefix matching shall be attempted in increasing array order: a match shall be declared if the given identifier is a leading, case-insensitive, substring of an identifier in the array, and the first post - substring character in the array identifier is a hyphen. 
            *For example, given identifier en matches array identifier en-US, but given identifier en-US matches neither en nor en - GB.
            *If no exact or prefix match can be found, the default text(if any) should be used.
            */

            /*14.9.3 Alternate Descriptions
            *
                *PDF documents may be enhanced by providing alternate descriptions for images, formulas, or other items that do not translate naturally into text.
                *
                *NOTE 1    Alternate descriptions are human-readable text that could, for example, be vocalized by a text - to - speech engine for the benefit of users with visual impairments.
                *
                *An alternate description may be specified for the following items:
                *
                *  •   A structure element(see 14.7.2, “Structure Hierarchy”), through an Alt entry in the structure element dictionary
                *
                *  •   (PDF 1.5) A marked-content sequence(see 14.6, “Marked Content”), through an Alt entry in a property list attached to the marked - content sequence with a Span tag.
                *
                *  •   Any type of annotation(see 12.5, “Annotations”) that does not already have a text representation, through a Contents entry in the annotation dictionary
                *
                *For annotation types that normally display text, the Contents entry of the annotation dictionary shall be used as the source for an alternate description.For annotation types that do not display text, a Contents entry(PDF 1.4) may be included to specify an alternate description. 
                *Sound annotations, which need no alternate description for the purpose of vocalization, may include a Contents entry specifying a description that may be displayed for the benefit of users with hearing impairments.
                *
                *An alternate name may be specified for an interactive form field (see 12.7, “Interactive Forms”) which, if present, shall be used in place of the actual field name when a conforming reader identifies the field in a user-interface. 
                *This alternate name, if provided, shall be specified using the TU entry of the field dictionary.
                *
                *NOTE 2    The TU entry is useful for vocalization purposes.
                *
                *Alternate descriptions are text strings, which shall be encoded in either PDFDocEncoding or Unicode character encoding.
                *
                *NOTE 3    As described in 7.9.2.2, “Text String Type,” Unicode defines an escape sequence for indicating the language of the text.
                *          This mechanism enables the alternate description to change from the language specified by the prevailing Lang entry(as described in the preceding sub - clause).
                *          Within alternate descriptions, Unicode escape sequences specifying language shall override the prevailing Lang entry.
                *
                *When applied to structure elements, the alternate description text shall be considered to be a complete (or whole) word or phrase substitution for the current element.
                *If each of two (or more) elements in a sequence have an Alt entry in their dictionaries, they shall be treated as if a word break is present between them.
                *The same applies to consecutive marked-content sequences.
                *
                *The Alt entry in property lists may be combined with other entries.
                *
                *EXAMPLE       This example shows the Alt entry combined with a Lang entry.
                *
                *              /Span << /Lang (en-us) /Alt(six-point star) >> BDC(A) Tj EMC
                */

            /*14.9.4 Replacement Text
            *
                *NOTE 1    Just as alternate descriptions can be provided for images and other items that do not translate naturally into text (as described in the preceding sub-clause), replacement text can be specified for content that does translate into text but that is represented in a nonstandard way. 
                *          These nonstandard representations might include, for example, glyphs for ligatures or custom characters, or inline graphics corresponding to letters in an illuminated manuscript or to dropped capitals.
                *
                *Replacement text may be specified for the following items:
                *
                *  •   A structure element(see 14.7.2, “Structure Hierarchy”), by means of the optional ActualText entry(PDF 1.4) of the structure element dictionary.
                *
                *  •   (PDF 1.5) A marked-content sequence(see 14.6, “Marked Content”), through an ActualText entry in a property list attached to the marked - content sequence with a Span tag.
                *
                *The ActualText value shall be used as a replacement, not a description, for the content, providing text that is equivalent to what a person would see when viewing the content.
                *The value of ActualText shall be considered to be a character substitution for the structure element or marked - content sequence.If each of two(or more) consecutive structure or marked - content sequences has an ActualText entry, they shall be treated as if no word break is present between them.
                *
                *NOTE 2    The treatment of ActualText as a character replacement is different from the treatment of Alt, which is treated as a whole word or phrase substitution.
                *
                *EXAMPLE   This example shows the use of replacement text to indicate the correct character content in a case where hyphenation changes the spelling of a word 
                *          (in German, up until recent spelling reforms, the word “Drucker” when hyphenated was rendered as “Druk-” and “ker”).
                *
                *          (Dru)Tj
                *          / Span
                *              <</ Actual Text(c) >>
                *                  BDC
                *                      (k -) Tj
                *                  EMC
                *          (ker) '
                *
                *
                *Like alternate descriptions (and other text strings), replacement text, if encoded in Unicode, may include an escape sequence for indicating the language of the text. 
                *Such a sequence shall override the prevailing Langentry (see 7.9.2.2, “Text String Type”).
                */

            /*14.9.5 Expansion of Abbreviations and Acronyms
                *
                *The expansion of an abbreviation or acronym may be specified for the following items:
                *
                *  •   Marked - content sequences, through an E property(PDF 1.4) in a property list attached to the sequence with a Span tag.
                *
                *  •   Structure elements, through an E entry(PDF 1.5) in the structure element dictionary.
                *
                *NOTE 1    Abbreviations and acronyms can pose a problem for text - to - speech engines.
                *          Sometimes the full pronunciation for an abbreviation can be divined without aid.For example, a dictionary search will probably reveal that “Blvd.” is pronounced “boulevard” and that “Ave.” is pronounced “avenue.” 
                *          However, some abbreviations are difficult to resolve, as in the sentence “Dr.Healwell works at 123 Industrial Dr.”.
                *
                *EXAMPLE       BT
                *                  / Span << / E(Doctor) >>
                *                      BDC
                *                          (Dr. ) Tj
                *                      EMC
                *                  (Healwell works at 123 Industrial) Tj
                *                  / Span << / E(Drive) >>
                *                      BDC
                *                          (Dr.) Tj
                *                      EMC
                *               ET
                *
                *The E value (a text string) shall be considered to be a word or phrase substitution for the tagged text and therefore shall be treated as if a word break separates it from any surrounding text.
                *The expansion text, if encoded in Unicode, may include an escape sequence for indicating the language of the text (see 7.9.2.2, “Text String Type”). 
                *Such a sequence shall override the prevailing Lang entry.
                *
                *NOTE 2        Some abbreviations or acronyms are conventionally not expanded into words. 
                *              For the text “XYZ,” for example, either no expansion should be supplied(leaving its pronunciation up to the text - to - speech engine) or, to be safe, the expansion “X Y Z” should be specified.
            */

        }

        //14.10 Web Capture
        public class Web_Capture
        { 

            /*14.10 Web Capture
            */
                
                /*14.10.1 General
                *
                *The information in the Web Capture data structures enables conforming products to perform the following operations:
                *
                *  •   Save locally and preserve the visual appearance of material from the Web
                *
                *  •   Retrieve additional material from the Web and add it to an existing PDF file
                *
                *  •   Update or modify existing material previously captured from the Web
                *
                *  •   Find source information for material captured from the Web, such as the URL(if any) from which it was captured
                *
                *  •   Find all material in a PDF file that was generated from a given URL
                *
                *  •   Find all material in a PDF file that matches a given digital identifier(MD5 hash)
                *
                *The information needed to perform these operations shall be recorded in two data structures in the PDF file:
                *
                *  •   The Web Capture information dictionary, which shall hold document-level information related to Web Capture.
                *
                *  •   The Web Capture content database, which shall hold a complete registry of the source content resources retrieved by Web Capture and where it came from.
                *
                *NOTE 3    The Web Capture content database enables the capturing process to avoid downloading material that is already present in the file.
                */

                /*14.10.2 Web Capture Information Dictionary
                *
                *The optional SpiderInfo entry in the document catalogue (see 7.7.2, “Document Catalog”), if present, shall hold Web Capture information dictionary.
                *
                *Table 350 - Entries in the Web Capture information dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          V                   number                  (Required) The Web Capture version number. The version number shall be 1.0 in a conforming file.
                *                                                      This value shall be a single real number, not a major and minor version number.
                *                                                      EXAMPLE     A version number of 1.2 would be considered greater than 1.15.
                *
                *          C                   array                   (Optional) An array of indirect references to Web Capture command dictionaries (see 14.10.5.3, “Command Dictionaries”) describing commands that were used in building the PDF file. 
                *                                                      The commands shall appear in the array in the order in which they were executed in building the file.
                */
                

                /*14.10.3 Content Database
                */

                    /*14.10.3.1 General
                *
                *When a PDF file, or part of a PDF file, is built from a content resource stored in another format, such as an HTML page, the resulting PDF file (or portion thereof) may contain content from more than the single content resources. 
                *Conversely, since many content formats do not have static pagination, a single content resource may give rise to multiple PDF pages.
                *
                *To keep track of the correspondence between PDF content and the resources from which the content was derived, a PDF file may contain a content database that maps URLs and digital identifiers to PDF objects such as pages and XObjects.
                *
                *NOTE 4        By looking up digital identifiers in the database, Web Capture can determine whether newly downloaded content is identical to content already retrieved from a different URL.
                *              Thus, it can perform optimizations such as storing only one copy of an image that is referenced by multiple HTML pages.
                *
                *Web Capture’s content database shall be organized into content sets.Each content set shall be a dictionary holding information about a group of related PDF objects generated from the same source data. 
                *A content set shall have for the value of its S(subtype) entry either the value SPS, for a page set, or SIS, for an image set.
                *
                *The mapping from a source content resource to a content set in a PDF document may be saved in the PDF file.
                *The mapping may be an association from the resource's URL to the content set, stored in the PDF document's URLS name tree.
                *The mapping may also be an association from a digital identifier(14.10.3.3, “Digital Identifiers”) generated from resource's data to the content set, stored in the PDF document's IDS name tree.Both associations may be present in the PDF file.
                *
                *(see Figure 84 - Simple Web Capture file structure, on page 618)
                *
                *Entries in the URLS and IDS name trees may refer to an array of content sets or a single content set. If the entry is an array, the content sets need not have the same subtype; the array may include both page sets and image sets.
                *
                *(see Figure 85 - Complex Web Capture file structure, on page 619)
                */

                    /*14.10.3.2 URL Strings
                *
                *URLs associated with Web Capture content sets shall be reduced to a predictable, canonical form before being used as keys in the URLS name tree. 
                *The following steps describe how to perform this reduction, using terminology from Internet RFCs 1738, Uniform Resource Locators, and 1808, Relative Uniform Resource Locators(see the Bibliography). 
                *This algorithm shall be applied for HTTP, FTP, and file URLs:
                *
                *Algorithm: URL strings
                *
                *  a)  If the URL is relative, it shall be converted into an absolute URL.
                *
                *  b)  If the URL contains one or more NUMBER SIGN(02h3) characters, it shall be truncated before the first NUMBER SIGN.
                *
                *  c)  Any uppercase ASCII characters within the scheme section of the URL shall be replaced with the corresponding lowercase ASCII characters.
                *
                *  d)  If there is a host section, any uppercase ASCII characters therein shall be converted to lowercase ASCII.
                *
                *  e)  If the scheme is file and the host is localhost, the host section shall be removed.
                *
                *  f)  If there is a port section and the port is the default port for the given protocol(80 for HTTP or 21 for FTP), the port section shall be removed.
                *
                *  g)  If the path section contains PERIOD(2Eh) (.) or DOUBLE PERIOD(..) subsequences, transform the path as described in section 4 of RFC 1808.
                *
                *NOTE      Because the PERCENT SIGN(25h) is unsafe according to RFC 1738 and is also the escape character for encoded characters, it is not possible in general to distinguish a URL with unencoded characters from one with encoded characters.
                *          For example, it is impossible to decide whether the sequence % 00 represents a single encoded null character or a sequence of three unencoded characters.
                *          Hence, no number of encoding or decoding passes on a URL can ever cause it to reach a stable state.
                *          Empirically, URLs embedded in HTML files have unsafe characters encoded with one encoding pass, and Web servers perform one decoding pass on received paths(though CGI scripts can make their own decisions).
                *
                *Canonical URLs are thus assumed to have undergone one and only one encoding pass. A URL whose initial encoding state is known can be safely transformed into a URL that has undergone only one encoding pass.
                */

                    /*14.10.3.3 Digital Identifiers
                *
                *Digital identifiers, used to associate source content resources with content sets by the IDS name tree, shall begenerated using the MD5 message - digest algorithm(Internet RFC 1321).
                *
                *NOTE 1    The exact data passed to the algorithm depends on the type of content set and the nature of the identifier being calculated.
                *
                *For a page set, the source data shall be passed to the MD5 algorithm first, followed by strings representing the digital identifiers of any auxiliary data files(such as images) referenced in the source data, in the order in which they are first referenced.
                *If an auxiliary file is referenced more than once, its identifier shall be passed only the first time. 
                *The resulting string shall be used as the digital identifier for the source content resource.
                *
                *NOTE 2        This sequence produces a composite identifier representing the visual appearance of the pages in the page set.
                *
                *NOTE 3        Two HTML source files that are identical, but for which the referenced images contain different data—for example, if they have been generated by a script or are pointed to by relative URLs—do not produce the same identifier.
                *
                *When the source data is a PDF file, the identifier shall be generated solely from the contents of that file; there shall be no auxiliary data.
                *
                *A page set may also have a text identifier, calculated by applying the MD5 algorithm to just the text present in the source data.
                *
                *EXAMPLE 1     For an HTML file the text identifier is based solely on the text between markup tags; no images are used in the calculation.
                *
                *For an image set, the digital identifier shall be calculated by passing the source data for the original image to the MD5 algorithm.
                *
                *EXAMPLE 2     The identifier for an image set created from a GIF image is calculated from the contents of the GIF.
                */

                    /*14.10.3.4 Unique Name Generation
                *
                *In generating PDF pages from a data source, items such as hypertext links and HTML form fields are converted into corresponding named destinations and interactive form fields.
                *These items shall be given names that do not conflict with those of other such items in the file.
                *
                *NOTE      As used here, the term name refers to a string, not a name object.
                *
                *Furthermore, when updating an existing file, a conforming processor shall ensure that each destination or field is given a unique name that shall be derived from its original name but constructed so that it avoids conflicts with similarly named items elsewhere.
                *
                *The unique name shall be formed by appending an encoded form of the page set’s digital identifier string to the original name of the destination or field. 
                *The identifier string shall be encoded to remove characters that have special meaning in destinations and fields.
                *The characters listed in the first column of Table 351 have special meaning and shall be encoded using the corresponding byte values from second column of Table 351.
                *
                *Table 351 - Characters with special meaning in destinations and fields and their byte values
                *
                *              [Character]             [Byte value]            [Escape sequence]
                *
                *                 (nul)                0x00                    \0 (0x5c 0x30)
                *               . (PERIOD)             0x2e                    \p (0x5c 0x70)
                *               \ (backslash)          0x5c                    \\ (0x5c 0x5c)
                *
                *EXAMPLE       Since the PERIOD character (2Eh) is used as the field separator in interactive form field names, it does not appear in the identifier portion of the unique name.
                *
                *If the name is used for an interactive form field, there is an additional encoding to ensure uniqueness and compatibility with interactive forms.
                *Each byte in the source string, encoded as described previously, shall bereplaced by two bytes in the destination string.
                *The first byte in each pair is 65(corresponding to the ASCII character A) plus the high - order 4 bits of the source byte; the second byte is 65 plus the low - order 4 bits of the source byte.
                */

                /*14.10.4 Content Sets
                */

                    /*14.10.4.1 General
                *
                *A Web Capture content set is a dictionary describing a set of PDF objects generated from the same source data. 
                *It may include information common to all the objects in the set as well as about the set itself.
                *Table 352 defines the contents of this type of dictionary.
                */

                    /*14.10.4.2 Page Sets
                *
                *A page set is a content set containing a group of PDF page objects generated from a common source, such as an HTML file. 
                *The pages shall be listed in the O array of the page set dictionary(see Table 352) in the same order in which they were initially added to the file. 
                *A single page object shall not belong to more than one page set. 
                *Table 353 defines the content set dictionary entries specific to Page Sets.
                *
                *The TID(text identifier) entry may be used to store an identifier generated from the text of the pages belonging to the page set(see 14.10.3.3, “Digital Identifiers”). 
                *A text identifier may not be appropriate for some page sets(such as those with no text) and may be omitted in these cases.
                *
                *EXAMPLE       This identifier may be used to determine whether the text of a document has changed.
                *
                *Table 352 - Entries common to all Web Capture content sets
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be SpiderContentSet for a Web Capture content set.
                *
                *          S                   name                    (Required) The subtype of content set that this dictionary describes. The value shall be one of:
                *
                *                                                      SPS(“Spider page set”) A page set
                *
                *                                                      SIS(“Spider image set”) An image set
                *
                *          ID                  byte string             (Required) The digital identifier of the content set (see 14.10.3.3, “Digital Identifiers”).
                *
                *          O                   array                   (Required) An array of indirect references to the objects belonging to the content set. 
                *                                                      The order of objects in the array is restricted when the content set subtype (S entry) is SPS (see 14.10.4.2, “Page Sets”).
                *
                *          SI                  dictionary              (Required) A source information dictionary (see 14.10.5, “Source Information”) or an array of such dictionaries, describing the sources from which the objects belonging to the content set were created.
                *                              or array
                *
                *          CT                  ASCII string            (Optional) The content type, an ASCII string characterizing the source from which the objects belonging to the content set were created. 
                *                                                      The string shall conform to the content type specification described in Internet RFC 2045, Multipurpose Internet Mail Extensions (MIME) Part One: Format of Internet Message Bodies (see the Bibliography).
                *                                                      EXAMPLE     for a page set consisting of a group of PDF pages created from an HTML file, the content type would be text/ html.
                *
                *          TS                  date                    (Optional) A time stamp giving the date and time at which the content set was created.
                *
                *
                *Table 353 - Addtitional entries specific to a Web Capture page set
                *
                *          [Key]               [Type]                  [Value]
                *
                *          S                   name                    (Required) The subtype of content set that this dictionary describes; shall be SPS.
                *
                *          T                   text string             (Optional) The title of the page set, a human-readable text string.
                *
                *          TID                 byte string             (Optional) A text identifier generated from the text of the page set, as described in 14.10.3.3, “Digital Identifiers.”
                *
                */

                    /*14.10.4.3 Image Sets
                *
                *An image set is a content set containing a group of image XObjects generated from a common source, such as multiple frames of an animated GIF image. 
                *A single XObject shall not belong to more than one image set. 
                *Table 354 shows the content set dictionary entries specific to Image Sets.
                *
                *Table 354 - Additional entries specific to a Web Capture image set
                *
                *          [Key]           [Type]                      [Value] 
                *
                *          S               name                        (Required) The subtype of content set that this dictionary describes; shall be SIS.
                *
                *          R               integer                     (Required) The reference counts for the image XObjects belonging to the image set. 
                *                          or array                    For an image set containing a single XObject, the value shall be the integer reference count for that XObject. 
                *                                                      For an image set containing multiple XObjects, the value shall be an array of reference counts parallel to the O array (see Table 352); that is, each element in the R array shall hold the reference count for the image XObject at the corresponding position in the O array.
                *
                *Each image XObject in an image set has a reference count indicating the number of PDF pages referring to that XObject. 
                *The reference count shall be incremented whenever Web Capture creates a new page referring to the XObject (including copies of already existing pages) and decremented whenever such a page is destroyed. 
                *The reference count shall be incremented or decremented only once per page, regardless of the number of times the XObject may be referenced by that page. 
                *If the reference count reaches 0, it shall be assumed that there are no remaining pages referring to the XObject and that the XObject can be removed from the image set’s O array. 
                *When removing an XObject from the O array of an image set, the corresponding entry in the R array shall be removed also.
                */

                /*14.10.5 Source Information
                */

                    /*14.10.5.1 General
                *
                *The SI entry in a content set dictionary(see Table 352) shall contain one or more source information dictionaries, each containing information about the locations from which the source data for the content set was retrieved.
                *
                *Table 355 - Entries in a source information dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          AU                  ASCII string            (Required) An ASCII string or URL alias dictionary (see 14.10.5.2, “URL Alias Dictionaries”) which shall identify the URLs from which the source data was retrieved.
                *                              or
                *                              dictionary
                *
                *          TS                  date                    (Optional) A time stamp which, if present, shall contain the most recent date and time at which the content set’s contents were known to be up to date with the source data.
                *
                *          E                   date                    (Optional) An expiration stamp which, if present, shall contain the date and time at which the content set’s contents shall be considered out of date with the source data.
                *
                *          S                   integer                 (Optional) A code which, if present, shall indicate the type of form submission, if any, by which the source data was accessed (see 12.7.5.2, “Submit-Form Action”). 
                *                                                      If present, the value of the S entry shall be 0, 1, or 2, in accordance with the following meanings:
                *
                *                                                      0       Not accessed by means of a form submission
                *
                *                                                      1       Accessed by means of an HTTP GET request
                *
                *                                                      2       Accessed by means of an HTTP POST request
                *
                *                                                      This entry may be present only in source information dictionaries associated with page sets.
                *                                                      Default value: 0.
                *
                *          C                   dictionary              (Optional; if present, shall be an indirect reference) A command dictionary (see 14.10.5.3, “Command Dictionaries”) describing the command that caused the source data to be retrieved. 
                *                                                      This entry may be present only in source information dictionaries associated with page sets.
                *
                *
                *A content set's SI entry may contain a single source information dictionary. 
                *However, a PDF processor may attempt to detect situations in which the same source data has been located via two or more distinct URLs. 
                *If a processor detects such a situation, it may generate a single content set from the source data, containing a single copy of the relevant PDF pages or image XObjects. 
                *In this case, the SI entry shall be an array containing one source information dictionary for each distinct URL from which the original source content was found.
                *
                *The determination that distinct URLs produce the same source data shall be made by comparing digitalidentifiers for the source data.
                *
                *A source information dictionary’s AU(aliased URLs) entry shall identify the URLs from which the source data was retrieved.
                *If there is only one such URL, the v value of this entry may be a string.
                *If multiple URLs map to the same location through redirection, the AU value shall be a URL alias dictionary(see 14.10.5.2, “URL Alias Dictionaries”).
                *
                *NOTE 1        For file size efficiency, the entire URL alias dictionary (excluding the URL strings) should be represented as a direct object because its internal structure should never be shared or externally referenced.
                *
                *The TS (time stamp) entry allows each source location associated with a content set to have its own time stamp.
                *
                *NOTE 2        This is necessary because the time stamp in the content set dictionary(see Table 352) merely refers to the creation date of the content set. 
                *              A hypothetical “Update Content Set” command might reset the time stamp in the source information dictionary to the current time if it found that the source data had not changed since the time stamp was last set.
                *
                *The E(expiration) entry specifies an expiration date for each source location associated with a content set.If the current date and time are later than those specified, the contents of the content set shall be considered out of date with respect to the original source.
                */

                    /*14.10.5.2 URL Alias Dictionaries
                *
                *When a URL is accessed via HTTP, a response header may be returned indicating that the requested data is at a different URL. 
                *This redirection process may be repeated in turn at the new URL and can potentially continue indefinitely.
                *It is not uncommon to find multiple URLs that all lead eventually to the same destination through one or more redirections.
                *A URL alias dictionary represents such a set of URL chains leading to a common destination. Table 356 shows the contents of this type of dictionary.
                *
                *Table 356 - Entries in a URL alias dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          U                   ASCII string        (Required) The destination URL to which all of the chains specified by the C entry lead.
                *
                *          C                   array               (Optional) An array of one or more arrays of strings, each representing a chain of URLs leading to the common destination specified by U.
                *
                *The C (chains) entry may be omitted if the URL alias dictionary contains only one URL. 
                *If C is present, its value shall be an array of arrays, each representing a chain of URLs leading to the common destination. 
                *Within each chain, the URLs shall be stored as ASCII strings in the order in which they occur in the redirection sequence. 
                *The common destination (the last URL in a chain) may be omitted, since it is already identified by the U entry.
                */

                    /*14.10.5.3 Command Dictionaries
                *
                *A Web Capture command dictionary represents a command executed by Web Capture to retrieve one or more pieces of source data that were used to create new pages or modify existing pages. 
                *The entries in this dictionary represent parameters that were originally specified interactively by the user who requested that the Web content be captured.
                *This information is recorded so that the command can subsequently be repeated to update the captured content. 
                *Table 357 shows the contents of this type of dictionary.
                *
                *Table 357 - Entries in a Web Capture command dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          URL                 ASCII string        (Required) The initial URL from which source data was requested.
                *
                *          L                   integer             (Optional) The number of levels of pages retrieved from the initial URL. 
                *                                                  Default value: 1.
                *
                *          F                   integer             (Optional) A set of flags specifying various characteristics of the command (see Table 357). 
                *                                                  Defaut value: 0.
                *
                *          P                   string or           (Optional) Data that was posted to the URL.
                *                              stream
                *
                *          CT                  ASCII string        (Optional) A content type describing the data posted to the URL. 
                *                                                  Default value: application/x-www-form-urlencoded.
                *
                *          H                   string              (Optional) Additional HTTP request headers sent to the URL.
                *
                *          S                   dictionary          (Optional) A command settings dictionary containing settings used in the conversion process (see 14.10.5.4, “Command Settings”).
                *
                *The URL entry shall contain the initial URL for the retrieval command. 
                *The L (levels) entry shall contain the number of levels of the hyperlinked URL hierarchy to follow from this URL, creating PDF pages from the retrieved material. 
                *If the L entry is omitted, its value shall be assumed to be 1, denoting retrieval of the initial URL only.
                *
                *The value of the command dictionary’s F entry shall be an integer that shall be interpreted as an array of flags specifying various characteristics of the command.
                *The flags shall be interpreted as defined in Table 358.
                *Only those flags defined in Table 358 may be set to 1; all other flags shall be 0.
                *Flags not defined in Table 358 are reserved for future use, and shall not be used by a conforming reader.
                *
                *NOTE 3        The low-order bit of the flags value is referred to as being at bit-position 1.     
                *
                *Table 358 - Web Capture command flags
                *
                *          [Bit Position]                  [Name]                  [Meaning]
                *
                *          1                               SameSite                If set, pages were retrieved only from the host specified in the initial URL.
                *
                *          2                               SamePath                If set, pages were retrieved only from the path specified in the initial URL.
                *
                *          3                               Submit                  If set, the command represents a form submission.
                *
                *
                *The SamePath flag shall be set if the retrieval of source content was restricted to source content in the same path as specified in the initial URL. 
                *Source content shall be considered to be in the same path if its scheme and network location components (as defined in Internet RFC 1808, Relative Uniform Resource Locators) match those of the initial URL and its path component matches up to and including the last forward slash (/) character in the initial URL.
                *
                *EXAMPLE 1         the URL
                *                  
                *                  http:*www.adobe.com/fiddle/faddle/foo.html
                *
                *                  is considered to be in the same path as the initial URL
                *
                *                  http:*www.adobe.com/fiddle/initial.html
                *
                *The comparison shall be case-insensitive for the scheme and network location components and case-sensitive for the path component.
                *
                *The Submit flag shall be set when the command represents a form submission.If no P(posted data) entry is present, the submitted data shall be encoded in the URL(an HTTP GET request).
                *If P is present, the command shall be an HTTP POST request.In this case, the value of the Submit flag shall be ignored.
                *
                *NOTE 4        If the posted data is small enough, it may be represented by a string.For large amounts of data, a stream should be used because it can be compressed.
                *
                *The CT(content type) entry shall only be present for POST requests. 
                *It shall describe the content type of the posted data, as described in Internet RFC 2045, Multipurpose Internet Mail Extensions(MIME), Part One: Format of Internet Message Bodies(see the Bibliography).
                *
                *The H (headers) entry, if present, shall specify additional HTTP request headers that were sent in the request for the URL. Each header line in the string shall be terminated with a CARRIAGE RETURN and a LINE FEED,as in this example:
                *
                *EXAMPLE 2     (Referer: http:*frumble.com\015\012From:veeble@frotz.com\015\012)
                *
                *The HTTP request header format is specified in Internet RFC 2616, Hypertext Transfer Protocol—HTTP / 1.1(see the Bibliography).
                *
                *The S(settings) entry specifies a command settings dictionary(see 14.10.5.4, “Command Settings”).
                *Holding settings specific to the conversion engines.
                *
                *14.10.5.4 Command Settings
                *
                *The S(settings) entry in a command dictionary, if present, shall contain a command settings dictionary, which holds settings for conversion engines that shall be used in converting the results of the command to PDF.
                *Table 359 shows the contents of this type of dictionary.
                *If this entry is omitted, default values are assumed.
                *Command settings dictionaries may be shared by any command dictionaries that use the same settings.
                *
                *Table 359 - Entries in a Web Capture command settings dictionary
                *
                *              [Key]                   [Type]                      [Value]
                *
                *              G                       dictionary                  (Optional) A dictionary containing global conversion engine settings relevant to all conversion engines. 
                *                                                                  If this entry is absent, default settings shall be used.
                *
                *              C                       dictionary                  (Optional) Settings for specific conversion engines. Each key in this dictionary is the internal name of a conversion engine. 
                *                                                                  The associated value is a dictionary containing the settings associated with that conversion engine. 
                *                                                                  If the settings for a particular conversion engine are not found in the dictionary, default settings shall be used.
                *
                *Each key in the C dictionary represents the internal name of a conversion engine, which shall be a name object of the following form:
                *
                *              / company:product: version: contentType
                *
                *where
                *
                *              company denotes the name(or abbreviation) of the company that created the conversion engine.
                *
                *              product denotes the name of the conversion engine. This field may be left blank, but the trailing COLON character(3Ah) is still required.
                *
                *              version denotes the version of the conversion engine.
                *
                *              contentType denotes an identifier for the content type the associated settings.shall be used because some converters may handle multiple content types.
                *
                *EXAMPLE       / ADBE:H2PDF:1.0:HTML
                *
                *All fields in the internal name are case-sensitive.
                *The company field shall conform to the naming guidelines described in Annex E.
                *The values of the other fields shall be unrestricted, except that they shall not contain aCOLON.
                *
                *The directed graph of PDF objects rooted by the command settings dictionary shall be entirely self-contained; that is, it shall not contain any object referred to from elsewhere in the PDF file.
                *
                *NOTE      This facilitates the operation of making a deep copy of a command settings dictionary without explicit knowledge of the settings it may contain.
                *
                *14.10.6 Object Attributes Related to Web Capture
                *
                *A given page object or image XObject may belong to at most one Web Capture content set, called its parent content set. 
                *However, the object shall not have direct pointer to its parent content set.
                *Such a pointer maypresent problems for an application that traces all pointers from an object to determine what resources the object depends on.
                *Instead, the object’s ID entry(see Table 30 and Table 89) contains the digital identifier of the parent content set, which shall be used to locate the parent content set via the IDS name tree in the document’s name dictionary. 
                *(If the IDS entry for the identifier contains an array of content sets, the parent maybe found by searching the array for the content set whose O entry includes the child object.)
                *
                *In the course of creating PDF pages from HTML files, Web Capture frequently scales the contents down to fit on fixed-sized pages.
                *The PZ(preferred zoom) entry in a page object(see 7.7.3.3, “Page Objects”) specifies a magnification factor by which the page may be scaled to undo the downscaling and view the page at its original size.
                *That is, when the page is viewed at the preferred magnification factor, one unit in default user space corresponds to one original source pixel.
                */


            }

        //14.11 Prepress Support
        public class Prepress_Support
        {
            /*14.11.1 General
            *
            *This sub-clause describes features of PDF that support prepress production workflows:
            *
            *  •   The specification of page boundaries governing various aspects of the prepress process, such as cropping, bleed, and trimming(14.11.2, “Page Boundaries”)
            *
            *  •   Facilities for including printer’s marks, such as registration targets, gray ramps, colour bars, and cut marks to assist in the production process(14.11.3, “Printer’s Marks”)
            *
            *  •   Information for generating colour separations for pages in a document(14.11.4, “Separation Dictionaries”)
            *
            *  •   Output intents for matching the colour characteristics of a document with those of a target output device or production environment in which it will be printed(14.11.5, “Output Intents”)
            *
            *  •   Support for the generation of traps to minimize the visual effects of misregistration between multiple colorants(14.11.6, “Trapping Support”)
            *
            *  •   The Open Prepress Interface(OPI) for creating low-resolution proxies for high - resolution images(14.11.7, “Open Prepress Interface(OPI)”)
            */

            /*14.11.2 Page Boundaries
            */

                /*14.11.2.1 General
            *
            *A PDF page may be prepared either for a finished medium, such as a sheet of paper, or as part of a prepress process in which the content of the page is placed on an intermediate medium, such as film or an imposed reproduction plate.
            *In the latter case, it is important to distinguish between the intermediate page and the finished page.
            *The intermediate page may often include additional production - related content, such as bleeds or printer marks, that falls outside the boundaries of the finished page.
            *To handle such cases, a PDF page maydefine as many as five separate boundaries to control various aspects of the imaging process:
            *
            *  •   The media box defines the boundaries of the physical medium on which the page is to be printed.
            *      It may include any extended area surrounding the finished page for bleed, printing marks, or other such purposes.
            *      It may also include areas close to the edges of the medium that cannot be marked because of physical limitations of the output device.
            *      Content falling outside this boundary may safely be discarded without affecting the meaning of the PDF file.
            *
            *  •   The crop box defines the region to which the contents of the page shall be clipped (cropped) when displayed or printed. 
            *      Unlike the other boxes, the crop box has no defined meaning in terms of physical page geometry or intended use; it merely imposes clipping on the page contents. 
            *      However, in the absence of additional information (such as imposition instructions specified in a JDF or PJTF job ticket), the crop box determines how the page’s contents shall be positioned on the output medium. 
            *      The default value is the page’s media box.
            *
            *  •   The bleed box(PDF 1.3) defines the region to which the contents of the page shall be clipped when output in a production environment.
            *      This may include any extra bleed area needed to accommodate the physical limitations of cutting, folding, and trimming equipment.
            *      The actual printed page may include printing marks that fall outside the bleed box. 
            *      The default value is the page’s crop box.
            *
            *  •   The trim box(PDF 1.3) defines the intended dimensions of the finished page after trimming. 
            *      It may be smaller than the media box to allow for production - related content, such as printing instructions, cut marks, or colour bars.
            *      The default value is the page’s crop box.
            *
            *  •   The art box(PDF 1.3) defines the extent of the page’s meaningful content(including potential white space) as intended by the page’s creator.
            *      The default value is the page’s crop box.
            *
            *The page object dictionary specifies these boundaries in the MediaBox, CropBox, BleedBox, TrimBox, and ArtBox entries, respectively(see Table 30).
            *All of them are rectangles expressed in default user space units.
            *The crop, bleed, trim, and art boxes shall not ordinarily extend beyond the boundaries of the media box.
            *If they do, they are effectively reduced to their intersection with the media box.Figure 86 illustrates the relationships among these boundaries. 
            *(The crop box is not shown in the figure because it has no defined relationship with any of the other boundaries.)
            *
            *(see Figure 86 - Page boundaries, on page 629)
            *
            *NOTE 1        How the various boundaries are used depends on the purpose to which the page is being put. 
            *              The following are typical purposes:
            *
            *              Placing the content of a page in another application. 
            *              The art box determines the boundary of the content that is to be placed in the application. 
            *              Depending on the applicable usage conventions, the placed content may be clipped to either the art box or the bleed box. 
            *              For example, a quarter - page advertisement to be placed on a magazine page might be clipped to the art box on the two sides of the ad that face into the middle of the page and to the bleed box on the two sides that bleed over the edge of the page.
            *              The media box and trim box are ignored.
            *
            *              Printing a finished page. 
            *              This case is typical of desktop or shared page printers, in which the page content is positioned directly on the final output medium.
            *              The art box and bleed box are ignored. The media box may be used as advice for selecting media of the appropriate size.
            *              The crop box and trim box, if present, should be the same as the media box.
            *
            *              Printing an intermediate page for use in a prepress process.
            *              The art box is ignored.
            *              The bleed box defines the boundary of the content to be imaged.
            *              The trim box specifies the positioning of the content on the medium; it may also be used to generate cut or fold marks outside the bleed box.
            *              Content falling within the media box but outside the bleed box may or may not be imaged, depending on the specific production process being used.
            *
            *              Building an imposition of multiple pages on a press sheet. 
            *              The art box is ignored. 
            *              The bleed box defines the clipping boundary of the content to be imaged; content outside the bleed box is ignored. 
            *              The trim box specifies the positioning of the page’s content within the imposition. 
            *              Cut and fold marks are typically generated for the imposition as a whole.
            *
            *NOTE 2        In the preceding scenarios, an application that interprets the bleed, trim, and art boxes for some purpose typically alters the crop box so as to impose the clipping that those boxes prescribe.
            */

                /*14.11.2.2 Display of Page Boundaries
            *
            *Conforming readers may offer the ability to display guidelines on the screen for the various page boundaries.
            *The optional BoxColorInfo entry in a page object(see 7.7.3.3, “Page Objects”) holds a box colour information dictionary(PDF 1.4) specifying the colours and other visual characteristics to be used for such display. 
            *Conforming readers typically provide a user interface to allow the user to set these characteristics interactively.
            *
            *NOTE      This information is page-specific and may vary from one page to another.
            *
            *As shown in Table 360, the box colour information dictionary contains an optional entry for each of the possible page boundaries other than the media box.
            *The value of each entry is a box style dictionary, whose contents are shown in Table 361. 
            *If a given entry is absent, the conforming reader shall use its own current default settings instead.
            *
            *
            *Table 360 - Entries in a box colour information dictionary
            *
            *          [Key]               [Type]              [Value]
            *
            *          CropBox             dictionary          (Optional) A box style dictionary (see Table 361) specifying the visual characteristics for displaying guidelines for the page’s crop box. 
            *                                                  This entry shall be ignored if no crop box is defined in the page object.
            *
            *          BleedBox            dictionary          (Optional) A box style dictionary (see Table 361) specifying the visual characteristics for displaying guidelines for the page’s bleed box. 
            *                                                  This entry shall be ignored if no bleed box is defined in the page object.
            *
            *          TrimBox             dictionary          (Optional) A box style dictionary (see Table 361) specifying the visual characteristics for displaying guidelines for the page’s trim box. 
            *                                                  This entry shall be ignored if no trim box is defined in the page object.
            *
            *          ArtBox              dictionary          (Optional) A box style dictionary (see Table 361) specifying the visual characteristics for displaying guidelines for the page’s art box. 
            *                                                  This entry shall be ignored if no art box is defined in the page object.
            *
            *Table 361 - Entries in a box style dictionary
            *
            *          [Key]               [Type]              [Value]
            *
            *          C                   array               (Optional) An array of three numbers in the range 0.0 to 1.0, representing the components in the DeviceRGB colour space of the colour to be used for displaying the guidelines. 
            *                                                  Default value: [0.0 0.0 0.0].
            *
            *          W                   number              (Optional) The guideline width in default user space units. 
            *                                                  Default value: 1.
            *
            *          S                   name                (Optional) The guideline style:
            *
            *                                                  S       (Solid) A solid rectangle.
            *
            *                                                  D       (Dashed) A dashed rectangle.The dash pattern shall be specified by the D entry.
            *
            *                                                  Other guideline styles may be defined in the future. 
            *                                                  Default value: S.
            *
            *          D                   array               (Optional) A dash array defining a pattern of dashes and gaps to be used in drawing dashed guidelines (guideline style D). 
            *                                                  The dash array shall be specified in default user space units, in the same format as in the line dash pattern parameter of the graphics state (see 8.4.3.6, “Line Dash Pattern”). 
            *                                                  The dash phase shall not be specified and shall be assumed to be 0.
            *
            *                                                  EXAMPLE         A D entry of[3 2] specifies guidelines drawn with 3 - point dashes alternating with 2 - point gaps.
            *                                                  Default value: [3].
            *
            */
    
            /*14.11.3 Printer’s Marks
            *
            *Printer’s marks are graphic symbols or text added to a page to assist production personnel in identifying components of a multiple - plate job and maintaining consistent output during production.
            *Examples commonly used in the printing industry include:
            *
            *  •   Registration targets for aligning plates
            *
            *  •   Gray ramps and colour bars for measuring colours and ink densities
            *
            *  •   Cut marks showing where the output medium is to be trimmed
            *
            *Although conforming writers traditionally include such marks in the content stream of a document, they are logically separate from the content of the page itself and typically appear outside the boundaries(the crop box, trim box, and art box) defining the extent of that content(see 14.11.2, “Page Boundaries”).
            *
            *Printer’s mark annotations(PDF 1.4) provide a mechanism for incorporating printer’s marks into the PDF representation of a page, while keeping them separate from the actual page content.
            *Each page in a PDF document may contain any number of such annotations, each of which represents a single printer’s mark.
            *
            *NOTE 1        Because printer’s marks typically fall outside the page’s content boundaries, each mark is represented as a separate annotation. 
            *              Otherwise—if, for example, the cut marks at the four corners of the page were defined in a single annotation—the annotation rectangle would encompass the entire contents of the page and could interfere with the user’s ability to select content or interact with other annotations on the page.
            *              Defining printer’s marks in separate annotations also facilitates the implementation of a drag - and - drop user interface for specifying them.
            *
            *
            *
            *The visual presentation of a printer’s mark shall be defined by a form XObject specified as an appearance stream in the N (normal) entry of the printer’s mark annotation’s appearance dictionary (see 12.5.5, “Appearance Streams”). 
            *More than one appearance may be defined for the same printer’s mark to meet the requirements of different regions or production facilities. 
            *In this case, the appearance dictionary’s N entry holds a subdictionary containing the alternate appearances, each identified by an arbitrary key. 
            *The AS (appearance state) entry in the annotation dictionary designates one of them to be displayed or printed.
            *
            *NOTE 2        The printer’s mark annotation’s appearance dictionary may include R(rollover) or D(down) entries, but appearances defined in either of these entries are never displayed or printed.
            *
            *Like all annotations, a printer’s mark annotation shall be defined by an annotation dictionary(see 12.5.2, “Annotation Dictionaries”); its annotation type is PrinterMark.
            *The AP (appearances)and F(flags) entries(which ordinarily are optional) shall be present, as shall the AS(appearance state) entry if the appearance dictionary AP contains more than one appearance stream.
            *The Print and ReadOnly flags in the F entry shall be set and all others clear(see 12.5.3, “Annotation Flags”). 
            *Table 362 shows an additional annotation dictionary entry specific to this type of annotation.
            *
            *Table 362 - Additional entries specific to a printer's mark annotation
            *
            *              [Key]               [Type]              [Value]
            *
            *              Subtype             name                (Required) The type of annotation that this dictionary describes; shall be PrinterMark for a printer’s mark annotation.
            *
            *              MN                  name                (Optional) An arbitrary name identifying the type of printer’s mark, such as ColorBar or RegistrationTarget.
            *
            *The form dictionary defining a printer’s mark may contain the optional entries shown in Table 363 in addition to the standard ones common to all form dictionaries (see 8.10.2, “Form Dictionaries”).
            *
            *Table 363 - Additional entries specific to a printer's mark form dictionary
            *
            *              [Key]               [Type]              [Value]
            *
            *              MarkStyle           text string         (Optional; PDF 1.4) A text string representing the printer’s mark in human-readable form and suitable for presentation to the user.
            *
            *              Colorants           dictionary          (Optional; PDF 1.4) A dictionary identifying the individual colorants associated with a printer’s mark, such as a colour bar. 
            *                                                      For each entry in this dictionary, the key is a colorant name and the value is an array defining a Separation colour space for that colorant (see 8.6.6.4, “Separation Colour Spaces”). 
            *                                                      The key shall match the colorant name given in that colour space.
            */

            /*14.11.4 Separation Dictionaries
            *
            *In high-end printing workflows, pages are ultimately produced as sets of separations, one per colorant(see 8.6.6.4, “Separation Colour Spaces”). 
            *Ordinarily, each page in a PDF file shall be treated as a composite page that paints graphics objects using all the process colorants and perhaps some spot colorants as well.
            *In other words, all separations for a page shall be generated from a single PDF description of that page.
            *
            *In some workflows, however, pages are preseparated before generating the PDF file.
            *In a preseparated PDF file, the separations for a page shall be described as separate page objects, each painting only a single colorant(usually specified in the DeviceGray colour space).
            *In this case, additional information is needed to identify the actual colorant associated with each separation and to group together the page objects representing all the separations for a given page.
            *This information shall be contained in a separation dictionary(PDF 1.3) in the SeparationInfo entry of each page object(see 7.7.3.3, “Page Objects”).
            *Table 364 shows the contents of this type of dictionary.
            *
            *Table 364 - Entries in a separation dictionary
            *
            *          [Key]                   [Type]              [Value]
            *
            *          Pages                   array               (Required) An array of indirect references to page objects representing separations of the same document page. 
            *                                                      One of the page objects in the array shall be the one with which this separation dictionary is associated, and all of them shall have separation dictionaries (SeparationInfoentries) containing Pages arrays identical to this one.
            *
            *          DeviceColorant          name or             (Required) The name of the device colorant to be used in rendering this separation, such as Cyan or PANTONE 35 CV.
            *                                  string
            *
            *          ColorSpace              array               (Optional) An array defining a Separation or DeviceN colour space (see 8.6.6.4, “Separation Colour Spaces” and 8.6.6.5, “DeviceN Colour Spaces”). 
            *                                                      It provides additional information about the colour specified by DeviceColorant—in particular, the alternate colour space and tint transformation function that shall be used to represent the colorant as a process colour. 
            *                                                      This information enables a conforming reader to preview the separation in a colour that approximates the device colorant.
            *                                                      The value of DeviceColorant shall match the space’s colorant name(if it is a Separation space) or be one of the space’s colorant names(if it is a DeviceN space).
            */
                
            /*14.11.5 Output Intents
            *
            *Output intents (PDF 1.4) provide a means for matching the colour characteristics of a PDF document with those of a target output device or production environment in which the document will be printed. 
            *The optional OutputIntents entry in the document catalogue (see 7.7.2, “Document Catalog”) holds an array of output intent dictionaries, each describing the colour reproduction characteristics of a possible output device or production condition. 
            *The contents of these dictionaries may vary for different devices and conditions. 
            *The dictionary’s Sentry specifies an output intent subtype that determines the format and meaning of the remaining entries.
            *
            *NOTE 1        This use of multiple output intents allows the production process to be customized to the expected workflow and the specific tools available. 
            *              For example, one production facility might process files conforming to a recognized standard such as PDF / X - 1, while another uses the PDF / A standard to produce RGB output for document distribution on the Web.
            *              Each of these workflows would require different sets of output intent information.
            *              Multiple output intents also allow the same PDF file to be distributed unmodified to multiple production facilities.
            *              The choice of which output intent to use in a given production environment is a matter for agreement between the purchaser and provider of production services.
            *              PDF intentionally does not include a selector for choosing a particular output intent from within the PDF file.
            *
            *At the time of publication, three output intent subtypes have been defined: GTS_PDFX corresponding to the PDF / X format standard specified in ISO 15930, GTS_PDFA1 corresponding to the PDF / A standard as defined by ISO 19005, and ISO_PDFE1 corresponding to the PDF / E standard as defined by ISO 24517.
            *Table 365shows the contents of this type of output intent dictionary.
            *Other subtypes may be added in the future; the names of any such additional subtypes shall conform to the naming guidelines described in Annex E.
            *
            *Table 365 - Entries in an output intent dictionary
            *
            *          [Key]                               [Type]                  [Value]
            *
            *          Type                                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be OutputIntent for an output intent dictionary.
            *
            *          S                                   name                    (Required) The output intent subtype; shall be either one of GTS_PDFX, GTS_PDFA1, ISO_PDFE1 or a key defined by an ISO 32000 extension.
            *
            *          OutputCondition                     text string             (Optional) A text string concisely identifying the intended output device or production condition in human-readable form. 
            *                                                                      This is the preferred method of defining such a string for presentation to the user.
            *
            *          OutputConditionIdentifier           text string             (Required) A text string identifying the intended output device or production condition in human- or machine-readable form. 
            *                                                                      If human-readable, this string may be used in lieu of an OutputCondition string for presentation to the user.
            *
            *                                                                      A typical value for this entry may be the name of a production condition maintained in an industry - standard registry such as the ICC Characterization Data Registry(see the Bibliography).
            *                                                                      If the designated condition matches that in effect at production time, the production software is responsible for providing the corresponding ICC profile as defined in the registry.
            *
            *                                                                      If the intended production condition is not a recognized standard, the value of this entry may be Custom or an application - specific, machine - readable name.
            *                                                                      The DestOutputProfile entry defines the ICC profile, and the Info entry shall be used for further human-readable identification.
            *
            *          RegistryName                        text string             (Optional) An text string (conventionally a uniform resource identifier, or URI) identifying the registry in which the condition designated by OutputConditionIdentifier is defined.
            *
            *          Info                                text string             (Required if OutputConditionIdentifier does not specify a standard production condition; optional otherwise) 
            *                                                                      A human-readable text string containing additional information or comments about the intended target device or production condition.
            *
            *          DestOutputProfile                   stream                  (Required if OutputConditionIdentifier does not specify a standard production condition; optional otherwise) 
            *                                                                      An ICC profile stream defining the transformation from the PDF document’s source colours to output device colorants.
            *
            *                                                                      The format of the profile stream is the same as that used in specifying an ICCBased colour space(see 8.6.5.5, “ICCBased Colour Spaces”).
            *                                                                      The output transformation uses the profile’s “from CIE” information(BToA in ICC terminology); the “to CIE” (AToB)information may optionally be used to remap source colour values to some other destination colour space, such as for screen preview or hardcopy proofing.
            *
            *NOTE 2        PDF/X is actually a family of standards representing varying levels of conformance. 
            *              The standard for a given conformance level may prescribe further restrictions on the usage and meaning of entries in the output intent dictionary. 
            *              Any such restrictions take precedence over the descriptions given in Table 364.
            *
            *The ICC profile information in an output intent dictionary supplements rather than replaces that in an ICCBased or default colour space(see 8.6.5.5, “ICCBased Colour Spaces,” and 8.6.5.6, “Default Colour Spaces”). 
            *Those mechanisms are specifically intended for describing the characteristics of source colour component values.
            *An output intent can be used in conjunction with them to convert source colours to those required for a specific production condition or to enable the display or proofing of the intended output.
            *
            *The data in an output intent dictionary shall be provided for informational purposes only, and conforming readers are free to disregard it.
            *In particular, there is no expectation that PDF production tools automatically convert colours expressed in the same source colour space to the specified target space before generating output. 
            *(In some workflows, such conversion may, in fact, be undesirable).
            *
            *NOTE      When working with CMYK source colours tagged with a source ICC profile solely for purposes of characterization, converting such colours from four components to three and back is unnecessary and will result in a loss of fidelity in the values of the black component; see 8.6.5.7, “Implicit Conversion of CIE-Based Colour Spaces” for further discussion.) 
            *          On the other hand, when source colours are expressed in different base colour spaces—for example, when combining separately generated images on the same PDF page—it is possible (though not required) to use the destination profile specified in the output intent dictionary to convert source colours to the same target colour space.
            *
            *EXAMPLE 1     This Example shows a PDF / X output intent dictionary based on an industry - standard production condition(CGATS TR 001) from the ICC Characterization Data Registry. 
            *              Example 2 shows one for a custom production condition.
            *
            *              << / Type / OutputIntent                                % Output intent dictionary
            *                 / S / GTS_PDFX
            *                 / OutputCondition(CGATS TR 001(SWOP))
            *                 / OutputConditionIdentifier(CGATS TR 001)
            *                 / RegistryName(http:*www.color.org)
            *                 / DestOutputProfile 100 0 R
            *              >>
            *              100 0 obj                                               % ICC profile stream
            *                  << / N 4
            *                      / Length 1605
            *                      / Filter / ASCIIHexDecode
            *                  >>
            *              stream
            *              00 00 02 0C 61 70 … >
            *              endstream
            *              endobj
            *
            *EXAMPLE 2     << / Type / OutputIntent                                % Output intent dictionary
            *                 / S / GTS_PDFX
            *                 / OutputCondition(Coated)
            *                 / OutputConditionIdentifier(Custom)
            *                 / Info(Coated 150lpi)
            *                 / DestOutputProfile 100 0 R
            *              >>
            *              100 0 obj                                               % ICC profile stream
            *                  << / N 4
            *                     / Length 1605
            *                     / Filter / ASCIIHexDecode
            *                  >>
            *              stream
            *                  00 00 02 0C 61 70 … >
            *              endstream
            *              endobj
            */

            /*14.11.6 Trapping Support
            */

                /*14.11.6.1 General
            *
            *On devices such as offset printing presses, which mark multiple colorants on a single sheet of physical medium, mechanical limitations of the device can cause imprecise alignment, or misregistration, between colorants. 
            *This can produce unwanted visual artifacts such as brightly coloured gaps or bands around the edges of printed objects. 
            *In high-quality reproduction of colour documents, such artifacts are commonly avoided by creating an overlap, called a trap, between areas of adjacent colour.
            *
            *NOTE      Figure 87 shows an example of trapping.
            *          The light and medium grays represent two different colorants, which are used to paint the background and the glyph denoting the letter A. 
            *          The first figure shows the intended result, with the two colorants properly registered. The second figure shows what happens when the colorants are misregistered. 
            *          In the third figure, traps have been overprinted along the boundaries, obscuring the artifacts caused by the misregistration. 
            *          (For emphasis, the traps are shown here in dark gray; in actual practice, their colour will be similar to one of the adjoining colours.)
            *
            *(see Figure 87 - Trapping example, on page 636)
            *
            *Trapping may be implemented by the application generating a PDF file, by some intermediate application that adds traps to a PDF document, or by the raster image processor (RIP) that produces final output. 
            *In the last two cases, the trapping process is controlled by a set of trapping instructions, which define two kinds of information:
            *
            *  •   Trapping zones within which traps should be created
            *
            *  •   Trapping parameters specifying the nature of the traps within each zone
            *
            *Trapping zones and trapping parameters are discussed fully in Sections 6.3.2 and 6.3.3, respectively, of the PostScript Language Reference, Third Edition. 
            *Trapping instructions are not directly specified in a PDF file(as they are in a PostScript file).
            *Instead, they shall be specified in a job ticket that accompanies the PDF file or isembedded within it. 
            *Various standards exist for the format of job tickets; two of them, JDF(Job Definition Format) and PJTF(Portable Job Ticket Format), are described in the CIP4 document JDF Specification and in Adobe Technical Note #5620, Portable Job Ticket Format (see the Bibliography).
            *
            *When trapping is performed before the production of final output, the resulting traps shall be placed in the PDF file for subsequent use. 
            *The traps themselves shall be described as a content stream in a trap network annotation(see 14.11.6.2, “Trap Network Annotations”).
            *The stream dictionary may include additional entries describing the method that was used to produce the traps and other information about their appearance.
            */

                /*14.11.6.2 Trap Network Annotations
            *
            *A complete set of traps generated for a given page under a specified set of trapping instructions is called a trap network(PDF 1.3).
            *It is a form XObject containing graphics objects for painting the required traps on the page.
            *A page may have more than one trap network based on different trapping instructions, presumably intended for different output devices.All of the trap networks for a given page shall be contained in a single trap network annotation(see 12.5, “Annotations”).
            *There may be at most one trap network annotation per page, which shallbe the last element in the page’s Annots array(see 7.7.3.3, “Page Objects”).
            *This ensures that the trap network shall be printed after all of the page’s other contents.
            *
            *The form XObject defining a trap network shall be specified as an appearance stream in the N(normal) entry of the trap network annotation’s appearance dictionary(see 12.5.5, “Appearance Streams”).
            *If more than one trap network is defined for the same page, the N entry holds a subdictionary containing the alternate trap networks, each identified by an arbitrary key.
            *The AS(appearance state) entry in the annotation dictionary designates one of them as the current trap network to be displayed or printed.
            *
            *NOTE 1        The trap network annotation’s appearance dictionary may include R(rollover) or D(down) entries, but appearances defined in either of these entries are never printed.
            *
            *Like all annotations, a trap network annotation shall be defined by an annotation dictionary (see 12.5.2, “Annotation Dictionaries”); its annotation type is TrapNet. 
            *The AP (appearances), AS (appearance state), and F (flags) entries (which ordinarily are optional) shall be present, with the Print and ReadOnly flags set and all others clear (see 12.5.3, “Annotation Flags”). 
            *Table 366 shows the additional annotation dictionary entries specific to this type of annotation.
            *
            *The Version and AnnotStates entries, if present, shall be used to detect changes in the content of a page that might require regenerating its trap networks. 
            *The Version array identifies elements of the page’s content that might be changed by an editing application and thus invalidate its trap networks. 
            *Because there is at most one Version array per trap network annotation(and thus per page), any conforming writer that generates a new trap network shall also verify the validity of existing trap networks by enumerating the objects identified in the array and verifying that the results exactly match the array’s current contents.
            *Any trap networks found to be invalid shall be regenerated.
            *
            *The LastModified entry may be used in place of the Version array to track changes to a page’s trap network. 
            *(The trap network annotation shall include either a LastModified entry or the combination of Version and AnnotStates, but not all three.) 
            *If the modification date in the LastModified entry of the page object(see 7.7.3.3, “Page Objects”) is more recent than the one in the trap network annotation dictionary, the page’s trap networks are invalid and shall be regenerated.
            *
            *NOTE 2        Not all editing applications correctly maintain these modification dates.
            *
            *This method of tracking trap network modifications may be used reliably only in a controlled workflow environment where the integrity of the modification dates is assured.
            *
            *Table 366 - Additional entries specific to a trap network annotation
            *
            *              [Key]               [Type]              [Value]
            *
            *              Subtype             name                (Required) The type of annotation that this dictionary describes; shall be TrapNet for a trap network annotation.
            *
            *              LastModified        date                (Required if Version and AnnotStates are absent; shall be absent if Version and AnnotStates are present; PDF 1.4) 
            *                                                      The date and time (see 7.9.4, “Dates”) when the trap network was most recently modified.
            *
            *              Version             array               (Required if AnnotStates is present; shall be absent if LastModifiedis present) 
            *                                                      An unordered array of all objects present in the page description at the time the trap networks were generated and that, if changed, could affect the appearance of the page. 
            *                                                      If present, the array shall include the following objects:
            *
            *                                                      •   All content streams identified in the page object’s Contents entry(see 7.7.3.3, “Page Objects”)
            *
            *                                                      •   All resource objects(other than procedure sets) in the page’s resource dictionary(see 7.8.3, “Resource Dictionaries”)
            *
            *                                                      •   All resource objects(other than procedure sets) in the resource dictionaries of any form XObjects on the page(see 8.10, “Form XObjects”)
            *
            *                                                      •   All OPI dictionaries associated with XObjects on the page(see 14.11.7, “Open Prepress Interface(OPI)”)
            *
            *              AnnotStates         array               (Required if Version is present; shall be absent if LastModified is present) An array of name objects representing the appearance states (value of the AS entry) for annotations associated with the page. 
            *                                                      The appearance states shall be listed in the same order as the annotations in the page’s Annots array (see 7.7.3.3, “Page Objects”). 
            *                                                      For an annotation with no AS entry, the corresponding array element should be null. 
            *                                                      No appearance state shall be included for the trap network annotation itself.
            *
            *              FontFauxing         array               (Optional) An array of font dictionaries representing fonts that were fauxed (replaced by substitute fonts) during the generation of trap networks for the page.
            */

                /*14.11.6.3 Trap Network Appearances
            *
            *Each entry in the N (normal) subdictionary of a trap network annotation’s appearance dictionary holds an appearance stream defining a trap network associated with the given page. 
            *Like all appearances, a trap network is a stream object defining a form XObject (see 8.10, “Form XObjects”). 
            *The body of the stream contains the graphics objects needed to paint the traps making up the trap network. 
            *Its dictionary entries include, besides the standard entries for a form dictionary, the additional entries shown in Table 367.
            *
            *Table 367 - Additional entries specific to a trap network appearance stream
            *
            *              [Key]                       [Type]              [Value]
            *
            *              PCM                         name                (Required) The name of the process colour model that was assumed when this trap network was created; equivalent to the PostScript page device parameter ProcessColorModel (see Section 6.2.5 of the PostScript Language Reference, Third Edition). 
            *                                                              Valid values are DeviceGray, DeviceRGB, DeviceCMYK, DeviceCMY, DeviceRGBK, and DeviceN.
            *
            *              SeparationColorNames        array               (Optional) An array of names identifying the colorants that were assumed when this network was created; equivalent to the PostScript page device parameter of the same name (see Section 6.2.5 of the PostScript Language Reference, Third Edition). 
            *                                                              Colourants implied by the process colour model PCM are available automatically and need not be explicitly declared. 
            *                                                              If this entry is absent, the colorants implied by PCM shall be assumed.
            *
            *              TrapRegions                 array               (Optional) An array of indirect references to TrapRegion objects defining the page’s trapping zones and the associated trapping parameters, as described in Adobe Technical Note #5620, Portable Job Ticket Format. 
            *                                                              These references refer to objects comprising portions of a PJTF job ticket that shall be embedded in the PDF file. 
            *                                                              When the trapping zones and parameters are defined by an external job ticket (or by some other means, such as JDF), this entry shall be absent.
            *
            *              TrapStyles                  text string         (Optional) A human-readable text string that applications may use to describe this trap network to the user.
            *                                                              EXAMPLE     To allow switching between trap networks).
            *
            *NOTE      Preseparated PDF files (see 14.11.4, “Separation Dictionaries”) may not be trapped because traps are defined along the borders between different colours and a preseparated file uses only one colour. 
            *          Therefore, preseparation shall occur after trapping, not before. 
            *          An conforming writer that preseparates a trapped PDF file is responsible for calculating new Version arrays for the separated trap networks.
            */

            /*14.11.7 Open Prepress Interface (OPI)
            *
            *The workflow in a prepress environment often involves multiple applications in areas such as graphic design, page layout, word processing, photo manipulation, and document construction. 
            *As pieces of the final document are moved from one application to another, it is useful to separate the data of high - resolution images, which can be quite large—in some cases, many times the size of the rest of the document combined—from that of the document itself. 
            *The Open Prepress Interface(OPI) is a mechanism, originally developed by Aldus Corporation, for creating low-resolution placeholders, or proxies, for such high-resolution images.
            *The proxy typically consists of a downsampled version of the full-resolution image, to be used for screen display and proofing. 
            *Before the document is printed, it passes through a filter known as an OPI server, which replaces the proxies with the original full-resolution images.
            *
            *NOTE 1    In PostScript programs, OPI proxies are defined by PostScript code surrounded by special OPI comments, which specify such information as the placement and cropping of the image and adjustments to its size, rotation, colour, and other attributes.
            *
            *In PDF, proxies shall be embedded in a document as image or form XObjects with an associated OPI dictionary(PDF 1.2). 
            *This dictionary contains the same information that the OPI comments convey in PostScript.
            *Two versions of OPI shall be supported, versions 1.3 and 2.0.
            *In OPI 1.3, a proxy consisting of a single image, with no changes in the graphics state, may be represented as an image XObject; otherwise it shall be a form XObject.
            *In OPI 2.0, the proxy always entails changes in the graphics state and hence shall be represented as a form XObject.
            *
            *An XObject representing an OPI proxy shall contain an OPI entry in its image or form dictionary(see Table 89and Table 95).
            *The value of this entry is an OPI version dictionary(Table 368) identifying the version of OPI to which the proxy corresponds. 
            *This dictionary consists of a single entry, whose key is the name 1.3 or 2.0 and whose value is the OPI dictionary defining the proxy’s OPI attributes.
            *
            *Table 368 - Entry in an OPI version dictionary
            *
            *          [Key]               [Type]                  [Value]
            *
            *          version             dictionary              (Required; PDF 1.2) An OPI dictionary specifying the attributes of this proxy (see Tables 369 and 370).
            *           number                                     The key for this entry shall be the name 1.3 or 2.0, identifying the version of OPI to which the proxy corresponds.
            *
            *
            *NOTE 2    As in any other PDF dictionary, the key in an OPI version dictionary is a name object. 
            *          The OPI version dictionary would thus be written in the PDF file in either the form
            *
            *          << / 1.3 d 0 R >>                           % OPI 1.3 dictionary
            *          or
            *          << / 2.0 d 0 R >>                           % OPI 2.0 dictionary
            *
            *          where d is the object number of the corresponding OPI dictionary.
            *
            *Table 369 and Table 370 describe the contents of the OPI dictionaries for OPI 1.3 and OPI 2.0, respectively, along with the corresponding PostScript OPI comments.
            *The dictionary entries shall be listed in the order in which the corresponding OPI comments appear in a PostScript program.
            *Complete details on the meanings of these entries and their effects on OPI servers can be found in OPI: Open Prepress Interface Specification 1.3
            *and Adobe Technical Note #5660, Open Prepress Interface (OPI) Specification, Version 2.0.
            *
            *Table 369 - Entries in a version 1.3 OPI dictionary
            *
            *          [Key]                   [Type]                  [OPI Comment]               [Value]
            *
            *          Type                    name                                                (Optional) The type of PDF object that this dictionary describes; if present, shall be OPIfor an OPI dictionary.
            *
            *          Version                 number                                              (Required) The version of OPI to which this dictionary refers; shall be the number 1.3 (not the name 1.3, as in an OPI version dictionary).
            *
            *          F                       file specification      %ALDImageFilename           (Required) The external file containing the image corresponding to this proxy.
            *
            *          ID                      byte string             %ALDImageID                 (Optional) An identifying string denoting the image.
            *
            *          Comments                text string             %ALDObjectComments          (Optional) A human-readable comment, typically containing instructions or suggestions to the operator of the OPI server on how to handle the image.
            *
            *          Size                    array                   %ALDImageDimensions         (Required) An array of two integers of the form
            *
            *                                                                                      [pixelsWide pixelsHigh]
            *                                                                                      specifying the dimensions of the image in pixels.
            *
            *          CropRect                rectangle               %ALDImageCropRect           (Required) An array of four integers of the form
            *
            *                                                                                      [left top right bottom]
            *                                                                                      specifying the portion of the image to be used.
            *
            *          CropFixed               array                   %ALDImageCropFixed          (Optional) An array with the same form and meaning as CropRect, but expressed in real numbers instead of integers. 
            *                                                                                      Default value: the value of CropRect.
            *
            *          Position                array                   %ALDImagePosition           (Required) An array of eight numbers of the form
            *
            *                                                                                      [llx lly ulx uly urx ury lrx lry]
            *
            *                                                                                      specifying the location on the page of the cropped image, where (llx, lly) are the user space coordinates of the lower-left corner, (ulx, uly) are those of the upper-left corner, 
            *                                                                                      (urx, ury) are those of the upper-right corner, and (lrx, lry) are those of the lower-right corner.
            *                                                                                      The specified coordinates shall define a parallelogram; that is, they shall satisfy the conditions
            *
            *                                                                                      ulx − llx = urx − lrx
            *
            *                                                                                      and
            *
            *                                                                                      uly − lly = ury − lry
            *
            *                                                                                      The combination of Position and CropRectdetermines the image’s scaling, rotation, reflection, and skew.
            *
            *          Resolution              array                   %ALDImageResolution         (Optional) An array of two numbers of the form
            *
            *                                                                                      [horizRes vertRes]
            *                                                                                      specifying the resolution of the image in samples per inch.
            *
            *          ColorType               name                    %ALDImageColorType          (Optional) The type of colour specified by the Color entry. Valid values are Process, Spot, and Separation. 
            *                                                                                      Default value: Spot.
            *
            *          Color                   array                   %ALDImageColor              (Optional) An array of four numbers and a byte string of the form
            *
            *                                                                                      [C M Y K colorName]
            *
            *                                                                                      specifying the value and name of the colour in which the image is to be rendered.
            *                                                                                      The values of C, M, Y, and K shall all be in the range 0.0 to 1.0. 
            *
            *                                                                                      Default value: [0.0 0.0 0.0 1.0 (Black)].
            *
            *          Tint                    number                  %ALDImageTint               (Optional) A number in the range 0.0 to 1.0 specifying the concentration of the colour specified by Color in which the image is to be rendered. 
            *                                                                                      Default value: 1.0.
            *
            *          Overprint               boolean                 %ALDImageOverprint          (Optional) A flag specifying whether the image is to overprint (true) or knock out (false) underlying marks on other separations. 
            *                                                                                      Default value: false.
            *
            *          ImageType               array                   %ALDImageType               (Optional) An array of two integers of the form
            *
            *                                                                                      [samples bits]
            *                                                                                      specifying the number of samples per pixel and bits per sample in the image.
            *
            *          GrayMap                 array                   %ALDImageGrayMap            (Optional) An array of 2n integers in the range 0 to 65,535 (where n is the number of bits per sample) recording changes made to the brightness or contrast of the image.
            *
            *          Transparency            boolean                 %ALDImageTransparency       (Optional) A flag specifying whether white pixels in the image shall be treated as transparent. 
            *                                                                                      Default value: true.
            *
            *          Tags                    array                   %ALDImageAsciiTag<NNN>      (Optional) An array of pairs of the form
            *
            *                                                                                      [tagNum1 tagText1 … tagNumn tagTextn]
            *
            *                                                                                      where each tagNum is an integer representing a TIFF tag number and each tagText is an ASCII string representing the corresponding ASCII tag value.
            *
            *
            *Table 370 - Entries in a version 2.0 OPI dictionary
            *
            *          [Key]                               [Type]                  [OPI Comment]                   [Value]
            *
            *          Type                                name                                                    (Optional) The type of PDF object that this dictionary describes; if present, shall be OPIfor an OPI dictionary.
            *
            *          Version                             number                                                  (Required) The version of OPI to which this dictionary refers; shall be the number 2 or 2.0 (not the name 2.0, as in an OPI version dictionary).
            *
            *          F                                   file specification      %%ImageFilename                 (Required) The external file containing the low- resolution proxy image.
            *
            *          MainImage                           byte string             %%MainImage                     (Optional) The pathname of the file containing the full-resolution image corresponding to this proxy, or any other identifying string that uniquely identifies the full-resolution image.
            *
            *          Tags                                array                   %%TIFFASCIITag                  (Optional) An array of pairs of the form
            *
            *                                                                                                      [tagNum1 tagText1 … tagNumn tagTextn]
            *
            *                                                                                                      where each tagNum is an integer representing a TIFF tag number and each tagText is an ASCII string or an array of ASCII strings representing the corresponding ASCII tag value.
            *
            *          Size                                array                   %%ImageDimensions               (Optional) An array of two numbers of the form
            *
            *                                                                                                      [width height]
            *
            *                                                                                                      specifying the dimensions of the image in pixels.
            *
            *          CropRect                            rectangle               %%ImageCropRect                 (Optional) An array of four numbers of the form
            *
            *                                                                                                      [left top right bottom]
            *
            *                                                                                                      specifying the portion of the image to be used.
            *
            *                                                                                                      The Size and CropRect entries shall either both be present or both be absent.
            *                                                                                                      If present, they shall satisfy the conditions
            *
            *                                                                                                      0 ≤ left<right ≤ width
            *
            *                                                                                                      and
            *
            *                                                                                                      0 ≤ top<bottom ≤ height
            *
            *                                                                                                      In this coordinate space, the positive y axis extends vertically downward; hence, the requirement that top<bottom.
            *
            *          Overprint                           boolean                 %%ImageOverprint                (Optional) A flag specifying whether the image is to overprint (true) or knock out (false) underlying marks on other separations. 
            *                                                                                                      Default value: false.
            *
            *          Inks                                name or array           %%ImageInks                     (Optional) A name object or array specifying the colorants to be applied to the image. 
            *                                                                                                      The value may be the name full_color or registration or an array of the form
            *
            *                                                                                                      [/monochrome name1 tint1 … namen tintn]
            *
            *                                                                                                      where each name is a string representing the name of a colorant and each tint is a real number in the range 0.0 to 1.0 specifying the concentration of that colorant to be applied.
            *
            *          IncludedImageDimensions             array                   %%IncludedImageDimensions       (Optional) An array of two integers of the form
            *
            *                                                                                                      [pixelsWide pixelsHigh]
            *                  
            *                                                                                                      specifying the dimensions of the included image in pixels.
            *
            *          IncludedImageQuality                number                  %%IncludedImageQuality          (Optional) A number indicating the quality of the included image. Valid values are 1, 2, and 3.
            *
            */

     
        }


    }
}
