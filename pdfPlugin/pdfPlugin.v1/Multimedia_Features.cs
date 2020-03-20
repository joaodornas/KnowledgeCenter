using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfPlugin
{
    //13 Multimidia Features
    class Multimedia_Features
    {

        /*13.1 General
        *
        *This clause describes those features of PDF that support embedding and playing multimedia content. It contains the following sub-clauses:
        *
        *  •   13.2, “Multimedia,” describes the comprehensive set of multimedia capabilities that were introduced in PDF 1.5.
        *
        *  •   13.3, “Sounds,” and 13.4, “Movies,” describe features that have been supported since PDF 1.2.
        *
        *  •   13.5, “Alternate Presentations,” describes a slideshow capability that was introduced in PDF 1.4.
        *
        *  •   13.6, “3D Artwork,” describes the capability of embedding three-dimensional graphics in a document, introduced in PDF 1.6.
        */

        //13.2 Multimedia
        public class Multimedia
        {
            /*13.2.1 General
            *
            *PDF 1.5 introduces a comprehensive set of language constructs to enable the following capabilities:
            *
            *  •   Arbitrary media types may be embedded in PDF files.
            *
            *  •   Embedded media, as well as referenced media outside a PDF file, may be played with a variety of player software. 
            *      (In some situations, the player software may be the conforming reader itself.)
            *
            *NOTE 1    The term playing is used with a wide variety of media, and is not restricted to audio or video.
            *          For example, it may be applied to static images such as JPEGs.
            *
            *  •   Media objects may have multiple renditions, which may be chosen at play-time based on considerations such as available bandwidth.
            *
            *  •   Document authors may control play-time requirements, such as which player software should be used to play a given media object.
            *
            *  •   Media objects may be played in various ways; for example, in a floating window as well as in a region on a page.
            *
            *  •   Future extensions to the media constructs may be handled in an appropriate manner by current conforming readers.
            *      Authors may control how old conforming readers treat future extensions.
            *
            *  •   Document authors may adapt the use of multimedia to accessibility requirements.
            *
            *  •   On-line media objects may be played efficiently, even when very large.
            *
            *The following list summarizes the multimedia features and indicates where each feature is discussed:
            *
            *  •   13.2.2, “Viability,” describes the rules for determining when media objects are suitable for playing on a particular system.
            *
            *  •   Rendition actions(see 12.6.4.13, “Rendition Actions”) shall be used to begin the playing of multimedia content.
            *
            *  •   A rendition action associates a screen annotation (see 12.5.6.18, “Screen Annotations”) with a rendition(see 13.2.3, “Renditions”).
            *
            *  •   Renditions are of two varieties: media renditions (see 13.2.3.2, “Media Renditions”) that define the characteristics of the media to be played, and selector renditions (see 13.2.3.3, “Selector Renditions”) that enables choosing which of a set of media renditions should be played.
            *
            *  •   Media renditions contain entries that specify what should be played(see 13.2.4, “Media Clip Objects”), how it should be played(see 13.2.5, “Media Play Parameters”), and where it should be played(see 13.2.6, “Media Screen Parameters”).
            *
            *  •   13.2.7, “Other Multimedia Objects,” describes several PDF objects that are referenced by the precedingmajor objects.
            *
            *NOTE 2    Some of the features described in the following sub-clauses have references to corresponding elements in the Synchronized Multimedia Integration Language(SMIL 2.0) standard(see the Bibliography).
            */

            /*13.2.2 Viability
            *
                *When playing multimedia content, the conforming reader shall often make decisions such as which player software and which options, such as volume and duration, to use.
                *
                *In making these decisions, the viewer shall determine the viability of the objects used.
                *If an object is considered non - viable, the media should not be played. 
                *If the object is viable, the media should be played, though possibly under less than optimum conditions.
                *
                *There are several entries in the multimedia object dictionaries whose values shall have an effect on viability. 
                *In particular, some of the object dictionaries define two entries that divide options into one of two categories:
                *
                *  •   MH(“must honour”): The options specified by this entry shall be honoured; otherwise, the containing object shall be considered non - viable.
                *
                *  •   BE(“best effort”): An attempt should be made to honour the options; however, if they cannot be honoured, the containing object is still considered viable.
                *
                *MH and BE are both dictionaries, and the same entries shall be defined for both of them.
                *In any dictionary where these entries are allowed, both entries may be present, or only one, or neither.
                *
                *EXAMPLE   The media play parameters dictionary(see Table 279) allows the playback volume to be set by means of the V entry in its MH and BE dictionaries(see Table 280).
                *
                *If the specified volume cannot be honoured, the object shall be considered non - viable if V is in the MHdictionary, and playback shall not occur. 
                *If V is in the BE dictionary(and not also in the MH dictionary), playback should still occur: the playing software attempts to honour the specified option as best it can.
                *
                *Using this mechanism, authors may specify minimum requirements (MH) and preferred options (BE). 
                *They may also specify how entries that are added in the future to the multimedia dictionaries shall be interpreted by old conforming readers. 
                *If an entry that is unrecognized by the viewer is in the MH dictionary, the object shall beconsidered non-viable. 
                *If an unrecognized entry is in a BE dictionary, the entry shall be ignored and viability shall be unaffected. 
                *Unless otherwise stated, an object shall be considered non-viable if its MH dictionary contains an unrecognized key or an unrecognized value for a recognized key.
                *
                *The following rules apply to the entries in MH and BE dictionaries, which behave somewhat differently from other PDF dictionaries:
                *
                *  •   If an entry is required, the requirement is met if the entry is present in either the MH dictionary or the BEdictionary.
                *
                *  •   If an optional entry is not present in either dictionary, it shall be considered to be present with its default value(if one is defined) in the BE dictionary.
                *
                *  •   If an instance of the same entry is present in both MH and BE, the instance in the BE dictionary shall beignored unless otherwise specified.
                *
                *  •   If the value of an entry in an MH or a BE dictionary is a dictionary or array, it shall be treated as an atomic unit when determining viability.
                *      That is, all entries within the dictionary or array shall be honoured for the containing object to be viable.
                *
                *NOTE      When determining whether entries can be honoured, it is not required that each one be evaluated independently, since they may be dependent on one another.
                *          That is, a conforming reader or player may examine multiple entries at once(even within different dictionaries) to determine whether their values can be honoured.
                *
                *The following media objects may have MH and BE dictionaries.They function as described previously, except where noted in the individual sub - clauses:
                *
                *  •   Rendition                   (Table 267)
                *
                *  •   Media clip data             (Table 276)
                *
                *  •   Media clip section          (Table 278)
                *
                *  •   Media play parameters       (Table 280)
                *
                *  •   Media screen parameters     (Table 283)
                */

            /*13.2.3 Renditions
            */
                
            /*13.2.3.1 General
                *
                *There are two types of rendition objects:
                *
                *  •   A media rendition(see 13.2.3.2, “Media Renditions”) is a basic media object that specifies what to play, how to play it, and where to play it.
                *
                *  •   A selector rendition(see 13.2.3.3, “Selector Renditions”) contains an ordered list of renditions. 
                *      This list may include other selector renditions, resulting in a tree whose leaves are media renditions.
                *      The conforming reader should play the first viable media rendition it encounters in the tree(see 13.2.2, “Viability”).
                *
                *NOTE 1    Table 266 shows the entries common to all rendition dictionaries. 
                *          The N entry in a rendition dictionary specifies a name that can be used to access the rendition object by means of name tree lookup(see Table 31).
                *          JavaScript actions(see 12.6.4.16, “JavaScript Actions”), for example, use this mechanism.
                *
                *Since the values referenced by name trees shall be indirect objects, all rendition objects should be indirect objects.
                *
                *NOTE 2        A rendition dictionary is not required to have a name tree entry.
                *              When it does, the conforming reader should ensure that the name specified in the tree is kept the same as the value of the N entry(for example, if the user interface allows the name to be changed). 
                *              A document should not contain multiple renditions with the same name.
                *
                *The MH and BE entries are dictionaries whose entries may be present in one or the other of them, as described in 13.2.2, “Viability.” 
                *For renditions, these dictionaries shall have a single entry C(see Table 267), whose value shall have a media criteria dictionary specifying a set of criteria that shall be met for the rendition to be considered viable(see Table 268).
                *
                *The media criteria dictionary behaves somewhat differently than other MH/BE entries, as they are described in 13.2.2, “Viability.” 
                *The criteria specified by all of its entries shall be met regardless of whether they are in an MHor a BE dictionary. 
                *The only exception is that if an entry in a BE dictionary is unrecognized by the conforming reader, it shall not affect the viability of the object. 
                *If a media criteria dictionary is present in both MH and BE, the entries in both dictionaries shall be individually evaluated, with MH taking precedence (corresponding BEentries shall be ignored).
                *
                *Table 266 - Entries common to all rendition dictionaries
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that dictionary describes; if present, shall be Rendition for a rendition object.
                *
                *              S                   name                (Required) The type of rendition that this dictionary describes. May be MRfor media rendition or SR for selector rendition. The rendition shall be considered non-viable if the conforming reader does not recognize the value of this entry.
                *
                *              N                   text string         (Optional) A Unicode-encoded text string specifying the name of the rendition for use in a user interface and for name tree lookup by JavaScript actions.
                *
                *              MH                  dictionary          (Optional) A dictionary whose entries (see Table 267) shall be honoured for the rendition to be considered viable.
                *
                *              BE                  dictionary          (Optional) A dictionary whose entries (see Table 267) shall only be honoured in a “best effort” sense.  
                *
                *Table 267 - Entries in a rendition MH/BE dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              C                   dictonary           (Optional) A media criteria dictionary (see Table 268).
                *                                                      The media criteria dictionary behaves somewhat differently than other MH/ BE entries described in 13.2.2, “Viability.” 
                *                                                      The criteria specified by all of its entries shall be met regardless of whether it is in an MH or a BE dictionary. 
                *                                                      The only exception is that if an entry in a BE dictionary is unrecognized by the conforming reader, it shall not affect the viability of the object.
                *
                *Table 268 - Entries in a media criteria dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaCriteria for a media criteria dictionary.
                *
                *              A                   boolean             (Optional) If specified, the value of this entry shall match the user’s preference for whether to hear audio descriptions in order for this object to be viable.
                *                                                      NOTE 1  Equivalent to SMIL’s systemAudioDesc attribute.
                *
                *              C                   boolean             (Optional) If specified, the value of this entry shall match the user’s preference for whether to see text captions in order for this object to be viable.
                *                                                      NOTE 2  Equivalent to SMIL’s systemCaptions attribute.
                *
                *              O                   boolean             (Optional) If specified, the value of this entry shall match the user’s preference for whether to hear audio overdubs in order for this object to be viable.
                *
                *              S                   boolean             (Optional) If specified, the value of this entry shall match the user’s preference for whether to see subtitles in order for this object to be viable.
                *
                *              R                   integer             (Optional) If specified, the system’s bandwidth (in bits per second) shall be greater than or equal to the value of this entry in order for this object to be viable.
                *                                                      NOTE 3  Equivalent to SMIL’s systemBitrate attribute.
                *
                *              D                   dictionary          (Optional) A dictionary (see Table 269) specifying the minimum bit depth required in order for this object to be viable.
                *                                                      NOTE 4  Equivalent to SMIL’s systemScreenDepth attribute.
                *
                *              Z                   dictionary          (Optional) A dictionary (see Table 270) specifying the minimum screen size required in order for this object to be viable.
                *                                                      NOTE 5  Equivalent to SMIL’s systemScreenSize attribute.
                *
                *              V                   array               (Optional) An array of software identifier objects (see 13.2.7.4, “Software Identifier Dictionary”). 
                *                                                      If this entry is present and non-empty, the conforming reader shall be identified by one or more of the objects in the array in order for this object to be viable.
                *
                *              P                   array               (Optional) An array containing one or two name objects specifying a minimum and optionally a maximum PDF language version, in the same format as the Version entry in the document catalog (see Table 28). 
                *                                                      If this entry is present and non-empty, the version of multimedia constructs fully supported by the conforming reader shall be within the specified range in order for this object to be viable.
                *
                *              L                   array               (Optional) An array of language identifiers (see 14.9.2.2, “Language Identifiers”). 
                *                                                      If this entry is present and non-empty, the language in which the conforming reader is running shall exactly match a language identifier, or consist only of a primary code that matches the primary code of an identifier, in order for this object to be viable.
                *                                                      NOTE 6  Equivalent to SMIL’s systemLanguage attribute.
                *
                *Table 269 - Entries in a minimum bit depth dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MinBitDepth for a minimum bit depth dictionary
                *
                *              V                   integer             (Required) A positive integer (0 or greater) specifying the minimum screen depth (in bits) of the monitor for the rendition to be viable. 
                *                                                      A negative value shall not be allowed.
                *
                *              M                   integer             (Optional) A monitor specifier (see Table 270) that specifies which monitor the value of V should be tested against. 
                *                                                      If the value is unrecognized, the object shall not be viable.
                *                                                      Default value: 0.
                *
                *Table 270 - Entries in a minimum screen size dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MinScreenSize for a rendition object.
                *
                *              V                   array               (Required) An array containing two non-negative integers. 
                *                                                      The width and height (in pixels) of the monitor specified by M shall be greater than or equal to the values of the first and second integers in the array, respectively, in order for this object to be viable.
                *
                *              M                   integer             (Optional) A monitor specifier (see Table 293) that specifies which monitor the value of V should be tested against. 
                *                                                      If the value is unrecognized, the object shall be not viable.
                *                                                      Default value: 0.
                */

                /*13.2.3.2 Media Renditions
                *
                *Table 271 lists the entries in a media rendition dictionary. 
                *Its entries specify what media should be played(C), how(P), and where(SP) it should be played. 
                *A media rendition object shall be viable if and only if the objects referenced by its C, P, and SP entries are viable.
                *
                *C may be omitted only in cases where a referenced player takes no meaningful input.
                *This requires that P shall be present and that its referenced media play parameters dictionary(see Table 279) shall contain a PL entry, whose referenced media players dictionary(see 13.2.7.2, “Media Players Dictionary”) has a non - empty MUarray or a non-empty A array.
                *
                *Table 271 - Additional entries in a media rendition dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              C                   dictionary          (Optional) A media clip dictionary (see 13.2.4, “Media Clip Objects”) that specifies what should be played when the media rendition object is played.
                *
                *              P                   dictionary          (Required if C is not present, otherwise optional) A media play parametersdictionary (see 13.2.5, “Media Play Parameters””) that specifies how the media rendition object should be played.
                *                                                      Default value: a media play parameters dictionary whose entries(see Table 279) all contain their default values.
                *
                *              SP                  dictionary          (Optional) A media screen parameters dictionary (see 13.2.6, “Media Screen Parameters”) that specifies where the media rendition object should be played.
                *                                                      Default value: a media screen parameters dictionary whose entries(see Table 282) all contain their default values.
                */

                /*13.2.3.3 Selector Renditions
                *
                *A selector rendition dictionary shall specify an array of rendition objects in its R entry (see Table 272). 
                *The renditions in this array should be ordered by preference, with the most preferred rendition first. 
                *At play-time, the renditions in the array shall be evaluated and the first viable media rendition, if any, shall be played. If one of the renditions is itself a selector, that selector shall be evaluated in turn, yielding the equivalent of a depth-first tree search. 
                *A selector rendition itself may be non-viable; in this case, none of its associated media renditions shall be evaluated (in effect, this branch of the tree is skipped).
                *
                *NOTE      This mechanism may be used, for example, to specify that a large video clip should be used on high - bandwidth machines and a smaller clip should be used on low - bandwidth machines.
                *
                *Table 272 - Additional entries specific to a selector rendition dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              R                   array               (Required) An array of rendition objects. 
                *                                                      The first viable media rendition object found in the array, or nested within a selector rendition in the array, should be used. 
                *                                                      An empty array is legal.
                */                                                      

            /*13.2.4 Media Clip Objects
           */
                
                /*13.2.4.1 General
                *
                *There are two types of media clip objects, determined by the subtype S, which can be either MCD for media clip data(see 13.2.4.2, “Media Clip Data”) or MCS for media clip section(see 13.2.4.3, “Media Clip Section”).
                *The entries common to all media clip dictionaries are listed in Table 273.
                *
                *Table 273 - Entries common to all media clip dictionaries
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaClip for a media clip dictionary.
                *
                *              S                   name                (Required) The subtype of media clip that this dictionary describes. May be MCD for media clip data (see 13.2.4.2, “Media Clip Data”) or MCS for a media clip section (see 13.2.4.3, “Media Clip Section”). 
                *                                                      The media clip shall be considered non-viable if the conforming reader does not recognize the value of this entry.
                *
                *              N                   text string         (Optional) The name of the media clip, for use in the user interface.
                *
                */

                /*13.2.4.2 Media Clip Data
                *
                *A media clip data dictionary defines the data for a media object that can be played.Its entries are listed in Table 274
                *
                *NOTE 1    It may reference a URL to a streaming video presentation or a movie embedded in the PDF file.
                *
                *Table 274 - Additionaol entries in a media clip data dictionary
                *
                *              [Key]               [Type]                      [Value]
                *
                *              D                   file                        (Required) A full file specification or form XObject that specifies the actual media data.
                *                                  specification or
                *                                  stream
                *
                *              CT                  ASCII string                (Optional; not allowed for form XObjects) An ASCII string identifying the type of data in D. 
                *                                                              The string should conform to the content type specification described in Internet RFC 2045, Multipurpose Internet Mail Extensions (MIME) Part One: Format of Internet Message Bodies (see the Bibliography).
                *
                *              P                   dictionary                  (Optional) A media permissions dictionary (see Table 275) containing permissions that control the use of the media data. 
                *                                                              Default value: a media permissions dictionary containing default values.
                *
                *              Alt                 array                       (Optional) An array that provides alternate text descriptions for the media clip data in case it cannot be played; see 14.9.2.4, “Multi-language Text Arrays.”
                *
                *              PL                  dictionary                  (Optional) A media players dictionary (see 13.2.7.2, “Media Players Dictionary”) that identifies, among other things, players that are legal and not legal for playing the media.
                *                                                              If the media players dictionary is non - viable, the media clip data shall be non - viable.
                *
                *              MH                  dictionary                  (Optional) A dictionary whose entries (see Table 276) shall be honoured for the media clip data to be considered viable.
                *
                *              BE                  dictionary                  (Optional) A dictionary whose entries (see Table 276) should only be honoured in a “best effort” sense.
                *
                *
                *The media clip data object shall be considered non-viable if the object referenced by the D entry does not contain a Type entry, the Type entry is unrecognized, or the referenced object is not a dictionary or stream.
                *
                *This shall effectively exclude the use of simple file specifications(see 7.11, “File Specifications”).
                *
                *If D references a file specification that has an embedded file stream(see 7.11.4, “Embedded File Streams”), the embedded file stream’s Subtype entry shall be ignored if present, and the media clip data dictionary’s CT entry shall identify the type of data.
                *
                *If D references a form XObject, the associated player is implicitly the conforming reader, and the form XObject shall be rendered as if it were any other data type.
                *
                *NOTE 2        The F and D entries in the media play parameters dictionary(see Table 279) should apply to a form XObject just as they do to a QuickTime movie.
                *
                *For media other than form XObjects, the media clip object shall provide enough information to allow a conforming reader to locate an appropriate player.
                *This may be done by providing one or both of the following entries, the first being the preferred method:
                *
                *  •   A CT entry that specifies the content type of the media.If this entry is present, any player that is selected shall support this content type.
                *
                *  •   A PL entry that specifies one or more players that may be used to play the referenced media. if CT is present, there should also be a PL present.
                *
                *The P entry specifies a media permissions dictionary (see Table 275) specifying the manner in which the data referenced by the media may be used by a conforming reader. 
                *These permissions allow authors control over how their data is exposed to operations that could allow it to be copied. /
                *If the dictionary contains unrecognized entries or entries with unrecognized values, it shall be considered non-viable, and the conforming reader shallnot play the media.
                *
                *Table 275 - Entries in a media permissions dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaPermissions for a media permissions dictionary.
                *
                *              TF                  ASCII string        (Optional) An ASCII string indicating the circumstances under which it is acceptable to write a temporary file in order to play a media clip. 
                *                                                      Valid values are:
                *
                *                                                      (TEMPNEVER)         Never allowed.
                *                                                      (TEMPEXTRACT)       Allowed only if the document permissions allow content extraction; when bit 5 of the user access permissions(see Table 22) is set.
                *                                                      (TEMPACCESS)        Allowed only if the document permissions allow content extraction, including for accessibility purposes; when bits 5 or 10 of the user access permissions(see Table 22) are set, or both.
                *                                                      (TEMPALWAYS)        Always allowed.
                *                  
                *                                                      Default value: (TEMPNEVER).
                *                                                      An unrecognized value shall be treated as (TEMPNEVER).
                *
                *The BU entry in the media clip data MH and BE dictionaries (see Table 276) specifies a base URL for the media data. 
                *Relative URLs in the media (which point to auxiliary files or are used for hyperlinking) should be resolved with respect to the value of BU. 
                *The following are additional requirements concerning the BU entry:
                *
                *  •   If BU is in the MH dictionary and the base URL is not honoured the media clip data shall be non - viable.
                *
                *NOTE 3        An example of this is that the player does not accept base URLs.
                *
                *  •   Determining the viability of the object shall not require checking whether the base URL is valid
                *
                *NOTE 4        The target host exists.
                *
                *  •   Absolute URls within the media shall not be affected.
                *
                *  •   If the media itself contains a base URL, that value shall be used in preference to BU.
                *
                *NOTE 5        An example of this is that the < BASE > element is defined in HTML.
                *
                *  •   BU is completely independent of and unrelated to the value of the URI entry in the document catalogue(see 7.7.2, “Document Catalog”).
                *
                *  •   If BU is not present and the media is embedded within the document, the URL to the PDF file itself shall be used as if it were the value of a BU entry in the BE dictionary; that is, as an implicit best-effort base URL.
                *
                *Table 276 - Entries in a media clip data MH/BE dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              BU                  ASCII string        (Optional) An absolute URL that shall be used as the base URL in resolving any relative URLs found within the media data.
                *
                */

                /*13.2.4.3 Media Clip Section
                *
                *A media clip section dictionary (see Table 277) defines a continuous section of another media clip object (known as the next-level media clip object). 
                *The next-level media clip object, specified by the D entry, may be either a media clip data object or another media clip section object. 
                *However, the linked list formed by the D entries of media clip sections shall terminate in a media clip data object. 
                *If the next-level media object is non-viable, the media clip section shall be also non-viable.
                *
                *NOTE 1        A media clip section could define a 15-minute segment of a media clip data object representing a two-hour movie.
                *
                *Table 277 - Additional entries in a media clip section dictionary
                *
                *              [Key]           [Type]              [Value]
                *
                *              D               dictionary          (Required) The media clip section or media clip data object (the next-level media object) of which this media clip section object defines a continuous section.
                *
                *              Alt             array               (Optional) An array that provides alternate text descriptions for the media clip section in case it cannot be played; see 14.9.2.4, “Multi-language Text Arrays.”
                *
                *              MH              dictionary          (Optional) A dictionary whose entries (see Table 278) shall be honoured for the media clip section to be considered viable.
                *
                *              BE              dictionary          (Optional) A dictionary whose entries (see Table 278) shall only be honoured in a “best effort” sense.
                *
                *
                *The B and E entries in the media clip section’s MH and BE dictionaries (see Table 278) shall define a subsection of the next-level media object referenced by D by specifying beginning and ending offsets into it. 
                *Depending on the media type, the offsets may be specified by time, frames, or markers (see 13.2.6.2, “Media Offset Dictionary”). 
                *B and E are not required to specify the same type of offset.
                *
                *The following rules apply to these offsets:
                *
                *  •   For media types where an offset makes no sense(such as JPEG images), B and E shall be ignored, with no effect on viability.
                *
                *  •   When B or E are specified by time or frames, their value shall be considered to be relative to the start of the next - level media clip. 
                *      However, if E specifies an offset beyond the end of the next - level media clip, the end value shall be used instead, and there is no effect on viability.
                *
                *  •   When B or E are specified by markers, there shall be a corresponding absolute offset into the underlying media clip data object.
                *      If this offset is not within the range defined by the next-level media clip(if any), or if the marker is not present in the underlying media clip, the existence of the entry shall be ignored, and there is no effect on viability.
                *
                *  •   If the absolute offset derived from the values of all B entries in a media clip section chain is greater than or equal to the absolute offset derived from the values of all E entries, an empty range shall be defined.
                *      An empty range is legal.
                *
                *  •   Any B or E entry in a media clip section’s MH dictionary shall be honoured at play-time in order for the media clip section to be considered viable.
                *
                *NOTE 2    The entry may not be honored if its value was not viable or if the player did not support its value; for example, the player did not support markers.
                *
                *  •   If a B or E entry is in a media clip section’s MH dictionary, all B or E entries, respectively, at deeper levels(closer to the media clip data), shall be evaluated as if they were in an MH dictionary(even if they are actually within BE dictionaries).
                *
                *  •   If B or E entry in a BE dictionary cannot be supported, it may be ignored at play - time.
                *
                *Table 278 - Entries in a media clip section MH/BE dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              B                   dictionary          (Optional) A media offset dictionary (see 13.2.6.2, “Media Offset Dictionary”) that specifies the offset into the next-level media object at which the media clip section begins. 
                *                                                      Default: the start of the next-level media object.
                *
                *              E                   dictionary          (Optional) A media offset dictionary (see 13.2.6.2, “Media Offset Dictionary”) that specifies the offset into the next-level media object at which the media clip section ends. 
                *                                                      Default: the end of the next-level media object.
                */

            /*13.2.5 Media Play Parameters
            *
                *A media play parameters dictionary specifies how a media object should be played. It shall be referenced from a media rendition (see 13.2.3.2, “Media Renditions”).
                *
                *Table 279 - Entries in a media play parameters dictionary
                *
                *              [Key]           [Type]              [Value]
                *
                *              Type            name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaPlayParams for a media play parameters dictionary.
                *
                *              PL              dictionary          (Optional) A media players dictionary (see 13.2.7.2, “Media Players Dictionary”) that identifies, among other things, players that are legal and not legal for playing the media.
                *                                                  If this object is non - viable, the media play parameters dictionary shall be considered non - viable.
                *
                *              MH              dictionary          (Optional) A dictionary whose entries (see Table 278) shall be honoured for the media play parameters to be considered viable.
                *
                *              BE              dictionary          (Optional) A dictionary whose entries (see Table 278) shall only be honoured in a “best effort” sense.
                *
                *Table 280 - Entries in a media play parameters MH/BE dictionary
                *
                *              [Key]           [Type]              [Value]
                *
                *              V               integer             (Optional) An integer that specifies the desired volume level as a percentage of recorded volume level. 
                *                                                  A zero value shall be equivalent to mute; negative values shall be illegal. 
                *                                                  Default value: 100.
                *
                *              C               boolean             (Optional) A flag specifying whether to display a player-specific controller user interface when playing.
                *
                *                                                  EXAMPLE     play / pause / stop controls.
                *
                *                                                  Default value: false
                *
                *              F               integer             (Optional) The manner in which the player shall treat a visual media type that does not exactly fit the rectangle in which it plays.
                *
                *                                                  0   The media’s width and height shall be scaled while preserving the aspect ratio so that the media and play rectangles have the greatest possible intersection while still displaying all media content.
                *
                *                                                  NOTE 1      Same as “meet” value of SMIL’s fit attribute.
                *      
                *                                                  1   The media’s width and height shall be scaled while preserving the aspect ratio so that the play rectangle is entirely filled, and the amount of media content that does not fit within the play rectangle shall be minimized.
                *
                *                                                  NOTE 2      Same as “slice” value of SMIL’s fit attribute.
                *
                *                                                  2   The media’s width and height shall be scaled independently so that the media and play rectangles are the same; the aspect ratio shall not be preserved.
                *
                *                                                  NOTE 3      Same as “fill” value of SMIL’s fit attribute.
                *
                *                                                  3   The media shall not be scaled. A scrolling user interface shall be provided if the media rectangle is wider or taller than the play rectangle.
                *
                *                                                  NOTE 4      Same as “scroll” value of SMIL’s fit attribute.
                *
                *                                                  4   The media shall not be scaled.Only the portions of the media rectangle that intersect the play rectangle shall be displayed.
                *
                *                                                  NOTE 5      Same as “hidden” value of SMIL’s fit attribute.
                *
                *                                                  5   Use the player’s default setting (author has no preference).
                *
                *                                                  Default value: 5.
                *
                *                                                  An unrecognized value shall be treated as the default value if the entry is in a BE dictionary.
                *                                                  If the entry is in an MH dictionary and it has an unrecognized value, the object shall be considered non-viable.
                *
                *              D               dictionary          (Optional) A media duration dictionary (see Table 281). 
                *                                                  Default value: a dictionary specifying the intrinsic duration (see RC).
                *
                *              A               boolean             (Optional) If true, the media shall automatically play when activated. 
                *                                                  If false, the media shall be initially paused when activated.
                *
                *                                                  EXAMPLE     The first frame is displayed.
                *
                *                                                  Relevant only for media that may be paused.
                *                                                  Default value: true.
                *
                *              RC              number              (Optional) Specifies the number of iterations of the duration D to repeat.
                *
                *                                                  NOTE 6      Similar to SMIL’s repeatCount attribute.Zero means repeat forever. 
                *                                                              Negative values shall be illegal; non - integral values shall be legal.
                *
                *                                                  Default value: 1.0.
                *
                *The value of the D entry is a media duration dictionary, whose entries are shown in Table 281. It specifies a temporal duration.
                *
                *NOTE 1        The D entry dictionary temporal duration corresponds to the notion of a simple duration in SMIL.
                *
                *The duration may be a specific amount of time, it may be infinity, or it may be the media’s intrinsic duration.
                *
                *EXAMPLE       The intrinsic duration of a two-hour QuickTime movie is two hours.
                *
                *The intrinsic duration may be modified when a media clip section (see 13.2.4.3, “Media Clip Section”) is used: the intrinsic duration shall be the difference between the absolute begin and end offsets. 
                *For a media type having no notion of time (such as a JPEG image), the duration shall be considered to be infinity.
                *
                *If the simple duration is longer than the intrinsic duration, the player shall freeze the media in its final state until the simple duration has elapsed.
                *For visual media types, the last appearance(frame) shall be displayed.For aural media types, the media is logically frozen but shall not continue to produce sound.
                *
                *NOTE 2        In this case, the RC entry, which specifies a repeat count, applies to the simple duration; therefore, the entire play - pause sequence is repeated RC times.
                *
                *Table 281 - Entries in a media duration dictionary
                *
                *                  [Key]               [Type]              [Value]
                *
                *                  Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaDuration for a media duration dictionary.
                *
                *                  S                   name                (Required) The subtype of media duration dictionary. 
                *                                                          Valid values are:
                *          
                *                                                          I       The duration is the intrinsic duration of the associated media
                *
                *                                                          F       The duration is infinity
                *
                *                                                          T       The duration shall be specified by the T entry
                *
                *                                                          The media duration dictionary shall be considered non-viable if the conforming reader does not recognize the value of this entry.
                *
                *                  T                   dictionary          (Required if the value of S is T; otherwise ignored) A timespan dictionary specifying an explicit duration (see Table 289). 
                *                                                          A negative duration is illegal.
                */

            /*13.2.6 Media Screen Parameters
            */
                
                /*13.2.6.1 General
                *
                *A media screen parameters dictionary(see Table 282) shall specify where a media object should be played. 
                *It shall contain MH and BE dictionaries(see Table 283), which shall function as discussed in 13.2.2, “Viability.” 
                *All media clips that are being played shall be associated with a particular document and shall be stopped when the document is closed.
                *
                *NOTE      Conforming readers should disallow floating windows and full-screen windows unless specifically allowed by the user.
                *          The reason is that document - based security attacks are possible if windows containing arbitrary media content can be displayed without indicating to the user that the window is merely hosting a media object.
                *          This recommendation may be relaxed if it is possible to communicate the nature of such windows to the user; for example, with text in a floating window’s title bar.
                *
                *Table 282 - Entries in a media screen parameters dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaScreenParams for a media screen parameters dictionary.
                *
                *          MH                  dictionary          (Optional) A dictionary whose entries (see Table 283) shall be honoured for the media screen parameters to be considered viable.
                *
                *          BE                  dictionary          (Optional) A dictionary whose entries (see Table 283) should be honoured.
                *
                *
                *Table 283 - Entries in a media screen parameters MH/BE dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          W                   integer             (Optional) The type of window that the media object shall play in:
                *
                *                                                  0   A floating window
                *
                *                                                  1   A full-screen window that obscures all other windows
                *
                *                                                  2   A hidden window
                *
                *                                                  3   The rectangle occupied by the screen annotation(see 12.5.6.18, “Screen Annotations”) associated with the media rendition
                *
                *                                                  Default value: 3.Unrecognized value in MH: object is non - viable; in BE: treat as default value.
                *
                *          B                   array               (Optional) An array of three numbers in the range 0.0 to 1.0 that shall specify the components in the DeviceRGB colour space of the background colour for the rectangle in which the media is being played. 
                *                                                  This colour shall be used if the media object does not entirely cover the rectangle or if it has transparent sections. 
                *                                                  It shall be ignored for hidden windows.
                *
                *                                                  Default value: implementation - defined.
                *                                                  The conforming reader should choose a reasonable value based on the value of W.
                *
                *                                                  EXAMPLE 1   A system default background colour for floating windows or a user - preferred background colour for full - screen windows.
                *                                                  
                *                                                  If a media format has an intrinsic background colour, B shall not override it.
                *                                                  However, the B colour shall be visible if the media has transparent areas or otherwise does not cover the entire window.
                *
                *          O                   number              (Optional) A number in the range 0.0 to 1.0 specifying the constant opacity value that shall be used in painting the background colour specified by B. A value below 1.0 means the window shall be transparent.
                *
                *                                                  EXAMPLE 2       Windows behind a floating window show through if the media does not cover the entire floating window.
                *
                *                                                  A value of 0.0 shall indicate full transparency and shall make B irrelevant.
                *                                                  It shall be ignored for full - screen and hidden windows.
                *
                *                                                  Default value: 1.0(fully opaque).
                *
                *          M                   integer             (Optional) A monitor specifier (see Table 293) that shall specify which monitor in a multi-monitor system, a floating or full-screen window shall appear on. 
                *                                                  Ignored for other types.
                *              
                *                                                  Default value: 0(document monitor).
                *                                                  Unrecognized value in MH: object is non - viable; in BE: treat as default value.
                *
                *          F                   dictionary          (Required if the value of W is 0; otherwise ignored) 
                *                                                  A floating window parameters dictionary (see Table 284) that shall specify the size, position, and options used in displaying floating windows.
                *
                *
                *The F entry in the media screen parameters MH/BE dictionaries shall be a floating window parameters dictionary, whose entries are listed in Table 284. 
                *The entries in the floating window parameters dictionary shall be treated as if they were present in the MH or BE dictionaries that they are referenced from. 
                *That is, the contained entries shall be individually evaluated for viability rather than the dictionary being evaluated as a whole. 
                *(There may be an F entry in both MH and BE. In such a case, if a given entry is present in both floating window parameters dictionaries, the one in the MH dictionary shall take precedence.)
                *
                *The D, P, and RT entries shall be used to specify the rectangle that the floating window occupies. 
                *Once created, the floating window’s size and position shall not be tied to any other window, even if the initial size or position was computed relative to other windows.
                *
                *Unrecognized values for the R, P, RT, and O entries shall be handled as follows: if they are nested within an MH dictionary, the floating window parameters object(and hence the media screen parameters object) shall be considered non-viable; 
                *if they are nested within a BE dictionary, they shall be considered to have their default values.
                *
                *Table 284 - Entries in a floating window parameters dictionary
                *
                *              [Key]               [Type]                  [Value]
                *
                *              Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be FWParams for a floating window parameters dictionary.
                *
                *              D                   array                   (Required) An array containing two non-negative integers that shall represent the floating window’s width and height, in pixels, respectively. 
                *                                                          These values shall correspond to the dimensions of the rectangle in which the media shall play, not including such items as title bar and resizing handles.
                *
                *              RT                  integer                 (Optional) The window relative to which the floating window shall be positioned:
                *
                *                                                          0       The document window
                *
                *                                                          1       The application window
                *
                *                                                          2       The full virtual desktop
                *
                *                                                          3       The monitor specified by M in the media screen parameters MH or BEdictionary(see 9.22)
                *
                *                                                          Default value: 0.
                *
                *              P                   integer                 (Optional) The location where the floating window (including such items as title bar and resizing handles) shall be positioned relative to the window specified by RT:
                *
                *                                                          0       Upper - left corner
                *
                *                                                          1       Upper center
                *
                *                                                          2       Upper - right corner
                *
                *                                                          3       Center left
                *
                *                                                          4       Center
                *
                *                                                          5       Center right
                *
                *                                                          6       Lower - left corner
                *
                *                                                          7       Lower center
                *
                *                                                          8       Lower - right corner
                *
                *                                                          Default value: 4.
                *
                *              O                   integer                 (Optional) Specifies what shall occur if the floating window is positioned totally or partially offscreen (that is, not visible on any physical monitor):
                *
                *                                                          0       Take no special action
                *
                *                                                          1       Move and/ or resize the window so that it is on - screen
                *
                *                                                          2       Consider the object to be non-viable
                *
                *                                                          Default value: 1
                *
                *              T                   boolean                 (Optional) If true, the floating window shall have a title bar. 
                *                                                          Default value: true.
                *
                *              UC                  boolean                 (Optional; meaningful only if T is true) If true, the floating window shall include user interface elements that allow a user to close a floating window.
                *                                                          Default value: true
                *
                *              R                   integer                 (Optional) Specifies whether the floating window may be resized by a user:
                *
                *                                                          0       May not be resized
                *
                *                                                          1       May be resized only if aspect ratio is preserved
                *
                *                                                          2       May be resized without preserving aspect ratio
                *
                *                                                          Default value: 0.
                *
                *              TT                  array                   (Optional; meaningful only if T is true) An array providing text to display on the floating window’s title bar. 
                *                                                          See 14.9.2.4, “Multi-language Text Arrays.” 
                *                                                          If this entry is not present, the conforming reader may provide default text.
                */

                /*13.2.6.2 Media Offset Dictionary
                *
                *A media offset dictionary (Table 285) shall specify an offset into a media object. 
                *The S (subtype) entry indicates how the offset shall be specified: in terms of time, frames or markers. Different media types support different types of offsets.
                *
                *EXAMPLE       Time, “10 seconds”; frames, “frame 20”; markers, “Chapter One.”
                *
                *Table 285 - Entries common to all media offset dictionaries
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaOffset for a media offset dictionary.
                *
                *              S                   name                (Required) The subtype of media offset dictionary. Valid values shall be:
                *
                *                                                      T       A media offset time dictionary(see Table 286)
                *
                *                                                      F       A media offset frame dictionary(see Table 287)
                *
                *                                                      M       A media offset marker dictionary(see Table 288)
                *
                *                                                      The rendition shall be considered non-viable if the conforming reader does not recognize the value of this entry.
                *
                *Table 286 - Additional entries in a media offset time dicitonary
                *
                *              [Key]               [Type]              [Value]
                *
                *              T                   dictionary          (Required) A timespan dictionary (see Table 289) that shall specify a temporal offset into a media object. 
                *                                                      Negative timespans are not allowed in this context. 
                *                                                      The media offset time dictionary is non-viable if its timespan dictionary is non-viable.
                *
                *Table 287 - Additional entries in a media offset frame dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              F                   integer             (Required) Shall specify a frame within a media object. Frame numbers begin at 0; negative frame numbers are not allowed.
                *
                *Table 288 - Additional entries in a media offset marker dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              M                   text string         (Required) A text string that identifies a named offset within a media object.
                */

                /*13.2.6.3 Timespan Dictionary
                *
                *A timespan dictionary shall specify a length of time; its entries are shown in Table 289.
                *
                *Table 289 - Entries in a timespan dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Timespan for a timespan dictionary.
                *
                *              S                   name                (Required) The subtype of timespan dictionary. The value shall be S (simple timespan). 
                *                                                      The rendition shall be considered non-viable if the conforming reader does not recognize the value of this entry.
                *
                *              V                   number              (Required) The number of seconds in the timespan. Non-integral values shall be allowed. 
                *                                                      Negative values shall be allowed, but may be disallowed in some contexts.
                *                                                      (PDF 1.5) Negative values are not allowed.
                *                                                      This entry shall be used only if the value of the S entry is S.
                *                                                      Subtypes defined in the future need not use this entry.
            */

           /*13.2.7 Other Multimedia Objects
            */

                /*13.2.7.1 General
                *
                *This sub-clause defines several dictionary types that are referenced by the previous sub - clauses.
                */

                /*13.2.7.2 Media Players Dictionary
                *
                *A media players dictionary may be referenced by media clip data(see 13.2.4.2, “Media Clip Data”) and media play parameters(see 13.2.5, “Media Play Parameters”) dictionaries, and shall allow them to specify which players may or may not be used to play the associated media.
                *The media players dictionary references media player info dictionaries(see 13.2.7.3, “Media Player Info Dictionary”) that shall provide specific information about each player.
                *
                *Table 290 - Entries in a media players dictionary
                *
                *              [Key]                   [Type]                  [Value]
                *
                *              Type                    name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaPlayers for a media players dictionary.
                *
                *              MU                      array                   (Optional) An array of media player info dictionaries (see Table 291) that shall specify a set of players, one of which shall be used in playing the associated media object.
                *                                                              Any players specified in NU are effectively removed from MU.
                *
                *                                                              EXAMPLE         If MU specifies versions 1 through 5 of a player and NUspecifies versions 1 and 2 of the same player, MU is effectively versions 3 through 5.
                *
                *              A                       array                   (Optional) An array of media player info dictionaries (see Table 291) that shall specify a set of players, any of which may be used in playing the associated media object. 
                *                                                              If MU is also present and non-empty, A shall be ignored.
                *
                *              NU                      array                   (Optional) An array of media player info dictionaries (see Table 291) that shall specify a set of players that shall not be used in playing the associated media object (even if they are also specified in MU).
                *
                *
                *The MU, A, and NU entries each shall specify one or more media player info dictionaries. An empty array shall be treated as if it is not present. 
                *The media player info dictionaries shall be allowed to specify overlapping player ranges.
                *
                *NOTE 1        MU could contain a media player info dictionary describing versions 1 to 10 of Player X and another describing versions 3 through 5 of Player X.
                *
                *If a non - viable media player info dictionary is referenced by MU, NU, or A, it shall be treated as if it were not present in its original array, and a media player info dictionary containing the same software identifier dictionary(see 13.2.7.4, “Software Identifier Dictionary”) shall logically considered present in NU.
                *The same rule shall apply to a media player info dictionary that contains a partially unrecognized software identifier dictionary.
                *
                *Since both media clip data and media play parameters dictionaries may be employed in a play operation, and each may reference a media players dictionary, there is a potential for conflict between the contents of the two media players dictionaries.
                *At play - time, the viewer shall use the following algorithm to determine whether a player present on the machine may be employed.
                *The player may not be used if any of the following conditions are true:
                *
                *Algorithm: Media Player
                *
                *  a)  The content type is known and the player does not support the type.
                *
                *  b)  The player is found in the NU array of either dictionary.
                *
                *  c)  Both dictionaries have non-empty MU arrays and the player is not found in both of them, or only one of the dictionaries has a non - empty MU array and the player is not found in it.
                *
                *  d)  Neither dictionary has a non - empty MU array, the content type is not known, and the player is not found in the A array of either dictionary.
                *
                *If none of the conditions are true, the player may be used.
                *
                *NOTE 2        A player is “found” in the NU, MU, or A arrays if it matches the information found in the PID entry of one of the entries, as described by the Algorithm in 13.2.7.4, “Software Identifier Dictionary.”
                */

                /*13.2.7.3 Media Player Info Dicitonary
                *
                *A media player info dictionary shall provide a variety of information regarding a specific media player. 
                *Its entries (see Table 291) shall associate information with a particular version or range of versions of a player. 
                *As of PDF 1.5, only the PID entry shall provide information about the player, as described in the next sub-clause, 13.2.7.4, “Software Identifier Dictionary.”
                *
                *Table 291 - Entries in a media player info dictionary
                *
                *          [Key]                   [Type]                  [Value]
                *
                *          Type                    name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be MediaPlayerInfo for a media player info dictionary.
                *
                *          PID                     dictionary              (Required) A software identifier dictionary (see 13.2.7.4, “Software Identifier Dictionary”) that shall specify the player name, versions, and operating systems to which this media player info dictionary applies.
                *
                *          MH                      dictionary              (Optional) A dictionary containing entries that shall be honored for this dictionary to be considered viable
                *                                                          Currently, there are no defined entries for this dictionary
                *
                *          BE                      dictionary              (Optional) A dictionary containing entries that need only be honored in a “best effort” sense.
                *                                                          Currently, there are no defined entries for this dictionary
                */
                
                /*13.2.7.4 Software Identifier Dictionary
                */

                    /*13.2.7.4.1 General
                *
                *A software identifier dictionary shall allow software to be identified by name, range of versions, and operating systems; its entries are listed in Table 292.
                *A conforming reader uses this information to determine whether a given media player may be used in a given situation.
                *If the dictionary contains keys that are unrecognized by the conforming reader, it shall be considered to be partially recognized.
                *The conforming reader may or may not decide to treat the software identifier as viable, depending on the context in which it is used.
                *
                *The following procedure shall be used to determine whether a piece of software is considered to match a software identifier dictionary:
                *
                *Algorithm: Software identifier
                *
                *  a)  The software name shall match the name specified by the U entry(see “Software URIs” in 13.2.7.4, “Software Identifier Dictionary”).
                *
                *  b)  The software version shall be within the interval specified by the L, H, LI, and H1 entries(see “Version arrays” in 13.2.7.4, “Software Identifier Dictionary”).
                *
                *  c)  The machine’s operating system name shall be an exact match for one present in the OS array.
                *      If the array is not present or empty, a match shall also be considered to exist.
                *
                *Table 292 - Entries in a software identifier dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be SoftwareIdentifier for a software identifier dictionary.
                *
                *          U                   ASCII string        (Required) A URI that identifies a piece of software (see “Software URIs” in 13.2.7.4, “Software Identifier Dictionary”).
                *
                *          L                   array               (Optional) The lower bound of the range of software versions that this software identifier dictionary specifies (see “Version arrays” in 13.2.7.4, “Software Identifier Dictionary”). 
                *                                                  Default value: the array [0].
                *
                *          LI                  boolean             (Optional) If true, the lower bound of the interval defined by L and H is inclusive; that is, the software version shall be greater than or equal to L (see “Version arrays” in 13.2.7.4, “Software Identifier Dictionary”). 
                *                                                  If false, it shall not be inclusive. 
                *                                                  Default value: true.
                *
                *          H                   array               (Optional) The upper bound of the range of software versions that this software identifier dictionary specifies (see “Version arrays” in 13.2.7.4, “Software Identifier Dictionary”). 
                *                                                  Default value: an empty array [].
                *
                *          HI                  boolean             (Optional) If true, the upper bound of the interval defined by L and H is inclusive; that is, the software version shall be less than or equal to H (see “Version arrays” in 13.2.7.4, “Software Identifier Dictionary”). 
                *                                                  If false, it shall not be inclusive. 
                *                                                  Default value: true.
                *
                *          OS                  array               (Optional) An array of byte strings representing operating system identifiers that shall indicate to which operating systems this object applies. 
                *                                                  The defined values are the same as those defined for SMIL 2.0’s systemOperatingSystem attribute. 
                *                                                  There may not be multiple copies of the same identifier in the array. 
                *                                                  An empty array shall be considered to represent all operating systems. 
                *                                                  Default value: an empty array.
                */

                    /*13.2.7.4.2 Software URIs
                *
                *The U entry is a URI (universal resource identifier) that identifies a piece of software. 
                *It shall be interpreted according to its scheme; the only presently defined scheme is vnd.adobe.swname.
                *The scheme name is case-insensitive; if shall not be recognized by the conforming reader, the software shall be considered a non - match.
                *The syntax of URIs of this scheme is
                *
                *          “vnd.adobe.swname:” software_name
                *
                *where software_name shall be reg_name as defined in Internet RFC 2396, Uniform Resource Identifiers(URI): Generic Syntax; see the Bibliography.software_name shall be a sequence of UTF - 8 - encoded characters that have been escaped with one pass of URL escaping(see 14.10.3.2, “URL Strings”).
                *That is, to recover the original software name, software_name shall be unescaped and then treated as a sequence of UTF - 8 characters.
                *The actual software names shall be compared in a case-sensitive fashion.
                *
                *Software names shall be second -class names (see Annex E).
                *
                *EXAMPLE       The URI for Adobe Acrobat is
                *
                *              vnd.adobe.swname:ADBE_Acrobat
                */

                    /*13.2.7.4.3 Version arrays
                *
                *The L, H, LI, and HI entries shall be used to specify a range of software versions. 
                *L and H shall be version arrays containing zero or more non-negative integers representing subversion numbers. 
                *The first integer shall be the major version numbers, and subsequent integers shall be increasingly minor.
                *H shall be greater than or equal to L, according to the following rules for comparing version arrays:
                *
                *Algorithm: Comparing version arrays
                *
                *  a)  An empty version array shall be treated as infinity; that is, it shall be considered greater than any other version array except another empty array. 
                *      Two empty arrays are equal.
                *
                *  b)  When comparing arrays that contain different numbers of elements, the smaller array shall be implicitly padded with zero - valued integers to make the number of elements equal.
                *
                *EXAMPLE       When comparing[5 1 2 3 4] to[5], the latter is treated as [5 0 0 0 0].
                *
                *  c)  The corresponding elements of the arrays shall be compared, starting with the first. 
                *      When a difference is found, the array containing the larger element shall be considered to have the larger version number.
                *      If no differences are found, the versions are equal.
                *
                *If a version array contains negative numbers, it shall be considered non - viable, as is the enclosing software identifier.
                */

                    /*13.2.7.5 Monitor Specifier
                *
                *A monitor specifier is an integer that shall identify a physical monitor attached to a system. It may have one of the values in Table 293:
                *
                *Table 293 - Monitor specifier values
                *
                *          [Value]             [Description]
                *
                *          0                   The monitor containing the largest section of the document window
                *
                *          1                   The monitor containing the smallest section of the document window
                *
                *          2                   Primary monitor. If no monitor is considered primary, shall treat as case 0
                *
                *          3                   Monitor with the greatest colour depth
                *
                *          4                   Monitor with the greatest area (in pixels squared)
                *
                *          5                   Monitor with the greatest height (in pixels)
                *
                *          6                   Monitor with the greatest width (in pixels)
                *
                *
                *For some of these values, it is possible have a “tie” at play-time; for example, two monitors might have the same colour depth. 
                *Ties may be broken in an implementation-dependent manner.
                */

        }

        //13.3 Sounds
        public class Sounds
        {
            /*13.3 Sounds
            *A sound object (PDF 1.2) shall be a stream containing sample values that define a sound to be played through the computer’s speakers. 
            *The Sound entry in a sound annotation or sound action dictionary (see Table 185and Table 208) shall identify a sound object representing the sound to be played when the annotation is activated.
            *
            *Since a sound object is a stream, it may contain any of the standard entries common to all streams, as described in Table 5. 
            *In particular, if it contains an F(file specification) entry, the sound shall be defined in an external file.
            *This sound file shall be self-describing, containing all information needed to render the sound; no additional information need be present in the PDF file.
            *
            *NOTE      The AIFF, AIFF-C(Mac OS), RIFF(.wav), and snd(.au) file formats are all self-describing.
            *
            *If no F entry is present, the sound object itself shall contain the sample data and all other information needed to define the sound.Table 294 shows the additional dictionary entries specific to a sound object.
            *
            *Table 294 - Additional entries specific to a sound object
            *
            *              [Key]               [Type]                  [Value]
            *
            *              Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Sound for a sound object.
            *
            *              R                   number                  (Required) The sampling rate, in samples per second.
            *
            *              C                   integer                 (Optional) The number of sound channels. 
            *                                                          Default value: 1.
            *
            *              B                   integer                 (Optional) The number of bits per sample value per channel. 
            *                                                          Default value: 8.
            *
            *              E                   name                    (Optional) The encoding format for the sample data:
            *
            *                                                          Raw         Unspecified or unsigned values in the range 0 to 2B − 1
            *
            *                                                          Signed      Twos-complement values
            *
            *                                                          muLaw       m-law–encoded samples
            *
            *                                                          ALaw        A-law–encoded samples
            *
            *                                                          Default value: Raw.
            *
            *              CO                  name                    (Optional) The sound compression format used on the sample data. 
            *                                                          (This is separate from any stream compression specified by the sound object’s Filterentry; see Table 5 and 7.4, “Filters.”) 
            *                                                          If this entry is absent, sound compression shall not be used; the data contains sampled waveforms that shall be played at R samples per second per channel.
            *
            *              CP                  (various)               (Optional) Optional parameters specific to the sound compression format used.
            *                                                          No standard values have been defined for the CO and CP entries.
            *
            *
            *Sample values shall be stored in the stream with the most significant bits first (big-endian order for samples larger than 8 bits). 
            *Samples that are not a multiple of 8 bits shall be packed into consecutive bytes, starting at the most significant end. 
            *If a sample extends across a byte boundary, the most significant bits shall be placed in the first byte, followed by less significant bits in subsequent bytes. 
            *For dual-channel stereophonic sounds, the samples shall be stored in an interleaved format, with each sample value for the left channel (channel 1) preceding the corresponding sample for the right (channel 2).
            *
            *To maximize the portability of PDF documents containing embedded sounds, conforming readers shouldsupport at least the following formats(assuming the platform has sufficient hardware and OS support to play sounds at all) :
            *
            *          R       8000, 11,025, or 22,050 samples per second
            *
            *          C       1 or 2 channels
            *
            *          B       8 or 16 bits per channel
            *
            *          E       Raw, Signed, or muLaw encoding
            *
            *If the encoding(E) is Raw or Signed, R shall be 11,025 or 22,050 samples per channel.If the encoding is muLaw, R shall be 8000 samples per channel, C shall be 1 channel, and B shall be 8 bits per channel.
            *Sound players shall convert between formats, downsample rates, and combine channels as necessary to render sound on the target platform.
            */

        }

        //13.4 Movies
        public class Movies
        {
            
            /*13.4 Movies
            *The features described in this sub-clause are obsolescent and their use is no longer recommended. 
            *They are superseded by the general multimedia framework described in 13.2, “Multimedia.”
            *
            *PDF shall embed movies within a document by means of movie annotations(see 12.5.6.17, “Movie Annotations”). 
            *Despite the name, a movie may consist entirely of sound with no visible images to be displayed on the screen.
            *The Movie and A(activation) entries in the movie annotation dictionary shall refer, respectively, to a movie dictionary(Table 295) that shall describe the static characteristics of the movie and a movie activation dictionary(Table 296) that shall specify how it shall be presented.
            *
            *Table 295 - Entries in a movie dictionary
            *
            *          [Key]               [Type]                          [Value]
            *
            *          F                   file specification              (Required) A file specification identifying a self-describing movie file.
            *                                                              NOTE        The format of a self-describing movie file shall be left unspecified, and there is no guarantee of portability.
            *
            *          Aspect              array                           (Optional) The width and height of the movie’s bounding box, in pixels, and shall be specified as [width height]. 
            *                                                              This entry should be omitted for a movie consisting entirely of sound with no visible images.
            *
            *          Rotate              integer                         (Optional) The number of degrees by which the movie shall be rotated clockwise relative to the page. 
            *                                                              The value shall be a multiple of 90. 
            *                                                              Default value: 0.
            *
            *          Poster              boolean or stream               (Optional) A flag or stream specifying whether and how a poster imagerepresenting the movie shall be displayed. If this value is a stream, it shall contain an image XObject (see 8.9, “Images”) to be displayed as the poster. 
            *                                                              If it is the boolean value true, the poster image shall be retrieved from the movie file; if it is false, no poster shall be displayed. 
            *                                                              Default value: false.
            *
            *Table 296 - Entries in a movie activation dictionary
            *
            *          [Key]               [Type]                          [Value]
            *
            *          Start               (various)                       (Optional) The starting time of the movie segment to be played. 
            *                                                              Movie time values shall be expressed in units of time based on a time scale, which defines the number of units per second. 
            *                                                              The default time scale shall be defined in the movie data. 
            *                                                              The starting time shall be nominally a non-negative 64-bit integer, specified as follows:
            *
            *                                                              •   If it is representable as an integer(subject to the implementation limit for integers, as described in Annex C), it shall be specified as such.
            *
            *                                                              •   If it is not representable as an integer, it shall be specified as an 8-byte string representing a 64-bit twos-complement integer, most significant byte first.
            *
            *                                                              •   If it is expressed in a time scale different from that of the movie itself, it shall be represented as an array of two values: an integer or byte string denoting the starting time, followed by an integer specifying the time scale in units per second.
            *
            *                                                              If this entry is omitted, the movie shall be played from the beginning.
            *                                                             
            *          Duration            (various)                       (Optional) The duration of the movie segment to be played, that shall be specified in the same form as Start. 
            *                                                              If this entry is omitted, the movie shall be played to the end.
            *
            *          Rate                number                          (Optional) The initial speed at which to play the movie. 
            *                                                              If the value of this entry is negative, the movie shall be played backward with respect to Start and Duration. 
            *                                                              Default value: 1.0.
            *
            *          Volume              number                          (Optional) The initial sound volume at which to play the movie, in the range −1.0 to 1.0. 
            *                                                              Higher values shall denote greater volume; negative values shall mute the sound. 
            *                                                              Default value: 1.0.
            *
            *          ShowControls        boolean                         (Optional) A flag specifying whether to display a movie controller bar while playing the movie. 
            *                                                              Default value: false.
            *
            *          Mode                name                            (Optional) The play mode for playing the movie:
            *
            *                                                              Once        Play once and stop.
            *
            *                                                              Open        Play and leave the movie controller bar open.
            *
            *                                                              Repeat      Play repeatedly from beginning to end until stopped.
            *
            *                                                              Palindrome  Play continuously forward and backward until stopped.
            *
            *                                                              Default value: Once.
            *
            *          Synchronous         boolean                         (Optional) A flag specifying whether to play the movie synchronously or asynchronously. 
            *                                                              If this value is true, the movie player shall retain control until the movie is completed or dismissed by the user. 
            *                                                              If the value is false, the player shall return control to the conforming reader immediately after starting the movie. 
            *                                                              Default value: false.
            *
            *          FWScale             array                           (Optional) The magnification (zoom) factor at which the movie shall be played. 
            *                                                              The presence of this entry implies that the movie shall be played in a floating window. 
            *                                                              If the entry is absent, the movie shall be played in the annotation rectangle.
            *                                                              The value of the entry shall be an array of two positive integers, [numerator denominator], denoting a rational magnification factor for the movie.
            *                                                              The final window size, in pixels, shall be
            *
            *                                                              (numerator ÷ denominator) × Aspect
            *
            *                                                              where the value of Aspect shall be taken from the movie dictionary(see Table 295).
            *
            *          FWPosition          array                           (Optional) For floating play windows, the relative position of the window on the screen. 
            *                                                              The value shall be an array of two numbers
            *
            *                                                              [horiz vert]
            *
            *                                                              each in the range 0.0 to 1.0, denoting the relative horizontal and vertical position of the movie window with respect to the screen.
            *
            *                                                              EXAMPLE         The value[0.5 0.5] centers the window on the screen.
            *
            *                                                              Default value: [0.5 0.5].
            *
            */

        }

        //13.5 Alternate Presentations
        public class Alternate_Presentations
        {
            /*13.5 Alternate Presentations
            *Beginning with PDF 1.4, a PDF document shall contain alternate presentations, which specify alternate ways in which the document may be viewed. 
            *The optional AlternatePresentations entry (PDF 1.4) in a document’s name dictionary (see Table 31) contains a name tree that maps name strings to the alternate presentations available for the document.
            *
            *NOTE 1        Since conforming readers are not required to support alternate presentations, authors of documents containing alternate presentations should define the files such that something useful and meaningful can be displayed and printed.
            *              For example, if the document contains an alternate presentation slideshow of a sequence of photographs, the photographs should be viewable in a static form by viewers that are not capable of playing the slideshow.
            *
            *As of PDF 1.5, the only type of alternate presentation is a slideshow.Slideshows may be invoked by means of JavaScript actions (see 12.6.4.16, “JavaScript Actions”) initiated by user action on an interactive form element(see 12.7, “Interactive Forms”).
            *
            *The following table shows the entries in a slideshow dictionary.
            *
            *Table 297 - Entries in a slideshow dictionary
            *
            *              [Key]               [Type]                  [Value]
            *
            *              Type                name                    (Required; PDF 1.4) The type of PDF object that this dictionary describes; shall be SlideShow for a slideshow dictionary.
            *
            *              Subtype             name                    (Required; PDF 1.4) The subtype of the PDF object that this dictionary describes; shall be Embedded for a slideshow dictionary.
            *
            *              Resources           name tree               (Required; PDF 1.4) A name tree that maps name strings to objects referenced by the alternate presentation.
            *                                                          NOTE        Even though PDF treats the strings in the name tree as strings without a specified encoding, the slideshow shall interpret them as UTF-8 encoded Unicode.
            *
            *              StartResource       byte string             (Required; PDF 1.4) A byte string that shall match one of the strings in the Resources entry. 
            *                                                          It shall define the root object for the slideshow presentation.
            *
            *
            *NOTE 2        The Resources name tree represents a virtual file system to the slideshow. 
            *It associates strings (“file names”) with PDF objects that represent resources used by the slideshow. 
            *For example, a root stream may reference a file name, which would be looked up in the Resources name tree, and the corresponding object would be loaded as the file. 
            *(This virtual file system is flat; that is, there is no way to reference subfolders.)
            *
            *NOTE 3        Typically, images are stored in the document as image XObjects(see 8.9.5, “Image Dictionaries”), thereby allowing them to be shared between the standard PDF representation and the slideshow.
            *              Other media objects are stored or embedded file streams (see 7.11.4, “Embedded File Streams”).
            *
            *To allow conforming readers to verify content against their own supported features all referenced objects shall include a Type entry in their dictionary, even when the Type entry is normally optional for a given object.
            *
            *EXAMPLE       The following example illustrates the use of alternate presentation slideshows.
            *
            *              1 0 obj
            *                  <</Type /Catalog
            *                    /Pages 2 0 R
            *                    /Names 3 0 R % Indirect reference to name dictionary
            *                  >>
            *              ...
            *              3 0 obj % The name dictionary
            *                  <</AlternatePresentations 4 0 R >>
            *              endobj
            *              4 0 obj % The alternate presentations name tree
            *                  <</Names[(MySlideShow)5 0 R]>>
            *              endobj
            *              5 0 obj % The slideshow definition
            *                  <</Type /SlideShow
            *                    /Subtype /Embedded
            *                    /Resources <</Names[(mysvg.svg)31 0R
            *                      (abc0001.jpg) 35 0 R(abc0002.jpg) 36 0 R
            *                      (mysvg.js) 61 0 R(mymusic.mp3) 65 0 R]>>
            *                    /StartResource(mysvg.svg)
            *                  >>
            *              ...
            *              31 0 obj
            *                  <</Type /Filespec% The root object, which
            *                    /F(mysvg.svg)% points to an embedded file stream
            *                    /EF <</F 32 0 R>>
            *                  >>
            *              endobj
            *              32 0 obj% The embedded file stream
            *                  <</Type /EmbeddedFile
            *                    /Subtype /image#2Fsvg+xml
            *                    /Length 72
            *                  >>
            *                  stream
            *                      <?xml version="1.0" standalone="no"?>
            *                      <svg><!-- Some SVG goes here--></svg>
            *                  endstream
            *              endobj
            *              % ... other objects not shown
            */

        }

        //13.6 3D Artwork
        public class ThreeD_Artwork
        {
            /*13.6.1 General
            *
            *Starting with PDF 1.6, collections of three-dimensional objects, such as those used by CAD software, may be embedded in PDF files.
            *Such collections are often called 3D models; in the context of PDF, they shall bereferred to as 3D artwork.
            *The PDF constructs for 3D artwork support the following features:
            *
            *  •   3D artwork may be rendered within a page; that is, not as a separate window or user interface element.
            *
            *  •   Multiple instances of 3D artwork may appear within a page or document.
            *
            *  •   Specific views of 3D artwork may be specified, including a default view that shall be displayed initially and other views that may be selected.
            *      Views may have names that can be presented in a user interface.
            *
            *  •   (PDF 1.7) Conforming readers may specify how 3D artwork shall be rendered, coloured, lit, and cross-sectioned, without the use of embedded JavaScript.
            *      They may also specify state information that shall be applied to individual nodes (3D graphic objects or collections thereof) in the 3D artwork, such as visibility, opacity, position, or orientation.
            *
            *  •   Pages containing 3D artwork may be printed.
            *
            *  •   Users may rotate and move the artwork, enabling them to examine complex objects from any angle or orientation.
            *
            *  •   (PDF 1.7) Keyframe animations contained in 3D artwork may be played in specific styles and timescales, without programatic intervention.
            *
            *  •   JavaScripts and other software may programmatically manipulate objects in the artwork, creating dynamic presentations in which objects move, spin, appear, and disappear.
            *
            *  •   (PDF 1.7) The activation of 3D artwork can trigger the display of additional user interface items in the conforming reader. 
            *      Such items may include model trees and toolbars.
            *
            *  •   Two-dimensional(2D) content such as labels may be overlaid on 3D artwork.
            *      This feature is not the same as the ability to apply 2D markup annotations.
            *
            *  •   (PDF 1.7) 2D markup annotations may be applied to specific views of the 3D artwork, using the ExDataentry to identify the 3D annotation and the 3D view in that annotation.
            *
            *The following sub-clauses describe the major PDF objects that relate to 3D artwork, as well as providing background information on 3D graphics:
            *
            *  •   3D annotations provide a virtual camera through which the artwork shall be viewed. (see 13.6.2, “3D Annotations”).
            *
            *  •   3D streams shall contain the actual specification of a piece of 3D artwork (see 13.6.3, “3D Streams””). 
            *      This specification supports the Standard ECMA-363, Universal 3D file format developed by the 3D Industry Forum (see Bibliography).
            *
            *  •   3D views shall specify information about the relationship between the camera and the 3D artwork(see 13.6.4, “3D Views”). 
            *      Beginning with PDF 1.7, views may also describe additional parameters such as render mode, lighting, cross sections, and nodes.
            *      Nodes shall be 3D graphic objects or collections thereof.
            *
            *  •   3D coordinate systems are described in 13.6.5, “Coordinate Systems for 3D.”
            *
            *  •   2D markup annotations applied to 3D artwork views are described in 13.6.6, “3D Markup.”
            *
            *NOTE      Many of the concepts and terminology of 3D rendering are beyond the scope of this reference.
            *          Readers interested in further information are encouraged to consult outside references.
            */

            /*13.6.2 3D Annotations
            *
                *3D annotations(PDF 1.6) are the means by which 3D artwork shall be represented in a PDF document.Table 298 shows the entries specific to a 3D annotation dictionary.
                *Table 164 describes the entries common to all annotation dictionaries.
                *
                *In addition to these entries, a 3D annotation shall provide an appearance stream in its AP entry (see Table 164) that has a normal appearance(the N entry in Table 168). 
                *This appearance may be used by applications that do not support 3D annotations and by all applications for the initial display of the annotation.
                *
                *Table 298 - Additional entries specific to a 3D annotation
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Subtype             name                    (Required) The type of annotation that this dictionary describes; shall be 3D for a 3D annotation.
                *
                *          3DD                 stream or dictionary    (Required) A 3D stream (see 13.6.3, “3D Streams”) or 3D reference dictionary (see 13.6.3.3, “3D Reference Dictionaries”) that specifies the 3D artwork to be shown.
                *
                *          3DV                 (various)               (Optional) An object that specifies the default initial view of the 3D artwork that shall be used when the annotation is activated. 
                *                                                      It may be either a 3D view dictionary (see 13.6.4, “3D Views”) or one of the following types specifying an element in the VA array in the 3D stream (see Table 300):
                *
                *                                                      •   An integer specifying an index into the VA array.
                *
                *                                                      •   A text string matching the IN entry in one of the views in the VAarray.
                *
                *                                                      •   A name that indicates the first(F), last(L), or default(D) entries in the VA array.
                *
                *                                                      Default value: the default view in the 3D stream object specified by 3DD.
                *
                *          3DA                 dictionary              (Optional) An activation dictionary (see Table 299) that defines the times at which the annotation shall be activated and deactivated and the state of the 3D artwork instance at those times. 
                *                                                      Default value: an activation dictionary containing default values for all its entries.
                *
                *          3DI                 boolean                 (Optional) A flag indicating the primary use of the 3D annotation. 
                *                                                      If true, it is intended to be interactive; if false, it is intended to be manipulated programmatically, as with a JavaScript animation. 
                *                                                      Conforming readers may present different user interface controls for interactive 3D annotations (for example, to rotate, pan, or zoom the artwork) than for those managed by a script or other mechanism.
                *                                                      Default value: true.
                *
                *          3DB                 rectangle               (Optional) The 3D view box, which is the rectangular area in which the 3D artwork shall be drawn. 
                *                                                      It shall be within the rectangle specified by the annotation’s Rect entry and shall be expressed in the annotation’s target coordinate system (see discussion following this Table).
                *                                                      Default value: the annotation’s Rect entry, expressed in the target coordinate system. 
                *                                                      This value is [-w / 2 - h / 2 w / 2 h / 2], where w and h are the width and height, respectively, of Rect.
                *
                *
                *The 3DB entry specifies the 3D view box, a rectangle in which the 3D artwork appears. 
                *The view box shall fit within the annotation’s rectangle (specified by its Rect entry). 
                *It may be the same size, or it may be smaller if necessary to provide extra drawing area for additional 2D graphics within the annotation.
                *
                *NOTE 1        Although 3D artwork can internally specify viewport size, conforming readers ignore it in favour of information provided by the 3DB entry.
                *
                *The view box shall be specified in the annotation’s target coordinate system, whose origin is at the center of the annotation’s rectangle. 
                *Units in this coordinate system are the same as default user space units.Therefore, the coordinates of the annotation’s rectangle in the target coordinate system are
                *
                *              [-w / 2 - h / 2 w / 2 h / 2]
                *
                *given w and h as the rectangle’s width and height.
                *
                *The 3DD entry shall specify a 3D stream that contains the 3D artwork to be shown in the annotation; 3D streams are described in Section 13.6.3.
                *The 3DD entry may specify a 3D stream directly; it may also specify a 3D stream indirectly by means of a 3D reference dictionary(see 13.6.3.3, "3D Reference Dictionaries"). 
                *These options control whether annotations shall share the same run-time instance of the artwork.
                *
                *The 3DV entry shall specify the view of the 3D artwork that is displayed when the annotation is activated (as described in the next paragraph). 
                *3D views, which are described in Section 13.6.4, represent settings for the virtual camera, such as position, orientation, and projection style.
                *The view specified by 3DV shall be one of the 3D view dictionaries listed in the VA entry in a 3D stream(see Table 300).
                *
                *The 3DA entry shall be an activation dictionary(see Table 299) that determines how the state of the annotation and its associated artwork may change.
                *
                *NOTE 2            These states serve to delay the processing or display of 3D artwork until a user chooses to interact with it. 
                *                  Such delays in activating 3D artwork can be advantageous to performance.
                *
                *At any given moment, a 3D annotation shall be in one of two states:
                *
                *  •   Inactive(the default initial state): the annotation displays the annotation’s normal appearance.
                *
                *NOTE 3            It is typical, though not required, for the normal appearance to be a pre - rendered bitmap of the default view of the 3D artwork.Conforming writers should provide bitmaps of appropriate resolution for all intended uses of the document; for example, a high - resolution bitmap for high - quality printing and a screen - resolution bitmap for on - screen viewing.
                *                  Optional content(see 8.11, “Optional Content”) may be used to select the appropriate bitmap for each situation.
                *
                *  •   Active: the annotation displays a rendering of the 3D artwork.This rendering shall be specified by the annotation’s 3DV entry.
                *
                *Table 299 - Entries in a 3D activation dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          A                   name                (Optional) A name specifying the circumstances under which the annotation shall be activated. Valid values are:
                *
                *                                                  PO          The annotation shall be activated as soon as the page containing the annotation is opened.
                *
                *                                                  PV          The annotation shall be activated as soon as any part of the page containing the annotation becomes visible.
                *
                *                                                  XA          The annotation shall remain inactive until explicitly activated by a script or user action.
                *
                *                                                  NOTE 1    At any one time, only a single page shall be considered open in a conforming reader, even though more than one page may be visible, depending on the page layout.
                *
                *                                                  Default value: XA.
                *
                *                                                  NOTE 2      For performance reasons, documents intended for viewing in a web browser should use explicit activation(XA). 
                *                                                              In non-interactive applications, such as printing systems or aggregating conforming reader, PO and PV indicate that the annotation shall be activated when the page is printed or placed; XA indicates that the annotation shall never be activated and the normal appearance shall be used.
                *
                *          AIS                 name                (Optional) A name specifying the state of the artwork instance upon activation of the annotation. 
                *                                                  Valid values are:
                *
                *                                                  I           The artwork shall be instantiated, but real-time script - driven animations shall be disabled.
                *
                *                                                  L           Real-time script - driven animations shall be enabled if present; if not, the artwork shall be instantiated.
                *
                *                                                  Default value: L.
                *
                *                                                  NOTE 3       In non-interactive conforming readers, the artwork shall be instantiated and scripts shall be disabled.
                *
                *          D                   name                (Optional) A name specifying the circumstances under which the annotation shall be deactivated. 
                *                                                  Valid values are:
                *
                *                                                  PC          The annotation shall be deactivated as soon as the page is closed.
                *
                *                                                  PI          The annotation shall be deactivated as soon as the page containing the annotation becomes invisible.
                *
                *                                                  XD          The annotation shall remain active until explicitly deactivated by a script or user action.
                *
                *                                                  NOTE 4      At any one time, only a single page shall be considered open in the conforming reader, even though more than one page may be visible, depending on the page layout.
                *
                *                                                  Default value: PI.
                *
                *          DIS                 name                (Optional) A name specifying the state of the artwork instance upon deactivation of the annotation. 
                *                                                  Valid values are U (uninstantiated), I(instantiated), and L (live). 
                *                                                  Default value: U.
                *                                                  NOTE 5      If the value of this entry is L, uninstantiation of instantiated artwork is necessary unless it has been modified. Uninstantiation is never required in non - interactive conforming readers.
                *
                *          TB                  boolean             (Optional; PDF 1.7) A flag indicating the default behavior of an interactive toolbar associated with this annotation. 
                *
                *                                                  If true, a toolbar shall be displayed by default when the annotation is activated and given focus. 
                *                                                  If false, a toolbar shall not be displayed by default.
                *
                *                                                  NOTE 6      Typically, a toolbar is positioned in proximity to the 3D annotation.
                *
                *                                                  Default value: true.
                *
                *          NP                  boolean             (Optional; PDF 1.7) A flag indicating the default behavior of the user interface for viewing or managing information about the 3D artwork. 
                *                                                  Such user interfaces can enable navigation to different views or can depict the hierarchy of the objects in the artwork (the model tree). 
                *                                                  If true, the user interface should be made visible when the annotation is activated. 
                *                                                  If false, the user interface should not be made visible by default.
                *                                                  Default value: false
                *
                *The A and D entries of the activation dictionary determine when a 3D annotation may become active and inactive. 
                *The AIS and DIS entries determine what state the associated artwork shall be in when the annotation is activated or deactivated. 
                *3D artwork may be in one of three states:
                *
                *  •   Uninstantiated: the initial state of the artwork before it has been used in any way.
                *
                *  •   Instantiated: the state in which the artwork has been read and a run - time instance of the artwork has been created.In this state, it may be rendered but script-driven real - time modifications(that is, animations) shall be disabled.
                *
                *  •   Live: the artwork has been instantiated, and it is being modified in real time to achieve some animation effect.
                *      In the case of keyframe animation, the artwork shall be live while it is playing and then shall revert to an instantiated state when playing completes or is stopped.
                *
                *NOTE 4        The live state is valid only for keyframe animations or in interactive conforming readers that have JavaScript support.
                *
                *If 3D artwork becomes uninstantiated after having been instantiated, later use of the artwork requires re - instantiation(animations are lost, and the artwork appears in its initial form).
                *
                *NOTE 5        For this reason, uninstantiation is not necessary unless the artwork has been modified in some way; consumers may choose to keep unchanged artwork instantiated for performance reasons.
                *
                *NOTE 6        In non - interactive systems such as printing systems, the artwork cannot be changed.
                *              Therefore, applications may choose to deactivate annotations and uninstantiate artwork differently, based on factors such as memory usage and the time needed to instantiate artwork, and the TB, NP, D and DIS entries may be ignored.
                *
                *Multiple 3D annotations may share an instance of 3D artwork, as described in 13.6.3.3, "3D Reference Dictionaries".
                *In such a case, the state of the artwork instance shall be determined in the following way:
                *
                *  •   If any active annotation dictates (through its activation dictionary) that the artwork shall be live, it shall belive.
                *
                *  •   Otherwise, if any active annotation dictates that the artwork shall be instantiated, it shall be instantiated.
                *
                *  •   Otherwise(that is, all active annotations dictate that the artwork shall be uninstantiated), the artwork shall be uninstantiated.
                *
                *The rules described in 13.6.2, “3D Annotations”, apply only to active annotations. 
                *If all annotations referring to the artwork are inactive, the artwork nevertheless may be uninstantiated, instantiated, or live 3D Streams.
                */

            /*13.6.3 3D Streams
            */

                /*13.6.3.1 General
                *
                *The specification of 3D artwork shall be contained in a 3D stream. 
                *3D stream dictionaries, whose entries(in addition to the regular stream dictionary's entries; see 7.3.7, “Dictionary Objects”) are shown in Table 300, mayprovide a set of predefined views of the artwork, as well as a default view. 
                *They may also provide scripts and resources for providing customized behaviours or presentations.
                *
                *Table 300 - Entries in a 3D stream dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be 3D for a 3D stream.
                *
                *          Subtype             name                (Required) A name specifying the format of the 3D data contained in the stream. The only valid value is U3D.
                *
                *          VA                  array               (Optional) An array of 3D view dictionaries, each of which specifies a named preset view of this 3D artwork (see Section 13.6.4, “3D Views”).
                *
                *          DV                  (various)           (Optional) An object that specifies the default (initial) view of the 3D artwork. 
                *                                                  It may be a 3D view dictionary (see Section 13.6.4, “3D Views”) or one of the following types:
                *
                *                                                  •   An integer specifying an index into the VA array.
                *
                *                                                  •   A text string matching the IN entry in one of the views in the VAarray.
                *
                *                                                  •   A name that indicates the first(F) or last(L) entries in the VAarray.
                *
                *                                                  Default value: 0(the first entry in the VA array) if VA is present; if VA is not present, the default view shall be specified within the 3D stream itself.
                *
                *          Resources           name tree           (Optional) A name tree that maps name strings to objects that may be used by applications or scripts to modify the default view of the 3D artwork.
                *
                *                                                  The names in this name tree shall be text strings so as to be encoded in a way that will be accessible from JavaScript.
                *
                *          OnInstantiate       stream              (Optional) A JavaScript script that shall be executed when the 3D stream is instantiated.
                *
                *          AN                  dictionary          (Optional; PDF 1.7) An animation style dictionary indicating the method that conforming readers should use to drive keyframe animations present in this artwork (see 13.6.3.2, "3D Animation Style Dictionaries").
                *                                                  Default value: an animation style dictionary whose Subtype entry has a value of None.
                *
                *The Subtype entry specifies the format of the 3D stream data. 
                *The only valid value is U3D, which indicates that the stream data conforms to the Universal 3D File Format specification (see Bibliography). 
                *Conforming readersshall be prepared to encounter unknown values for Subtype and recover appropriately, which usually means leaving the annotation in its inactive state, displaying its normal appearance.
                *
                *NOTE      Conforming readers should follow the approach of falling back to the normal appearance with regard to entries in other dictionaries that may take different types or values than the ones specified here.
                *
                *If present, the VA entry shall be an array containing a list of named present views of the 3D artwork. 
                *Each entry in the array shall be a 3D view dictionary (see 13.6.4, “3D Views”) that shall contain the name of the view and the information needed to display the view. 
                *The order of array elements determines the order in which the views shall be presented in a user interface. 
                *The DV entry specifies the view that shall be used as the initial view of the 3D artwork.
                *
                *Default views shall be determined in the following order of precedence: in the annotation dictionary, in the 3D stream dictionary, or in the 3D artwork contained in the 3D stream.
                *
                *3D streams contain information that may be used by conforming readers and by scripts to perform animations and other programmatically - defined behaviours, such as changing the viewing orientation or moving individual components of the artwork. 
                *If present, the OnInstantiate entry shall contain a JavaScript script that shall beexecuted by applications that support JavaScript whenever a 3D stream is read to create an instance of the 3D artwork.
                *The Resources entry shall be a name tree that contains objects that may be used to modify the initial appearance of the 3D artwork.
                */
                
                /*13.6.3.2 3D Animation Style Dictionaries
                *
                *A 3D animation style dictionary(PDF 1.7) specifies the method that conforming readers should use to apply timeline scaling to keyframe animations.
                *It may also specify that keyframe animations be played repeatedly. 
                *The AN entry of the 3D stream shall specify a 3D animation style dictionary.
                *
                *A keyframe animation may be provided as the content of a 3D stream dictionary. 
                *A keyframe animation provides key frames and specifies the mapping for the position of geometry over a set period of time(animation timeline).
                *Keyframe animation is an interactive feature that is highly dependent on the behaviour and controls provided by the conforming reader.
                *
                *Table 301 shows the entries in an animation style dictionary.
                *
                *Table 301 - Entries in an 3D animation style dictionary
                *
                *              [Key]               [Type]                  [Value]
                *
                *              Type                name                    (Optional). The type of PDF object that this dictionary describes; if present, shall be 3DAnimationStyle.
                *
                *              Subtype             name                    (Optional) The animation style described by this dictionary; see Table 302for valid values. 
                *                                                          If an animation style is encountered other than those described in Table 302, an animation style of None shall be used.
                *                                                          Default value: None
                *
                *              PC                  integer                 (Optional) An integer specifying the play count for this animation style. 
                *                                                          A non-negative integer represents the number of times the animation shall be played. 
                *                                                          A negative integer indicates that the animation shall be infinitely repeated. 
                *                                                          This value shall be ignored for animation styles of type None.
                *                                                          Default value: 0
                *
                *              TM                  number                  (Optional) A positive number specifying the time multiplier to be used when running the animation. 
                *                                                          A value greater than one shortens the time it takes to play the animation, or effectively speeds up the animation.
                *                                                          
                *                                                          NOTE    This allows authors to adjust the desired speed of animations, without having to re - author the 3D artwork.
                *
                *                                                          This value shall be ignored for animation styles of type None.
                *
                *                                                          Default value: 1
                *
                *The descriptions of the animation styles (see Table 302) use the following variables to represent application time or keyframe settings specified in the 3D artwork.
                *
                *  •   t is a point on the animation time line. This value shall be used in conjunction with the keyframe animation data to determine the state of the 3D artwork.
                *
                *  •   [r0, r1] is the keyframe animation time line.
                *
                *  •   ta is the current time of the conforming reader.
                *
                *  •   t0 is the time when the conforming reader starts the animation.
                *
                *  •   p is the time it takes to play the keyframe animation through one cycle.
                *      In the case of the Linear animation style, one cycle consists of playing the animation through once from beginning to end.
                *      In the case of the Oscillating animation style, one cycle consists of playing the animation from beginning to end and then from end to beginning.
                *
                *  •   m is the positive multiplier specified by the TM entry in the animation style dictionary.
                *
                *Table 302 - Animation styles
                *
                *          [None]              Keyframe animations shall not be driven directly by the conforming reader. 
                *                              This value shall be used by documents that are intended to drive animations through an alternate means, such as JavaScript.
                *                              The remaining entries in the animation style dictionary shall be ignored.
                *
                *          [Linear]            Keyframe animations shall be driven linearly from beginning to end. 
                *                              This animation style results in a repetitive playthrough of the animation, such as in a walking motion.
                *
                *                              t = (m(ta - t0) + r0) % (r1 - r0)
                *
                *                              p = (r1 - r0) / m
                *
                *                              The “%” symbol indicates the modulus operator.
                *
                *          [Oscillating]       Keyframe animations shall oscillate along their time range. 
                *                              This animation style results in a back-and-forth playing of the animation, such as exploding or collapsing parts.
                *
                *                              t = (0.5)(r1 - r0)(1 - cos(m(ta - t0))) + r0
                *
                *                              p = 2 * pi / m
                */
                
               /*13.6.3.3 3D Reference Dictionaries
                *
                *More than one 3D annotation may be associated with the same 3D artwork.
                *There are two ways in which this association may occur, as determined by the annotation’s 3DD entry(see Table 298):
                *
                *  •   If the 3DD entry specifies a 3D stream, the annotation shall have its own run - time instance of the 3D artwork.
                *      Any changes to the artwork shall be reflected only in this annotation.Other annotations that refer to the same stream shall have separate run-time instances.
                *
                *  •   If the 3DD entry specifies a 3D reference dictionary(whose entries are shown in Table 303), the annotation shall have a run-time instance of the 3D artwork with all other annotations that specify the same reference dictionary.Any changes to the artwork shall be reflected in all such annotations.
                *
                *Table 303 - Entries in a 3D reference dictionary
                *
                *              [Key]               [Type]              [Value]
                *
                *              Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be 3DRef for a 3D reference dictionary.
                *
                *              3DD                 stream              (Required) The 3D stream (see 13.6.3, “3D Streams”) containing the specification of the 3D artwork.
                *
                *EXAMPLE       The following example and Figure 66 through Figure 68 show three annotations that use the same 3D artwork. 
                *              Object 100 (Annotation 1) has its own run-time instance of the 3D stream (object 200); object 101(Annotation 2) and object 102 (Annotation 3) share a run-time instance through the 3D reference dictionary (object 201).
                *
                *              100 0 obj % 3D annotation 1
                *                  << / Type / Annot
                *                     / Subtype / 3D
                *                     / 3DD 200 0 R % Reference to the 3D stream containing the 3D artwork
                *                  >>
                *              endobj
                *              101 0 obj % 3D annotation 2
                *                  << / Type / Annot
                *                     / Subtype / 3D
                *                     / 3DD 201 0 R % Reference to a 3D reference dictionary
                *                  >>
                *              endobj
                *              102 0 obj % 3D annotation 3
                *                  << / Type / Annot
                *                     / Subtype / 3D
                *                     / 3DD 201 0 R % Reference to the same 3D reference dictionary
                *                  >>
                *              endobj
                *              200 0 obj % The 3D stream
                *                  << / Type / 3D
                *                     / Subtype / U3D
                *                     ...other keys related to a stream, such as / Length
                *                  >>
                *                  stream
                *                      ...U3D data...
                *                  endstream
                *              endobj
                *              201 0 obj % 3D reference dictionary
                *                  << / Type / 3DRef
                *                     / 3DD 200 0 R % Reference to the actual 3D artwork.
                *                  >>
                *              endobj
                *
                *(see Figure 66 - Default view of artwork, on page 520)
                *(see Figure 67 - Annotation 2 rotated, on page 520)
                *(see Figure 68 - Shared artwork (annotations 2 & 3) modified, on page 520)
                *
                *The figures show how the objects in the Example in 13.5, “Alternate Presentations,” might be used. 
                *Figure 66 shows the same initial view of the artwork in all three annotations. Figure 67 shows the results of rotating the view of the artwork within Annotation 2. 
                *Figure 68 shows the results of manipulating the artwork shared by Annotation 2 and Annotation 3: they both reflect the change in the artwork because they share the same run-time instance. 
                *Annotation 1 remains unchanged because it has its own run-time instance.
                *
                *NOTE      When multiple annotations refer to the same instance of 3D artwork, the state of the instance is determined as described in 13.6.2, “3D Annotations.”
                *
                */

            /*13.6.4 3D Views
            */

                /*13.6.4.1 General
                *
                *A 3D view(or simply view) specifies parameters that shall be applied to the virtual camera associated with a 3D annotation.
                *These parameters may include orientation and position of the camera, details regarding the projection of camera coordinates into the annotation’s target coordinate system, and a description of the background on which the artwork shall be drawn.
                *Starting with PDF 1.7, views may specify how 3D artwork is rendered, coloured, lit, and cross-sectioned, without the use of embedded JavaScript. 
                *Views may also specify which nodes (three-dimensional areas) of 3D artwork shall be included in a view and whether those nodes are opaque or invisible.
                *
                *NOTE 1        Users can manipulate views by performing interactive operations such as free rotation and translation. 
                *              In addition, 3D artwork can contain a set of predefined views that the author deems to be of particular interest.
                *              For example, a mechanical drawing of a part may have specific views showing the top, bottom, left, right, front, and back of an object.
                *
                *A 3D stream may contain a list of named preset views of the 3D artwork, as specified by the VA entry, which shall be an array of 3D view dictionaries. 
                *The entries in a 3D view dictionary are shown in Table 304.
                *
                *Table 304 - Entries in a 3D view dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be 3DView for a 3D view dictionary.
                *
                *          XN                  text string         (Required) The external name of the view, suitable for presentation in a user interface.
                *
                *          IN                  text string         (Optional) The internal name of the view, used to refer to the view from other objects, such as the go-to-3D-view action (see 12.6.4.15, “Go-To-3D-View Actions”).
                *
                *          MS                  name                (Optional) A name specifying how the 3D camera-to-world transformation matrix shall be determined. 
                *                                                  The following values are valid:
                *
                *                                                  M           Indicates that the C2W entry shall specify the matrix
                *
                *                                                  U3D         Indicates that the view node selected by the U3DPath entry shall specify the matrix.
                *
                *                                                  If omitted, the view specified in the 3D artwork shall be used.
                *
                *          C2W                 array               (Required if the value of MS is M, ignored otherwise) A 12-element 3D transformation matrix that specifies a position and orientation of the camera in world coordinates.
                *
                *          U3DPath             text string         (Required if the value of MS is U3D, ignored otherwise) A sequence of one or more text strings used to access a view node within the 3D artwork. The first string in the array is a node ID for the root view node, and each subsequent string is the node ID for a child of the view node specified by the prior string. Each view node specifies a 3D transformation matrix (see 13.6.5, “Coordinate Systems for 3D”); the concatenation of all the matrices forms the camera-to-world matrix.
                *                              or arary            Conforming writers should specify only a single text string, not an array, for this entry.
                *                                                  NOTE        Do not confuse View Nodes with nodes.A View Node is a parameter in the 3D artwork that specifies a view, while a node is a PDF dictionary that specifies 3D graphic objects or collections thereof.
                *
                *          CO                  number              (Optional; used only if MS is present) A non-negative number indicating a distance in the camera coordinate system along the z axis to the center of orbit for this view; see discussion following this Table. 
                *                                                  If this entry is not present, the conforming reader shall determine the center of orbit.    
                *
                *          P                   dictionary          (Optional) A projection dictionary (see 13.6.4.2, “Projection Dictionaries”) that defines the projection of coordinates in the 3D artwork (already transformed into camera coordinates) onto the target coordinate system of the annotation.
                *
                *                                                  Default value: a projection dictionary where the value of Subtype is Perspective, the value of FOV is 90, and all other entries take their default values.
                *
                *          O                   stream              (Optional; meaningful only if MS and P are present) A form XObject that shall be used to overlay 2D graphics on top of the rendered 3D artwork (see 13.6.6, “3D Markup”).
                *
                *          BG                  dictionary          (Optional) A background dictionary that defines the background over which the 3D artwork shall be drawn (see 13.6.4.3, “3D Background Dictionaries”). 
                *                                                  Default value: a background dictionary whose entries take their default values.
                *
                *          RM                  dictionary          (Optional; PDF 1.7) A render mode dictionary that specifies the render mode to use when rendering 3D artwork with this view (see 13.6.4.4, “3D Render Mode Dictionaries”). 
                *                                                  If omitted, the render mode specified in the 3D artwork shall be used.
                *
                *          LS                  dictionary          (Optional; PDF 1.7) A lighting scheme dictionary that specifies the lighting scheme to be used when rendering 3D artwork with this view (see 13.6.4.5, “3D Lighting Scheme Dictionaries”). 
                *                                                  If omitted, the lighting scheme specified in the 3D artwork shall be used.
                *
                *          SA                  array               (Optional; PDF 1.7) An array that contains cross section dictionaries (see 13.6.4.6, “3D Cross Section Dictionaries”). 
                *                                                  Each cross section dictionary provides parameters for applying a cross section to the 3D artwork when using this view. 
                *                                                  An empty array signifies that no cross sections shall be displayed.
                *
                *          NA                  array               (Optional; PDF 1.7; meaningful only if NR is present) An array that contains 3D node dictionaries (see 13.6.4.7, “3D Node Dictionaries”). 
                *                                                  Each node dictionary may contain entries that change the node’s state, including its opacity and its position in world space. 
                *                                                  This entry and the NR entry specify how the state of each node shall be changed.
                *                                                  If a node dictionary is present more than once, only the last such dictionary(using a depth-first traversal) shall be used.
                *
                *          NR                  boolean             (Optional; PDF 1.7) Specifies whether nodes specified in the NA array shall be returned to their original states (as specified in the 3D artwork) before applying transformation matrices and opacity settings specified in the node dictionaries. 
                *                                                  If true, the artwork’s 3D node parameters shall be restored to their original states and then the dictionaries specified by the NA array shall be applied. 
                *                                                  If false, the dictionaries specified by the NAarray shall be applied to the current states of the nodes.
                *                                                  In addition to the parameters specified by a 3D node dictionary, this flag should also apply to any runtime parameters used by a conforming reader.
                *                                                  Default value: false
                *
                *For any view, the conforming writer may provide 2D content specific to the view, to be drawn on top of the 3D artwork. 
                *The O entry specifies a form XObject that shall be overlaid on the rendered 3D artwork. 
                *The coordinate system of the form XObject shall be defined to be the same as the (x, y, 0) plane in the camera coordinate system (see 13.6.5, “Coordinate Systems for 3D”).
                *
                *Use of the O entry is subject to the following restrictions.
                *
                *NOTE 2        Failure to abide by them could result in misalignment of the overlay with the rendered 3D graphics:
                *
                *  •   It may be specified only in 3D view dictionaries in which both a camera-to - world matrix(MSand associated entries) and a projection dictionary(the P entry) are present.
                *
                *  •   The form XObject shall be associated with a specific view(not with the camera position defined by the 3D view dictionary). 
                *      The conforming reader should draw it only when the user navigates using the 3D view, not when the user happens to navigate to the same orientation by manual means.
                *
                *  •   The confirming reader should draw it only if the user has not invoked any actions that alter the artwork-to - world matrix.
                *
                *The CO entry specifies the distance from the camera to the center of orbit for the 3D view, which is the point around which the camera shall rotate when performing an orbit - style navigation.
                *Figure 69 illustrates camera positioning when orbiting around the center of orbit.
                *
                *(see Figure 69 - Rotation around the center of orbit, on page 523)
                *
                *NOTE 3        The LS entry allows the lighting of the 3D artwork to be changed without changing the artwork itself. 
                *              This enables consumers to view a given piece of 3D artwork with a variety of lighting options without requiring multiple copies of the 3D artwork stream that differ only in lighting. 
                *              It also enables artwork with poor lighting to be corrected in cases where the original content cannot be re-authored. 
                *              See 13.6.4.5, “3D Lighting Scheme Dictionaries.”
                *
                *              The SA entry provides cross section information for clipping 3D artwork while its associated view is active.
                *              This allows view authors to be more clear in calling out the intended areas of interest for a particular view, some of which might otherwise be completely obscured.
                *              See 13.6.4.6, “3D Cross Section Dictionaries.”
                *
                *              The NR and NA entries are meant to give a more accurate representation of the 3D artwork at a given state.
                *              These keys give view authors finer granularity in manipulating the artwork to be presented in a particular way.
                *              They also provide a means for returning node parameters to a known state after potential changes by interactive features such as keyframe animations and JavaScript.See 13.6.4.7, “3D Node Dictionaries.”
                */

                /*13.6.4.2 Projection Dictionaries
                *
                *A projection dictionary(see Table 305) defines the mapping of 3D camera coordinates onto the target coordinate system of the annotation. 
                *Each 3D view may specify a projection dictionary by means of its P entry.
                *
                *NOTE      Although view nodes can specify projection information, PDF consumers ignore it in favour of information in the projection dictionary.
                *
                *PDF 1.6 introduces near/far clipping. This type of clipping defines a near plane and a far plane (as shown in Figure 70). 
                *Objects, or parts of objects, that are beyond the far plane or closer to the camera than the near plane are not drawn. 
                *3D objects shall be projected onto the near plane and then scaled and positioned within the annotation’s target coordinate system, as described Table 305.
                *
                *Table 305 - Entries in a projection dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Subtype             name                (Required) The type of projection. Valid values shall be O (orthographic) or P(perspective).
                *
                *          CS                  name                (Optional) The clipping style. Valid values shall be XNF (explicit near/far) or ANF(automatic near/far). 
                *                                                  Default value: ANF
                *
                *          F                   number              (Optional; meaningful only if the value of CS is XNF) The far clipping distance, expressed in the camera coordinate system. 
                *                                                  No parts of objects whose zcoordinates are greater than the value of this entry are drawn. 
                *                                                  If this entry is absent, no far clipping occurs.
                *
                *          N                   number              (Meaningful only if the value of CS is XNF; required if the value of Subtype is P) 
                *                                                  The near clipping distance, expressed in the camera coordinate system. No parts of objects whose z coordinates are less than the value of this entry are drawn. 
                *                                                  If Subtype is P, the value shall be positive; if Subtype is O, the value shall be non-negative, and the default value is 0.
                *
                *          FOV                 number              (Required if Subtype is P, ignored otherwise) A number between 0 and 180, inclusive, specifying the field of view of the virtual camera, in degrees. 
                *                                                  It defines a cone in 3D space centered around the z axis and a circle where the cone intersects the near clipping plane. 
                *                                                  The circle, along with the value of PS, specify the scaling of the projected artwork when rendered in the 2D plane of the annotation.
                *
                *          PS                  number              (Optional; meaningful only if Subtype is P) An object that specifies the scaling used when projecting the 3D artwork onto the annotation’s target coordinate system. 
                *                              or name             It defines the diameter of the circle formed by the intersection of the near plane and the cone specified by FOV. 
                *                                                  The value may be one of the following:
                *
                *                                                  •   A positive number that explicitly specifies the diameter as a distance in the annotation’s target coordinate system.
                *
                *                                                  •   A name specifying that the diameter shall be set to the width(W), height(H), minimum of width and height(Min), or maximum of width and height(Max) of the annotation’s 3D view box.
                *
                *                                                  Default value: W.
                *
                *          OS                  number              (Optional; meaningful only if Subtype is O) A positive number that specifies the scale factor to be applied to both the x and y coordinates when projecting onto the annotation’s target coordinate system (the z coordinate is discarded). 
                *                                                  Default value: 1.    
                *
                *          OB                  name                (Optional; PDF 1.7; meaningful only if Subtype is O) A name that specifies a strategy for binding (scaling to fit) the near plane’s x and y coordinates onto the annotation’s target coordinate system. 
                *                                                  The scaling specified in this entry shall be applied in addition to the scaling factor specified by the OS entry. 
                *                                                  The value may be one of the following:
                *
                *                                                  W               Scale to fit the width of the annotation
                *
                *                                                  H               Scale to fit the height of the annotation
                *
                *                                                  Min             Scale to fit the lesser of width or height of the annotation
                *
                *                                                  Max             Scale to fit the greater of width or height of the annotation
                *
                *                                                  Absolute        No scaling should occur due to binding.
                *
                *                                                  Default value: Absolute.
                *
                *
                *The CS entry defines how the near and far planes are determined. 
                *A value of XNF means that the N and Fentries explicitly specify the z coordinate of the near and far planes, respectively. 
                *A value of ANF for CS means that the near and far planes shall be determined automatically based on the objects in the artwork.
                *
                *The Subtype entry specifies the type of projection, which determines how objects are projected onto the near plane and scaled.
                *The possible values are O for orthographic projection and P for perspective projection.
                *
                *For orthographic projection, objects shall be projected onto the near plane by simply discarding their z value.
                *They shall be scaled from units of the near plane’s coordinate system to those of the annotation’s target coordinate system by the combined factors specified by the OS entry and the OB entry.
                *
                *For perspective projection, a given coordinate(x, y, z) shall be projected onto the near plane, defining a 2D coordinate(x1, y1) using the following formulas:
                *
                *          X1 = X x n/2
                *
                *          Y1 = Y x n/2
                *
                *where n is the z coordinate of the near plane.
                *
                *Scaling with perspective projection is more complicated than for orthographic projection. 
                *The FOV entry specifies an angle that defines a cone centered along the z axis in the camera coordinate system(see Figure 70).
                *The cone intersects with the near plane, forming a circular area on the near plane.Figure 71 shows this circle and graphics from the position of the camera.
                *
                *(see Figure 70 - Perspective projection of 3D artwork onto the near plane, on page 525)
                *
                *(see Figure 71 - Objects projected onto the near clipping plane, as seen from the position of the camera, on page 525)
                *
                *The PS entry specifies the diameter that this circle will have when the graphics projected onto the near plane are rendered in the annotation’s 3D view box (see Figure 72). 
                *Although the diameter of the circle determines the scaling factor, graphics outside the circle shall also be displayed, providing they fit within the view box, as seen in the figure.
                *
                *Figure 73 shows the entire 3D annotation.
                *In this case, the 3D view box is smaller than the annotation’s rectangle, which also contains 2D content outside the 3D view box.
                *
                *(see Figure 72 - Positioning and scaling the near plane onto the annotation's 3D view box, on page 526)
                *
                *(see Figure 73 - 3D annotation positioned on the page, on page 526)
                */
                
                /*13.6.4.3 3D Background Dictionaries
                *
                *A 3D background dictionary defines the background over which a 3D view shall be drawn.
                *Table 306 shows the entries in a background dictionary.
                *Currently, only a single opaque colour is supported, where the colour shallbe defined in the DeviceRGB colour space. 
                *3D artwork may include transparent objects; however, there is no interaction between such objects and objects drawn below the annotation.
                *In effect, the 3D artwork and its background form a transparency group whose flattened results have an opacity of 1(see 11, “Transparency”).
                *
                *NOTE      An annotation’s normal appearance should have the same behaviour with respect to transparency when the appearance is intended to depict the 3D artwork.
                *          This does not apply when the appearance is used for another purpose, such as a compatibility warning message.
                *
                *Table 306 - Entries in a 3D background dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be 3DBG for a 3D background dictionary.
                *
                *          Subtype             name                (Optional) The type of background. The only valid value shall be SC (solid colour), which indicates a single opaque colour. 
                *                                                  Default value: SC.
                *
                *          CS                  name or array       (Optional) The colour space of the background. The only valid value shall be the name DeviceRGB. \
                *                                                  Default value: DeviceRGB.
                *                                                  PDF consumers shall be prepared to encounter other values that may be supported in future versions of PDF.
                *
                *          C                   (various)           (Optional) The colour of the background, in the colour space defined by CS. 
                *                                                  Default value: an array [1 1 1] representing the colour white when the value of CS is DeviceRGB.
                *
                *          EA                  boolean             (Optional) If true, the background shall apply to the entire annotation; if false, the background shall apply only to the rectangle specified by the annotation’s 3D view box (the 3DB entry in Table 298). 
                *                                                  Default value: false.
                */
                
                /*13.6.4.4 3D Render Mode Dictionaries
                *
                *A 3D render mode dictionary (PDF 1.7) specifies the style in which the 3D artwork shall be rendered.
                *
                *NOTE 1        Surfaces may be filled with opaque colours, they may be stroked as a “wireframe,” or the artwork may be rendered with special lighting effects.
                *
                *NOTE 2        A render mode dictionary enables document authors to customize the rendered appearance of 3D artwork to suit the needs of the intended consumer, without reauthoring the artwork. 
                *              For conforming readers concerned strictly with geometry, complex artwork rendered using the Wireframe or Points style may have much better performance without the added overhead of texturing and lighting effects.
                *              Artwork in a document intended for print may have a much more integrated feel when using the Illustration render mode style.
                *
                *The RM entry in the 3D views dictionary may specify a 3D render mode dictionary.
                *
                *Table 307 shows the entries in a render mode dictionary.
                *
                *Table 307 - Entries in a render mode dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be 3DRenderMode.
                *
                *          Subtype             name                    (Required) The type of render mode described by this dictionary; see Table 308 for specific values. 
                *                                                      If an unrecognized value is encountered, then this render mode dictionary shall be ignored.
                *
                *          AC                  array                   (Optional) An array that specifies the auxiliary colour that shall be used when rendering the 3D image. 
                *                                                      The first entry in the array shall be a colour space; the subsequent entries shall be values specifying colour values in that colour space. 
                *                                                      The interpretation of this entry depends on the render mode specified by the Subtype entry, but it is often used to specify a colour for drawing points or edges.
                *                                                      The only valid colour space shall be DeviceRGB. 
                *                                                      If a colour space other than DeviceRGB is specified, this entry shall be ignored and the default value shall be used.
                *                                                      Default value: [/DeviceRGB 0 0 0] representing the colour black.
                *
                *          FC                  name or array           (Optional) A name or array that specifies the face color to be used when rendering the 3D image. 
                *                                                      This entry shall be relevant only when Subtype has a value of Illustration.
                *                                                      If the value of FC is an array, the first entry in the array shall be a colour space and the subsequent entries shall be values specifying values in that colour space.
                *                                                      The only valid colour space is DeviceRGB.
                *                                                      Any colour space other than DeviceRGB shall be ignored and the default value shall be used.
                *                                                      If the value of FC is a name, it shall describe a colour.The only valid name value shall BG, specifying the current background colour in use for displaying the artwork.
                *                                                      If a name other than BG is encountered, this entry shall be ignored and the background colour for the host annotation shall be used(see Table 189).
                *                                                      Default value: BG
                *
                *          O                   number                  (Optional) A number specifying the opacity of the added transparency applied by some render modes, using a standard additive blend.
                *                                                      Default value: 0.5
                *
                *          CV                  number                  (Optional) A number specifying the angle, in degrees, that shall be used as the crease value when determining silhouette edges. 
                *                                                      If two front-facing faces share an edge and the angle between the normals of those faces is greater than or equal to the crease value, then that shared edge shall be considered a silhouette edge.
                *                                                      Default value: 45
                *
                *For render modes that add a level of transparency to the rendering, the O entry specifies the additional opacity that shall be used. 
                *All such transparency effects use a standard additive blend mode.
                *
                *The CV entry sets the crease value that shall be used when determining silhouette edges, which may be used to adjust the appearance of illustrated render modes. 
                *An edge shared by two faces shall be considered a silhouette edge if either of the following conditions is met:
                *
                *  •   One face is front - facing and the other is back - facing.
                *
                *  •   The angle between the two faces is greater than or equal to the crease value.
                *
                *Table 308 describes the render modes that may be specified in a render mode dictionary.
                *
                *Table 308 - Render modes
                *
                *          [Mode]                          [Description]
                *
                *          Solid                           Displays textured and lit geometric shapes. In the case of artwork that conforms to the Universal 3D File Format specification, these shapes are triangles. 
                *                                          The AC entry shall be ignored.
                *
                *          SolidWireframe                  Displays textured and lit geometric shapes (triangles) with single colour edges on top of them. 
                *                                          The colour of these edges shall be determined by the AC entry.
                *
                *          Transparent                     Displays textured and lit geometric shapes (triangles) with an added level of transparency. 
                *                                          The AC entry shall be ignored.
                *
                *          TransparentWireframe            Displays textured and lit geometric shapes (triangles) with an added level of transparency, with single colour opaque edges on top of it. 
                *                                          The colour of these edges shall be determined by the ACentry.
                *
                *          BoundingBox                     Displays the bounding box edges of each node, aligned with the axes of the local coordinate space for that node. 
                *                                          The colour of the bounding box edges shall be determined by the AC entry.
                *
                *          TransparentBoundingBox          Displays bounding boxes faces of each node, aligned with the axes of the local coordinate space for that node, with an added level of transparency. 
                *                                          The colour of the bounding box faces shall be determined by the FC entry.
                *
                *          TransparentBoundingBoxOutline   Displays bounding boxes edges and faces of each node, aligned with the axes of the local coordinate space for that node, with an added level of transparency. 
                *                                          The colour of the bounding box edges shall be determined by the AC entry. 
                *                                          The colour of the bounding boxes faces shall be determined by the FC entry.
                *
                *          Wireframe                       Displays only edges in a single colour. The colour of these edges shall be determined by the AC entry.
                *
                *          ShadedWireframe                 Displays only edges, though interpolates their colour between their two vertices and applies lighting. 
                *                                          The AC entry shall be ignored.
                *
                *          HiddenWireframe                 Displays edges in a single colour, though removes back-facing and obscured edges. 
                *                                          The colour of these edges shall be determined by the AC entry.
                *
                *          Vertices                        Displays only vertices in a single colour. 
                *                                          The colour of these points shall be determined by the AC entry.
                *
                *          ShadedVertices                  Displays only vertices, though uses their vertex colour and applies lighting. 
                *                                          The AC entry shall be ignored.
                *
                *          Illustration                    Displays silhouette edges with surfaces, removes obscured lines. 
                *                                          The colour of these edges shall be determined by the AC entry, and the colour of the surfaces shall be determined by the FCentry.
                *
                *          SolidOutline                    Displays silhouette edges with lit and textured surfaces, removes obscured lines. 
                *                                          The colour of these edges shall be determined by the AC entry.
                *
                *          ShadedIllustration              Displays silhouette edges with lit and textured surfaces and an additional emissive term to remove poorly lit areas of the artwork. 
                *                                          The colour of these edges shall be determined by the AC entry.
                *
                *If a render mode type is encountered other than those described in Table 308, the render mode dictionary containing that entry shall be ignored by its consumers. 
                *This allows future documents using new render modes to behave consistently with future documents using new 3D view constructs that are ignored by older viewers.
                */

                /*13.6.4.5 3D Lighting Scheme Dictionaries
                *
                *A 3D lighting scheme dictionary(PDF 1.7) specifies the lighting to apply to 3D artwork.
                *The LS entry in the 3D view may include a 3D lighting scheme dictionary.
                *
                *Table 301 shows the entries in a 3D lighting scheme dictionary.
                *
                *Table 309 - Entries in a 3D lighting scheme dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be 3DLightingScheme.
                *
                *          Subtype             name                    (Required) The style of lighting scheme described by this dictionary (see Table 310).
                *
                *Table 310 describes the supported lighting schemes. With the exception of the Artwork lighting style, all the lights specified in Table 310 are infinite lights (also known as distant lights). Unlike lights from a point source, all rays from an infinite light source are emitted along a single direction vector. 
                *For lights specifying an ambientterm, this term shall be added to the diffuse colour of an object’s material. All colours shall be specified in the DeviceRGB colour space.
                *
                *When a style other than Artwork is used, only those lights described shall be present; any lighting described in the artwork shall not be used.
                *
                *Table 310 - 3D lighting shceme styles
                *
                *          [Scheme]                [Description]
                *
                *          Artwork                 Lights as specified in the 3D artwork. This has the same effect as if the 3D lighting scheme dictionary were omitted.
                *
                *          None                    No lights shall be used. That is, lighting specified in the 3D artwork shall be ignored.
                *
                *          White                   Three blue-grey infinite lights, no ambient term
                *                                  Light 1             Colour: < 0.38, 0.38, 0.45 > Direction: < -2.0, -1.5, -0.5 >
                *                                  Light 2             Colour: < 0.6, 0.6, 0.67 > Direction: < 2.0, 1.1, -2.5 >
                *                                  Light 3             Colour: < 0.5, 0.5, 0.57 > Direction: < -0.5, 0.0, 2.0 >
                *
                *          Day                     Three light-grey infinite lights, no ambient term
                *                                  Light 1             Colour: < 0.5, 0.5, 0.5 > Direction: < -2.0, -1.5, -.5 >
                *                                  Light 2             Colour: < 0.8, 0.8, 0.9 > Direction: < 2.0, 1.1, -2.5 >
                *                                  Light 3             Colour: < 0.9, 0.9, 0.9 > Direction: < 0.02, 0.01, 2.0 >
                *
                *          Night                   One yellow, one aqua, and one blue infinite light, no ambient term
                *                                  Light 1             Colour: < 1, .75, .39 > Direction: < -2.0, -1.5, -0.5 >
                *                                  Light 2             Colour: < 0.31, 0.47, 0.55 > Direction: < 2.0, 1.1, -2.5 >
                *                                  Light 3             Colour: < .5, .5, 1.0 > Direction: < 0.0, 0.0, 2.0 >
                *
                *          Hard                    Three grey infinite lights, moderate ambient term
                *                                  Light 1             Colour: < 0.5, 0.5, 0.5 > Direction: < -1.5, -1.5, -1.5 >
                *                                  Light 2             Colour: < 0.8, 0.8, 0.9 > Direction: < 1.5, 1.5, -1.5 >
                *                                  Light 3             Colour: < 0.9, 0.9, 0.9 > Direction: < -0.5, 0, 2.0 >
                *                                  AmbientColour: < 0.5, 0.5, 0.5 >
                *
                *          Primary                 One red, one green, and one blue infinite light, no ambient term
                *                                  Light 1             Colour: < 1, 0.2, 0.5 > Direction: < -2, -1.5, -0.5 >
                *                                  Light 2             Colour: < 0.2, 1.0, 0.5 > Direction: < 2.0, 1.1, -2.5 >
                *                                  Light 3             Colour: < 0, 0, 1 > Direction: < 0.0, 0.0, 2.0 >
                *
                *          Blue                    Three blue infinite lights, no ambient term
                *                                  Light 1             Colour: < 0.4, 0.4, 0.7 > Direction: < -2.0, -1.5, -0.5 >
                *                                  Light 2             Colour: < 0.75, 0.75, 0.95 > Direction: < 2.0, 1.1, -2.5 >
                *                                  Light 3             Colour: < 0.7, 0.7, 0.95 > Direction: < 0.0, 0.0, 2.0 >
                *
                *          Red                     Three red infinite lights, no ambient term
                *                                  Light 1             Colour: < 0.8, 0.3, 0.4 > Direction: < -2.0, -1.5, -0.5 >
                *                                  Light 2             Colour: < 0.95, 0.5, 0.7 > Direction: < 2.0, 1.1, -2.5 >
                *                                  Light 3             Colour: < 0.95, 0.4, 0.5 > Direction: < 0.0, 0.0, 2.0 >
                *
                *          Cube                    Six grey infinite lights aligned with the major axes, no ambient term
                *                                  Light 1             Colour: < .4, .4, .4 > Direction: < 1.0, 0.01, 0.01 >
                *                                  Light 2             Colour: < .4, .4, .4 > Direction: < 0.01, 1.0, 0.01 >
                *                                  Light 3             Colour: < .4, .4, .4 > Direction: < 0.01, 0.01, 1.0 >
                *                                  Light 4             Colour: < .4, .4, .4 > Direction: < -1.0, 0.01, 0.01 >
                *                                  Light 5             Colour: < .4, .4, .4 > Direction: < 0.01, -1.0, 0.01 >
                *                                  Light 6             Colour: < .4, .4, .4 > Direction: < 0.01, 0.01, -1.0 >
                *
                *          CAD                     Three grey infinite lights and one light attached to the camera, no ambient term
                *                                  Light 1             Colour: < 0.72, 0.72, 0.81 > Direction: < 0.0, 0.0, 0.0 >
                *                                  Light 2             Colour: < 0.2, 0.2, 0.2 > Direction: < -2.0, -1.5, -0.5 >
                *                                  Light 3             Colour: < 0.32, 0.32, 0.32 > Direction: < 2.0, 1.1, -2.5 >
                *                                  Light 4             Colour: < 0.36, 0.36, 0.36 > Direction: < 0.04, 0.01, 2.0 >
                *
                *          Headlamp                Single infinite light attached to the camera, low ambient term
                *                                  Light 1             Colour: < 0.8, 0.8, 0.9 > Direction: < 0.0, 0.0, 0.0 >
                *                                  AmbientColour: < 0.1, 0.1, 0.1 >
                *
                *NOTE      If a lighting scheme style is encountered other than those described in Table 310, the lighting scheme dictionary containing that entry shall be ignored. 
                *          This allows future documents using new lighting schemes to behave consistently with future documents using new 3D view constructs. 
                *          That is, the expected behaviour is for the conforming reader to ignore unrecognized lighting styles and 3D view constructs.
                */

                /*13.6.4.6 3D Cross Section Dictionaries
                *
                *A 3D cross section dictionary(PDF 1.7) specifies how a portion of the 3D artwork shall be clipped for the purpose of showing artwork cross sections.
                *The SA entry of a 3D view may specify multiple 3D cross section dictionaries.
                *
                *NOTE      Cross sections enable conforming readers to display otherwise hidden parts of the artwork. 
                *          They also allow users to comment on cross sections, using markup annotations. 
                *          For example, markup annotations can be used to apply markup annotations to a cross section or to measure distances in a cross section. 
                *          If multiple cross sections are specified for a view, the markup annotations in the view apply to all cross sections in the view.
                *
                *Table 311 shows the entries in a 3D cross section dictionary.
                *
                *Table 311 - Entries in a 3D cross section dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be 3DCrossSection for a 3D cross section dictionary.
                *
                *          C                   array                   (Optional) A three element array specifying the center of rotation on the cutting plane in world space coordinates (see 13.6.5, “Coordinate Systems for 3D”).
                *                                                      Default value: [0 0 0] specifying a cutting plane rotating about the origin of the world space.
                *
                *          O                   array                   (Required) A three-element array specifying the orientation of the cutting plane in world space, where each value represents the orientation in relation to the X, Y, and Z axes, respectively (see 13.6.5, “Coordinate Systems for 3D”). 
                *                                                      Exactly one of the values shall be null, indicating an initial state of the cutting plane that is perpendicular to the corresponding axis and clipping all geometry on the positive side of that axis. 
                *                                                      The other two values shall be numbers indicating the rotation of the plane, in degrees, around their corresponding axes. 
                *                                                      The order in which these rotations are applied shall match the order in which the values appear in the array.
                *                                                      Default value: [null 0 0] specifying a cutting plane that is perpendicular to the X axis and coplanar with the Y and Z axes.
                *
                *          PO                  number                  (Optional) A number in the range [0, 1] indicating the opacity of the cutting plane using a standard additive blend mode.
                *                                                      Default value: 0.5
                *
                *          PC                  array                   (Optional) An array that specifies the colour for the cutting plane. 
                *                                                      The first entry in the array is a colour space, and the remaining entries are values in that colour space. 
                *                                                      The only valid colour space is DeviceRGB. If a colour space other than DeviceRGB is specified, this entry shall be ignored and the default value shall be used.
                *                                                      Default value: [/DeviceRGB 1 1 1] representing the colour white.
                *
                *          IV                  boolean                 (Optional) A flag indicating the visibility of the intersection of the cutting plane with any 3D geometry. 
                *                                                      If true, then the intersection shall be visible. 
                *                                                      If false, then the intersection shall not be visible.
                *                                                      Default value: false
                *
                *          IC                  array                   (Optional) An array that specifies the colour for the cutting plane’s intersection with the 3D artwork. 
                *                                                      The first entry in the array is a colour space, and the remaining entries are values in that colour space. 
                *                                                      The only valid colour space is DeviceRGB. 
                *                                                      If a colour space other than DeviceRGB is specified, this entry shall be ignored and the default value shall be used. 
                *                                                      This entry is meaningful only if IV is true.
                *                                                      Default value: [/DeviceRGB 0 1 0] representing the colour green.
                *
                *The C entry specifies the center of the cutting plane. This implies that the plane passes through the center point, but it is also the point of reference when determining the orientation of the plane.
                *
                *The O array indicates the orientation of the cutting plane, taking into account its center. The orientation may be determined by a two-step process:
                *
                *  •   The plane shall be situated such that it passes through point C, and oriented such that it is perpendicular to the axis specified by the array entry whose value is null.
                *
                *  •   For each of the other two axes, the plane shall be rotated the specified number of degrees around the associated axis, while maintaining C as a fixed point on the plane. 
                *      Since the two axes are perpendicular, the order in which the rotations are performed is irrelevant.
                *
                *The PO entry specifies the opacity of the plane itself when rendered, while the PC entry provides its colour.
                *When the PO entry is greater than 0, a visual representation of the cutting plane shall be rendered with the 3D artwork.
                *This representation is a square with a side length equal to the length of the diagonal of the maximum bounding box for the 3D artwork, taking into account any keyframe animations present.
                *When the PO entry is 0, no visible representation of the cutting plane shall be rendered.
                *
                *The IV entry shall be a boolean value that determines whether a visual indication shall be drawn of the plane’s intersection with the 3D artwork.
                *If such an indication is drawn, the IC entry shall specify its colour.
                *
                *EXAMPLE       The following example describes a set of views and corresponding cross sections that illustrate the various effects of orientation.
                *
                *              3 0 obj                                             %CrossSection1
                *                  <<
                *                      / Type / 3DCrossSection
                *                      / C[0 0 0]
                *                      / O[null 0 0]
                *                      / PO 0.35
                *                      / PC[/ DeviceRGB 0.75 0.86 1]
                *                      / IV true
                *                      / IC[/ DeviceRGB 0 1 0]
                *                  >>
                *              endobj
                *              4 0 obj                                             % CrossSection2
                *                  <<
                *                      / Type / 3DCrossSection
                *                      / C[0 0 0]
                *                      / O[null - 30 0]
                *                      / PO 0.35
                *                      / PC[/ DeviceRGB 0.75 0.86 1]
                *                      / IV true
                *                      / IC[/ DeviceRGB 0 1 0]
                *                  >>
                *              endobj
                *              5 0 obj                                             %CrossSection3
                *                  <<
                *                      / Type / 3DCrossSection
                *                      / C[0 0 0]
                *                      / O[null 0 30]
                *                      / PO 0.35
                *                      / PC[/ DeviceRGB 0.75 0.86 1]
                *                      / IV true
                *                      / IC[/ DeviceRGB 0 1 0]
                *                  >>
                *              endobj
                *              6 0 obj                                             % CrossSection4
                *                  <<
                *                      / Type / 3DCrossSection
                *                      /C [0 0 0]
                *                      / O[null - 30 30]
                *                      / PO 0.35
                *                      / PC[/ DeviceRGB 0.75 0.86 1]
                *                      / IV true
                *                      / IC[/ DeviceRGB 0 1 0]
                *                  >>
                *              endobj
                *              7 0 obj                                             % View0
                *                  <<
                *                      / Type / 3DView
                *                      / XN(NoCrossSection)
                *                      / SA[]
                *                      ...
                *                  >>
                *              endobj
                *              8 0 obj                                             % View1
                *                  <<
                *                      / Type / 3DView
                *                      / XN(CrossSection1)
                *                      / SA[3 0 R]
                *                      ...
                *                  >>
                *              endobj
                *              9 0 obj                                             % View2
                *                  <<
                *                      / Type / 3DView
                *                      / XN(CrossSection2)
                *                      / SA[4 0 R]
                *                      ...
                *                  >>
                *              endobj
                *              10 0 obj                                            % View3
                *                  <<
                *                      / Type / 3DView
                *                      / XN(CrossSection3)
                *                      / SA[5 0 R]
                *                      ...
                *                  >>
                *              endobj
                *              11 0 obj                                            % View4
                *                  <<
                *                      / Type / 3DView
                *                      / XN(CrossSection4)
                *                      / SA[6 0 R]
                *                      ...
                *                  >>
                *              endobj
                *
                *The following illustrations show the views described in the previous example, some of which include cross sections.
                *
                *(see Figure 74 - Rendenring of the 3D artwork using View0 (no crosso section), on page 535)
                *
                *Figure 74 through Figure 78 use world coordinates whose origin is the center of the cube. 
                *The axes illustrated in each diagram show the relative orientation of the world coordinate axes, not the actual position of those axes. 
                *These axes are not part of the 3D artwork used in this example.
                *
                *(see Figure 75 - Rendenring of the 3D artwork using View1 (cross section perpendicular to the x axis), on page 535)
                *
                *Figure 75 shows the cross section specified for the 3DView that references CrossSection1. 
                *The illustration shows the edges of the cutting plane ending at the edges of the annotation’s rectangle. 
                *This cross section specifies a plane with the following characteristics:
                *
                *  •   Includes the world art origin: / C[0 0 0]
                *
                *  •   Perpendicular to the X axis and parallel to the Y and Z axes: / O[null 0 0]
                *
                *  •   Opacity of the cutting plane is 35 %: / PO 0.35
                *
                *  •   Colour of the cutting plane is light blue: /PC [/DeviceRGB 0.75 0.86 1]
                *
                *  •   Intersection of the cutting plane with the object is visible: / IV true
                *
                *  •   Colour of the intersection of the cutting plane and the object is green:
                *          / IC[/ DeviceRGB 0 1 0]
                *
                *(see Figure 76 - Rendenring of the 3D artwork using View2 (cross section rotated around the y axis by -30 degrees), on page 536)
                *
                *Figure 76 shows the cross section specified for the 3DView that references CrossSection2. 
                *This cross section specifies a plane that differs from the one specified in CrossSection1 (Figure 75) in the following way:
                *
                *  •   Perpendicular to the X axis, rotated - 30 degrees around the Y axis, and parallel to the Z axis: / O[null - 30 0]
                *
                *(see Figure 77 - Rendenring of the 3D artwork using View3 (cross section rotated around the z axis by 30 degrees), on page 536)
                *
                *Figure 77 shows the cross section specified for the 3DView that references CrossSection3. 
                *This cross section specifies a plane that differs from the one specified in CrossSection1 (Figure 75) in the following way:
                *
                *  •   Perpendicular to the X axis, parallel to the Y axis, and rotated 30 degrees around the Z axis: / O[null 0 30]
                *
                *(see Figure 78 - Rendenring of the 3D artwork using View4 (cross section rotated around the y axis by -30 degrees and around the z axis by 30 degrees), on page 537)
                *
                *Figure 78 shows the cross section specified for the 3DView that references CrossSection4. 
                *This cross section specifies a plane that differs from the one specified in CrossSection1 (Figure 75) in the following way:
                *
                *  •   Perpendicular to the X axis, rotated - 30 degrees around the Y axis, and rotated 30 degrees around the Zaxis: / O[null - 30 30]
                */

                /*13.6.4.7 3D Node Dictionaries
                *
                *A 3D view may specify a 3D node dictionary(PDF 1.7), which specifies particular areas of 3D artwork and the opacity and visibility with which individual nodes shall be displayed.
                *The 3D artwork shall be contained in the parent 3D stream object.The NA entry of the 3D views dictionary may specify multiple 3D node dictionaries for a particular view.
                *
                *NOTE 1        While many PDF dictionaries reference 3D artwork in its entirety, it is often useful to reference 3D artwork at a more granular level.
                *              This enables properties such as visibility, opacity, and orientation to be applied to subsets of the 3D artwork.
                *              These controls enable underlying nodes to be revealed, by making the overlying nodes transparent or by moving them out of the way.
                *
                *NOTE 2        Do not confuse nodes with view nodes.
                *              A node is a PDF dictionary that specifies an area in 3D artwork, while a view node is a parameter in the 3D artwork that specifies a view.
                *
                *Table 312 shows the entries in a 3D node dictionary.
                *
                *Table 312 - Entries in a 3D node dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be 3DNode for a 3D node dictionary.
                *
                *          N                   text string         (Required) The name of the node being described by the node dictionary. 
                *                                                  If the Subtype of the corresponding 3D Stream is U3D, this entry corresponds to the field Node block name, as described in the Universal 3D file formatspecification (see Bibliography). 
                *                                                  In the future, nodes may be described using other 3D conventions.
                *
                *                                                  NOTE        When comparing this entry to node names for a particular convention(such as Universal 3D), conforming readers shall translate between the PDF text encoding used by PDF and the character encoding specified in the 3D stream.
                *
                *          O                   number              (Optional) A number in the range [0, 1] indicating the opacity of the geometry supplied by this node using a standard additive blend mode.
                *                                                  If this entry is absent, the viewer shall use the opacity specified for the parent node or for the 3D artwork(in ascending order).
                *
                *          V                   boolean             (Optional) A flag indicating the visibility of this node. If true, then the node is visible. If false, then the node shall not be visible.
                *                                                  If this entry shall be absent, the viewer shall use the visibility specified for the parent node or for the 3D artwork(in ascending order).
                *
                *          M                   array               (Optional) A 12-element 3D transformation matrix that specifies the position and orientation of this node, relative to its parent, in world coordinates (see 13.6.5, “Coordinate Systems for 3D”).
                *
                *
                *The N entry specifies which node in the 3D stream corresponds to this node dictionary.
                *
                *The O entry describes the opacity that shall be used when rendering this node, and the V entry shall determine whether or not the node is rendered at all.
                *While a node with an opacity of 0 shall be rendered in the same way as a non - visible node, having a separate value for the visibility of a node allows interactive conforming readersto show / hide partially transparent nodes, without overwriting the intended opacity of those nodes.
                *
                *The M entry specifies the node’s matrix relative to its parent, in world coordinates.
                *If an hierarchy of nodes is intended to be repositioned while still maintaining its internal structure, then only the node at the root of the hierarchy needs to be adjusted.
                *
                *EXAMPLE       The following example shows a 3D view specifying an array of node parameters.
                *
                *              3 0 obj                             % Default node params with all shapes visible and opaque
                *                  [   <</ Type / 3DNode
                *                        / N(Sphere)
                *                        / O1
                *                        / Vtrue
                *                        / M[...] >>
                *                      <</ Type / 3DNode
                *                        / N(Cone)
                *                        / O 1
                *                        / V true >>
                *                      <</Type /3DNode
                *                        / N(Cube)
                *                        / O 1
                *                        / V true >>
                *                  ]
                *              4 0 obj                             % Params with the cone hidden and the sphere semi-transparent
                *                  [   <</ Type / 3DNode
                *                        / N(Sphere)
                *                        / O0.5
                *                        / Vtrue >>
                *                      <</ Type / 3DNode
                *                        / N(Cone)
                *                        / O 1
                *                        / V false >>
                *                      <</ Type / 3DNode
                *                        / N(Cube)
                *                        / O 1
                *                        / V true >>
                *                  ]
                *              endobj
                *              5 0 obj                             % View1, using the default set of node params
                *                  <<
                *                      / Type / 3DView
                *                      / XN(View1)
                *                      / NA 3 0 R
                *                      ...
                *                  >>
                *              endobj
                *              6 0 obj                             % View2, using the alternate set of node params
                *                  <<
                *                      / Type / 3DView
                *                      / XN(View2)
                *                      / NA 4 0 R
                *                      ...
                *                  >>
                *              endobj
                *
                *(see Figure 79 - Rendenring of the 3D artwork using View1 (all shapes visible and opaque), on page 540)
                *
                *Figure 79 shows a view whose node array includes three nodes, all of which shall be rendered with the appearance opaque (/O 1) and visible (/V true).
                *
                *(see Figure 80 - Rendenring of the 3D artwork using View2 (the cone is hidden and the sphere is semi-transparent), on page 540)
                *
                *Figure 80 shows a view with a node array that specifies the same three nodes used in Figure 79. These nodes have the following display characteristics:
                *
                *  •   The node named Sphere is partially transparent (/ O 0.5) and visible(/ V true)
                *
                *  •   The node named Cone is opaque (/ O 1) and invisible(/ V false)
                *
                *  •   The node named Cube is opaque (/ O 1) and visible(/ V true)
                *
                */

            /*13.6.5 Coordinate Systems for 3D
            *
                *3D artwork is a collection of objects whose positions and geometry shall be specified using three-dimensional coordinates. 
                *8.3, “Coordinate Systems,” discusses the concepts of two-dimensional coordinate systems, their geometry and transformations. 
                *This sub-clause extends those concepts to include the third dimension.
                *
                *As described in 8.3, “Coordinate Systems,” positions shall be defined in terms of pairs of x and y coordinates on the Cartesian plane. 
                *The origin of the plane specifies the location(0, 0); x values increase to the right and yvalues increase upward. 
                *For three-dimensional graphics, a third axis, the z axis, shall be used.The origin shall be at(0, 0, 0); positive z values increase going into the page.
                *
                *In two-dimensional graphics, the transformation matrix transforms the position, size, and orientation of objects in a plane. 
                *It is a 3 - by - 3 matrix, where only six of the elements may be changed; therefore, the matrix shall beexpressed in PDF as an array of six numbers:
                *
                *          [   a   b   0   ]
                *          [   c   d   0   ]   =   [   a   b   c   d   tx  ty  ]
                *          [   tx  ty  1   ]
                *
                *In 3D graphics, a 4-by-4 matrix shall be used to transform the position, size, and orientations of objects in a three-dimensional coordinate system. 
                *Only the first three columns of the matrix may be changed; therefore, the matrix shall be expressed in PDF as an array of 12 numbers:
                *
                *          [   a   b   c   0   ]
                *          [   d   e   f   0   ]   =   [   a   b   c   d   e   f   g   i   tx  ty  tz  ]
                *          [   g   h   i   0   ]
                *          [   tx  ty  tz  1   ]
                *
                *3D coordinate transformations shall be expressed as matrix transformations:
                *
                *          [   x' y'  z'  1    ]   =   [   x   y   z   1   ] x [   a   b   c   0   ]
                *                                                              [   d   e   f   0   ]
                *                                                              [   g   h   i   0   ]
                *                                                              [   tx  ty  tz  1   ]
                *
                *Carrying out the multiplication has the following results:
                *
                *          x'  =   a x X + d x Y + g x Z + tx
                *
                *          y'  =   b x X + e x Y + h x Z + ty
                *
                *          z'  =   c x X + f x Y + i x Z + tz
                *
                *Position and orientation of 3D artwork typically involves translation (movement) and rotation along any axis. 
                *The virtual camera represents the view of the artwork. The relationship between camera and artwork may be thought of in two ways:
                *
                *  •   The 3D artwork is in a fixed position and orientation, and the camera moves to different positions and orientations.
                *
                *  •   The camera is in a fixed location, and the 3D artwork is translated and rotated.
                *
                *Both approaches may achieve the same visual effects; in practice, 3D systems typically use a combination of both. 
                *Conceptually, there are three distinct coordinate systems:
                *
                *  •   The artwork coordinate system.
                *
                *  •   The camera coordinate system, in which the camera shall be positioned at(0, 0, 0) facing out along the positive z axis, with the positive x axis to the right and the positive y axis going straight up.
                *
                *  •   An intermediate system called the world coordinate system.
                *      Two 3D transformation matrices shall be used in coordinate conversions:
                *
                *  •   The artwork-to - world matrix specifies the position and orientation of the artwork in the world coordinate system. 
                *      This matrix shall be contained in the 3D stream.
                *
                *  •   The camera-to - world matrix specifies the position and orientation of the camera in the world coordinate system. 
                *      This matrix shall be specified by either the C2W or U3DPath entries of the 3D view dictionary.
                *
                *When drawing 3D artwork in a 3D annotation’s target coordinate system, the following transformations take place:
                *
                *  a)  Artwork coordinates shall be transformed to world coordinates:
                *
                *      [   Xw  Yw  Zw  1   ]   =   [   Xa  Ya  Za  1   ]   x aw
                *
                *  b)  World coordinates shall be transformed to camera coordinates:
                *
                *      [   Xc  Yc  Zc  1   ]   =   [   Xw  Yw  Zw  1   ]   x (cw(^-1))
                *
                *  c)  The first two steps can be expressed as a single equation, as follows:
                *
                *      [   Xc Yc Zc 1 ]    =   [   Xa  Ya  Za  1   ]   x   (aw x cw(^-1))
                *
                *  d)  Finally, the camera coordinates shall be projected into two dimensions, eliminating the z coordinate, then scaled and positioned within the annotation’s target coordinate system.
                */
            
            /*13.6.6 3D Markup
            *
                *Beginning with PDF 1.7, users may comment on specific views of 3D artwork by using markup annotations (see 12.5.6.2, “Markup Annotations”). 
                *Markup annotations (as other annotations) are normally associated with a location on a page. 
                *To associate the markup with a specific view of a 3D annotation, the annotation dictionary for the markup annotation contains an ExData entry (see Table 174) that specifies the 3D annotation and view. 
                *Table 313 describes the entries in an external data dictionary used to markup 3D annotations.
                *
                *Table 313 - Entries in an external data dictionary used to markup 3D annotations
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Required) The type of PDF object that this dictionary describes; if present, shall be ExData for an external data dictionary.
                *
                *          Subtype             name                (Required) The type of external data that this dictionary describes; shall be Markup3D for a 3D comment. 
                *                                                  The only defined value is Markup3D.
                *
                *          3DA                 dictionary          (Required) The 3D annotation to which this markup annotation applies. 
                *                              or text string      The 3D annotation may be specified as a child dictionary or as the name of a 3D annotation, as specified by its NM entry. 
                *                                                  In the latter case, the 3D annotation and the markup annotation shall be on the same page of the document.
                *
                *          3DV                 dictionary          (Required) The 3D view that this markup annotation is associated with. 
                *                                                  The annotation will be hidden unless this view is currently being used for the 3D annotation specified by 3DA.     
                *
                *          MD5                 byte string         (Optional) A 16-byte string that contains the checksum of the bytes of the 3D stream data that this 3D comment shall be associated with. 
                *                                                  The checksum shall be calculated by applying the standard MD5 message-digest algorithm (described in Internet RFC 1321, The MD5 Message-Digest Algorithm; see the Bibliography) to the bytes of the stream data. 
                *                                                  This value shall be used to determine if artwork data has changed since this 3D comment was created.
                *
                *In a Markup3D ExData dictionary, the 3DA entry identifies the 3D annotation to which the markup shall beassociated. 
                *Even though the markup annotation exists alongside the associated annotation in the page’s Annots array, the markup may be thought of as a child of the 3DA annotation.
                *
                *The 3DV entry specifies the markup’s associated 3D view.
                *The markup shall only be printed and displayed when the specified view is the current view of its parent 3D annotation.
                *This ensures that the proper context is preserved when the markup is displayed.
                *
                *NOTE      An equivalent view is not sufficient; if more than one markup specify equivalent views represented by different objects, the markups will not display simultaneously.
                *
                *The MD5 entry gives conforming readers a means to detect whether or not the 3D stream of the 3D annotation specified by 3DA has changed.
                *If the 3D stream has changed, the context provided by the 3DV entry may no longer apply, and the markup may no longer be useful.
                *Any action taken as a response to such a situation is dependent on the conforming reader, but a warning shall be issued to the user.
                *
                *EXAMPLE       The following example shows how markup annotations can be associated with particular views.
                *
                *              2 0 obj                         % 3D stream data with two named views
                *                  <<
                *                      / Type / 3D
                *                      / Subtype / U3D
                *                      / VA[4 0 R 5 0 R]
                *                      ...
                *                  >>
                *              stream
                *              ...
                *              endstream
                *              endobj
                *              3 0 obj                         % 3D annotation
                *                  <<
                *                      / Type / Annot
                *                      / Subtype / 3D
                *                      / 3DD 2 0 R
                *                  >>
                *              endobj
                *              4 0 obj                         % CommentView1
                *                  <<
                *                      / Type / 3DView
                *                      / XN(CommentView1)
                *                      ...
                *                  >>
                *              endobj
                *              5 0 obj                         % CommentView2
                *                  <<
                *                      / Type / 3DView
                *                      / XN(CommentView2)
                *                      ...
                *                  >>
                *              endobj
                *              6 0 obj                         % Cloud comment with no ExData
                *                  <<
                *                      / Type / Annot
                *                      / Subtype / Polygon
                *                      / IT / PolygonCloud
                *                      ...
                *                   >>
                *              endobj
                *              7 0 obj                         % Callout comment on CommentView1
                *                   <<
                *                      / Type / Annot
                *                      / Subtype / FreeText
                *                      / IT / FreeTextCallout
                *                      / ExData <<
                *                          / Type / Markup3D
                *                          / 3DA 3 0 R
                *                          / 3DV 4 0 R
                *                      >>
                *                      ...
                *                  >>
                *              endobj
                *              8 0 obj                         % Dimension comment on CommentView2
                *                  <<
                *                      / Type / Annot
                *                      / Subtype / Line
                *                      / IT / LineDimension
                *                      / ExData <<
                *                          / Type / Markup3D
                *                          / 3DA 3 0 R
                *                          / 3DV 5 0 R
                *                      >>
                *                      ...
                *                  >>
                *              endobj
                *              9 0 obj                         % Stamp comment on CommentView2
                *                  <<
                *                       / Type / Annot
                *                       / Subtype / Stamp
                *                       / ExData <<
                *                          / Type / Markup3D
                *                          / 3DA 3 0 R
                *                          / 3DV 5 0 R
                *                       >>
                *                       ...
                *                  >>
                *              endobj
                *
                *The following illustrations show the placement of markup on annotations on different views of the same 3D artwork.
                *
                *(see Figure 81 - 3D artwork set to its default view, on page 5450
                *
                *Figure 81 shows the default view, which has no markup annotations.
                *
                *(see Figure 82 - 3D artwork set to CommentView1, on page 545)
                *
                *Figure 82 shows another view to which a markup annotation is applied.
                *
                *(see Figure 83 - 3D artwork set to CommentView2, on page 546)
                *
                *Figure 83 shows a view referenced by two markup annotations:
                *
                *  •   A line annotation(/ Subtype / Line) with a line dimension intent
                *          (/ IT / LineDimension)
                *
                *  •   A stamp annotation(/ Subtype / Stamp)
                */
            

        }

    }

}
