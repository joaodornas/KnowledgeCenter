using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfPlugin
{
    //12 Interactive_Features
    public class Interactive_Features
    {

        /*12.1 General
        *
        *For purposes of the trigger events E (enter), X (exit), D (down), and U (up), the term mouse denotes a generic pointing device with the following characteristics:
        *
        *  •   A selection button that can be pressed, held down, and released.If there is more than one mouse button, the selection button is typically the left button.
        *
        *  •   A notion of location—that is, an indication of where on the screen the device is pointing.Location is typically denoted by a screen cursor.
        *
        *  •   A notion of focus—that is, which element in the document is currently interacting with the with the user.In many systems, this element is denoted by a blinking caret, a focus rectangle, or a colour change.
        *
        *This clause describes the PDF features that allow a user to interact with a document on the screen, using the mouse and keyboard(with the exception of multimedia features, which are described in 13, “Multimedia Features”) :
        *
        *  •   Preference settings to control the way the document is presented on the screen(12.2, “Viewer Preferences”)
        *
        *  •   Navigation facilities for moving through the document in a variety of ways(Sections 12.3, “Document-Level Navigation” and 12.4, “Page-Level Navigation”)
        *
        *  •   Annotations for adding text notes, sounds, movies, and other ancillary information to the document(12.5, “Annotations”)
        *
        *  •   Actions that can be triggered by specified events(12.6, “Actions”)
        *
        *  •   Interactive forms for gathering information from the user(12.7, “Interactive Forms”)
        *
        *  •   Digital signatures that authenticate the identity of a user and the validity of the document’s contents (12.8, “Digital Signatures”)
        *
        *  •   Measurement properties that enable the display of real-world units corresponding to objects on a page(12.9, “Measurement Properties”)
        */


        //12.2 Viewer Preferences
        public class Viewer_Preferences
        {

            /*12.2 Viewer Preferences
            *The ViewerPreferences entry in a document’s catalogue(see 7.7.2, “Document Catalog”) designates a viewer preferences dictionary(PDF 1.2) controlling the way the document shall be presented on the screen or in print.
            *If no such dictionary is specified, conforming readers should behave in accordance with their own current user preference settings.
            *Table 150 shows the contents of the viewer preferences dictionary.
            *
            *Table 150 - Entries in a viewer preferences dictionary
            *
            *              [Key]                       [Type]                  [Value]
            *
            *              HideToolbar                 boolean                 (Optional) A flag specifying whether to hide the conforming reader’s tool bars when the document is active. 
            *                                                                  Default value: false.
            *
            *              HideManuebar                boolean                 (Optional) A flag specifying whether to hide the conforming reader’s menu bar when the document is active. 
            *                                                                  Default value: false.
            *
            *              HideWindowUI                boolean                 (Optional) A flag specifying whether to hide user interface elements in the document’s window (such as scroll bars and navigation controls), leaving only the document’s contents displayed. 
            *                                                                  Default value: false.
            *
            *              FitWindow                   boolean                 (Optional) A flag specifying whether to resize the document’s window to fit the size of the first displayed page. 
            *                                                                  Default value: false.
            *
            *              CenterWindow                boolean                 (Optional) A flag specifying whether to position the document’s window in the center of the screen. 
            *                                                                  Default value: false.
            *
            *              DisplayDocTitle             boolean                 (Optional; PDF 1.4) A flag specifying whether the window’s title bar should display the document title taken from the Title entry of the document information dictionary (see 14.3.3, “Document Information Dictionary”). 
            *                                                                  If false, the title bar should instead display the name of the PDF file containing the document. 
            *                                                                  Default value: false.
            *
            *              NonFullScreenPageMode       name                    (Optional) The document’s page mode, specifying how to display the document on exiting full-screen mode:
            *
            *                                                                  UseNoneNeither document outline nor thumbnail images visible
            *       
            *                                                                  UseOutlinesDocument outline visible
            *
            *                                                                  UseThumbsThumbnail images visible
            *
            *                                                                  UseOCOptional content group panel visible
            *
            *                                                                  This entry is meaningful only if the value of the PageMode entry in the Catalog dictionary(see 7.7.2, “Document Catalog”) is FullScreen; it shall be ignored otherwise.
            *                                                                  Default value: UseNone.
            *
            *              Direction                   name                    (Optional; PDF 1.3) The predominant reading order for text:
            *
            *                                                                  L2RLeft to right
            *
            *                                                                  R2LRight to left(including vertical writing systems, such as Chinese, Japanese, and Korean)
            *
            *                                                                  This entry has no direct effect on the document’s contents or page numbering but may be used to determine the relative positioning of pages when displayed side by side or printed n-up.
            *                          
            *                                                                  Default value: L2R.
            *
            *              ViewArea                    name                    (Optional; PDF 1.4) The name of the page boundary representing the area of a page that shall be displayed when viewing the document on the screen. 
            *
            *                                                                  The value is the key designating the relevant page boundary in the page object (see 7.7.3, “Page Tree”and 14.11.2, “Page Boundaries”). 
            *
            *                                                                  If the specified page boundary is not defined in the page object, its default value shall be used, as specified in Table 30. 
            *                                                                  
            *                                                                  Default value: CropBox.
            *
            *                                                                  This entry is intended primarily for use by prepress applications that interpret or manipulate the page boundaries as described in 14.11.2, “Page Boundaries.”
            *
            *                                                                  NOTE 1  Most conforming readers disregard it.
            *
            *              ViewClip                    name                    (Optional; PDF 1.4) The name of the page boundary to which the contents of a page shall be clipped when viewing the document on the screen. 
            *
            *                                                                  The value is the key designating the relevant page boundary in the page object (see 7.7.3, “Page Tree” and 14.11.2, “Page Boundaries”). 
            *
            *                                                                  If the specified page boundary is not defined in the page object, its default value shall be used, as specified in Table 30. 
            *
            *                                                                  Default value: CropBox.
            *
            *                                                                  This entry is intended primarily for use by prepress applications that interpret or manipulate the page boundaries as described in 14.11.2, “Page Boundaries.”
            *
            *                                                                  NOTE 2  Most conforming readers disregard it.
            *
            *              PrintArea                   name                    (Optional; PDF 1.4) The name of the page boundary representing the area of a page that shall be rendered when printing the document. 
            *
            *                                                                  The value is the key designating the relevant page boundary in the page object (see 7.7.3, “Page Tree” and 14.11.2, “Page Boundaries”). 
            *
            *                                                                  If the specified page boundary is not defined in the page object, its default value shall be used, as specified in Table 30. 
            *
            *                                                                  Default value: CropBox.
            *
            *                                                                  This entry is intended primarily for use by prepress applications that interpret or manipulate the page boundaries as described in 14.11.2, “Page Boundaries.”
            *
            *                                                                  NOTE 3  Most conforming readers disregard it.
            *
            *              PrintClip                   name                    (Optional; PDF 1.4) The name of the page boundary to which the contents of a page shall be clipped when printing the document. 
            *
            *                                                                  The value is the key designating the relevant page boundary in the page object (see 7.7.3, “Page Tree” and 14.11.2, “Page Boundaries”). 
            *
            *                                                                  If the specified page boundary is not defined in the page object, its default value shall be used, as specified in Table 30. 
            *
            *                                                                  Default value: CropBox.
            *
            *                                                                  This entry is intended primarily for use by prepress applications that interpret or manipulate the page boundaries as described in 14.11.2, “Page Boundaries.”
            *
            *                                                                  NOTE 4  Most conforming readers disregard it.
            *
            *              PrintScaling                name                    (Optional; PDF 1.6) The page scaling option that shall be selected when a print dialog is displayed for this document. 
            *      
            *                                                                  Valid values are None, which indicates no page scaling, and AppDefault, which indicates the conforming reader’s default print scaling. 
            *
            *                                                                  If this entry has an unrecognized value, AppDefault shall be used. 
            *
            *                                                                  Default value: AppDefault.
            *
            *                                                                  If the print dialog is suppressed and its parameters are provided from some other source, this entry nevertheless shall be honored.
            *
            *              Duplex                      name                    (Optional; PDF 1.7) The paper handling option that shall be used when printing the file from the print dialog. 
            *
            *                                                                  The following values are valid:
            *
            *                                                                  Simplex Print single-sided
            *
            *                                                                  DuplexFlipShortEdge Duplex and flip on the short edge of the sheet
            *
            *                                                                  DuplexFlipLongEdge Duplex and flip on the long edge of the sheet
            *
            *                                                                  Default value: none
            *
            *              PickTrayByPDFSize           boolean                 (Optional; PDF 1.7) A flag specifying whether the PDF page size shall be used to select the input paper tray. 
            *
            *                                                                  This setting influences only the preset values used to populate the print dialog presented by a conforming reader. 
            *
            *                                                                  If PickTrayByPDFSize is true, the check box in the print dialog associated with input paper tray shall bechecked.
            *
            *                                                                  This setting has no effect on operating systems that do not provide the ability to pick the input tray by size.
            *
            *                                                                  Default value: as defined by the conforming reader
            *
            *              PrintPageRange              array                   (Optional; PDF 1.7) The page numbers used to initialize the print dialog box when the file is printed. 
            *
            *                                                                  The array shall contain an even number of integers to be interpreted in pairs, with each pair specifying the first and last pages in a sub-range of pages to be printed.
            *
            *                                                                  The first page of the PDF file shall be denoted by 1. 
            *
            *                                                                  Default value: as defined by the conforming reader
            *
            *              NumCopies                   integer                 (Optional; PDF 1.7) The number of copies that shall be printed when the print dialog is opened for this file. 
            *
            *                                                                  Values outside this range shall be ignored.
            *
            *                                                                  Default value: as defined by the conforming reader, but typically 1
            */

      
        }

        //12.3 Document-Level Navigation
        public class Document_Level_Navigation
        {
            /*12.3.1 General
            *
            *The features described in this sub-clause allow a conforming reader to present the user with an interactive, global overview of a document in either of two forms:
            *
            *  •   As a hierarchical outline showing the document’s internal structure
            *
            *  •   As a collection of thumbnail images representing the pages of the document in miniature form
            *
            *Each item in the outline or each thumbnail image may be associated with a corresponding destination in the document, so that the user can jump directly to the destination by clicking with the mouse.
            */
            
            /*12.3.2 Destinations
            */

                /*12.3.2.1 General
                *
                *A destination defines a particular view of a document, consisting of the following items:
                *
                *  •   The page of the document that shall be displayed
                *
                *  •   The location of the document window on that page
                *
                *  •   The magnification(zoom) factor
                *
                *Destinations may be associated with outline items(see 12.3.3, “Document Outline”), annotations(12.5.6.5, “Link Annotations”), or actions(12.6.4.2, “Go - To Actions” and 12.6.4.3, “Remote Go-To Actions”). 
                *In each case, the destination specifies the view of the document that shall be presented when the outline item or annotation is opened or the action is performed.
                *In addition, the optional OpenAction entry in a document’s catalogue(7.7.2, “Document Catalog”) may specify a destination that shall be displayed when the document is opened.
                *A destination may be specified either explicitly by an array of parameters defining its properties or indirectly by name.
                */

                /*12.3.2.2 Explicit Destinations
                *
                *Table 151 shows the allowed syntactic forms for specifying a destination explicitly in a PDF file.
                *In each case, page is an indirect reference to a page object(except in a remote go - to action; see 12.6.4.3, “Remote Go-To Actions”). 
                *All coordinate values(left, right, top, and bottom) shall be expressed in the default user space coordinate system. 
                *The page’s bounding box is the smallest rectangle enclosing all of its contents. (If any side of the bounding box lies outside the page’s crop box, the corresponding side of the crop box shall be used instead; see 14.11.2, “Page Boundaries,” for further discussion of the crop box.)
                *
                *No page object can be specified for a destination associated with a remote go - to action(see 12.6.4.3, “Remote Go - To Actions”) because the destination page is in a different PDF document.
                *In this case, the page parameter specifies an integer page number within the remote document instead of a page object in the current document.
                *
                *Table 151 - Destination Syntax
                *
                *          [Syntax]                                [Meaning]
                *
                *          [page /XYZ left top zoom]               Display the page designated by page, with the coordinates (left, top) positioned at the upper-left corner of the window and the contents of the page magnified by the factor zoom. 
                *                                                  A null value for any of the parameters left, top, or zoom specifies that the current value of that parameter shall be retained unchanged. 
                *                                                  A zoom value of 0 has the same meaning as a null value.
                *
                *          [page /Fit]                             Display the page designated by page, with its contents magnified just enough to fit the entire page within the window both horizontally and vertically. 
                *                                                  If the required horizontal and vertical magnification factors are different, use the smaller of the two, centering the page within the window in the other dimension.
                *
                *          [page /FitH top]                        Display the page designated by page, with the vertical coordinate toppositioned at the top edge of the window and the contents of the page magnified just enough to fit the entire width of the page within the window. 
                *                                                  A null value for top specifies that the current value of that parameter shall be retained unchanged.
                *
                *          [page /FitV left]                       Display the page designated by page, with the horizontal coordinate left positioned at the left edge of the window and the contents of the page magnified just enough to fit the entire height of the page within the window. 
                *                                                  A null value for left specifies that the current value of that parameter shall be retained unchanged.
                *
                *          [page /FitR left bottom right top]      Display the page designated by page, with its contents magnified just enough to fit the rectangle specified by the coordinates left, bottom, right, and top entirely within the window both horizontally and vertically. 
                *                                                  If the required horizontal and vertical magnification factors are different, use the smaller of the two, centering the rectangle within the window in the other dimension.
                *
                *          [page /FitBH top]                       (PDF 1.1) Display the page designated by page, with the vertical coordinate top positioned at the top edge of the window and the contents of the page magnified just enough to fit the entire width of its bounding box within the window. 
                *                                                  A null value for top specifies that the current value of that parameter shall be retained unchanged.
                *
                *          [page /FitBV left]                      (PDF 1.1) Display the page designated by page, with the horizontal coordinate left positioned at the left edge of the window and the contents of the page magnified just enough to fit the entire height of its bounding box within the window. 
                *                                                  A null value for left specifies that the current value of that parameter shall be retained unchanged.
                */

                /*12.3.2.3 Named Destinations
                *
                *Instead of being defined directly with the explicit syntax shown in Table 151, a destination may be referred to indirectly by means of a name object (PDF 1.1) or a byte string (PDF 1.2). 
                *This capability is especially useful when the destination is located in another PDF document.
                *
                *NOTE 1        A link to the beginning of Chapter 6 in another document might refer to the destination by a name, such as Chap6.begin, instead of by an explicit page number in the other document.
                *              Then, the location of the chapter in the other document could change without invalidating the link.
                *              If an annotation or outline item that refers to a named destination has an associated action, such as a remote go-to action (see 12.6.4.3, “Remote Go-To Actions”) or a thread action(12.6.4.6, “Thread Actions”), the destination is in the file specified by the action’s Fentry, if any; if there is no F entry, the destination is in the current file.
                *
                *In PDF 1.1, the correspondence between name objects and destinations shall be defined by the Dests entry in the document catalogue(see 7.7.2, “Document Catalog”). 
                *The value of this entry shall be a dictionary in which each key is a destination name and the corresponding value is either an array defining the destination, using the syntax shown in Table 151, or a dictionary with a D entry whose value is such an array.
                *
                *NOTE 2        The latter form allows additional attributes to be associated with the destination, as well as enabling a go-to action(see 12.6.4.2, “Go-To Actions”) that shall be used as the target of a named destination.
                *
                *In PDF 1.2 and later, the correspondence between strings and destinations may alternatively be defined by the Dests entry in the document’s name dictionary (see 7.7.4, “Name Dictionary”). 
                *The value of this entry shall be a name tree (7.9.6, “Name Trees”) mapping name strings to destinations. 
                *(The keys in the name tree may be treated as text strings for display purposes.) 
                *The destination value associated with a key in the name tree may be either an array or a dictionary, as described in the preceding paragraph.
                *
                *NOTE 3        The use of strings as destination names is a PDF 1.2 feature. 
                *              If compatibility with earlier versions of PDF is required, only name objects may be used to refer to named destinations. 
                *              A document that supports PDF 1.2 can contain both types. However, if backward compatibility is not a consideration, applications should use the string form of representation in the Dests name tree.
                */
            
            /*12.3.3 Document Outline
            *
                *A PDF document may contain a document outline that the conforming reader may display on the screen, allowing the user to navigate interactively from one part of the document to another. 
                *The outline consists of a tree-structured hierarchy of outline items (sometimes called bookmarks), which serve as a visual table of contents to display the document’s structure to the user. 
                *The user may interactively open and close individual items by clicking them with the mouse. When an item is open, its immediate children in the hierarchy shall become visible on the screen; each child may in turn be open or closed, selectively revealing or hiding further parts of the hierarchy. 
                *When an item is closed, all of its descendants in the hierarchy shall be hidden. Clicking the text of any visible item activates the item, causing the conforming reader to jump to a destination or trigger an action associated with the item.
                *
                *The root of a document’s outline hierarchy is an outline dictionary specified by the Outlines entry in the document catalogue(see 7.7.2, “Document Catalog”).
                *Table 152 shows the contents of this dictionary.Each individual outline item within the hierarchy shall be defined by an outline item dictionary(Table 153).
                *The items at each level of the hierarchy form a linked list, chained together through their Prev and Next entries and accessed through the First and Last entries in the parent item(or in the outline dictionary in the case of top level items). 
                *When displayed on the screen, the items at a given level shall appear in the order in which they occur in the linked list.
                *
                *Table 152 - Entries in the outline dictionary
                *
                *              [Key]                   [Type]                  [Value]
                *
                *              Type                    name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Outlines for an outline dictionary.
                *
                *              First                   dictionary              (Required if there are any open or closed outline entries; shall be an indirect reference) An outline item dictionary representing the first top-level item in the outline.
                *
                *              Last                    dictionary              (Required if there are any open or closed outline entries; shall be an indirect reference) An outline item dictionary representing the last top-level item in the outline.
                *
                *              Count                   integer                 (Required if the document has any open outline entries) Total number of visible outline items at all levels of the outline. 
                *                                                              The value cannot be negative.
                *                                                              This entry shall be omitted if there are no open outline items.
                *
                *Table 153 - Entries in an outline intem dictionary
                *
                *              [Key]                   [Type]                  [Value]
                *
                *              Title                   text string             (Required) The text that shall be displayed on the screen for this item.
                *
                *              Parent                  dictionary              (Required; shall be an indirect reference) The parent of this item in the outline hierarchy. The parent of a top-level item shall be the outline dictionary itself.
                *
                *              Prev                    dictionary              (Required for all but the first item at each level; shall be an indirect reference) The previous item at this outline level.
                *
                *              Next                    dictionary              (Required for all but the last item at each level; shall be an indirect reference) The next item at this outline level.
                *
                *              First                   dictionary              (Required if the item has any descendants; shall be an indirect reference) The first of this item’s immediate children in the outline hierarchy.
                *
                *              Last                    dictionary              (Required if the item has any descendants; shall be an indirect reference) The last of this item’s immediate children in the outline hierarchy.
                *
                *              Count                   integer                 (Required if the item has any descendants) If the outline item is open, Count is the sum of the number of visible descendent outline items at all levels. 
                *                                                              
                *                                                              The number of visible descendent outline items shall bedetermined by the following recursive process:
                *                                                              
                *                                                              Step 1.     Initialize Count to zero.
                *
                *                                                              Step 2.     Add to Count the number of immediate children.
                *                                                                          During repetitions of this step, update only the Count of the original outline item.
                *
                *                                                              Step 3.     For each of those immediate children whose Count is positive and non - zero, repeat steps 2 and 3.
                *
                *                                                              If the outline item is closed, Count is negative and its absolute value is the number of descendants that would be visible if the outline item were opened.
                *
                *              Dest                    name,                   (Optional; shall not be present if an A entry is present) The destination that shall be displayed when this item is activated (see 12.3.2, “Destinations”).
                *                                      byte string, or
                *                                      array
                *
                *              A                       dictionary              (Optional; PDF 1.1; shall not be present if a Dest entry is present) The action that shall be performed when this item is activated (see 12.6, “Actions”).
                *
                *              SE                      dictionary              (Optional; PDF 1.3; shall be an indirect reference) The structure element to which the item refers (see 14.7.2, “Structure Hierarchy”).
                *
                *                                                              (PDF 1.0) An item may also specify a destination (Dest) corresponding to an area of a page where the contents of the designated structure element are displayed.
                *
                *              C                       array                   (Optional; PDF 1.4) An array of three numbers in the range 0.0 to 1.0, representing the components in the DeviceRGB colour space of the colour that shall be used for the outline entry’s text. 
                *                                                              Default value: [0.0 0.0 0.0].
                *
                *              F                       integer                 (Optional; PDF 1.4) A set of flags specifying style characteristics for displaying the outline item’s text (see Table 154). 
                *                                                              Default value: 0.
                *
                *
                *The value of the outline item dictionary’s F entry (PDF 1.4) shall be an integer interpreted as one-bit flags specifying style characteristics for displaying the item. 
                *Bit positions within the flag word are numbered from low-order to high-order bits, with the lowest-order bit numbered 1. 
                *Table 154 shows the meanings of the flags; all other bits of the integer shall be 0.
                *
                *Table 154 - Outline item flags
                *
                *          [Bit position]              [Name]              [Meaning]
                *
                *          1                           Italic              If set to 1, display the item in italic.
                *
                *          2                           Bold                If set to 1, display the item in bold.
                *
                *
                *EXAMPLE       The following example shows a typical outline dictionary and outline item dictionary. 
                *              See H.6, “Outline Hierarchy Example” for an example of a complete outline hierarchy.
                *
                *              21 0 obj
                *                  << / Count 6
                *                     / First 22 0 R
                *                     / Last 29 0 R
                *                  >>
                *              endobj
                *              22 0 obj
                *                  << / Title(Chapter 1)
                *                     / Parent 21 0 R
                *                     / Next 26 0 R
                *                     / First 23 0 R
                *                     / Last 25 0 R
                *                     / Count 3
                *                     / Dest[3 0 R / XYZ 0 792 0]
                *                  >>
                *              endobj
                */

            /*12.3.4 Thumbnail Images
            *
                *A PDF document may contain thumbnail images representing the contents of its pages in miniature form. 
                *A conforming reader may display these images on the screen, allowing the user to navigate to a page by clicking its thumbnail image:
                *
                *NOTE      Thumbnail images are not required, and may be included for some pages and not for others.
                *
                *The thumbnail image for a page shall be an image XObject specified by the Thumb entry in the page object(see 7.7.3, “Page Tree”).
                *It has the usual structure for an image dictionary(8.9.5, “Image Dictionaries”), but only the Width, Height, ColorSpace, BitsPerComponent, and Decode entries are significant; all of the other entries listed in Table 89 shall be ignored if present. 
                *(If a Subtype entry is specified, its value shall be Image.) The image’s colour space shall be either DeviceGray or DeviceRGB, or an Indexed space based on one of these.
                *
                *EXAMPLE       This example shows a typical thumbnail image definition.
                *
                *              12 0 obj
                *                  << / Width 76
                *                     / Height 99
                *                     / ColorSpace / DeviceRGB
                *                     / BitsPerComponent 8
                *                     / Length 13 0 R
                *                     / Filter[/ ASCII85Decode / DCTDecode]
                *                  >>
                *              stream
                *              s4IA > !"M;*Ddm8XA,lT0!!3,S!/(=R!<E3%!<N<(!WrK*!WrN,
                *              …Omitted data…
                *              endstream
                *              endobj
                *
                *              13 0 obj                                                    % Length of stream
                *              …
                *              endobj
                */
           
            /*12.3.5 Collections
            *
                *Beginning with PDF 1.7, PDF documents may specify how a conforming reader’s user interface presents collections of file attachments, where the attachments are related in structure or content. 
                *Such a presentation is called a portable collection.
                *
                *NOTE 1        The intent of portable collections is to present, sort, and search collections of related documents embedded in the containing PDF document, such as email archives, photo collections, and engineering bid sets.
                *              There is no requirement that documents in a collection have an implicit relationship or even a similarity; however, showing differentiating characteristics of related documents can be helpful for document navigation.
                *
                *A collection dictionary specifies the viewing and organizational characteristics of portable collections. 
                *If this dictionary is present in a PDF document, the conforming reader shall present the document as a portable collection.
                *The EmbeddedFiles name tree specifies file attachments (see 7.11.4, “Embedded File Streams”).
                *
                *When a conforming reader first opens a PDF document containing a collection, it shall display the contents of the initial document, along with a list of the documents present in the EmbeddedFiles name tree.
                *The document list shall include the additional document information specified by the collection schema.
                *The initial document may be the container PDF or one of the embedded documents.
                *
                *NOTE 2        The page content in the initial document should contain information that helps the user understand what is contained in the collection, such as a title and an introductory paragraph.
                *
                *The file attachments comprising a collection shall be located in the EmbeddedFiles name tree. 
                *All attachments in that tree are in the collection; any attachments not in that tree are not.
                *
                *Table 155 describes the entries in a collection dictionary.
                *
                *          [Key]                       [Type]                      [Value]
                *
                *          Type                        name                        (Optional) The type of PDF object that this dictionary describes; if present, shall be Collection for a collection dictionary.
                *
                *          Schema                      dictionary                  (Optional) A collection schema dictionary (see Table 156). 
                *                                                                  If absent, the conforming reader may choose useful defaults that are known to exist in a file specification dictionary, such as the file name, file size, and modified date.
                *
                *          D                           byte string                 (Optional) A string that identifies an entry in the EmbeddedFiles name tree, determining the document that shall be initially presented in the user interface. 
                *                                                                  If the D entry is missing or in error, the initial document shall be the one that contains the collection dictionary.
                *
                *          View                        name                        (Optional) The initial view. The following values are valid:
                *
                *                                                                  D   The collection view shall be presented in details mode, with all information in the Schema dictionary presented in a multi-column format.
                *                                                                      This mode provides the most information to the user.
                *
                *                                                                  T   The collection view shall be presented in tile mode, with each file in the collection denoted by a small icon and a subset of information from the Schema dictionary.
                *                                                                      This mode provides top - level information about the file attachments to the user.
                *
                *                                                                  H   The collection view shall be initially hidden, The conforming reader shall provide means for the user to view the collection by some explicit action.
                *                                                                      Default value: D
                *
                *          Sort                        dictionary                  (Optional) A collection sort dictionary, which specifies the order in which items in the collection shall be sorted in the user interface (see Table 158).
                *
                *
                *A collection schema dictionary consists of a variable number of individual collection field dictionaries. 
                *Each collection field dictionary has a key chosen by the conforming writer, which shall be used to associate a field with data in a file specification. 
                *Table 156 describes the entries in a collection schema dictionary.
                *
                *Table 156 - Entries in a collection schema dictionary
                *
                *          [Key]                       [Type]                      [Value]
                *
                *          Type                        name                        (Optional) The type of PDF object that this dictionary describes; if present, shall be CollectionSchema for a collection schema dictionary.
                *
                *          Other keys                  dictionary                  (Optional) A collection field dictionary. Each key name is chosen at the discretion of the conforming writer. 
                *                                                                  The key name shall be used to identify a corresponding collection item dictionary referenced from the file specification dictionary's CI entry (see CI key in Table 44).
                *
                *
                *A collection field dictionary describes the attributes of a particular field in a portable collection, including the type of data stored in the field and the lookup key used to locate the field data in the file specification dictionary. 
                *Table 157 describes the entries in a collection field dictionary.
                *
                *Table 157 - Entries in a collection field dictionary
                *
                *          [Key]                       [Type]                      [Value]
                *
                *          Type                        name                        (Optional) The type of PDF object that this dictionary describes; if present, shall be CollectionField for a collection field dictionary.
                *
                *          Subtype                     name                        (Required) The subtype of collection field or file-related field that this dictionary describes. 
                *                                                                  This entry identifies the type of data that shall be stored in the field.
                *
                *                                                                  The following values identify the types of fields in the collection item or collection subitem dictionary:
                *
                *                                                                  S                   A text field.The field data shall be stored as a PDF text string.
                *
                *                                                                  D                   A date field. The field data shall be stored as a PDF date string.
                *
                *                                                                  N                   A number field. The field data shall be stored as a PDF number.
                *
                *                                                                  The following values identify the types of file-related fields:
                *
                *                                                                  F                   The field data shall be the file name of the embedded file stream, as identified by the UF entry of the file specification, if present; otherwise by the Fentry of the file specification(see Table 44).
                *
                *                                                                  Desc                The field data shall be the description of the embedded file stream, as identified by the Descentry in the file specification dictionary(see Table 44).
                *
                *                                                                  ModDate             The field data shall be the modification date of the embedded file stream, as identified by the ModDateentry in the embedded file parameter dictionary(see Table 46).
                *
                *                                                                  CreationDate        The field data shall be the creation date of the embedded file stream, as identified by the CreationDate entry in the embedded file parameter dictionary (see Table 46).
                *
                *                                                                  Size                The field data shall be the size of the embedded file, as identified by the Size entry in the embedded file parameter dictionary(see Table 46).
                *
                *            N                          text string                (Required) The textual field name that shall be presented to the user by the conforming reader.
                *
                *            O                          integer                    (Optional) The relative order of the field name in the user interface. Fields shall be sorted by the conforming reader in ascending order.
                *
                *            V                          boolean                    (Optional) The initial visibility of the field in the user interface. 
                *                                                                  Default value: true.
                *
                *            E                          boolean                    (Optional) A flag indicating whether the conforming reader should provide support for editing the field value. 
                *                                                                  Default value: false.
                *
                *
                *A collection sort dictionary identifies the fields that shall be used to sort items in the collection. The type of sorting depends on the type of data:
                *
                *  •   Text strings shall be ordered lexically from smaller to larger, if ascending order is specified.
                *
                *NOTE 3    Lexical ordering is an implementation dependency for conforming readers.
                *
                *  •   Numbers shall be ordered numerically from smaller to larger, if ascending order is specified.
                *
                *  •   Dates shall be ordered from oldest to newest, if ascending order is specified.
                *
                *Table 158 describes the entries in a collection sort dictionary.
                *
                *Table 158 - Entries in a collection sort dictionary
                *
                *              [Key]                   [Type]                      [Value]
                *
                *              Type                    name                        (Optional) The type of PDF object that this dictionary describes; if present, shall be CollectionSort for a collection sort dictionary.
                *
                *              S                       name or array               (Required) The name or names of fields that the conforming reader shall use to sort the items in the collection. 
                *                                                                  If the value is a name, it identifies a field described in the parent collection dictionary.
                *
                *                                                                  If the value is an array, each element of the array shall be a name that identifies a field described in the parent collection dictionary. 
                *                                                                  The array form shall be used to allow additional fields to contribute to the sort, where each additional field shall be used to break ties.
                *                                                                  More specifically, if multiple collection item dictionaries have the same value for the first field named in the array, the values for successive fields named in the array shall be used for sorting, until a unique order is determined or until the named fields are exhausted.
                *
                *              A                       boolean or array            (Optional) If the value is a boolean, it specifies whether the conforming reader shall sort the items in the collection in ascending order (true) or descending order (false). 
                *                                                                  If the value is an array, each element of the array shall be a boolean value that specifies whether the entry at the same index in the S array shall be sorted in ascending or descending order.
                *
                *                                                                  If the number of entries in the A array is larger than the number of entries in the S array the extra entries in the A array shall be ignored. 
                *                                                                  If the number of entries in the A array is less than the number of entries in the S array the missing entries in the A array shall be assumed to be true.
                *                                                                  Default value: true.
                *
                *EXAMPLE 1     This example shows a collection dictionary representing an email in-box, where each item in the collection is an email message. 
                *              The actual email messages are contained in file specification dictionaries. 
                *              The organizational data associated with each email is described in a collection schema dictionary. 
                *              Most actual organizational data (from, to, date, and subject) is provided in a collection item dictionary, but the size data comes from the embedded file parameter dictionary.
                *
                *              / Collection <<
                *                  / Type / Collection
                *                  / Schema <<
                *                      / Type / CollectionSchema
                *                      / from << / Subtype / S / N(From) / O 1 / V true / E false >>
                *                      / to << / Subtype / S / N(To) / O 2 / V true / E false >>
                *                      / date << / Subtype / D / N(Date received) / O 3 / V true / E false >>
                *                      / subject << / Subtype / S / N(Subject) / O 4 / V true / E false >>
                *                      / size << / Subtype / Size / N(Size) / O 5 / V true / E false >>
                *                      >>
                *                  / D(Doc1)
                *                  / View / D
                *                  / Sort << / S / date / A false >>
                *              >>
                *
                *EXAMPLE 2     This example shows a collection item dictionary and a collection subitem dictionary. 
                *              These dictionaries contain entries that correspond to the schema entries specified in the Example in 12.4.2, “Page Labels.”. 7.11.6, “Collection Items” specifies the collection item and collection subitem dictionaries.
                *
                *              / CI <<
                *                  / Type / CollectionItem
                *                  / from(Rob McAfee)
                *                  / to(Patty McAfee)
                *                  / subject <<
                *                      / Type / CollectionSubitem
                *                      / P(Re:)
                *                      / D(Let's have lunch on Friday!)
                *                  >>
                *                  / date(D: 20050621094703 - 07’00’)
                *              >>
                */
           
        }

        //12.4 Page-Level Navigation
        public class Page_Level_Navigation
        {

            /*12.4.1 General
            *
            *This sub-clause describes PDF facilities that enable the user to navigate from page to page within a document:
            *
            *  •   Page labels for numbering or otherwise identifying individual pages(see 12.4.2, “Page Labels”).
            *
            *  •   Article threads, which chain together items of content within the document that are logically connected but not physically sequential(see 12.4.3, “Articles”).
            *
            *  •   Presentations that display the document in the form of a slide show, advancing from one page to the next either automatically or under user control(see 12.4.4, “Presentations”).
            *
            *For another important form of page-level navigation, see 12.5.6.5, “Link Annotations.”
            */

            /*12.4.2 Page Labels
            *
                *Each page in a PDF document shall be identified by an integer page index that expresses the page’s relative position within the document. 
                *In addition, a document may optionally define page labels (PDF 1.3) to identify each page visually on the screen or in print. 
                *Page labels and page indices need not coincide: the indices shall be fixed, running consecutively through the document starting from 0 for the first page, but the labels may be specified in any way that is appropriate for the particular document.
                *
                *NOTE 1        If the document begins with 12 pages of front matter numbered in roman numerals and the remainder of the document is numbered in arabic, the first page would have a page index of 0 and a page label of i, the twelfth page would have index 11 and label xii, and the thirteenth page would have index 12 and label 1.
                *
                *For purposes of page labelling, a document shall be divided into labelling ranges, each of which is a series of consecutive pages using the same numbering system. 
                *Pages within a range shall be numbered sequentially in ascending order. 
                *A page’s label consists of a numeric portion based on its position within its labelling range, optionally preceded by a label prefix denoting the range itself.
                *
                *NOTE 2        The pages in an appendix might be labeled with decimal numeric portions prefixed with the string A-; the resulting page labels would be A - 1, A - 2, and so on.
                *
                *A document’s labelling ranges shall be defined by the PageLabels entry in the document catalogue(see 7.7.2, “Document Catalog”).
                *The value of this entry shall be a number tree(7.9.7, “Number Trees”), each of whose keys is the page index of the first page in a labelling range.
                *The corresponding value shall be a page label dictionary defining the labelling characteristics for the pages in that range.
                *The tree shall include a value for page index 0.Table 159 shows the contents of a page label dictionary.
                *
                *Table 159 - Entries in a page label dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be PageLabel for a page label dictionary.
                *
                *          S                   name                (Optional) The numbering style that shall be used for the numeric portion of each page label:
                *
                *                                                  D       Decimal arabic numerals
                *
                *                                                  R       Uppercase roman numerals
                *
                *                                                  r       Lowercase roman numerals
                *
                *                                                  A       Uppercase letters(A to Z for the first 26 pages, AA to ZZ for the next 26, and so on)
                *                  
                *                                                  a       Lowercase letters(a to z for the first 26 pages, aa to zz for the next 26, and so on)
                *          
                *                                                  There is no default numbering style; if no S entry is present, page labels shall consist solely of a label prefix with no numeric portion.
                *
                *                                                  NOTE    If the P entry(next) specifies the label prefix Contents, each page is simply labeled Contents with no page number. 
                *                                                          (If the P entry is also missing or empty, the page label is an empty string.)
                *
                *          P                   text string         (Optional) The label prefix for page labels in this range.
                *
                *          St                  integer             (Optional) The value of the numeric portion for the first page label in the range. Subsequent pages shall be numbered sequentially from this value, which shall be greater than or equal to 1. 
                *                                                  Default value: 1.
                *
                *EXAMPLE       The following example shows a document with pages labeled
                *
                *              i, ii, iii, iv, 1, 2, 3, A - 8, A - 9, …
                *              
                *              1 0 obj
                *                  << / Type / Catalog
                *                     / PageLabels << / Nums[  0 << / S / r >>               % A number tree containing
                *                                              4 << / S / D >>               % three page label dictionaries
                *                                              7 << / S / D
                *                                                   / P(A -)
                *                                                   / St 8
                *                                                >>
                *                                           ]
                *                                  >>
                *                      …
                *                  >>
                *              endobj
                *
                */

            /*12.4.3 Articles
            *
                *Some types of documents may contain sequences of content items that are logically connected but not physically sequential.
                *
                *EXAMPLE 1     A news story may begin on the first page of a newsletter and run over onto one or more nonconsecutive interior pages.
                *
                *To represent such sequences of physically discontiguous but logically related items, a PDF document may define one or more articles(PDF 1.1).
                *The sequential flow of an article shall be defined by an article thread; the individual content items that make up the article are called beads on the thread.
                *Conforming readers may provide navigation facilities to allow the user to follow a thread from one bead to the next.
                *
                *The optional Threads entry in the document catalogue(see 7.7.2, “Document Catalog”) holds an array of thread dictionaries(Table 160) defining the document’s articles. 
                *Each individual bead within a thread shall be represented by a bead dictionary (Table 161). 
                *The thread dictionary’s F entry shall refer to the first bead in the thread; the beads shall be chained together sequentially in a doubly linked list through their N (next) and V(previous) entries. 
                *In addition, for each page on which article beads appear, the page object (see 7.7.3, “Page Tree”) shall contain a B entry whose value is an array of indirect references to the beads on the page, in drawing order.
                *
                *Table 160 - Entries in a thread dictionary
                *
                *              [Key]                   [Type]                  [Value]
                *
                *              Type                    name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Thread for a thread dictionary.
                *
                *              F                       dictionary              (Required; shall be an indirect reference) The first bead in the thread.
                *
                *              I                       dictionary              (Optional) A thread information dictionary containing information about the thread, such as its title, author, and creation date. 
                *                                                              The contents of this dictionary shall conform to the syntax for the document information dictionary (see 14.3.3, “Document Information Dictionary”).
                *
                *Table 161 - Entries in a bead dictionary
                *
                *              [Key]                   [Type]                  [Value]
                *
                *              Type                    name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Bead for a bead dictionary.
                *
                *              T                       dictionary              (Required for the first bead of a thread; optional for all others; shall be an indirect reference) The thread to which this bead belongs.
                *                                                              (PDF 1.1) This entry shall be permitted only for the first bead of a thread.
                *                                                              (PDF 1.2) It shall be permitted for any bead but required only for the first.
                *
                *              N                       dictionary              (Required; shall be an indirect reference) The next bead in the thread. In the last bead, this entry shall refer to the first bead.
                *
                *              V                       dictionary              (Required; shall be an indirect reference) The previous bead in the thread. In the first bead, this entry shall refer to the last bead.
                *
                *              P                       dictionary              (Required; shall be an indirect reference) The page object representing the page on which this bead appears.
                *
                *              R                       rectangle               (Required) A rectangle specifying the location of this bead on the page.
                *
                *
                *EXAMPLE 2     The following example shows a thread with three beads.
                *
                *              22 0 obj
                *                  << / F 23 0 R
                *                     / I << / Title(Man Bites Dog) >>
                *                  >>
                *              endobj
                *
                *              23 0 obj
                *                  << / T 22 0 R
                *                     / N 24 0 R
                *                     / V 25 0 R
                *                     / P 8 0 R
                *                     / R[158 247 318 905]
                *                  >>
                *              endobj
                *
                *              24 0 obj
                *                  << / T 22 0 R
                *                     / N 25 0 R
                *                     / V 23 0 R
                *                     / P 8 0 R
                *                     / R[322 246 486 904]
                *                  >>
                *              endobj
                *
                *              25 0 obj
                *                  << / T 22 0 R
                *                     / N 23 0 R
                *                     / V 24 0 R
                *                     / P 10 0 R
                *                     / R[157 254 319 903]
                *                  >>
                *              endobj
                */

            /*12.4.4 Presentations
            */
                
                /*12.4.4.1 General
                *
                *Some conforming readers may allow a document to be displayed in the form of a presentation or slide show, advancing from one page to the next either automatically or under user control. 
                *In addition, PDF 1.5 introduces the ability to advance between different states of the same page(see 12.4.4.2, “Sub - page Navigation”).
                *
                *NOTE 1        PDF 1.4 introduces a different mechanism, known as alternate presentations, for slide show displays, described in 13.5, “Alternate Presentations.”
                *
                *A page object(see 7.7.3, “Page Tree”) may contain two optional entries, Dur and Trans(PDF 1.1), to specify how to display that page in presentation mode.
                *The Trans entry shall contain a transition dictionary describing the style and duration of the visual transition to use when moving from another page to the given page during a presentation.
                *Table 162 shows the contents of the transition dictionary. (Some of the entries shown are needed only for certain transition styles, as indicated in the table.)
                *
                *The Dur entry in the page object specifies the page’s display duration(also called its advance timing): the maximum length of time, in seconds, that the page shall be displayed before the presentation automatically advances to the next page.
                *
                *NOTE 2        The user can advance the page manually before the specified time has expired.
                *
                *If no Dur entry is specified in the page object, the page shall not advance automatically.
                *
                *Table 162 - Entries in a transition dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shallbe Trans for a transition dictionary.
                *
                *          S                   name                    (Optional) The transition style that shall be used when moving to this page from another during a presentation. 
                *                                                      Default value: R.
                *
                *                                                      Split       Two lines sweep across the screen, revealing the new page.
                *                                                                  The lines may be either horizontal or vertical and may move inward from the edges of the page or outward from the center, as specified by the Dmand M entries, respectively.
                *
                *                                                      Blinds      Multiple lines, evenly spaced across the screen, synchronously sweep in the same direction to reveal the new page.
                *                                                                  The lines may be either horizontal or vertical, as specified by the Dm entry. 
                *                                                                  Horizontal lines move downward; vertical lines move to the right.
                *
                *                                                      Box         A rectangular box sweeps inward from the edges of the page or outward from the center, as specified by the M entry, revealing the new page.
                *
                *                                                      Wipe        A single line sweeps across the screen from one edge to the other in the direction specified by the Di entry, revealing the new page.
                *                  
                *                                                      Dissolve    The old page dissolves gradually to reveal the new one.
                *
                *                                                      Glitter     Similar to Dissolve, except that the effect sweeps across the page in a wide band moving from one side of the screen to the other in the direction specified by the Di entry.
                *
                *                                                      R           The new page simply replaces the old one with no special transition effect; the D entry shall be ignored.
                *
                *                                                      Fly         (PDF 1.5) Changes are flown out or in (as specified by M), in the direction specified by Di, to or from a location that is offscreen except when Di is None.
                *
                *                                                      Push        (PDF 1.5) The old page slides off the screen while the new page slides in, pushing the old page out in the direction specified by Di.
                *
                *                                                      Cover       (PDF 1.5) The new page slides on to the screen in the direction specified by Di, covering the old page.
                *
                *                                                      Uncover     (PDF 1.5) The old page slides off the screen in the direction specified by Di, uncovering the new page in the direction specified by Di.
                *
                *                                                      Fade        (PDF 1.5) The new page gradually becomes visible through the old one.
                *
                *          D                   number                  (Optional) The duration of the transition effect, in seconds. Default value: 1.
                *
                *          Dm                  name                    (Optional; Split and Blinds transition styles only) The dimension in which the specified transition effect shall occur:
                *
                *                                                      H       Horizontal
                *
                *                                                      V       Vertical
                *
                *                                                      Default value: H.
                *
                *          M                   name                    (Optional; Split, Box and Fly transition styles only) The direction of motion for the specified transition effect:
                *
                *                                                      I       Inward from the edges of the page
                *
                *                                                      O       Outward from the center of the page
                *
                *                                                      Default value: I.
                *  
                *          Di                  number or               (Optional; Wipe, Glitter, Fly, Cover, Uncover and Push transition styles only)
                *                              name                    The direction in which the specified transition effect shall moves, expressed in degrees counterclockwise starting from a left-to-right direction. 
                *                                                      (This differs from the page object’s Rotate entry, which is measured clockwise from the top.)
                *                                                      If the value is a number, it shall be one of:
                *
                *                                                      0       Left to right
                *      
                *                                                      90      Bottom to top(Wipe only)
                *
                *                                                      180     Right to left(Wipe only)
                *
                *                                                      270     Top to bottom
                *
                *                                                      315     Top - left to bottom-right (Glitter only)
                *
                *                                                      If the value is a name, it shall be None, which is relevant only for the Fly transition when the value of SS is not 1.0.
                *
                *                                                      Default value: 0.
                *              
                *          SS                  number                  (Optional; PDF 1.5; Fly transition style only) The starting or ending scale at which the changes shall be drawn. 
                *                                                      If M specifies an inward transition, the scale of the changes drawn shall progress from SS to 1.0 over the course of the transition. 
                *                                                      If M specifies an outward transition, the scale of the changes drawn shall progress from 1.0 to SS over the course of the transition
                *                                                      Default: 1.0.
                *
                *          B                   boolean                 (Optional; PDF 1.5; Fly transition style only) If true, the area that shall be flown in is rectangular and opaque. 
                *                                                      Default: false.
                *
                *
                *NOTE 3        Figure 56 illustrates the relationship between transition duration (D in the transition dictionary) and display duration (Dur in the page object). 
                *              Note that the transition duration specified for a page (page 2 in the figure) governs the transition to that page from another page; the transition from the page is governed by the next page’s transition duration.
                *
                *(see Figure 56 - Presentation timing, on page 379)
                *
                *EXAMPLE       The following example shows the presentation parameters for a page to be displayed for 5 seconds. 
                *              Before the page is displayed, there is a 3.5-second transition in which two vertical lines sweep outward from the center to the edges of the page.
                *
                *              10 0 obj
                *                  << / Type / Page
                *                     / Parent 4 0 R
                *                     / Contents 16 0 R
                *                     / Dur 5
                *                     / Trans <</ Type / Trans
                *                     / D 3.5
                *                     / S / Split
                *                     / Dm / V
                *                     / M / O
                *                              >>
                *                  >>
                *              endobj
                */

                /*12.4.4.2 Sub-page Navigation
                *
                *Sub - page navigation(PDF 1.5) provides the ability to navigate not only between pages but also between different states of the same page.
                *
                *NOTE 1        A single page in a PDF presentation could have a series of bullet points that could be individually turned on and off. 
                *              In such an example, the bullets would be represented by optional content(see 8.11.2, “Optional Content Groups”), and each state of the page would be represented as a navigation node.
                *
                *NOTE 2        Conforming readers should save the state of optional content groups when a user enters presentation mode and restore it when presentation mode ends.
                *              This ensures, for example, that transient changes to bullets do not affect the printing of the document.
                *
                *A navigation node dictionary(see Table 163) specifies actions to execute when the user makes a navigation request.
                *
                *EXAMPLE       Pressing an arrow key.
                *
                *The navigation nodes on a page form a doubly linked list by means of their Next and Prev entries.
                *The primary node on a page shall be determined by the optional PresSteps entry in a page dictionary(see Table 30).
                *
                *NOTE 3        A conforming reader should respect navigation nodes only when in presentation mode(see 12.4.4, “Presentations”).
                *
                *Table 163 – Entries in a navigation node dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Optional) The type of PDF object that this dictionary describes; shall be NavNode for a navigation node dictionary.
                *
                *          NA                  dictionary              (Optional) An action (which may be the first in a sequence of actions) that shall be executed when a user navigates forward.
                *
                *          PA                  dictionary              (Optional) An action (which may be the first in a sequence of actions) that shall be executed when a user navigates backward.
                *
                *          Next                dictionary              (Optional) The next navigation node, if any.
                *
                *          Prev                dictionary              (Optional) The previous navigation node, if any.
                *
                *          Dur                 number                  (Optional) The maximum number of seconds before the conforming readershall automatically advance forward to the next navigation node. 
                *                                                      If this entry is not specified, no automatic advance shall occur.
                *
                *A conforming reader shall maintain a current navigation node. 
                *When a user navigates to a page, if the page dictionary has a PresSteps entry, the node specified by that entry shall become the current node. 
                *(Otherwise, there is no current node.) If the user requests to navigate forward (such as an arrow key press) and there is a current navigation node, the following shall occur:
                *
                *  a)  The sequence of actions specified by NA(if present) shall be executed.
                *
                *If NA specifies an action that navigates to another page, the following actions for navigating to another page take place, and Next should not be present.
                *
                *  b)  The node specified by Next(if present) shall become the new current navigation node.
                *
                *Similarly, if the user requests to navigate backward and there is a current navigation node, the following shall occur:
                *
                *  a)  The sequence of actions specified by PA(if present) shall be executed.
                *
                *      If PA specifies an action that navigates to another page, the following actions for navigating to another page take place, and Prev should not be present.
                *
                *  b)  The node specified by Prev(if present) shall become the new current navigation node.
                *
                *Transition effects, similar to the page transitions described earlier, may be specified as transition actions that are part of the NA or PA sequence; see 12.6.4.14, “Transition Actions.”
                *
                *If the user requests to navigate to another page(regardless of whether there is a current node) and that page’s dictionary contains a PresSteps entry, the following shall occur:
                *
                *  a)  The navigation node represented by PresSteps shall become the current node.
                *
                *  b)  If the navigation request was forward, or if the navigation request was for random access (such as by clicking on a link), the actions specified by NA shall be executed and the node specified by Next shall become the new current node, as described previously.
                *
                *If the navigation request was backward, the actions specified by PA shall be executed and the node specified by Prev shall become the new current node, as described previously.
                *
                *  c)  The conforming reader shall make the new page the current page and shall display it.
                *      Any page transitions specified by the Trans entry of the page dictionary shall be performed.
                */

        
            
        }

        //12.5 Annotations
        public class Annotations
        {
            /*12.5.1 General
            *
            *An annotation associates an object such as a note, sound, or movie with a location on a page of a PDF document, or provides a way to interact with the user by means of the mouse and keyboard.
            *PDF includes a wide variety of standard annotation types, described in detail in 12.5.6, “Annotation Types.”
            *
            *Many of the standard annotation types may be displayed in either the open or the closed state.
            *When closed, they appear on the page in some distinctive form, such as an icon, a box, or a rubber stamp, depending on the specific annotation type. 
            *When the user activates the annotation by clicking it, it exhibits its associated object, such as by opening a pop-up window displaying a text note (Figure 57) or by playing a sound or a movie.
            *
            *(see Figure 57 - Open annotation, on page 382)
            *
            *Conforming readers may permit the user to navigate through the annotations on a page by using the keyboard (in particular, the tab key). 
            *Beginning with PDF 1.5, PDF producers may make the navigation order explicit with the optional Tabs entry in a page object (see Table 30). 
            *The following are the possible values for this entry:
            *
            *  •   R(row order) : Annotations shall be visited in rows running horizontally across the page.
            *      The direction within a row shall be determined by the Direction entry in the viewer preferences dictionary (see 12.2, “Viewer Preferences”). 
            *      The first annotation that shall be visited is the first annotation in the topmost row.When the end of a row is encountered, the first annotation in the next row shall be visited.
            *
            *  •   C (column order): Annotations shall be visited in columns running vertically up and down the page. 
            *      Columns shall be ordered by the Direction entry in the viewer preferences dictionary (see 12.2, “Viewer Preferences”). 
            *      The first annotation that shall be visited is the one at the top of the first column.When the end of a column is encountered, the first annotation in the next column shall be visited.
            *
            *  •   S (structure order): Annotations shall be visited in the order in which they appear in the structure tree (see 14.7, “Logical Structure”). 
            *      The order for annotations that are not included in the structure tree shall be determined in a manner of the conforming reader's choosing.
            *
            *These descriptions assume the page is being viewed in the orientation specified by the Rotate entry.
            *
            *Conceptually, the behaviour of each annotation type may be implemented by a software module called an annotation handler.
            *A conforming reader shall provide annotation handlers for all of the conforming annotation types.
            *The set of annotation types is extensible.A conforming reader shall provide certain expected behaviour for all annotation types that it does not recognize, as documented in 12.5.2, “Annotation Dictionaries.”
            */
            
            /*12.5.2 Annotation Dictionaries
            *
                *The optional Annots entry in a page object (see 7.7.3, “Page Tree”) holds an array of annotation dictionaries, each representing an annotation associated with the given page. 
                *Table 164 shows the required and optional entries that are common to all annotation dictionaries. 
                *The dictionary may contain additional entries specific to a particular annotation type; see the descriptions of individual annotation types in 12.5.6, “Annotation Types,”for details. 
                *A given annotation dictionary shall be referenced from the Annots array of only one page. 
                *This requirement applies only to the annotation dictionary itself, not to subsidiary objects, which may be shared among multiple annotations.
                *
                *Table 164 - Entries common to all annotation dictionaries
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Annot for an annotation dictionary.
                *
                *          Subtype             name                    (Required) The type of annotation that this dictionary describes; see Table 169 for specific values.
                *
                *          Rect                rectangle               (Required) The annotation rectangle, defining the location of the annotation on the page in default user space units.
                *
                *          Contents            text string             (Optional) Text that shall be displayed for the annotation or, if this type of annotation does not display text, an alternate description of the annotation’s contents in human-readable form. 
                *                                                      In either case, this text is useful when extracting the document’s contents in support of accessibility to users with disabilities or for other purposes (see 14.9.3, “Alternate Descriptions”). 
                *                                                      See 12.5.6, “Annotation Types” for more details on the meaning of this entry for each annotation type.
                *
                *          P                   dictionary              (Optional except as noted below; PDF 1.3; not used in FDF files) An indirect reference to the page object with which this annotation is associated.
                *                                                      This entry shall be present in screen annotations associated with rendition actions(PDF 1.5; see 12.5.6.18, “Screen Annotations” and 12.6.4.13, “Rendition Actions”).
                *
                *          NM                  text string             (Optional; PDF 1.4) The annotation name, a text string uniquely identifying it among all the annotations on its page.
                *
                *          M                   date or text string     (Optional; PDF 1.1) The date and time when the annotation was most recently modified. 
                *                                                      The format should be a date string as described in 7.9.4, “Dates,” but conforming readers shall accept and display a string in any format.
                *
                *          F                   integer                 (Optional; PDF 1.1) A set of flags specifying various characteristics of the annotation (see 12.5.3, “Annotation Flags”). 
                *                                                      Default value: 0.
                *
                *          AP                  dictionary              (Optional; PDF 1.2) An appearance dictionary specifying how the annotation shall be presented visually on the page (see 12.5.5, “Appearance Streams”). 
                *                                                      Individual annotation handlers may ignore this entry and provide their own appearances.
                *
                *          AS                  name                    (Required if the appearance dictionary AP contains one or more subdictionaries; PDF 1.2) 
                *                                                      The annotation’s appearance state, which selects the applicable appearance stream from an appearance subdictionary (see Section 12.5.5, “Appearance Streams”).
                *
                *          Border              array                   (Optional) An array specifying the characteristics of the annotation’s border, which shall be drawn as a rounded rectangle.
                *
                *                                                      (PDF 1.0) The array consists of three numbers defining the horizontal corner radius, vertical corner radius, and border width, all in default user space units.
                *                                                      If the corner radii are 0, the border has square(not rounded) corners; if the border width is 0, no border is drawn.
                *
                *                                                      (PDF 1.1) The array may have a fourth element, an optional dash arraydefining a pattern of dashes and gaps that shall be used in drawing the border.
                *                                                      The dash array shall be specified in the same format as in the line dash pattern parameter of the graphics state(see 8.4.3.6, “Line Dash Pattern”).
                *
                *                                                      EXAMPLE     A Border value of[0 0 1[3 2]] specifies a border 1 unit wide, with square corners, drawn with 3 - unit dashes alternating with 2 - unit gaps.
                *
                *                                                      NOTE        (PDF 1.2) The dictionaries for some annotation types(such as free text and polygon annotations) can include the BSentry.
                *                                                                  That entry specifies a border style dictionary that has more settings than the array specified for the Border entry.
                *                                                                  If an annotation dictionary includes the BS entry, then the Border entry is ignored.
                *
                *                                                      Default value: [0 0 1].
                *
                *          C                   array                   (Optional; PDF 1.1) An array of numbers in the range 0.0 to 1.0, representing a colour used for the following purposes:
                *
                *                                                      The background of the annotation’s icon when closed
                *
                *                                                      The title bar of the annotation’s pop-up window
                *  
                *                                                      The border of a link annotation
                *                          
                *                                                      The number of array elements determines the colour space in which the colour shall be defined:
                *
                *                                                      0   No colour; transparent
                *
                *                                                      1   DeviceGray
                *
                *                                                      3   DeviceRGB
                *                  
                *                                                      4   DeviceCMYK
                *
                *          StructParent        integer                 (Required if the annotation is a structural content item; PDF 1.3) 
                *                                                      The integer key of the annotation’s entry in the structural parent tree (see 14.7.4.4, “Finding Structure Elements from Content Items”).
                *
                *          OC                  dictionary              (Optional; PDF 1.5) An optional content group or optional content membership dictionary (see 8.11, “Optional Content”) specifying the optional content properties for the annotation. 
                *                                                      Before the annotation is drawn, its visibility shall be determined based on this entry as well as the annotation flags specified in the F entry (see 12.5.3, “Annotation Flags”). 
                *                                                      If it is determined to be invisible, the annotation shall be skipped, as if it were not in the document.
                */

            /*12.5.3 Annotation Flags
           *
                *The value of the annotation dictionary’s F entry is an integer interpreted as one-bit flags specifying various characteristics of the annotation. 
                *Bit positions within the flag word shall be numbered from low-order to high-order, with the lowest-order bit numbered 1. 
                *Table 165 shows the meanings of the flags; all other bits of the integer shall be set to 0.
                *
                *Table 165 - Annotation Flags
                *
                *          [Bit Position]              [Name]                  [Meaning]
                *
                *          1                           Invisible               If set, do not display the annotation if it does not belong to one of the standard annotation types and no annotation handler is available. 
                *                                                              If clear, display such an unknown annotation using an appearance stream specified by its appearance dictionary, if any (see 12.5.5, “Appearance Streams”).
                *
                *          2                           Hidden                  (PDF 1.2) If set, do not display or print the annotation or allow it to interact with the user, regardless of its annotation type or whether an annotation handler is available.
                *
                *                                                              NOTE 1      In cases where screen space is limited, the ability to hide and show annotations selectively can be used in combination with appearance streams(see 12.5.5, “Appearance Streams”) 
                *                                                                          to display auxiliary pop-up information similar in function to online help systems.
                *
                *          3                           Print                   (PDF 1.2) If set, print the annotation when the page is printed. 
                *                                                              If clear, never print the annotation, regardless of whether it is displayed on the screen.
                *
                *                                                              NOTE 2      This can be useful for annotations representing interactive pushbuttons, which would serve no meaningful purpose on the printed page.
                *
                *          4                           NoZoom                  (PDF 1.3) If set, do not scale the annotation’s appearance to match the magnification of the page. 
                *                                                              The location of the annotation on the page (defined by the upper-left corner of its annotation rectangle) shall remain fixed, regardless of the page magnification. 
                *                                                              See further discussion following this Table.
                *
                *          5                           NoRotate                (PDF 1.3) If set, do not rotate the annotation’s appearance to match the rotation of the page. 
                *                                                              The upper-left corner of the annotation rectangle shall remain in a fixed location on the page, regardless of the page rotation. 
                *                                                              See further discussion following this Table.
                *
                *          6                           NoView                  (PDF 1.3) If set, do not display the annotation on the screen or allow it to interact with the user. 
                *                                                              The annotation may be printed (depending on the setting of the Print flag) but should be considered hidden for purposes of on-screen display and user interaction.
                *
                *          7                           ReadOnly                (PDF 1.3) If set, do not allow the annotation to interact with the user. 
                *                                                              The annotation may be displayed or printed (depending on the settings of the NoView and Print flags) but should not respond to mouse clicks or change its appearance in response to mouse motions.
                *                                                              This flag shall be ignored for widget annotations; its function is subsumed by the ReadOnly flag of the associated form field(see Table 221).
                *
                *          8                           Locked                  (PDF 1.4) If set, do not allow the annotation to be deleted or its properties (including position and size) to be modified by the user. 
                *                                                              However, this flag does not restrict changes to the annotation’s contents, such as the value of a form field.
                *
                *          9                           ToggleNoView            (PDF 1.5) If set, invert the interpretation of the NoView flag for certain events.
                *
                *                                                              NOTE 3      A typical use is to have an annotation that appears only when a mouse cursor is held over it.
                *
                *          10                          LockedContents          (PDF 1.7) If set, do not allow the contents of the annotation to be modified by the user. 
                *                                                              This flag does not restrict deletion of the annotation or changes to other annotation properties, such as position and size.
                *
                *
                *If the NoZoom flag is set, the annotation shall always maintain the same fixed size on the screen and shall be unaffected by the magnification level at which the page itself is displayed. 
                *Similarly, if the NoRotate flag is set, the annotation shall retain its original orientation on the screen when the page is rotated (by changing the Rotate entry in the page object; see 7.7.3, “Page Tree”).
                *
                *In either case, the annotation’s position shall be determined by the coordinates of the upper - left corner of its annotation rectangle, as defined by the Rect entry in the annotation dictionary and interpreted in the default user space of the page.
                *When the default user space is scaled or rotated, the positions of the other three corners of the annotation rectangle are different in the altered user space than they were in the original user space. 
                *The conforming reader shall perform this alteration automatically. 
                *However, it shall not actually change the annotation’s Rect entry, which continues to describe the annotation’s relationship with the unscaled, unrotated user space.
                *
                *NOTE      Figure 58 shows how an annotation whose NoRotate flag is set remains upright when the page it is on is rotated 90 degrees clockwise. 
                *          The upper-left corner of the annotation remains at the same point in default user space; the annotation pivots around that point.
                *
                *(see Figure 58 - Coordinate adjustment with the NoRotate flag, on page 386)
                */
              
            /*12.5.4 Border Styles
            *
                *An annotation may optionally be surrounded by a border when displayed or printed. 
                *If present, the border shall be drawn completely inside the annotation rectangle. 
                *In PDF 1.1, the characteristics of the border shall be specified by the Border entry in the annotation dictionary (see Table 164). 
                *Beginning with PDF 1.2, the border characteristics for some types of annotations may instead be specified in a border style dictionary designated by the annotation’s BS entry. 
                *Such dictionaries may also be used to specify the width and dash pattern for the lines drawn by line, square, circle, and ink annotations. 
                *Table 166 summarizes the contents of the border style dictionary. If neither the Border nor the BS entry is present, the border shall be drawn as a solid line with a width of 1 point.
                *
                *Table 166 - Entries in a border style dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Border for a border style dictionary.
                *
                *          W                   number              (Optional) The border width in points. If this value is 0, no border shall drawn. 
                *                                                  Default value: 1.
                *
                *          S                   name                (Optional) The border style:
                *
                *                                                  S           (Solid) A solid rectangle surrounding the annotation.
                *
                *                                                  D           (Dashed) A dashed rectangle surrounding the annotation. The dash pattern may be specified by the D entry.
                *
                *                                                  B           (Beveled) A simulated embossed rectangle that appears to be raised above the surface of the page.
                *
                *                                                  I           (Inset) A simulated engraved rectangle that appears to be recessed below the surface of the page.
                *
                *                                                  U           (Underline) A single line along the bottom of the annotation rectangle.
                *
                *                                                  A conforming reader shall tolerate other border styles that it does not recognize and shall use the default value.
                *
                *          D                   array               (Optional) A dash array defining a pattern of dashes and gaps that shall beused in drawing a dashed border (border style D in the S entry). 
                *                                                  The dash array shall be specified in the same format as in the line dash pattern parameter of the graphics state (see 8.4.3.6, “Line Dash Pattern”). 
                *                                                  The dash phase is not specified and shall be assumed to be 0.
                *
                *                                                  EXAMPLE     A D entry of[3 2] specifies a border drawn with 3 - point dashes alternating with 2 - point gaps.
                *
                *                                                  Default value: [3].
                *
                *Beginning with PDF 1.5, some annotations (square, circle, and polygon) may have a BE entry, which is a border effect dictionary that specifies an effect that shall be applied to the border of the annotations. 
                *Beginning with PDF 1.6, the free text annotation may also have a BE entry. 
                *Table 167 describes the entries in a border effect dictionary.
                *
                *Table 167 - Entries in a border effect dictionary
                *
                *          [Key]           [Type]              [Value]
                *
                *          S               name                (Optional) A name representing the border effect to apply. Possible values are:
                *
                *                                              S       No effect: the border shall be as described by the annotation dictionary’s BS entry.
                *
                *                                              C       The border should appear “cloudy”. The width and dash array specified by BS shall be honored.
                *
                *                                              Default value: S.
                *
                *          I               number              (Optional; valid only if the value of S is C) A number describing the intensity of the effect, in the range 0 to 2. 
                *                                              Default value: 0.
                */
            
            /*12.5.5 Appearance Streams
            *
                *Beginning with PDF 1.2, an annotation may specify one or more appearance streams as an alternative to the simple border and colour characteristics available in earlier versions. 
                *Appearance streams enable the annotation to be presented visually in different ways to reflect its interactions with the user. 
                *Each appearance stream is a form XObject (see 8.10, “Form XObjects”): a self-contained content stream that shall be rendered inside the annotation rectangle.
                *
                *The algorithm outlined in this sub - clause shall be used to map from the coordinate system of the appearance XObject(as defined by its Matrix entry; see Table 97) to the annotation’s rectangle in default user space:
                *
                *Algorithm: Appearance streams
                *
                *  a)  The appearance’s bounding box(specified by its BBox entry) shall be transformed, using Matrix, to produce a quadrilateral with arbitrary orientation.
                *      The transformed appearance box is the smallest upright rectangle that encompasses this quadrilateral.
                *
                *  b)  A matrix A shall be computed that scales and translates the transformed appearance box to align with the edges of the annotation’s rectangle(specified by the Rect entry). 
                *      A maps the lower-left corner(the corner with the smallest x and y coordinates) and the upper - right corner(the corner with the greatest x and ycoordinates) of the transformed appearance box to the corresponding corners of the annotation’s rectangle.
                *
                *  c)  Matrix shall be concatenated with A to form a matrix AA that maps from the appearance’s coordinate system to the annotation’s rectangle in default user space:
                *
                *      AA = Matrix ¥ A
                *
                *The annotation may be further scaled and rotated if either the NoZoom or NoRotate flag is set (see 12.5.3, “Annotation Flags”). 
                *Any transformation applied to the annotation as a whole shall also applied to the appearance within it.
                *
                *Starting with PDF 1.4, an annotation appearance may include transparency. 
                *If the appearance’s stream dictionary does not contain a Group entry, it shall be treated as a non - isolated, non - knockout transparency group. 
                *Otherwise, the isolated and knockout values specified in the group dictionary(see 11.6.6, “Transparency Group XObjects”) shall be used.
                *
                *The transparency group shall be composited with a backdrop consisting of the page content along with any previously painted annotations, using a blend mode of Normal, an alpha constant of 1.0, and a soft mask of None.
                *
                *NOTE 1        If a transparent annotation appearance is painted over an annotation that is drawn without using an appearance stream, the effect is implementation-dependent. 
                *              This is because such annotations are sometimes drawn by means that do not conform to the PDF imaging model. 
                *              Also, the effect of highlighting a transparent annotation appearance is implementation-dependent.
                *
                *An annotation may define as many as three separate appearances:
                *
                *  •   The normal appearance shall be used when the annotation is not interacting with the user.This appearance is also used for printing the annotation.
                *
                *  •   The rollover appearance shall be used when the user moves the cursor into the annotation’s active area without pressing the mouse button.
                *
                *  •   The down appearance shall be used when the mouse button is pressed or held down within the annotation’s active area.
                *
                *NOTE 2        As used here, the term mouse denotes a generic pointing device that controls the location of a cursor on the screen and has at least one button that can be pressed, held down, and released.
                *              See 12.6.3, “Trigger Events,” for further discussion.
                *
                *The normal, rollover, and down appearances shall be defined in an appearance dictionary, which in turn is the value of the AP entry in the annotation dictionary(see Table 164).
                *Table 168 shows the contents of the appearance dictionary.
                *
                *Table 168 - Entries in an appearance dictionary
                *
                *          [Key]                   [Type]                          [Value]
                *
                *          N                       stream or dictionary            (Required) The annotation’s normal appearance.
                *
                *          R                       stream or dictionary            (Optional) The annotation’s rollover appearance. 
                *                                                                  Default value: the value of the N entry.
                *
                *          D                       stream or dictionary            (Optional) The annotation’s down appearance. Default value: the value of the N entry.
                *
                *Each entry in the appearance dictionary may contain either a single appearance stream or an appearance subdictionary. 
                *In the latter case, the subdictionary shall define multiple appearance streams corresponding to different appearance states of the annotation.
                *
                *EXAMPLE       An annotation representing an interactive check box may have two appearance states named On and Off. Its appearance dictionary may be defined as
                *
                *              / AP << / N << / On formXObject1
                *                             / Off formXObject2
                *                          >>
                *                      / D << / On formXObject3
                *                             / Off formXObject4
                *                          >>
                *                  >>
                *
                *              where formXObject1 and formXObject2 define the check box’s normal appearance in its checked and unchecked states, and formXObject3 and formXObject4 provide visual feedback, such as emboldening its outline, when the user clicks it. 
                *              (No R entry is defined because no special appearance is needed when the user moves the cursor over the check box without pressing the mouse button.) 
                *              The choice between the checked and unchecked appearance states is determined by the AS entry in the annotation dictionary(see Table 164).
                *
                *NOTE 3        If a conforming reader does not have native support for a particular annotation type conforming readers shall display the annotation with its normal(N) appearance.
                *              Conforming readers shall also attempt to provide reasonable behavior(such as displaying nothing) if an annotation’s AS entry designates an appearance state for which no appearance is defined in the appearance dictionary.
                *
                *For convenience in managing appearance streams that are used repeatedly, the AP entry in a PDF document’s name dictionary (see 7.7.4, “Name Dictionary”) may contain a name tree mapping name strings to appearance streams. 
                *The name strings have no standard meanings; no PDF objects may refer to appearance streams by name.
                */

            /*12.5.6 Annotation_Types
            */

                /*12.5.6.1 General
                *
                *PDF supports the standard annotation types listed in Table 169.The following sub-clauses describe each of these types in detail.
                *
                *The values in the first column of Table 169 represent the value of the annotation dictionary’s Subtype entry.
                *The third column indicates whether the annotation is a markup annotation, as described in 12.5.6.2, “Markup Annotations.” 
                *The sub-clause also provides more information about the value of the Contents entry for different annotation types.
                *
                *Table 169 - Annotation types
                *
                *      [Annotation type]           [Description]                               [Markup]                [Discussed in sub-clause]
                *
                *      Text                        Text annotation                             Yes                     12.5.6.4, "Text Annotation"
                *
                *      Link                        Link annotation                             No                      12.5.6.5, "Link Annotations"
                *
                *      FreeText                    (PDF 1.3) Free Text annotation              Yes                     12.5.6.6, "Free Text Annotations"
                *
                *      Line                        (PDF 1.3) Line annotation                   Yes                     12.5.6.7, "Line Annotations"
                *
                *      Square                      (PDF 1.3) Square annotation                 Yes                     12.5.6.8, "Square and Circle Annotations"
                *
                *      Circle                      (PDF 1.3) Circle annotation                 Yes                     12.5.6.8, "Square and Circle Annotations"
                *
                *      Polygon                     (PDF 1.5) Polygon annotation                Yes                     12.5.6.9, "Polygon and Polyline Annotations"
                *
                *      PolyLine                    (PDF 1.5) Polyline annotation               Yes                     12.5.6.9, "Polygon and Polyline Annotations"
                *
                *      Highlight                   (PDF 1.3) Highlight annotation              Yes                     12.5.6.10, "Text Markup Annotations"
                *
                *      Underline                   (PDF 1.3) Underline annotation              Yes                     12.5.6.10, "Text Markup Annotations"
                *
                *      Squiggly                    (PDF 1.4) Squiggly-underline annotation     Yes                     12.5.6.10, "Text Markup Annotations"
                *
                *      StrikeOut                   (PDF 1.3) Strikeout annotation              Yes                     12.5.6.10, "Text Markup Annotations"
                *
                *      Stamp                       (PDF 1.3) Rubber stamp annotation           Yes                     12.5.6.12, "Rubber Stamp Annotations"
                *
                *      Caret                       (PDF 1.5) Caret annotation                  Yes                     12.5.6.11, "Caret Annotations"
                *
                *      Ink                         (PDF 1.3) Ink annotation                    Yes                     12.5.6.13, "Ink Annotations"
                *
                *      Popup                       (PDF 1.3) Pop-up annotation                 No                      12.5.6.14, "Pop-up Annotations"
                *
                *      FileAttachment              (PDF 1.3) File attachment                   Yes                     12.5.6.15, "File Attachment Annotations"
                *
                *      Sound                       (PDF 1.2) Sound annotation                  Yes                     12.5.6.16, "Sound Annotations"
                *
                *      Movie                       (PDF 1.2) Movie annotation                  No                      12.5.6.17, "Movie Annotations"
                *
                *      Widget                      (PDF 1.2) Widget annotation                 No                      12.5.6.19, "Widget Annotations"
                *
                *      Screen                      (PDF 1.5) Screen annotation                 No                      12.5.6.18, "Screen Annotations"
                *
                *      PrinterMark                 (PDF 1.4) Printer's mark annotation         No                      12.5.6.20, "Printer's Mark Annotations"
                *
                *      TrapNet                     (PDF 1.3) Trap network annotation           No                      12.5.6.21, "Trap Network Annotations"
                *
                *      3D                          (PDF 1.6) 3D annotation                     No                      13.6.2, "3D Annotations"
                *
                *      Redact                      (PDF 1.7) Redact annotation                 Yes                     12.5.6.23, "Redaction Annotations"
                *
                */

                /*12.5.6.2 Markup Annotations
                *
                *As mentioned in 12.5.2, “Annotation Dictionaries,” the meaning of an annotation’s Contents entry varies by annotation type. 
                *Typically, it is the text that shall be displayed for the annotation or, if the annotation does not display text, an alternate description of the annotation’s contents in human-readable form. 
                *In either case, the Contents entry is useful when extracting the document’s contents in support of accessibility to users with disabilities or for other purposes (see 14.9.3, “Alternate Descriptions”).
                *
                *Many annotation types are defined as markup annotations because they are used primarily to mark up PDF documents(see Table 170).
                *These annotations have text that appears as part of the annotation and may be displayed in other ways by a conforming reader, such as in a Comments pane.
                *
                *Markup annotations may be divided into the following groups:
                *
                *  •   Free text annotations display text directly on the page.
                *      The annotation’s Contents entry specifies the displayed text.
                *
                *  •   Most other markup annotations have an associated pop-up window that may contain text.
                *      The annotation’s Contents entry specifies the text that shall be displayed when the pop - up window is opened.These include text, line, square, circle, polygon, polyline, highlight, underline, squiggly - underline, strikeout, rubber stamp, caret, ink, and file attachment annotations.
                *
                *  •   Sound annotations do not have a pop - up window but may also have associated text specified by the Contents entry.
                *
                *When separating text into paragraphs, a CARRIAGE RETURN(0Dh) shall be used and not, for example, a LINE FEED character(0Ah).
                *
                *NOTE 1        A subset of markup annotations is called text markup annotations(see 12.5.6.10, “Text Markup Annotations”).
                *
                *The remaining annotation types are not considered markup annotations:
                *
                *  •   The pop-up annotation type shall not appear by itself; it shall be associated with a markup annotation that uses it to display text.
                *
                *NOTE 2        If an annotation has no parent, the Contents entry shall represent the text of the annotation, otherwise it shall be ignored by a conforming reader.
                *
                *  •   For all other annotation types(Link, Movie, Widget, PrinterMark, and TrapNet), the Contents entry shall provide an alternate representation of the annotation’s contents in human - readable form, which is useful when extracting the document’s contents in support of accessibility to users with disabilities or for other purposes (see 14.9.3, “Alternate Descriptions”).
                *
                *Table 170 lists entries that apply to all markup annotations.
                *
                *Table 170 - Additional entries specific to markup annotations
                *
                *          [Key]               [Type]              [Value]
                *
                *          T                   text string         (Optional; PDF 1.1) The text label that shall be displayed in the title bar of the annotation’s pop-up window when open and active. 
                *                                                  This entry shall identify the user who added the annotation.
                *
                *          Popup               dictionary          (Optional; PDF 1.3) An indirect reference to a pop-up annotation for entering or editing the text associated with this annotation.
                *
                *          CA                  number              (Optional; PDF 1.4) The constant opacity value that shall be used in painting the annotation (see Sections 11.2, “Overview of Transparency,”and 11.3.7, “Shape and Opacity Computations”). 
                *                                                  This value shall apply to all visible elements of the annotation in its closed state (including its background and border) but not to the pop-up window that appears when the annotation is opened.
                *
                *                                                  The specified value shall not used if the annotation has an appearance stream(see 12.5.5, “Appearance Streams”); in that case, the appearance stream shall specify any transparency. 
                *                                                  (However, if the compliant viewer regenerates the annotation’s appearance stream, it may incorporate the CA value into the stream’s content.)
                *
                *                                                  The implicit blend mode (see 11.3.5, “Blend Mode””) is Normal.
                *                                                  Default value: 1.0.
                *
                *                                                  If no explicit appearance stream is defined for the annotation, it may bepainted by implementation-dependent means that do not necessarily conform to the PDF imaging model; 
                *                                                  in this case, the effect of this entry is implementation-dependent as well.
                *
                *          RC                  text string         (Optional; PDF 1.5) A rich text string (see 12.7.3.4, “Rich Text Strings”) that shall be displayed in the pop-up window when the annotation is opened.
                *                              or text
                *                              stream
                *
                *          CreationDate        date                (Optional; PDF 1.5) The date and time (7.9.4, “Dates”) when the annotation was created.
                *
                *          IRT                 dictionary          (Required if an RT entry is present, otherwise optional; PDF 1.5) A reference to the annotation that this annotation is “in reply to.” 
                *                                                  Both annotations shall be on the same page of the document. 
                *                                                  The relationship between the two annotations shall be specified by the RT entry.
                *                                                  
                *                                                  If this entry is present in an FDF file(see 12.7.7, “Forms Data Format”), its type shall not be a dictionary but a text string containing the contents of the NM entry of the annotation being replied to, 
                *                                                  to allow for a situation where the annotation being replied to is not in the same FDF file.
                *
                *          Subj                text string         (Optional; PDF 1.5) Text representing a short description of the subject being addressed by the annotation.
                *
                *          RT                  name                (Optional; meaningful only if IRT is present; PDF 1.6) A name specifying the relationship (the “reply type”) between this annotation and one specified by IRT. 
                *                                                  
                *                                                  Valid values are:
                *                                                  
                *                                                  R       The annotation shall be considered a reply to the annotation specified by IRT.
                *                                                          Conforming readers shall not display replies to an annotation individually but together in the form of threaded comments.
                *
                *                                                  Group   The annotation shall be grouped with the annotation specified by IRT; see the discussion following this Table.
                *
                *                                                  Default value: R.
                *
                *          IT                  name                (Optional; PDF 1.6) A name describing the intent of the markup annotation. 
                *                                                  Intents allow conforming readers to distinguish between different uses and behaviors of a single markup annotation type. 
                *                                                  If this entry is not present or its value is the same as the annotation type, the annotation shall have no explicit intent and should behave in a generic manner in a conforming reader.
                *
                *                                                  Free text annotations(Table 174), line annotations(Table 175), polygon annotations(Table 178), and(PDF 1.7) polyline annotations(Table 178) have defined intents, whose values are enumerated in the corresponding tables.
                *
                *          ExData              dictionary          (Optional; PDF 1.7) An external data dictionary specifying data that shall be associated with the annotation. 
                *                                                  
                *                                                  This dictionary contains the following entries:
                *
                *                                                  Type            (optional) If present, shall be ExData.
                *
                *                                                  Subtype         (required) a name specifying the type of data that the markup annotation shall be associated with.
                *                                                                  The only defined value is Markup3D.
                *                                                                  Table 298 lists the values that correspond to a subtype of Markup3D.
                *
                *In PDF 1.6, a set of annotations may be grouped so that they function as a single unit when a user interacts with them. 
                *The group consists of a primary annotation, which shall not have an IRT entry, and one or more subordinate annotations, which shall have an IRT entry that refers to the primary annotation and an RT entry whose value is Group.
                *
                *Some entries in the primary annotation are treated as “group attributes” that shall apply to the group as a whole; the corresponding entries in the subordinate annotations shall be ignored. 
                *These entries are Contents(or RC and DS), M, C, T, Popup, CreationDate, Subj, and Open. Operations that manipulate any annotation in a group, such as movement, cut, and copy, shall be treated by conforming readers as acting on the entire group.
                *
                *NOTE 3        A primary annotation may have replies that are not subordinate annotations; that is, that do not have an RTvalue of Group.
                */

                /*12.5.6.3 Annotation States
                *
                *Beginning with PDF 1.5, annotations may have an author - specific state associated with them. 
                *The state is not specified in the annotation itself but in a separate text annotation that refers to the original annotation by means of its IRT(“in reply to”) entry(see Table 173).
                *States shall be grouped into a number of state models, as shown in Table 171.
                *
                *Table 171 - Annotation states
                *
                *      [State model]           [State]         [Description]
                *
                *      Marked                  Marked          The annotation has been marked by the user.
                *
                *                              Unmarked        The annotation has not been matked by the user (the default).
                *
                *      Review                  Accepted        The user agrees with the change.
                *
                *                              Rejected        The user disagrees with the change.
                *
                *                              Cancelled       The change has been cancelled.
                *
                *                              Completed       The change has been completed.
                *
                *                              None            The user has indicated nothing about the change (the default).
                *
                *Annotations shall be thought of as initially being in the default state for each state model. 
                *State changes made by a user shall be indicated in a text annotation with the following entries:
                *
                *  •   The T entry(see Table 170) shall specify the user.
                *
                *  •   The IRT entry(see Table 173) shall refer to the original annotation.
                *
                *  •   State and StateModel(see Table 172) shall update the state of the original annotation for the specified user.
                *
                *Additional state changes shall be made by adding text annotations in reply to the previous reply for a given user.
                */

                /*12.5.6.4 Text Annotations
                *
                *A text annotation represents a “sticky note” attached to a point in the PDF document.When closed, the annotation shall appear as an icon; when open, it shall display a pop-up window containing the text of the note in a font and size chosen by the conforming reader.
                *Text annotations shall not scale and rotate with the page; they shall behave as if the NoZoom and NoRotate annotation flags(see Table 165) were always set.
                *Table 172shows the annotation dictionary entries specific to this type of annotation.
                *
                *Table 172 - Additional entries specific to a text annotation
                *
                *      [Key]           [Type]              [Value]
                *
                *      Subtype         name                (Required) The type of annotation that this dictionary describes; shall be Text for a text annotation.
                *
                *      Open            boolean             (Optional) A flag specifying whether the annotation shall initially be displayed open. 
                *                                          Default value: false (closed).
                *
                *      Name            name                (Optional) The name of an icon that shall be used in displaying the annotation. 
                *                                          Conforming readers shall provide predefined icon appearances for at least the following standard names:
                *                                          
                *                                          Comment, Key, Note, Help, NewParagraph, Paragraph, Insert
                *
                *                                          Additional names may be supported as well.
                *
                *                                          Default value: Note.
                *
                *                                          The annotation dictionary’s AP entry, if present, shall take precedence over the Name entry; see Table 168 and 12.5.5, “Appearance Streams.”
                *
                *      State           text string         (Optional; PDF 1.5) The state to which the original annotation shall be set; see 12.5.6.3, “Annotation States.”
                *
                *                                          Default: “Unmarked” if StateModel is “Marked”; “None” if StateModel is “Review”.
                *
                *      StateModel      text string         (Required if State is present, otherwise optional; PDF 1.5) The state model corresponding to State; see 12.5.6.3, “Annotation States.”
                *
                *EXAMPLE       The following example shows the definition of a text annotation.
                *
                *              22 0 obj
                *                  << / Type / Annot
                *                     / Subtype / Text
                *                     / Rect[266 116 430 204]
                *                     / Contents(The quick brown fox ate the lazy mouse.)
                *                  >>
                *              endobj
                */

                /*12.5.6.5 Link Annotations
                *
                *A link annotation represents either a hypertext link to a destination elsewhere in the document(see 12.3.2, “Destinations”) or an action to be performed(12.6, “Actions”). 
                *Table 173 shows the annotation dictionary entries specific to this type of annotation.
                *
                *Table 173 - Additional entries specific to a link annotation
                *
                *      [Key]           [Type]              [Value]
                *
                *      Subtype         name                (Required) The type of annotation that this dictionary describes; shall be Link for a link annotation.
                *
                *      A               dictionary          (Optional; PDF 1.1) An action that shall be performed when the link annotation is activated (see 12.6, “Actions”).
                *
                *      Dest            array, name or      (Optional; not permitted if an A entry is present) A destination that shall be displayed when the annotation is activated (see 12.3.2, “Destinations”).
                *                      byte string
                *
                *      H               name                (Optional; PDF 1.2) The annotation’s highlighting mode, the visual effect that shall be used when the mouse button is pressed or held down inside its active area:
                *
                *                                          N       (None) No highlighting.
                *
                *                                          I       (Invert) Invert the contents of the annotation rectangle.
                *
                *                                          O       I(Outline) Invert the annotation’s border.
                *
                *                                          P       (Push) Display the annotation as if it were being pushed below the surface of the page.
                *
                *                                          Default value: I.
                *
                *      PA              dictionary          (Optional; PDF 1.3) A URI action (see 12.6.4.7, “URI Actions”) formerly associated with this annotation. 
                *                                          When Web Capture (14.10, “ Web Capture”) changes an annotation from a URI to a go-to action (12.6.4.2, “Go-To Actions”), it uses this entry to save the data from the original URI action so that it can be changed back in case the target page for the go-to action is subsequently deleted.
                *
                *      QuadPoints      array               (Optional; PDF 1.6) An array of 8 × n numbers specifying the coordinates of n quadrilaterals in default user space that comprise the region in which the link should be activated. 
                *                                          
                *                                          The coordinates for each quadrilateral are given in the order
                *
                *                                          x1 y1 x2 y2 x3 y3 x4 y4
                *
                *                                          specifying the four vertices of the quadrilateral in counterclockwise order. 
                *                                          For orientation purposes, such as when applying an underline border style, the bottom of a quadrilateral is the line formed by(x1, y1)and(x2, y2).
                *
                *                                          If this entry is not present or the conforming reader does not recognize it, the region specified by the Rect entry should be used.
                *                                          QuadPointsshall be ignored if any coordinate in the array lies outside the region specified by Rect.
                *
                *      BS               dictionary         (Optional; PDF 1.6) A border style dictionary (see Table 166) specifying the line width and dash pattern to be used in drawing the annotation’s border.
                *
                *                                          The annotation dictionary’s AP entry, if present, takes precedence over the BS entry; see Table 164 and 12.5.5, “Appearance Streams”.
                *
                *EXAMPLE       The following example shows a link annotation that jumps to a destination elsewhere in the document.
                *
                *               93 0 obj
                *                  << / Type / Annot
                *                     / Subtype / Link
                *                     / Rect[71 717 190 734]
                *                     / Border[16 16 1]
                *                     / Dest[3 0 R / FitR –4 399 199 533]
                *                  >>
                *              endobj
                *
                */

                /*12.5.6.6 Free Text Annotations
                *
                *A free text annotation(PDF 1.3) displays text directly on the page. 
                *Unlike an ordinary text annotation(see 12.5.6.4, “Text Annotations”), a free text annotation has no open or closed state; instead of being displayed in a pop-up window, the text shall be always visible. 
                *Table 174 shows the annotation dictionary entries specific to this type of annotation. 12.7.3.3, “Variable Text” describes the process of using these entries to generate the appearance of the text in these annotations.
                *
                *Table 174 - Additional entries specific to a free text annotation
                *
                *          [Key]           [Type]              [Value]
                *
                *          Subtype         name                (Required) The type of annotation that this dictionary describes; shall be FreeText for a free text annotation.
                *
                *          DA              string              (Required) The default appearance string that shall be used in formatting the text (see 12.7.3.3, “Variable Text”).
                *                                              
                *                                              The annotation dictionary’s AP entry, if present, shall take precedence over the DA entry; see Table 168 and 12.5.5, “Appearance Streams.”
                *
                *          Q               integer             (Optional; PDF 1.4) A code specifying the form of quadding (justification) that shall be used in displaying the annotation’s text:
                *
                *                                              0       Left - justified
                *
                *                                              1       Centered
                *
                *                                              2       Right - justified
                *
                *                                              Default value: 0(left - justified).
                *
                *          RC              text string         (Optional; PDF 1.5) A rich text string (see 12.7.3.4, “Rich Text Strings”) that shall be used to generate the appearance of the annotation.
                *                          or text
                *                          stream
                *
                *          DS              text string         (Optional; PDF 1.5) A default style string, as described in 12.7.3.4, “Rich Text Strings.”
                *
                *          CL              array               (Optional; meaningful only if IT is FreeTextCallout; PDF 1.6) 
                *                                              An array of four or six numbers specifying a callout line attached to the free text annotation. 
                *                                              Six numbers [ x1 y1 x2 y2 x3 y3 ] represent the starting, knee point, and ending coordinates of the line in default user space, as shown in Figure 8.4. 
                *                                              Four numbers [ x1 y1 x2 y2 ] represent the starting and ending coordinates of the line.
                *
                *          IT              name                (Optional; PDF 1.6) A name describing the intent of the free text annotation (see also the IT entry in Table 170). 
                *  
                *                                              The following values shall be valid:
                *
                *                                              FreeText            The annotation is intended to function as a plain free-text annotation.
                *                                                                  A plain free - text annotation is also known as a text box comment.
                *
                *                                              FreeTextCallout     The annotation is intended to function as a callout.
                *                                                                  The callout is associated with an area on the page through the callout line specified in CL.
                *
                *                                              FreeTextTypeWriter  The annotation is intended to function as a click - to - type or typewriter object and no callout line is drawn.
                *
                *                                              Default value: FreeText
                *
                *          BE              dictionary          (Optional; PDF 1.6) A border effect dictionary (see Table 167) used in conjunction with the border style dictionary specified by the BS entry.
                *
                *          RD              rectangle           (Optional; PDF 1.6) A set of four numbers describing the numerical differences between two rectangles: the Rect entry of the annotation and a rectangle contained within that rectangle. 
                *                                              The inner rectangle is where the annotation’s text should be displayed. Any border styles and/or border effects specified by BS and BE entries, respectively, shall be applied to the border of the inner rectangle.
                *
                *                                              The four numbers correspond to the differences in default user space between the left, top, right, and bottom coordinates of Rect and those of the inner rectangle, respectively.
                *                                              Each value shall be greater than or equal to 0.The sum of the top and bottom differences shall be less than the height of Rect, and the sum of the left and right differences shall be less than the width of Rect.
                *
                *          BS              dictionary          (Optional; PDF 1.6) A border style dictionary (see Table 166) specifying the line width and dash pattern that shall be used in drawing the annotation’s border.
                *
                *                                              The annotation dictionary’s AP entry, if present, takes precedence over the BS entry; see Table 164 and 12.5.5, “Appearance Streams”.
                *
                *          LE              name                (Optional; meaningful only if CL is present; PDF 1.6) A name specifying the line ending style that shall be used in drawing the callout line specified in CL. 
                *                                              The name shall specify the line ending style for the endpoint defined by the pairs of coordinates (x1, y1). 
                *                                              Table 176 shows the possible line ending styles.
                *                                              Default value: None.
                *
                *(see Figure 59 - Free text annotation with callout, page 397)
                */
                
                /*12.5.6.7  Line Annotations
                *
                *The purpose of a line annotation(PDF 1.3) is to display a single straight line on the page.
                *When opened, it shall display a pop-up window containing the text of the associated note. 
                *Table 175 shows the annotation dictionary entries specific to this type of annotation.
                *
                *Table 175 - Additional entries specific to a line annotation
                *
                *          [Key]           [Type]              [Value]
                *
                *          Subtype         name                (Required) The type of annotation that this dictionary describes; shall be Line for a line annotation.
                *
                *          L               array               (Required) An array of four numbers, [x1 y1 x2 y2], specifying the starting and ending coordinates of the line in default user space.
                *                                              If the LL entry is present, this value shall represent the endpoints of the leader lines rather than the endpoints of the line itself; see Figure 60.
                *
                *          BS              dictionary          (Optional) A border style dictionary (see Table 166) specifying the width and dash pattern that shall be used in drawing the line.
                *                                              The annotation dictionary’s AP entry, if present, shall take precedence over the L and BS entries; see Table 168 and 12.5.5, “Appearance Streams.”
                *
                *          LE              array               (Optional; PDF 1.4) An array of two names specifying the line ending styles that shall be used in drawing the line. 
                *                                              The first and second elements of the array shall specify the line ending styles for the endpoints defined, respectively, by the first and second pairs of coordinates, (x1, y1)and (x2, y2), in the L array. 
                *                                              Table 176 shows the possible values. Default value: [/None /None].
                *
                *          IC              array               (Optional; PDF 1.4) An array of numbers in the range 0.0 to 1.0 specifying the interior color that shall be used to fill the annotation’s line endings (see Table 176). 
                *                                              The number of array elements shall determine the colour space in which the colour is defined:
                *              
                *                                              0   No colour; transparent
                *          
                *                                              1   DeviceGray
                *   
                *                                              3   DeviceRGB
                *
                *                                              4   DeviceCMYK
                *
                *          LL              number              (Required if LLE is present, otherwise optional; PDF 1.6) The length of leader lines in default user space that extend from each endpoint of the line perpendicular to the line itself, as shown in Figure 60. 
                *                                              A positive value shall mean that the leader lines appear in the direction that is clockwise when traversing the line from its starting point to its ending point (as specified by L); a negative value shall indicate the opposite direction.
                *                                              Default value: 0(no leader lines).
                *
                *          LE              number              (Optional; PDF 1.6) A non-negative number that shall represents the length of leader line extensions that extend from the line proper 180 degrees from the leader lines, as shown in Figure 60.
                *                                              Default value: 0(no leader line extensions).
                *
                *          Cap             boolean             (Optional; PDF 1.6) If true, the text specified by the Contents or RCentries shall be replicated as a caption in the appearance of the line, as shown in Figure 61 and Figure 62. 
                *                                              The text shall be rendered in a manner appropriate to the content, taking into account factors such as writing direction.
                *                                              Default value: false.
                *
                *          IT              name                (Optional; PDF 1.6) A name describing the intent of the line annotation (see also Table 170). 
                *                                              Valid values shall be LineArrow, which means that the annotation is intended to function as an arrow, and LineDimension, which means that the annotation is intended to function as a dimension line.
                *
                *          LLO             number              (Optional; PDF 1.7) A non-negative number that shall represent the length of the leader line offset, which is the amount of empty space between the endpoints of the annotation and the beginning of the leader lines.
                *
                *          CP              name                (Optional; meaningful only if Cap is true; PDF 1.7) A name describing the annotation’s caption positioning. 
                *                                              Valid values are Inline, meaning the caption shall be centered inside the line, and Top, meaning the caption shall be on top of the line.
                *                                              Default value: Inline
                *
                *          Measure         dictionary          (Optional; PDF 1.7) A measure dictionary (see Table 261) that shall specify the scale and units that apply to the line annotation.
                *
                *          CO              array               (Optional; meaningful only if Cap is true; PDF 1.7) An array of two numbers that shall specify the offset of the caption text from its normal position. 
                *                                              The first value shall be the horizontal offset along the annotation line from its midpoint, with a positive value indicating offset to the right and a negative value indicating offset to the left. 
                *                                              The second value shall be the vertical offset perpendicular to the annotation line, with a positive value indicating a shift up and a negative value indicating a shift down.
                *                                              Default value: [0, 0] (no offset from normal positioning)
                *
                *(see Figure 60 - Leader lines, on page 399)
                *
                *Figure 61 illustrates the effect of including a caption to a line annotation, which is specified by setting Cap to true.
                *
                *(see Figure 61 - Lines with captions appearing as part of the line, on page 400)
                *
                *Figure 62 illustrates the effect of applying a caption to a line annotation that has a leader offset.
                *
                *(see Figure 62 - Line with a caption appearing as part of the offset, on page 400)
                *
                *Table 176 - Line ending styles
                *
                *          [Name]              [Appearance]                [Description]
                *
                *          Square              (see on page 400)           A square filled with the annotation’s interior color, if any
                *
                *          Circle              (see on page 400)           A circle filled with the annotation’s interior color, if any
                *
                *          Diamond             (see on page 400)           A diamond shape filled with the annotation’s interior color, if any
                *
                *          OpenArrow           (see on page 400)           Two short lines meeting in an acute angle to form an open arrowhead
                *
                *          ClosedArrow         (see on page 400)           Two short lines meeting in an acute angle as in the OpenArrow style and connected by a third line to form a triangular closed arrowhead filled with the annotation’s interior color, if any
                *
                *          None                (see on page 400)           No line ending
                *
                *          Butt                (see on page 400)           (PDF 1.5) A short line at the endpoint perpendicular to the line itself
                *
                *          ROpenArrow          (see on page 400)           (PDF 1.5) Two short lines in the reverse direction from OpenArrow
                *
                *          RClosedArrow        (see on page 400)           (PDF 1.5) A triangular closed arrowhead in the reverse direction from ClosedArrow
                *
                *          Slash               (see on page 400)           (PDF 1.6) A short line at the endpoint approximately 30 degrees clockwise from perpendicular to the line itself
                *
                */

                /*12.5.6.8 Square and Circle Annotations
                *
                *Square and circle annotations (PDF 1.3) shall display, respectively, a rectangle or an ellipse on the page. When opened, they shall display a pop-up window containing the text of the associated note. 
                *The rectangle or ellipse shall be inscribed within the annotation rectangle defined by the annotation dictionary’s Rect entry (see Table 168).
                *
                *Figure 63 shows two annotations, each with a border width of 18 points.
                *Despite the names square and circle, the width and height of the annotation rectangle need not be equal.Table 177 shows the annotation dictionary entries specific to these types of annotations.
                *
                *(see Figure 63 - Square and circle annotations, on page 401)
                *
                *Table 177 - Additional entries specific to a square or circle annotation
                *
                *          [Key]           [Type]          [Value]
                *
                *          Subtype         name            (Required) The type of annotation that this dictionary describes; shall be Square or Circle for a square or circle annotation, respectively.
                *
                *          BS              dictionary      (Optional) A border style dictionary (see Table 166) specifying the line width and dash pattern that shall be used in drawing the rectangle or ellipse.
                *                                          The annotation dictionary’s AP entry, if present, shall take precedence over the Rect and BS entries; see Table 168 and 12.5.5, “Appearance Streams.”
                *
                *          IC              array           (Optional; PDF 1.4) An array of numbers that shall be in the range 0.0 to 1.0 and shall specify the interior color with which to fill the annotation’s rectangle or ellipse. 
                *                                          The number of array elements determines the colour space in which the colour shall be defined:
                *
                *                                          0   No colour; transparent
                *
                *                                          1   DeviceGray
                *
                *                                          3   DeviceRGB
                *
                *                                          4   DeviceCMYK
                *
                *          BE              dictionary      (Optional; PDF 1.5) A border effect dictionary describing an effect applied to the border described by the BS entry (see Table 167).
                *
                *          RD              rectangle       (Optional; PDF 1.5) A set of four numbers that shall describe the numerical differences between two rectangles: the Rect entry of the annotation and the actual boundaries of the underlying square or circle. 
                *                                          Such a difference may occur in situations where a border effect (described by BE) causes the size of the Rect to increase beyond that of the square or circle.
                *
                *                                          The four numbers shall correspond to the differences in default user space between the left, top, right, and bottom coordinates of Rect and those of the square or circle, respectively. 
                *                                          Each value shall be greater than or equal to 0.The sum of the top and bottom differences shall be less than the height of Rect, and the sum of the left and right differences shall be less than the width of Rect.
                */

                /*12.5.6.9 - Polygon and polyline Annotations
                *
                *Polygon annotations (PDF 1.5) display closed polygons on the page. Such polygons may have any number of vertices connected by straight lines. 
                *Polyline annotations (PDF 1.5) are similar to polygons, except that the first and last vertex are not implicitly connected.
                *
                *Table 178 - Additional entries specific to a polygon or polyline annotation
                *
                *          [Key]           [Type]              [Value]
                *
                *          Subtype         name                (Required) The type of annotation that this dictionary describes; shall be Polygon or PolyLine for a polygon or polyline annotation, respectively.
                *
                *          Vertices        array               (Required) An array of numbers (see Table 174) specifying the width and dash pattern that shall represent the alternating horizontal and vertical coordinates, respectively, of each vertex, in default user space.
                *
                *          LE              array               (Optional; meaningful only for polyline annotations) An array of two names that shall specify the line ending styles. 
                *                                              The first and second elements of the array shall specify the line ending styles for the endpoints defined, respectively, by the first and last pairs of coordinates in the Vertices array. 
                *                                              Table 176 shows the possible values. Default value: [/None /None].
                *
                *          BS              dictionary          (Optional) A border style dictionary (see Table 166) specifying the width and dash pattern that shall be used in drawing the line.
                *                                              The annotation dictionary’s AP entry, if present, shall take precedence over the Vertices and BS entries; see Table 168 and 12.5.5, “Appearance Streams.”
                *
                *          IC              array               (Optional; PDF 1.4) An array of numbers that shall be in the range 0.0 to 1.0 and shall specify the interior color with which to fill the annotation’s line endings (see Table 176). 
                *                                              The number of array elements determines the colour space in which the colour shall be defined:
                *
                *                                              0   No colour; transparent
                *
                *                                              1   DeviceGray
                *
                *                                              3   DeviceRGB
                *
                *                                              4   DeviceCMYK
                *
                *          BE              dictionary          (Optional; meaningful only for polygon annotations) A border effect dictionary that shall describe an effect applied to the border described by the BS entry (see Table 167).
                *
                *          IT              name                (Optional; PDF 1.6) A name that shall describe the intent of the polygon or polyline annotation (see also Table 170). 
                *                                              The following values shall bevalid:
                *
                *                                              PolygonCloud            The annotation is intended to function as a cloud object.
                *
                *                                              PolyLineDimension       (PDF 1.7) The polyline annotation is intended to function as a dimension.
                *
                *                                              PolygonDimension        (PDF 1.7) The polygon annotation is intended to function as a dimension.
                *
                *          Measure         dictionary          (Optional; PDF 1.7) A measure dictionary (see Table 261) that shall specify the scale and units that apply to the annotation.
                */

                /*12.5.6.10 Text Markup Annotations
                *
                *Text markup annotations shall appear as highlights, underlines, strikeouts (all PDF 1.3), or jagged (“squiggly”) underlines (PDF 1.4) in the text of a document. 
                *When opened, they shall display a pop-up window containing the text of the associated note. 
                *Table 179 shows the annotation dictionary entries specific to these types of annotations.
                *
                *Table 179 - Additional entries specific to a text markup annotations
                *
                *          [Key]           [Type]          [Value]
                *
                *          Subtype         Name            (Required) The type of annotation that this dictionary describes; shallbe Highlight, Underline, Squiggly, or StrikeOut for a highlight, underline, squiggly-underline, or strikeout annotation, respectively.
                *
                *          QuadPoints      array           (Required) An array of 8 × n numbers specifying the coordinates of nquadrilaterals in default user space. 
                *                                          Each quadrilateral shall encompasses a word or group of contiguous words in the text underlying the annotation. 
                *                                          The coordinates for each quadrilateral shall be given in the order
                *
                *                                          x1 y1 x2 y2 x3 y3 x4 y4
                *
                *                                          specifying the quadrilateral’s four vertices in counterclockwise order(see Figure 64). 
                *                                          The text shall be oriented with respect to the edge connecting points(x1, y1) and(x2, y2).
                *
                *                                          The annotation dictionary’s AP entry, if present, shall take precedence over QuadPoints; see Table 168 and 12.5.5, “Appearance Streams.”
                *
                *(see Figure 64 - QuadPoints specification, on page 404)
                */

                /*12.5.6.11 Caret Annotations
                *
                *A caret annotation (PDF 1.5) is a visual symbol that indicates the presence of text edits. Table 180 lists the entries specific to caret annotations.
                *
                *Table 180 - Additional entries specific to a caret annotation
                *
                *          [Key]           [Type]          [Value]
                *
                *          Subtype         name            (Required) The type of annotation that this dictionary describes; shall be Caret for a caret annotation.
                *
                *          RD              rectangle       (Optional; PDF 1.5) A set of four numbers that shall describe the numerical differences between two rectangles: the Rect entry of the annotation and the actual boundaries of the underlying caret. 
                *                                          Such a difference can occur. When a paragraph symbol specified by Sy is displayed along with the caret.
                *
                *                                          The four numbers shall correspond to the differences in default user space between the left, top, right, and bottom coordinates of Rect and those of the caret, respectively. 
                *                                          Each value shall be greater than or equal to 0.
                *                                          The sum of the top and bottom differences shall be less than the height of Rect, and the sum of the left and right differences shall be less than the width of Rect.
                *
                *          Sy              name            (Optional) A name specifying a symbol that shall be associated with the caret:
                *
                *                                          P       A new paragraph symbol(¶) should be associated with the caret.
                *
                *                                          None    No symbol should be associated with the caret.
                *
                *                                          Default value: None.
                */

                /*12.5.6.12 Rubber Stamp Annotations
                *
                *A rubber stamp annotation (PDF 1.3) displays text or graphics intended to look as if they were stamped on the page with a rubber stamp. When opened, it shall display a pop-up window containing the text of the associated note. 
                *Table 181 shows the annotation dictionary entries specific to this type of annotation.
                *
                *Table 181 - Additional entries specific to a rubber stamp annotation
                *
                *          [Key]           [Type]          [Value]
                *
                *          Subtype         name            (Required) The type of annotation that this dictionary describes; shall be Stamp for a rubber stamp annotation.
                *
                *          Name            name            (Optional) The name of an icon that shall be used in displaying the annotation. 
                *                                          Conforming readers shall provide predefined icon appearances for at least the following standard names:
                *
                *                                          Approved, Experimental, NotApproved, AsIs, Expired , NotForPublicRelease, Confidential, Final, Sold, Departmental, ForComment, TopSecret, Draft, ForPublicRelease
                *
                *                                          Additional names may be supported as well.
                *                                          Default value: Draft.
                *
                *                                          The annotation dictionary’s AP entry, if present, shall take precedence over the Name entry; see Table 168 and 12.5.5, “Appearance Streams.”
                */

                /*12.5.6.13 Ink Annotations
                *
                *An ink annotation (PDF 1.3) represents a freehand “scribble” composed of one or more disjoint paths. 
                *When opened, it shall display a pop-up window containing the text of the associated note. 
                *Table 182 shows the annotation dictionary entries specific to this type of annotation.
                *
                *Table 182 - Additional entries specific to an ink annotation
                *
                *          [Key]               [Type]              [Value]
                *
                *          Subtype             name                (Required) The type of annotation that this dictionary describes; shall be Ink for an ink annotation.
                *
                *          InkList             array               (Required) An array of n arrays, each representing a stroked path. 
                *                                                  Each array shall be a series of alternating horizontal and vertical coordinates in default user space, specifying points along the path. 
                *                                                  When drawn, the points shall be connected by straight lines or curves in an implementation-dependent way.
                *
                *          BS                  dictionary          (Optional) A border style dictionary (see Table 166) specifying the line width and dash pattern that shall be used in drawing the paths.
                *
                *                                                  The annotation dictionary’s AP entry, if present, shall take precedence over the InkList and BS entries; see Table 168 and 12.5.5, “Appearance Streams.”
                */

                /*12.5.6.14 Pop-up Annotations
                *
                *A pop-up annotation (PDF 1.3) displays text in a pop-up window for entry and editing. 
                *It shall not appear alone but is associated with a markup annotation, its parent annotation, and shall be used for editing the parent’s text. 
                *It shall have no appearance stream or associated actions of its own and shall be identified by the Popup entry in the parent’s annotation dictionary (see Table 174). 
                *Table 183 shows the annotation dictionary entries specific to this type of annotation.
                *
                *Table 183 - Additional entries specific to a pop-up annotation
                *
                *          [Key]               [Type]              [Value]
                *
                *          Subtype             name                (Required) The type of annotation that this dictionary describes; shallbe Popup for a pop-up annotation.
                *
                *          Parent              dictionary          (Optional; shall be an indirect reference) The parent annotation with which this pop-up annotation shall be associated.
                *
                *                                                  If this entry is present, the parent annotation’s Contents, M, C, and Tentries(see Table 168) shall override those of the pop-up annotation itself.
                *
                *          Open                boolean             (Optional) A flag specifying whether the pop-up annotation shallinitially be displayed open. 
                *                                                  Default value: false (closed).
                */

                /*12.5.6.15 File Attachment Annotations
                *
                *A file attachment annotation (PDF 1.3) contains a reference to a file, which typically shall be embedded in the PDF file (see 7.11.4, “Embedded File Streams”).
                *
                *NOTE      A table of data might use a file attachment annotation to link to a spreadsheet file based on that data; activating the annotation extracts the embedded file and gives the user an opportunity to view it or store it in the file system.
                *          Table 184 shows the annotation dictionary entries specific to this type of annotation.
                *
                *The Contents entry of the annotation dictionary may specify descriptive text relating to the attached file. 
                *Conforming readers shall use this entry rather than the optional Desc entry(PDF 1.6) in the file specification dictionary(see Table 44) identified by the annotation’s FS entry.
                *
                *Table 184 - Additional entries specific to a file attachment annotation
                *
                *              [Key]               [Type]                  [Value]
                *
                *              Subtype             name                    (Required) The type of annotation that this dictionary describes; shallbe FileAttachment for a file attachment annotation.
                *
                *              FS                  file specification      (Required) The file associated with this annotation.
                *
                *              Name                name                    (Optional) The name of an icon that shall be used in displaying the annotation. 
                *                                                          Conforming readers shall provide predefined icon appearances for at least the following standard names:
                *
                *                                                          GraphPushPin
                *
                *                                                          PaperclipTag
                *
                *                                                          Additional names may be supported as well.
                *                                                          Default value: PushPin.
                *
                *                                                          The annotation dictionary’s AP entry, if present, shall take precedence over the Name entry; see Table 168 and 12.5.5, “Appearance Streams.”
                */

                /*12.5.6.16 Sound Annotations
                *
                *A sound annotation (PDF 1.2) shall analogous to a text annotation except that instead of a text note, it contains sound recorded from the computer’s microphone or imported from a file. 
                *When the annotation is activated, the sound shall be played. The annotation shall behave like a text annotation in most ways, with a different icon (by default, a speaker) to indicate that it represents a sound. 
                *Table 185 shows the annotation dictionary entries specific to this type of annotation. Sound objects are discussed in 13.3, “Sounds.”
                *
                *Table 185 - Additional entries specific to a sound annotation
                *
                *          [Key]               [Type]              [Value]
                *
                *          Subtype             name                (Required) The type of annotation that this dictionary describes; shall be Sound for a sound annotation.
                *
                *          Sound               stream              (Required) A sound object defining the sound that shall be played when the annotation is activated (see 13.3, “Sounds”).
                *
                *          Name                name                (Optional) The name of an icon that shall be used in displaying the annotation. 
                *                                                  Conforming readers shall provide predefined icon appearances for at least the standard names Speaker and Mic. 
                *                                                  Additional names may be supported as well. 
                *                                                  Default value: Speaker.
                *                                                  The annotation dictionary’s AP entry, if present, shall take precedence over the Name entry; see Table 168 and 12.5.5, “Appearance Streams.”
                */

                /*12.5.6.17 Movie Annotations
                *
                *A movie annotation (PDF 1.2) contains animated graphics and sound to be presented on the computer screen and through the speakers. 
                *When the annotation is activated, the movie shall be played. 
                *Table 186 shows the annotation dictionary entries specific to this type of annotation. Movies are discussed in 13.4, “Movies.”
                *
                *Table 186 - Additional entries specific to a movie annotation
                *
                *          [Key]               [Type]              [Value]
                *
                *          Subtype             name                (Required) The type of annotation that this dictionary describes; shall be Movie for a movie annotation.
                *
                *          T                   text string         (Optional) The title of the movie annotation. Movie actions (12.6.4.9, “Movie Actions”) may use this title to reference the movie annotation.
                *
                *          Movie               dictionary          (Required) A movie dictionary that shall describe the movie’s static characteristics (see 13.4, “Movies”).
                *
                *          A                   boolean or          (Optional) A flag or dictionary specifying whether and how to play the movie when the annotation is activated. 
                *                              dictionary          If this value is a dictionary, it shall be a movie activation dictionary (see 13.4, “Movies”) specifying how to play the movie. 
                *                                                  If the value is the boolean true, the movie shall be played using default activation parameters. 
                *                                                  If the value is false, the movie shall not be played. 
                *                                                  Default value: true.
                *
                */
                
                /*12.5.6.18 Screen Annotations
                *
                *A screen annotation (PDF 1.5) specifies a region of a page upon which media clips may be played. 
                *It also serves as an object from which actions can be triggered. 12.6.4.13, “Rendition Actions” discusses the relationship between screen annotations and rendition actions. 
                *Table 187 shows the annotation dictionary entries specific to this type of annotation.
                *
                *Table 187 - Additional entries specific to a screen annotation
                *
                *          [Key]               [Type]              [Value]
                *
                *          Subtype             name                (Required) The type of annotation that this dictionary describes; shall be Screen for a screen annotation.
                *
                *          T                   text string         (Optional) The title of the screen annotation.
                *
                *          MK                  dictionary          (Optional) An appearance characteristics dictionary (see Table 189). 
                *                                                  The I entry of this dictionary provides the icon used in generating the appearance referred to by the screen annotation’s AP entry.
                *
                *          A                   dictionary          (Optional; PDF 1.1) An action that shall be performed when the annotation is activated (see 12.6, “Actions”).
                *
                *          AA                  dictionary          (Optional; PDF 1.2) An additional-actions dictionary defining the screen annotation’s behaviour in response to various trigger events (see 12.6.3, “Trigger Events”).
                *
                *In addition to the entries in Table 187, screen annotations may use the common entries in the annotation dictionary (see Table 164) in the following ways:
                *
                *  •   The P entry shall be used for a screen annotation referenced by a rendition action.
                *      It shall reference a valid page object, and the annotation shall be present in the page’s Annots array for the action to be valid.
                *
                *  •   The AP entry refers to an appearance dictionary(see Table 168) whose normal appearance provides the visual appearance for a screen annotation that shall be used for printing and default display when a media clip is not being played.
                *      If AP is not present, the screen annotation shall not have a default visual appearance and shall not be printed.
                */

                /*12.5.6.19 Widget Annotations
                *
                *Interactive forms (see 12.7, “Interactive Forms”) use widget annotations (PDF 1.2) to represent the appearance of fields and to manage user interactions. 
                *As a convenience, when a field has only a single associated widget annotation, the contents of the field dictionary (12.7.3, “Field Dictionaries”) and the annotation dictionary may be merged into a single dictionary containing entries that pertain to both a field and an annotation.
                *
                *NOTE          This presents no ambiguity, since the contents of the two kinds of dictionaries do not conflict.
                *
                *Table 188 shows the annotation dictionary entries specific to this type of annotation; interactive forms and fields are discussed at length in 12.7, “Interactive Forms.”
                *
                *Table 188 - Additional entris specific to a widget annotation
                *
                *              [Key]                   [Type]                      [Value]
                *
                *              Subtype                 name                        (Required) The type of annotation that this dictionary describes; shall be Widget for a widget annotation.
                *
                *              H                       name                        (Optional) The annotation’s highlighting mode, the visual effect that shall be used when the mouse button is pressed or held down inside its active area:
                *
                *                                                                  N   (None) No highlighting.
                *                                                                  I   (Invert) Invert the contents of the annotation rectangle.
                *                                                                  O   (Outline) Invert the annotation’s border.
                *                                                                  P   (Push) Display the annotation’s down appearance, if any(see 12.5.5, “Appearance Streams”).If no down appearance is defined, the contents of the annotation rectangle shall be offset to appear as if it were being pushed below the surface of the page.
                *                                                                  T   (Toggle) Same as P (which is preferred).
                *                                              
                *                                                                  A highlighting mode other than P shall override any down appearance defined for the annotation.
                *                                                                  Default value: I.
                *
                *              MK                      dictionary                  (Optional) An appearance characteristics dictionary (see Table 189) that shall be used in constructing a dynamic appearance stream specifying the annotation’s visual presentation on the page.
                *                                                                  The name MK for this entry is of historical significance only and has no direct meaning.
                *
                *              A                       dictionary                  (Optional; PDF 1.1) An action that shall be performed when the annotation is activated (see 12.6, “Actions”).
                *
                *              AA                      dictionary                  (Optional; PDF 1.2) An additional-actions dictionary defining the annotation’s behaviour in response to various trigger events (see 12.6.3, “Trigger Events”).
                *
                *              BS                      dictionary                  (Optional; PDF 1.2) A border style dictionary (see Table 166) specifying the width and dash pattern that shall be used in drawing the annotation’s border.
                *                                                                  The annotation dictionary’s AP entry, if present, shall take precedence over the L and BS entries; see Table 168 and 12.5.5, “Appearance Streams.”
                *
                *              Parent                  dictionary                  (Required if this widget annotation is one of multiple children in a field; absent otherwise) An indirect reference to the widget annotation’s parent field. 
                *                                                                  A widget annotation may have at most one parent; that is, it can be included in the Kids array of at most one field.
                *
                *The MK entry may be used to provide an appearance characteristics dictionary containing additional information for constructing the annotation’s appearance stream. Table 189 shows the contents of this dictionary.
                *
                *Table 189 - Entries in an appearance characteristics dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          R                   integer             (Optional) The number of degrees by which the widget annotation shall be rotated counterclockwise relative to the page. 
                *                                                  The value shall be a multiple of 90. 
                *                                                  Default value: 0.
                *
                *          BC                  array               (Optional) An array of numbers that shall be in the range 0.0 to 1.0 specifying the colour of the widget annotation’s border. 
                *                                                  The number of array elements determines the colour space in which the colour shall bedefined:
                *
                *                                                  0   No colour; transparent
                *
                *                                                  1   DeviceGray
                *
                *                                                  3   DeviceRGB
                *
                *                                                  4   DeviceCMYK
                *
                *           BG                 array               (Optional) An array of numbers that shall be in the range 0.0 to 1.0 specifying the colour of the widget annotation’s background. 
                *                                                  The number of array elements shall determine the colour space, as described for BC.
                *
                *           CA                 text string         (Optional; button fields only) The widget annotation’s normal caption, which shall be displayed when it is not interacting with the user.
                *
                *                                                  Unlike the remaining entries listed in this Table, which apply only to widget annotations associated with pushbutton fields(see Pushbuttons in 12.7.4.2, “Button Fields”), 
                *                                                  the CA entry may be used with any type of button field, including check boxes(see Check Boxes in 12.7.4.2, “Button Fields”) and radio buttons(Radio Buttons in 12.7.4.2, “Button Fields”).
                *
                *           RC                 text string         (Optional; pushbutton fields only) The widget annotation’s rollover caption, which shall be displayed when the user rolls the cursor into its active area without pressing the mouse button.
                *
                *           AC                 text string         (Optional; pushbutton fields only) The widget annotation’s alternate (down) caption, which shall be displayed when the mouse button is pressed within its active area.
                *
                *           I                  stream              (Optional; pushbutton fields only; shall be an indirect reference) A form XObject defining the widget annotation’s normal icon, which shall be displayed when it is not interacting with the user.
                *
                *           RI                 stream              (Optional; pushbutton fields only; shall be an indirect reference) A form XObject defining the widget annotation’s rollover icon, which shall be displayed when the user rolls the cursor into its active area without pressing the mouse button.
                *
                *           IX                 stream              (Optional; pushbutton fields only; shall be an indirect reference) A form XObject defining the widget annotation’s alternate (down) icon, which shall be displayed when the mouse button is pressed within its active area.
                *
                *           IF                 dictionary          (Optional; pushbutton fields only) An icon fit dictionary (see Table 247) specifying how the widget annotation’s icon shall be displayed within its annotation rectangle. If present, the icon fit dictionary shall apply to all of the annotation’s icons (normal, rollover, and alternate).
                *
                *           TP                 integer             (Optional; pushbutton fields only) A code indicating where to position the text of the widget annotation’s caption relative to its icon:
                *
                *                                                  0   No icon; caption only
                *
                *                                                  1   No caption; icon only
                *
                *                                                  2   Caption below the icon
                *
                *                                                  3   Caption above the icon
                *
                *                                                  4   Caption to the right of the icon
                *
                *                                                  5   Caption to the left of the icon
                *
                *                                                  6   Caption overlaid directly on the icon
                *
                *                                                  Default value: 0.
                */

                /*12.5.6.20 Printer's Mark Annotations
                *
                *A printer’s mark annotation (PDF 1.4) represents a graphic symbol, such as a registration target, colour bar, or cut mark, that may be added to a page to assist production personnel in identifying components of a multiple-plate job and maintaining consistent output during production. 
                *See 14.11.3, “Printer’s Marks,” for further discussion.
                */

                /*12.5.6.21 Trap Network Annotations
                *
                *A trap network annotation(PDF 1.3) may be used to define the trapping characteristics for a page of a PDF document.
                *
                *NOTE      Trapping is the process of adding marks to a page along colour boundaries to avoid unwanted visual artifacts resulting from misregistration of colorants when the page is printed.
                *
                *A page shall have no more than one trap network annotation, whose Subtype entry has the value TrapNet and which shall always be the last element in the page object’s Annots array(see 7.7.3.3, “Page Objects”).See 14.11.6, “Trapping Support,” for further discussion.
                */

                /*12.5.6.22 Watermark Annotations
                *
                *A watermark annotation(PDF 1.6) shall be used to represent graphics that shall be printed at a fixed size and position on a page, regardless of the dimensions of the printed page.
                *The FixedPrint entry of a watermark annotation dictionary(see Table 190) shall be a dictionary that contains values for specifying the size and position of the annotation(see Table 191).
                *
                *Watermark annotations shall have no pop-up window or other interactive elements. When displaying a watermark annotation on-screen, conforming readers shall use the dimensions of the media box as the page size so that the scroll and zoom behaviour is the same as for other annotations.
                *
                *NOTE      Since many printing devices have non printable margins, such margins should be taken into consideration when positioning watermark annotations near the edge of a page.
                *
                *Table 190 - Additional entries specific to a watermark annotation
                *
                *          [Key]               [Type]          [Value]
                *
                *          Subtype             name            (Required) The type of annotation that this dictionary describes; shall be Watermark for a watermark annotation.
                *
                *          FixedPrint          dictionary      (Optional) A fixed print dictionary (see Table 191) that specifies how this annotation shall be drawn relative to the dimensions of the target media. 
                *                                              If this entry is not present, the annotation shall be drawn without any special consideration for the dimensions of the target media.
                *
                *                                              If the dimensions of the target media are not known at the time of drawing, drawing shall be done relative to the dimensions specified by the page’s MediaBox entry(see Table 30).
                *
                *Table 191 - Entries in a fixed print dictionary
                *
                *          [Key]           [Type]              [Value]
                *
                *          Type            name                (Required) Shall be FixedPrint
                *
                *          Matrix          array               (Optional) The matrix used to transform the annotation’s rectangle before rendering.
                *
                *                                              Default value: the identity matrix[1 0 0 1 0 0].
                *
                *                                              When positioning content near the edge of a page, this entry should be used to provide a reasonable offset to allow for nonburnable margins.
                *
                *          H               number              (Optional) The amount to translate the associated content horizontally, as a percentage of the width of the target media (or if unknown, the width of the page’s MediaBox). 
                *                                              1.0 represents 100% and 0.0 represents 0%. 
                *                                              Negative values should not be used, since they may cause content to be drawn off the page.
                *                                              Default value: 0.
                *
                *          V               number              (Optional) The amount to translate the associated content vertically, as a percentage of the height of the target media (or if unknown, the height of the page’s MediaBox). 
                *                                              1.0 represents 100% and 0.0 represents 0%. 
                *                                              Negative values should not be used, since they may cause content to be drawn off the page.
                *                                              Default value: 0.
                *
                *When rendering a watermark annotation with a FixedPrint entry, the following behaviour shall occur:
                *
                *  •   The annotation’s rectangle(as specified by its Rect entry) shall be translated to the origin and transformed by the Matrix entry of its FixedPrint dictionary to produce a quadrilateral with arbitrary orientation.
                *
                *  •   The transformed annotation rectangle shall be defined as the smallest upright rectangle that encompasses this quadrilateral; it shall be used in place of the annotation rectangle referred to in steps 2 and 3 of "Algorithm: Appearance streams".
                *
                *In addition, given a matrix B that maps a scaled and rotated page into the default user space, a new matrix shall be computed that cancels out B and translates the origin of the printed page to the origin of the default user space. 
                *This transformation shall be applied to ensure the correct scaling and alignment.
                *
                *EXAMPLE           The following example shows a watermark annotation that prints a text string one inch from the left and one inch from the top of the printed page.
                *
                *                  8 0 obj                     % Watermark appearance
                *                      <<
                *                          / Length...
                *                          / Subtype / Form
                *                          / Resources...
                *                          / BBox...
                *                      >>
                *                  stream
                *                  ...
                *                      BT
                *                      / F1 1 Tf
                *                      36 0 0 36 0 - 36 Tm
                *                      (Do Not Build) Tx
                *                      ET
                *                      ...
                *                  endstream
                *                  endobj
                *                  9 0 obj                     % Watermark annotation
                *                      <<
                *                          / Rect...
                *                          / Type / Annot
                *                          / Subtype / Watermark
                *                          / FixedPrint 10 0 R
                *                          / AP <</ N 8 0 R >>
                *                      >>
                *                  % in the page dictionary
                *                          / Annots[9 0 R]
                *                  10 0 obj                    % Fixed print dictionary
                *                      <<
                *                          / Type / FixedPrint
                *                          / Matrix[1 0 0 1 72 - 72] % Translate one inch right and one inch down
                *                          / H 0
                *                          / V 1.0 % Translate the full height of the page vertically
                *                      >>
                *                  endobj
                *
                *In situations other than the usual case where the PDF page size equals the printed page size, watermark annotations with a FixedPrint entry shall be printed in the following manner:
                *
                *  •   When page tiling is selected in a conforming reader(that is, a single PDF page is printed on multiple pages), the annotations shall be printed at the specified size and position on each page to ensure that any enclosed content is present and legible on each printed page.
                *
                *  •   When n-up printing is selected (that is, multiple PDF pages are printed on a single page), the annotations shall be printed at the specified size and shall be positioned as if the dimensions of the printed page were limited to a single portion of the page. 
                *      This ensures that any enclosed content does not overlap content from other pages, thus rendering it illegible.
                */

                /*12.5.6.23 Redaction Annotations
                *
                *A redaction annotation(PDF 1.7) identifies content that is intended to be removed from the document.
                *The intent of redaction annotations is to enable the following process:
                *
                *  a)  Content identification. A user applies redact annotations that specify the pieces or regions of content that should be removed.
                *      Up until the next step is performed, the user can see, move and redefine these annotations.
                *
                *  b)  Content removal. The user instructs the viewer application to apply the redact annotations, after which the content in the area specified by the redact annotations is removed. 
                *      In the removed content’s place, some marking appears to indicate the area has been redacted. Also, the redact annotations are removed from the PDF document.
                *
                *Redaction annotations provide a mechanism for the first step in the redaction process(content identification).
                *This allows content to be marked for redaction in a non - destructive way, thus enabling a review process for evaluating potential redactions prior to removing the specified content.
                *
                *Redaction annotations shall provide enough information to be used in the second phase of the redaction process(content removal).
                *This phase is application - specific and requires the conforming reader to remove all content identified by the redaction annotation, as well as the annotation itself.
                *
                *Conforming readers that support redaction annotations shall provide a mechanism for applying content removal, and they shall remove all traces of the specified content.
                *If a portion of an image is contained in a redaction region, that portion of the image data shall be destroyed; clipping or image masks shall not be used to hide that data. 
                *Such conforming readers shall also be diligent in their consideration of all content that can exist in a PDF document, including XML Forms Architecture(XFA) content and Extensible Metadata Platform(XMP) content.
                *
                *Table 192 - Additional entries specific to a redaction annotation
                *
                *          [Key]           [Type]              [Value]
                *
                *          Subtype         name                (Required) The type of annotation that this dictionary describes; shallbe Redact for a redaction annotation.
                *
                *          QuadPoints      array               (Optional) An array of 8 x n numbers specifying the coordinates of n quadrilaterals in default user space, as described in Table 175 for text markup annotations. 
                *                                              If present, these quadrilaterals denote the content region that is intended to be removed. 
                *                                              If this entry is not present, the Rect entry denotes the content region that is intended to be removed.
                *
                *          IC              array               (Optional) An array of three numbers in the range 0.0 to 1.0 specifying the components, in the DeviceRGB colour space, of the interior colour with which to fill the redacted region after the affected content has been removed. 
                *                                              If this entry is absent, the interior of the redaction region is left transparent. 
                *                                              This entry is ignored if the ROentry is present.
                *
                *          RO              stream              (Optional) A form XObject specifying the overlay appearance for this redaction annotation. 
                *                                              After this redaction is applied and the affected content has been removed, the overlay appearance should be drawn such that its origin lines up with the lower-left corner of the annotation rectangle. 
                *                                              This form XObject is not necessarily related to other annotation appearances, and may or may not be present in the APdictionary. 
                *                                              This entry takes precedence over the IC, OverlayText, DA, and Q entries.
                *
                *          OverlayText     text string         (Optional) A text string specifying the overlay text that should be drawn over the redacted region after the affected content has been removed. 
                *                                              This entry is ignored if the RO entry is present.
                *
                *          Repeat          boolean             (Optional) If true, then the text specified by OverlayText should be repeated to fill the redacted region after the affected content has been removed. 
                *                                              This entry is ignored if the RO entry is present. 
                *                                              Default value: false.
                *
                *          DA              byte string         (Required if OverlayText is present, ignored otherwise) The appearance string to be used in formatting the overlay text when it is drawn after the affected content has been removed (see 12.7.3.3, “Variable Text”). 
                *                                              This entry is ignored if the RO entry is present.
                *
                *          Q               integer             (Optional) A code specifying the form of quadding (justification) to be used in laying out the overlay text:
                *
                *                                              0 Left - justified
                *
                *                                              1 Centered
                *
                *                                              2 Right - justified
                *
                *                                              This entry is ignored if the RO entry is present.Default value: 0(left - justified).
                */


        }

        //12.6 Actions
        public class Actions
        {

            /*12.6.1 General
            *
            *In addition to jumping to a destination in the document, an annotation or outline item may specify an action(PDF 1.1) to perform, such as launching an application, playing a sound, changing an annotation’s appearance state.
            *The optional A entry in the annotation or outline item dictionary(see Tables 168 and 153) specifies an action performed when the annotation or outline item is activated; in PDF 1.2, a variety of other circumstances may trigger an action as well(see 12.6.3, “Trigger Events”). 
            *In addition, the optional OpenAction entry in a document’s catalogue(7.7.2, “Document Catalog”) may specify an action that shall be performed when the document is opened.
            *PDF includes a wide variety of standard action types, described in detail in 12.6.4, “Action Types.”
            */

            /*12.6.2 Action Dictionaries
            *
                *An action dictionary defines the characteristics and behaviour of an action. 
                *Table 193 shows the required and optional entries that are common to all action dictionaries. 
                *The dictionary may contain additional entries specific to a particular action type; see the descriptions of individual action types in 12.6.4, “Action Types,” for details.
                *
                *Table 193 - Entries common to all action dictionaries
                *
                *      [Key]           [Type]                  [Value]
                *
                *      Type            name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Action for an action dictionary.
                *
                *      S               name                    (Required) The type of action that this dictionary describes; see Table 194 for specific values.
                *
                *      Next            dictionary or array     (Optional; PDF 1.2) The next action or sequence of actions that shall be performed after the action represented by this dictionary. 
                *                                              The value is either a single action dictionary or an array of action dictionaries that shall be performed in order; see the Note for further discussion.
                *
                *NOTE 1    The action dictionary’s Next entry (PDF 1.2) allows sequences of actions to be chained together. 
                *          For example, the effect of clicking a link annotation with the mouse might be to play a sound, jump to a new page, and start up a movie. 
                *          Note that the Next entry is not restricted to a single action but may contain an array of actions, each of which in turn may have a Next entry of its own. 
                *          The actions may thus form a tree instead of a simple linked list. 
                *          Actions within each Next array are executed in order, each followed in turn by any actions specified in its Next entry, and so on recursively. 
                *          Conforming readers should attempt to provide reasonable behavior in anomalous situations. 
                *          For example, self-referential actions should not be executed more than once, and actions that close the document or otherwise render the next action impossible should terminate the execution sequence. 
                *          Applications should also provide some mechanism for the user to interrupt and manually terminate a sequence of actions.
                *
                *PDF 1.5 introduces transition actions, which allow the control of drawing during a sequence of actions; see 12.6.4.14, “Transition Actions.”
                *
                *NOTE 2    No action should modify its own action dictionary or any other in the action tree in which it resides. 
                *          The effect of such modification on subsequent execution of actions in the tree is undefined.
                *
                */

            /*12.6.3 Trigger Events
            *
                *An annotation, page object, or (beginning with PDF 1.3) interactive form field may include an entry named AA that specifies an additional-actions dictionary (PDF 1.2) that extends the set of events that can trigger the execution of an action. 
                *In PDF 1.4, the document catalogue dictionary (see 7.7.2, “Document Catalog”) may also contain an AA entry for trigger events affecting the document as a whole. 
                *Tables 194 to 197 show the contents of this type of dictionary.
                *
                *PDF 1.5 introduces four trigger events in annotation’s additional-actions dictionary to support multimedia presentations:
                *
                *  •   The PO and PC entries have a similar function to the O and C entries in the page object’s additional-actions dictionary(see Table 194).
                *      However, associating these triggers with annotations allows annotation objects to be self - contained.
                *
                *EXAMPLE       Annotations containing such actions can be copied or moved between pages without requiring page open / close actions to be changed.
                *
                *  •   The PV and PI entries allow a distinction between pages that are open and pages that are visible. 
                *      At any one time, while more than one page may be visible, depending on the page layout.
                *
                *NOTE 1        For these trigger events, the values of the flags specified by the annotation’s F entry(see 12.5.3, “Annotation Flags”) have no bearing on whether a given trigger event occurs.
                *
                *Table 194 - Entries in an annotation's additional-actions dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              E                   dictionary          (Optional; PDF 1.2) An action that shall be performed when the cursor enters the annotation’s active area.
                *
                *              X                   dictionary          (Optional; PDF 1.2) An action that shall be performed when the cursor exits the annotation’s active area.
                *
                *              D                   dictionary          (Optional; PDF 1.2) An action that shall be performed when the mouse button is pressed inside the annotation’s active area.
                *
                *              U                   dictionary          (Optional; PDF 1.2) An action that shall be performed when the mouse button is released inside the annotation’s active area.
                *
                *                                                      For backward compatibility, the A entry in an annotation dictionary, if present, takes precedence over this entry (see Table 168).
                *
                *              Fo                  dictionary          (Optional; PDF 1.2; widget annotations only) An action that shall be performed when the annotation receives the input focus.
                *
                *              BI                  dictionary          (Optional; PDF 1.2; widget annotations only) (Uppercase B, lowercase L) An action that shall be performed when the annotation loses the input focus.
                *
                *              PO                  dictionary          (Optional; PDF 1.5) An action that shall be performed when the page containing the annotation is opened.
                *
                *                                                      EXAMPLE 1       When the user navigates to it from the next or previous page or by means of a link annotation or outline item.
                *
                *                                                      The action shall be executed after the O action in the page’s additional-actions dictionary (see Table 195) and the OpenAction entry in the document Catalog (see Table 28), if such actions are present.
                *
                *              PC                  dictionary          (Optional; PDF 1.5) An action that shall be performed when the page containing the annotation is closed.
                *
                *                                                      EXAMPLE 2       When the user navigates to the next or previous page, or follows a link annotation or outline item.
                *
                *                                                      The action shall be executed before the C action in the page’s additional-actions dictionary (see Table 195), if present.
                *
                *              PV                  dictionary          (Optional; PDF 1.5) An action that shall be performed when the page containing the annotation becomes visible.
                *
                *              PI                  dictionary          (Optional; PDF 1.5) An action that shall be performed when the page containing the annotation is no longer visible in the conforming reader’s user interface.
                *
                *
                *Table 195 - Entries in a page object's additional-actions dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              O                   dictionary          (Optional; PDF 1.2) An action that shall be performed when the page is opened (for example, when the user navigates to it from the next or previous page or by means of a link annotation or outline item). 
                *                                                      This action is independent of any that may be defined by the OpenAction entry in the document Catalog (see 7.7.2, “Document Catalog”) and shall be executed after such an action.
                *
                *              C                   dictionary          (Optional; PDF 1.2) An action that shall be performed when the page is closed (for example, when the user navigates to the next or previous page or follows a link annotation or an outline item). 
                *                                                      This action applies to the page being closed and shall be executed before any other page is opened.
                *
                *Table 196 - Entries in a form field's additional-actions dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              K                   dictionary          (Optional; PDF 1.3) A JavaScript action that shall be performed when the user modifies a character in a text field or combo box or modifies the selection in a scrollable list box. 
                *                                                      This action may check the added text for validity and reject or modify it.
                *
                *              F                   dictionary          (Optional; PDF 1.3) A JavaScript action that shall be performed before the field is formatted to display its value. 
                *                                                      This action may modify the field’s value before formatting.
                *
                *              V                   dictionary          (Optional; PDF 1.3) A JavaScript action that shall be performed when the field’s value is changed. 
                *                                                      This action may check the new value for validity. (The name V stands for “validate.”)
                *
                *              C                   dictionary          (Optional; PDF 1.3) A JavaScript action that shall be performed to recalculate the value of this field when that of another field changes. (The name C stands for “calculate.”) 
                *                                                      The order in which the document’s fields are recalculated shall be defined by the CO entry in the interactive form dictionary (see 12.7.2, “Interactive Form Dictionary”).
                *
                *Table 197 - Entries in the document catalog's additional-actions dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              WC                  dictionary          (Optional; PDF 1.4) A JavaScript action that shall be performed before closing a document. (The name WC stands for “will close.”)
                *
                *              WS                  dictionary          (Optional; PDF 1.4) A JavaScript action that shall be performed before saving a document. (The name WS stands for “will save.”)
                *
                *              DS                  dictionary          (Optional; PDF 1.4) A JavaScript action that shall be performed after saving a document. (The name DS stands for “did save.”)
                *
                *              WP                  dictionary          (Optional; PDF 1.4) A JavaScript action that shall be performed before printing a document. (The name WP stands for “will print.”)
                *
                *              DP                  dictionary          (Optional; PDF 1.4) A JavaScript action that shall be performed after printing a document. (The name DP stands for “did print.”)
                *
                *Conforming readers shall ensure the presence of such a device, or equivalent controls for simulating one, for the corresponding actions to be executed correctly. 
                *Mouse -related trigger events are subject to the following constraints:
                *
                *  •   An E(enter) event may occur only when the mouse button is up.
                *
                *  •   An X(exit) event may not occur without a preceding E event.
                *
                *  •   A U (up) event may not occur without a preceding E and D event.
                *
                *  •   In the case of overlapping or nested annotations, entering a second annotation’s active area causes an Xevent to occur for the first annotation.
                *
                *NOTE 2        The field-related trigger events K (keystroke), F(format), V(validate), and C(calculate) are not defined for button fields(see 12.7.4.2, “Button Fields”). 
                *              The effects of an action triggered by one of these events are limited only by the action itself and can occur outside the described scope of the event. 
                *              For example, even though the F event is used to trigger actions that format field values prior to display, it is possible for an action triggered by this event to perform a calculation or make any other modification to the document.
                *
                *              These field-related trigger events can occur either through user interaction or programmatically, such as in response to the NeedAppearances entry in the interactive form dictionary (see 12.7.2, “Interactive Form Dictionary”), importation of FDF data(12.7.7, “Forms Data Format”), or JavaScript actions(12.6.4.16, “JavaScript Actions”). 
                *              For example, the user’s modifying a field value can trigger a cascade of calculations and further formatting and validation for other fields in the document.
                */

            /*12.6.4 Action Types
            */

                /*12.6.4.1 General
                *
                *PDF supports the standard action types listed in Table 198.The following sub-clauses describe each of these types in detail.
                *
                *Table 198 - Action Types
                *
                *          [Action Type]               [Description]                                                               [Discussed in sub-clause]
                *
                *          GoTo                        Go to a destination in the current document.                                12.6.4.2, "Go-To Actions"
                *
                *          GoToR                       (“Go-to remote”) Go to a destination in another document.                   12.6.4.3, "Remote Go-To Actions"
                *
                *          GoToE                       (“Go-to embedded”; PDF 1.6) Go to a destination in an embedded file.        12.6.4.4, "Embedded Go-To Actions"
                *
                *          Launch                      Launch an application, usually to open a file.                              12.6.4.5, "Launch Actions"
                *
                *          Thread                      Begin reading an article thread.                                            12.6.4.6, "Thread Actions"
                *
                *          URI                         Resolve a uniform resource identifier.                                      12.6.4.7, "URI Actions"
                *
                *          Sound                       (PDF 1.2) Play a sound.                                                     12.6.4.8, "Sound Actions"
                *
                *          Movie                       (PDF 1.2) Play a movie.                                                     12.6.4.9, "Movie Actions"
                *
                *          Hide                        (PDF 1.2) Set an annotation's Hidden flag.                                  12.6.4.10, "Hide Actions"
                *
                *          Named                       (PDF 1.2) Execute an action predefined by the conforming reader.            12.6.4.11, "Named Actions"
                *
                *          SubmitForm                  (PDF 1.2) Send data to a uniform resource locator.                          12.7.5.2, "Submit-Form Action"
                *
                *          ResetForm                   (PDF 1.2) Set fields to their default values.                               12.7.5.3, "Reset-Form Action"
                *
                *          ImportData                  (PDF 1.2) Import field values from a file.                                  12.7.5.4, "Import-Data Action"
                *
                *          JavaScript                  (PDF 1.3) Execute a JavaScript script.                                      12.6.4.16, "JavaScript Actions"
                *
                *          SetOCGState                 (PDF 1.5) Set the states of optional content groups.                        12.6.4.12, "Set-OCG-State Actions"
                *
                *          Rendition                   (PDF 1.5) Controls the playing of multimedia content.                       12.6.4.13, "Rendition Actions"
                *
                *          Trans                       (PDF 1.5) Updates the display of a document, using a transition             12.6.4.14, "Transition Actions"
                *                                      dictionary.
                *
                *          GoTo3DView                  (PDF 1.6) Set the current view of a 3D annotation.                          12.6.4.15, "Go-To-3D-View Actions"
                *
                *
                *NOTE      The set-state action is considered obsolete and should not be used.
                */

                /*12.6.4.2 Go-To Actions
            *
            *A go-to action changes the view to a specified destination(page, location, and magnification factor). 
            *Table 199shows the action dictionary entries specific to this type of action.
            *
            *Table 199 - Additional entries specific to a go-to action
            *
            *          [Key]               [Type]              [Value]
            *
            *          S                   name                (Required) The type of action that this dictionary describes; shall be GoTofor a go-to action.
            *
            *          D                   name,               (Required) The destination to jump to (see 12.3.2, “Destinations”).
            *                              byte string,
            *                              or array
            *
            *NOTE          Specifying a go-to action in the A entry of a link annotation or outline item (see Table 173 and Table 153) has the same effect as specifying the destination directly with the Dest entry. 
            *              For example, the link annotation shown in the Example in 12.6.4.12, “Set-OCG-State Actions,” which uses a go-to action, has the same effect as the one in the following Example, which specifies the destination directly. 
            *              However, the go-to action is less compact and is not compatible with PDF 1.0; therefore, using a direct destination is preferable.
            *
            *EXAMPLE           93 0 obj
            *                      << / Type / Annot
            *                         / Subtype / Link
            *                         / Rect[71 717 190 734]
            *                         / Border[16 16 1]
            *                         / A << / Type / Action
            *                                / S / GoTo
            *                                / D[3 0 R / FitR –4 399 199 533]
            *                             >>
            *                      >>
            *                  endobj
            */

                /*12.6.4.3 Remote Go-To Actions
            *
            *A remote go-to action is similar to an ordinary go-to action but jumps to a destination in another PDF file instead of the current file. 
            *Table 200 shows the action dictionary entries specific to this type of action.
            *
            *NOTE      Remote go-to actions cannot be used with embedded files; see 12.6.4.4, “Embedded Go-To Actions.”
            *
            *Table 200 - Additional entries specific to a remote go-to action
            *
            *          [Key]               [Type]                      [Value]
            *
            *          S                   name                        (Required) The type of action that this dictionary describes; shall be GoToR for a remote go-to action.
            *
            *          F                   file specification          (Required) The file in which the destination shall be located.
            *
            *          D                   name,                       (Required) The destination to jump to (see 12.3.2, “Destinations”). 
            *                              byte string, or             If the value is an array defining an explicit destination (as described under 12.3.2.2, “Explicit Destinations”), its first element shall be a page number within the remote document rather than an indirect reference to a page object in the current document. 
            *                              array                       The first page shall benumbered 0.
            *                              
            *          NewWindow           boolean                     (Optional; PDF 1.2) A flag specifying whether to open the destination document in a new window. 
            *                                                          If this flag is false, the destination document replaces the current document in the same window. 
            *                                                          If this entry is absent, the conforming reader should behave in accordance with its preference.
            *                                                          
            */

                /*12.6.4.4 Embedded Go-To Actions
            *
            *An embedded go-to action (PDF 1.6) is similar to a remote go-to action but allows jumping to or from a PDF file that is embedded in another PDF file (see 7.11.4, “Embedded File Streams”). 
            *Embedded files may be associated with file attachment annotations (see 12.5.6.15, “File Attachment Annotations”) or with entries in the EmbeddedFiles name tree (see 7.7.4, “Name Dictionary”). 
            *Embedded files may in turn contain embedded files. Table 201 shows the action dictionary entries specific to embedded go-to actions.
            *
            *Embedded go-to actions provide a complete facility for linking between a file in a hierarchy of nested embedded files and another file in the same or different hierarchy.The following terminology shall be used:
            *
            *  •   The source is the document containing the embedded go - to action.
            *
            *  •   The target is the document in which the destination lives.
            *
            *  •   The T entry in the action dictionary is a target dictionary that locates the target in relation to the source, in much the same way that a relative path describes the physical relationship between two files in a file system.
            *      Target dictionaries may be nested recursively to specify one or more intermediate targets before reaching the final one.As the hierarchy is navigated, each intermediate target shall be referred to as the current document.
            *      Initially, the source is the current document.
            *
            *NOTE  It is an error for a target dictionary to have an infinite cycle (for example, one where a target dictionary refers to itself). 
            *      Conforming readers should attempt to detect such cases and refuse to execute the action if found.
            *
            *  •   A child document shall be one that is embedded within another PDF file.
            *
            *  •   The document in which a file is embedded shall be its parent.
            *
            *  •   A root document is one that is not embedded in another PDF file.The target and source may be contained in root documents or embedded documents.
            *
            *Table 201 - Additional entries specific to an embedded go-to action
            *
            *          [Key]               [Type]                  [Value]
            *
            *          S                   name                    (Required) The type of action that this dictionary describes; shall be GoToE for an embedded go-to action.
            *
            *          F                   file                    (Optional) The root document of the target relative to the root document of the source. 
            *                              specification           If this entry is absent, the source and target share the same root document.
            *                              
            *          D                   name,                   (Required) The destination in the target to jump to (see 12.3.2, “Destinations”).
            *                              byte string,
            *                              or array
            *
            *          NewWindow           boolean                 (Optional) If true, the destination document should be opened in a new window; if false, the destination document should replace the current document in the same window. 
            *                                                      If this entry is absent, the conforming reader should act according to its preference.
            *
            *          T                   dictionary              (Optional if F is present; otherwise required) A target dictionary (see Table 202) specifying path information to the target document. 
            *                                                      Each target dictionary specifies one element in the full path to the target and may have nested target dictionaries specifying additional elements.
            *
            *Table 202 - Entries specific to a target dictionary
            *
            *          [Key]               [Type]                  [Value]
            *
            *          R                   name                    (Required) Specifies the relationship between the current document and the target (which may be an intermediate target). 
            *                                                      Valid values are P (the target is the parent of the current document) and C (the target is a child of the current document).
            *
            *          N                   byte string             (Required if the value of R is C and the target is located in the EmbeddedFiles name tree; otherwise, it shall be absent) The name of the file in the EmbeddedFiles name tree.
            *
            *          P                   integer or              (Required if the value of R is C and the target is associated with a file attachment annotation; otherwise, it shall be absent) 
            *                              byte string             If the value is an integer, it specifies the page number (zero-based) in the current document containing the file attachment annotation. 
            *                                                      If the value is a string, it specifies a named destination in the current document that provides the page number of the file attachment annotation.
            *
            *          A                   integer or text         (Required if the value of R is C and the target is associated with a file attachment annotation; otherwise, it shall be absent) 
            *                              string                  If the value is an integer, it specifies the index (zero-based) of the annotation in the Annots array (see Table 30) of the page specified by P. 
            *                                                      If the value is a text string, it specifies the value of NM in the annotation dictionary (see Table 164).
            *
            *          T                   dictionary              (Optional) A target dictionary specifying additional path information to the target document. 
            *                                                      If this entry is absent, the current document is the target file containing the destination. 
            *
            *EXAMPLE       The following example illustrates several possible relationships between source and target. Each object shown is an action dictionary for an embedded go-to action.
            *
            *              1 0 obj                             % Link to a child
            *                  << / Type / Action
            *                     / S / GoToE
            *                     / D(Chapter 1)
            *                     / T << / R / C
            *                     / N(Embedded document) >>
            *                         >>
            *              endobj
            *              2 0 obj                             % Link to the parent
            *                  << / Type / Action
            *                     / S / GoToE
            *                     / D(Chapter 1)
            *                     / T << / R / P >>
            *                  >>
            *              endobj
            *              3 0 obj                             % Link to a sibling
            *                  << / Type / Action
            *                     / S / GoToE
            *                     / D(Chapter 1)
            *                     / T << / R / P
            *                            / T << / R / C
            *                                   / N(Another embedded document) >>
            *                          >>
            *                  >>
            *              endobj
            *              4 0 obj                             % Link to an embedded file in an external document
            *                  << / Type / Action
            *                     / S / GoToE
            *                     / D(Chapter 1)
            *                     / F(someFile.pdf)
            *                     / T << / R / C
            *                            / N(Embedded document) >>
            *                  >>
            *              endobj
            *              5 0 obj                             % Link from an embedded file to a normal file
            *                  << / Type / Action
            *                     / S / GoToE
            *                     / D(Chapter 1)
            *                     / F(someFile.pdf)
            *                  >>
            *              endobj
            *              6 0 obj                             % Link to a grandchild
            *                  << / Type / Action
            *                     / S / GoToE
            *                     / D(Chapter 1)
            *                     / T << / R / C
            *                            / N(Embedded document)
            *                            / T << / R / C
            *                                   / P(A destination name)
            *                                   / A(annotName)
            *                                >>
            *                          >>
            *                  >>
            *              endobj
            *              7 0 obj                             % Link to a niece/nephew through the source’s parent
            *                  << /Type /Action
            *                     / S / GoToE
            *                     / D(destination)
            *                     / T <</ R / P
            *                           / T << / R / C
            *                                  / N(Embedded document)
            *                                  / T << / R / C
            *                                         / P 3
            *                                         / A(annotName)
            *                                      >>
            *                               >>
            *                          >>
            *                  >>
            *              endobj
            */

                /*12.6.4.5 Launch Actions
            *
            *A launch action launches an application or opens or prints a document. Table 203 shows the action dictionary entries specific to this type of action.
            *
            *The optional Win, Mac, and Unix entries allow the action dictionary to include platform-specific parameters for launching the designated application.
            *If no such entry is present for the given platform, the F entry shall be used instead.Table 203 shows the platform - specific launch parameters for the Windows platform.
            *Parameters for the Mac OS and UNIX platforms are not yet defined at the time of publication.
            *
            *Table 203 - Additional entries specific to a launch action
            *
            *              [Key]               [Type]                  [Value]
            *
            *              S                   name                    (Required) The type of action that this dictionary describes; shall be Launch for a launch action.
            *
            *              F                   file speficication      (Required if none of the entries Win, Mac, or Unix is present) The application that shall be launched or the document that shall beopened or printed. 
            *                                                          If this entry is absent and the conforming readerdoes not understand any of the alternative entries, it shall do nothing.
            *
            *              Win                 dictionary              (Optional) A dictionary containing Windows-specific launch parameters (see Table 204).
            *
            *              Mac                 (undefined)             (Optional) Mac OS–specific launch parameters; not yet defined.
            *
            *              Unix                (undefined)             (Optional) UNIX-specific launch parameters; not yet defined.
            *
            *              NewWindow           boolean                 (Optional; PDF 1.2) A flag specifying whether to open the destination document in a new window. 
            *                                                          If this flag is false, the destination document replaces the current document in the same window. 
            *                                                          If this entry is absent, the conforming reader should behave in accordance with its current preference. 
            *                                                          This entry shall be ignored if the file designated by the F entry is not a PDF document.
            *
            *Table 204 - Entries in a Windows launch parameter dictionary
            *
            *              [Key]               [Type]                  [Value]
            *
            *              F                   byte string             (Required) The file name of the application that shall be launched or the document that shall be opened or printed, in standard Windows pathname format. If the name string includes a backslash character (\), the backslash shall itself be preceded by a backslash.
            *                                                          This value shall be a simple string; it is not a file specification.
            *
            *              D                   byte string             (Optional) A bye string specifying the default directory in standard DOS syntax.
            *
            *              O                   ASCII string            (Optional) An ASCII string specifying the operation to perform:
            *
            *                                                          open        Open a document.
            *      
            *                                                          print       Print a document.
            *
            *                                                          If the F entry designates an application instead of a document, this entry shall be ignored and the application shall be launched. 
            *                                                          Default value: open.
            *
            *              P                   byte string             (Optional) A parameter string that shall be passed to the application designated by the F entry. 
            *                                                          This entry shall be omitted if F designates a document.
            */

                /*12.6.4.6 Thread Actions
            *
            *A thread action jumps to a specified bead on an article thread (see 12.4.3, “Articles”), in either the current document or a different one. 
            *Table 205 shows the action dictionary entries specific to this type of action.
            *
            *Table 205 - Additional entries specific to a thread action
            *
            *              [Key]               [Type]                      [Value]
            *
            *              S                   name                        (Required) The type of action that this dictionary describes; shall be Thread for a thread action.
            *
            *              F                   file specification          (Optional) The file containing the thread. If this entry is absent, the thread is in the current file.
            *
            *              D                   dictionary, integer, or     (Required) The destination thread, specified in one of the following forms:
            *                                  text string
            *                                                              An indirect reference to a thread dictionary(see 12.4.3, “Articles”).
            *                                                              In this case, the thread shall be in the current file.
            *
            *                                                              The index of the thread within the Threads array of its document’s Catalog(see 7.7.2, “Document Catalog”). 
            *                                                              The first thread in the array has index 0.
            *
            *                                                              The title of the thread as specified in its thread information dictionary(see Table 160). 
            *                                                              If two or more threads have the same title, the one appearing first in the document Catalog’s Threads array shall beused.
            *                                  
            *              B                   dictionary or integer       (Optional) The bead in the destination thread, specified in one of the following forms:
            *
            *                                                              An indirect reference to a bead dictionary(see 12.4.3, “Articles”).
            *                                                              In this case, the thread shall be in the current file.
            *
            *                                                              The index of the bead within its thread. The first bead in a thread has index 0.
            */

                /*12.6.4.7 URI Actions
            *
            *A uniform resource identifier (URI) is a string that identifies (resolves to) a resource on the Internet—typically a file that is the destination of a hypertext link, although it may also resolve to a query or other entity. 
            *(URIs are described in Internet RFC 2396, Uniform Resource Identifiers (URI): Generic Syntax; see the Bibliography.)
            *
            *A URI action causes a URI to be resolved.Table 206 shows the action dictionary entries specific to this type of action.
            *
            *Table 206 - Additional entries specific to a URI action
            *
            *              [Key]               [Type]              [Value]
            *
            *              S                   name                (Required) The type of action that this dictionary describes; shall be URI for a URI action.
            *
            *              URI                 ASCII string        (Required) The uniform resource identifier to resolve, encoded in 7-bit ASCII.
            *
            *              IsMap               boolean             (Optional) A flag specifying whether to track the mouse position when the URI is resolved (see the discussion following this Table). 
            *                                                      Default value: false.
            *                                                      This entry applies only to actions triggered by the user’s clicking an annotation; it shall be ignored for actions associated with outline items or with a document’s OpenAction entry.
            *
            *If the IsMap flag is true and the user has triggered the URI action by clicking an annotation, the coordinates of the mouse position at the time the action has been triggered shall be transformed from device space to user space and then offset relative to the upper-left corner of the annotation rectangle 
            *(that is, the value of the Rectentry in the annotation with which the URI action is associated).
            *
            *EXAMPLE 1     If the mouse coordinates in user space are(xm, ym) and the annotation rectangle extends from(llx, lly) at the lower - left to(urx, ury) at the upper - right, the final coordinates(xf, yf) are as follows:
            *
            *              (Xf = Xm - llx)
            *              (Yf = URy - Ym)
            *
            *If the resulting coordinates (xf, yf) are fractional, they shall be rounded to the nearest integer values. 
            *They shall then be appended to the URI to be resolved, separated by COMMAS (2Ch) and preceded by a QUESTION MARK (3Fh), as shown in this example:
            *
            *EXAMPLE 2     http:*www.adobe.com/intro?100,200
            *
            *NOTE 1        To support URI actions, a PDF document’s Catalog(see 7.7.2, “Document Catalog”) may include a URI entry whose value is a URI dictionary.
            *              Only one entry shall be defined for such a dictionary(see Table 207).
            *
            *Table 207 - Entry in a URI dictionary
            *
            *              [Key]               [Type]              [Value]
            *
            *              Base                ASCII string        (Optional) The base URI that shall be used in resolving relative URI references. 
            *                                                      URI actions within the document may specify URIs in partial form, to be interpreted relative to this base address. 
            *                                                      If no base URI is specified, such partial URIs shall be interpreted relative to the location of the document itself. 
            *                                                      The use of this entry is parallel to that of the body element <BASE>, as described in the HTML 4.01 Specification (see the Bibliography).
            *
            *NOTE 2        The Base entry allows the URI of the document to be recorded in situations in which the document may be accessed out of context. 
            *              For example, if a document has been moved to a new location but contains relative links to other documents that have not been moved, the Base entry could be used to refer such links to the true location of the other documents, rather than that of the moved document.
            */

                /*12.6.4.8 Sound Actions
            *
            *A sound action (PDF 1.2) plays a sound through the computer’s speakers. Table 208 shows the action dictionary entries specific to this type of action. 
            *Sounds are discussed in 13.3, “Sounds.”
            *
            *Table 208 - Additional entries specific to a sound action
            *
            *              [Key]               [Type]              [Value]
            *
            *              S                   name                (Required) The type of action that this dictionary describes; shall be Sound for a sound action.
            *
            *              Sound               stream              (Required) A sound object defining the sound that shall be played (see 13.3, “Sounds”).
            *
            *              Volume              number              (Optional) The volume at which to play the sound, in the range −1.0 to 1.0. Default value: 1.0.
            *
            *              Synchronous         boolean             (Optional) A flag specifying whether to play the sound synchronously or asynchronously. 
            *                                                      If this flag is true, the conforming reader retains control, allowing no further user interaction other than canceling the sound, until the sound has been completely played. 
            *                                                      Default value: false.
            *
            *              Repeat              boolean             (Optional) A flag specifying whether to repeat the sound indefinitely. 
            *                                                      If this entry is present, the Synchronous entry shall be ignored. 
            *                                                      Default value: false.
            *
            *              Mix                 boolean             (Optional) A flag specifying whether to mix this sound with any other sound already playing. 
            *                                                      If this flag is false, any previously playing sound shall be stopped before starting this sound; this can be used to stop a repeating sound (see Repeat). 
            *                                                      Default value: false.
            */

                /*12.6.4.9 Movie Actions
            *
            *A movie action (PDF 1.2) can be used to play a movie in a floating window or within the annotation rectangle of a movie annotation (see 12.5.6.17, “Movie Annotations” and 13.4, “Movies”). 
            *The movie annotation shall be associated with the page that is the destination of the link annotation or outline item containing the movie action, or with the page object with which the action is associated.
            *
            *NOTE      A movie action by itself does not guarantee that the page the movie is on will be displayed before attempting to play the movie; such page change actions shall be done explicitly.
            *  
            *The contents of a movie action dictionary are identical to those of a movie activation dictionary(see Table 296), with the additional entries shown in Table 209.
            *The contents of the activation dictionary associated with the movie annotation provide the default values.
            *Any information specified in the movie action dictionary overrides these values.
            *
            *Table 209 - Additional entries specific to a movie action
            *
            *              [Key]               [Type]              [Value]
            *
            *              S                   name                (Required) The type of action that this dictionary describes; shall be Moviefor a movie action.
            *
            *              Annotation          dictionary          (Optional) An indirect reference to a movie annotation identifying the movie that shall be played.
            *
            *              T                   text string         (Optional) The title of a movie annotation identifying the movie that shall beplayed.
            *                                                      The dictionary shall include either an Annotation or a T entry but not both.
            *
            *              Operation           name                (Optional) The operation that shall be performed on the movie:
            *
            *                                                      PlayStart playing the movie, using the play mode specified by the dictionary’s Mode entry (see Table 296).
            *                                                      If the movie is currently paused, it shall be repositioned to the beginning before playing (or to the starting point specified by the dictionary’s Start entry, if present).
            *
            *                                                      Stop        Stop playing the movie.
            *
            *                                                      Pause       Pause a playing movie.
            *
            *                                                      Resume      Resume a paused movie.
            *
            *                                                      Default value: Play.
            */

                /*12.6.4.10 Hide Actions
            *
            *A hide action (PDF 1.2) hides or shows one or more annotations on the screen by setting or clearing their Hidden flags (see 12.5.3, “Annotation Flags”). 
            *This type of action can be used in combination with appearance streams and trigger events (Sections 12.5.5, “Appearance Streams,” and 12.6.3, “Trigger Events”) to display pop-up help information on the screen.
            *
            *NOTE      The E(enter) and X(exit) trigger events in an annotation’s additional-actions dictionary can be used to show and hide the annotation when the user rolls the cursor in and out of its active area on the page.
            *          This can be used to pop up a help label, or tool tip, describing the effect of clicking at that location on the page.
            *
            *Table 210 shows the action dictionary entries specific to this type of action.
            *
            *Table 210 - Additional entries specific to a hide action
            *
            *              [Key]               [Type]              [Value]
            *
            *              S                   name                (Required) The type of action that this dictionary describes; shall be Hide for a hide action.
            *
            *              T                   dictionary,         (Required) The annotation or annotations to be hidden or shown, shall bespecified in any of the following forms:
            *                                  text string, or     
            *                                  array               An indirect reference to an annotation dictionary
            *
            *                                                      A text string giving the fully qualified field name of an interactive form field whose associated widget annotation or annotations are to be affected (see 12.7.3.2, “Field Names”)
            *
            *                                                      An array of such dictionaries or text strings
            *
            *              H                   boolean             (Optional) A flag indicating whether to hide the annotation (true) or show it (false). 
            *                                                      Default value: true.
            */

                /*12.6.4.11 Named Actions
            *
            *Table 211 lists several named actions (PDF 1.2) that conforming readers shall support; further names may be added in the future.
            *
            *Table 211 - Named actions
            *
            *          [Name]              [Action]
            *
            *          NextPage            Go to the next page of the document.
            *
            *          PrevPage            Go to the previous page of the document.
            *
            *          FirstPage           Go to the first page of the document.
            *
            *          LastPage            Go to the last page of the document.
            *
            *NOTE      Conforming readers may support additional, nonstandard named actions, but any document using them is not portable. 
            *          If the viewer encounters a named action that is inappropriate for a viewing platform, or if the viewer does not recognize the name, it shall take no action.
            *
            *Table 212 shows the action dictionary entries specific to named actions.
            *
            *Table 212 - Additional entries specific to named actions
            *
            *              [Key]               [Type]              [Value]
            *
            *              S                   name                (Required) The type of action that this dictionary describes; shall be Named for a named action.
            *
            *              N                   name                (Required) The name of the action that shall be performed (see Table 211).
            */

                /*12.6.4.12 Set-OCG-State Actions
            *
            *A set-OCG-state action (PDF 1.5) sets the state of one or more optional content groups (see 8.11, “Optional Content”). 
            *Table 213 shows the action dictionary entries specific to this type of action.
            *
            *Table 213 - Additional entries specific to a set-OCG-state action
            *
            *              [Key]               [Type]              [Value]
            *
            *              S                   name                (Required) The type of action that this dictionary describes; shall be SetOCGState for a set-OCG-state action.
            *
            *              State               array               (Required) An array consisting of any number of sequences beginning with a name object (ON, OFF, or Toggle) followed by one or more optional content group dictionaries. 
            *                                                      The array elements shall be processed from left to right; each name shall be applied to the subsequent groups until the next name is encountered:
            *
            *                                                      ON      sets the state of subsequent groups to ON
            *
            *                                                      OFF     sets the state of subsequent groups to OFF
            *
            *                                                      Toggle reverses the state of subsequent groups.
            *
            *              PreserveRB          boolean             (Optional) If true, indicates that radio-button state relationships between optional content groups (as specified by the RBGroups entry in the current configuration dictionary; see Table 101) should be preserved when the states in the State array are applied. 
            *                                                      That is, if a group is set to ON (either by ON or Toggle) during processing of the State array, any other groups belonging to the same radio-button group shall be turned OFF. 
            *                                                      If a group is set to OFF, there is no effect on other groups.
            *                                                      If PreserveRB is false, radio - button state relationships, if any, shall beignored.
            *                                                      Default value: true.
            *
            *When a set-OCG-state action is performed, the State array shall be processed from left to right. 
            *Each name shall be applied to subsequent groups in the array until the next name is encountered, as shown in the following example.
            *
            *EXAMPLE 1         << / S / SetOCGState
            *                     / State[/ OFF 2 0 R 3 0 R / Toggle 16 0 R 19 0 R / ON 5 0 R]
            *                  >>
            *
            *A group may appear more than once in the State array; its state shall be set each time it is encountered, based on the most recent name. 
            *ON, OFF and Toggle sequences have no required order. More than one sequence in the array may contain the same name.
            *
            *EXAMPLE 2     If the array contained[/ OFF 1 0 R / Toggle 1 0 R], the group’s state would be ON after the action was performed.
            *
            *NOTE      While the specification allows a group to appear more than once in the State array, this is not intended to implement animation or any other sequential drawing operations. 
            *          PDF processing applications are free to accumulate all state changes and apply only the net changes simultaneously to all affected groups before redrawing.
            */

                /*12.6.4.13  Rendition Actions
            *
            *A rendition action(PDF 1.5) controls the playing of multimedia content(see 13.2, “Multimedia”). This action may be used in the following ways:
            *
            *  •   To begin the playing of a rendition object(see 13.2.3, “Renditions”), associating it with a screen annotation(see 12.5.6.18, “Screen Annotations”). 
            *      The screen annotation specifies where the rendition shall be played unless otherwise specified.
            *
            *  •   To stop, pause, or resume a playing rendition.
            *
            *  •   To trigger the execution of a JavaScript script that may perform custom operations.
            *
            *Table 214 lists the entries in a rendition action dictionary.
            *
            *Table 214 - Additional entries specific to a rendition action
            *
            *          [Key]               [Type]                  [Value]
            *
            *          S                   name                    (Required) The type of action that this dictionary describes; shall be Renditionfor a rendition action.
            *
            *          R                   dictionary              (Required when OP is present with a value of 0 or 4; otherwise optional) A rendition object (see 13.2.3, “Renditions”).
            *
            *          AN                  dictionary              (Required if OP is present with a value of 0, 1, 2, 3 or 4; otherwise optional) An indirect reference to a screen annotation (see 12.5.6.18, “Screen Annotations”).
            *
            *          OP                  integer                 (Required if JS is not present; otherwise optional) The operation to perform when the action is triggered. Valid values shall be:
            *
            *                                                      0   If no rendition is associated with the annotation specified by AN, play the rendition specified by R, associating it with the annotation. 
            *                                                          If a rendition is already associated with the annotation, it shall be stopped, and the new rendition shall be associated with the annotation.
            *
            *                                                      1   Stop any rendition being played in association with the annotation specified by AN, and remove the association. 
            *                                                          If no rendition is being played, there is no effect.
            *
            *                                                      2   Pause any rendition being played in association with the annotation specified b y AN. 
            *                                                          If no rendition is being played, there is no effect.
            *
            *                                                      3   Resume any rendition being played in association with the annotation specified by AN.
            *                                                          If no rendition is being played or the rendition is not paused, there is no effect.
            *
            *                                                      4   Play the rendition specified by R, associating it with the annotation specified by AN.
            *                                                          If a rendition is already associated with the annotation, resume the rendition if it is paused; otherwise, do nothing.
            *
            *          JS                  text string             (Required if OP is not present; otherwise optional) A text string or stream containing a JavaScript script that shall be executed when the action is triggered.
            *                              or stream
            *
            *Either the JS entry or the OP entry shall be present. If both are present, OP is considered a fallback that shall be executed if the conforming reader is unable to execute JavaScripts. 
            *If OP has an unrecognized value and there is no JS entry, the action is invalid.
            *
            *In some situations, a pause (OP value of 2) or resume (OP value of 3) operation may not make sense or the player may not support it. 
            *In such cases, the user should be notified of the failure to perform the operation.
            *
            *EXAMPLE       A JPEG image
            *
            *Before a rendition action is executed, the conforming reader shall make sure that the P entry of the screen annotation dictionary references a valid page object and that the annotation is present in the page object’s Annots array(see Table 30).
            *
            *A rendition may play in the rectangle occupied by a screen annotation, even if the annotation itself is not visible; for example, if its Hidden or NoView flags(see Table 165) are set. 
            *If a screen annotation is not visible because its location on the page is not being displayed by the viewer, the rendition is not visible. 
            *However, it may become visible if the view changes, such as by scrolling.
            *
            */

                /*12.6.4.14 Transition Actions
                *
                *A transition action(PDF 1.5) may be used to control drawing during a sequence of actions.
                *As discussed in 12.6.2, “Action Dictionaries,” the Next entry in an action dictionary may specify a sequence of actions.Conforming readers shall normally suspend drawing when such a sequence begins and resume drawing when it ends.
                *If a transition action is present during a sequence, the conforming reader shall render the state of the page viewing area as it exists after completion of the previous action and display it using a transition specified in the action dictionary(see Table 215).
                *Once this transition completes, drawing shall be suspended again.
                *
                *Table 215 - Additional entries specific to a transition action
                *
                *          [Key]               [Type]              [Value]
                *
                *          S                   name                (Required) The type of action that this dictionary describes; shall be Transfor a transition action.
                *
                *          Trans               dictionary          (Required) The transition to use for the update of the display (see Table 162).
                *
                */

                /*12.6.4.15 Go-To-3D-View Actions
                *
                *A go-to-3D-view action (PDF 1.6) identifies a 3D annotation and specifies a view for the annotation to use (see 13.6, “3D Artwork”). 
                *Table 216 shows the entries in a go-to-3D-view action dictionary.
                *
                *Table 216 - Additional entries specific to a go-to-3D-view action
                *
                *          [Key]              [Type]               [Value]
                *
                *          S                  name                 (Required) The type of action that this dictionary describes; shall be GoTo3DView for a transition action.
                *
                *          TA                 dictionary           (Required) The target annotation for which to set the view.
                *
                *          V                  (various)            (Required) The view to use. It may be one of the following types:
                *
                *                                                  A 3D view dictionary(see 13.6.4, “3D Views”).
                *
                *                                                  An integer specifying an index into the VA array in the 3D stream(see Table 300).
                *
                *                                                  A text string matching the IN entry in one of the views in the VA array(see Table 304).
                *
                *                                                  A name that indicates the first(F), last(L), next(N), previous(P), or default(D) entries in the VA array; see discussion following this Table.
                *
                *The V entry selects the view to apply to the annotation specified by TA. 
                *This view may be one of the predefined views specified by the VA entry of the 3D stream (see Table 300) or a unique view specified here.
                *
                *If the predefined view is specified by the names N (next) or P (previous), it should be interpreted in the following way:
                *
                *  •   When the last view applied was specified by means of the VA array, N and P indicate the next and previous entries, respectively, in the VA array(wrapping around if necessary).
                *
                *  •   When the last view was not specified by means of VA, using N or P should result in reverting to the default view.
                */

                /*12.6.4.16 JavaScript Actions
                *
                *Upon invocation of a JavaScript action, a conforming processor shall execute a script that is written in the JavaScript programming language. 
                *Depending on the nature of the script, various interactive form fields in the document may update their values or change their visual appearances.
                *Mozilla Development Center’s Client-Side JavaScript Reference and the Adobe JavaScript for Acrobat API Reference(see the Bibliography) give details on the contents and effects of JavaScript scripts.
                *Table 217 shows the action dictionary entries specific to this type of action.
                *
                *Table 217 - Additional entries specific to a JavaScript action
                *
                *              [Key]               [Type]              [Name]
                *
                *              S                   name                (Required) The type of action that this dictionary describes; shall be JavaScript for a JavaScript action.
                *
                *              JS                  text string or      (Required) A text string or text stream containing the JavaScript script to be executed.
                *                                  text stream         PDFDocEncoding or Unicode encoding (the latter identified by the Unicode prefix U+FEFF) shall be used to encode the contents of the string or stream.
                *
                *To support the use of parameterized function calls in JavaScript scripts, the JavaScript entry in a PDF document’s name dictionary (see 7.7.4, “Name Dictionary”) may contain a name tree that maps name strings to document-level JavaScript actions. 
                *When the document is opened, all of the actions in this name tree shall be executed, defining JavaScript functions for use by other scripts in the document.
                *
                *NOTE          The name strings associated with individual JavaScript actions in the name dictionary serve merely as a convenient means for organizing and packaging scripts.
                *              The names are arbitrary and need not bear any relation to the JavaScript name space.
                */
                
        }

        //12.7 Interactive Forms
        public class Interactive_Forms
        {
            /*12.7.1 General
            *
            *An interactive form(PDF 1.2)—sometimes referred to as an AcroForm—is a collection of fields for gathering information interactively from the user.
            *A PDF document may contain any number of fields appearing on any combination of pages, all of which make up a single, global interactive form spanning the entire document.
            *Arbitrary subsets of these fields can be imported or exported from the document; see 12.7.5, “Form Actions.”
            *
            *NOTE 1        Interactive forms should not be confused with form XObjects (see 8.10, “Form XObjects”). 
            *              Despite the similarity of names, the two are different, unrelated types of objects.
            *
            *Each field in a document’s interactive form shall be defined by a field dictionary(see 12.7.3, “Field Dictionaries”). 
            *For purposes of definition and naming, the fields can be organized hierarchically and can inherit attributes from their ancestors in the field hierarchy.
            *A field’s children in the hierarchy may also include widget annotations(see 12.5.6.19, “Widget Annotations”) that define its appearance on the page.
            *A field that has children that are fields is called a non-terminal field.A field that does not have children that are fields is called a terminal field.
            *
            *A terminal field may have children that are widget annotations (see 12.5.6.19, “Widget Annotations“) that define its appearance on the page. 
            *As a convenience, when a field has only a single associated widget annotation, the contents of the field dictionary and the annotation dictionary (12.5.2, “Annotation Dictionaries”) may be merged into a single dictionary containing entries that pertain to both a field and an annotation. 
            *(This presents no ambiguity, since the contents of the two kinds of dictionaries do not conflict.) If such an object defines an appearance stream, the appearance shall be consistent with the object’s current value as a field.
            *
            *NOTE 2        Fields containing text whose contents are not known in advance may need to construct their appearance streams dynamically instead of defining them statically in an appearance dictionary; see 12.7.3.3, “Variable Text.”
            */

            /*12.7.2 Interactive Form Dictionary
            *
                *The contents and properties of a document’s interactive form shall be defined by an interactive form dictionarythat shall be referenced from the AcroForm entry in the document catalogue (see 7.7.2, “Document Catalog”). 
                *Table 218 shows the contents of this dictionary.
                *
                *Table 218 - Entries in the interactive form dictionary
                *
                *          [Key]               [Type]          [Value]
                *
                *          Fields              array           (Required) An array of references to the document’s root fields(those with no ancestors in the field hierarchy).
                *
                *          NeedAppearances     boolean         (Optional) A flag specifying whether to construct appearance streams and appearance dictionaries for all widget annotations in the document (see 12.7.3.3, “Variable Text”). 
                *                                              Default value: false.
                *
                *          SigFlags            integer         (Optional; PDF 1.3) A set of flags specifying various document-level characteristics related to signature fields (see Table 219, and 12.7.4.5, “Signature Fields”). 
                *                                              Default value: 0.
                *
                *          CO                  array           (Required if any fields in the document have additional-actions dictionaries containing a C entry; PDF 1.3) 
                *                                              An array of indirect references to field dictionaries with calculation actions, defining the calculation order in which their values will be recalculated when the value of any field changes (see 12.6.3, “Trigger Events”).
                *
                *          DR                  dictionary      (Optional) A resource dictionary (see 7.8.3, “Resource Dictionaries”) containing default resources (such as fonts, patterns, or colour spaces) that shall be used by form field appearance streams. 
                *                                              At a minimum, this dictionary shall contain a Font entry specifying the resource name and font dictionary of the default font for displaying text.
                *
                *          DA                  string          (Optional) A document-wide default value for the DA attribute of variable text fields (see 12.7.3.3, “Variable Text”).
                *
                *          XFA                 stream or       (Optional; PDF 1.5) A stream or array containing an XFA resource, whose format shall be described by the Data Package (XDP) Specification. (see the Bibliography).
                *                              array           The value of this entry shall be either a stream representing the entire contents of the XML Data Package or an array of text string and stream pairs representing the individual packets comprising the XML Data Package.
                *                                              See 12.7.8, “XFA Forms,” for more information.
                *
                *The value of the interactive form dictionary’s SigFlags entry is an unsigned 32-bit integer containing flags specifying various document-level characteristics related to signature fields (see 12.7.4.5, “Signature Fields”). 
                *Bit positions within the flag word shall be numbered from 1 (low-order) to 32 (high-order). 
                *Table 219 shows the meanings of the flags; all undefined flag bits shall be reserved and shall be set to 0.
                *
                *Table 219 - Signature flags
                *
                *          [Bit position]          [Name]                  [Meaning]
                *
                *          1                       SignaturesExist         If set, the document contains at least one signature field. This flag allows a conforming reader to enable user interface items (such as menu items or pushbuttons) related to signature processing without having to scan the entire document for the presence of signature fields.
                *
                *          2                       AppendOnly              If set, the document contains signatures that may be invalidated if the file is saved (written) in a way that alters its previous contents, as opposed to an incremental update. 
                *                                                          Merely updating the file by appending new information to the end of the previous version is safe (see H.7, “Updating Example”). 
                *                                                          Conforming readers may use this flag to inform a user requesting a full save that signatures will be invalidated and require explicit confirmation before continuing with the operation.
                */

            /*12.7.3 Field Dictionaries
            */

                /*12.7.3.1 General
                *
                *Each field in a document’s interactive form shall be defined by a field dictionary, which shall be an indirect object.
                *The field dictionaries may be organized hierarchically into one or more tree structures. 
                *Many field attributes are inheritable, meaning that if they are not explicitly specified for a given field, their values are taken from those of its parent in the field hierarchy.
                *Such inheritable attributes shall be designated as such in the Tables 220 and 221.
                *The designation(Required; inheritable) means that an attribute shall be defined for every field, whether explicitly in its own field dictionary or by inheritance from an ancestor in the hierarchy.
                *Table 220shows those entries that are common to all field dictionaries, regardless of type.
                *Entries that pertain only to a particular type of field are described in the relevant sub - clauses in Table 220.
                *
                *Table 220 - Entries common to all field dictionaries
                *
                *          [Key]               [Type]              [Value]
                *
                *          FT                  name                (Required for terminal fields; inheritable) The type of field that this dictionary describes:
                *
                *                                                  Btn     Button(see 12.7.4.2, “Button Fields”)
                *
                *                                                  Tx      Text(see 12.7.4.3, “Text Fields”)
                *
                *                                                  Ch      Choice(see 12.7.4.4, “Choice Fields”)
                *
                *                                                  Sig     (PDF 1.3) Signature(see 12.7.4.5, “Signature Fields”)
                *      
                *                                                  This entry may be present in a non-terminal field(one whose descendants are fields) to provide an inheritable FT value. 
                *                                                  However, a non-terminal field does not logically have a type of its own; it is merely a container for inheritable attributes that are intended for descendant terminal fields of any type.
                *
                *          Parent              dictionary          (Required if this field is the child of another in the field hierarchy; absent otherwise) 
                *                                                  The field that is the immediate parent of this one (the field, if any, whose Kids array includes this field). 
                *                                                  A field can have at most one parent; that is, it can be included in the Kids array of at most one other field.
                *
                *          Kids                array               (Sometimes required, as described below) An array of indirect references to the immediate children of this field.
                *
                *                                                  In a non - terminal field, the Kids array shall refer to field dictionaries that are immediate descendants of this field.
                *                                                  In a terminal field, the Kids array ordinarily shall refer to one or more separate widget annotations that are associated with this field.However, if there is only one associated widget annotation, and its contents have been merged into the field dictionary, Kids shall be omitted.
                *
                *          T                   text string         (Optional) The partial field name (see 12.7.3.2, “Field Names”).
                *
                *          TU                  text string         (Optional; PDF 1.3) An alternate field name that shall be used in place of the actual field name wherever the field shall be identified in the user interface (such as in error or status messages referring to the field). 
                *                                                  This text is also useful when extracting the document’s contents in support of accessibility to users with disabilities or for other purposes (see 14.9.3, “Alternate Descriptions”).
                *
                *          TM                  text string         (Optional; PDF 1.3) The mapping name that shall be used when exporting interactive form field data from the document.
                *
                *          Ff                  integer             (Optional; inheritable) A set of flags specifying various characteristics of the field (see Table 221). 
                *                                                  Default value: 0.
                *
                *          V                   (various)           (Optional; inheritable) The field’s value, whose format varies depending on the field type. 
                *                                                  See the descriptions of individual field types for further information.
                *
                *          DV                  (various)           (Optional; inheritable) The default value to which the field reverts when a reset-form action is executed (see 12.7.5.3, “Reset-Form Action”). 
                *                                                  The format of this value is the same as that of V.
                *
                *          AA                  dictionary          (Optional; PDF 1.2) An additional-actions dictionary defining the field’s behaviour in response to various trigger events (see 12.6.3, “Trigger Events”). 
                *                                                  This entry has exactly the same meaning as the AA entry in an annotation dictionary (see 12.5.2, “Annotation Dictionaries”).
                *
                *The value of the field dictionary’s Ff entry is an unsigned 32-bit integer containing flags specifying various characteristics of the field. 
                *Bit positions within the flag word shall be numbered from 1 (low-order) to 32 (high-order). The flags shown in Table 221 are common to all types of fields. 
                *Flags that apply only to specific field types are discussed in the sub-clauses describing those types. 
                *All undefined flag bits shall be reserved and shall be set to 0.
                *
                *Table 221 - Field flags common to all field types
                *
                *          [Bit position]              [Name]              [Meaning]
                *
                *          1                           ReadOnly            If set, the user may not change the value of the field. 
                *                                                          Any associated widget annotations will not interact with the user; that is, they will not respond to mouse clicks or change their appearance in response to mouse motions. 
                *                                                          This flag is useful for fields whose values are computed or imported from a database.
                *
                *          2                           Required            If set, the field shall have a value at the time it is exported by a submit-form action (see 12.7.5.2, “Submit-Form Action”).
                *
                *          3                           NoExport            If set, the field shall not be exported by a submit-form action (see 12.7.5.2, “Submit-Form Action”).
                *
                */

                /*12.7.3.2 Field Names
                *
                *The T entry in the field dictionary (see Table 220) holds a text string defining the field’s partial field name. 
                *The fully qualified field name is not explicitly defined but shall be constructed from the partial field names of the field and all of its ancestors. 
                *For a field with no parent, the partial and fully qualified names are the same. 
                *For a field that is the child of another field, the fully qualified name shall be formed by appending the child field’s partial name to the parent’s fully qualified name, separated by a PERIOD (2Eh) as shown:
                *
                *      parent’s_full_name.child’s_partial_name
                *
                *EXAMPLE       If a field with the partial field name PersonalData has a child whose partial name is Address, which in turn has a child with the partial name ZipCode, the fully qualified name of this last field is
                *
                *              PersonalData.Address.ZipCode
                *
                *Because the PERIOD is used as a separator for fully qualified names, a partial name shall not contain a PERIOD character.
                *Thus, all fields descended from a common ancestor share the ancestor’s fully qualified field name as a common prefix in their own fully qualified names.
                *
                *It is possible for different field dictionaries to have the same fully qualified field name if they are descendants of a common ancestor with that name and have no partial field names(T entries) of their own.
                *Such field dictionaries are different representations of the same underlying field; they should differ only in properties that specify their visual appearance.
                *In particular, field dictionaries with the same fully qualified field name shall have the same field type (FT), value (V), and default value (DV).
                */

                /*12.7.3.3 Variable Text
                *
                *When the contents and properties of a field are known in advance, its visual appearance can be specified by an appearance stream defined in the PDF file (see 12.5.5, “Appearance Streams,” and 12.5.6.19, “Widget Annotations”). In some cases, however, the field may contain text whose value is not known until viewing time.
                *
                *NOTE      Examples include text fields to be filled in with text typed by the user from the keyboard, scrollable list boxes whose contents are determined interactively at the time the document is displayed and fields containing current dates or values calculated by a JavaScript.
                *
                *In such cases, the PDF document cannot provide a statically defined appearance stream for displaying the field.
                *Instead, the conforming reader shall construct an appearance stream dynamically at viewing time.
                *The dictionary entries shown in Table 222 provide general information about the field’s appearance that can be combined with the specific text it contains to construct an appearance stream.
                *
                *Table 222 - Additional entries common to all fields containing variable text
                *
                *          [Key]               [Type]              [Value]
                *
                *          DA                  string              (Required; inheritable) The default appearance string containing a sequence of valid page-content graphics or text state operators that define such properties as the field’s text size and colour.
                *
                *          Q                   integer             (Optional; inheritable) A code specifying the form of quadding (justification) that shall be used in displaying the text:
                *
                *                                                  0       Left - justified
                *
                *                                                  1       Centered
                *
                *                                                  2       Right - justified
                *
                *                                                  Default value: 0    (left - justified).
                *
                *          DS                  text string         (Optional; PDF 1.5) A default style string, as described in 12.7.3.4, “Rich Text Strings.”
                *
                *          RV                  text string         (Optional; PDF 1.5) A rich text string, as described in 12.7.3.4, “Rich Text Strings.”
                *                              or text
                *                              stream
                *
                *The new appearance stream becomes the normal appearance (N) in the appearance dictionary associated with the field’s widget annotation (see Table 168). 
                *(If the widget annotation has no appearance dictionary, the conforming reader shall create one and store it in the annotation dictionary’s AP entry.)
                *
                *In PDF 1.5, form fields that have the RichText flag set(see Table 226) specify formatting information as described in 12.7.3.4, “Rich Text Strings.” 
                *For these fields, the following conventions are not used, and the entire annotation appearance shall be regenerated each time the value is changed.
                *
                *For non - rich text fields, the appearance stream—which, like all appearance streams, is a form XObject—has the contents of its form dictionary initialized as follows:
                *
                *  •   The resource dictionary(Resources) shall be created using resources from the interactive form dictionary’s DR entry(see Table 218).
                *
                *  •   The lower-left corner of the bounding box(BBox) is set to coordinates(0, 0) in the form coordinate system. 
                *      The box’s top and right coordinates are taken from the dimensions of the annotation rectangle(the Rectentry in the widget annotation dictionary).
                *
                *  •   All other entries in the appearance stream’s form dictionary are set to their default values(see 8.10, “Form XObjects”).
                *
                *EXAMPLE       The appearance stream includes the following section of marked content, which represents the portion of the stream that draws the text:
                *
                *              / Tx BMC                                                % Begin marked content with tag Tx
                *                  q                                                   % Save graphics state
                *                          …Any required graphics state changes, such as clipping…
                *                      BT                                              % Begin text object
                *                          …Default appearance string(DA)…
                *                          …Text - positioning and text-showing operators to show the variable text…
                *                      ET                                              % End text object
                *                   Q                                                  % Restore graphics state
                *              EMC                                                     % End marked content
                *
                *              The BMC(begin marked content) and EMC(end marked content) operators are discussed in 14.6, “Marked Content.” 
                *              q(save graphics state) and Q(restore graphics state) are discussed in 8.4.4, “Graphics State Operators.” 
                *              BT(begin text object) and ET(end text object) are discussed in 9.4, “Text Objects.” See Example 1 in 12.7.8, “XFA Forms” for an example.
                *
                *The default appearance string (DA) contains any graphics state or text state operators needed to establish the graphics state parameters, such as text size and colour, for displaying the field’s variable text. 
                *Only operators that are allowed within text objects shall occur in this string (see Figure 9). 
                *At a minimum, the string shall include a Tf (text font) operator along with its two operands, font and size. 
                *The specified font value shall match a resource name in the Font entry of the default resource dictionary (referenced from the DR entry of the interactive form dictionary; see Table 218). 
                *A zero value for size means that the font shall be auto-sized: its size shall be computed as a function of the height of the annotation rectangle.
                *
                *The default appearance string shall contain at most one Tm(text matrix) operator. 
                *If this operator is present, the conforming reader shall replace the horizontal and vertical translation components with positioning values it determines to be appropriate, based on the field value, the quadding(Q) attribute, and any layout rules it employs. 
                *If the default appearance string contains no Tm operator, the viewer shall insert one in the appearance stream(with appropriate horizontal and vertical translation components) after the default appearance string and before the text - positioning and text-showing operators for the variable text.
                *
                *To update an existing appearance stream to reflect a new field value, the conforming reader shall first copy any needed resources from the document’s DR dictionary (see Table 218) into the stream’s Resources dictionary. 
                *(If the DR and Resources dictionaries contain resources with the same name, the one already in the Resources dictionary shall be left intact, not replaced with the corresponding value from the DR dictionary.) 
                *The conforming reader shall then replace the existing contents of the appearance stream from /Tx BMC to the matching EMC with the corresponding new contents as shown in Example 1 in "Check Boxes," 12.7.4, “Field Types.” 
                *(If the existing appearance stream contains no marked content with tag Tx, the new contents shall be appended to the end of the original stream.)
                */

                /*12.7.3.4 Rich Text Strings
                *
                *Beginning with PDF 1.5, the text contents of variable text form fields, as well as markup annotations, may include formatting (style) information. 
                *These rich text strings are fully-formed XML documents that conform to the rich text conventions specified for the XML Forms Architecture (XFA) specification, which is itself a subset of the XHTML 1.0 specification, augmented with a restricted set of CSS2 style attributes 
                *(see the Bibliographyfor references to all these standards).
                *
                *Table 223 lists the XHTML elements that may appear in rich text strings.
                *The<body> element is the root element; its required attributes are listed in Table 224.
                *Other elements(< p > and<span>) contain enclosed text that may take style attributes, which are listed in Table 225.
                *These style attributes are CSS inline style property declarations of the form name:value, with each declaration separated by a SEMICOLON(3Bh), as illustrated in the Example in "Radio Buttons," 12.7.4, “Field Types.”
                *
                *In PDF 1.6, PDF supports the rich text elements and attributes specified in the XML Forms Architecture(XFA) Specification, 2.2(see Bibliography).
                *These rich text elements and attributes are a superset of those described in Table 223, Table 224 and Table 225.
                *In PDF 1.7, PDF supports the rich text elements and attributes specified in the XML Forms Architecture(XFA) Specification, 2.4(see Bibliography).
                *
                *Table 223 - XHTML elements used in rich text strings
                *
                *          [element]               [Description]
                *
                *          <body>                  The element at the root of the XML document. Table 224 lists the required attributes for this element.
                *
                *          <p>                     Encloses text that shall be interpreted as a paragraph. It may take the style attributes listed in Table 225.
                *
                *          <i>                     Encloses text that shall be displayed in an italic font.
                *
                *          <b>                     Encloses text that shall be displayed in a bold font.
                *
                *          <span>                  Groups text solely for the purpose of applying styles (using the attributes in Table 225).
                *
                *Table 224 - Attributes of the <body> element
                *
                *          [Attribute]             [Description]
                *
                *          xmlns                   The default namespaces for elements within the rich text string. 
                *                                  Shall be xmlns="http:*www.w3.org/1999/xhtml"
                *                                  xmlns: xfa = "http:*www.xfa.org/schema/xfa-data/1.0".
                *
                *          xfa:contentType         Shall be "text/html".
                *
                *          xfa:APIVersion          A string that identifies the software used to generate the rich text string. 
                *                                  It shall be of the form software_name:software_version, where
                *
                *                                  software_name identifies the software by name. It shall not contain spaces.
                *
                *                                  software_version identifies the version of the software.It consists of a series of integers separated by decimal points. 
                *                                  Each integer is a version number, the leftmost value being a major version number, with values to the right increasingly minor. 
                *                                  When comparing strings, the versions shall be compared in order.
                *
                *                                  NOTE    “5.2” is less than “5.13” because 2 is less than 13; the string is not treated as a decimal number. 
                *                                          When comparing strings with different numbers of sections, the string with fewer sections is implicitly padded on the right with sections containing “0” to make the number of sections equivalent.
                *
                *          xfa:spec                The version of the XML Forms Architecture (XFA) specification to which the rich text string complies. 
                *                                  If the file being written conforms to PDF 1.5, then the rich text string shall conform to XFA 2.0, and this attribute shall be XFA 2.0; if the file being written conforms to PDF 1.6, then the rich text string shall conform to XFA 2.2, and this attribute shall be XFA 2.2; and if the file being written conforms to PDF 1.7, then the rich text string shall conform to XFA 2.4, and this attribute shall be XFA 2.4.
                *
                *Table 225 - CSS2 style attributes used in rich text strings
                *
                *          [Attribute]             [Value]             [Description]
                *
                *          text-align              keyword             Horizontal alignment. Possible values: left, right, and center.
                *
                *          vertical-align          decimal             An amount by which to adjust the baseline of the enclosed text. 
                *                                                      A positive value indicates a superscript; a negative value indicates a subscript. 
                *                                                      The value is of the form <decimal number>pt, optionally preceded by a sign, and followed by “pt”.
                *                                                      EXAMPLE - 3pt, 4pt.
                *
                *          font-size               decimal             The font size of the enclosed text. The value is of the form
                *                                                      < decimal number > pt.
                *
                *          font-style              keyword             Specifies the font style of the enclosed text. Possible values: normal, italic.
                *
                *          font-weight             keyword             The weight of the font for the enclosed text. 
                *                                                      Possible values: normal, bold, 100, 200, 300, 400, 500, 600, 700, 800, 900.
                *                                                      normal is equivalent to 400, and bold is equivalent to 700.
                *
                *          font-family             list                A font name or list of font names that shall be used to display the enclosed text. 
                *                                                      (If a list is provided, the first one containing glyphs for the specified text shall be used.)
                *
                *          font                    list                A shorthand CSS font property of the form
                *                                                      font:< font - style > < font - weight > < font - size > < font - family >
                *
                *          color                   RGB                 The colour of the enclosed text. It can be in one of two forms:
                *                                  value               #rrggbb with a 2-digit hexadecimal value for each component
                *                                                      rgb(rrr,ggg,bbb) with a decimal value for each component.
                *                                                      Although the values specified by the color property are interpreted as sRGB values, they shall be transformed into values in a non-ICC based colour space when used to generate the annotation’s appearance.
                *
                *          text-decoration         keyword             One of the following keywords:
                *                                                      underline: The enclosed text shall be underlined.
                *                                                      line - through: The enclosed text shall have a line drawn through it.
                *
                *          font-stretch            keyword             Specifies a normal, condensed or extended face from a font family. 
                *                                                      Supported values from narrowest to widest are ultra-condensed, extra-condensed, condensed, semi-condensed, normal, semi-expanded, expanded, extra-expanded, and ultra-expanded.
                *
                *Rich text strings shall be specified by the RV entry of variable text form field dictionaries (see Table 222) and the RC entry of markup annotation dictionaries (see Table 170). 
                *Rich text strings may be packaged as text streams (see 7.9.3, “Text Streams”). Form fields using rich text streams should also have the RichText flag set (see Table 228).
                *
                *A default style string shall be specified by the DS entry for free text annotations(see Table 174) or variable text form fields(see Table 222).
                *This string specifies the default values for style attributes, which shall be used for any style attributes that are not explicitly specified for the annotation or field.
                *All attributes listed in Table 225are legal in the default style string.This string, in addition to the RV or RC entry, shall be used to generate the appearance.
                *
                *NOTE 1        Markup annotations other than free text annotations(see 12.5.6.2, “Markup Annotations”) do not use a default style string because their appearances are implemented using platform controls requiring the conforming reader to pick an appropriate system font for display.
                *
                *When a form field or annotation contains rich text strings, the flat text(character data) of the string should also be preserved(in the V entry for form fields and the Contents entry for annotations).
                *This enables older readers to read and edit the data(although with loss of formatting information).The DA entry should be written out as well when the file is saved.
                *
                *When a rich text string specifies font attributes, the conforming reader shall use font name selection as described in Section 15.3 of the CSS2 specification(see the Bibliography). 
                *Precedence should be given to the fonts in the default resources dictionary, as specified by the DR entry in Table 218.
                *
                *EXAMPLE       The following example illustrates the entries in a widget annotation dictionary for rich text. 
                *              The DS entry specifies the default font. The RV entry contains two paragraphs of rich text: the first paragraph specifies bold and italic text in the default font; the second paragraph changes the font size.
                *
                *              / DS(font: 18pt Arial)                                          % Default style string using an abbreviated font
                *                                                                              % descriptor to specify 18pt text using an Arial font
                *              / RV(<? xml version = "1.0" ?>< body xmlns = "http:*www.w3.org/1999/xtml"
                *                      xmlns: xfa = "http:*www.xfa.org/schema/xfa-data/1.0/"
                *                      xfa: contentType = "text/html" xfa: APIVersion = "Acrobat:8.0.0" xfa: spec = "2.4" >
                *                      < p style = "text-align:left" >
                *                          < b >
                *                              < i >
                *                                  Here is some bold italic text
                *                              </ i >
                *                          </ b >
                *                      </ p >
                *                      < p style = "font-size:16pt" >
                *                      This text uses default text state parameters but changes the font size to 16.
                *                      </ p >
                *              </ body > )
                */

            /*12.7.4 Field Types
            */

                /*12.7.4.1 General
                *
                *Interactive forms support the following field types:
                *
                *  •   Button fields represent interactive controls on the screen that the user can manipulate with the mouse.
                *
                *They include pushbuttons, check boxes, and radio buttons.
                *
                *  •   Text fields are boxes or spaces in which the user can enter text from the keyboard.
                *
                *  •   Choice fields contain several text items, at most one of which may be selected as the field value. They
                *      include scrollable list boxes and combo boxes.
                *
                *  •   Signature fields represent digital signatures and optional data for authenticating the name of the signer and
                *      the document’s contents.
                *
                *The following sub - clauses describe each of these field types in detail.Further types may be added in the future.
                */

                /*12.7.4.2 Button Fields
                */

                /*12.7.4.2.1 General
                *
                *A button field(field type Btn) represents an interactive control on the screen that the user can manipulate with
                *the mouse.There are three types of button fields:
                *  •   A pushbutton is a purely interactive control that responds immediately to user input without retaining a
                *      permanent value(see 12.7.4.2.2, “Pushbuttons”).
                *
                *  •   A check box toggles between two states, on and off(see 12.7.4.2.3, “Check Boxes”).
                *
                *  •   Radio button fields contain a set of related buttons that can each be on or off. Typically, at most one radio
                *      button in a set may be on at any given time, and selecting any one of the buttons automatically deselects
                *      all the others. (There are exceptions to this rule, as noted in "Radio Buttons.")
                *
                *For button fields, bits 15, 16, 17, and 26 shall indicate the intended behaviour of the button field.A conforming
                *reader shall follow the intended behaviour, as defined in Table 226 and clauses 12.7.4.2.2, "Pushbuttons", 12.7.4.2.3, "Check Boxes" and 12.7.4.2.4, "Radio Buttons".
                *
                *Table 226 - Field flags specific to button fields
                *
                *          [Bit position]              [Name]              [Meaning]
                *
                *          15                          NoToggleToOff       (Radio buttons only) If set, exactly one radio button shall be
                *                                                          selected at all times; selecting the currently selected button has no
                *                                                          effect.If clear, clicking the selected button deselects it, leaving no
                *                                                          button selected.
                *
                *          16                          Radio               If set, the field is a set of radio buttons; if clear, the field is a check
                *                                                          box.This flag may be set only if the Pushbutton flag is clear.
                *
                *          17                          Pushbutton          If set, the field is a pushbutton that does not retain a permanent
                *                                                          value.
                *
                *          26                          RadiosInUnison      (PDF 1.5) If set, a group of radio buttons within a radio button field
                *                                                          that use the same value for the on state will turn on and off in
                *                                                          unison; that is if one is checked, they are all checked. If clear, the
                *                                                          buttons are mutually exclusive(the same behavior as HTML radio
                *                                                          buttons).
                */

                /*12.7.4.2.2 Pushbuttons
                *
                *A pushbutton field shall have a field type of Btn and the Pushbutton flag(see Table 226) set to one.Because this type of button retains no permanent value, 
                *it shall not use the V and DV entries in the field dictionary(see Table 220).
                */

                /*12.7.4.2.3 Check Boxes
                *
                *A check box field represents one or more check boxes that toggle between two states, on and off, when manipulated by the user with the mouse or keyboard. 
                *Its field type shall be Btn and its Pushbutton and Radio flags(see Table 226) shall both be clear. 
                *Each state can have a separate appearance, which shall be defined by an appearance stream in the appearance dictionary of the field’s widget annotation
                *(see 12.5.5, “Appearance Streams”).The appearance for the off state is optional but, if present, shall be stored in the appearance dictionary under the name Off.
                *Yes should be used as the name for the on state.
                *
                *The V entry in the field dictionary(see Table 220) holds a name object representing the check box’s appearance state, which shall be used to select the appropriate appearance from the appearance dictionary.
                *
                *EXAMPLE 1     This example shows a typical check box definition.
                *
                *              1 0 obj
                *                  << / FT / Btn
                *                     / T(Urgent)
                *                     / V / Yes
                *                     / AS / Yes
                *                     / AP << / N << / Yes 2 0 R / Off 3 0 R >>
                *                  >>
                *              endobj
                *              2 0 obj
                *                  << / Resources 20 0 R
                *                     / Length 104
                *                  >>
                *              stream
                *                  q
                *                      0 0 1 rg
                *                      BT
                *                          / ZaDb 12 Tf
                *                          0 0 Td
                *                          (8) Tj
                *                      ET
                *                  Q
                *              endstream
                *              endobj
                *
                *              3 0 obj
                *                  << / Resources 20 0 R
                *                     / Length 104
                *                  >>
                *              stream
                *                  q
                *                      0 0 1 rg
                *                      BT
                *                          / ZaDb 12 Tf
                *                          0 0 Td
                *                          (8) Tj
                *                      ET
                *                  Q
                *              endstream
                *              endobj
                *
                *Beginning with PDF 1.4, the field dictionary for check boxes and radio buttons may contain an optional Optentry (see Table 227). 
                *If present, the Opt entry shall be an array of text strings representing the export value of each annotation in the field. It may be used for the following purposes:
                *
                *  •   To represent the export values of check box and radio button fields in non - Latin writing systems. 
                *      Because name objects in the appearance dictionary are limited to PDFDocEncoding, they cannot represent non-Latin text.
                *
                *  •   To allow radio buttons or check boxes to be checked independently, even if they have the same export value.
                *
                *EXAMPLE 2     A group of check boxes may be duplicated on more than one page such that the desired behavior is that when a user checks a box, the corresponding boxes on each of the other pages are also checked. 
                *              In this case, each of the corresponding check boxes is a widget in the Kids array of a check box field.
                *
                *NOTE      For radio buttons, the same behavior shall occur only if the RadiosInUnison flag is set.
                *          If it is not set, at most one radio button in a field shall be set at a time.
                *
                *Table 227 - Additional entry specific to check box and radio button fields
                *
                *              [Key]               [Type]                          [Value]
                *
                *              Opt                 array of text strings           (Optional; inheritable; PDF 1.4) An array containing one entry for each widget annotation in the Kids array of the radio button or check box field. 
                *                                                                  Each entry shall be a text string representing the on state of the corresponding widget annotation.
                *                                                                  When this entry is present, the names used to represent the on state in the APdictionary of each annotation(for example, / 1, / 2) numerical position(starting with 0) of the annotation in the Kids array, encoded as a name object.
                *                                                                  This allows distinguishing between the annotations even if two or more of them have the same value in the Opt array.
                */

                /*12.7.4.2.4 Radio Buttons
                *
                *A radio button field is a set of related buttons. Like check boxes, individual radio buttons have two states, on and off. 
                *A single radio button may not be turned off directly but only as a result of another button being turned on. 
                *Typically, a set of radio buttons (annotations that are children of a single radio button field) have at most one button in the on state at any given time; selecting any of the buttons automatically deselects all the others.
                *
                *NOTE      An exception occurs when multiple radio buttons in a field have the same on state and the RadiosInUnison flag is set.
                *          In that case, turning on one of the buttons turns on all of them.
                *
                *The field type is Btn, the Pushbutton flag(see Table 226) is clear, and the Radio flag is set.
                *This type of button field has an additional flag, NoToggleToOff, which specifies, if set, that exactly one of the radio buttons shall be selected at all times.
                *In this case, clicking the currently selected button has no effect; if the NoToggleToOff flag is clear, clicking the selected button deselects it, leaving no button selected.
                *
                *The Kids entry in the radio button field’s field dictionary(see Table 220) holds an array of widget annotations representing the individual buttons in the set. 
                *The parent field’s V entry holds a name object corresponding to the appearance state of whichever child field is currently in the on state; the default value for this entry is Off.
                *
                *EXAMPLE       This example shows the object definitions for a set of radio buttons.
                *
                *              10 0 obj                                                % Radio button field
                *                  << / FT / Btn
                *                     / Ff…                                            % …Radio flag = 1, Pushbutton = 0…
                *                     / T(Credit card)
                *                     / V / cardbrand1
                *                     / Kids[ 11 0 R
                *                             12 0 R
                *                           ]
                *                  >>
                *              endobj
                *              11 0 obj                                                % First radio button
                *                  << / Parent 10 0 R
                *                     / AS / cardbrand1
                *                     / AP << / N << / cardbrand1 8 0 R
                *                     / Off 9 0 R
                *                                 >>
                *                          >>
                *                  >>
                *              endobj
                *              12 0 obj                                                % Second radio button
                *                  << / Parent 10 0 R
                *                     / AS / Off
                *                     / AP << / N << / cardbrand2 8 0 R
                *                     / Off 9 0 R
                *                                 >>
                *                          >>
                *                  >>
                *              endobj
                *              8 0 obj                                                 % Appearance stream for “on” state
                *                  << / Resources 20 0 R
                *                     / Length 104
                *                  >>
                *              stream
                *                  q
                *                      0 0 1 rg
                *                      BT
                *                          / ZaDb 12 Tf
                *                          0 0 Td
                *                          (8) Tj
                *                      ET
                *                  Q
                *              endstream
                *              endobj
                *              9 0 obj                                                 % Appearance stream for “off” state
                *                  << / Resources 20 0 R
                *                     / Length 104
                *                  >>
                *              stream
                *                  q
                *                      0 0 1 rg
                *                      BT
                *                          / ZaDb 12 Tf
                *                          0 0 Td
                *                          (4) Tj
                *                      ET
                *                  Q
                *              endstream
                *              endobj
                *
                *Like a check box field, a radio button field may use the optional Opt entry in the field dictionary (PDF 1.4) to define export values for its constituent radio buttons, using Unicode (UTF-16BE) encoding for non-Latin characters (see Table 227).
                */

                /*12.7.4.3 Text Fields
                *
                *A text field (field type Tx) is a box or space for text fill-in data typically entered from a keyboard. 
                *The text may be restricted to a single line or may be permitted to span multiple lines, depending on the setting of the Multiline
                *flag in the field dictionary’s Ff entry. Table 228 shows the flags pertaining to this type of field. 
                *A text field shall have a field type of Tx. A conforming PDF file, and a conforming processor shall obey the usage guidelines in Table 228.
                *
                *Table 228 - Field flags specific to text fields
                *
                *          [Bit position]              [Name]              [Meaning]
                *
                *          13                          Multiline           If set, the field may contain multiple lines of text; if clear, the field’s text shall be restricted to a single line.
                *
                *          14                          Password            If set, the field is intended for entering a secure password that should not be echoed visibly to the screen. 
                *                                                          Characters typed from the keyboard shall instead be echoed in some unreadable form, such as asterisks or bullet characters.
                *
                *                                                          NOTE        To protect password confidentiality, readers should never store the value of the text field in the PDF file if this flag is set.
                *
                *          21                          FileSelect          (PDF 1.4) If set, the text entered in the field represents the pathname of a file whose contents shall be submitted as the value of the field.
                *
                *          23                          DoNotSpellCheck     (PDF 1.4) If set, text entered in the field shall not be spell-checked.
                *
                *          24                          DoNotScroll         (PDF 1.4) If set, the field shall not scroll (horizontally for single-line fields, vertically for multiple-line fields) to accommodate more text than fits within its annotation rectangle. 
                *                                                          Once the field is full, no further text shall be accepted for interactive form filling; for non-interactive form filling, the filler should take care not to add more character than will visibly fit in the defined area.
                *
                *          25                          Comb                (PDF 1.5) May be set only if the MaxLen entry is present in the text field dictionary (see Table 229) and if the Multiline, Password, and FileSelect flags are clear. 
                *                                                          If set, the field shall be automatically divided into as many equally spaced positions, or combs, as the value of MaxLen, and the text is laid out into those combs.
                *
                *          26                          RichText            (PDF 1.5) If set, the value of this field shall be a rich text string (see 12.7.3.4, “Rich Text Strings”). If the field has a value, the RV entry of the field dictionary (Table 222) shall specify the rich text string.
                *
                *The field’s text shall be held in a text string (or, beginning with PDF 1.5, a stream) in the V (value) entry of the field dictionary. 
                *The contents of this text string or stream shall be used to construct an appearance stream for displaying the field, as described under 12.7.3.3, “Variable Text.” 
                *The text shall be presented in a single style (font, size, colour, and so forth), as specified by the DA (default appearance) string.
                *
                *If the FileSelect flag(PDF 1.4) is set, the field shall function as a file - select control.In this case, the field’s text represents the pathname of a file whose contents shall be submitted as the field’s value:
                *
                *  •   For fields submitted in HTML Form format, the submission shall use the MIME content type multipart / form - data, as described in Internet RFC 2045, Multipurpose Internet Mail Extensions(MIME), Part One: Format of Internet Message Bodies(see the Bibliography).
                *
                *  •   For Forms Data Format(FDF) submission, the value of the V entry in the FDF field dictionary(see FDF Fields in 12.7.7.3, “FDF Catalog”) shall be a file specification(7.11, “File Specifications”) identifying the selected file.
                *
                *  •   XML format is not supported for file - select controls; therefore, no value shall be submitted in this case.
                *
                *Besides the usual entries common to all fields(see Table 220) and to fields containing variable text(see Table 222), the field dictionary for a text field may contain the additional entry shown in Table 229.
                *
                *Table 229 - Additional entry specific to a text field
                *
                *              [Key]               [Type]                  [Value]
                *
                *              MaxLen              integer                 (Optional; inheritable) The maximum length of the field’s text, in characters.
                *
                *EXAMPLE       The following example shows the object definitions for a typical text field.
                *
                *              6 0 obj
                *                  << / FT / Tx
                *                     / Ff …                               % Set Multiline flag
                *                     / T(Silly prose)
                *                     / DA(0 0 1 rg / Ti 12 Tf)
                *                     / V(The quick brown fox ate the lazy mouse)
                *                     / AP << / N 5 0 R >>
                *                  >>
                *              endobj
                *              5 0 obj
                *                  << / Resources 21 0 R
                *                     / Length 172
                *                  >>
                *              stream
                *                  / Tx BMC
                *                      q
                *                          BT
                *                              0 0 1 rg
                *                              / Ti 12 Tf
                *                              1 0 0 1 100 100 Tm
                *                              0 0 Td
                *                              (The quick brown fox) Tj
                *                              0 - 13 Td
                *                              (ate the lazy mouse.) Tj
                *                          ET
                *                      Q
                *                  EMC
                *              endstream
                *              endobj
                */

                /*12.7.4.4 Choice Fields
                *
                *A choice field shall have a field type of Ch that contains several text items, one or more of which shall be selected as the field value. 
                *The items may be presented to the user in one of the following two forms:
                *
                *  •   A scrollable list box
                *
                *  •   A combo box consisting of a drop - down list.
                *      The combo box may be accompanied by an editable text box in which the user can type a value other than the predefined choices, as directed by the value of the Edit bit in the Ff entry.
                *
                *Table 230 - Field flags specific to choice fields
                *
                *          [Bit position]              [Name]                  [Meaning]
                *
                *          18                          Combo                   If set, the field is a combo box; if clear, the field is a list box.
                *
                *          19                          Edit                    If set, the combo box shall include an editable text box as well as a drop-down list; if clear, it shall include only a drop-down list. 
                *                                                              This flag shall be used only if the Combo flag is set.
                *
                *          20                          Sort                    If set, the field’s option items shall be sorted alphabetically. This flag is intended for use by writers, not by readers. 
                *                                                              Conforming readers shall display the options in the order in which they occur in the Opt array (see Table 231).
                *
                *          22                          MultiSelect             (PDF 1.4) If set, more than one of the field’s option items may be selected simultaneously; if clear, at most one item shall be selected.
                *
                *          23                          DoNotSpellCheck         (PDF 1.4) If set, text entered in the field shall not be spell-checked. This flag shall not be used unless the Combo and Edit flags are both set.
                *
                *          27                          CommitOnSelChange       (PDF 1.5) If set, the new value shall be committed as soon as a selection is made (commonly with the pointing device). 
                *                                                              In this case, supplying a value for a field involves three actions: selecting the field for fill-in, selecting a choice for the fill-in value, and leaving that field, which finalizes or “commits” the data choice and triggers any actions associated with the entry or changing of this data. 
                *                                                              If this flag is on, then processing does not wait for leaving the field action to occur, but immediately proceeds to the third step.
                *                                                              This option enables applications to perform an action once a selection is made, without requiring the user to exit the field. 
                *                                                              If clear, the new value is not committed until the user exits the field.
                *
                *The various types of choice fields are distinguished by flags in the Ff entry, as shown in Table 230. 
                *Table 231shows the field dictionary entries specific to choice fields.
                *
                *Table 231 - Additional entries specific to a choice field
                *
                *          [Key]               [Type]              [Value]
                *
                *          Opt                 array               (Optional) An array of options that shall be presented to the user. 
                *                                                  Each element of the array is either a text string representing one of the available options or an array consisting of two text strings: the option’s export value and the text that shall be displayed as the name of the option.
                *                                                  If this entry is not present, no choices should be presented to the user.
                *
                *          TI                  integer             (Optional) For scrollable list boxes, the top index (the index in the Opt array of the first option visible in the list). 
                *                                                  Default value: 0.
                *
                *          I                   array               (Sometimes required, otherwise optional; PDF 1.4) For choice fields that allow multiple selection (MultiSelect flag set), an array of integers, sorted in ascending order, representing the zero-based indices in the Opt array of the currently selected option items. 
                *                                                  This entry shall be used when two or more elements in the Opt array have different names but the same export value or when the value of the choice field is an array. 
                *                                                  This entry should not be used for choice fields that do not allow multiple selection. 
                *                                                  If the items identified by this entry differ from those in the V entry of the field dictionary (see discussion following this Table), the V entry shall be used.
                *
                *The Opt array specifies the list of options in the choice field, each of which shall be represented by a text string that shall be displayed on the screen. 
                *Each element of the Opt array contains either this text string by itself or a two-element array, whose second element is the text string and whose first element is a text string representing the export value that shall be used when exporting interactive form field data from the document.
                *The field dictionary’s V(value) entry(see Table 220) identifies the item or items currently selected in the choice field.
                *If the field does not allow multiple selection—that is, if the MultiSelect flag(PDF 1.4) is not set—or if multiple selection is supported but only one item is currently selected, V is a text string representing the name of the selected item, as given in the field dictionary’s Opt array. 
                *If multiple items are selected, V is an array of such strings. 
                *(For items represented in the Opt array by a two-element array, the name string is the second of the two array elements.) 
                *The default value of V is null, indicating that no item is currently selected.
                *
                *EXAMPLE       The following example shows a typical choice field definition.
                *
                *              << / FT / Ch
                *                 / Ff …
                *                 / T(Body Color)
                *                 / V(Blue)
                *                 / Opt    [(Red)
                *                           (My favorite color)
                *                           (Blue)
                *                          ]
                *              >>
                */

                /*12.7.4.5 Signature Fields
                *
                *A signature field(PDF 1.3) is a form field that contains a digital signature(see 12.8, “Digital Signatures”). 
                *The field dictionary representing a signature field may contain the additional entries listed in Table 232, as well as the standard entries described in Table 220.
                *The field type(FT) shall be Sig, and the field value(V), if present, shall be a signature dictionary containing the signature and specifying various attributes of the signature field(see Table 252).
                *
                *NOTE 1        This signature form field serves two primary purposes. 
                *              The first is to define the form field that will provide the visual signing properties for display but it also may hold information needed later when the actual signing takes place, such as the signature technology to use.
                *              This carries information from the author of the document to the software that later does the signing.
                *
                *NOTE 2        Filling in (signing)the signature field entails updating at least the V entry and usually also the AP entry of the associated widget annotation.
                *              Exporting a signature field typically exports the T, V, and AP entries.
                *
                *Like any other field, a signature field may be described by a widget annotation dictionary containing entries pertaining to an annotation as well as a field
                *(see 12.5.6.19, “Widget Annotations”).The annotation rectangle(Rect) in such a dictionary shall give the position of the field on its page.
                *Signature fields that are not intended to be visible shall have an annotation rectangle that has zero height and width.Conforming readers shall treat such signatures as not visible.
                *Conforming readers shall also treat signatures as not visible if either the Hiddenbit or the NoView bit of the F entry is true.
                *The F entry is described in Table 164, and annotation flags are described in Table 165.
                *
                *The appearance dictionary (AP) of a signature field’s widget annotation defines the field’s visual appearance on the page (see 12.5.5, “Appearance Streams”).
                *
                *Table 232 - Additional entries specific to a signature field
                *
                *              [Key]               [Type]                  [Value]
                *
                *              Lock                dictionary              (Optional; shall be an indirect reference; PDF 1.5) A signature field lock dictionary that specifies a set of form fields that shall be locked when this signature field is signed. 
                *                                                          Table 233 lists the entries in this dictionary.
                *
                *              SV                  dictionary              (Optional; shall be an indirect reference; PDF 1.5) A seed value dictionary (see Table 234) containing information that constrains the properties of a signature that is applied to this field.
                *
                *The signature field lock dictionary (described in Table 233) contains field names from the signature seed value dictionary (described in Table 234) that cannot be changed through the user interface of a conforming reader.
                *
                *Table 233 - Entries in signature field lock dictionary
                *
                *              [Key]               [Type]                  [Value]
                *
                *              Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be SigFieldLock for a signature field lock dictionary.
                *
                *              Action              name                    (Required) A name which, in conjunction with Fields, indicates the set of fields that should be locked. 
                *                                                          The value shall be one of the following:
                *
                *                                                          All         All fields in the document
                *                                                          Include     All fields specified in Fields
                *                                                          Exclude     All fields except those specified in Fields
                *
                *              Fields              array                   (Required if the value of Action is Include or Exclude) An array of text strings containing field names.
                *
                *The value of the SV entry in the field dictionary is a seed value dictionary whose entries (see Table 234) provide constraining information that shall be used at the time the signature is applied. 
                *Its Ff entry specifies whether the other entries in the dictionary shall be honoured or whether they are merely recommendations.
                *
                *The seed value dictionary may include seed values for private entries belonging to multiple handlers.
                *A given handler shall use only those entries that are pertinent to itself and ignore the others.
                *
                *Table 234 - Entries in a signature field seed value dictionary
                *
                *              [Key]               [Type]                  [Value]
                *
                *              Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be SV for a seed value dictionary.
                *      
                *              Ff                  integer                 (Optional) A set of bit flags specifying the interpretation of specific entries in this dictionary. 
                *                                                          A value of 1 for the flag indicates that the associated entry is a required constraint. 
                *                                                          A value of 0 indicates that the associated entry is an optional constraint. 
                *                                                          Bit positions are 1 (Filter); 2 (SubFilter); 3 (V); 4 (Reasons); 5 (LegalAttestation); 6 (AddRevInfo); and 7 (DigestMethod). 
                *                                                          Default value: 0.
                *
                *              Filter              name                    (Optional) The signature handler that shall be used to sign the signature field. 
                *                                                          Beginning with PDF 1.7, if Filter is specified and the Ff entry indicates this entry is a required constraint, then the signature handler specified by this entry shall be used when signing; otherwise, signing shall not take place. 
                *                                                          If Ff indicates that this is an optional constraint, this handler may be used if it is available. 
                *                                                          If it is not available, a different handler may be used instead.
                *
                *              SubFilter           array                   (Optional) An array of names indicating encodings to use when signing. 
                *                                                          The first name in the array that matches an encoding supported by the signature handler shall be the encoding that is actually used for signing. 
                *                                                          If SubFilter is specified and the Ff entry indicates that this entry is a required constraint, then the first matching encodings shall be used when signing; otherwise, signing shall not take place. 
                *                                                          If Ff indicates that this is an optional constraint, then the first matching encoding shall be used if it is available. If none is available, a different encoding may be used.
                *
                *              DigestMethod        array                   (Optional; PDF 1.7) An array of names indicating acceptable digest algorithms to use while signing. 
                *                                                          The value shall be one of SHA1, SHA256, SHA384, SHA512 and RIPEMD160. The default value is implementation-specific.
                *                                                          This property is only applicable if the digital credential signing contains RSA public/private keys.
                *                                                          If it contains DSA public/ private keys, thedigest algorithm is always SHA1 and this attribute shall be ignored.
                *
                *              V                   real                    (Optional) The minimum required capability of the signature field seed value dictionary parser. 
                *                                                          A value of 1 specifies that the parser shall be able to recognize all seed value dictionary entries in a PDF 1.5 file. 
                *                                                          A value of 2 specifies that it shall be able to recognize all seed value dictionary entries specified.
                *                                                          The Ff entry indicates whether this shall be treated as a required constraint.
                *
                *              Cert                dictionary              (Optional) A certificate seed value dictionary (see Table 235) containing information about the characteristics of the certificate that shall be used when signing.
                *
                *              Reasons             array                   (Optional) An array of text strings that specifying possible reasons for signing a document. 
                *                                                          If specified, the reasons supplied in this entry replace those used by conforming products.
                *                                                          If the Reasons array is provided and the Ff entry indicates that Reasons is a required constraint, one of the reasons in the array shall be used for the signature dictionary; otherwise, signing shall not take place.
                *                                                          If the Ff entry indicates Reasons is an optional constraint, one of the reasons in the array may be chosen or a custom reason can be provided.
                *                                                          If the Reasons array is omitted or contains a single entry with the value PERIOD(2Eh) and the Ff entry indicates that Reasons is a required constraint, the Reason entry shall be omitted from the signature dictionary(see Table 252).
                *
                *              MDP                 dictionary              (Optional; PDF 1.6) A dictionary containing a single entry whose key is P and whose value is an integer between 0 and 3. 
                *                                                          A value of 0 defines the signature as an author signature (see 12.8, “Digital Signatures”). 
                *                                                          The values 1 through 3 shall be used for certification signatures and correspond to the value of P in a DocMDP transform parameters dictionary (see Table 254).
                *                                                          If this MDP key is not present or the MDP dictionary does not contain a P entry, no rules shall be defined regarding the type of signature or its permissions.
                *
                *              TimeStamp           dictionary              (Optional; PDF 1.6) A time stamp dictionary containing two entries:
                *
                *                                                          URL         An ASCII string specifying the URL of a time - stamping server, providing a time stamp that is compliant with RFC 3161, Internet X.509 Public Key Infrastructure Time-Stamp Protocol(see the Bibliography).
                *
                *                                                          Ff          An integer whose value is 1(the signature shall have a time stamp) or 0(the signature need not have a time stamp).Default value: 0.
                *
                *                                                          NOTE        Please see 12.8.3.3, "PKCS#7 Signatures as used in ISO 32000" for more details about hashing.
                *
                *              LegalAttestation    array                   (Optional; PDF 1.6) An array of text strings specifying possible legal attestations (see 12.8.5, “Legal Content Attestations”). 
                *                                                          The value of the corresponding flag in the Ff entry indicates whether this is a required constraint.
                *
                *              AddRevInfo          boolean                 (Optional; PDF 1.7) A flag indicating whether revocation checking shall be carried out. 
                *                                                          If AddRevInfo is true, the conforming processor shall perform the following additional tasks when signing the signature field:
                *
                *                                                          Perform revocation checking of the certificate(and the corresponding issuing certificates) used to sign.
                *
                *                                                          Include the revocation information within the signature value.
                *
                *                                                          Three SubFilter values have been defined for ISO 32000.For those values the AddRevInfo value shall be true only if SubFilter is adbe.pkcs7.detached or adbe.pkcs7.sha1.
                *                                                          If SubFilter is x509.rsa_sha1, this entry shall be omitted or set to false.Additional SubFilters may be defined that also use AddRevInfo values.
                *
                *                                                          If AddRevInfo is true and the Ff entry indicates this is a required constraint, then the preceding tasks shall be performed.
                *                                                          If they cannot be performed, then signing shall fail.
                *
                *                                                          Default value: false
                *
                *                                                          NOTE 1      Revocation information is carried in the signature data as specified by PCKS#7. 
                *                                                                      See 12.8.3.3, "PKCS#7 Signatures as used in ISO 32000".
                *
                *                                                          NOTE 2      The trust anchors used are determined by the signature handlers for both creation and validation.
                *
                *For optional keys that are not present, no constraint shall be placed upon the signature handler for that property when signing.
                *
                *Table 235 - Entries in a certificate seed value dictionary
                *
                *              [Key]               [Type]                  [Value]
                *
                *              Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be SVCert for a certificate seed value dictionary.
                *
                *              Ff                  integer                 (Optional) A set of bit flags specifying the interpretation of specific entries in this dictionary. 
                *                                                          A value of 1 for the flag means that a signer shall be required to use only the specified values for the entry. A value of 0 means that other values are permissible. 
                *                                                          Bit positions are 1 (Subject); 2 (Issuer); 3 (OID); 4 (SubjectDN); 5 (Reserved); 6 (KeyUsage); 7 (URL).
                *                                                          Default value: 0.
                *
                *              Subject             array                   (Optional) An array of byte strings containing DER-encoded X.509v3 certificates that are acceptable for signing. X.509v3 certificates are described in RFC 3280, Internet X.509 Public Key Infrastructure, Certificate and Certificate Revocation List (CRL) Profile (see the Bibliography). 
                *                                                          The value of the corresponding flag in the Ff entry indicates whether this is a required constraint.
                *
                *              SubjectDN           array of                (Optional; PDF 1.7) An array of dictionaries, each specifying a Subject Distinguished Name (DN) that shall be present within the certificate for it to be acceptable for signing. 
                *                                  dictionaries            The certificate ultimately used for the digital signature shall contain all the attributes specified in each of the dictionaries in this array. 
                *                                                          (PDF keys and values are mapped to certificate attributes and values.) The certificate is not constrained to use only attribute entries from these dictionaries but may contain additional attributes.
                *                                                          The Subject Distinguished Name is described in RFC 3280 (see the Bibliography). 
                *                                                          The key can be any legal attribute identifier (OID). Attribute names shall contain characters in the set a-z A-Z 0-9 and PERIOD.
                *                                                          Certificate attribute names are used as key names in the dictionaries in this array.
                *                                                          Values of the attributes are used as values of the keys.Values shall be text strings.
                *                                                          The value of the corresponding flag in the Ff entry indicates whether this entry is a required constraint.
                *                                      
                *              KeyUsage            array of ASCII          (Optional; PDF 1.7) An array of ASCII strings, where each string specifies an acceptable key-usage extension that shall be present in the signing certificate. 
                *                                  strings                 Multiple strings specify a range of acceptable key-usage extensions. The key-usage extension is described in RFC 3280.
                *                                                          Each character in a string represents a key-usage type, where the order of the characters indicates the key - usage extension it represents.
                *                                                          The first through ninth characters in the string, from left to right, represent the required value for the following key - usage extensions:
                *
                *                                                          1 digitalSignature      4   dataEncipherment    7   cRLSign
                *                                                          2 non - Repudiation     5   keyAgreement        8   encipherOnly
                *                                                          3 keyEncipherment       6   keyCertSign         9   decipherOnly
                *
                *                                                          Any additional characters shall be ignored.
                *                                                          Any missing characters or characters that are not one of the following values, shall be treated as ‘X’. 
                *                                                          The following character values shall be supported:
                *
                *                                                          0   Corresponding key - usage shall not be set.
                *                                                          1   Corresponding key - usage shall be set.
                *                                                          X   State of the corresponding key - usage does not matter.
                *
                *                                                          EXAMPLE 1   The string values ‘1’ and ‘1XXXXXXXX’ represent settings where the key - usage type digitalSignature is set and the state of all other key - usage types do not matter.
                *
                *                                                          The value of the corresponding flag in the Ff entry indicates whether this is a required constraint.
                *                                  
                *               Issuer               array                 (Optional) An array of byte strings containing DER-encoded X.509v3 certificates of acceptable issuers. 
                *                                                          If the signer’s certificate refers to any of the specified issuers (either directly or indirectly), the certificate shall be considered acceptable for signing. 
                *                                                          The value of the corresponding flag in the Ff entry indicates whether this is a required constraint.
                *                                                          This array may contain self - signed certificates.
                *
                *               OID                  array                 (Optional) An array of byte strings that contain Object Identifiers (OIDs) of the certificate policies that shall be present in the signing certificate.
                *
                *                                                          EXAMPLE 2   An example of such a string is: (2.16.840.1.113733.1.7.1.1).
                *
                *                                                          This field shall only be used if the value of Issuer is not empty. 
                *                                                          The certificate policies extension is described in RFC 3280(see the Bibliography).
                *                                                          The value of the corresponding flag in the Ff entry indicates whether this is a required constraint.
                *
                *               URL                  ASCII string          (Optional) A URL, the use for which shall be defined by the URLTypeentry.
                *
                *               URLType              Name                  (Optional; PDF 1.7) A name indicating the usage of the URL entry. 
                *                                                          There are standard uses and there can be implementation-specific uses for this URL. 
                *                                                          The following value specifies a valid standard usage:
                *                                                          Browser – The URL references content that shall be displayed in a web browser to allow enrolling for a new credential if a matching credential is not found. 
                *                                                          The Ff attribute’s URL bit shall be ignored for this usage.
                *                                                          Third parties may extend the use of this attribute with their own attribute values, which shall conform to the guidelines described in Annex E.
                *                                                          The default value is Browser.
                */
            
            /*12.7.5 Form Actions
            */
              
                /*12.7.5.1 General
                *
                *Interactive forms also support special types of actions in addition to those described in 12.6.4, “Action Types”:
                *
                *  •   submit - form action
                *
                *  •   reset - form action
                *
                *  •   import - data action
                */

                /*12.7.5.2  Submit - Form Action
                *
                *Upon invocation of a submit-form action, a conforming processor shall transmit the names and values of selected interactive form fields to a specified uniform resource locator(URL),
                *
                *NOTE      Presumably, the URL is the address of a Web server that will process them and send back a response.
                *
                *The value of the action dictionary’s Flags entry shall be an non - negative integer containing flags specifying various characteristics of the action.
                *Bit positions within the flag word shall be numbered starting with 1(low - order).
                *Table 237 shows the meanings of the flags; all undefined flag bits shall be reserved and shall be set to 0.
                *
                *Table 236 - Additional entries specific to a submit-form action
                *
                *          [Key]               [Type]                      [Value]
                *
                *          S                   name                        (Required) The type of action that this dictionary describes; shall be SubmitForm for a submit-form action.
                *
                *          F                   file specification          (Required) A URL file specification (see 7.11.5, “URL Specifications”) giving the uniform resource locator (URL) of the script at the Web server that will process the submission.
                *
                *          Fields              array                       (Optional) An array identifying which fields to include in the submission or which to exclude, depending on the setting of the Include/Exclude flag in the Flags entry (see Table 237). 
                *                                                          Each element of the array shall be either an indirect reference to a field dictionary or (PDF 1.3) a text string representing the fully qualified name of a field. 
                *                                                          Elements of both kinds may be mixed in the same array.
                *                                                          If this entry is omitted, the Include/ Exclude flag shall beignored, and all fields in the document’s interactive form shall be submitted except those whose NoExport flag(see Table 221) is set.
                *                                                          Fields with no values may also be excluded, as dictated by the value of the IncludeNoValueFields flag; see Table 237.
                *
                *          Flags               integer                     (Optional; inheritable) A set of flags specifying various characteristics of the action (see Table 237). 
                *                                                          Default value: 0.
                *
                *Table 237 - Flags for submit-form actions
                *
                *          [Bit position]      [Name]                      [Meaning]
                *
                *          1                    Include/Exclude            If clear, the Fields array (see Table 236) specifies which fields to include in the submission. (All descendants of the specified fields in the field hierarchy shall be submitted as well.)
                *                                                          If set, the Fields array tells which fields to exclude.
                *                                                          All fields in the document’s interactive form shall be submitted exceptthose listed in the Fields array and those whose NoExport flag(see Table 221) is set and fields with no values if the IncludeNoValueFields flag is clear.
                *
                *          2                   IncludeNoValueFields        If set, all fields designated by the Fields array and the Include/Exclude flag shall be submitted, regardless of whether they have a value (V entry in the field dictionary). 
                *                                                          For fields without a value, only the field name shall betransmitted.
                *                                                          If clear, fields without a value shall not be submitted.
                *
                *          3                   ExportFormat                Meaningful only if the SubmitPDF and XFDF flags are clear. 
                *                                                          If set, field names and values shall be submitted in HTML Form format. 
                *                                                          If clear, they shall be submitted in Forms Data Format (FDF); see 12.7.7, “Forms Data Format.”
                *
                *          4                   GetMethod                   If set, field names and values shall be submitted using an HTTP GET request. 
                *                                                          If clear, they shall be submitted using a POST request. 
                *                                                          This flag is meaningful only when the ExportFormat flag is set; if ExportFormat is clear, this flag shall also be clear.
                *
                *          5                   SubmitCoordinates           If set, the coordinates of the mouse click that caused the submit-form action shall be transmitted as part of the form data. 
                *                                                          The coordinate values are relative to the upper-left corner of the field’s widget annotation rectangle. They shall be represented in the data in the format
                *
                *                                                          name.x = xval & name.y = yval
                *
                *                                                          where name is the field’s mapping name(TM in the field dictionary) if present; otherwise, name is the field name.
                *                                                          If the value of the TM entry is a single ASCII SPACE(20h) character, both the name and the ASCII PERIOD(2Eh)following it shall be suppressed, resulting in the format
                *
                *                                                          x = xval & y = yval
                *      
                *                                                          This flag shall be used only when the ExportFormat flag is set.
                *                                                          If ExportFormat is clear, this flag shall also be clear.
                *
                *          6                   XFDF                        (PDF 1.4) shall be used only if the SubmitPDF flags are clear. If set, field names and values shall be submitted as XFDF.
                *
                *          7                   IncludeAppendSaves          (PDF 1.4) shall be used only when the form is being submitted in Forms Data Format (that is, when both the XFDF and ExportFormat flags are clear). 
                *                                                          If set, the submitted FDF file shall include the contents of all incremental updates to the underlying PDF document, as contained in the Differences entry in the FDF dictionary (see Table 243). 
                *                                                          If clear, the incremental updates shall not be included.
                *
                *          8                   IncludeAnnotations          (PDF 1.4) shall be used only when the form is being submitted in Forms Data Format (that is, when both the XFDF and ExportFormat flags are clear). 
                *                                                          If set, the submitted FDF file shall include includes all markup annotations in the underlying PDF document (see 12.5.6.2, “Markup Annotations”). 
                *                                                          If clear, markup annotations shall not beincluded.
                *
                *          9                   SubmitPDF                   (PDF 1.4) If set, the document shall be submitted as PDF, using the MIME content type application/pdf (described in Internet RFC 2045, Multipurpose Internet Mail Extensions (MIME), Part One: Format of Internet Message Bodies; see the Bibliography). 
                *                                                          If set, all other flags shall be ignored except GetMethod.
                *
                *          10                  CanonicalFormat             (PDF 1.4) If set, any submitted field values representing dates shall be converted to the standard format described in 7.9.4, “Dates.”
                *
                *                                                          NOTE 1      The interpretation of a form field as a date is not specified explicitly in the field itself but only in the JavaScript code that processes it.
                *
                *          11                   ExclNonUserAnnots          (PDF 1.4) shall be used only when the form is being submitted in Forms Data Format (that is, when both the XFDF and ExportFormat flags are clear) and the IncludeAnnotations flag is set. 
                *                                                          If set, it shall include only those markup annotations whose T entry (see Table 170) matches the name of the current user, as determined by the remote server to which the form is being submitted.
                *
                *                                                          NOTE 2      The T entry for markup annotations specifies the text label that is displayed in the title bar of the annotation’s pop - up window and is assumed to represent the name of the user authoring the annotation.
                *
                *                                                          NOTE 3      This allows multiple users to collaborate in annotating a single remote PDF document without affecting one another’s annotations.
                *
                *          12                   ExclFKey                   (PDF 1.4) shall be used only when the form is being submitted in Forms Data Format (that is, when both the XFDF and ExportFormat flags are clear). 
                *                                                          If set, the submitted FDF shall exclude the F entry.
                *
                *          14                  EmbedForm                   (PDF 1.5) shall be used only when the form is being submitted in Forms Data Format (that is, when both the XFDF and ExportFormat flags are clear). 
                *                                                          If set, the F entry of the submitted FDF shall be a file specification containing an embedded file stream representing the PDF file from which the FDF is being submitted.
                *
                *The set of fields whose names and values are to be submitted shall be defined by the Fields array in the action dictionary (Table 236) together with the Include/Exclude and IncludeNoValueFields flags in the Flags entry (Table 237). 
                *Each element of the Fields array shall identify an interactive form field, either by an indirect reference to its field dictionary or (PDF 1.3) by its fully qualified field name (see 12.7.3.2, “Field Names”). 
                *If the Include/Exclude flag is clear, the submission consists of all fields listed in the Fields array, along with any descendants of those fields in the field hierarchy. 
                *If the Include/Exclude flag is set, the submission shall consist of all fields in the document’s interactive form except those listed in the Fields array.
                *
                *The NoExport flag in the field dictionary’s Ff entry(see Table 220 and Table 221) takes precedence over the action’s Fields array and Include / Exclude flag.Fields whose NoExport flag is set shall not be included in a submit-form action.
                *
                *Field names and values may be submitted in any of the following formats, depending on the settings of the action’s ExportFormat, SubmitPDF, and XFDF flags(see the Bibliography for references):
                *
                *  •   HTML Form format(described in the HTML 4.01 Specification)
                *
                *  •   Forms Data Format(FDF), which is described in 12.7.7, “Forms Data Format.”
                *
                *  •   XFDF, a version of FDF based on XML.XFDF is described in the Adobe technical note XML Forms Data Format Specification, Version 2.0.XML is described in the W3C document Extensible Markup Language(XML) 1.1)
                *
                *  •   PDF(in this case, the entire document shall be submitted rather than individual fields and values).
                *
                *The name submitted for each field shall be its fully qualified name(see 12.7.3.2, “Field Names”), and the value shall be specified by the V entry in its field dictionary.
                *
                *For pushbutton fields submitted in FDF, the value submitted shall be that of the AP entry in the field’s widget annotation dictionary. 
                *If the submit-form action dictionary contains no Fields entry, such pushbutton fields shall not be submitted.
                *
                *Fields with no value(that is, whose field dictionary does not contain a V entry) are ordinarily not included in the submission. 
                *The submit-form action’s IncludeNoValueFields flag may override this behaviour.
                *If this flag is set, such valueless fields shall be included in the submission by name only, with no associated value.
                *
                *12.7.5.3 Reset-Form Action
                *
                *Upon invocation of a reset-form action, a conforming processor shall reset selected interactive form fields to their default values; that is, it shall set the value of the V entry in the field dictionary to that of the DV entry (see Table 220). 
                *If no default value is defined for a field, its V entry shall be removed. For fields that can have no value (such as pushbuttons), the action has no effect. Table 238 shows the action dictionary entries specific to this type of action.
                *
                *The value of the action dictionary’s Flags entry is a non - negative containing flags specifying various characteristics of the action. 
                *Bit positions within the flag word shall be numbered starting from 1(low - order).Only one flag is defined for this type of action.
                *All undefined flag bits shall be reserved and shall be set to 0.
                *
                *Table 238 - Additional entries specific to a reset-form action
                *
                *          [Key]               [Type]              [Value]
                *
                *          S                   name                (Required) The type of action that this dictionary describes; shallbe ResetForm for a reset-form action.
                *
                *          Fields              array               (Optional) An array identifying which fields to reset or which to exclude from resetting, depending on the setting of the Include/Exclude flag in the Flags entry (see Table 239). 
                *                                                  Each element of the array shall be either an indirect reference to a field dictionary or (PDF 1.3) a text string representing the fully qualified name of a field. 
                *                                                  Elements of both kinds may be mixed in the same array.
                *                                                  If this entry is omitted, the Include/ Exclude flag shall be ignored; all fields in the document’s interactive form are reset.
                *
                *          Flags               integer             (Optional; inheritable) A set of flags specifying various characteristics of the action (see Table 239). 
                *                                                  Default value: 0.
                *
                *Table 239 - Flag for reset-form actions
                *
                *          [Bit position]       [Name]             [Meaning]
                *
                *          1                    Include/Exclude    If clear, the Fields array (see Table 238) specifies which fields to reset. 
                *                                                  (All descendants of the specified fields in the field hierarchy are reset as well.) 
                *                                                  If set, the Fields array indicates which fields to exclude from resetting; that is, all fields in the document’s interactive form shall be reset except those listed in the Fieldsarray.
                */

                /*12.7.5.4 Import-Data Action
                *
                *Upon invocation of an import-data action, a conforming processor shall import Forms Data Format (FDF) data into the document’s interactive form from a specified file.
                *
                *Table 240 - Additional entries specific to an import-data action
                *
                *          [Key]               [Type]                      [Value]
                *
                *          S                   name                        (Required) The type of action that this dictionary describes; shall be ImportData for an import-data action.
                *
                *          F                   file specification          (Required) The FDF file from which to import the data.
                */
            
            /*12.7.6 Named Pages
            *
            *The optional Pages entry (PDF 1.3) in a document’s name dictionary (see 7.7.4, “Name Dictionary”) contains a name tree that maps name strings to individual pages within the document. 
                *Naming a page allows it to be referenced in two different ways:
                *
                *  •   An import-data action can add the named page to the document into which FDF is being imported, either as a page or as a button appearance.
                *
                *  •   A script executed by a JavaScript action can add the named page to the current document as a regular page.
                *
                *A named page that is intended to be visible to a user shall be left in the page tree(see 7.7.3, “Page Tree”), and there shall be a reference to it in the appropriate leaf node of the name dictionary’s Pages tree.
                *If the page is not intended to be displayed by the reader, it shall be referenced from the name dictionary’s Templates tree instead. 
                *Such invisible pages shall have an object type of Template rather than Page and shall have no Parentor B entry(see Table 30).
                */

            /*12.7.7 Forms Data Format
            */
            
                /*12.7.7.1 General
                *
                *This sub-clause describes Forms Data Format(FDF), the file format used for interactive form data(PDF 1.2).
                *FDF can be used when submitting form data to a server, receiving the response, and incorporating it into the interactive form.
                *It can also be used to export form data to stand - alone files that can be stored, transmitted electronically, and imported back into the corresponding PDF interactive form.
                *In addition, beginning in PDF 1.3, FDF can be used to define a container for annotations that are separate from the PDF document to which they apply.
                *
                *FDF is based on PDF; it uses the same syntax and has essentially the same file structure(7.5, “File Structure”). However, it differs from PDF in the following ways:
                *
                *  •   The cross-reference table(7.5.4, “Cross - Reference Table”) is optional.
                *
                *  •   FDF files shall not be updated(see 7.5.6, “Incremental Updates”). 
                *      Objects shall only be of generation 0, and no two objects within an FDF file shall have the same object number.
                *
                *  •   The document structure is much simpler than PDF, since the body of an FDF document consists of only one required object.
                *
                *  •   The length of a stream shall not be specified by an indirect object.
                *
                *FDF uses the MIME content type application / vnd.fdf.On the Windows and UNIX platforms, FDF files have the extension.fdf; on Mac OS, they have file type 'FDF '.
                */

                /*12.7.7.2 FDF File Structure
                */

                    /*12.7.7.2.1 General
                *
                *An FDF file shall be structured in essentially the same way as a PDF file but contains only those elements required for the export and import of interactive form and annotation data.
                *It consists of three required elements and one optional element(see Figure 65):
                *
                *  •   A one - line header identifying the version number of the PDF specification to which the file conforms
                *
                *  •   A body containing the objects that make up the content of the file
                *
                *  •   An optional cross - reference table containing information about the indirect objects in the file
                *
                *  •   An optional trailer giving the location of the cross - reference table and of certain special objects within the body of the file
                *
                *(see Figure 65 - FDF file structure, on page 457)
                */

                    /*12.7.7.2.2 FDF Header
                *
                *The first line of an FDF file shall be a header, which shall contain
                *
                *      % FDF - 1.2
                *
                *The version number is given by the Version entry in the FDF catalogue dictionary(see 12.7.7.3, “FDF Catalog”).
                */

                    /*12.7.7.2.3 FDF Body
                *
                *The body of an FDF file shall consist of a sequence of indirect objects representing the file’s catalogue(see 12.7.7.3, “FDF Catalog”) and any additional objects that the catalogue references. 
                *The objects are of the same basic types described in 7.5, “File Structure” (other than the % PDF–n.m and %% EOF comments described in 7.5, “File Structure”) have no semantics.
                *They are not necessarily preserved by applications that edit PDF files.” Just as in PDF, objects in FDF can be direct or indirect.
                */

                    /*12.7.7.2.4 FDF Trailer
                *
                *The trailer of an FDF file enables a reader to find significant objects quickly within the body of the file. 
                *The last line of the file contains only the end-of - file marker, %% EOF.
                *This marker shall be preceded by the FDF trailer dictionary, consisting of the keyword trailer followed by a series of one or more key-value pairs enclosed in double angle brackets(<<…>>)(using LESS - THAN SIGNs(3Ch) and GREATER-THAN SIGNs(3Eh)). 
                *The only required key is Root, whose value is an indirect reference to the file’s catalogue dictionary(see Table 242).
                *The trailer may optionally contain additional entries for objects that are referenced from within the catalogue.
                *
                *Table 241 - Entry in the FDF trailer dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Root                dictionary          (Required; shall be an indirect reference) The Catalog object for this FDF file (see 12.7.7.3, “FDF Catalog”).
                *
                *Thus, the trailer has the overall structure
                *
                *      trailer
                *          << / Root c 0 R
                *             key2 value2
                *              …
                *              keyn valuen
                *          >>
                *      %% EOF
                *
                *where c is the object number of the file’s catalogue dictionary.
                */

                /*12.7.7.3 FDF Catalog
                */

                    /*12.7.7.3.1 General
                *
                *The root node of an FDF file’s object hierarchy is the Catalog dictionary, located by means of the Root entry in the file’s trailer dictionary(see FDF Trailer in 12.7.7.2, “FDF File Structure”).
                *As shown in Table 241, the only required entry in the catalogue is FDF; its value shall be an FDF dictionary(Table 243), which in turn shall contain references to other objects describing the file’s contents. 
                *The catalogue may also contain an optional Version entry identifying the version of the PDF specification to which this FDF file conforms.
                *
                *Table 242 - Entries in the FDF catalog dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Version             name                    (Optional; PDF 1.4) The version of the FDF specification to which this FDF file conforms (for example, 1.4) if later than the version specified in the file’s header (see FDF Header in 12.7.7.2, “FDF File Structure”). 
                *                                                      If the header specifies a later version, or if this entry is absent, the document conforms to the version specified in the header.
                *                                                      The value of this entry is a name object, not a number, and therefore shall be preceded by a slash character(/) when written in the FDF file(for example, / 1.4).
                *
                *          FDF                 dictionary              (Required) The FDF dictionary for this file (see Table 243).
                *
                *          F                   file specification      (Optional) The source file or target file: the PDF document file that this FDF file was exported from or is intended to be imported into.
                *
                *          ID                  array                   (Optional) An array of two byte strings constituting a file identifier (see 14.4, “File Identifiers”) for the source or target file designated by F, taken from the ID entry in the file’s trailer dictionary (see 7.5.5, “File Trailer”).
                *
                *          Fields              array                   (Optional) An array of FDF field dictionaries (see FDF Fieldsin 12.7.7.3, “FDF Catalog”) describing the root fields (those with no ancestors in the field hierarchy) that shall be exported or imported. 
                *                                                      This entry and the Pages entry shall not both be present.
                *
                *          Status              PDFDocEncoded string    (Optional) A status string that shall be displayed indicating the result of an action, typically a submit-form action (see 12.7.5.2, “Submit-Form Action”). 
                *                                                      The string shall be encoded with PDFDocEncoding. This entry and the Pages entry shallnot both be present.
                *
                *          Pages               array                   (Optional; PDF 1.3) An array of FDF page dictionaries (see FDF Pages in 12.7.7.3, “FDF Catalog”) describing pages that shall be added to a PDF target document. 
                *                                                      The Fields and Status entries shall not be present together with this entry.
                *
                *          Encoding            name                    (Optional; PDF 1.3) The encoding that shall be used for any FDF field value or option (V or Opt in the field dictionary; see Table 246) or field name that is a string and does not begin with the Unicode prefix U+FEFF.
                *                                                      Default value: PDFDocEncoding.
                *                                                      Other allowed values include Shift_JIS, BigFive, GBK, UHC, utf_8, utf_16
                *
                *          Annots              array                   (Optional; PDF 1.3) An array of FDF annotation dictionaries (see FDF Annotation Dictionaries in 12.7.7.3, “FDF Catalog”). 
                *                                                      The array may include annotations of any of the standard types listed in Table 169 except Link, Movie, Widget, PrinterMark, Screen, and TrapNet.
                *
                *          Differences         stream                  (Optional; PDF 1.4) A stream containing all the bytes in all incremental updates made to the underlying PDF document since it was opened (see 7.5.6, “Incremental Updates”). 
                *                                                      If a submit-form action submitting the document to a remote server as FDF has its IncludeAppendSaves flag set (see 12.7.5.2, “Submit-Form Action”), the contents of this stream shall be included in the submission. 
                *                                                      This allows any digital signatures (see 12.8, “Digital Signatures”) to be transmitted to the server. 
                *                                                      An incremental update shall be automatically performed just before the submission takes place, in order to capture all changes made to the document.
                *                                                      The submission shall include the full set of incremental updates back to the time the document was first opened, even if some of them may already have been included in intervening submissions.
                *                                                      Although a Fields or Annots entry(or both) may be present along with Differences, there is no requirement that their contents will be consistent with each other.
                *                                                      In particular, if Differences contains a digital signature, only the values of the form fields given in the Differences stream shall be considered trustworthy under that signature.
                *
                *          Target              string                  (Optional; PDF 1.4) The name of a browser frame in which the underlying PDF document shall be opened. This mimics the behaviour of the target attribute in HTML <href> tags.
                *
                *          EmbeddedFDFs        array                   (Optional; PDF 1.4) An array of file specifications (see 7.11, “File Specifications”) representing other FDF files embedded within this one (7.11.4, “Embedded File Streams”).
                *
                *          JavaScript          dictionary              (Optional; PDF 1.4) A JavaScript dictionary (see Table 245) defining document-level JavaScript scripts.
                *
                *
                *Embedded FDF files specified in the FDF dictionary’s EmbeddedFDFs entry may be encrypted. 
                *Besides the usual entries for an embedded file stream, the stream dictionary representing such an encrypted FDF file shall contain the additional entry shown in Table 244 to identify the revision number of the FDF encryption algorithm used to encrypt the file. 
                *Although the FDF encryption mechanism is separate from the one for PDF file encryption described in 7.6, “Encryption,” revision 1 (the only one defined) uses a similar RC4 encryption algorithm based on a 40-bit encryption key. 
                *The key shall be computed by means of an MD5 hash, using a padded user-supplied password as input. 
                *The computation shall be identical to steps (a) and (b) of the "Algorithm 2: Computing an encryption key" in 7.6.3.3, "Encryption Key Algorithm"; the first 5 bytes of the result shall be the encryption key for the embedded FDF file.
                *
                *Table 244 - Additional entry in an embedded file stream dicitonary for an encrypted FDF file
                *
                *          [Key]                       [Type]              [Value]
                *
                *          EncryptionRevision          integer             (Required if the FDF file is encrypted; PDF 1.4) The revision number of the FDF encryption algorithm used to encrypt the file. 
                *                                                          This value shall be defined at the time of publication is 1.
                *
                *The JavaScript entry in the FDF dictionary holds a JavaScript dictionary containing JavaScript scripts that shall be defined globally at the document level, rather than associated with individual fields. 
                *The dictionary may contain scripts defining JavaScript functions for use by other scripts in the document, as well as scripts that shall be executed immediately before and after the FDF file is imported. 
                *Table 245 shows the contents of this dictionary.
                *
                *Table 245 - Entries in the JavaScript dictionary
                *
                *          [Key]                   [Type]              [Value]
                *
                *          Before                  text string or      (Optional) A text string or text stream containing a JavaScript script that shall be executed just before the FDF file is imported.
                *                                  text stream
                *
                *          After                   text string or      (Optional) A text string or text stream containing a JavaScript script that shall be executed just after the FDF file is imported.
                *                                  text stream
                *
                *          AfterPermsReady         text string or      (Optional; PDF 1.6) A text string or text stream containing a JavaScript script that shall be executed after the FDF file is imported and the usage rights in the PDF document have been determined (see 12.8.2.3, “UR”).
                *                                  text stream         Verification of usage rights requires the entire file to be present, in which case execution of this script shall be deferred until that requirement is met.
                *
                *          Doc                     array               (Optional) An array defining additional JavaScript scripts that shall be added to those defined in the JavaScript entry of the document’s name dictionary (see 7.7.4, “Name Dictionary”). 
                *                                                      The array shall contain an even number of elements, organized in pairs. 
                *                                                      The first element of each pair shall be a name and the second shall be a text string or text stream defining the script corresponding to that name. 
                *                                                      Each of the defined scripts shall be added to those already defined in the name dictionary and shall then be executed before the script defined in the Before entry is executed.
                *
                *                                                      NOTE    As described in 12.6.4.16, “JavaScript Actions,” these scripts are used to define JavaScript functions for use by other scripts in the document.
                */

                    /*12.7.7.3.2 FDF Fields
                *
                *Each field in an FDF file shall be described by an FDF field dictionary.
                *Table 246 shows the contents of this type of dictionary.
                *Most of the entries have the same form and meaning as the corresponding entries in a field dictionary(Table 220, Table 222, Table 229, and Table 231) or a widget annotation dictionary(Table 168 and Table 188).
                *Unless otherwise indicated in the table, importing a field causes the values of the entries in the FDF field dictionary to replace those of the corresponding entries in the field with the same fully qualified name in the target document.
                *
                *Table 246 - Entries in an FDF field dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Kids                array               (Optional) An array containing the immediate children of this field.
                *                                                  Unlike the children of fields in a PDF file, which shall be specified as indirect object references, those of an FDF field may be either direct or indirect objects.
                *
                *          T                   text string         (Required) The partial field name (see 12.7.3.2, “Field Names”).
                *
                *          V                   (various)           (Optional) The field’s value, whose format varies depending on the field type; see the descriptions of individual field types in 12.7.4, “Field Types” for further information.
                *
                *          Ff                  integer             (Optional) A set of flags specifying various characteristics of the field (see Table 221, Table 226, Table 228, and Table 230). 
                *                                                  When imported into an interactive form, the value of this entry shall replace that of the Ff entry in the form’s corresponding field dictionary. 
                *                                                  If this field is present, the SetFf and ClrFfentries, if any, shall be ignored.
                *
                *          SetFf               integer             (Optional) A set of flags to be set (turned on) in the Ff entry of the form’s corresponding field dictionary. 
                *                                                  Bits equal to 1 in SetFf shall cause the corresponding bits in Ff to be set to 1. 
                *                                                  This entry shall be ignored if an Ff entry is present in the FDF field dictionary.
                *
                *          ClrFf               integer             (Optional) A set of flags to be cleared (turned off) in the Ff entry of the form’s corresponding field dictionary. 
                *                                                  Bits equal to 1 in ClrFf shall cause the corresponding bits in Ff to be set to 0. If a SetFf entry is also present in the FDF field dictionary, it shall be applied before this entry. 
                *                                                  This entry shall beignored if an Ff entry is present in the FDF field dictionary.
                *
                *          F                   integer             (Optional) A set of flags specifying various characteristics of the field’s widget annotation (see 12.5.3, “Annotation Flags”). 
                *                                                  When imported into an interactive form, the value of this entry shall replace that of the F entry in the form’s corresponding annotation dictionary. 
                *                                                  If this field is present, the SetF and ClrFentries, if any, shall be ignored.
                *
                *          SetF                integer             (Optional) A set of flags to be set (turned on) in the F entry of the form’s corresponding widget annotation dictionary. 
                *                                                  Bits equal to 1 in SetF shall cause the corresponding bits in F to be set to 1. 
                *                                                  This entry shall be ignored if an Fentry is present in the FDF field dictionary.
                *
                *          ClrF                integer             (Optional) A set of flags to be cleared (turned off) in the F entry of the form’s corresponding widget annotation dictionary. 
                *                                                  Bits equal to 1 in ClrF shall cause the corresponding bits in F to be set to 0. 
                *                                                  If a SetF entry is also present in the FDF field dictionary, it shall be applied before this entry. 
                *                                                  This entry shall beignored if an F entry is present in the FDF field dictionary.
                *
                *          AP                  dictionary          (Optional) An appearance dictionary specifying the appearance of a pushbutton field (see Pushbuttons in 12.7.4.2, “Button Fields”). 
                *                                                  The appearance dictionary’s contents are as shown in Table 168, except that the values of the N, R, and D entries shall all be streams.
                *
                *          APRef               dictionary          (Optional; PDF 1.3) A dictionary holding references to external PDF files containing the pages to use for the appearances of a pushbutton field.
                *                                                  This dictionary is similar to an appearance dictionary (see Table 168), except that the values of the N, R, and D entries shall all be named page reference dictionaries (Table 250). 
                *                                                  This entry shall be ignored if an AP entry is present.
                *
                *          IF                  dictionary          (Optional; PDF 1.3; button fields only) An icon fit dictionary (see Table 247) specifying how to display a button field’s icon within the annotation rectangle of its widget annotation.
                *
                *          Opt                 array               (Required; choice fields only) An array of options that shall be presented to the user. 
                *                                                  Each element of the array shall take one of two forms:
                *
                *                                                  A text string representing one of the available options
                *
                *                                                  A two-element array consisting of a text string representing one of the available options and a default appearance string for constructing the item’s appearance dynamically at viewing time(see 12.7.3.3, “Variable Text”).
                *
                *          A                   dictionary          (Optional) An action that shall be performed when this field’s widget annotation is activated (see 12.6, “Actions”).
                *
                *          AA                  dictionary          (Optional) An additional-actions dictionary defining the field’s behaviour in response to various trigger events (see 12.6.3, “Trigger Events”).
                *
                *          RV                  text string or      (Optional; PDF 1.5) A rich text string, as described in 12.7.3.4, “Rich Text Strings.”
                *                              text stream
                *
                *In an FDF field dictionary representing a button field, the optional IF entry holds an icon fit dictionary (PDF 1.3)specifying how to display the button’s icon within the annotation rectangle of its widget annotation. 
                *Table 247shows the contents of this type of dictionary.
                *
                *Table 247 - Entries in an icon fit dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          SW                  name                (Optional) The circumstances under which the icon shall be scaled inside the annotation rectangle:
                *
                *                                                  A       Always scale.
                *
                *                                                  B       Scale only when the icon is bigger than the annotation rectangle.
                *
                *                                                  S       Scale only when the icon is smaller than the annotation rectangle.
                *
                *                                                  N       Never scale.
                *
                *                                                  Default value: A.
                *
                *          S                   name                (Optional) The type of scaling that shall be used:
                *
                *                                                  A   Anamorphic scaling:     Scale the icon to fill the annotation rectangle exactly, without regard to its original aspect ratio(ratio of width to height).
                *
                *                                                  P   Proportional scaling:   Scale the icon to fit the width or height of the annotation rectangle while maintaining the icon’s original aspect ratio. If the required horizontal and vertical scaling factors are different, use the smaller of the two, centering the icon within the annotation rectangle in the other dimension.
                *
                *                                                  Default value: P.
                *
                *          A                   array               (Optional) An array of two numbers that shall be between 0.0 and 1.0 indicating the fraction of leftover space to allocate at the left and bottom of the icon. 
                *                                                  A value of [0.0 0.0] shall position the icon at the bottom-left corner of the annotation rectangle. 
                *                                                  A value of [0.5 0.5] shall center it within the rectangle. 
                *                                                  This entry shall be used only if the icon is scaled proportionally. 
                *                                                  Default value: [0.5 0.5].
                *
                *          FB                  boolean             (Optional; PDF 1.5) If true, indicates that the button appearance shall be scaled to fit fully within the bounds of the annotation without taking into consideration the line width of the border. 
                *                                                  Default value: false.
                */

                    /*12.7.7.3.3 FDF Pages
                *
                *The optional Pages field in an FDF dictionary(see Table 243) shall contain an array of FDF page dictionaries(PDF 1.3) describing new pages that shall be added to the target document. 
                *Table 248 shows the contents of this type of dictionary.
                *
                *Table 248 - Entries in an FDF page dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Templates           array               (Required) An array of FDF template dictionaries (see Table 249) that shall describe the named pages that serve as templates on the page.
                *
                *          Info                dictionary          (Optional) An FDF page information dictionary that shall containadditional information about the page.
                *
                *An FDF template dictionary shall contain information describing a named page that serves as a template. Table 249 shows the contents of this type of dictionary.
                *
                *Table 249 - Entries in an FDF template dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          TRef                dictionary          (Required) A named page reference dictionary (see Table 250) that shall specify the location of the template.
                *
                *          Fields              array               (Optional) An array of references to FDF field dictionaries (see Table 246) describing the root fields that shall be imported (those with no ancestors in the field hierarchy).
                *
                *          Rename              boolean             (Optional) A flag that shall specify whether fields imported from the template shall be renamed in the event of name conflicts with existing fields; see the Note in this sub-clause for further discussion. 
                *                                                  Default value: true.
                *
                *NOTE      The names of fields imported from a template can sometimes conflict with those of existing fields in the target document. 
                *          This can occur, for example, if the same template page is imported more than once or if two different templates have fields with the same names.
                *
                *The Rename flag does not define a renaming algorithm(see Annex J).
                *
                *The TRef entry in an FDF template dictionary shall hold a named page reference dictionary that shall describe the location of external templates or page elements. 
                *Table 250 shows the contents of this type of dictionary.
                *
                *Table 250 - Entries in an FDF named page reference dictionary
                *
                *          [Key]           [Type]                  [Value]
                *
                *          Name            string                  (Required) The name of the referenced page.
                *
                *          F               file specification      (Optional) The file containing the named page. 
                *                                                  If this entry is absent, it shall be assumed that the page resides in the associated PDF file.
                */

                    /*12.7.7.3.4 FDF Annotation Dictionaries
                *
                *Each annotation dictionary in an FDF file shall have a Page entry(see Table 251) that shall indicate the page of the source document to which the annotation is attached.
                *
                *Table 251 - Additional entry for annotation dictionaries in an FDF file
                *
                *          [Key]               [Type]              [Value]
                *
                *          Page                integer             (Required for annotations in FDF files) The ordinal page number on which this annotation shall appear, where page 0 is the first page.
                */
            
            /*12.7.8 XFA Forms
            *
            *PDF 1.5 introduces support for interactive forms based on the Adobe XML Forms Architecture (XFA). 
                *The XFAentry in the interactive forms dictionary (see Table 218) specifies an XFA resource, which shall be an XML stream that contains the form information. 
                *The format of an XFA resource is described in the XML Data Package (XDP) Specification (see the Bibliography).
                *
                *The XFA entry shall be either a stream containing the entire XFA resource or an array specifying individual packets that together make up the XFA resource. 
                *The resource includes but is not limited to the following information:
                *
                *  •   The form template(specified in the template packet), which describes the characteristics of the form, including its fields, calculations, validations, and formatting. 
                *      The XML Template Specification describes the architecture of a form template(see Bibliography).
                *
                *  •   The data (specified in the datasets packet), which represents the state of the form
                *
                *  •   The configuration information(specified in the config packet), which shall be used to properly process the form template and associated data.
                *      Configuration information shall be formatted as described in the XML Configuration Specification(see Bibliography).
                *
                *A packet is a pair of a string and stream.The string contains the name of the XML element and the stream contains the complete text of this XML element. 
                *Each packet represents a complete XML element, with the exception of the first and last packet, which specify begin and end tags for the xdp:xdp element(see EXAMPLE 1 in this sub - clause).
                *
                *EXAMPLE 1     This example shows the XFA entry consisting of an array of packets.
                *
                *              1 0 obj                                             XFA entry in interactive form dictionary
                *                  << / XFA[(xdp: xdp) 10 0 R                      XFA resource specified as individual packets
                *                      (template) 11 0 R
                *                      (datasets) 12 0 R
                *                      (config) 13 0 R
                *                      (/ xdp:xdp) 14 0 R]
                *                  >>
                *              endobj
                *              10 0 obj
                *                  stream
                *                      < xdp:xdp xmlns:xdp = "http:*ns.adobe.com/xdp/" >
                *                  endstream
                *              11 0 obj
                *                  stream
                *                      < template xmlns = "http:*www.xfa.org/schema/xfa-template/2.4/" >
                *                      ...remaining contents of template packet...
                *                      </ template >
                *                  endstream
                *              12 0 obj
                *                  stream
                *                      < xfa:datasets xmlns:xfa = "http:*www.xfa.org/schema/xfa-data/1.0/" >
                *                      ...contents of datasets packet...
                *                      </ xfa:datasets >
                *                  endstream
                *              13 0 obj
                *                  stream
                *                      < config xmlns = "http:*www.xfa.org/schema/xci/1.0/" >
                *                      ...contents of config node of XFA Data Package...
                *                      < config >
                *                  endstream
                *              14 0 obj
                *                  stream
                *                      </ xdp:xdp >
                *                  endstream
                *
                *EXAMPLE 2     The following example shows the same entry specified as a stream.
                *
                *              1 0 obj                             XFA entry in interactive form dictionary
                *                  << / XFA 10 0 R >>
                *              endobj
                *              10 0 obj
                *                  stream
                *                      < xdp:xdp xmlns:xdp = "http:*ns.adobe.com/xdp/" >
                *                      < template xmlns = "http:*www.xfa.org/schema/xfa-template/2.4/" >
                *                      ...remaining contents of template packet...
                *                      </ template >
                *                      <xfa:datasets xmlns:xfa="http:*www.xfa.org/schema/xfa-data/1.0/">
                *                      ...contents of datasets packet...
                *                      </ xfa:datasets >
                *                      < config xmlns = "http:*www.xfa.org/schema/xci/1.0/" >
                *                      ...contents of config node of XFA Data Package...
                *                      < config >
                *                      </ xdp:xdp >
                *                  endstream
                *              endobj
                *
                *When an XFA entry is present in an interactive form dictionary, the XFA resource provides information about the form; in particular, all form-related events such as calculations and validations. 
                *The other entries in the interactive form dictionary shall be consistent with the information in the XFA resource. 
                *When creating or modifying a PDF file with an XFA resource, a conforming writer shall follow these rules:
                *
                *  •   PDF interactive form field objects shall be present for each field specified in the XFA resource.
                *      The XFA field values shall be consistent with the corresponding V entries of the PDF field objects.
                *
                *  •   The XFA Scripting Object Model(SOM) specifies a naming convention that shall be used to connect interactive form field names with field names in the XFA resource.
                *      Information about this model is available in the XFA Specification, version 2.5(see the Bibliography).
                *
                *  •   No A or AA entries(see Table 164) shall be present in the annotation dictionaries of fields that also have actions specified by the XFA resource.
                */
        }

        //12.8 Digital Signatures
        public class Digital_Signatures
        {
            /*12.8.1 General
            *
            *A digital signature (PDF 1.3) may be used to authenticate the identity of a user and the document’s contents. It
            *stores information about the signer and the state of the document when it was signed.The signature may be
            *purely mathematical, such as a public/private-key encrypted document digest, or it may be a biometric form of
            *identification, such as a handwritten signature, fingerprint, or retinal scan.The specific form of authentication
            *used shall be implemented by a special software module called a signature handler.Signature handlers shall
            *be identified in accordance with the rules defined in Annex E.
            *
            *Digital signatures in ISO 32000 currently support two activities: adding a digital signature to a document and
            *later checking that signature for validity.Revocation information is a signed attribute, which means that the
            *signing software must capture the revocation information before signing.A similar requirement applies to the
            *chain of certificates.The signing software must capture and validate the certificate's chain before signing.
            *Signature information shall be contained in a signature dictionary, whose entries are listed in Table 252.
            *
            *Signature handlers may use or omit those entries that are marked optional in the table but should use them in a
            *standard way if they are used at all.In addition, signature handlers may add private entries of their own.To
            *avoid name duplication, the keys for all such private entries shall be prefixed with the registered handler name
            *followed by a PERIOD(2Eh).
            *
            *Signatures shall be created by computing a digest of the data(or part of the data) in a document, and storing
            *the digest in the document.To verify the signature, the digest shall be re-computed and compared with the one
            *stored in the document. Differences in the digest values indicate that modifications have been made since the
            *document was signed.
            *
            *There are two defined techniques for computing a digital signature of the contents of all or part of a PDF file:
            *
            *  •   A byte range digest shall be computed over a range of bytes in the file, that shall be indicated by the
            *      ByteRange entry in the signature dictionary.This range should be the entire file, including the signature
            *      dictionary but excluding the signature value itself(the Contents entry). Other ranges may be used but
            *      since they do not check for all changes to the document, their use is not recommended.When a byte
            *      range digest is present, all values in the signature dictionary shall be direct objects.
            *
            *  •   Additionally, modification detection may be specified by a signature reference dictionary. 
            *      The TransformMethod entry shall specify the general method for modification detection, and the TransformParams entry shall specify the variable portions of the method.
            *
            *A PDF document may contain the following standard types of signatures:
            *
            *  •   One or more approval signatures.These signatures appear in signature form fields(see 12.7.4.5, “Signature Fields”). 
            *      The signature dictionary corresponding to each signature shall be the value of the form field(as specified by its V entry). 
            *      The signature dictionary shall contain a ByteRange entry representing a byte range digest, as described previously.
            *      A signature shall be validated by recomputing the digest and comparing it with the one stored in the signature.
            *
            *NOTE 1        If a signed document is modified and saved by incremental update (see 7.5.6, “Incremental Updates”), the data corresponding to the byte range of the original signature is preserved.
            *              Therefore, if the signature is valid, it is possible to recreate the state of the document as it existed at the time of signing.
            *
            *  •   At most one certification signature(PDF 1.5). The signature dictionary of a certification signature shall be the value of a signature field and shall contain a ByteRange entry.
            *      It may also be referenced from the DocMDP entry in the permissions dictionary(see 12.8.4, “Permissions”). 
            *      The signature dictionary shall contain a signature reference dictionary(see Table 253) that has a DocMDP transform method.See 12.8.2.2, “DocMDP” for information on how these signatures shall be created and validated.
            *
            *      A signature dictionary for a certification or approval signature may also have a signature reference dictionary with a FieldMDP transform method; see 12.8.2.4, “FieldMDP.”
            *
            *  •   At most two usage rights signatures(PDF 1.5). Its signature dictionary shall be referenced from the UR3(PDF 1.6) entry in the permissions dictionary, whose entries are listed in Table 258, (not from a signature field). 
            *      The signature dictionary shall contain a Reference entry whose value is a signature reference dictionary that has a UR transform method.
            *      See 12.8.2.3, “UR” for information on how these signatures shall be created and validated.
            *
            *Table 252 - Entries in a signature dictionary
            *
            *          [Key]           [Type]              [Value]
            *
            *          Type            name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Sig for a signature dictionary.
            *
            *          Filter          name                (Required; inheritable) The name of the preferred signature handler to use when validating this signature. 
            *                                              If the Prop_Build entry is not present, it shall be also the name of the signature handler that was used to create the signature. 
            *                                              If Prop_Build is present, it may be used to determine the name of the handler that created the signature (which is typically the same as Filter but is not needed to be). 
            *                                              A conforming reader may substitute a different handler when verifying the signature, as long as it supports the specified SubFilter format. 
            *                                              Example signature handlers are Adobe.PPKLite, Entrust.PPKEF, CICI.SignIt, and VeriSign.PPKVS. 
            *                                              The name of the filter (i.e. signature handler) shall be identified in accordance with the rules defined in Annex E.
            *
            *          SubFilter       name                (Optional) A name that describes the encoding of the signature value and key information in the signature dictionary. 
            *                                              A conforming reader may use any handler that supports this format to validate the signature.
            *                                              (PDF 1.6) The following values for public-key cryptographic signatures shall be used: adbe.x509.rsa_sha1, adbe.pkcs7.detached, and adbe.pkcs7.sha1(see 12.8.3, “Signature Interoperability”). 
            *                                              Other values may be defined by developers, and when used, shall be prefixed with the registered developer identification.
            *                                              All prefix names shall be registered (see Annex E). 
            *                                              The prefix “adbe” has been registered by Adobe Systems and the three subfilter names listed above and defined in 12.8.3, “Signature Interoperability“ may be used by any developer.
            *
            *          Contents        byte string         (Required) The signature value. When ByteRange is present, the value shall be a hexadecimal string (see 7.3.4.3, “Hexadecimal Strings”) representing the value of the byte range digest.
            *                                              For public-key signatures, Contents should be either a DER-encoded PKCS#1 binary data object or a DER-encoded PKCS#7 binary data object.
            *                                              Space for the Contents value must be allocated before the message digest is computed. (See 7.3.4, “String Objects“)
            *
            *          Cert            array or            (Required when SubFilter is adbe.x509.rsa_sha1) An array of byte strings that shall represent the X.509 certificate chain used when signing and verifying signatures that use public-key cryptography, or a byte string if the chain has only one entry. 
            *                          byte string         The signing certificate shall appear first in the array; it shall be used to verify the signature value in Contents, and the other certificates shall be used to verify the authenticity of the signing certificate.
            *                                              If SubFilter is adbe.pkcs7.detached or adbe.pkcs7.sha1, this entry shall not be used, and the certificate chain shall be put in the PKCS#7 envelope in Contents.
            *                          
            *          ByteRange       array               (Required for all signatures that are part of a signature field and usage rights signatures referenced from the UR3 entry in the permissions dictionary) 
            *                                              An array of pairs of integers (starting byte offset, length in bytes) that shall describe the exact byte range for the digest calculation. 
            *                                              Multiple discontiguous byte ranges shall be used to describe a digest that does not include the signature value (theContents entry) itself.
            *
            *          Reference       array               (Optional; PDF 1.5) An array of signature reference dictionaries (see Table 253).
            *
            *          Changes         array               (Optional) An array of three integers that shall specify changes to the document that have been made between the previous signature and this signature: in this order, the number of pages altered, the number of fields altered, and the number of fields filled in.
            *                                              The ordering of signatures shall be determined by the value of ByteRange.Since each signature results in an incremental save, later signatures have a greater length value.
            *
            *          Name            text string         (Optional) The name of the person or authority signing the document. 
            *                                              This value should be used only when it is not possible to extract the name from the signature.
            *
            *                                              EXAMPLE 1       From the certificate of the signer.
            *
            *          M               date                (Optional) The time of signing. Depending on the signature handler, this may be a normal unverified computer time or a time generated in a verifiable way from a secure time server.
            *                                              This value should be used only when the time of signing is not available in the signature.
            *
            *                                              EXAMPLE 2       A time stamp can be embedded in a PKCS#7 binary data object (see 12.8.3.3, “PKCS#7 Signatures as used in ISO 32000”).
            *
            *          Location        text string         (Optional) The CPU host name or physical location of the signing.
            *
            *          Reason          text string         (Optional) The reason for the signing, such as (I agree…).
            *
            *          ContactInfo     text string         (Optional) Information provided by the signer to enable a recipient to contact the signer to verify the signature.
            *
            *                                              EXAMPLE 3       A phone number.
            *
            *          R               integer             (Optional) The version of the signature handler that was used to create the signature. (PDF 1.5) This entry shall not be used, and the information shall be stored in the Prop_Build dictionary.
            *
            *          V               integer             (Optional; PDF 1.5) The version of the signature dictionary format. 
            *                                              It corresponds to the usage of the signature dictionary in the context of the value of SubFilter. 
            *                                              The value is 1 if the Reference dictionary shall be considered critical to the validation of the signature.
            *                                              Default value: 0.
            *
            *          Prop_Build      dictionary          (Optional; PDF 1.5) A dictionary that may be used by a signature handler to record information that captures the state of the computer environment used for signing, such as the name of the handler used to create the signature, software build date, version, and operating system.
            *                                              he PDF Signature Build Dictionary Specification, provides implementation guidelines for the use of this dictionary.
            *
            *          Prop_AuthTime   integer             (Optional; PDF 1.5) The number of seconds since the signer was last authenticated, used in claims of signature repudiation. It should be omitted if the value is unknown.
            *
            *          Prop_AuthType   name                (Optional; PDF 1.5) The method that shall be used to authenticate the signer, used in claims of signature repudiation. Valid values shall be PIN, Password, and Fingerprint.
            *
            *
            *NOTE 2        The entries in the signature dictionary can be conceptualized as being in different dictionaries; they are in one dictionary for historical and cryptographic reasons. 
            *              The categories are signature properties (R, M, Name, Reason, Location, Prop_Build, Prop_AuthTime, and Prop_AuthType); key information (Cert and portions of Contents when the signature value is a PKCS#7 object); 
            *              reference (Reference and ByteRange); and signature value (Contents when the signature value is a PKCS#1 object).
            *
            *Table 253 - Entries in a signature reference dictionary
            *
            *          [Key]               [Type]             [Value]
            *
            *          Type                name               (Optional) The type of PDF object that this dictionary describes; if present, shall be SigRef for a signature reference dictionary.
            *
            *          TransformMethod     name                (Required) The name of the transform method (see Section 12.8.2, “Transform Methods”) that shall guide the modification analysis that takes place when the signature is validated. 
            *                                                  Valid values shall be:
            *
            *                                                  DocMDP          Used to detect modifications to a document relative to a signature field that is signed by the originator of a document; see 12.8.2.2, “DocMDP.”
            *
            *                                                  UR              Used to detect modifications to a document that would invalidate a signature in a rights-enabled document; see 12.8.2.3, “UR.”
            *
            *                                                  FieldMDP        Used to detect modifications to a list of form fields specified in TransformParams; see 12.8.2.4, “FieldMDP.”
            *
            *          TransformParams     dictionary          (Optional) A dictionary specifying transform parameters (variable data) for the transform method specified by TransformMethod. Each method takes its own set of parameters. 
            *                                                  See each of the sub-clauses specified previously for details on the individual transform parameter dictionaries
            *
            *          Data                (various)           (Required when TransformMethod is FieldMDP) An indirect reference to the object in the document upon which the object modification analysis should be performed. 
            *                                                  For transform methods other than FieldMDP, this object is implicitly defined.
            *
            *          DigestMethod        name                (Optional; PDF 1.5 required) A name identifying the algorithm that shall be used when computing the digest. Valid values are MD5 and SHA1. Default value: MD5. For security reasons, MD5 should not be used. 
            *                                                  It is mentioned for backwards compatibility, since it remains the default value.
            */
            
            /*12.8.2 Transform Methods
            */
              
                /*12.8.2.1 General
                *
                *Transform methods, along with transform parameters, shall determine which objects are included and excluded in revision comparison. 
                *The following sub - clauses discuss the types of transform methods, their transform parameters, and when they shall be used.
                */

                /*12.8.2.2 DocMDP
                */

                    /*12.8.2.2.1 General
                *
                *The DocMDP transform method shall be used to detect modifications relative to a signature field that is signed by the author of a document(the person applying the first signature).
                *A document can contain only one signature field that contains a DocMDP transform method; it shall be the first signed field in the document.
                *It enables the author to specify what changes shall be permitted to be made the document and what changes invalidate the author’s signature.
                *
                *NOTE      As discussed earlier, “MDP” stands for modification detection and prevention.
                *          Certification signatures that use the DocMDP transform method enable detection of disallowed changes specified by the author.
                *          In addition, disallowed changes can also be prevented when the signature dictionary is referred to by the DocMDP entry in the permissions dictionary(see 12.8.4, “Permissions”).
                *
                *A certification signature should have a legal attestation dictionary(see 12.8.5, “Legal Content Attestations”) that specifies all content that might result in unexpected rendering of the document contents, along with the author’s attestation to such content.
                *This dictionary may be used to establish an author’s intent if the integrity of the document is questioned.
                *
                *The P entry in the DocMDP transform parameters dictionary (see Table 254) shall indicate the author’s specification of which changes to the document will invalidate the signature. 
                *(These changes to the document shall also be prevented if the signature dictionary is referred from the DocMDP entry in the permissions dictionary.) 
                *A value of 1 for P indicates that the document shall be final; that is, any changes shall invalidate the signature. 
                *The values 2 and 3 shall permit modifications that are appropriate for form field or comment workflows.
                */

                    /*12.8.2.2.2 Validating Signatures That Use the DocMDP Transform Method
                *
                *To validate a signature that uses the DocMDP transform method, a conforming reader first shall verify the byte range digest.
                *Next, it shall verify that any modifications that have been made to the document are permitted by the transform parameters.
                *
                *Once the byte range digest is validated, the portion of the document specified by the ByteRange entry in the signature dictionary(see Table 252) is known to correspond to the state of the document at the time of signing.
                *
                *Therefore, conforming readers may compare the signed and current versions of the document to see whether there have been modifications to any objects that are not permitted by the transform parameters.
                *
                *Table 254 - Entries in the DocMDP transform parameters dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be TransformParams for a transform parameters dictionary.
                *
                *          P                   number              (Optional) The access permissions granted for this document. 
                *                                                  Valid values shall be:
                *
                *                                                  1       No changes to the document shall be permitted; any change to the document shall invalidate the signature.
                *                                                  2       Permitted changes shall be filling in forms, instantiating page templates, and signing; other changes shall invalidate the signature.
                *                                                  3       Permitted changes shall be the same as for 2, as well as annotation creation, deletion, and modification; other changes shall invalidate the signature.
                *                                                  Default value: 2.
                *
                *          V                   name                (Optional) The DocMDP transform parameters dictionary version. The only valid value shall be 1.2.
                *
                *                                                  NOTE        this value is a name object, not a number.
                *
                *                                                  Default value: 1.2.
                */

                /*12.8.2.3 UR
                *
                *The UR transform method shall be used to detect changes to a document that shall invalidate a usage rights signature, which is referred to from the UR3 entry in the permissions dictionary(see 12.8.4, “Permissions”).
                *Usage rights signatures shall be used to enable additional interactive features that may not available by default in a conforming reader.
                *The signature shall be used to validate that the permissions have been granted by a bonafide granting authority. 
                *The transform parameters dictionary(see Table 255) specifies the additional rights that shall be enabled if the signature is valid.If the signature is invalid because the document has been modified in a way that is not permitted or the identity of the signer is not granted the extended permissions, additional rights shall not be granted.
                *
                *EXAMPLE           Adobe Systems grants permissions to enable additional features in Adobe Reader, using public-key cryptography.It uses certificate authorities to issue public key certificates to document creators with which it has entered into a business relationship.
                *                  Adobe Reader verifies that the rights-enabling signature uses a certificate from an Adobe-authorized certificate authority.
                *                  Other conforming readers are free to use this same mechanism for their own purposes.
                *
                *UR3 (PDF 1.6): The ByteRange entry in the signature dictionary(see Table 252) shall be present.
                *First, a conforming reader shall verify the byte range digest to determine whether the portion of the document specified by ByteRange corresponds to the state of the document at the time of 
                *signing. Next, a conforming reader shall examine the current version of the document to see whether there have been modifications to any objects that are not permitted by the transform parameters.
                *
                *Table 255 - Entries in the UR transform parameters dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be TransformParams for a transform parameters dictionary.
                *
                *          Document            array               (Optional) An array of names specifying additional document-wide usage rights for the document. 
                *                                                  The only defined value shall be FullSave, which permits a user to save the document along with modified form and/or annotation data. 
                *                                                  (PDF 1.5) Any usage right that permits the document to be modified implicitly shall enable the FullSave right.
                *                                                  
                *                                                  If the PDF document contains a UR3 dictionary, only rights specified by the Annots entry that permit the document to be modified shall implicitly enable the FullSave right.
                *                                                  For all other rights, FullSave shall be explicitly enabled in order to save the document. 
                *                                                  (Signature rights shall permit saving as part of the signing process but not otherwise).
                *                                                  
                *                                                  If the P entry in the UR transform parameters dictionary is true(PDF 1.6) and greater conforming readers shall permit only those rights that are enabled by the entries in the dictionary. 
                *                                                  However, conforming readers shall permit saving the document as long as any rights that permit modifying the document are enabled.
                *
                *          Msg                 text string         (Optional) A text string that may be used to specify any arbitrary information, such as the reason for adding usage rights to the document.
                *
                *          V                   name                (Optional) The UR transform parameters dictionary version. The value shall be2.2. 
                *                                                  If an unknown version is present, no rights shall be enabled.
                *
                *                                                  NOTE        This value is a name object, not a number.
                *
                *                                                  Default value: 2.2.
                *
                *          Annots              array               (Optional) An array of names specifying additional annotation-related usage rights for the document. 
                *                                                  Valid names (PDF 1.5) are Create, Delete, Modify, Copy, Import, and Export, which shall permit the user to perform the named operation on annotations.
                *                                                  The following names(PDF 1.6) shall be permitted only when the signature dictionary is referenced from the UR3 entry of the permissions dictionary(see Table 258):
                *
                *                                                  Online              Permits online commenting; that is, the ability to upload or download markup annotations from a server.
                *
                *                                                  SummaryView         Permits a user interface to be shown that summarizes the comments(markup annotations) in a document.
                *
                *          Form                array               (Optional) An array of names specifying additional form-field-related usage rights for the document. 
                *                                                  Valid names (PDF 1.5) are:
                *
                *                                                  Add                 Permits the user to add form fields to the document.
                *
                *                                                  Delete              Permits the user to delete form fields to the document.
                *
                *                                                  FillIn              Permits the user to save a document on which form fill -in has been done.
                *
                *                                                  Import              Permits the user to import form data files in FDF, XFDF and text(CSV / TSV) formats.
                *
                *                                                  Export              Permits the user to export form data files as FDF or XFDF.
                *
                *                                                  SubmitStandalone    Permits the user to submit form data when the document is not open in a Web browser.
                *
                *                                                  SpawnTemplate       Permits new pages to be instantiated from named page templates.
                *
                *                                                  The following names(PDF 1.6) shall be permitted only when the signature dictionary is referenced from the UR3 entry of the permissions dictionary; see Table 258:
                *
                *                                                  BarcodePlaintext    Permits(PDF 1.6) text form field data to be encoded as a plaintext two-dimensional barcode.
                *
                *                                                  Online              Permits(PDF 1.6) the use of forms-specific online mechanisms such as SOAP or Active Data Object.
                *
                *          Signature           array               (Optional) An array of names specifying additional signature-related usage rights for the document. 
                *                                                  The only defined value shall be Modify, which permits a user to apply a digital signature to an existing signature form field or clear a signed signature form field.
                *
                *          EF                  array               (Optional; PDF 1.6) An array of names specifying additional usage rights for named embedded files in the document. 
                *                                                  Valid names shall be Create, Delete, Modify, and Import, which shall permit the user to perform the named operation on named embedded files.
                *
                *          P                   boolean             (Optional; PDF 1.6) If true, permissions for the document shall be restricted in all consumer applications to those permissions granted by a conforming reader, while allowing permissions for rights enabled by other entries in this dictionary. 
                *                                                  Default value: false.
                *
                */

                /*12.8.2.4 FieldMDP
                *
                *The FieldMDP transform method shall be used to detect changes to the values of a list of form fields.
                *The entries in its transform parameters dictionary are listed in Table 256.
                *
                *Table 256 - Entries in the FieldMDP transform parameters dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be TransformParams for a transform parameters dictionary.
                *
                *              Action              name                (Required) A name that, along with the Fields array, describes which form fields do not permit changes after the signature is applied.
                *
                *                                                      Valid values shall be:
                *
                *                                                      All         All form fields.
                *
                *                                                      Include     Only those form fields that specified in Fields.
                *
                *                                                      Exclude     Only those form fields not specified in Fields.
                *
                *              Fields              array               (Required if Action is Include or Exclude) An array of text strings containing field names.
                *
                *              V                   name                (Optional: PDF 1.5 required) The transform parameters dictionary version. The value for PDF 1.5 and later shall be 1.2.
                *
                *                                                      NOTE    This value is a name object, not a number.
                *
                *                                                      Default value: 1.2.
                *
                *On behalf of a document author creating a document containing both form fields and signatures the following shall be supported by conforming writers:
                *
                *  •   The author specifies that form fields shall be filled in without invalidating the approval or certification signature.
                *      The P entry of the DocMDP transform parameters dictionary shall be set to either 2 or 3(see Table 254).
                *
                *  •   The author can also specify that after a specific recipient has signed the document, any modifications to specific form fields shall invalidate that recipient’s signature. 
                *      There shall be a separate signature field for each designated recipient, each having an associated signature field lock dictionary(see Table 233) specifying the form fields that shall be locked for that user.
                *
                *  •   When the recipient signs the field, the signature, signature reference, and transform parameters dictionaries shall be created.
                *      The Action and Fields entries in the transform parameters dictionary shall be copied from the corresponding fields in the signature field lock dictionary.
                *
                *NOTE  This copying is done because all objects in a signature dictionary must be direct objects if the dictionary contains a byte range signature.
                *      Therefore, the transform parameters dictionary cannot reference the signature field lock dictionary indirectly.
                *
                *FieldMDP signatures shall be validated in a similar manner to DocMDP signatures. 
                *See Validating Signatures That Use the DocMDP Transform Method in 12.8.2.2, “DocMDP” for details.
           */

            /*12.8.3 Signature Interoperability
            */
              
                /*12.8.3.1 General
                *
                *It is intended that conforming readers allow interoperability between signature handlers; that is, a PDF file signed with a handler from one vendor shall be able to be validated with a handler from a different vendor.
                *
                *If present, the SubFilter entry in the signature dictionary shall specify the encoding of the signature value and key information, while the Filter entry shall specify the preferred handler that should be used to validate the signature.
                *When handlers are being registered according to Annex E they shall specify the SubFilter encodings they support enabling handlers other than the preferred handler to validate the signatures that the preferred handler creates.
                *
                *There are several defined values for the SubFilter entry, all based on public-key cryptographic standards published by RSA Security and also as part of the standards issued by the Internet Engineering Task Force(IETF) Public Key Infrastructure(PKIX) working group; see the Bibliography for references.
                */

                /*12.8.3.2 PKCS#1 Signatures
                *
                *The PKCS#1 standard supports several public-key cryptographic algorithms and digest methods, including RSA encryption, DSA signatures, and SHA-1 and MD5 digests (see the Bibliography for references). 
                *For signing PDF files using PKCS#1, the only value of SubFilter that should be used is adbe.x509.rsa_sha1, which uses the RSA encryption algorithm and SHA-1 digest method. 
                *The certificate chain of the signer shall be stored in the Cert entry.
                *
                *12.8.3.3 PKCS#7 Signatures as used in ISO 32000
                */

                    /*12.8.3.3.1 General
                *
                *When PKCS#7 signatures are used, the value of Contents shall be a DER-encoded PKCS#7 binary data object containing the signature. 
                *The PKCS#7 object shall conform to RFC3852 Cryptographic Message Syntax. Different subfilters may be used and shall be registered in accordance with Annex E. 
                *SubFilter shall take one of the following values:
                *
                *  •   adbe.pkcs7.detached: The original signed message digest over the document’s byte range shall be incorporated as the normal PKCS#7 SignedData field. 
                *      No data shall be encapsulated in the PKCS#7 SignedData field.
                *
                *  •   adbe.pkcs7.sha1: The SHA1 digest of the document’s byte range shall be encapsulated in the PKCS#7 SignedData field with ContentInfo of type Data. 
                *      The digest of that SignedData shall be incorporated as the normal PKCS#7 digest.
                *
                *The PKCS#7 object shall conform to the PKCS#7 specification in Internet RFC 2315, PKCS #7: Cryptographic Message Syntax, Version 1.5 (see the Bibliography). 
                *At minimum, it shall include the signer’s X.509 signing certificate. This certificate shall be used to verify the signature value in Contents.
                *The PKCS#7 object should contain the following:
                *
                *Time stamp information as an unsigned attribute (PDF 1.6): The timestamp token shall conform to RFC 3161 and shall be computed and embedded into the PKCS#7 object as described in Appendix A of RFC 3161. 
                *The specific treatment of timestamps and their processing is left to the particular signature handlers to define.
                *
                *  •   Revocation information as an signed attribute(PDF 1.6): This attribute may include all the revocation information that is necessary to carry out revocation checks for the signer's certificate and its issuer certificates. 
                *Since revocation information is a signed attribute, it must be obtained before the computation of the digital signature. 
                *This means that the software used by the signer must be able to construct the certification path and the associated revocation information. 
                *If one of the elements cannot be obtained (e.g. no connection is possible), a signature with this attribute will not be possible.
                *
                *  •   (PDF 1.6).This differs from the treatment when using adbe.x509.rsa_sha1 when the certificates shall be placed in the Cert key of the signature dictionary as defined in Table 252.
                *
                *  •   One or more RFC 3281 attribute certificates associated with the signer certificate(PDF 1.7).
                *      The specific treatment of attribute certificates and their processing is left to the particular signature handlers to define.
                *
                *NOTE      For maximum compatibility with earlier versions, conforming writers should follow this practice.
                *
                *The policy of how to establish trusted identity lists to validate embedded certificates is up to the validation signature handler.
                */

                    /*12.8.3.3.2 Revocation Information
                *
                *The adbe Revocation Information attribute:
                *
                *      adbe - revocationInfoArchival OBJECT IDENTIFIER ::=
                *                                      { adbe(1.2.840.113583) acrobat(1) security(1) 8 }
                *
                *The value of the revocation information attribute can include any of the following data types:
                *
                *  •   Certificate Revocation Lists(CRLs), described in RFC 3280(see the Bibliography): CRLs are generally large and therefore should not be embedded in the PKCS#7 object.
                *
                *  •   Online Certificate Status Protocol (OCSP) Responses, described in RFC 2560, X.509 Internet Public Key Infrastructure Online Certificate Status Protocol—OCSP (see the Bibliography): These are generally small and constant in size and should be the data type included in the PKCS#7 object.
                *
                *  •   Custom revocation information: The format is not prescribed by this specification, other than that it be encoded as an OCTET STRING. 
                *      The application should be able to determine the type of data contained within the OCTET STRING by looking at the associated OBJECT IDENTIFIER.
                *
                *      adbe's Revocation Information attribute value has ASN.1 type RevocationInfoArchival:
                *
                *                  RevocationInfoArchival::= SEQUENCE {
                *                      crl                 [0] EXPLICIT SEQUENCE of CRLs, OPTIONAL
                *                      ocsp                [1] EXPLICIT SEQUENCE of OCSP Responses, OPTIONAL
                *                      otherRevInfo        [2] EXPLICIT SEQUENCE of OtherRevInfo, OPTIONAL
                *                  }
                *                  OtherRevInfo::= SEQUENCE {
                *                  Type OBJECT IDENTIFIER
                *                  Value OCTET STRING
                *                  }
                *
                *For byte range signatures, Contents shall be a hexadecimal string with “<” and “>” delimiters.It shall fit precisely in the space between the ranges specified by ByteRange. 
                *Since the length of PKCS#7 objects is not entirely predictable, the value of Contents shall be padded with zeros at the end of the string (before the “>” delimiter) before writing the PKCS#7 to the allocated space in the file.
                *
                *The format for encoding signature values should be adbe.pkcs7.detached.This encoding allows the most options in terms of algorithm use.
                *The following table shows the algorithms supported for the various SubFiltervalues.
                *
                *Table 257 - SubFilter value algorithm support
                *
                *                                              [                               SUBFILTER VALUE                            ]
                *                                              [adbe.pkcs7.detached]       [adbe.pkcs7.sha1]       [adbe.x509.rsa.sha1(*a)]
                *
                *              Message Digest                  SHA1 (PDF 1.3)              SHA1 (PDF 1.3)(*b)      SHA1 (PDF 1.3)
                *                                              SHA256 (PDF 1.6)                                    SHA256 (PDF 1.6)
                *                                              SHA384 (PDF 1.7)                                    SHA384 (PDF 1.7)
                *                                              SHA512 (PDF 1.7)                                    SHA512 (PDF 1.7)
                *                                              RIPEMD160 (PDF 1.7)                                 RIPEMD160 (PDF 1.7)
                *
                *              RSA Algorithm Support           Up to 1024-bit (PDF 1.3)    See                     See
                *                                              Up to 2048-bit (PDF 1.5)    adbe.pkcs7.detached     adbe.pkcs7.detached
                *                                              Up to 4096-bit (PDF 1.5)
                *
                *              DSA Algorithm Support           Up to 4096-bits (PDF 1.6)   See                     No
                *                                                                          adbe.pkcs7.detached
                *
                *              (*a)    Despite the appearance of sha1 in the name of this SubFilter value, supported encodings shall not be limited to the SHA1 algorithm. 
                *                      The PKCS#1 object contains an identifier that indicates which algorithm shall be used.
                *
                *              (*b)    Other digest algorithms may be used to digest the signed-data field; however, SHA1 shall be used to digest the data that is being signed.
                *
                */

            /*12.8.4 Permissions
            *
                *The Perms entry in the document catalogue (see Table 28) shall specify a permissions dictionary (PDF 1.5). 
                *Each entry in this dictionary (see Table 258 for the currently defined entries) shall specify the name of a permission handler that controls access permissions for the document. 
                *These permissions are similar to those defined by security handlers (see Table 22) but do not require that the document be encrypted. 
                *For a permission to be actually granted for a document, it shall be allowed by each permission handler that is present in the permissions dictionary as well as by the security handler.
                *
                *NOTE      An example of a permission is the ability to fill in a form field.
                *
                *Table 258 - Entries in a permissions dictionary
                *
                *          [Key]           [Type]              [Value]
                *
                *          DocMP           dictionary          (Optional) An indirect reference to a signature dictionary (see Table 252). 
                *                                              This dictionary shall contain a Reference entry that shall be a signature reference dictionary (see Table 252) that has a DocMDP transform method (see 12.8.2.2, “DocMDP”) and corresponding transform parameters.
                *                                              If this entry is present, consumer applications shall enforce the permissions specified by the P attribute in the DocMDP transform parameters dictionary and shall also validate the corresponding signature based on whether any of these permissions have been violated.
                *
                *          UR3             dictionary          (Optional) A signature dictionary that shall be used to specify and validate additional capabilities (usage rights) granted for this document; that is, the enabling of interactive features of the conforming reader that are not available by default.
                *                                              For example, A conforming reader does not permit saving documents by default, but an agent may grant permissions that enable saving specific documents.
                *                                              The signature shall be used to validate that the permissions have been granted by the agent that did the signing.
                *                                              The signature dictionary shall contain a Reference entry that shall be a signature reference dictionary that has a UR transform method(see 12.8.2.3, “UR”).
                *                                              The transform parameter dictionary for this method indicates which additional permissions shall be granted for the document. 
                *                                              If the signature is valid, the conforming reader shall allow the specified permissions for the document, in addition to the application’s default permissions.
            */

      
            /*12.8.5 Legal Content Attestations
            *
                *The PDF language provides a number of capabilities that can make the rendered appearance of a PDF document vary. 
                *These capabilities could potentially be used to construct a document that misleads the recipient of a document, intentionally or unintentionally. 
                *These situations are relevant when considering the legal implications of a signed PDF document.
                *
                *Therefore, a mechanism shall be provided by which a document recipient can determine whether the document can be trusted. 
                *The primary method is to accept only documents that contain certification signatures(one that has a DocMDP signature that defines what shall be permitted to change in a document; see 12.8.2.2, “DocMDP”).
                *
                *When creating certification signatures, conforming writers should also create a legal attestation dictionary, whose entries are shown in Table 259.
                *This dictionary shall be the value of the Legal entry in the document catalogue(see Table 28).
                *Its entries shall specify all content that may result in unexpected rendering of the document contents. 
                *The author may provide further clarification of such content by means of the Attestationentry. 
                *Reviewers should establish for themselves that they trust the author and contents of the document.In the case of a legal challenge to the document, any questionable content can be reviewed in the context of the information in this dictionary.
                *
                *Table 259 - Entries in a legal attestations dictionary
                *
                *          [Key]                       [Type]                      [Value]
                *
                *          JavaScriptActions           integer                     (Optional) The number of JavaScript actions found in the document (see 12.6.4.16, “JavaScript Actions”).
                *
                *          LaunchActions               integer                     (Optional) The number of launch actions found in the document (see 12.6.4.5, “Launch Actions”).
                *
                *          URIActions                  integer                     (Optional) The number of URI actions found in the document (see 12.6.4.7, “URI Actions”).
                *
                *          MovieActions                integer                     (Optional) The number of movie actions found in the document (see 12.6.4.9, “Movie Actions”).
                *
                *          SoundActions                integer                     (Optional) The number of sound actions found in the document (see 12.6.4.8, “Sound Actions”).
                *
                *          HideAnnotationActions       integer                     (Optional) The number of hide actions found in the document (see 12.6.4.10, “Hide Actions”).
                *
                *          GoToRemoteActions           integer                     (Optional) The number of remote go-to actions found in the document (see 12.6.4.3, “Remote Go-To Actions”).
                *
                *          AlternateImages             integer                     (Optional) The number of alternate images found in the document (see 8.9.5.4, “Alternate Images”)
                *
                *          ExternalStreams             integer                     (Optional) The number of external streams found in the document.
                *
                *          TrueTypeFonts               integer                     (Optional) The number of TrueType fonts found in the document (see 9.6.3, “TrueType Fonts”).
                *
                *          ExternalRefXobjects         integer                     (Optional) The number of reference XObjects found in the document (see 8.10.4, “Reference XObjects”).
                *
                *          ExternalOPIdicts            integer                     (Optional) The number of OPI dictionaries found in the document (see 14.11.7, “Open Prepress Interface (OPI)”).
                *
                *          NonEmbeddedFonts            integer                     (Optional) The number of non-embedded fonts found in the document (see 9.9, “Embedded Font Programs””)
                *
                *          DevDepGS_OP                 integer                     (Optional) The number of references to the graphics state parameter OP found in the document (see Table 58).
                *
                *          DevDepGS_HT                 integer                     (Optional) The number of references to the graphics state parameter HT found in the document (see Table 58).
                *
                *          DevDepGS_TR                 integer                     (Optional) The number of references to the graphics state parameter TR found in the document (see Table 58).
                *
                *          DevDepGS_UCR                integer                     (Optional) The number of references to the graphics state parameter UCR found in the document (see Table 58).
                *
                *          DevDepGS_BG                 integer                     (Optional) The number of references to the graphics state parameter BG found in the document (see Table 58).
                *
                *          DevDepGS_FL                 integer                     (Optional) The number of references to the graphics state parameter FL found in the document (see Table 58).
                *
                *          Annotations                 integer                     (Optional) The number of annotations found in the document (see 12.5, “Annotations”).
                *
                *          OptionalContent             boolean                     (Optional) true if optional content is found in the document (see 8.11, “Optional Content”).
                *
                *          Attestation                 text string                 (Optional) An attestation, created by the author of the document, explaining the presence of any of the other entries in this dictionary or the presence of any other content affecting the legal integrity of the document.
                *
                */
          
        }

        //12.9 Measurement Properties
        public class Measurement_Properties
        {
            /*12.9 Measurement Properties
            *PDF documents, such as those created by CAD software, may contain graphics that are intended to represent real-world objects. 
            *Users of such documents often require information about the scale and units of measurement of the corresponding real-world objects and their relationship to units in PDF user space.
            *
            *This information enables users of conforming readers to perform measurements that yield results in the units intended by the creator of the document.
            *A measurement in this context is the result of a canonical function that takes as input a set of n coordinate pairs
            *
            *          {(x(0),y(0)),...,(x(n-1),y(n-1))}
            *
            *and produces a single number as output depending on the type of measurement.For example, distance measurement is equivalent to
            *
            *          (see Equation on page 479)
            *
            *Beginning with PDF 1.6, such information may be stored in a measure dictionary(see Table 261). 
            *Measure dictionaries provide information about measurement units associated with a rectangular area of the document known as a viewport.
            *
            *A viewport (PDF 1.6) is a rectangular region of a page.The optional VP entry in a page dictionary (see Table 30) shall specify an array of viewport dictionaries, whose entries shall be as shown in Table 260. 
            *Viewports allow different measurement scales(specified by the Measure entry) to be used in different areas of a page, if necessary.
            *
            *The dictionaries in the VP array shall be in drawing order. Since viewports might overlap, to determine the viewport to use for any point on a page, the dictionaries in the array shall be examined, starting with the last one and iterating in reverse, and the first one whose BBox entry contains the point shall be chosen.
            *
            *NOTE 1    Any measurement that potentially involves multiple viewports, such as one specifying the distance between two points, shall use the information specified in the viewport of the first point.
            *
            *Table 260 - Entries in a viewpoint dictionary
            *
            *              [Key]                   [Type]                  [Value]
            *
            *              Type                    name                    (Optional) The type of PDF object that this dictionary describes; shall be Viewport for a viewport dictionary.
            *
            *              BBox                    rectangle               (Required) A rectangle in default user space coordinates specifying the location of the viewport on the page.
            *
            *                                                              The two coordinate pairs of the rectangle shall be specified in normalized form; that is, lower-left followed by upper-right, relative to the measuring coordinate system.
            *                                                              This ordering shall determine the orientation of the measuring coordinate system (that is, the direction of the positive x and yaxes) in this viewport, which may have a different rotation from the page.
            *
            *                                                              The coordinates of this rectangle are independent of the origin of the measuring coordinate system, specified in the O entry (see Table 262) of the measurement dictionary specified by Measure.
            *
            *              Name                    text string             (Optional) A descriptive text string or title of the viewport, intended for use in a user interface.
            *
            *              Measure                 dictionary              (Optional) A measure dictionary (see Table 261) that specifies the scale and units that shall apply to measurements taken on the contents within the viewport.
            *
            *
            *A measure dictionary shall specify an alternate coordinate system for a region of a page. 
            *Along with the viewport dictionary, it shall provide the information needed to convert coordinates in the page’s coordinate system to coordinates in the measuring coordinate system. 
            *The measure dictionary shall provide information for formatting the resulting values into textual form for presentation in a graphical user interface.
            *
            *Table 261 shows the entries in a measure dictionary.
            *PDF 1.6 defines only a single type of coordinate system, a rectilinear coordinate system, that shall be specified by the value RL for the Subtype entry, which is defined as one in which the x and y axes are perpendicular and have units that increment linearly(to the right and up, respectively). 
            *Other subtypes may be used, providing the flexibility to measure using other types of coordinate systems.
            *
            *Table 261 - Entries in a measure dictionary
            *
            *              [Key]                   [Type]                  [Value]
            *
            *              Type                    name                    (Optional) The type of PDF object that this dictionary describes; shall be Measure for a measure dictionary.
            *
            *              Subtype                 name                    (Optional) A name specifying the type of coordinate system to use for measuring.
            *                                                              Default value: RL, which specifies a rectilinear coordinate system
            *
            *Table 262 shows the additional entries in a rectilinear measure dictionary. 
            *Many of the entries in this dictionary shall be number format arrays, which are arrays of number format dictionaries (see Table 263). 
            *Each number format dictionary shall represent a specific unit of measurement (such as miles or feet). 
            *It shall contain information about how each unit shall be expressed in text and factors for calculating the number of units.
            *
            *Number format arrays specify all the units that shall be used when expressing a specific measurement.
            *Each array shall contain one or more number format dictionaries, in descending order of granularity. 
            *If one unit of measurement X is larger than one unit of measurement Y then X has a larger order of granularity than Y.All the elements in the array shall contain text strings that, concatenated together, specify how the units shall be displayed.
            *
            *NOTE 2        For example, a measurement of 1.4505 miles might be expressed as “1.4505 mi”, which would require one number format dictionary for miles, or as “1 mi 2,378 ft 7 5/8 in”, which would require three dictionaries (for miles, feet, and inches).
            *
            *EXAMPLE 1     A number format dictionary specifying feet should precede one specifying inches.
            *
            *Table 262 - Additional entries in a rectilinear measure dictionary
            *
            *              [Key]                   [Type]                  [Value]
            *
            *              R                       text string             (Required) A text string expressing the scale ratio of the drawing in the region corresponding to this dictionary. 
            *                                                              Universally recognized unit abbreviations should be used, either matching those of the number format arrays in this dictionary or those of commonly used scale ratios.
            *
            *                                                              EXAMPLE 1       a common scale in architectural drawings is “1/4 in = 1 ft”, indicating that 1/4 inches in default user space is equivalent to 1 foot in real-world measurements.
            *
            *                                                              If the scale ratio differs in the x and y directions, both scales should be specified.
            *
            *                                                              EXAMPLE 2       “in X 1 cm = 1 m, in Y 1 cm = 30 m”.
            *
            *              X                       array                   (Required) A number format array for measurement of change along the xaxis and, if Y is not present, along the y axis as well. 
            *                                                              The first element in the array shall contain the scale factor for converting from default user space units to the largest units in the measuring coordinate system along that axis.
            *                                                              The directions of the x and y axes are in the measuring coordinate system and are independent of the page rotation.
            *                                                              These directions shall bedetermined by the BBox entry of the containing viewport(see Table 260).
            *
            *              Y                       array                   (Required when the x and y scales have different units or conversion factors) A number format array for measurement of change along the y axis. 
            *                                                              The first element in the array shall contain the scale factor for converting from default user space units to the largest units in the measuring coordinate system along the y axis.
            *
            *              D                       array                   (Required) A number format array for measurement of distance in any direction. 
            *                                                              The first element in the array shall specify the conversion to the largest distance unit from units represented by the first element in X. 
            *                                                              The scale factors from X, Y (if present) and CYX (if Y is present) shall be used to convert from default user space to the appropriate units before applying the distance function.
            *
            *              A                       array                   (Required) A number format array for measurement of area. The first element in the array shall specify the conversion to the largest area unit from units represented by the first element in X, squared. 
            *                                                              The scale factors from X, Y (if present) and CYX (if Y is present) shall be used to convert from default user space to the appropriate units before applying the area function.
            *
            *              T                       array                   (Optional) A number format array for measurement of angles. The first element in the array shall specify the conversion to the largest angle unit from degrees. 
            *                                                              The scale factor from CYX (if present) shall be used to convert from default user space to the appropriate units before applying the angle function.
            *
            *              S                       array                   (Optional) A number format array for measurement of the slope of a line. The first element in the array shall specify the conversion to the largest slope unit from units represented by the first element in Y divided by the first element in X. 
            *                                                              The scale factors from X, Y (if present) and CYX (if Y is present) shall beused to convert from default user space to the appropriate units before applying the slope function.
            *
            *              O                       array                   (Optional) An array of two numbers that shall specify the origin of the measurement coordinate system in default user space coordinates. 
            *                                                              The directions by which x and y increase in value from this origin shall bedetermined by the viewport’s BBox entry (see Table 260).
            *                                                              Default value: the first coordinate pair(lower-left corner) of the rectangle specified by the viewport’s BBox entry.
            *
            *              CYX                     number                  (Optional; meaningful only when Y is present) A factor that shall be used to convert the largest units along the y axis to the largest units along the x axis. 
            *                                                              It shall be used for calculations (distance, area, and angle) where the units are be equivalent; if not specified, these calculations may not be performed (which would be the case in situations such as x representing time and yrepresenting temperature). 
            *                                                              Other calculations (change in x, change in y, and slope) shall not require this value.
            *
            *The X and Y entries in a measure dictionary shall be number format arrays that shall specify the units used for measurements in the x and y directions, respectively, and the ratio between user space units and the specified units. 
            *Y is present only when the x and y measurements are in different units or have different ratios; in this case, the CYX entry shall be used to convert y values to x values when appropriate.
            *
            *Table 263 - Entries in a number format dictionary
            *
            *              [Key]                   [Type]                  [Value]
            *
            *              Type                    name                    (Optional) The type of PDF object that this dictionary describes; shall be NumberFormat for a number format dictionary.
            *
            *              U                       text string             (Required) A text string specifying a label for displaying the units represented by this dictionary in a user interface; the label should use a universally recognized abbreviation.
            *
            *              C                       number                  (Required) The conversion factor used to multiply a value in partial units of the previous number format array element to obtain a value in the units of this dictionary. 
            *                                                              When this entry is in the first number format dictionary in the array, its meaning (that is, what it shall be multiplied by) depends on which entry in the rectilinear measure dictionary (see Table 262) references the number format array.
            *
            *              F                       name                    (Optional; meaningful only for the last dictionary in a number format array) A name indicating whether and in what manner to display a fractional value from the result of converting to the units of this dictionary by means of the Centry. 
            *                                                              Valid values shall be:
            *
            *                                                              D       Show as decimal to the precision specified by the D entry.
            *                                                              F       Show as a fraction with denominator specified by the D entry.
            *                                                              R       No fractional part; round to the nearest whole unit.
            *                                                              T       No fractional part; truncate to achieve whole units.
            *
            *                                                              Default value: D.
            *
            *              D                       integer                 (Optional; meaningful only for the last dictionary in a number format array) A positive integer that shall specify the precision or denominator of a fractional amount:
            *
            *                                                              When the value of F is D, this entry shall be the precision of a decimal display; it shall be a multiple of 10. 
            *                                                              Low-order zeros may be truncated unless FD is true. 
            *                                                              Default value: 100 (hundredths, corresponding to two decimal digits).
            *
            *                                                              When the value of F is F, this entry shall be the denominator of a fractional display.
            *                                                              The fraction may be reduced unless the value of FD is true. 
            *                                                              Default value: 16.
            *
            *              FD                      boolean                 (Optional; meaningful only for the last dictionary in a number format array) If true, a fractional value formatted according to the D entry may not have its denominator reduced or low-order zeros truncated.
            *                                                              Default value: false.
            *
            *              RT                      text string             (Optional) Text that shall be used between orders of thousands in display of numerical values. 
            *                                                              An empty string indicates that no text shall be added.
            *                                                              Default value: COMMA(2Ch).
            *
            *              RD                      text string             (Optional) Text that shall be used as the decimal position in displaying numerical values. 
            *                                                              An empty string indicates that the default shall be used.
            *                                                              Default value: PERIOD(2Eh).
            *
            *              PS                      text string             (Optional) Text that shall be concatenated to the left of the label specified by U. 
            *                                                              An empty string indicates that no text shall be added.
            *                                                              Default value: A single ASCII SPACE character(20h).
            *
            *              SS                      text string             (Optional) Text that shall be concatenated after the label specified by U. 
            *                                                              An empty string indicates that no text shall be added.
            *                                                              Default value: A single ASCII SPACE character(20h).
            *
            *              O                       name                    (Optional) A name indicating the position of the label specified by U with respect to the calculated unit value. 
            *                                                              Valid values shall be:
            *
            *                                                              S   The label is a suffix to the value.
            *
            *                                                              P   The label is a prefix to the value.
            *
            *                                                              The characters specified by PS and SS shall be concatenated before considering this entry.
            *
            *                                                              Default value: S.
            *
            *To use a number format array to create a text string containing the appropriately formatted units for display in a user interface, apply the following algorithm:
            *
            *Algorithm: Use of a number format array to create a formatted text string
            *
            *  a)  The entry in the rectilinear measure dictionary(see Table 262) that references the number format array determines the meaning of the initial measurement value.
            *      For example, the X entry specifies user space units, and the T entry specifies degrees.
            *
            *  b)  Multiply the value specified previously by the C entry of the first number format dictionary in the array, which converts the measurement to units of the largest granularity specified in the array.
            *      Apply the value of RT as appropriate.
            *
            *  c)  If the result contains no nonzero fractional portion, concatenate the label specified by the U entry in the order specified by O, after adding spacing from PS and SS.
            *      The formatting is then complete.
            *
            *  d)  If there is a nonzero fractional portion and no more elements in the array, format the fractional portion as specified by the RD, F, D, and FD entries of the last dictionary.
            *      Concatenate the label specified by the Uentry in the order specified by O, after adding spacing from PS and SS.The formatting is then complete.
            *
            *  e)  If there is a nonzero fractional portion and more elements in the array, proceed to the next number format dictionary in the array.
            *      Multiply its C entry by the fractional result from the previous step.Apply the value of RT as appropriate.
            *      Then proceed to step 3.
            *
            *The concatenation of elements in this process assumes left-to-right order. 
            *Documents using right-to-left languages may modify the process and the meaning of the entries as appropriate to produce the correct results.
            *
            *EXAMPLE 2     The following example shows a measure dictionary that specifies that changes in x or y are expressed in miles; distances are expressed in miles, feet, and inches; and area is expressed in acres. 
            *              Given a sample distance in scaled units of 1.4505 miles, the formatted text produced by applying the number format array would be
            *
            *              “1 mi 2,378 ft 7 5/8 in”.
            *
            *              <</Type /Measure
            *                /Subtype /RL
            *                /R(1in = 0.1 mi)
            *                /X[ <</ U(mi)                             % x offset represented in miles
            *                      / C .00139                          % Conversion from user space units to miles
            *                      / D 100000
            *                  ]
            *                /D[<< / U(mi) / C 1 >>                    % Distance: initial unit is miles; no conversion needed
            *                   << /U(ft) /C 5280 >>                   % Conversion from miles to feet
            *                   << /U(in) /C 12                        % Conversion from feet to inches
            *                      /F /F /D 8 >>                       % Fractions of inches rounded to nearest 1/8
            *                  ]
            *                /A[<</ U(acres) % Area: measured in acres
            *                     / C 640 >>% Conversion from square miles to acres
            *                  ]
            *              >>
            */
            
        }

        //12.10 Document Requirements
        public class Document_Requirements
        {
            /*12.10.1 General
            *
            *Beginning with PDF 1.7, a document may specify requirements that shall be present in a conforming reader in order for the document to function properly.
            *The Requirements entry in the document catalogue(see 7.7.2, “Document Catalog”) shall specify an array of requirement dictionaries, whose entries are shown in Table 264.
            *
            *Table 264 - Entries common to all requirement dictionaries
            *
            *          [Key]               [Type]              [Description]
            *
            *          Type                name                (Optional) The type of PDF object that this dictionary describes. If present, shall be Requirement for a requirement dictionary.
            *
            *          S                   name                (Required) The type of requirement that this dictionary describes. The value shall be EnableJavaScripts.
            *
            *          RH                  array               (Optional) An array of requirement handler dictionaries (see Table 265). 
            *                                                  This array lists the requirement handlers that shall be disabled (not executed) if the conforming reader can check the requirement specified in the S entry.
            *
            *The RH entry ensures backward-capability for this feature. Some PDF documents include JavaScript segments that verify compliance with certain requirements. 
            *Such JavaScript segments are called requirement handlers. Backward-compatibility shall be achieved by ensuring that either the conforming reader checks the requirement or the JavaScript segment checks the requirement, but not both.
            *
            *When a PDF document is first opened, all JavaScript segments in the document shall be executed, including the requirement handlers.
            *If the conforming reader understands the requirement dictionary, it shall disable execution of the requirement handlers named by the RH entry.
            *If the requirement handler is in JavaScript, the conforming reader shall look up the segment using the Names dictionary(7.7.4, “Name Dictionary”).
            *
            *In PDF 1.7, the only defined requirement type shall be EnableJavaScripts.
            *This requirement indicates that the document requires JavaScript execution to be enabled in the conforming reader.
            *If the EnableJavaScriptsrequirement is present, an interactive conforming reader may allow the user to choose between keeping JavaScript execution disabled or temporarily enabling it to benefit from the full function of the document.
            *
            *If the EnableJavaScripts requirement is present in a requirement dictionary, the inclusion of the RH entry that specifies a JavaScript segment would be pointless. 
            *Writing a JavaScript segment to verify that JavaScript is enabled would not achieve the desired goal. The RH entry shall not be used in PDF 1.7.
            */

            /*12.10.2 Requirement Handlers
            *
                *A requirement handler is a program that verifies certain requirements are satisfied. Table 265 describes the entries in a requirement handler dictionary.
                *
                *Table 265 - Entries in a requirement handler dictionary
                *
                *              [Key]               [Type]              [Description]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes. If present, shall be ReqHandler for a requirement handler dictionary.
                *
                *              S                   name                (Required) The type of requirement handler that this dictionary describes. 
                *                                                      Valid requirement handler types shall beJS (for a JavaScript requirement handlers) and NoOp.
                *                                                      A value of NoOp allows older conforming readers to ignore unrecognized requirements. 
                *                                                      This value does not add any specific entry to the requirement handler dictionary.
                *
                *              Script              text string         (Optional; valid only if the S entry has a value of JS) The name of a document-level JavaScript action stored in the document name dictionary (see 7.7.4, “Name Dictionary”). 
                *                                                      If the conforming reader understands the parent requirement dictionary and can verify the requirement specified in that dictionary, it shall disable execution of the requirement handler identified in this dictionary.
                */

        }

    }
}
