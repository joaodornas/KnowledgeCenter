using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfPlugin
{
    //8 Graphics
    public class Graphics
    {

        /*  8.1 General
        *
        *The graphics operators used in PDF content streams describe the appearance of pages that are to be reproduced on a raster output device. 
        *The facilities described in this clause are intended for both printer and display applications.
        *
        *The graphics operators form six main groups:
        *
        *  •   Graphics state operators manipulate the data structure called the graphics state, the global framework within which the other graphics operators execute.
        *      The graphics state includes the current transformation matrix (CTM), which maps user space coordinates used within a PDF content stream into output device coordinates.
        *      It also includes the current colour, the current clipping path, and many other parameters that are implicit operands of the painting operators.
        *
        *  •   Path construction operators specify paths, which define shapes, line trajectories, and regions of various sorts.
        *      They include operators for beginning a new path, adding line segments and curves to it, and closing it.
        *
        *  •   Path-painting operators fill a path with a colour, paint a stroke along it, or use it as a clipping boundary.
        *
        *  •   Other painting operators paint certain self-describing graphics objects.These include sampled images, geometrically defined shadings, 
        *      and entire content streams that in turn contain sequences of graphics operators.
        *
        *  •   Text operators select and show character glyphs from fonts (descriptions of typefaces for representing text characters). 
        *      Because PDF treats glyphs as general graphical shapes, many of the text operators could be grouped with the graphics state or painting operators.
        *      However, the data structures and mechanisms for dealing with glyph and font descriptions are sufficiently specialized that clause 9, "Text" focuses on them.
        *
        *  •   Marked-content operators associate higher-level logical information with objects in the content stream. 
        *      This information does not affect the rendered appearance of the content (although it may determine if the content should be presented at all; see 8.11, "Optional Content"); 
        *      it is useful to applications that use PDF for document interchange. Marked content is described in 14.6, "Marked Content".
        *
        *This clause presents general information about device-independent graphics in PDF: how a PDF content stream describes the abstract appearance of a page.
        *Rendering—the device-dependent part of graphics—is covered in clause 10, "Rendering". 
        *The Bibliography lists a number of books that give details of these computer graphics concepts and their implementation.
        */

        /*8.2 Graphics Objects
        *
        *As discussed in 7.8.2, "Content Streams", the data in a content stream shall be interpreted as a sequence of operators and their operands, expressed as basic data objects according to standard PDF syntax. 
        *A content stream can describe the appearance of a page, or it can be treated as a graphical element in certain other contexts.
        *
        *The operands and operators shall be written sequentially using postfix notation. Although this notation resembles the sequential execution model of the PostScript language, a PDF content stream is not a program to be interpreted; 
        *rather, it is a static description of a sequence of graphics objects.
        *There are specific rules, described below, for writing the operands and operators that describe a graphics object.
        *
        *PDF provides five types of graphics objects:
        *
        *  •   A path object is an arbitrary shape made up of straight lines, rectangles, and cubic Bézier curves. A path may intersect itself and may have disconnected sections and holes.
        *      A path object ends with one or more painting operators that specify whether the path shall be stroked, filled, used as a clipping boundary, or some combination of these operations.
        *
        *  •   A text object consists of one or more character strings that identify sequences of glyphs to be painted. Like a path, text can be stroked, filled, or used as a clipping boundary.
        *
        *  •   An external object (XObject) is an object defined outside the content stream and referenced as a named resource (see 7.8.3, "Resource Dictionaries"). The interpretation of an XObject depends on its type.
        *      An image XObject defines a rectangular array of colour samples to be painted; a form XObject is an entire content stream to be treated as a single graphics object. 
        *      Specialized types of form XObjects shall be used to import content from one PDF file into another (reference XObjects) and to group graphical elements together as a unit for various purposes (group XObjects). 
        *      In particular, the latter are used to define transparency groups for use in the transparent imaging model (transparency group XObjects, discussed in detail in clause 11, "Transparency"). 
        *      There is also a PostScript XObject that may appear in some existing PDF files, but it should not be used by a PDF 1.7 conforming writer.
        *
        *  •   An inline image object uses a special syntax to express the data for a small image directly within the content stream.
        *
        *  •   A shading object describes a geometric shape whose colour is an arbitrary function of position within the shape. 
        *      (A shading can also be treated as a colour when painting other graphics objects; it is not considered to be a separate graphics object in that case.)
        *
        *PDF 1.3 and earlier versions use an opaque imaging model in which each graphics object is painted in sequence, completely obscuring any previous marks it may overlay on the page. 
        *PDF 1.4 introduced a transparent imaging model in which objects can be less than fully opaque, allowing previously painted marks to show through. 
        *Each object is painted on the page with a specified opacity, which may be constant at every point within the object’s shape or may vary from point to point. 
        *The previously existing contents of the page form a backdrop with which the new object is composited, producing results that combine the colours of the object and backdrop according to their respective opacity characteristics. 
        *The objects at any given point on the page forms a transparency stack, where the stacking order is defined to be the order in which the objects shall be specified, bottommost object first. 
        *All objects in the stack can potentially contribute to the result, depending on their colours, shapes, and opacities.
        *
        *PDF’s graphics parameters are so arranged that objects shall be painted by default with full opacity, reducing the behaviour of the transparent imaging model to that of the opaque model.
        *Accordingly, the material in this clause applies to both the opaque and transparent models except where explicitly stated otherwise; the transparent model is described in its full generality in clause 11, "Transparency".
        *
        *Although the painting behaviour described above is often attributed to individual operators making up an object, it is always the object as a whole that is painted.
        *Figure 9 in Annex L shows the ordering rules for the operations that define graphics objects.
        *Some operations shall be permitted only in certain types of graphics objects or in the intervals between graphics objects (called the page description level in the figure). 
        *Every content stream begins at the page description level, where changes may be made to the graphics state, such as colours and text attributes, as discussed in the following sub-clauses.
        *
        *In the Figure 9 in Annex L, arrows indicate the operators that mark the beginning or end of each type of graphics object. Some operators are identified individually, others by general category.
        *Table 51 summarizes these categories for all PDF operators.
        *
        *
        *Table 51 - Operator Categories
        *
        *          [Category]                      [Operators]                         [Table]
        *
        *          General graphics state          w, J, j, M, d, ri, i, gs            57
        *
        *          Special graphics state          q, Q, cm                            57
        *
        *          Path construction               m, l, c, v, y, h, re                59
        *
        *          Path painting                   S, s, f, F, f*, B, B*, b, b*, n     60
        *
        *          Clipping paths                  W, W*                               61
        *
        *          Text objects                    BT, ET                              107
        *
        *          Text state                      Tc, Tw, Tz, TL, Tf, Tr, Ts
        *
        *          Text positioning                Td, TD, Tm, T*                      108
        *
        *          Text showing                    Tj, TJ, ', "                        109
        *
        *          Type 3 fonts                    d0, d1                              113
        *
        *          Color                           CS, cs, SC, SCN, sc, scn, G         74
        *                                          g, RG, rg, K, k
        *
        *          Shading patterns                sh                                  77
        *
        *          Inline images                   BI, ID, EI                          92
        *
        *          XObjects                        Do                                  87
        *
        *          Marked content                  MP, DP, BMC, BDC, EMC               320
        *
        *          Compatibility                   BX, EX                              32
        *
        *
        *(see Figure 9 - Graphics Objects, on page 113)
        *
        *EXAMPLE       The path construction operators m and re signal the beginning of a path object. Inside the path object, additional path construction operators are permitted, as are the clipping path operators W and W*, but not general graphics state operators such as w or J. 
        *              A path-painting operator, such as S or f, ends the path object and returns to the page description level.
        *
        *NOTE          A conforming reader may process a content stream whose operations violate these rules for describing graphics objects and can produce unpredictable behaviour, even though it may display and print the stream correctly. Applications that attempt to extract graphics objects for editing or other purposes depend on the objects’ being well formed.
        *              The rules for graphics objects are also important for the proper interpretation of marked content(see 14.6, "Marked Content").
        *
        *A graphics object also implicitly includes all graphics state parameters that affect its behaviour.
        *For instance, a path object depends on the value of the current colour parameter at the moment the path object is defined.
        *The effect shall be as if this parameter were specified as part of the definition of the path object. 
        *However, the operators that are invoked at the page description level to set graphics state parameters shall not be considered to belong to any particular graphics object. 
        *Graphics state parameters should be specified only when they change. 
        *A graphics object can depend on parameters that were defined much earlier.
        *
        *Similarly, the individual character strings within a text object implicitly include the graphics state parameters on which they depend. Most of these parameters may be set inside or outside the text object. 
        *The effect is as if they were separately specified for each text string.
        *
        *The important point is that there is no semantic significance to the exact arrangement of graphics state operators. 
        *A conforming reader or writer of a PDF content stream may change an arrangement of graphics state operators to any other arrangement that achieves the same values of the relevant graphics state parameters for each graphics object. 
        *A conforming reader or writer shall not infer any higher-level logical semantics from the arrangement of tokens constituting a graphics object. 
        *A separate mechanism, marked content(see 14.6, "Marked Content"), allows such higher-level information to be explicitly associated with the graphics objects.
        */

        //8.3 Coordinate Systems
        public class Coordinate_Systems
        { 
           /*8.3.1 General
        *
        *Coordinate systems define the canvas on which all painting occurs. They determine the position, orientation, and size of the text, graphics, and images that appear on a page. 
        *This sub-clause describes each of the coordinate systems used in PDF, how they are related, and how transformations among them are specified.
        *
        *NOTE      The coordinate systems discussed in this sub-clause apply to two-dimensional graphics.
        *          PDF 1.6 introduced the ability to display 3D artwork, in which objects are described in a three-dimensional coordinate system, as described in 13.6.5, "Coordinate Systems for 3D".
        */

           /*8.3.2 Coordinate Spaces
        *
        */

                /*8.3.2.1 General
        *
        *Paths and positions shall be defined in terms of pairs of coordinates on the Cartesian plane. A coordinate pair is a pair of real numbers x and y that locate a point horizontally and vertically within a two-dimensional coordinate space. 
        *A coordinate space is determined by the following properties with respect to the current page:
        *
        *  •   The location of the origin
        *
        *  •   The orientation of the x and y axes
        *
        *  •   The lengths of the units along each axis
        *
        *PDF defines several coordinate spaces in which the coordinates specifying graphics objects shall be interpreted.
        *The following sub - clauses describe these spaces and the relationships among them.
        *
        *Transformations among coordinate spaces shall be defined by transformation matrices, which can specify any linear mapping of two - dimensional coordinates, including translation, scaling, rotation, reflection, and skewing.
        *Transformation matrices are discussed in 8.3.3, "Common Transformations" and 8.3.4, "Transformation Matrices".
        */

                /*8.3.2.2 Device Space
        *
        *The contents of a page ultimately appear on a raster output device such as a display or a printer. 
        *Such devices vary greatly in the built-in coordinate systems they use to address pixels within their imageable areas. 
        *A particular device’s coordinate system is called its device space. The origin of the device space on different devices can fall in different places on the output page; 
        *on displays, the origin can vary depending on the window system. 
        *Because the paper or other output medium moves through different printers and imagesetters in different directions, the axes of their device spaces may be oriented differently. 
        *For instance, vertical (y) coordinates may increase from the top of the page to the bottom on some devices and from bottom to top on others. 
        *Finally, different devices have different resolutions; some even have resolutions that differ in the horizontal and vertical directions.
        *
        *NOTE      If coordinates in a PDF file were specified in device space, the file would be device-dependent and would appear differently on different devices.
        *
        *EXAMPLE   Images specified in the typical device spaces of a 72 - pixel - per - inch display and a 600 - dot - per - inch printer would differ in size by more than a factor of 8; 
        *an 8 - inch line segment on the display would appear less than 1 inch long on the printer. 
        *          Figure 10 in Annex L shows how the same graphics object, specified in device space, can appear drastically different when rendered on different output devices.
        *
        *(see Figure 10 - Device Space, on page 115)
        */

                /*8.3.2.3 User Space
        *
        *To avoid the device-dependent effects of specifying objects in device space, PDF defines a device-independent coordinate system that always bears the same relationship to the current page, 
        *regardless of the output device on which printing or displaying occurs. 
        *This device-independent coordinate system is called user space.
        *
        *The user space coordinate system shall be initialized to a default state for each page of a document.
        *The CropBox entry in the page dictionary shall specify the rectangle of user space corresponding to the visible area of the intended output medium (display window or printed page).
        *The positive x axis extends horizontally to the right and the positive y axis vertically upward, as in standard mathematical practice (subject to alteration by the Rotate entry in the page dictionary).
        *The length of a unit along both the x and y axes is set by the UserUnit entry(PDF 1.6) in the page dictionary (see Table 30). If that entry is not present or supported, the default value of 1⁄72 inch is used.
        *This coordinate system is called default user space.
        *
        *NOTE 1        In PostScript, the origin of default user space always corresponds to the lower - left corner of the output medium.
        *              While this convention is common in PDF documents as well, it is not required; the page dictionary’s CropBox entry can specify any rectangle of default user space to be made visible on the medium.
        *
        *NOTE 2        The default for the size of the unit in default user space (1⁄72 inch) is approximately the same as a point, a unit widely used in the printing industry.
        *              It is not exactly the same, however; there is no universal definition of a point.
        *
        *Conceptually, user space is an infinite plane. Only a small portion of this plane corresponds to the imageable area of the output device: a rectangular region defined by the CropBox entry in the page dictionary.
        *The region of default user space that is viewed or printed can be different for each page and is described in 14.11.2, "Page Boundaries".
        *
        *Coordinates in user space (as in any other coordinate space) may be specified as either integers or real numbers, and the unit size in default user space does not constrain positions to any arbitrary grid.
        *The resolution of coordinates in user space is not related in any way to the resolution of pixels in device space.
        *
        *The transformation from user space to device space is defined by the current transformation matrix (CTM), an element of the PDF graphics state (see 8.4, "Graphics State"). 
        *A conforming reader can adjust the CTM for the native resolution of a particular output device, maintaining the device-independence of the PDF page description. 
        *Figure 11 in Annex L shows how this allows an object specified in user space to appear the same regardless of the device on which it is rendered.
        *
        *NOTE 3        The default user space provides a consistent, dependable starting place for PDF page descriptions regardless of the output device used.
        *              If necessary, a PDF content stream may modify user space to be more suitable to its needs by applying the coordinate transformation operator, cm (see 8.4.4, "Graphics State Operators").
        *              Thus, what may appear to be absolute coordinates in a content stream are not absolute with respect to the current page because they are expressed in a coordinate system that may slide around 
        *              and shrink or expand.
        *              Coordinate system transformation not only enhances device - independence but is a useful tool in its own right.
        *
        *EXAMPLE       A content stream originally composed to occupy an entire page can be incorporated without change as an element of another page by shrinking the coordinate system in which it is drawn.
        *
        *(see Figure 11 - User Space, on page 116)
        */

                /*8.3.2.4 Other Coordinate Spaces
        *
        *In addition to device space and user space, PDF uses a variety of other coordinate spaces for specialized purposes:
        *
        *  •   The coordinates of text shall be specified in text space. 
        *      The transformation from text space to user space shall be defined by a text matrix in combination with several text-related parameters in the graphics state (see 9.4.2, "Text-Positioning Operators").
        *
        *  •   Character glyphs in a font shall be defined in glyph space (see 9.2.4, "Glyph Positioning and Metrics"). 
        *      The transformation from glyph space to text space shall be defined by the font matrix. For most types of fonts, this matrix shall be predefined to map 1000 units of glyph space to 1 unit of text space; 
        *      for Type 3 fonts, the font matrix shall be given explicitly in the font dictionary(see 9.6.5, "Type 3 Fonts").
        *
        *  •   All sampled images shall be defined in image space. The transformation from image space to user space shall be predefined and cannot be changed. All images shall be 1 unit wide by 1 unit high in user space,
        *      regardless of the number of samples in the image. To be painted, an image shall be mapped to a region of the page by temporarily altering the CTM.
        *
        *  •   A form XObject (discussed in 8.10, "Form XObjects") is a self-contained content stream that can be treated as a graphical element within another content stream. 
        *      The space in which it is defined is called form space. The transformation from form space to user space shall be specified by a form matrix contained in the form XObject.
        *
        *  •   PDF 1.2 defined a type of colour known as a pattern, discussed in 8.7, "Patterns".
        *      A pattern shall be defined either by a content stream that shall be invoked repeatedly to tile an area or by a shading whose colour is a function of position. 
        *      The space in which a pattern is defined is called pattern space. The transformation from pattern space to user space shall be specified by a pattern matrix contained in the pattern.
        *
        *  •   PDF 1.6 embedded 3D artwork, which is described in three - dimensional coordinates (see 13.6.5, "Coordinate Systems for 3D") that are projected into an annotation’s target coordinate system 
        *      (see 13.6.2, "3D Annotations").
        */

                /*8.3.2.5 Relationships among Coordinate Spaces
        *
        *Figure 12 in Annex L shows the relationships among the coordinate spaces described above. 
        *Each arrow in the figure represents a transformation from one coordinate space to another.
        *PDF allows modifications to many of these transformations.
        *
        *Because PDF coordinate spaces are defined relative to one another, changes made to one transformation can affect the appearance of objects defined in several 
        *coordinate spaces.
        *
        *EXAMPLE       A change in the CTM, which defines the transformation from user space to device space, affects forms, text, images, and patterns, 
        *since they are all upstream from user space.
        */

            /*8.3.3 Common Transformations
        *
        *A transformation matrix specifies the relationship between two coordinate spaces. By modifying a transformation matrix, objects can be scaled, rotated, translated, or transformed in other ways.
        *
        *            (see Figure 12 - Relationships Among Coordinate Systems)
        *
        *                                      ___________
        *                                    |             |
        *                     Image Space -> |             |
        *      Glyphe Space -> Text Space -> | User Space  | -> Device Space
        *                   Pattern Space -> |             | 
        *                      Form Space -> | ___________ |
        *
        *
        *A transformation matrix in PDF shall be specified by six numbers, usually in the form of an array containing six elements. 
        *In its most general form, this array is denoted [a b c d e f]; it can represent any linear transformation from one coordinate system to another. 
        *This sub-clause lists the arrays that specify the most common transformations; 8.3.4, "Transformation Matrices", 
        *discusses more mathematical details of transformations, including information on specifying transformations that are combinations of those listed here:
        *
        *  •   Translations shall be specified as [1 0 0 1 tx ty], where tx and ty shall be the distances to translate the origin of the coordinate system in the horizontal and vertical dimensions, respectively.
        *
        *  •   Scaling shall be obtained by [sx 0 0 sy 0 0]. This scales the coordinates so that 1 unit in the horizontal and vertical dimensions of the new coordinate system is the same size as sx and sy units, respectively, in the previous coordinate system.
        *
        *  •   Rotations shall be produced by [cos q sin q - sin q cos q 0 0], which has the effect of rotating the coordinate system axes by an angle q counter clockwise.
        *
        *  •   Skew shall be specified by [1 tan a tan b 1 0 0], which skews the x axis by an angle a and the y axis by an angle b.
        *
        *      Figure 13 in Annex L shows examples of each transformation. The directions of translation, rotation, and skew shown in the figure correspond to positive values of the array elements.
        *
        *(see Figure 13 - Effects of Coordinate Transformations, on page 118)
        *
        *NOTE      If several transformations are combined, the order in which they are applied is significant. For example, first scaling and then translating the x axis is not the same as first translating and then scaling it. 
        *          In general, to obtain the expected results, transformations should be done in the following order: Translate, Rotate, Scale or skew.
        *
        *          Figure 14 in Annex L shows the effect of the order in which transformations are applied.The figure shows two sequences of transformations applied to a coordinate system.After each successive transformation, an outline of the letter n is drawn.
        *
        *(see Figure 14 - Effect of Transformation Order, on page 119)
        *
        *NOTE      The following transformations are shown in the figure: a translation of 10 units in the x direction and 20 units in the y direction; a rotation of 30 degrees; a scaling by a factor of 3 in the x direction
        *
        *          In the figure, the axes are shown with a dash pattern having a 2-unit dash and a 2-unit gap. In addition, the original (untransformed) axes are shown in a lighter colour for reference.
        *          Notice that the scale-rotate-translate ordering results in a distortion of the coordinate system, leaving the x and y axes no longer perpendicular; 
        *          the recommended translate-rotate-scale ordering results in no distortion.
        */

            /*8.3.4 Transformation Matrices
        *
        *This sub-clause discusses the mathematics of transformation matrices.
        *
        *To understand the mathematics of coordinate transformations in PDF, it is vital to remember two points:
        *
        *  •   Transformations alter coordinate systems, not graphics objects.
        *      All objects painted before a transformation is applied shall be unaffected by the transformation.Objects painted after the transformation is applied shall be interpreted in the transformed coordinate system.
        *
        *  •   Transformation matrices specify the transformation from the new(transformed) coordinate system to the original(untransformed) coordinate system.
        *      All coordinates used after the transformation shall be expressed in the transformed coordinate system. 
        *      PDF applies the transformation matrix to find the equivalent coordinates in the untransformed coordinate system.
        *
        *NOTE 1        Many computer graphics textbooks consider transformations of graphics objects rather than of coordinate systems. 
        *              Although either approach is correct and self-consistent, some details of the calculations differ depending on which point of view is taken.
        *
        *PDF represents coordinates in a two-dimensional space. The point (x, y) in such a space can be expressed in vector form as [x y 1]. 
        *The constant third element of this vector (1) is needed so that the vector can be used with 3-by-3 matrices in the calculations described below.
        *
        *The transformation between two coordinate systems can be represented by a 3-by-3 transformation matrix written as follows:
        *
        *  [ a b 0 ]
        *  [ c d 0 ]
        *  [ e f 1 ]
        *
        *Because a transformation matrix has only six elements that can be changed, in most cases in PDF it shall be specified as the six-element array [a b c d e f].
        *
        *Coordinate transformations shall be expressed as matrix multiplications:
        *
        *  [x' y' 1] = [x y 1] x [ a b 0 ]
        *                        [ c d 0 ]
        *                        [ e f 1 ]
        *
        *Because PDF transformation matrices specify the conversion from the transformed coordinate system to the original (untransformed) coordinate system, x¢ and y¢ in this equation shall be the coordinates in the untransformed coordinate system, and x and y shall be the coordinates in the transformed system. 
        *The multiplication is carried out as follows:
        *
        *  X'= A x X + C x Y + E
        *  Y'= B x X + D x Y + F
        *
        *If a series of transformations is carried out, the matrices representing each of the individual transformations can be multiplied together to produce a single equivalent matrix representing the composite transformation.
        *
        *NOTE 2    Matrix multiplication is not commutative—the order in which matrices are multiplied is significant.Consider a sequence of two transformations: a scaling transformation applied to the user space coordinate system, followed by a conversion from the resulting scaled user space to device space.Let MS be the matrix specifying the scaling and MC the current transformation matrix, which transforms user space to device space.
        *          Recalling that coordinates are always specified in the transformed space, the correct order of transformations first converts the scaled coordinates to default user space and then converts the default user space coordinates to device space. 
        *          This can be expressed as
        *
        *          Xd = Xu x Mc = (Xs x Ms) x MC = Xs x (Ms x Mc)
        *
        *where
        *
        *  Xd      denotes the coordinates in device space
        *  Xu      denotes the coordinates in default user space
        *  Xs      denotes the coordinates in scaled user space
        *
        *This shows that when a new transformation is concatenated with an existing one, the matrix representing it shall be multiplied before (premultiplied with) the existing transformation matrix.
        *
        *This result is true in general for PDF: when a sequence of transformations is carried out, 
        *the matrix representing the combined transformation(M¢) is calculated by premultiplying the matrix representing the additional transformation(MT) with the one representing all previously existing transformations(M):
        *
        *  M' = Mt x M
        *
        *NOTE 3    When rendering graphics objects, it is sometimes necessary for a conforming reader to perform the inverse of a transformation—that is, to find the user space coordinates that correspond to a given pair of device space
        *          coordinates. Not all transformations are invertible, however. For example, if a matrix contains a, b, c, and delements that are all zero, all user coordinates map to the same device coordinates and there is no unique inverse transformation. 
        *          Such noninvertible transformations are not very useful and generally arise from unintended operations, such as scaling by 0. Use of a noninvertible matrix when painting graphics objects can result in unpredictable behaviour.
        */
        }

        //8.4 Graphics State
        public class Graphics_State
        {
            /*8.4.1 General
            *
            *A conforming reader shall maintain an internal data structure called the graphics state that holds current graphics control parameters. 
            *These parameters define the global framework within which the graphics operators execute.
            *
            *EXAMPLE 1     The f(fill) operator implicitly uses the current colour parameter, and the S(stroke) operator additionally uses the current line width parameter from the graphics state.
            *
            *A conforming reader shall initialize the graphic state at the beginning of each page with the values specified in Table 52 and Table 53. 
            *Table 52 lists those graphics state parameters that are device-independent and are appropriate to specify in page descriptions.
            *The parameters listed in Table 53 control details of the rendering (scan conversion) process and are device-dependent; a page description that is intended to be device-independent should not be written to modify these parameters.
            *
            *Table 52 - Device-Independent Graphics State Parameters
            *
            *          [Parameter]                 [Type]                      [Value]
            *
            *          CTM                         array                       The current transformation matrix, which maps positions from user coordinates to device coordinates (see 8.3, "Coordinate Systems"). This matrix is modified by each application of the coordinate transformation operator, cm. Initial value: a matrix that transforms default user coordinates to device coordinates.
            *
            *          clipping path               (internal)                  The current clipping path, which defines the boundary against which all output shall be cropped (see 8.5.4, "Clipping Path Operators"). Initial value: the boundary of the entire imageable portion of the output page.
            *
            *          color space                 name or array               The current colour space in which colour values shall beinterpreted (see 8.6, "Colour Spaces"). There are two separate colour space parameters: one for stroking and one for all other painting operations. Initial value: DeviceGray.
            *
            *          color                       (various)                   The current colour to be used during painting operations (see 8.6, "Colour Spaces"). The type and interpretation of this parameter depend on the current colour space; for most colour spaces, a colour value consists of one to four numbers. There are two separate colour parameters: one for stroking and one for all other painting operations. Initial value: black.
            *
            *          text state                  (various)                   A set of nine graphics state parameters that pertain only to the painting of text. These include parameters that select the font, scale the glyphs to an appropriate size, and accomplish other effects. The text state parameters are described in 9.3, "Text State Parameters and Operators".
            *
            *          line width                  number                      The thickness, in user space units, of paths to be stroked (see 8.4.3.2, "Line Width"). Initial value: 1.0.
            *
            *          line cap                    integer                     A code specifying the shape of the endpoints for any open path that is stroked (see 8.4.3.3, "Line Cap Style"). Initial value: 0, for square butt caps.
            *
            *          line join                   integer                     A code specifying the shape of joints between connected segments of a stroked path (see 8.4.3.4, "Line Join Style"). Initial value: 0, for mitered joins.
            *
            *          miter limit                 number                      The maximum length of mitered line joins for stroked paths (see 8.4.3.5, "Miter Limit"). This parameter limits the length of “spikes” produced when line segments join at sharp angles. Initial value: 10.0, for a miter cutoff below approximately 11.5 degrees.
            *
            *          dash pattern                array and number            A description of the dash pattern to be used when paths are stroked (see 8.4.3.6, "Line Dash Pattern"). Initial value: a solid line.
            *
            *          rendering intent            name                        The rendenring intent to be used when converting CIE-based colours to device colours (see 8.6.5.8, "Rendering Intents"). Initial value: RelativeColorimetric.
            *
            *          stroke adjustment           boolean                     (PDF 1.2) A flag specifying whether to compensate for possible rasterization effects when stroking a path with a line width that is small relative to the pixel resolution of the output device (see 10.6.5, "Automatic Stroke Adjustment").
            *                                                                  NOTE        This is considered a device-independent parameter, even though the details of its effects are device-dependent.
            *                                                                  Initial value: false.
            *
            *          blend mode                  name or array               (PDF 1.4) The current blend mode to be used in the transparent imaging model (see 11.3.5, "Blend Mode" and 11.6.3, "Specifying Blending Colour Space and Blend Mode"). 
            *                                                                  A conforming reader shall implicitly reset this parameter to its initial value at the beginning of execution of a transparency group XObject (see 11.6.6, "Transparency Group XObjects"). 
            *                                                                  Initial value: Normal.
            *
            *          soft mask                   dictionary or               (PDF 1.4) A soft-mask dictionary (see 11.6.5.2, "Soft-Mask Dictionaries") specifying the mask shape or mask opacity values to be used in the transparent imaging model (see 11.3.7.2, "Source Shape and Opacity" and 11.6.4.3, "Mask Shape and Opacity"), or the name None if no such mask is specified.
            *                                      array                       A conforming reader shall implicitly reset this parameter implicitly reset to its initial value at the beginning of execution of a transparency group XObject (see 11.6.6, "Transparency Group XObjects"). 
            *                                                                  Initial value: None.
            *
            *          alpha constant              number                      (PDF 1.4) The constant shape or constant opacity value to be used in the transparent imaging model (see 11.3.7.2, "Source Shape and Opacity" and 11.6.4.4, "Constant Shape and Opacity"). 
            *                                                                  There are two separate alpha constant parameters: one for stroking and one for all other painting operations. 
            *                                                                  A conforming reader shall implicitly reset this parameter to its initial value at the beginning of execution of a transparency group XObject (see 11.6.6, "Transparency Group XObjects"). 
            *                                                                  Initial value: 1.0.
            *
            *          alpha source                boolean                     (PDF 1.4) A flag specifying whether the current soft mask and alpha constant parameters shall be interpreted as shape values (true) or opacity values (false). 
            *                                                                  This flag also governs the interpretation of the SMask entry, if any, in an image dictionary (see 8.9.5, "Image Dictionaries"). 
            *                                                                  Initial value: false.
            *
            *
            *Table 53 - Device-Dependent Graphics State Parameters
            *
            *          [Parameter]                 [Type]                      [Value]
            *
            *          overprint                   boolean                     (PDF 1.2) A flag specifying (on output devices that support the overprint control feature) whether painting in one set of colorants should cause the corresponding areas of other colorants to be erased (false) or left unchanged (true); see 8.6.7, "Overprint Control". 
            *                                                                  In PDF 1.3, there are two separate overprint parameters: one for stroking and one for all other painting operations. 
            *                                                                  Initial value: false.
            *
            *          overprint mode              number                      (PDF 1.3) A code specifying whether a colour component value of 0 in a DeviceCMYK colour space should erase that component (0) or leave it unchanged (1) when overprinting (see 8.6.7, "Overprint Control"). 
            *                                                                  Initial value: 0.
            *
            *          black generation            function                    (PDF 1.2) A function that calculates the level of the black colour component to use when converting RGB colours to CMYK (see 10.3.4, "Conversion from DeviceRGB to DeviceCMYK"). 
            *                                      or name                     Initial value: a conforming reader shall initialize this to a suitable device dependent value.
            *
            *          undercolor removal          function                    (PDF 1.2) A function that calculates the reduction in the levels of the cyan, magenta, and yellow colour components to compensate for the amount of black added by black generation (see 10.3.4, "Conversion from DeviceRGB to DeviceCMYK"). 
            *                                      or name                     Initial value: a conforming reader shall initialize this to a suitable device dependent value.
            *
            *          transfer                    function,                   (PDF 1.2) A function that adjusts device gray or colour component levels to compensate for nonlinear response in a particular output device (see 10.4, “Transfer Functions“). 
            *                                      array, or name              Initial value: a conforming reader shall initialize this to a suitable device dependent value.
            *
            *          halftone                    dictionary,                 (PDF 1.2) A halftone screen for gray and colour rendering, specified as a halftone dictionary or stream (see 10.5, "Halftones"). 
            *                                      stream, or name             Initial value: a conforming reader shall initialize this to a suitable device dependent value.
            *
            *          flatness                    number                      The precision with which curves shall be rendered on the output device (see 10.6.2, "Flatness Tolerance"). The value of this parameter (positive number) gives the maximum error tolerance, measured in output device pixels; smaller numbers give smoother curves at the expense of more computation and memory use. 
            *                                                                  Initial value: 1.0.
            *
            *          smoothness                  number                      (PDF 1.3) The precision with which colour gradients are to be rendered on the output device (see 10.6.3, "Smoothness Tolerance"). The value of this parameter (0 to 1.0) gives the maximum error tolerance, expressed as a fraction of the range of each colour component; smaller numbers give smoother colour transitions at the expense of more computation and memory use. 
            *                                                                  Initial value: a conforming reader shall initialize this to a suitable device dependent value.
            *
            *
            *NOTE 1    Some graphics state parameters are set with specific PDF operators, some are set by including a particular entry in a graphics state parameter dictionary, and some can be specified either way.
            *
            *EXAMPLE 2     The current line width can be set either with the w operator or (in PDF 1.3) with the LW entry in a graphics state parameter dictionary, whereas the current colour is set only with specific operators, and the current halftone is set only with a graphics state parameter dictionary.
            *
            *In general, a conforming reader, when interpreting the operators that set graphics state parameters, shall simply store them unchanged for later use when interpreting the painting operators.However, some parameters have special properties or call for behaviour that a conforming reader shall handle:
            *
            *  •   Most parameters shall be of the correct type or have values that fall within a certain range.
            *
            *  •   Parameters that are numeric values, such as the current colour, line width, and miter limit, shall be forced into valid range, if necessary.However, they shall not be adjusted to reflect capabilities of the raster output device, such as resolution or number of distinguishable colours. Painting operators perform such adjustments, but the adjusted values shall not be stored back into the graphics state.
            *
            *  •   Paths shall be internal objects that shall not be directly represented in PDF.
            *
            *NOTE 2    As indicated in Table 52 and Table 53, some of the parameters—color space, color, and overprint—have two values, one used for stroking(of paths and text objects) and one for all other painting operations.
            *          The two parameter values can be set independently, allowing for operations such as combined filling and stroking of the same path with different colours.
            *          Except where noted, a term such as current colour should be interpreted to refer to whichever colour parameter applies to the operation being performed.When necessary, the individual colour parameters are distinguished explicitly as the stroking colour and the nonstroking colour.
            */

            /*8.4.2 Graphics State Stack
            *
            *A PDF document typically contains many graphical elements that are independent of each other and nested to multiple levels. 
            *The graphics state stack allows these elements to make local changes to the graphics state without disturbing the graphics state of the surrounding environment. 
            *The stack is a LIFO (last in, first out) data structure in which the contents of the graphics state may be saved and later restored using the following operators:
            *
            *  •   The q operator shall push a copy of the entire graphics state onto the stack.
            *
            *  •   The Q operator shall restore the entire graphics state to its former value by popping it from the stack.
            *
            *NOTE      These operators can be used to encapsulate a graphical element so that it can modify parameters of the graphics state and later restore them to their previous values.
            *
            *Occurrences of the q and Q operators shall be balanced within a given content stream (or within the sequence of streams specified in a page dictionary’s Contents array).
            */

            /*8.4.3 Details of Graphics State Parameters
            *
            */

                 /*8.4.3.1 General
            *
            *This sub - clause gives details of several of the device - independent graphics state parameters listed in Table 52.
            */

                 /*8.4.3.2 Line Width
            *
            *The line width parameter specifies the thickness of the line used to stroke a path.
            *It shall be a non - negative number expressed in user space units; stroking a path shall entail painting all points whose perpendicular distance from the path in user space is less than or equal to half the line width.
            *The effect produced in device space depends on the current transformation matrix(CTM) in effect at the time the path is stroked.
            *If the CTM specifies scaling by different factors in the horizontal and vertical dimensions, the thickness of stroked lines in device space shall vary according to their orientation. 
            *The actual line width achieved can differ from the requested width by as much as 2 device pixels, depending on the positions of lines with respect to the pixel grid.Automatic stroke adjustment may be used to ensure uniform line width; see 10.6.5, "Automatic Stroke Adjustment".
            *
            *A line width of 0 shall denote the thinnest line that can be rendered at device resolution: 1 device pixel wide.However, some devices cannot reproduce 1 - pixel lines, and on high - resolution devices, they are nearly invisible. 
            *Since the results of rendering such zero - width lines are device - dependent, they should not be used.
            */

                 /*8.4.3.3 Line Cap Style
            *
            *The line cap style shall specify the shape that shall be used at the ends of open subpaths(and dashes, if any) when they are stroked. 
            *Table 54 shows the possible values.
            *
            *(se Table 54 on page 125)
            *Table 54 - Line Cap Styles
            *
            *      [Style]             [Appearance]                [Description]
            *
            *      0                                               Butt cap. The stroke shall be squared off at the endpoint of the path. There shall be no projection beyond the end of the path.
            *      
            *      1                                               Round cap. A semicircular arc with a diameter equal to the line width shall be drawn around the endpoint and shall be filled in.
            *
            *      2                                               Projecting square cap. The stroke shall continue beyond the endpoint of the path for a distance equal to half the line width and shall besquared off.
            */

                 /*8.4.3.4 Line Join Style
            *
            *The line join style shall specify the shape to be used at the corners of paths that are stroked.Table 55 shows the possible values. 
            *Join styles shall be significant only at points where consecutive segments of a path connect at an angle; segments that meet or intersect fortuitously shall receive no special treatment.
            *
            *(see Table 55 on page 126)
            *Table 55 - Line Join Styles
            *
            *      [Style]             [Appearance]                [Description]
            *
            *      0                                               Miter join. The outer edges of the strokes for the two segments shall beextended until they meet at an angle, as in a picture frame. If the segments meet at too sharp an angle (as defined by the miter limit parameter—see 8.4.3.5, "Miter Limit"), a bevel join shall be used instead.
            *
            *      1                                               Round join. An arc of a circle with a diameter equal to the line width shall be drawn around the point where the two segments meet, connecting the outer edges of the strokes for the two segments. This pieslice-shaped figure shall be filled in, producing a rounded corner.
            *
            *      2                                               Bevel join. The two segments shall be finished with butt caps (see 8.4.3.3, "Line Cap Style") and the resulting notch beyond the ends of the segments shall be filled with a triangle.
            *
            *NOTE      The definition of round join was changed in PDF 1.5. In rare cases, the implementation of the previous specification could produce unexpected results.  
            */

                 /*8.4.3.5 Miter Limit
            *
            *When two line segments meet at a sharp angle and mitered joins have been specified as the line join style, it is possible for the miter to extend far beyond the thickness of the line stroking the path. 
            *The miter limit shall impose a maximum on the ratio of the miter length to the line width (see Figure 15 in Annex L). 
            *When the limit is exceeded, the join is converted from a miter to a bevel.
            *
            *The ratio of miter length to line width is directly related to the angle j between the segments in user space by the following formula:
            *
            *  miterLength / lineWidth = 1 / sin( ϕ/2 )
            *
            *EXAMPLE       A miter limit of 1.414 converts miters to bevels for j less than 90 degrees, a limit of 2.0 converts them for j less than 60 degrees, and a limit of 10.0 converts them for j less than approximately 11.5 degrees.
            *
            *(see Figure 15 - Miter Length, on page 126)
            */

                 /*8.4.3.6 Line Dash Pattern
            *
            *The line dash pattern shall control the pattern of dashes and gaps used to stroke paths. 
            *It shall be specified by a dash array and a dash phase. The dash array’s elements shall be numbers that specify the lengths of alternating dashes and gaps; the numbers shall be nonnegative and not all zero. 
            *The dash phase shall specify the distance into the dash pattern at which to start the dash. The elements of both the dash array and the dash phase shall be expressed in user space units.
            *
            *Before beginning to stroke a path, the dash array shall be cycled through, adding up the lengths of dashes and gaps.When the accumulated length equals the value specified by the dash phase, stroking of the path shall begin, and the dash array shall be used cyclically from that point onward.
            *Table 56 shows examples of line dash patterns. As can be seen from the table, an empty dash array and zero phase can be used to restore the dash pattern to a solid line.
            *
            *(see Table 56 on page 127)
            *
            *Table 56 - Examples of Line Dash Patterns
            *
            *          [Dash Array and Phase]              [Appearance]                        [Description]
            *
            *          [] 0                                                                    No dash; solid, unbroken lines
            *
            *          [3] 0                                                                   3 units on, 3 units off, ...
            *
            *          [2] 1                                                                   1 on, 2 off, 2 on, 2 off, ...
            *
            *          [2 1] 0                                                                 2 on, 1 off, 2 on, 1 off, ...
            *
            *          [3 5] 6                                                                 2 off, 3 on, 5 off, 3 on, 5 off, ...
            *
            *          [2 3] 11                                                                1 on, 3 off, 2 on, 3 off, 2 on, ...
            *
            *Dashed lines shall wrap around curves and corners just as solid stroked lines do. 
            *The ends of each dash shall be treated with the current line cap style, and corners within dashes shall be treated with the current line join style. 
            *A stroking operation shall take no measures to coordinate the dash pattern with features of the path; it simply shall dispense dashes and gaps along the path in the pattern defined by the dash array.
            *
            *When a path consisting of several subpaths is stroked, each subpath shall be treated independently—that is, the dash pattern shall be restarted and the dash phase shall be reapplied to it at the beginning of each subpath.
            */

            /*8.4.4 Graphics State Operators
            *
            *Table 57 shows the operators that set the values of parameters in the graphics state. (See also the colour operators listed in Table 74 and the text state operators in Table 105.)
            *
            *Table 57 - Graphics State Operators
            *
            *      [Operands]              [Operator]                  [Descriptor]
            *
            *      -                       q                           Save the current graphics state on the graphics state stack (see 8.4.2, "Graphics State Stack").
            *
            *      -                       Q                           Restore the graphics state by removing the most recently saved state from the stack and making it the current state (see 8.4.2, "Graphics State Stack").
            *
            *      a b c d e f             cm                          Modify the current transformation matrix (CTM) by concatenating the specified matrix (see 8.3.2, "Coordinate Spaces"). Although the operands specify a matrix, they shall be written as six separate numbers, not as an array.
            *
            *      lineWidth               w                           Set the line width in the graphics state (see 8.4.3.2, "Line Width").
            *
            *      lineCap                 J                           Set the line cap style in the graphics state (see 8.4.3.3, "Line Cap Style").
            *
            *      lineJoin                j                           Set the line join style in the graphics state (see 8.4.3.4, "Line Join Style").
            *
            *      miterLimit              M                           Set the miter limit in the graphics state (see 8.4.3.5, "Miter Limit").
            *
            *      dashArray dashPhase     d                           Set the line dash pattern in the graphics state (see 8.4.3.6, "Line Dash Pattern").
            *
            *      intent                  ri                          (PDF 1.1) Set the colour rendering intent in the graphics state (see 8.6.5.8, "Rendering Intents").
            *
            *      flatness                i                           Set the flatness tolerance in the graphics state (see 10.6.2, "Flatness Tolerance"). flatness is a number in the range 0 to 100; a value of 0 shall specify the output device’s default flatness tolerance.
            *
            *      dictName                gs                          (PDF 1.2) Set the specified parameters in the graphics state. dictName shall be the name of a graphics state parameter dictionary in the ExtGState subdictionary of the current resource dictionary (see the next sub-clause).
            *
            */

            /*8.4.5 Graphics State Parameter Dictionaries
            *
            *While some parameters in the graphics state may be set with individual operators, as shown in Table 57, others may not. 
            *The latter may only be set with the generic graphics state operator gs (PDF 1.2). 
            *The operand supplied to this operator shall be the name of a graphics state parameter dictionary whose contents specify the values of one or more graphics state parameters. 
            *This name shall be looked up in the ExtGState subdictionary of the current resource dictionary.
            *
            *The graphics state parameter dictionary is also used by type 2 patterns, which do not have a content stream in which the graphics state operators could be invoked (see 8.7.4, "Shading Patterns").
            *
            *Each entry in the parameter dictionary shall specify the value of an individual graphics state parameter, as shown in Table 58.
            *All entries need not be present for every invocation of the gs operator; the supplied parameter dictionary may include any combination of parameter entries.
            *The results of gs shall be cumulative; parameter values established in previous invocations persist until explicitly overridden.
            *
            *NOTE      Note that some parameters appear in both Table 57 and Table 58; these parameters can be set either with individual graphics state operators or with gs. 
            *          It is expected that any future extensions to the graphics state will be implemented by adding new entries to the graphics state parameter dictionary rather than by introducing new graphics state operators.
            *
            *Table 58 - Entries in a Graphics State Parameter Dictionary
            *
            *              [Key]               [Type]                  [Description]
            *
            *              Type                name                    (Optional) The type of PDF object that this dictionary describes; shall be ExtGState for a graphics state parameter dictionary.
            *
            *              LW                  number                  (Optional; PDF 1.3) The line width (see 8.4.3.2, "Line Width").
            *
            *              LC                  integer                 (Optional; PDF 1.3) The line cap style (see 8.4.3.3, "Line Cap Style").
            *
            *              LJ                  integer                 (Optional; PDF 1.3) The line join style (see 8.4.3.4, "Line Join Style").
            *
            *              ML                  number                  (Optional; PDF 1.3) The miter limit (see 8.4.3.5, "Miter Limit").
            *
            *              D                   array                   (Optional; PDF 1.3) The line dash pattern, expressed as an array of the form [dashArray dashPhase], where dashArray shall be itself an array and dashPhase shall be an integer (see 8.4.3.6, "Line Dash Pattern").
            *
            *              RI                  name                    (Optional; PDF 1.3) The name of the rendering intent (see 8.6.5.8, "Rendering Intents").
            *
            *              OP                  boolean                 (Optional) A flag specifying whether to apply overprint (see 8.6.7, "Overprint Control"). In PDF 1.2 and earlier, there is a single overprint parameter that applies to all painting operations. 
            *                                                          Beginning with PDF 1.3, there shall be two separate overprint parameters: one for stroking and one for all other painting operations. 
            *                                                          Specifying an OP entry shall set both parameters unless there is also an op entry in the same graphics state parameter dictionary, in which case the OP entry shall set only the overprint parameter for stroking.
            *
            *              op                  boolean                 (Optional; PDF 1.3) A flag specifying whether to apply overprint (see 8.6.7, "Overprint Control") for painting operations other than stroking. If this entry is absent, the OP entry, if any, shall also set this parameter.
            *
            *              OPM                 integer                 (Optional; PDF 1.3) The overprint mode (see 8.6.7, "Overprint Control").
            *
            *              Font                array                   (Optional; PDF 1.3) An array of the form [font size], where font shall be an indirect reference to a font dictionary and size shall be a number expressed in text space units. 
            *                                                          These two objects correspond to the operands of the Tf operator (see 9.3, "Text State Parameters and Operators"); however, the first operand shall be an indirect object reference instead of a resource name.
            *
            *              BG                  function                (Optional) The black-generation function, which maps the interval [0.0 1.0] to the interval [0.0 1.0] (see 10.3.4, "Conversion from DeviceRGB to DeviceCMYK").
            *
            *              BG2                 function or name        (Optional; PDF 1.3) Same as BG except that the value may also be the name Default, denoting the black-generation function that was in effect at the start of the page. If both BG and BG2 are present in the same graphics state parameter dictionary, BG2 shall take precedence.
            *
            *              UCR                 function                (Optional) The undercolor-removal function, which maps the interval [0.0 1.0] to the interval [−1.0 1.0] (see 10.3.4, "Conversion from DeviceRGB to DeviceCMYK").
            *
            *              UCR2                function or name        (Optional; PDF 1.3) Same as UCR except that the value may also be the name Default, denoting the undercolor-removal function that was in effect at the start of the page. 
            *                                                          If both UCR and UCR2 are present in the same graphics state parameter dictionary, UCR2 shall takeprecedence.
            *
            *              TR                  function, array, or     (Optional) The transfer function, which maps the interval [0.0 1.0] to the interval [0.0 1.0] (see 10.4, "Transfer Functions").   
            *                                  name                    The value shall be either a single function (which applies to all process colorants) or an array of four functions (which apply to the process colorants individually).
            *                                                          The name Identity may be used to represent the identity function.
            *      
            *              TR2                 function, array, or     (Optional; PDF 1.3) Same as TR except that the value may also be the name Default, denoting the transfer function that was in effect at the start of the page.
            *                                  name                    If both TR and TR2 are present in the same graphics state parameter dictionary, TR2 shall take precedence.
            *
            *              HT                  dictionary,             (Optional) The halftone dictionary or stream (see 10.5, "Halftones") or the name Default, denoting the halftone that was in effect at the start of the page.
            *                                  stream, or name
            *
            *              FL                  number                  (Optional; PDF 1.3) The flatness tolerance (see 10.6.2, "Flatness Tolerance").
            *
            *              SM                  number                  (Optional; PDF 1.3) The smoothness tolerance (see 10.6.3, "Smoothness Tolerance").
            *
            *              SA                  boolean                 (Optional) A flag specifying whether to apply automatic stroke adjustment (see 10.6.5, "Automatic Stroke Adjustment").
            *
            *              BM                  name or array           (Optional; PDF 1.4) The current blend mode to be used in the transparent imaging model (see 11.3.5, "Blend Mode" and 11.6.3, "Specifying Blending Colour Space and Blend Mode").
            *
            *              SMask               dictionary or           (Optional; PDF 1.4) The current soft mask, specifying the mask shape or mask opacity values that shall be used in the transparent imaging model (see 11.3.7.2, "Source Shape and Opacity" and 11.6.4.3, "Mask Shape and Opacity").
            *                                  name                    Although the current soft mask is sometimes referred to as a “soft clip,” altering it with the gs operator completely replaces the old value with the new one, rather than intersecting the two as is done with the current clipping path parameter (see 8.5.4, "Clipping Path Operators").
            *
            *              CA                  number                  (Optional; PDF 1.4) The current stroking alpha constant, specifying the constant shape or constant opacity value that shall be used for stroking operations in the transparent imaging model (see 11.3.7.2, "Source Shape and Opacity" and 11.6.4.4, "Constant Shape and Opacity").
            *
            *              ca                  number                  (Optional; PDF 1.4) Same as CA, but for nonstroking operations.
            *
            *              AIS                 boolean                 (Optional; PDF 1.4) The alpha source flag (“alpha is shape”), specifying whether the current soft mask and alpha constant shall beinterpreted as shape values (true) or opacity values (false).
            *
            *              TK                  boolean                 (Optional; PDF 1.4) The text knockout flag, shall determine the behaviour of overlapping glyphs within a text object in the transparent imaging model (see 9.3.8, "Text Knockout").
            *
            *EXAMPLE       The following shows two graphics state parameter dictionaries. 
            *              In the first, automatic stroke adjustment is turned on, and the dictionary includes a transfer function that inverts its value, f(x) = 1 - x. 
            *              In the second, overprint is turned off, and the dictionary includes a parabolic transfer function, f(x) = (2x - 1)2, with a sample of 21 values. 
            *              The domain of the transfer function, [0.0 1.0], is mapped to [0 20], and the range of the sample values, [0 255], is mapped to the range of the transfer function, [0.0 1.0].
            *
            *              10 0 obj                    % Page object
            *                  <</ Type / Page
            *                    / Parent 5 0 R
            *                    / Resources 20 0 R
            *                    / Contents 40 0 R
            *                  >>
            *              endobj
            *              20 0 obj                    % Resource dictionary for page
            *                  << / ProcSet[/ PDF / Text]
            *                     / Font << / F1 25 0 R >>
            *                     / ExtGState << / GS1 30 0 R
            *                                    / GS2 35 0 R
            *                                 >>
            *                  >>
            *              endobj
            *              30 0 obj                    % First graphics state parameter dictionary
            *                  << / Type / ExtGState
            *                     / SA true
            *                     / TR 31 0 R
            *                  >>
            *              endobj
            *              31 0 obj% First transfer function
            *                  << / FunctionType 0
            *                     / Domain[0.0 1.0]
            *                     / Range[0.0 1.0]
            *                     / Size 2
            *                     / BitsPerSample 8
            *                     / Length 7
            *                     / Filter / ASCIIHexDecode
            *                  >>
            *              stream
            *              01 00 >
            *              endstream
            *              endobj
            *              35 0 obj                    % Second graphics state parameter dictionary
            *                  << / Type / ExtGState
            *                     / OP false
            *                     / TR 36 0 R
            *                  >>
            *              endobj
            *              36 0 obj% Second transfer function
            *                  << / FunctionType 0
            *                     / Domain[0.0 1.0]
            *                     / Range[0.0 1.0]
            *                     / Size 21
            *                     / BitsPerSample 8
            *                     / Length 63
            *                     / Filter / ASCIIHexDecode
            *                  >>
            *              stream
            *              FF CE A3 7C 5B 3F 28 16 0A 02 00 02 0A 16 28 3F 5B 7C A3 CE FF >
            *              endstream
            *              endobj
            */

            public void getGraphicStateDictionary(ref Globals globals, ref PDF_Graphics_State_Parameter_Dictionary pdf_graphics_state_parameter_dictionary, ref string stream_hexa, int offset)
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////
                /// SET TOOLS AND CONSTANTS
                /////////////////////////////////////////////////////////////////////////////////////////////////

                tools Tools = new tools();

                DELIMITER_CHARACTERS delimiter_characters = new DELIMITER_CHARACTERS();
                WHITE_SPACE_CHARACTERS white_space_characters = new WHITE_SPACE_CHARACTERS();

                string L_del = delimiter_characters.LESS_THAN_SIGN.Hexadecimal + '-' + delimiter_characters.LESS_THAN_SIGN.Hexadecimal;
                string R_del = delimiter_characters.GREATER_THAN_SIGN.Hexadecimal + '-' + delimiter_characters.GREATER_THAN_SIGN.Hexadecimal;

                string SL = delimiter_characters.SOLIDUS.Hexadecimal;

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// CHECK THAT WE HAVE A GRAPHIC STATE DICTIONARY IN THIS OBJECT
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                string obj_stream_hexa = Tools.getOBJstream(ref stream_hexa, offset);

                string teste = Tools.lineHexToString(obj_stream_hexa);

                offset = 0;
                
                int type_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.Type.key), offset);
                
                int name_idx = stream_hexa.IndexOf(SL, type_idx + 1);

                int name_after_idx = obj_stream_hexa.IndexOf(SL, name_idx + 1);

                string name_hexa = obj_stream_hexa.Substring(name_idx, name_after_idx - name_idx);

                string name_string = Tools.lineHexToString(name_hexa);

                name_string = name_string.Trim(' ');

                if (String.Compare(name_string, pdf_graphics_state_parameter_dictionary.Type.value) != 0)
                {
                    // ERROR - IT FOUND THE WRONG DICTIONARY
                }

                int key_idx = -1;

                int ERROR_ID = 0;
                string ERROR_MSG = string.Empty;
                
                //////////////////////////////////////////////////////////////////////////
                //  /LW                  number (see 8.4.3.2, "Line Width").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.LW.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.LW.found = true;

                    Tools.getNumberInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.LW.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.LW.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.LW.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /LC                  integer (see 8.4.3.3, "Line Cap Style").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.LC.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.LC.found = true;

                    Tools.getIntegerInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.LC.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.LC.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.LC.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /LJ                  integer (see 8.4.3.4, "Line Join Style").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.LJ.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.LJ.found = true;

                    Tools.getIntegerInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.LJ.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.LJ.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.LJ.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /ML                  number (see 8.4.3.5, "Miter Limit").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.ML.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.ML.found = true;

                    Tools.getNumberInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.ML.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.ML.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.ML.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /D                   array (see 8.4.3.6, "Line Dash Pattern").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.D.key), offset);
                
                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.D.found = true;

                    Tools.getArrayInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.D.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.D.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.D.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /RI                  name (see 8.6.5.8, "Rendering Intents").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.RI.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.RI.found = true;

                    Tools.getNameInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.RI.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.RI.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.RI.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /OP                  boolean (see 8.6.7, "Overprint Control"). 
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.OP.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.OP.found = true;

                    Tools.getBooleanInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.OP.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.OP.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.OP.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /op                  boolean (see 8.6.7, "Overprint Control").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.op.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.op.found = true;

                    Tools.getBooleanInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.op.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.op.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.op.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /OPM                 integer (see 8.6.7, "Overprint Control").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.OPM.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.OPM.found = true;

                    Tools.getIntegerInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.OPM.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.OPM.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.OPM.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /Font                array (see 9.3, "Text State Parameters and Operators").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.Font.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.Font.found = true;

                    Tools.getArrayInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.Font.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.Font.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.Font.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /BG                  function (see 10.3.4, "Conversion from DeviceRGB to DeviceCMYK").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.BG.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.BG.found = true;

                    Tools.getFunctionInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.BG.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.BG.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.BG.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /BG2                 function or name.
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.BG2.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.BG2.found = true;

                    int kind_of_obj = Tools.identifyObject(ref globals, ref stream_hexa, key_idx);

                    if (kind_of_obj == globals.Kinds_of_Objects.Functions)
                    {
                        pdf_graphics_state_parameter_dictionary.BG2.kind_of_obj = globals.Kinds_of_Objects.Functions;

                        Tools.getFunctionInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.BG2.value_x, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.BG2.key).Length, ref ERROR_ID, ref ERROR_MSG);

                    }
                    else if (kind_of_obj == globals.Kinds_of_Objects.Name)
                    {
                        pdf_graphics_state_parameter_dictionary.BG2.kind_of_obj = globals.Kinds_of_Objects.Name;

                        Tools.getNameInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.BG2.value_y, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.BG2.key).Length, ref ERROR_ID, ref ERROR_MSG);
                    }
                    else
                    {
                        //SHOW ERROR
                    }
                    
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.BG2.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /UCR                 function (see 10.3.4, "Conversion from DeviceRGB to DeviceCMYK").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.UCR.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.UCR.found = true;

                    Tools.getFunctionInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.UCR.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.UCR.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.UCR.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /TR                  function, array or name (see 10.4, "Transfer Functions").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TR.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.TR.found = true;

                    int kindOfObject = Tools.identifyObject(ref globals, ref stream_hexa, key_idx);

                    if (kindOfObject == globals.Kinds_of_Objects.Functions)
                    {
                        pdf_graphics_state_parameter_dictionary.TR.kind_of_obj = globals.Kinds_of_Objects.Functions;

                        PDF_Function pdf_function = new PDF_Function();

                        Tools.getFunctionInDictionaryHex(ref pdf_function, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TR.key).Length, ref ERROR_ID, ref ERROR_MSG);

                        pdf_graphics_state_parameter_dictionary.TR.value_x.Add(pdf_function);

                    }
                    else if (kindOfObject == globals.Kinds_of_Objects.Array)
                    {
                        pdf_graphics_state_parameter_dictionary.TR.kind_of_obj = globals.Kinds_of_Objects.Array;

                        Tools.getFunctionArrayInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.TR.value_x, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TR.key).Length, ref ERROR_ID, ref ERROR_MSG);
                    }
                    else if (kindOfObject == globals.Kinds_of_Objects.Name)
                    {
                        pdf_graphics_state_parameter_dictionary.TR.kind_of_obj = globals.Kinds_of_Objects.Name;

                        Tools.getNameInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.TR.value_y, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TR.key).Length, ref ERROR_ID, ref ERROR_MSG);
                    }
                    else
                    {
                        //SHOW ERROR
                    }

                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.TR.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /TR2                 function, array, or name.
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TR2.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.TR2.found = true;

                    int kindOfObject = Tools.identifyObject(ref globals, ref stream_hexa, key_idx);

                    if (kindOfObject == globals.Kinds_of_Objects.Functions)
                    {
                        pdf_graphics_state_parameter_dictionary.TR2.kind_of_obj = globals.Kinds_of_Objects.Functions;

                        PDF_Function pdf_function = new PDF_Function();

                        Tools.getFunctionInDictionaryHex(ref pdf_function, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TR2.key).Length, ref ERROR_ID, ref ERROR_MSG);

                        pdf_graphics_state_parameter_dictionary.TR2.value_x.Add(pdf_function);
                    }
                    else if (kindOfObject == globals.Kinds_of_Objects.Array)
                    {
                        pdf_graphics_state_parameter_dictionary.TR2.kind_of_obj = globals.Kinds_of_Objects.Array;

                        Tools.getFunctionArrayInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.TR2.value_x, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TR2.key).Length, ref ERROR_ID, ref ERROR_MSG);
                    }
                    else if (kindOfObject == globals.Kinds_of_Objects.Name)
                    {
                        pdf_graphics_state_parameter_dictionary.TR2.kind_of_obj = globals.Kinds_of_Objects.Name;

                        Tools.getNameInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.TR2.value_y, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TR2.key).Length, ref ERROR_ID, ref ERROR_MSG);
                    }
                    else
                    {
                        //SHOW ERROR
                    }

                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.TR2.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /HT                  dictionary, stream or name (see 10.5, "Halftones").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.HT.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.HT.found = true;

                  
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.HT.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /FL                  number (see 10.6.2, "Flatness Tolerance").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.FL.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.FL.found = true;

                    Tools.getNumberInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.FL.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.FL.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.FL.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /SM                  number (see 10.6.3, "Smoothness Tolerance").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.SM.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.SM.found = true;

                    Tools.getNumberInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.SM.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.SM.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.SM.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /SA                  boolean (see 10.6.5, "Automatic Stroke Adjustment").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.SA.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.SA.found = true;

                    Tools.getBooleanInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.SA.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.SA.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.SA.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /BM                  name or array (see 11.3.5, "Blend Mode" and 11.6.3, "Specifying Blending Colour Space and Blend Mode").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.BM.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.BM.found = true;

                    int kindOfObject = Tools.identifyObject(ref globals, ref stream_hexa, key_idx);

                    if (kindOfObject == globals.Kinds_of_Objects.Name)
                    {
                        pdf_graphics_state_parameter_dictionary.BM.kind_of_obj = globals.Kinds_of_Objects.Name;

                        Tools.getNameInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.BM.value_x, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.BM.key).Length, ref ERROR_ID, ref ERROR_MSG);

                    }
                    else if (kindOfObject == globals.Kinds_of_Objects.Array)
                    {
                        pdf_graphics_state_parameter_dictionary.BM.kind_of_obj = globals.Kinds_of_Objects.Array;

                        Tools.getArrayInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.BM.value_y, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.BM.key).Length, ref ERROR_ID, ref ERROR_MSG);
                    }
                    else
                    {
                        //SHOW ERROR
                    }
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.BM.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /SMask               dictionary or name (see 11.3.7.2, "Source Shape and Opacity" and 11.6.4.3, "Mask Shape and Opacity"), (see 8.5.4, "Clipping Path Operators").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.SMask.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.SMask.found = true;

                    int kindOfObject = Tools.identifyObject(ref globals, ref stream_hexa, key_idx);

                    if (kindOfObject == globals.Kinds_of_Objects.Dictionary)
                    {
                        pdf_graphics_state_parameter_dictionary.SMask.kind_of_obj = globals.Kinds_of_Objects.Dictionary;

                        Transparency.Soft_Masks soft_masks = new Transparency.Soft_Masks();

                        soft_masks.getSoftMask(ref pdf_graphics_state_parameter_dictionary.SMask.value_x, ref obj_stream_hexa, key_idx);
                    }
                    else if (kindOfObject == globals.Kinds_of_Objects.Name)
                    {
                        pdf_graphics_state_parameter_dictionary.SMask.kind_of_obj = globals.Kinds_of_Objects.Name;

                        Tools.getNameInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.SMask.value_y, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.SMask.key).Length, ref ERROR_ID, ref ERROR_MSG);
                    }
                    else
                    {
                        //SHOW ERROR
                    }

                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.SMask.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /CA                  number (see 11.3.7.2, "Source Shape and Opacity" and 11.6.4.4, "Constant Shape and Opacity").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.CA.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.CA.found = true;

                    Tools.getNumberInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.CA.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.CA.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.CA.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /ca                  number
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.ca.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.ca.found = true;

                    Tools.getNumberInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.ca.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.ca.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.ca.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /AIS                 boolean
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.AIS.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.AIS.found = true;

                    Tools.getBooleanInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.AIS.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.AIS.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.AIS.found = false;
                }

                //////////////////////////////////////////////////////////////////////////
                //  /TK                  boolean (see 9.3.8, "Text Knockout").
                //////////////////////////////////////////////////////////////////////////

                key_idx = obj_stream_hexa.IndexOf(Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TK.key), offset);

                ERROR_ID = 0;
                ERROR_MSG = string.Empty;

                if (key_idx != -1)
                {
                    pdf_graphics_state_parameter_dictionary.TK.found = true;

                    Tools.getBooleanInDictionaryHex(ref pdf_graphics_state_parameter_dictionary.TK.value, ref obj_stream_hexa, key_idx, Tools.lineStringToHex(pdf_graphics_state_parameter_dictionary.TK.key).Length, ref ERROR_ID, ref ERROR_MSG);
                }
                else
                {
                    pdf_graphics_state_parameter_dictionary.TK.found = false;
                }


            }

        }

        //8.5 Path Construction and Painting
        public class Path_Construction_and_Painting
        { 
            /*8.5.1 General
            *
            *Paths define shapes, trajectories, and regions of all sorts. They shall be used to draw lines, define the shapes of filled areas, and specify boundaries for clipping other graphics. 
            *The graphics state shall include a current clipping path that shall define the clipping boundary for the current page. At the beginning of each page, the clipping path shall be initialized to include the entire page.
            *
            *A path shall be composed of straight and curved line segments, which may connect to one another or may be disconnected.
            *A pair of segments shall be said to connect only if they are defined consecutively, with the second segment starting where the first one ends. 
            *Thus, the order in which the segments of a path are defined shall be significant.Nonconsecutive segments that meet or intersect fortuitously shall not be considered to connect.
            *
            *NOTE      A path is made up of one or more disconnected subpaths, each comprising a sequence of connected segments.The topology of the path is unrestricted: it may be concave or convex, may contain multiple subpaths representing disjoint areas, and may intersect itself in arbitrary ways.
            *
            *The h operator explicitly shall connect the end of a subpath back to its starting point; such a subpath is said to be closed.
            *A subpath that has not been explicitly closed is said to be open.
            *
            *As discussed in 8.2, "Graphics Objects", a path object is defined by a sequence of operators to construct the path, followed by one or more operators to paint the path or to use it as a clipping boundary. 
            *PDF path operators fall into three categories:
            *
            *  •   Path construction operators(8.5.2, "Path Construction Operators") define the geometry of a path.A path is constructed by sequentially applying one or more of these operators.
            *
            *  •   Path-painting operators (8.5.3, "Path-Painting Operators") end a path object, usually causing the object to be painted on the current page in any of a variety of ways.
            *
            *  •   Clipping path operators (8.5.4, "Clipping Path Operators"), invoked immediately before a path-painting operator, cause the path object also to be used for clipping of subsequent graphics objects.
            */

            /*8.5.2 Path Constructuion Operators
            *
            */
                
                /*8.5.2.1 General
                *
                *A page description shall begin with an empty path and shall build up its definition by invoking one or more path construction operators to add segments to it. 
                *The path construction operators may be invoked in any sequence, but the first one invoked shall be m or re to begin a new subpath.
                *The path definition may conclude with the application of a path-painting operator such as S, f, or b(see 8.5.3, "Path-Painting Operators"); this operator may optionally be preceded by one of the clipping path operators W or W*(8.5.4, "Clipping Path Operators").
                *
                *NOTE      Note that the path construction operators do not place any marks on the page; only the painting operators do that.A path definition is not complete until a path - painting operator has been applied to it.
                *
                *The path currently under construction is called the current path. 
                *In PDF(unlike PostScript), the current path is not part of the graphics state and is not saved and restored along with the other graphics state parameters.
                *PDF paths shall be strictly internal objects with no explicit representation.After the current path has been painted, it shall become no longer defined; there is then no current path until a new one is begun with the m or re operator.
                *
                *The trailing endpoint of the segment most recently added to the current path is referred to as the current point.If the current path is empty, the current point shall be undefined.
                *Most operators that add a segment to the current path start at the current point; if the current point is undefined, an error shall be generated.
                *
                *Table 59 shows the path construction operators.All operands shall be numbers denoting coordinates in user space.
                *
                *Table 59 - Path Construction Operators
                *
                *          [Operands]              [Operator]                  [Description]
                *
                *          x y                     m                           Begin a new subpath by moving the current point to coordinates (x, y), omitting any connecting line segment. If the previous path construction operator in the current path was also m, the new m overrides it; no vestige of the previous m operation remains in the path.
                *
                *          x y                     I (lowercase L)             Append a straight line segment from the current point to the point (x, y). The new current point shall be (x, y).
                *
                *          x1 y1 x2 y2 x3 y3       c                           Append a cubic Bézier curve to the current path. The curve shall extend from the current point to the point (x3, y3), using (x1, y1) and (x2, y2) as the Bézier control points (see 8.5.2.2, "Cubic Bézier Curves"). The new current point shall be (x3, y3).
                *
                *          x2 y2 x3 y3             v                           Append a cubic Bézier curve to the current path. The curve shall extend from the current point to the point (x3, y3), using the current point and (x2, y2) as the Bézier control points (see 8.5.2.2, "Cubic Bézier Curves"). The new current point shall be (x3, y3).
                *
                *          x1 y1 x3 y3             y                           Append a cubic Bézier curve to the current path. The curve shall extend from the current point to the point (x3, y3), using (x1, y1) and (x3, y3) as the Bézier control points (see 8.5.2.2, "Cubic Bézier Curves"). The new current point shall be (x3, y3).
                *
                *          -                       h                           Close the current subpath by appending a straight line segment from the current point to the starting point of the subpath. If the current subpath is already closed, h shall donothing.
                *                                                              This operator terminates the current subpath. Appending another segment to the current path shall begin a new subpath, even if the new segment begins at the endpoint reached by the h operation.
                *
                *          x y width height        re                          Append a rectangle to the current path as a complete subpath, with lower-left corner (x, y) and dimensions widthand height in user space. 
                *                                                              
                *                                                              The operation
                *  
                *                                                              x y width height re
                *  
                *                                                              is equivalent to
                *          
                *                                                              x y m
                *
                *                                                              (x + width) y |
                *                                                              (x + width) (y + height) |
                *                                                              (x (y + height) |
                *                                                              h
                */

                /*8.5.2.2   Cubic Bézier Curves
                *
                *Curved path segments shall be specified as cubic Bézier curves. Such curves shall be defined by four points: the two endpoints(the current point P0 and the final point P3) and two control points P1 and P2.
                *Given the coordinates of the four points, the curve shall be generated by varying the parameter t from 0.0 to 1.0 in the following equation:
                *
                * (see Equation on page 133)
                *
                *When t = 0.0, the value of the function R(t) coincides with the current point P0; when t = 1.0, R(t) coincides with the final point P3. Intermediate values of t generate intermediate points along the curve. 
                *The curve does not, in general, pass through the two control points P1 and P2.
                *
                *NOTE 1    Cubic Bézier curves have two useful properties:
                *          The curve can be very quickly split into smaller pieces for rapid rendering.
                *          The curve is contained within the convex hull of the four points defining the curve, most easily visualized as the polygon obtained by stretching a rubber band around the outside of the four points.
                *          This property allows rapid testing of whether the curve lies completely outside the visible region, and hence does not have to be rendered.
                *
                *NOTE 2    The Bibliography lists several books that describe cubic Bézier curves in more depth.
                *
                *The most general PDF operator for constructing curved path segments is the c operator, which specifies the coordinates of points P1, P2, and P3 explicitly, as shown in Figure 16 in Annex L. (The starting point, P0, is defined implicitly by the current point.)
                *
                *(see Figure 16 - Cubic Bézier Curve Generated by the c Operator, on page 134)
                *
                *Two more operators, v and y, each specify one of the two control points implicitly (see Figure 17 in Annex L). 
                *In both of these cases, one control point and the final point of the curve shall be supplied as operands; the other control point shall be implied:
                *
                *  •   For the v operator, the first control point shall coincide with initial point of the curve.
                *
                *  •   For the y operator, the second control point shall coincide with final point of the curve.
                *
                *(see Figure 17 - Cubic Bézier Curves Generated by the v and y Operators
                */

            /*8.5.3 Path-Painting Operators
                */

                /*8.5.3.1 General
                *
                *The path - painting operators end a path object, causing it to be painted on the current page in the manner that the operator specifies.
                *The principal path - painting operators shall be S(for stroking) and f(for filling).
                *Variants of these operators combine stroking and filling in a single operation or apply different rules for determining the area to be filled. 
                *Table 60 lists all the path-painting operators.
                *
                *Table 60 - Path-Painting Operators
                *
                *              [Operands]              [Operator]                  [Description]
                *
                *              -                       S                           Stroke the path.
                *
                *              -                       s                           Close and stroke the path. This operator shall have the same effect as the sequence h S.
                *
                *              -                       f                           Fill the path, using the nonzero winding number rule to determine the region to fill (see 8.5.3.3.2, "Nonzero Winding Number Rule"). Any subpaths that are open shall be implicitly closed before being filled.
                *
                *              -                       F                           Equivalent to f; included only for compatibility. Although PDF readerapplications shall be able to accept this operator, PDF writer applications should use f instead.
                *
                *              -                       f*                          Fill the path, using the even-odd rule to determine the region to fill (see 8.5.3.3.3, "Even-Odd Rule").
                *
                *              -                       B                           Fill and then stroke the path, using the nonzero winding number rule to determine the region to fill. This operator shall produce the same result as constructing two identical path objects, painting the first with f and the second with S.
                *
                *                                                                  NOTE        The filling and stroking portions of the operation consult different values of several graphics state parameters, such as the current colour. See also 11.7.4.4, "Special Path-Painting Considerations".
                *
                *              -                       B*                          Fill and then stroke the path, using the even-odd rule to determine the region to fill. This operator shall produce the same result as B, except that the path is filled as if with f* instead of f. See also 11.7.4.4, "Special Path-Painting Considerations".
                *
                *              -                       b                           Close, fill, and then stroke the path, using the nonzero winding number rule to determine the region to fill. This operator shall have the same effect as the sequence h B. See also 11.7.4.4, "Special Path-Painting Considerations".
                *
                *              -                       b*                          Close, fill, and then stroke the path, using the even-odd rule to determine the region to fill. This operator shall have the same effect as the sequence h B*. See also 11.7.4.4, "Special Path-Painting Considerations".
                *
                *              -                       n                           End the path object without filling or stroking it. This operator shall be a path-painting no-op, used primarily for the side effect of changing the current clipping path (see 8.5.4, "Clipping Path Operators").
                *
                */

                /*8.5.3.2   Stroking
                *
                *The S operator shall paint a line along the current path. The stroked line shall follow each straight or curved segment in the path, centred on the segment with sides parallel to it. 
                *Each of the path’s subpaths shall be treated separately.
                *
                *The results of the S operator shall depend on the current settings of various parameters in the graphics state(see 8.4, "Graphics State", for further information on these parameters):
                *
                *  •   The width of the stroked line shall be determined by the current line width parameter(8.4.3.2, "Line Width").
                *
                *  •   The colour or pattern of the line shall be determined by the current colour and colour space for stroking operations.
                *
                *  •   The line may be painted either solid or with a dash pattern, as specified by the current line dash pattern (see 8.4.3.6, "Line Dash Pattern").
                *
                *  •   If a subpath is open, the unconnected ends shall be treated according to the current line cap style, which may be butt, rounded, or square(see 8.4.3.3, "Line Cap Style").
                *
                *  •   Wherever two consecutive segments are connected, the joint between them shall be treated according to the current line join style, which may be mitered, rounded, or beveled(see 8.4.3.4, "Line Join Style").Mitered joins shall be subject to the current miter limit(see 8.4.3.5, "Miter Limit").
                *
                *Points at which unconnected segments happen to meet or intersect receive no special treatment. In particular, using an explicit l operator to give the appearance of closing a subpath, rather than using h, may result in a messy corner, because line caps are applied instead of a line join.
                *
                *  •   The stroke adjustment parameter (PDF 1.2) specifies that coordinates and line widths be adjusted automatically to produce strokes of uniform thickness despite rasterization effects(see 10.6.5, "Automatic Stroke Adjustment").
                *
                *If a subpath is degenerate(consists of a single-point closed path or of two or more points at the same coordinates), the S operator shall paint it only if round line caps have been specified, producing a filled circle centered at the single point.
                *If butt or projecting square line caps have been specified, S shall produce no output, because the orientation of the caps would be indeterminate.
                *This rule shall apply only to zero-length subpaths of the path being stroked, and not to zero-length dashes in a dash pattern.In the latter case, the line caps shall always be painted, since their orientation is determined by the direction of the underlying path. 
                *A single-point open subpath (specified by a trailing m operator) shall produce no output.
                */

                /*8.5.3.3 Filling
                */

                    /*8.5.3.3.1 General
                *
                *The f operator shall use the current nonstroking colour to paint the entire region enclosed by the current path. 
                *If the path consists of several disconnected subpaths, f shall paint the insides of all subpaths, considered together. 
                *Any subpaths that are open shall be implicitly closed before being filled.
                *
                *If a subpath is degenerate (consists of a single - point closed path or of two or more points at the same coordinates), f shall paint the single device pixel lying under that point; the result is device - dependent and not generally useful. 
                *A single-point open subpath(specified by a trailing m operator) shall produce no output.
                *
                *For a simple path, it is intuitively clear what region lies inside. However, for a more complex path, it is not always obvious which points lie inside the path.For more detailed information, see 10.6.4, “Scan Conversion Rules“.
                *
                *EXAMPLE   A path that intersects itself or has one subpath that encloses another.
                *
                *The path machinery shall use one of two rules for determining which points lie inside a path: the nonzero winding number rule and the even - odd rule, both discussed in detail below.
                *The nonzero winding number rule is more versatile than the even - odd rule and shall be the standard rule the f operator uses.
                *Similarly, the Woperator shall use this rule to determine the inside of the current clipping path.
                *The even - odd rule is occasionally useful for special effects or for compatibility with other graphics systems; the f*and W* operators invoke this rule.
                */

                    /*8.5.3.3.2 Nonzero Winding Number Rule
                *
                *The nonzero winding number rule determines whether a given point is inside a path by conceptually drawing a ray from that point to infinity in any direction and then examining the places where a segment of the path crosses the ray. 
                *Starting with a count of 0, the rule adds 1 each time a path segment crosses the ray from left to right and subtracts 1 each time a segment crosses from right to left.
                *After counting all the crossings, if the result is 0, the point is outside the path; otherwise, it is inside.
                *
                *The method just described does not specify what to do if a path segment coincides with or is tangent to the chosen ray. Since the direction of the ray is arbitrary, the rule simply chooses a ray that does not encounter such problem intersections.
                *
                *For simple convex paths, the nonzero winding number rule defines the inside and outside as one would intuitively expect.
                *The more interesting cases are those involving complex or self-intersecting paths like the ones shown in Figure 18 in Annex L. 
                *For a path consisting of a five - pointed star, drawn with five connected straight line segments intersecting each other, the rule considers the inside to be the entire area enclosed by the star, including the pentagon in the centre. 
                *For a path composed of two concentric circles, the areas enclosed by both circles are considered to be inside, provided that both are drawn in the same direction.
                *If the circles are drawn in opposite directions, only the doughnut shape between them is inside, according to the rule; the doughnut hole is outside.
                *
                *(see Figure 18 - Nonzero Winding Number Rule, on page 137)
                */

                    /*8.5.3.3.3 Even-Odd- Rule
                *
                *An alternative to the nonzero winding number rule is the even-odd rule. This rule determines whether a point is inside a path by drawing a ray from that point in any direction and simply counting the number of path segments that cross the ray, regardless of direction. 
                *If this number is odd, the point is inside; if even, the point is outside. This yields the same results as the nonzero winding number rule for paths with simple shapes, but produces different results for more complex shapes.
                *
                *Figure 19 shows the effects of applying the even - odd rule to complex paths. For the five - pointed star, the rule considers the triangular points to be inside the path, but not the pentagon in the centre. 
                *For the two concentric circles, only the doughnut shape between the two circles is considered inside, regardless of the directions in which the circles are drawn.
                *
                *(see Figure 19 - Even-Odd Rule, on page 137)
                *
                */

             /*8.5.4 Clipping Path Operators
                *
                *The graphics state shall contain a current clipping path that limits the regions of the page affected by painting operators. 
                *The closed subpaths of this path shall define the area that can be painted. Marks falling inside this area shall be applied to the page; those falling outside it shall not be. Sub-clause 8.5.3.3, "Filling" discusses precisely what shall be considered to be inside a path.
                *
                *In the context of the transparent imaging model (PDF 1.4), the current clipping path constrains an object’s shape (see 11.2, "Overview of Transparency"). 
                *The effective shape is the intersection of the object’s intrinsic shape with the clipping path; the source shape value shall be 0.0 outside this intersection. 
                *Similarly, the shape of a transparency group (defined as the union of the shapes of its constituent objects) shall be influenced both by the clipping path in effect when each of the objects is painted and by the one in effect at the time the group’s results are painted onto its backdrop.
                *
                *The initial clipping path shall include the entire page.A clipping path operator (W or W*, shown in Table 61) may appear after the last path construction operator and before the path-painting operator that terminates a path object.
                *Although the clipping path operator appears before the painting operator, it shall not alter the clipping path at the point where it appears. 
                *Rather, it shall modify the effect of the succeeding painting operator. 
                *After the path has been painted, the clipping path in the graphics state shall be set to the intersection of the current clipping path and the newly constructed path.
                *
                *Table 61 - Clipping Path Operators
                *
                *          [Operands]          [Operator]              [Description]
                *
                *          -                   W                       Modify the current clipping path by intersecting it with the current path, using the nonzero winding number rule to determine which regions lie inside the clipping path.
                *
                *          -                   W*                      Modify the current clipping path by intersecting it with the current path, using the even-odd rule to determine which regions lie inside the clipping path.
                *
                *
                *NOTE 1    In addition to path objects, text objects may also be used for clipping; see 9.3.6, "Text Rendering Mode".
                *
                *The n operator (see Table 60) is a no - op path - painting operator; it shall cause no marks to be placed on the page, but can be used with a clipping path operator to establish a new clipping path.
                *That is, after a path has been constructed, the sequence W n shall intersect that path with the current clipping path and shall establish a new clipping path.
                *
                *NOTE 2    There is no way to enlarge the current clipping path or to set a new clipping path without reference to the current one.
                *          However, since the clipping path is part of the graphics state, its effect can be localized to specific graphics objects by enclosing the modification of the clipping path and the painting of those objects between a pair of q and Q operators(see 8.4.2, "Graphics State Stack").
                *          Execution of the Q operator causes the clipping path to revert to the value that was saved by the q operator before the clipping path was modified.
                */

        }

        //8.6 Colour Spaces
        public class Colour_Spaces
        {
            /*8.6.1 General
            *
            *PDF includes facilities for specifying the colours of graphics objects to be painted on the current page. The colour facilities are divided into two parts:
            *
            *  •   Colour specification. A conforming writer may specify abstract colours in a device-independent way. Colours may be described in any of a variety of colour systems, or colour spaces. Some colour spaces are related to device colour representation (grayscale, RGB, CMYK), others to human visual perception (CIE-based). 
            *      Certain special features are also modelled as colour spaces: patterns, colour mapping, separations, and high-fidelity and multitone colour.
            *
            *  •   Colour rendering. A conforming reader shall reproduce colours on the raster output device by a multiple-step process that includes some combination of colour conversion, gamma correction, halftoning, and scan conversion.
            *      Some aspects of this process use information that is specified in PDF. However, unlike the facilities for colour specification, the colour-rendering facilities are device-dependent and should not be included in a page description.
            *
            *Figure 20 and Figure 21 illustrate the division between PDF’s (device-independent) colour specification and (device-dependent) colour-rendering facilities.
            *This sub-clause describes the colour specification features, covering everything that PDF documents need to specify colours. The facilities for controlling colour rendering
            *are described in clause 10, "Rendering"; a conforming writer should use these facilities only to configure or calibrate an output device or to achieve special device-dependent effects.
            */

            /*8.6.2 Colour Values
            *
            *As described in 8.5.3, "Path-Painting Operators", marks placed on the page by operators such as f and S shall have a colour that is determined by the current colour parameter of the graphics state. 
            *A colour value consists of one or more colour components, which are usually numbers. A gray level shall be specified by a single number ranging from 0.0 (black) to 1.0 (white). 
            *Full colour values may be specified in any of several ways; a common method uses three numeric values to specify red, green, and blue components.
            *
            *Colour values shall be interpreted according to the current colour space, another parameter of the graphics state. 
            *A PDF content stream first selects a colour space by invoking the CS operator (for the stroking colour) or the cs operator (for the nonstroking colour).
            *It then selects colour values within that colour space with the SC operator (stroking) or the sc operator (nonstroking).
            *There are also convenience operators—G, g, RG, rg, K, and k—that select both a colour space and a colour value within it in a single step. Table 74 lists all the colour-setting operators.
            *
            *Sampled images(see 8.9, "Images") specify the colour values of individual samples with respect to a colour space designated by the image object itself. 
            *While these values are independent of the current colour space and colour parameters in the graphics state, all later stages of colour processing shall treat them in exactly the same way as colour values 
            * specified with the SC or sc operator.
            */

            /*8.6.3 Colour Space Families
            *
            *Colour spaces are classified into colour space families. 
            *Spaces within a family share the same general characteristics; they shall be distinguished by parameter values supplied at the time the space is specified. 
            *The families fall into three broad categories:
            *
            *  •   Device colour spaces directly specify colours or shades of gray that the output device shall produce. 
            *      They provide a variety of colour specification methods, including grayscale, RGB(red - green - blue), and CMYK(cyan-magenta - yellow - black), corresponding to the colour space families DeviceGray, DeviceRGB, and DeviceCMYK. 
            *      Since each of these families consists of just a single colour space with no parameters, they may be referred to as the DeviceGray, DeviceRGB, and DeviceCMYK colour spaces.
            *
            *  •   CIE - based colour spaces shall be based on an international standard for colour specification created by the Commission Internationale de l’Éclairage(International Commission on Illumination).
            *      These spaces specify colours in a way that is independent of the characteristics of any particular output device. Colour space families in this category include CalGray, CalRGB, Lab, and ICCBased.
            *      Individual colour spaces within these families shall be specified by means of dictionaries containing the parameter values needed to define the space.
            *
            *  •   Special colour spaces add features or properties to an underlying colour space.
            *      They include facilities for patterns, colour mapping, separations, and high - fidelity and multitone colour.
            *      The corresponding colour space families are Pattern, Indexed, Separation, and DeviceN.
            *      Individual colour spaces within these families shall be specified by means of additional parameters.
            *
            *Table 62 summarizes the colour space families in PDF.
            *
            *Table 62 - Colour Space Families
            *
            *          [Device]                [CIE-based]             [Special]
            *
            *          DeviceGray (PDF 1.1)    CalGray (PDF 1.1)       Indexed (PDF 1.1)
            *
            *          DeviceRGB (PDF 1.1)     CalRGB (PDF 1.1)        Pattern (PDF 1.2)
            *
            *          DeviceCMYK (PDF 1.1)    Lab (PDF 1.1)           Separation (PDF 1.2)
            *
            *                                  ICCBased (PDF 1.3)      DeviceN (PDF 1.3)
            *
            *
            *(see Figure 20 - Colour Specification, on page 140)
            *
            *(see Figure 21 - Colour Rendering, on page 141)
            *
            *A colour space shall be defined by an array object whose first element is a name object identifying the colour space family. 
            *The remaining array elements, if any, are parameters that further characterize the colour space; their number and types vary according to the particular family. 
            *For families that do not require parameters, the colour space may be specified simply by the family name itself instead of an array.
            *
            *A colour space shall be specified in one of two ways:
            *
            *  •   Within a content stream, the CS or cs operator establishes the current colour space parameter in the graphics state.
            *      The operand shall always be name object, which either identifies one of the colour spaces that need no additional parameters(DeviceGray, DeviceRGB, DeviceCMYK, or some cases of Pattern) 
            *      or shall be used as a key in the ColorSpace subdictionary of the current resource dictionary(see 7.8.3, "Resource Dictionaries"). In the latter case, the value of the dictionary entry in turn shall be a colour space array or name. 
            *      A colour space array shall never be inline within a content stream.
            *
            *  •   Outside a content stream, certain objects, such as image XObjects, shall specify a colour space as an explicit parameter, often associated with the key ColorSpace. 
            *      In this case, the colour space array or name shall always be defined directly as a PDF object, not by an entry in the ColorSpace resource subdictionary. 
            *      This convention also applies when colour spaces are defined in terms of other colour spaces.
            *
            *The following operators shall set the current colour space and current colour parameters in the graphics state:
            *
            *  •   CS shall set the stroking colour space; cs shall set the nonstroking colour space.
            *
            *  •   SC and SCN shall set the stroking colour; sc and scn shall set the nonstroking colour. 
            *      Depending on the colour space, these operators shall have one or more operands, each specifying one component of the colour value.
            *
            *  •   G, RG, and K shall set the stroking colour space implicitly and the stroking colour as specified by the operands; g, rg, and k do the same for the nonstroking colour space and colour.
            */

            /*8.6.4 Device Colour Spaces
            *
            */
                
                /*8.6.4.1 General
                *
                *The device colour spaces enable a page description to specify colour values that are directly related to their representation on an output device.
                *Colour values in these spaces map directly(or by simple conversions) to the application of device colorants, such as quantities of ink or intensities of display phosphors.
                *This enables a conforming writer to control colours precisely for a particular device, but the results might not be consistent from one device to another.
                *
                *Output devices form colours either by adding light sources together or by subtracting light from an illuminating source.
                *Computer displays and film recorders typically add colours; printing inks typically subtract them.
                *These two ways of forming colours give rise to two complementary methods of colour specification, called additive and subtractive colour(see Figure L.1 in Annex L).
                *The most widely used forms of these two types of colour specification are known as RGB and CMYK, respectively, for the names of the primary colours on which they are based.
                *They correspond to the following device colour spaces:
                *
                *  •   DeviceGray controls the intensity of achromatic light, on a scale from black to white.
                *
                *  •   DeviceRGB controls the intensities of red, green, and blue light, the three additive primary colours used in displays.
                *
                *  •   DeviceCMYK controls the concentrations of cyan, magenta, yellow, and black inks, the four subtractive process colours used in printing.
                *
                *NOTE      Although the notion of explicit colour spaces is a PDF 1.1 feature, the operators for specifying colours in the device colour spaces—G, g, RG, rg, K, and k—are available in all versions of PDF. 
                *          Beginning with PDF 1.2, colours specified in device colour spaces can optionally be remapped systematically into other colour spaces; see 8.6.5.6, "Default Colour Spaces".
                *
                *In the transparent imaging model (PDF 1.4), the use of device colour spaces is subject to special treatment within a transparency group whose group colour space is CIE-based (see 11.4, "Transparency Groups" and 11.6.6, "Transparency Group XObjects"). 
                *In particular, the device colour space operators should be used only if device colour spaces have been remapped to CIE-based spaces by means of the default colour space mechanism. 
                *Otherwise, the results are implementation-dependent and unpredictable.
                */

                /*8.6.4.2 DeviceGray Colour Space
                *
                *Black, white, and intermediate shades of gray are special cases of full colour. A grayscale value shall be represented by a single number in the range 0.0 to 1.0, where 0.0 corresponds to black, 1.0 to white, and intermediate values to different gray levels.
                *
                *EXAMPLE   This example shows alternative ways to select the DeviceGray colour space and a specific gray level within that space for stroking operations.
                *
                *          / DeviceGray CS         % Set DeviceGray colour space
                *          gray SC                 % Set gray level
                *          gray G                  % Set both in one operation
                *
                *The CS and SC operators shall select the current stroking colour space and current stroking colour separately; G shall set them in combination. 
                *(The cs, sc, and g operators shall perform the same functions for nonstroking operations.) 
                *Setting either current colour space to DeviceGray shall initialize the corresponding current colour to 0.0.
                */

                /*8.6.4.3 DeviceRGB Colour Space
                *
                *Colours in the DeviceRGB colour space shall be specified according to the additive RGB(red-green - blue) colour model, in which colour values shall be defined by three components representing the intensities of the additive primary colorants red, green, and blue.
                *Each component shall be specified by a number in the range 0.0 to 1.0, where 0.0 shall denote the complete absence of a primary component and 1.0 shall denote maximum intensity.
                *
                *EXAMPLE   This example shows alternative ways to select the DeviceRGB colour space and a specific colour within that space for stroking operations.
                *
                *          / DeviceRGB CS % Set DeviceRGB colour space
                *          red green blue SC % Set colour
                *          red green blue RG % Set both in one operation
                *
                *The CS and SC operators shall select the current stroking colour space and current stroking colour separately; RG shall set them in combination.
                *The cs, sc, and rg operators shall perform the same functions for nonstroking operations. Setting either current colour space to DeviceRGB shall initialize the red, green, and blue components of the corresponding current colour to 0.0.
                */

                /*8.6.4.4 DeviceCMYK Colour Space
                *
                *The DeviceCMYK colour space allows colours to be specified according to the subtractive CMYK(cyan-magenta - yellow - black) model typical of printers and other paper - based output devices. 
                *The four components in a DeviceCMYK colour value shall represent the concentrations of these process colorants. 
                *Each component shall be a number in the range 0.0 to 1.0, where 0.0 shall denote the complete absence of a process colorant and 1.0 shall denote maximum concentration(absorbs as much as possible of the additive primary).
                *
                *NOTE  As much as the reflective colours(CMYK) decrease reflection with increased ink values and radiant colours(RGB) increases the intensity of colours with increased values the values work in an opposite manner.
                *
                *EXAMPLE   The following shows alternative ways to select the DeviceCMYK colour space and a specific colour within that space for stroking operations.
                *
                *          / DeviceCMYK CS % Set DeviceCMYK colour space
                *          cyan magenta yellow black SC % Set colour
                *          cyan magenta yellow black K % Set both in one operation
                *          
                *The CS and SC operators shall select the current stroking colour space and current stroking colour separately; K shall set them in combination.
                *The cs, sc, and k operators shall perform the same functions for nonstroking operations. 
                *Setting either current colour space to DeviceCMYK shall initialize the cyan, magenta, and yellow components of the corresponding current colour to 0.0 and the black component to 1.0.
                */
            
            /*8.6.5 CIE-Based Colour Spaces
            *
            */
                
                /*8.6.5.1 General
                *
                *Calibrated colour in PDF shall be defined in terms of an international standard used in the graphic arts, television, and printing industries. 
                *CIE -based colour spaces enable a page description to specify colour values in a way that is related to human visual perception. 
                *The goal is for the same colour specification to produce consistent results on different output devices, within the limitations of each device; 
                *Figure L.2 in Annex Lillustrates the kind of variation in colour reproduction that can result from the use of uncalibrated colour on different devices. 
                *PDF 1.1 supports three CIE-based colour space families, named CalGray, CalRGB, and Lab; PDF 1.3 added a fourth, named ICCBased.
                *
                *NOTE 1    In PDF 1.1, a colour space family named CalCMYK was partially defined, with the expectation that its definition would be completed in a future version.
                *          However, this feature has been deprecated. PDF 1.3 and later versions support calibrated four-component colour spaces by means of ICC profiles(see 8.6.5.5, "ICCBased Colour Spaces").
                *          A conforming reader should ignore CalCMYK colour space attributes and render colours specified in this family as if they had been specified using DeviceCMYK.
                *
                *NOTE 2    The details of the CIE colourimetric system and the theory on which it is based are beyond the scope of this specification; see the Bibliography for sources of further information.
                *          The semantics of CIE - based colour spaces are defined in terms of the relationship between the space’s components and the tristimulus values X, Y, and Z of the CIE 1931 XYZ space.
                *          The CalRGB and Lab colour spaces(PDF 1.1) are special cases of three - component CIE - based colour spaces, known as CIE - based ABC colour spaces.
                *          These spaces are defined in terms of a two - stage, nonlinear transformation of the CIE 1931 XYZ space.
                *          The formulation of such colour spaces models a simple zone theory of colour vision, consisting of a nonlinear trichromatic first stage combined with a nonlinear opponent - colour second stage.
                *          This formulation allows colours to be digitized with minimum loss of fidelity, an important consideration in sampled images.
                *
                *Colour values in a CIE - based ABC colour space shall have three components, arbitrarily named A, B, and C.
                *The first stage shall transform these components by first forcing their values to a specified range, then applying decoding functions, and then multiplying the results by a 3 - by - 3 matrix, 
                *producing three intermediate components arbitrarily named L, M, and N.The second stage shall transform these intermediate components in a similar fashion, 
                *producing the final X, Y, and Z components of the CIE 1931 XYZ space(see Figure 22).
                *
                *(see Figure 22 - Component Transformations in a CIE-based ABC Colour Space, on page 144)
                *
                *Colour spaces in the CIE-based families shall be defined by an array
                *
                *[name dictionary]
                *
                *where name is the name of the family and dictionary is a dictionary containing parameters that further characterize the space.
                *The entries in this dictionary have specific interpretations that depend on the colour space; some entries are required and some are optional.See the sub-clauses on specific colour space families for details.
                *
                *Setting the current stroking or nonstroking colour space to any CIE-based colour space shall initialize all components of the corresponding current colour to 0.0 
                *(unless the range of valid values for a given component does not include 0.0, in which case the nearest valid value shall be substituted.)
                *
                *NOTE 3    The model and terminology used here—CIE-based ABC (above) and CIE-based A (below)—are derived from the PostScript language, which supports these colour space families in their full generality. 
                *          PDF supports specific useful cases of CIE-based ABC and CIE-based A spaces; most others can be represented as ICCBased spaces.
                */

                /*8.6.5.2 CalGray Colour Spaces
                *
                *A CalGray colour space(PDF 1.1) is a special case of a single - component CIE - based colour space, known as a CIE - based A colour space.
                *This type of space is the one - dimensional(and usually achromatic) analog of CIE - based ABC spaces. 
                *Colour values in a CIE-based A space shall have a single component, arbitrarily named A.
                *Figure 23 illustrates the transformations of the A component to X, Y, and Z components of the CIE 1931 XYZspace.
                *
                *(see Figure 23 - Component Transformations in a CIE-based A Colour Space, on page 145)
                *
                *A CalGray colour space shall be a CIE-based A colour space with only one transformation stage instead of two. 
                *In this type of space, A represents the gray component of a calibrated gray space. 
                *This component shall be in the range 0.0 to 1.0. The decoding function (denoted by “Decode A” in Figure 23) is a gamma function whose coefficient shall be specified by the Gamma entry in the colour space dictionary (see Table 63). 
                *The transformation matrix denoted by “Matrix A” in the figure is derived from the dictionary’s WhitePoint entry, as described below. 
                *Since there is no second transformation stage, “Decode LMN” and “Matrix LMN” shall be implicitly taken to be identity transformations.
                *
                *Table 63 - Entries in a CalGray Colour Space Dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          WhitePoint          array                   (Required) An array of three numbers [XW YW ZW] specifying the tristimulus value, in the CIE 1931 XYZ space, of the diffuse white point; see 8.6.5.3, "CalRGB Colour Spaces", for further discussion. The numbers XW and ZW shall be positive, and YW shall be equal to 1.0.
                *
                *          BlackPoint          array                   (Optional) An array of three numbers [XB YB ZB] specifying the tristimulus value, in the CIE 1931 XYZ space, of the diffuse black point; see 8.6.5.3, "CalRGB Colour Spaces", for further discussion. All three of these numbers shall be non-negative. Default value: [0.0 0.0 0.0].
                *
                *          Gamma               number                  (Optional) A number G defining the gamma for the gray (A)component. G shall be positive and is generally greater than or equal to 1. Default value: 1.
                *
                *The transformation defined by the Gamma and WhitePoint entries is
                *
                *      X = L = X(w) x A^G
                *      Y = M = Y(w) x A^G
                *      Z = N = Z(w) x A^G
                *
                *In other words, the A component shall be first decoded by the gamma function, and the result shall be multiplied by the components of the white point to obtain the L, M, and N components of the intermediate representation. 
                *Since there is no second stage, the L, M, and N components shall also be the X, Y, and Zcomponents of the final representation.
                *
                *EXAMPLE 1     The examples in this sub - clause illustrate interesting and useful special cases of CalGray spaces.
                *              This example establishes a space consisting of the Y dimension of the CIE 1931 XYZ space with the CCIR XA/ 11–recommended D65 white point.
                *
                *              [ /CalGray
                *                  << /WhitePoint [0.9505 1.0000 1.0890] >>
                *              ]
                *
                *EXAMPLE 2     This example establishes a calibrated gray space with the CCIR XA/11–recommended D65 white point and opto-electronic transfer function.
                *
                *              [ / CalGray
                *                  << / WhitePoint[0.9505 1.0000 1.0890]
                *                     / Gamma 2.222
                *                  >>
                *              ]
                */

                /*8.6.5.3 CalRGB Colour Spaces
                *
                *A CalRGB colour space is a CIE - based ABC colour space with only one transformation stage instead of two.
                *In this type of space, A, B, and C represent calibrated red, green, and blue colour values. These three colour components shall be in the range 0.0 to 1.0; component values falling outside that range shall be adjusted to the nearest valid value without error indication.
                *The decoding functions(denoted by “Decode ABC” in Figure 22) are gamma functions whose coefficients shall be specified by the Gamma entry in the colour space dictionary(see Table 64). 
                *The transformation matrix denoted by “Matrix ABC” in Figure 22 shall be defined by the dictionary’s Matrix entry.
                *Since there is no second transformation stage, “Decode LMN” and “Matrix LMN” shall be implicitly taken to be identity transformations.
                *
                *Table 64 - Entries in a CalRGB Colour Space Dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          WhitePoint          array                   (Required) An array of three numbers [XW YW ZW] specifying the tristimulus value, in the CIE 1931 XYZ space, of the diffuse white point; see below for further discussion. 
                *                                                      The numbers XW and ZW shall be positive, and YW shall be equal to 1.0.
                *
                *          BlackPoint          array                   (Optional) An array of three numbers [XB YB ZB] specifying the tristimulus value, in the CIE 1931 XYZ space, of the diffuse black point; see below for further discussion. All three of these numbers shall be non-negative. Default value: [0.0 0.0 0.0].
                *
                *          Gamma               array                   (Optional) An array of three numbers [GR GG GB] specifying the gamma for the red, green, and blue (A, B, and C) components of the colour space. Default value: [1.0 1.0 1.0].
                *
                *          Matrix              array                   (Optional) An array of nine numbers [XA YA ZA XB YB ZB XC YC ZC] specifying the linear interpretation of the decoded A, B, and C components of the colour space with respect to the final XYZ representation. Default value: the identity matrix [1 0 0 0 1 0 0 0 1].
                *
                *The WhitePoint and BlackPoint entries in the colour space dictionary shall control the overall effect of the CIE-based gamut mapping function described in sub-clause 10.2, "CIE-Based Colour to Device Colour". 
                *Typically, the colours specified by WhitePoint and BlackPoint shall be mapped to the nearly lightest and nearly darkest achromatic colours that the output device is capable of rendering in a way that preserves colour appearance and visual contrast.
                *
                *WhitePoint represents the diffuse achromatic highlight, not a specular highlight. 
                *Specular highlights, achromatic or otherwise, are often reproduced lighter than the diffuse highlight. 
                *BlackPoint represents the diffuse achromatic shadow; its value is limited by the dynamic range of the input device. 
                *In images produced by a photographic system, the values of WhitePoint and BlackPoint vary with exposure, system response, and artistic intent; hence, their values are image-dependent.
                *
                *The transformation defined by the Gamma and Matrix entries in the CalRGB colour space dictionary shall be
                *
                *(see Equations on page 147) X, Y, Z
                *
                *The A, B, and C components shall first be decoded individually by the gamma functions. 
                *The results shall be treated as a three-element vector and multiplied by Matrix (a 3-by-3 matrix) to obtain the L, M, and Ncomponents of the intermediate representation. 
                *Since there is no second stage, these shall also be the X, Y, and Z components of the final representation.
                *
                *EXAMPLE   The following shows an example of a CalRGB colour space for the CCIR XA / 11–recommended D65 white point with 1.8 gammas and Sony Trinitron phosphor chromaticities.
                *
                *          [ / CalRGB
                *              << / WhitePoint[0.9505 1.0000 1.0890]
                *                 / Gamma[1.8000 1.8000 1.8000]
                *                 / Matrix[0.4497 0.2446 0.0252
                *                          0.3163 0.6720 0.1412
                *                          0.1845 0.0833 0.9227
                *                         ]
                *              >>
                *          ]
                *
                *The parameters of a CalRGB colour space may be specified in terms of the CIE 1931 chromaticity coordinates (xR, yR), (xG, yG), (xB, yB) of the red, green, and blue phosphors, respectively, and the chromaticity (xW, yW) of the diffuse white point corresponding to a linear RGB value (R, G, B), where R, G, and B should all equal 1.0. 
                *The standard CIE notation uses lowercase letters to specify chromaticity coordinates and uppercase letters to specify tristimulus values. 
                *Given this information, Matrix and WhitePoint shall be calculated as follows:
                *
                *(see Equations on page 147) z, Y(A), X(A), Y(B), X(B)
                *
                *(see Equatins on page 148) Y(C), X(C), X(W), Y(W), Z(W)
                */

                /*8.6.5.4 Lab Colour Spaces
                *
                *A Lab colour space is a CIE - based ABC colour space with two transformation stages(see Figure 22).
                *In this type of space, A, B, and C represent the L *, a *, and b*components of a CIE 1976 L* a*b * space.The range of the first(L*) component shall be 0 to 100; the ranges of the second and third(a * and b *) components shall be defined by the Range entry in the colour space dictionary(see Table 65).
                *
                *Figure L.3 in Annex L illustrates the coordinates of a typical Lab colour space; Figure L.4 in Annex L compares the gamuts(ranges of representable colours) for L* a*b *, RGB, and CMYK spaces.
                *
                *Table 65 - Entries in a Lab Colour Space Dictionary
                *
                *      [Key]           [Type]          [Value]
                *
                *      WhitePoint      array           (Required) An array of three numbers [XW YW ZW] that shall specify the tristimulus value, in the CIE 1931 XYZ space, of the diffuse white point; see 8.6.5.3, "CalRGB Colour Spaces" for further discussion. The numbers XW and ZW shall be positive, and YW shall be 1.0.
                *
                *      BlackPoint      array           (Optional) An array of three numbers [XB YB ZB] that shall specify the tristimulus value, in the CIE 1931 XYZ space, of the diffuse black point; see 8.6.5.3, "CalRGB Colour Spaces" for further discussion. All three of these numbers shall be non-negative. Default value: [0.0 0.0 0.0].
                *
                *      Range           array           (Optional) An array of four numbers [amin amax bmin bmax] that shall specify the range of valid values for the a* and b* (B and C) components of the colour space—that is,
                *
                *                                      a(min) <= a* <= a(max)
                *                                      and
                *                                      b(min) <= b* <= b(max)
                *                          
                *                                      Component values falling outside the specified range shall be adjusted to the nearest valid value without error indication.
                *                                      Default value: [−100 100 −100 100].
                *
                *A Lab colour space shall not specify explicit decoding functions or matrix coefficients for either stage of the transformation from L*a*b* space to XYZ space (denoted by “Decode ABC,” “Matrix ABC,” “Decode LMN,” and “Matrix LMN” in Figure 22). 
                *Instead, these parameters shall have constant implicit values. The first transformation stage shall be defined by the equations
                *
                *(see Equations on page 149) L, M, N
                *
                *The second transformation stage shall be
                *
                *(see Equations on Page 149) X, Y, Z
                *
                *where the function g(x) shall be defined as 
                *
                *(see Equations on Page 149) g(x)
                *
                *EXAMPLE       The following defines the CIE 1976 L*a*b* space with the CCIR XA/11–recommended D65 white point. 
                *              The a* and b* components, although theoretically unbounded, are defined to lie in the useful range -128 to +127.
                *
                *              [ /Lab
                *                  << /WhitePoint [0.9505 1.0000 1.0890]
                *                     /Range[-128 127 - 128 127]
                *                  >>
                *              ]
                */

                /*8.6.5.5 ICCBased Colour Spaces
                *
                *ICCBased colour spaces(PDF 1.3) shall be based on a cross-platform colour profile as defined by the International Color Consortium(ICC)(see, “Bibliography“).
                *Unlike the CalGray, CalRGB, and Lab colour spaces, which are characterized by entries in the colour space dictionary, an ICCBased colour space shall be characterized by a sequence of bytes in a standard format.
                *Details of the profile format can be found in the ICC specification(see, “Bibliography“).
                *
                *An ICCBased colour space shall be an array:
                *
                *  [/ICCBased stream]
                *
                *The stream shall contain the ICC profile.Besides the usual entries common to all streams(see Table 5), the profile stream shall have the additional entries listed in Table 66.
                *
                *Table 66 - Additional Entries Specific to an ICC Profile Stream Dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          N                   integer             (Required) The number of colour components in the colour space described by the ICC profile data. This number shall match the number of components actually in the ICC profile. N shall be 1, 3, or 4.
                *
                *          Alternate           array or name       (Optional) An alternate colour space that shall be used in case the one specified in the stream data is not supported. 
                *                                                  Non -conforming readers may use this colour space. The alternate space may be any valid colour space (except a Pattern colour space) that has the number of components specified by N. 
                *                                                  If this entry is omitted and the conforming reader does not understand the ICC profile data, the colour space that shall be used is DeviceGray, DeviceRGB, or DeviceCMYK, depending on whether the value of N is 1, 3, or 4, respectively.
                *                                                  There shall not be conversion of source colour values, such as a tint transformation, when using the alternate colour space. Colour values within the range of the ICCBased colour space might not be within the range of the alternate colour space.
                *                                                  In this case, the nearest values within the range of the alternate space shall be substituted.
                *
                *          Range               array               (Optional) An array of 2 × N numbers [min0 max0 min1 max1 …] that shall specify the minimum and maximum valid values of the corresponding colour components. These values shall match the information in the ICC profile. Default value: [0.0 1.0 0.0 1.0 …].
                *
                *          Metadata            stream              (Optional; PDF 1.4) A metadata stream that shall contain metadata for the colour space (see 14.3.2, "Metadata Streams").
                *
                *The ICC specification is an evolving standard. Table 67 shows the versions of the ICC specification on which the ICCBased colour spaces that PDF versions 1.3 and later shall use. 
                *(Earlier versions of the ICC specification shall also be supported.)
                *
                *Table 67 - ICC Specification Version Supported by ICC Based Colour Spaces
                *
                *          [PDF Version]               [ICC Specification Version]
                *
                *          1.3                         3.3
                *          1.4                         ICC.1:1998-09 and its addendum ICC.1A:1999-04
                *          1.5                         ICC.1:2001-12
                *          1.6                         ICC.1:2003-09
                *          1.7                         ICC.1:2004-10 (ISO 15076-1:2005)
                *
                *Conforming writers and readers should follow these guidelines:
                *
                *  •   A conforming reader shall support ICC.1:2004:10 as required by PDF 1.7, which will enable it to properly render all embedded ICC profiles regardless of the PDF version.
                *
                *  •   A conforming reader shall always process an embedded ICC profile according to the corresponding version of the PDF being processed as shown in Table 67 above; it shall not substitute the Alternate colour space in these cases.
                *
                *  •   A conforming writer should use ICC 1:2004 - 10 profiles.It may embed profiles conforming to a later ICC version. The conforming reader should process such profiles according to Table 67; if that is not possible, it shall substitute the Alternate colour space.
                *
                *  •   Conforming writers shall only use the profile types shown in Table 68 for specifying calibrated colour spaces for colouring graphic objects.Each of the indicated fields shall have one of the values listed for that field in the second column of the table.
                *      Profiles shall satisfy both the criteria shown in the table.The terminology is taken from the ICC specifications.
                *
                *NOTE 1        XYZ and 16-bit L*a*b* profiles are not listed.
                *
                *Table 68 - ICC Profile Types
                *
                *      [Header Field]              [Required Value]
                *
                *      deviceClass                 icSigInputClass('scnr')
                *                                  icSigDisplayClass('mntr')
                *                                  icSigOutputClass('prtr')
                *                                  icSigColorSpaceClass('spac')
                *
                *      colorSpace                  icSigGrayData('GRAY')
                *                                  icSigRgbData('RGB')
                *                                  icSigCmykData('CMYK')
                *                                  icSigLabData('Lab')
                *
                *The terminology used in PDF colour spaces and ICC colour profiles is similar, but sometimes the same terms are used with different meanings. 
                *The default value for each component in an ICCBased colour space is 0. The range of each colour component is a function of the colour space specified by the profile and is indicated in the ICC specification. 
                *The ranges for several ICC colour spaces are shown in Table 69.
                *
                *Table 69 - Ranges for Typical ICC Colour Spaces
                *
                *          [ICC Colour Space]              [Component Ranges]
                *
                *          Gray                            [0.0 1.0]
                *
                *          RGB                             [0.0 1.0]
                *
                *          CMYK                            [0.0 1.0]
                *
                *          L*a*b*                          L*: [0 100]; a* and b*: [-128 127]
                *
                *Since the ICCBased colour space is being used as a source colour space, only the “to CIE” profile information (AToB in ICC terminology) shall be used; the “from CIE” (BToA) information shall be ignored when present. 
                *An ICC profile may also specify a rendering intent, but a conforming reader shall ignore this information; the rendering intent shall be specified in PDF by a separate parameter (see 8.6.5.8, "Rendering Intents").
                *
                *The requirements stated above apply to an ICCBased colour space that is used to specify the source colours of graphics objects.When such a space is used as the blending colour space for a transparency group in the 
                *transparent imaging model(see 11.3.4, "Blending Colour Space"; 11.4, "Transparency Groups"; and 11.6.6, "Transparency Group XObjects"), 
                *it shall have both “to CIE” (AToB)and “from CIE” (BToA)information.This is because the group colour space shall be used as both the destination for objects being painted within the group and the source for the group’s results.
                *ICC profiles shall also be used in specifying output intents for matching the colour characteristics of a PDF document with those of a target output device or production environment.
                *When used in this context, they shall be subject to still other constraints on the “to CIE” and “from CIE” information; see 14.11.5, "Output Intents", for details.
                *
                *The representations of ICCBased colour spaces are less compact than CalGray, CalRGB, and Lab, but can represent a wider range of colour spaces.
                *
                *NOTE 2    One particular colour space is the “standard RGB” or sRGB, defined in the International Electrotechnical Commission(IEC) document Color Measurement and Management in Multimedia Systems and Equipment(see, “Bibliography“).
                *          In PDF, the sRGB colour space can only be expressed as an ICCBased space, although it can be approximated by a CalRGB space.
                *
                *EXAMPLE   The following shows an ICCBased colour space for a typical three-component RGB space. 
                *          The profile’s data has been encoded in hexadecimal representation for readability; in actual practice, a lossless decompression filter such as FlateDecode should be used.
                *
                *          10 0 obj                            % Colour space
                *             [/ ICCBased 15 0 R]
                *          endobj
                *          15 0 obj                            % ICC profile stream
                *              << / N 3
                *                 / Alternate / DeviceRGB
                *                 / Length 1605
                *                 / Filter / ASCIIHexDecode
                *              >>
                *          stream
                *          00 00 02 0C 61 70 70 6C 02 00 00 00 6D 6E 74 72
                *          52 47 42 20 58 59 5A 20 07 CB 00 02 00 16 00 0E
                *          00 22 00 2C 61 63 73 70 41 50 50 4C 00 00 00 00
                *          61 70 70 6C 00 00 04 01 00 00 00 00 00 00 00 02
                *          00 00 00 00 00 00 F6 D4 00 01 00 00 00 00 D3 2B
                *          00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                *          00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                *          00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                *          00 00 00 09 64 65 73 63 00 00 00 F0 00 00 00 71
                *          72 58 59 5A 00 00 01 64 00 00 00 14 67 58 59 5A
                *          00 00 01 78 00 00 00 14 62 58 59 5A 00 00 01 8C
                *          00 00 00 14 72 54 52 43 00 00 01 A0 00 00 00 0E
                *          67 54 52 43 00 00 01 B0 00 00 00 0E 62 54 52 43
                *          00 00 01 C0 00 00 00 0E 77 74 70 74 00 00 01 D0
                *          00 00 00 14 63 70 72 74 00 00 01 E4 00 00 00 27
                *          64 65 73 63 00 00 00 00 00 00 00 17 41 70 70 6C
                *          65 20 31 33 22 20 52 47 42 20 53 74 61 6E 64 61
                *          72 64 00 00 00 00 00 00 00 00 00 00 00 17 41 70
                *          70 6C 65 20 31 33 22 20 52 47 42 20 53 74 61 6E
                *          64 61 72 64 00 00 00 00 00 00 00 00 00 00 00 00
                *          00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                *          00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
                *          00 58 59 5A 58 59 5A 20 00 00 00 00 00 00 63 0A
                *          00 00 35 0F 00 00 03 30 58 59 5A 20 00 00 00 00
                *          00 00 53 3D 00 00 AE 37 00 00 15 76 58 59 5A 20
                *          00 00 00 00 00 00 40 89 00 00 1C AF 00 00 BA 82
                *          63 75 72 76 00 00 00 00 00 00 00 01 01 CC 63 75
                *          63 75 72 76 00 00 00 00 00 00 00 01 01 CC 63 75
                *          63 75 72 76 00 00 00 00 00 00 00 01 01 CC 58 59
                *          58 59 5A 20 00 00 00 00 00 00 F3 1B 00 01 00 00
                *          00 01 67 E7 74 65 78 74 00 00 00 00 20 43 6F 70
                *          79 72 69 67 68 74 20 41 70 70 6C 65 20 43 6F 6D
                *          70 75 74 65 72 73 20 31 39 39 34 00 >
                *          endstream
                *          endobj
                */

                /*8.6.5.6 Default Colour Spaces
                *
                *Colours that are specified in a device colour space(DeviceGray, DeviceRGB, or DeviceCMYK) are device-dependent.
                *By setting default colour spaces(PDF 1.1), a conforming writer can request that such colours shall be systematically transformed(remapped) into device-independent CIE - based colour spaces. 
                *This capability can be useful in a variety of circumstances:
                *
                *  •   A document originally intended for one output device is redirected to a different device.
                *
                *  •   A document is intended to be compatible with non - compliant readers and thus cannot specify CIE - based colours directly.
                *
                *  •   Colour corrections or rendering intents need to be applied to device colours(see 8.6.5.8, "Rendering Intents").
                *
                *A colour space is selected for painting each graphics object.This is either the current colour space parameter in the graphics state or a colour space given as an entry in an image XObject, inline image, or shading
                *dictionary. Regardless of how the colour space is specified, it shall be subject to remapping as described below.
                *
                *When a device colour space is selected, the ColorSpace subdictionary of the current resource dictionary(see 7.8.3, "Resource Dictionaries") is checked for the presence of an entry designating a corresponding default colour space(DefaultGray, DefaultRGB, or DefaultCMYK, corresponding to DeviceGray, DeviceRGB, or DeviceCMYK, respectively).
                *If such an entry is present, its value shall be used as the colour space for the operation currently being performed.
                *
                *Colour values in the original device colour space shall be passed unchanged to the default colour space, which shall have the same number of components as the original space.
                *The default colour space should be chosen to be compatible with the original, taking into account the components’ ranges and whether the components are additive or subtractive.
                *If a colour value lies outside the range of the default colour space, it shall be adjusted to the nearest valid value.
                *
                *Any colour space other than a Lab, Indexed, or Pattern colour space may be used as a default colour space and it should be compatible with the original device colour space as described above.
                *
                *If the selected space is a special colour space based on an underlying device colour space, the default colour space shall be used in place of the underlying space.
                *This shall apply to the following colour spaces:
                *
                *  •   The underlying colour space of a Pattern colour space
                *
                *  •   The base colour space of an Indexed colour space
                *
                *  •   The alternate colour space of a Separation or DeviceN colour space(but only if the alternate colour space is actually selected)
                *
                *See 8.6.6, "Special Colour Spaces", for details on these colour spaces.
                *
                *There is no conversion of colour values, such as a tint transformation, when using the default colour space. Colour values that are within the range of the device colour space might not be within the range of the default colour space(particularly if the default is an ICCBased colour space). 
                *In this case, the nearest values within the range of the default space are used.For this reason, a Lab colour space shall not be used as the DefaultRGB colour space.
                */

                /*8.6.5.7 Implicit Conversion of CIE-Based Colour Spaces
                *
                *In cases where a source colour space accurately represents the particular output device being used, a conforming reader should avoid converting the component colour values but use the source values directly as output values.
                *This avoids any unwanted computational error and in the case of 4 component colour spaces avoids the conversion from 4 components to 3 and back to 4, a process that loses critical colour information.
                *
                *NOTE 1    In workflows in which PDF documents are intended for rendering on a specific target output device(such as a printing press with particular inks and media), it is often useful to specify the source colours for some or all of a document’s objects in a CIE - based colour space that matches the calibration of the intended device.
                *          The resulting document, although tailored to the specific characteristics of the target device, remains device - independent and will produce reasonable results if retargeted to a different output device.
                *          However, the expectation is that if the document is printed on the intended target device, source colours that have been specified in a colour space matching the calibration of the device will pass through unchanged, without conversion to and from the intermediate CIE 1931 XYZ space as depicted in Figure 22.
                *
                *NOTE 2    In particular, when colours intended for a CMYK output device are specified in an ICCBased colour space using a matching CMYK printing profile, converting such colours from four components to three and back is unnecessary and results in a loss of fidelity in the black component.
                *          In such cases, a conforming reader may provide the ability for the user to specify a particular calibration to use for printing, proofing, or previewing.
                *          This calibration is then considered to be that of the native colour space of the intended output device(typically DeviceCMYK), and colours expressed in a CIE - based source colour space matching it can be treated as if they were specified directly in the device’s native colour space.
                *
                *NOTE 3    The conditions under which such implicit conversion is done cannot be specified in PDF, since nothing in PDF describes the calibration of the output device (although an output intent dictionary, if present, may suggest such a calibration; see 14.11.5, "Output Intents"). 
                *          The conversion is completely hidden by the conforming reader and plays no part in the interpretation of PDF colour spaces.
                *
                *When this type of implicit conversion is done, all of the semantics of the device colour space shall also apply, even though they do not apply to CIE-based spaces in general.
                *In particular:
                *
                *  •   The nonzero overprint mode (see 8.6.7, "Overprint Control") shall determine the interpretation of colour component values in the space.
                *
                *  •   If the space is used as the blending colour space for a transparency group in the transparent imaging model (see 11.3.4, "Blending Colour Space"; 11.4, "Transparency Groups"; and 11.6.6, "Transparency Group XObjects"), components of the space, such as Cyan, may be selected in a Separation or DeviceNcolour space used within the group(see 8.6.6.4, "Separation Colour Spaces" and 8.6.6.5, "DeviceN Colour Spaces").
                *
                *  •   Likewise, any uses of device colour spaces for objects within such a transparency group have well-defined conversions to the group colour space.
                *
                *NOTE 4    A source colour space can be specified directly(for example, with an ICCBased colour space) or indirectly using the default colour space mechanism(for example, DefaultCMYK; see 8.6.5.6, "Default Colour Spaces"). 
                *          The implicit conversion of a CIE-based colour space to a device space should not depend on whether the CIE-based space is specified directly or indirectly.
                */

                /*8.6.5.8 Rendering Intents
                *
                *Although CIE-based colour specifications are theoretically device - independent, they are subject to practical limitations in the colour reproduction capabilities of the output device. 
                *Such limitations may sometimes require compromises to be made among various properties of a colour specification when rendering colours for a given device.Specifying a rendering intent(PDF 1.1) allows a conforming writer to set priorities regarding which of these properties to preserve and which to sacrifice.
                *
                *EXAMPLE   The conforming writer might request that colours falling within the output device’s gamut(the range of colours it can reproduce) be rendered exactly while sacrificing the accuracy of out-of - gamut colours, or that a scanned image such as a photograph be rendered in a perceptually pleasing manner at the cost of strict colourimetric accuracy.
                *
                *Rendering intents shall be specified with the ri operator (see 8.4.4, "Graphics State Operators"), the RI entry in a graphics state parameter dictionary(see 8.4.5, "Graphics State Parameter Dictionaries"), or with the Intententry in image dictionaries(see 8.9.5, "Image Dictionaries"). 
                *The value shall be a name identifying the rendering intent. Table 70 lists the standard rendering intents that shall be recognized.Figure L.5 in Annex Lillustrates their effects. 
                *These intents have been chosen to correspond to those defined by the International Color Consortium(ICC), an industry organization that has developed standards for device - independent colour.
                *If a conforming reader does not recognize the specified name, it shall use the RelativeColorimetric intent by default.
                *
                *NOTE  Note, however, that the exact set of rendering intents supported may vary from one output device to another; a particular device may not support all possible intents or may support additional ones beyond those listed in the table.
                *
                *See 11.7.5, "Rendering Parameters and Transparency", and in particular 11.7.5.3, "Rendering Intent and Colour Conversions", for further discussion of the role of rendering intents in the transparent imaging model.
                *
                *Table 70 - Rendering Intents
                *
                *              [Name]                          [Description]
                *
                *              AbsoluteColorimetric            Colours shall be represented solely with respect to the light source; no correction shall be made for the output medium’s white point (such as the colour of unprinted paper). 
                *                                              Thus, for example, a monitor’s white point, which is bluish compared to that of a printer’s paper, would be reproduced with a blue cast. 
                *                                              In -gamut colours shall bereproduced exactly; out-of-gamut colours shall be mapped to the nearest value within the reproducible gamut.
                *
                *                                              NOTE 1  This style of reproduction has the advantage of providing exact colour matches from one output medium to another. 
                *                                                      It has the disadvantage of causing colours with Yvalues between the medium’s white point and 1.0 to be out of gamut. 
                *                                                      A typical use might be for logos and solid colours that require exact reproduction across different media.
                *
                *              RelativeColorimetric            Colours shall be represented with respect to the combination of the light source and the output medium’s white point (such as the colour of unprinted paper). 
                *                                              Thus, a monitor’s white point can be reproduced on a printer by simply leaving the paper unmarked, ignoring colour differences between the two media. 
                *                                              In -gamut colours shall be reproduced exactly; out-of-gamut colours shall bemapped to the nearest value within the reproducible gamut.
                *
                *                                              NOTE 2  This style of reproduction has the advantage of adapting for the varying white points of different output media.
                *                                                      It has the disadvantage of not providing exact colour matches from one medium to another.A typical use might be for vector graphics.
                *
                *              Saturation                      Colours shall be represented in a manner that preserves or emphasizes saturation. Reproduction of in-gamut colours may or may not be colourimetrically accurate.
                *
                *                                              NOTE 3  A typical use might be for business graphics, where saturation is the most important attribute of the colour.
                *
                *              Perceptual                      Colours shall be represented in a manner that provides a pleasing perceptual appearance. To preserve colour relationships, both in-gamut and out-of-gamut colours shall be generally modified from their precise colourimetric values.
                *
                *                                              NOTE 4  A typical use might be for scanned images.
                */                                              

            /*8.6.6 Special Colour Spaces
            *
            */

                /*8.6.6.1 General
                *
                *Special colour spaces add features or properties to an underlying colour space. There are four special colour space families: Pattern, Indexed, Separation, and DeviceN.
                */

                /*8.6.6.2 Pattern Colour Spaces
                *
                *A Pattern colour space(PDF 1.2) specifies that an area is to be painted with a pattern rather than a single colour.
                *The pattern shall be either a tiling pattern(type 1) or a shading pattern(type 2). 8.7, "Patterns", discusses patterns in detail.
                */

                /*8.6.6.3 Indexed Colour Spaces
                *
                *An Indexed colour space specifies that an area is to be painted using a colour map or colour table of arbitrary colours in some other space.
                *A conforming reader shall treat each sample value as an index into the colour table and shall use the colour value it finds there. 
                *This technique can considerably reduce the amount of data required to represent a sampled image.
                *
                *An Indexed colour space shall be defined by a four-element array:
                *
                *  [/ Indexed base hival lookup]
                *
                *The first element shall be the colour space family name Indexed.
                *The remaining elements shall be parameters that an Indexed colour space requires; their meanings are discussed below.Setting the current stroking or nonstroking colour space to an Indexed colour space shall initialize the corresponding current colour to 0.
                *
                *The base parameter shall be an array or name that identifies the base colour space in which the values in the colour table are to be interpreted. 
                *It shall be any device or CIE-based colour space or (PDF 1.3) a Separationor DeviceN space, but shall not be a Pattern space or another Indexed space. 
                *If the base colour space is DeviceRGB, the values in the colour table shall be interpreted as red, green, and blue components; if the base colour space is a CIE-based ABC space such as a CalRGB or Lab space, the values shall be interpreted as A, B, and C components.
                *
                *The hival parameter shall be an integer that specifies the maximum valid index value. The colour table shall be indexed by integers in the range 0 to hival. 
                *hival shall be no greater than 255, which is the integer required to index a table with 8 - bit index values.
                *
                *The colour table shall be defined by the lookup parameter, which may be either a stream or(PDF 1.2) a byte string.
                *It shall provide the mapping between index values and the corresponding colours in the base colour space.
                *
                *The colour table data shall be m ¥ (hival + 1) bytes long, where m is the number of colour components in the base colour space. 
                *Each byte shall be an unsigned integer in the range 0 to 255 that shall be scaled to the range of the corresponding colour component in the base colour space; 
                *that is, 0 corresponds to the minimum value in the range for that component, and 255 corresponds to the maximum.
                *
                *The colour components for each entry in the table shall appear consecutively in the string or stream.
                *
                *EXAMPLE 1     If the base colour space is DeviceRGB and the indexed colour space contains two colours, the order of bytes in the string or stream is R0 G0 B0 R1 G1 B1, 
                *              where letters denote the colour component and numeric subscripts denote the table entry.
                *
                *EXAMPLE 1     The following illustrates the specification of an Indexed colour space that maps 8 - bit index values to three-component colour values in the DeviceRGB colour space.
                *
                *              [ /Indexed
                *                /DeviceRGB
                *                255
                *                <000000 FF0000 00FF00 0000FF B57342 …>
                *              ]
                *
                *              The example shows only the first five colour values in the lookup string; in all, there should be 256 colour values and the string should be 768 bytes long. 
                *              Having established this colour space, the program can now specify colours as single-component values in the range 0 to 255. 
                *              For example, a colour value of 4 selects an RGB colour whose components are coded as the hexadecimal integers B5, 73, and 42.
                *
                *              Dividing these by 255 and scaling the results to the range 0.0 to 1.0 yields a colour with red, green, and blue components of 0.710, 0.451, and 0.259, respectively.
                *
                *Although an Indexed colour space is useful mainly for images, index values can also be used with the colour selection operators SC, SCN, sc, and scn.
                *
                *EXAMPLE 2     The following selects the same colour as does an image sample value of 123.
                *
                *              123 sc
                *              
                *The index value should be an integer in the range 0 to hival. If the value is a real number, it shall be rounded to the nearest integer; if it is outside the range 0 to hival, it shall be adjusted to the nearest value within that range.
                */

                /*8.6.6.4 Separation Colour Spaces
                *
                *A Separation colour space(PDF 1.2) provides a means for specifying the use of additional colorants or for isolating the control of individual colour components of a device colour space for a subtractive device.
                *When such a space is the current colour space, the current colour shall be a single - component value, called a tint, that controls the application of the given colorant or colour components only.
                *
                *NOTE 1    Colour output devices produce full colour by combining primary or process colorants in varying amounts.On an additive colour device such as a display, the primary colorants consist of red, green, and blue phosphors; on a subtractive device such as a printer, they typically consist of cyan, magenta, yellow, and sometimes black inks. 
                *          In addition, some devices can apply special colorants, often called spot colorants, to produce effects that cannot be achieved with the standard process colorants alone. Examples include metallic and fluorescent colours and special textures.
                *
                *NOTE 2    When printing a page, most devices produce a single composite page on which all process colorants(and spot colorants, if any) are combined. 
                *          However, some devices, such as imagesetters, produce a separate, monochromatic rendition of the page, called a separation, for each colorant. 
                *          When the separations are later combined—on a printing press, for example—and the proper inks or other colorants are applied to them, the result is a full - colour page.
                *
                *NOTE 3    The term separation is often misused as a synonym for an individual device colorant.
                *          In the context of this discussion, a printing system that produces separations generates a separate piece of physical medium(generally film) for each colorant. 
                *          It is these pieces of physical medium that are correctly referred to as separations.A particular colorant properly constitutes a separation only if the device is generating physical separations, one of which corresponds to the given colorant. 
                *          The Separation colour space is so named for historical reasons, but it has evolved to the broader purpose of controlling the application of individual colorants in general, regardless of whether they are actually realized as physical separations.
                *
                *NOTE 4    The operation of a Separation colour space itself is independent of the characteristics of any particular output device. 
                *          Depending on the device, the space may or may not correspond to a true, physical separation or to an actual colorant. 
                *          For example, a Separation colour space could be used to control the application of a single process colorant (such as cyan) on a composite device that does not produce physical separations, or could represent a colour (such as orange) for which no specific colorant exists on the device. 
                *          A Separation colour space provides consistent, predictable behaviour, even on devices that cannot directly generate the requested colour.
                *
                *A Separation colour space is defined as follows:
                *  
                *  [/Separation name alternateSpace tintTransform]
                *
                *It shall be a four-element array whose first element shall be the colour space family name Separation.
                *The remaining elements are parameters that a Separation colour space requires; their meanings are discussed below.
                *
                *A colour value in a Separation colour space shall consist of a single tint component in the range 0.0 to 1.0. 
                *The value 0.0 shall represent the minimum amount of colorant that can be applied; 1.0 shall represent the maximum.
                *Tints shall always be treated as subtractive colours, even if the device produces output for the designated component by an additive method.
                *Thus, a tint value of 0.0 denotes the lightest colour that can be achieved with the given colorant, and 1.0 is the darkest. 
                *The initial value for both the stroking and nonstroking colour in the graphics state shall be 1.0. 
                *The SCN and scn operators respectively shall set the current stroking and nonstroking colour to a tint value. 
                *A sampled image with single-component samples may also be used as a source of tint values.
                *
                *NOTE 5    This convention is the same as for DeviceCMYK colour components but opposite to the one for DeviceGrayand DeviceRGB.
                *
                *The name parameter is a name object that shall specify the name of the colorant that this Separation colour space is intended to represent(or one of the special names All or None; see below). 
                *Such colorant names are arbitrary, and there may be any number of them, subject to implementation limits.
                *
                *The special colorant name All shall refer collectively to all colorants available on an output device, including those for the standard process colorants.
                *When a Separation space with this colorant name is the current colour space, painting operators shall apply tint values to all available colorants at once.
                *
                *NOTE 6    This is useful for purposes such as painting registration targets in the same place on every separation.
                *          Such marks are typically painted as the last step in composing a page to ensure that they are not overwritten by subsequent painting operations.
                *
                *The special colorant name None shall not produce any visible output.Painting operations in a Separationspace with this colorant name shall have no effect on the current page.
                *
                *A conforming reader shall support Separation colour spaces with the colorant names All and None on all devices, even if the devices are not capable of supporting any others. 
                *When processing Separation spaces with either of these colorant names conforming readers shall ignore the alternateSpace and tintTransformparameters(discussed below), although valid values shall still be provided.
                *
                *At the moment the colour space is set to a Separation space, the conforming reader shall determine whether the device has an available colorant corresponding to the name of the requested space. 
                *If so, the conforming reader shall ignore the alternateSpace and tintTransform parameters; subsequent painting operations within the space shall apply the designated colorant directly, according to the tint values supplied.
                *
                *The preceding paragraph applies only to subtractive output devices such as printers and imagesetters. 
                *For an additive device such as a computer display, a Separation colour space never applies a process colorant directly; it always reverts to the alternate colour space as described below. 
                *This is because the model of applying process colorants independently does not work as intended on an additive device.
                *
                *EXAMPLE 1     Painting tints of the Red component on a white background produces a result that varies from white to cyan.
                *
                *This exception applies only to colorants for additive devices, not to the specific names Red, Green, and Blue.In contrast, a printer might have a(subtractive) ink named Red, which should work as a Separation colour space just the same as any other supported colorant.
                *
                *If the colorant name associated with a Separation colour space does not correspond to a colorant available on the device, the conforming reader shall arrange for subsequent painting operations to be performed in an alternate colour space.
                *The intended colours may be approximated by colours in a device or CIE - based colour space, which shall then be rendered with the usual primary or process colorants:
                *
                *  •   The alternateSpace parameter shall be an array or name object that identifies the alternate colour space, which may be any device or CIE - based colour space but may not be another special colour space(Pattern, Indexed, Separation, or DeviceN).
                *
                *  •   The tintTransform parameter shall be a function(see 7.10, "Functions").During subsequent painting operations, a conforming reader calls this function to transform a tint value into colour component values in the alternate colour space.The function shall be called with the tint value and shall return the corresponding colour component values. 
                *      That is, the number of components and the interpretation of their values shall depend on the alternate colour space.
                *
                *NOTE 7    Painting in the alternate colour space may produce a good approximation of the intended colour when only opaque objects are painted. 
                *          However, it does not correctly represent the interactions between an object and its backdrop when the object is painted with transparency or when overprinting (see 8.6.7, "Overprint Control") is enabled.
                *
                *EXAMPLE 2     The following illustrates the specification of a Separation colour space(object 5) that is intended to produce a colour named LogoGreen.
                *              If the output device has no colorant corresponding to this colour, DeviceCMYK is used as the alternate colour space, and the tint transformation function(object 12) maps tint values linearly into shades of a CMYK colour value approximating the LogoGreen colour.
                *
                *              5 0 obj                 % Colour space
                *                  [ / Separation
                *                    / LogoGreen
                *                    / DeviceCMYK
                *                    12 0 R
                *                  ]
                *              endobj
                *              12 0 obj                % Tint transformation function
                *                  << / FunctionType 4
                *                     / Domain[0.0 1.0]
                *                     / Range[0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0]
                *                     / Length 62
                *                  >>
                *              stream
                *                  {   dup 0.84 mul
                *                  exch 0.00 exch dup 0.44 mul
                *                  exch 0.21 mul
                *                  }
                *              endstream
                *              endobj
                *
                *See 11.7.3, "Spot Colours and Transparency", for further discussion of the role of Separation colour spaces in the transparent imaging model.
                */

                /*8.6.6.5 DeviceN Colour Spaces
                *
                *DeviceN colour spaces(PDF 1.3) may contain an arbitrary number of colour components.
                *
                *NOTE 1    They provide greater flexibility than is possible with standard device colour spaces such as DeviceCMYK or with individual Separation colour spaces.
                *
                *EXAMPLE 1     It is possible to create a DeviceN colour space consisting of only the cyan, magenta, and yellow colour components, with the black component excluded.
                *
                *NOTE 2    DeviceN colour spaces are used in applications such as these:
                *
                *          High - fidelity colour is the use of more than the standard CMYK process colorants to produce an extended gamut, or range of colours. 
                *          A popular example is the PANTONE Hexachrome system, which uses six colorants: the usual cyan, magenta, yellow, and black, plus orange and green.
                *
                *          Multitone colour systems use a single-component image to specify multiple colour components. 
                *          In a duotone, for example, a single - component image can be used to specify both the black component and a spot colour component.
                *          The tone reproduction is generally different for the different components.For example, the black component might be painted with the exact sample data from the single-component image; 
                *          the spot colour component might be generated as a nonlinear function of the image data in a manner that emphasizes the shadows. 
                *          Figure L.6 in Annex L shows an example that uses black and magenta colour components. 
                *          In Figure L.7 in Annex L, a single - component grayscale image is used to generate a quadtone result that uses four colorants: black and three PANTONE spot colours. 
                *          See EXAMPLE 5 in this sub - clause for the code used to generate this image.
                *
                *DeviceN shall be used to represent colour spaces containing multiple components that correspond to colorants of some target device. 
                *As with Separation colour spaces, conforming readers shall be able to approximate the colorants if they are not available on the current output device, such as a display. 
                *To accomplish this, the colour space definition provides a tint transformation function that shall be used to convert all the components to an alternate colour space.
                *
                *PDF 1.6 extended the meaning of DeviceN to include colour spaces that are referred to as NChannel colour spaces. 
                *Such colour spaces may contain an arbitrary number of spot and process components, which may or may not correspond to specific device colorants(the process components shall be from a single process colour space). 
                *They provide information about each component that allows conforming readers more flexibility in converting colours. 
                *These colour spaces shall be identified by a value of NChannel for the Subtype entry of the attributes dictionary(see Table 71).
                *A value of DeviceN for the Subtype entry, or no value, shall mean that only the previous features shall be supported.
                *Conforming readers that do not support PDF 1.6 shall treat these colour spaces as normal DeviceN colour spaces and shall use the tint transformation function as appropriate.
                *Conforming writers using the NChannel features should follow certain guidelines, as noted throughout this sub - clause, to achieve good backward compatibility.
                *
                *EXAMPLE 2     They may use their own blending algorithms for on - screen viewing and composite printing, rather than being required to use a specified tint transformation function.
                *
                *DeviceN colour spaces shall be defined in a similar way to Separation colour spaces—in fact, a Separationcolour space can be defined as a DeviceN colour space with only one component.
                *
                *A DeviceN colour space shall be specified as follows:
                *
                *      [/DeviceN names alternateSpace tintTransform]
                *or
                *      [/ DeviceN names alternateSpace tintTransform attributes]
                *
                *It is a four - or five - element array whose first element shall be the colour space family name DeviceN. 
                *The remaining elements shall be parameters that a DeviceN colour space requires.
                *
                *The names parameter shall be an array of name objects specifying the individual colour components.
                *The length of the array shall determine the number of components in the DeviceN colour space, which is subject to an implementation limit; see Annex C.
                *The component names shall all be different from one another, except for the name None, which may be repeated as described later in this sub - clause.
                *The special name All, used by Separation colour spaces, shall not be used.
                *
                *Colour values shall be tint components in the range 0.0 to 1.0:
                *
                *  •   For DeviceN colour spaces that do not have a subtype of NChannel, 0.0 shall represent the minimum amount of colorant; 1.0 shall represent the maximum. 
                *      Tints shall always be treated as subtractive colours, even if the device produces output for the designated component by an additive method.
                *      Thus, a tint value of 0.0 shall denote the lightest colour that can be achieved with the given colorant, and 1.0 the darkest.
                *
                *NOTE 3    This convention is the same one as for DeviceCMYK colour components but opposite to the one for DeviceGray and DeviceRGB.
                *
                *  •   For NChannel colour spaces, values for additive process colours(such as RGB) shall be specified in their natural form, where 1.0 shall represent maximum intensity of colour.
                *
                *When this space is set to the current colour space (using the CS or cs operators), each component shall be given an initial value of 1.0. 
                *The SCN and scn operators respectively shall set the current stroking and nonstroking colour. 
                *Operand values supplied to SCN or scn shall be interpreted as colour component values in the order in which the colours are given in the names array, as are the values in a sampled image that uses a DeviceN colour space.
                *
                *The alternateSpace parameter shall be an array or name object that can be any device or CIE-based colour space but shall not be another special colour space (Pattern, Indexed, Separation, or DeviceN). 
                *When the colour space is set to a DeviceN space, if any of the component names in the colour space do not correspond to a colorant available on the device, the conforming reader shall perform subsequent painting operations in the alternate colour space specified by this parameter.
                *
                *For NChannel colour spaces, the components shall be evaluated individually; that is, only the ones not present on the output device shall use the alternate colour space.
                *
                *The tintTransform parameter shall specify a function(see 7.10, "Functions") that is used to transform the tint values into the alternate colour space.
                *It shall be called with n tint values and returns m colour component values, where n is the number of components needed to specify a colour in the DeviceN colour space and m is the number required by the alternate colour space.
                *
                *NOTE 4    Painting in the alternate colour space may produce a good approximation of the intended colour when only opaque objects are painted.
                *          However, it does not correctly represent the interactions between an object and its backdrop when the object is painted with transparency or when overprinting(see 8.6.7, "Overprint Control") is enabled.
                *
                *The colour component name None, which may be present only for DeviceN colour spaces that do not have the NChannel subtype, indicates that the corresponding colour component shall never be painted on the page, as in a Separation colour space for the None colorant.
                *When a DeviceN colour space is painting the named device colorants directly, colour components corresponding to None colorants shall be discarded.
                *However, when the DeviceN colour space reverts to its alternate colour space, those components shall be passed to the tint transformation function, which may use them as desired.
                *
                *A DeviceN colour space whose component colorant names are all None shall always discard its output, just the same as a Separation colour space for None; 
                *it shall never revert to the alternate colour space.Reversion shall occur only if at least one colour component(other than None) is specified and is not available on the device.
                *
                *The optional attributes parameter shall be a dictionary (see Table 71) containing additional information about the components of colour space that conforming readers may use. 
                *Conforming readers need not use the alternateSpace and tintTransform parameters, and may instead use custom blending algorithms, along with other information provided in the attributes dictionary if present. 
                *(If the value of the Subtype entry in the attributes dictionary is NChannel, such information shall be present.) 
                *However, alternateSpace and tintTransform shall always be provided for conforming readers that want to use them or do not support PDF 1.6.
                *
                *Table 71 - Entries in a DeviceN Colour Space Attributes Dictionary
                *
                *              [Key]                   [Type]                  [Value]
                *
                *              Subtype                 name                    (Optional; PDF 1.6) A name specifying the preferred treatment for the colour space. Values shall be DeviceN or NChannel. Default value: DeviceN.
                *
                *              Colorants               dictionary              (Required if Subtype is NChannel and the colour space includes spot colorants; otherwise optional) 
                *                                                              A dictionary describing the individual colorants that shall be used in the DeviceN colour space. 
                *                                                              For each entry in this dictionary, the key shall be a colorant name and the value shall be an array defining a Separation colour space for that colorant (see 8.6.6.4, "Separation Colour Spaces"). 
                *                                                              The key shall match the colorant name given in that colour space.
                *                                                              This dictionary provides information about the individual colorants that may be useful to some conforming readers. 
                *                                                              In particular, the alternate colour space and tint transformation function of a Separation colour space describe the appearance of that colorant alone, whereas those of a DeviceN colour space describe only the appearance of its colorants in combination.
                *                                                              If Subtype is NChannel, this dictionary shall have entries for all spot colorants in this colour space.This dictionary may also include additional colorants not used by this colour space.
                *
                *              Process                 dictionary              (Required if Subtype is NChannel and the colour space includes components of a process colour space, otherwise optional; PDF 1.6) A dictionary (see Table 72) that describes the process colour space whose components are included in this colour space.
                *
                *              MixingHints             dictionary              (Optional; PDF 1.6) A dictionary (see Table 73) that specifies optional attributes of the inks that shall be used in blending calculations when used as an alternative to the tint transformation function.
                *
                *
                *A value of NChannel for the Subtype entry indicates that some of the other entries in this dictionary are required rather than optional. 
                *The Colorants entry specifies a colorants dictionary that contains entries for all the spot colorants in the colour space; they shall be defined using individual Separation colour spaces. 
                *The Process entry specifies a process dictionary (see Table 72) that identifies the process colour space that is used by this colour space and the names of its components. 
                *It shall be present if Subtype is NChannel and the colour space has process colour components. An NChannel colour space shall contain components from at most one process colour space.
                *
                *For colour spaces that have a value of NChannel for the Subtype entry in the attributes dictionary(see Table 71), the following restrictions apply to process colours:
                *
                *  •   There may be colour components from at most one process colour space, which may be any device or CIE - based colour space.
                *
                *  •   For a non - CMYK colour space, the names of the process components shall appear sequentially in the names array, in the normal colour space order(for example, Red, Green, and Blue).However, the names in the names array need not match the actual colour space names(for example, a Red component need not be named Red).The mapping of names is specified in the process dictionary(see Table 72 and discussion below), which shall be present.
                *
                *  •   Definitions for process colorants should not appear in the colorants dictionary.Any such definition shall be ignored if the colorant is also present in the process dictionary.Any component not specified in the process dictionary shall be considered to be a spot colorant.
                *
                *  •   For a CMYK colour space, a subset of the components may be present, and they may appear in any order in the names array.The reserved names Cyan, Magenta, Yellow, and Black shall always be considered to be process colours, which do not necessarily correspond to the colorants of a specific device; they need not have entries in the process dictionary.
                *
                *  •   The values associated with the process components shall be stored in their natural form (that is, subtractive colour values for CMYK and additive colour values for RGB), since they shall be interpreted directly as process values by consumers making use of the process dictionary. (For additive colour spaces, this is the reverse of how colour values are specified for DeviceN, as described above in the discussion of the names parameter.)
                *
                *The MixingHints entry in the attributes dictionary specifies a mixing hints dictionary (see Table 73) that provides information about the characteristics of colorants that may be used in blending calculations when the actual colorants are not available on the target device. Conforming readers need not use this information.
                *
                *Table 72 - Entries in a DeviceN Process Dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          ColorSpace          name or array       (Required) A name or array identifying the process colour space, which may be any device or CIE-based colour space. If an ICCBased colour space is specified, it shall provide calibration information appropriate for the process colour components specified in the names array of the DeviceN colour space.
                *
                *          Components          array               (Required) An array of component names that correspond, in order, to the components of the process colour space specified in ColorSpace. For example, an RGB colour space shall have three names corresponding to red, green, and blue. 
                *                                                  The names may be arbitrary (that is, not the same as the standard names for the colour space components) and shall match those specified in the names array of the DeviceN colour space, even if all components are not present in the names array.
                *
                *Table 73 - Entries in a DeviceN Mixing Hints Dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Solidities          dictionary          (Optional) A dictionary specifying the solidity of inks that shall be used in blending calculations when used as an alternative to the tint transformation function. 
                *                                                  For each entry, the key shall be a colorantname, and the value shall be a number between 0.0 and 1.0. This dictionary need not contain entries for all colorants used in this colour space; it may also include additional colorants not used by this colour space.
                *                                                  A value of 1.0 simulates an ink that completely covers the inks beneath; a value of 0.0 simulates a transparent ink that completely reveals the inks beneath. An entry with a key of Default specifies a value that shall be used by all components in the associated DeviceN colour space for which a solidity value is not explicitly provided.
                *                                                  If Default is not present, the default value for unspecified colorants shall be 0.0; conforming readers may choose to use other values.
                *                                                  If this entry is present, PrintingOrder shall also be present.
                *
                *          PrintingOrder       array               (Required if Solidities is present) An array of colorant names, specifying the order in which inks shall be laid down. Each component in the names array of the DeviceN colour space shall appear in this array (although the order is unrelated to the order specified in the namesarray). 
                *                                                  This entry may also list colorants unused by this specific DeviceN instance.
                *
                *          DotGain             dictionary          (Optional) A dictionary specifying the dot gain of inks that shall be used in blending calculations when used as an alternative to the tint transformation function. 
                *                                                  Dot gain (or loss) represents the amount by which a printer’s halftone dots change as the ink spreads and is absorbed by paper.
                *                                                  For each entry, the key shall be a colorant name, and the value shall bea function that maps values in the range 0 to 1 to values in the range 0 to 1.
                *                                                  The dictionary may list colorants unused by this specific DeviceNinstance and need not list all colorants. 
                *                                                  An entry with a key of Defaultshall specify a function to be used by all colorants for which a dot gain function is not explicitly specified.
                *                                                  Conforming readers may ignore values in this dictionary when other sources of dot gain information are available, such as ICC profiles associated with the process colour space or tint transformation functions associated with individual colorants.
                *
                *Each entry in the mixing hints dictionary refers to colorant names, which include spot colorants referenced by the Colorants dictionary. 
                *Under some circumstances, they may also refer to one or more individual process components called Cyan, Magenta, Yellow, or Black when DeviceCMYK is specified as the process colour space in the process dictionary. 
                *However, applications shall ignore these process component entries if they can obtain the information from an ICC profile.
                *
                *NOTE 5    The mixing hints subdictionaries(as well as the colorants dictionary) may specify colorants that are not used in any given instance of a DeviceN colour space. 
                *This allows them to be referenced from multiple DeviceNcolour spaces, which can produce smaller file sizes as well as consistent colour definitions across instances.
                *
                *For consistency of colour, conforming readers should follow these guidelines:
                *
                *  •   The conforming reader shall apply either the specified tint transformation function or invoke the same alternative blending algorithm for all DeviceN instances in the document.
                *
                *NOTE 6    When the tint transformation function is used, the burden is on the conforming writer to guarantee that the individual function definitions chosen for all DeviceN instances produce similar colour appearances throughout the document.
                *
                *  •   Blending algorithms should produce a similar appearance for colours when they are used as separation colours or as a component of a DeviceN colour space.
                *
                *EXAMPLE 3     This example shows a DeviceN colour space consisting of three colour components named Orange, Green, and None. 
                *              In this example, the DeviceN colour space, object 30, has an attributes dictionary whose Colorants entry is an indirect reference to object 45 (which might also be referenced by attributes dictionaries of other DeviceN colour spaces). 
                *              tintTransform1, whose definition is not shown, maps three colour components (tints of the colorants Orange, Green, and None) to four colour components in the alternate colour space, DeviceCMYK. 
                *              tintTransform2 maps a single colour component (an orange tint) to four components in DeviceCMYK. 
                *              Likewise, tintTransform3 maps a green tint to DeviceCMYK, and tintTransform4 maps a tint of PANTONE 131 to DeviceCMYK.
                *
                *              30 0 obj                        % Colour space
                *                  [ / DeviceN
                *                      [/ Orange / Green / None]
                *                       / DeviceCMYK
                *                       tintTransform1
                *                       << / Colorants 45 0 R >>
                *                  ]
                *              endobj
                *
                *EXAMPLE 4     45 0 obj                        % Colorants dictionary
                *                  << / Orange     [/ Separation
                *                                   / Orange
                *                                   / DeviceCMYK
                *                                   tintTransform2
                *                                  ]
                *                     / Green      [ / Separation
                *                                    / Green
                *                                    / DeviceCMYK
                *                                    tintTransform3
                *                                   ]
                *                     / PANTONE#20131 [ /Separation
                *                                       / PANTONE#20131
                *                                       / DeviceCMYK
                *                                        tintTransform4
                *                                     ]
                *                  >>
                *              endobj
                *
                *NOTE 7    EXAMPLE 5 through EXAMPLE 8 show the use of NChannel colour spaces.
                *
                *EXAMPLE 5     This example shows the use of calibrated CMYK process components. EXAMPLE 6 shows the use of Lab process components.
                *
                *              10 0 obj % Colour space
                *                  [ / DeviceN
                *                      [/ Magenta / Spot1 / Yellow / Spot2]
                *                       alternateSpace
                *                       tintTransform1
                *                       <<                                     % Attributes dictionary
                *                          / Subtype / NChannel
                *                          / Process
                *                              << / ColorSpace[/ ICCBased CMYK_ICC profile]
                *                                 / Components[/ Cyan / Magenta / Yellow / Black]
                *                              >>
                *                          / Colorants
                *                              << / Spot1[/ Separation / Spot1 alternateSpace tintTransform2]
                *                                 / Spot2[/ Separation / Spot2 alternateSpace tintTransform3]
                *                              >>
                *                      >>
                *                  ]
                *              endobj
                *
                *EXAMPLE 6     10 0 obj%Colour space
                *                  [ /DeviceN
                *                      [/L /a /b /Spot1 /Spot2]
                *                      alternateSpace
                *                      tintTransform1
                *                      << % Attributes dictionary
                *                          /Subtype /NChannel
                *                          /Process
                *                              << /ColorSpace[ / Lab << / WhitePoint... / Range... >> ]
                *                                 /Components[/ L / a / b]
                *                              >>
                *                          /Colorants
                *                              << /Spot1[/ Separation / Spot1 alternateSpace tintTransform2]
                *                                  /Spot2[/ Separation / Spot2 alternateSpace tintTransform3]
                *                              >>
                *                      >>
                *                  ]
                *
                *EXAMPLE 7     This example shows the recommended convention for dealing with situations where a spot colorant and a process colour component have the same name.
                *              Since the names array may not have duplicate names, the process colours should be given different names, which are mapped to process components in the Components entry of the process dictionary. 
                *              In this case, Red refers to a spot colorant; ProcessRed, ProcessGreen, and ProcessBlue are mapped to the components of an RGB colour space.
                *
                *              
                *              10 0 obj                                % Colour space
                *              [ /DeviceN
                *                  [/ProcessRed /ProcessGreen /ProcessBlue /Red]
                *                  alternateSpace
                *                  tintTransform1
                *                  <<                                  % Attributes dictionary
                *                      /Subtype /NChannel
                *                      /Process
                *                          << /ColorSpace[ / ICCBased RGB_ICC profile]
                *                             /Components[/ ProcessRed / ProcessGreen / ProcessBlue]
                *                          >>
                *                      /Colorants
                *                          << /Red[/ Separation / Red alternateSpace tintTransform2] >>
                *                  >>
                *              ]
                *
                *EXAMPLE 8     This example shows the use of a mixing hints dictionary.
                *
                *              10 0 obj                                % Colour space
                *                  [/DeviceN
                *                      [/Magenta /Spot1 /Yellow /Spot2]
                *                      alternateSpace
                *                      tintTransform1
                *                      <<
                *                          /Subtype /NChannel
                *                          /Process
                *                              << /ColorSpace[ / ICCBased CMYK_ICC profile]
                *                                 /Components[/ Cyan / Magenta / Yellow / Black]
                *                              >>
                *                          /Colorants
                *                              << /Spot1[/ Separation / Spot1 alternateSpace tintTransform2]
                *                                 /Spot2[/ Separation / Spot2 alternateSpace tintTransform2]
                *                              >>
                *                          /MixingHints
                *                              <<
                *                                  /Solidities
                *                                      << /Spot1 1.0
                *                                         /Spot2 0.0
                *                                      >>
                *                                  /DotGain
                *                                      << /Spot1 function1
                *                                         /Spot2 function2
                *                                         /Magenta function3
                *                                         /Yellow function4
                *                                      >>
                *                                  /PrintingOrder[/ Magenta / Yellow / Spot1 / Spot2]
                *                              >>
                *                      >>
                *                  ]
                *
                *See 11.7.3, "Spot Colours and Transparency", for further discussion of the role of DeviceN colour spaces in the transparent imaging model.
                */

                /*8.6.6.6   Multitone Examples
                *
                *NOTE 1    The following examples illustrate various interesting and useful special cases of the use of Indexed and DeviceN colour spaces in combination to produce multitone colours.
                *
                *NOTE 2    EXAMPLE 1 and EXAMPLE 2 in this sub - clause illustrate the use of DeviceN to create duotone colour spaces.
                *
                *EXAMPLE 1     In this example, an Indexed colour space maps index values in the range 0 to 255 to a duotone DeviceNspace in cyan and black. 
                *              In effect, the index values are treated as if they were tints of the duotone space, which are then mapped into tints of the two underlying colorants. 
                *              Only the beginning of the lookup table string for the Indexed colour space is shown; the full table would contain 256 two-byte entries, each specifying a tint value for cyan and black, for a total of 512 bytes. 
                *              If the alternate colour space of the DeviceN space is selected, the tint transformation function (object 15 in the example) maps the two tint components for cyan and black to the four components for a DeviceCMYK colour space by supplying zero values for the other two components.
                *
                *              10 0 obj                                            %Colour space
                *                  [ /Indexed
                *                      [ /DeviceN
                *                          [/Cyan /Black]
                *                           /DeviceCMYK
                *                           15 0 R
                *                      ]
                *                      255
                *                      <6605 6806 6907 6B09 6C0A …>
                *                  ]
                *              endobj
                *              15 0 obj                                            % Tint transformation function
                *                  << /FunctionType 4
                *                     /Domain[0.0 1.0 0.0 1.0]
                *                     /Range[0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0]
                *                     /Length 16
                *                  >>
                *              stream
                *                  {0 0 3 -1 roll}
                *              endstream
                *              endobj
                *
                *EXAMPLE 2     This example shows the definition of another duotone colour space, this time using black and gold colorants (where gold is a spot colorant) and using a CalRGB space as the alternate colour space. 
                *              This could be defined in the same way as in the preceding example, with a tint transformation function that converts from the two tint components to colours in the alternate CalRGB colour space.
                *
                *              30 0 obj                                            % Colour space
                *                  [ / Indexed
                *                      [ / DeviceN
                *                          [/ Black / Gold]
                *                          [/ CalRGB
                *                              << / WhitePoint[1.0 1.0 1.0]
                *                                 / Gamma[2.2 2.2 2.2]
                *                              >>
                *                          ]
                *              35 0 R                                              % Tint transformation function
                *                      ]
                *                      255
                *                      …Lookup table…
                *                  ]
                *              endobj
                *
                *NOTE 3        Given a formula for converting any combination of black and gold tints to calibrated RGB, a 2-in, 3-out type 4 (PostScript calculator) function could be used for the tint transformation. 
                *              Alternatively, a type 0 (sampled) function could be used, but this would require a large number of sample points to represent the function accurately; for example, sampling each input variable for 256 tint values between 0.0 and 1.0 would require 256^2 = 65,536 samples. 
                *              But since the DeviceN colour space is being used as the base of an Indexed colour space, there are actually only 256 possible combinations of black and gold tint values.
                *
                *EXAMPLE 3     This example shows a more compact way to represent this information is to put the alternate colour values directly into the lookup table alongside the DeviceN colour values.
                *
                *              10 0 obj                            % Colour space
                *                  [ / Indexed
                *                      [ / DeviceN
                *                          [/ Black / Gold / None / None / None]
                *                          [/ CalRGB
                *                              << / WhitePoint[1.0 1.0 1.0]
                *                                 / Gamma[2.2 2.2 2.2]
                *                              >>
                *                          ]
                *              20 0 R                              % Tint transformation function
                *                      ]
                *                      255
                *                      …Lookup table…
                *                  ]
                *              endobj
                *
                *NOTE 4        In EXAMPLE 3 in this sub-clause, each entry in the lookup table has five components: two for the black and gold colorants and three more (specified as None) for the equivalent CalRGB colour components. 
                *              If the black and gold colorants are available on the output device, the None components are ignored; if black and gold are not available, the tint transformation function is used to convert a five-component colour into a three-component equivalent in the alternate CalRGB colour space. 
                *              But because, by construction, the third, fourth, and fifth components are the CalRGB components, the tint transformation function can merely discard the first two components and return the last three. 
                *              This can be readily expressed with a type 4 (PostScript calculator) function (see EXAMPLE 4 in this sub-clause).
                *
                *EXAMPLE 4     This example shows a type 4 (PostScript calculator) function.
                *
                *              20 0 obj                            % Tint transformation function
                *                  << / FunctionType 4
                *                     / Domain[0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0]
                *                     / Range[0.0 1.0 0.0 1.0 0.0 1.0]
                *                     / Length 27
                *                  >>
                *              stream
                *                  {5 3 roll pop pop}
                *              endstream
                *              endobj
                *
                *EXAMPLE 5     This example uses an extension of the techniques described above to produce the quadtone (four-component) image shown in Figure L.7 in Annex L.
                *
                *              5 0 obj                             % Image XObject
                *                  << / Type / XObject
                *                     / Subtype / Image
                *                     / Width 288
                *                     / Height 288
                *                     / ColorSpace 10 0 R
                *                     / BitsPerComponent 8
                *                     / Length 105278
                *                     / Filter / ASCII85Decode
                *                  >>
                *              stream
                *              …Data for grayscale image…
                *              endstream
                *              endobj
                *
                *              10 0 obj                            % Indexed colour space for image
                *                  [ / Indexed
                *                      15 0 R                      % Base colour space
                *                      255                         % Table has 256 entries
                *                      30 0 R                      % Lookup table
                *                  ]
                *              endobj
                *              15 0 obj                            % Base colour space(DeviceN) for Indexed space
                *                  [ / DeviceN
                *                      [ / Black                   % Four colorants(black plus three spot colours)
                *                        / PANTONE#20216#20CVC
                *                        / PANTONE#20409#20CVC
                *                        / PANTONE#202985#20CVC
                *                        / None                    % Three components for alternate space
                *                        / None
                *                        / None
                *                      ]
                *              16 0 R                              % Alternate colour space
                *              20 0 R                              % Tint transformation function
                *                  ]
                *              endobj
                *              16 0 obj                            % Alternate colour space for DeviceN space
                *                  [ / CalRGB
                *                      << / WhitePoint[1.0 1.0 1.0] >>
                *                  ]
                *              endobj
                *              20 0 obj                            % Tint transformation function for DeviceN space
                *                  << / FunctionType 4
                *                     / Domain[0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0]
                *                     / Range[0.0 1.0 0.0 1.0 0.0 1.0]
                *                     / Length 44
                *                  >>
                *
                *              stream
                *              { 7 3 roll                          % Just discard first four values
                *              pop pop pop pop
                *              }
                *              endstream
                *              endobj
                *              30 0 obj                            % Lookup table for Indexed colour space
                *                  << / Length 1975
                *                     / Filter[/ ASCII85Decode / FlateDecode]
                *                  >>
                *              stream
                *              8; T1BB2"M7*!"psYBt1k\gY1T < D & tO]r* F7Hga*
                *              …Additional data(seven components for each table entry)…
                *              endstream
                *              endobj
                *
                *NOTE 5        As in the preceding examples, an Indexed colour space based on a DeviceN space is used to paint the grayscale image shown on the left in the plate with four colorants: black and three PANTONE spot colours. 
                *              The alternate colour space is a simple calibrated RGB. Thus, the DeviceN colour space has seven components: the four desired colorants plus the three components of the alternate space. 
                *              The example shows the image XObject (see 8.9.5, "Image Dictionaries") representing the quadtone image, followed by the colour space used to interpret the image data.
                */
                
            /*8.6.7 Overprint Control
            *
            *The graphics state contains an overprint parameter, controlled by the OP and op entries in a graphics state parameter dictionary. 
            *Overprint control is useful mainly on devices that produce true physical separations, but it is available on some composite devices as well. 
            *Although the operation of this parameter is device-dependent, it is described here rather than in the sub-clause on colour rendering, because it pertains to an aspect of painting in device colour spaces that is important to many applications.
            *
            *Any painting operation marks some specific set of device colorants, depending on the colour space in which the painting takes place.
            *In a Separation or DeviceN colour space, the colorants to be marked shall be specified explicitly; in a device or CIE-based colour space, they shall be implied by the process colour model of the output device(see clause 10, "Rendering"). 
            *The overprint parameter is a boolean flag that determines how painting operations affect colorants other than those explicitly or implicitly specified by the current colour space.
            *
            *If the overprint parameter is false(the default value), painting a colour in any colour space shall cause the corresponding areas of unspecified colorants to be erased(painted with a tint value of 0.0). 
            *The effect is that the colour at any position on the page is whatever was painted there last, which is consistent with the normal painting behaviour of the opaque imaging model.
            *
            *If the overprint parameter is true and the output device supports overprinting, erasing actions shall not be performed; anything previously painted in other colorants is left undisturbed. 
            *Consequently, the colour at a given position on the page may be a combined result of several painting operations in different colorants. 
            *The effect produced by such overprinting is device - dependent and is not defined here.
            *
            *NOTE 1    Not all devices support overprinting.Furthermore, many PostScript printers support it only when separations are being produced, and not for composite output.
            *
            *If overprinting is not supported, the value of the overprint parameter shall be ignored.
            *
            *An additional graphics state parameter, the overprint mode (PDF 1.3), shall affect the interpretation of a tint value of 0.0 for a colour component in a DeviceCMYK colour space when overprinting is enabled. 
            *This parameter is controlled by the OPM entry in a graphics state parameter dictionary; it shall have an effect only when the overprint parameter is true, as described above.
            *
            *When colours are specified in a DeviceCMYK colour space and the native colour space of the output device is also DeviceCMYK, each of the source colour components controls the corresponding device colorant directly. 
            *Ordinarily, each source colour component value replaces the value previously painted for the corresponding device colorant, no matter what the new value is; this is the default behaviour, specified by overprint mode 0.
            *
            *When the overprint mode is 1(also called nonzero overprint mode), a tint value of 0.0 for a source colour component shall leave the corresponding component of the previously painted colour unchanged.
            *The effect is equivalent to painting in a DeviceN colour space that includes only those components whose values are nonzero.
            *
            *EXAMPLE       If the overprint parameter is true and the overprint mode is 1, the operation
            *
            *              0.2 0.3 0.0 1.0 k
            *
            *              is equivalent to
            *               
            *              0.2 0.3 1.0 scn
            *
            *              in the colour space shown in this example.
            *
            *              10 0 obj                                            % Colour space
            *                  [ / DeviceN
            *                      [/ Cyan / Magenta / Black]
            *                       / DeviceCMYK
            *              15 0 R
            *                  ]
            *              endobj
            *              15 0 obj                                            % Tint transformation function
            *                  << / FunctionType 4
            *                     / Domain[0.0 1.0 0.0 1.0 0.0 1.0]
            *                     / Range[0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0]
            *                     / Length 13
            *                  >>
            *              stream
            *              { 0 exch}
            *              endstream
            *              endobj
            *
            *Nonzero overprint mode shall apply only to painting operations that use the current colour in the graphics state when the current colour space is DeviceCMYK (or is implicitly converted to DeviceCMYK; see 8.6.5.7, "Implicit Conversion of CIE-Based Colour Spaces"). 
            *It shall not apply to the painting of images or to any colours that are the result of a computation, such as those in a shading pattern or conversions from some other colour space. 
            *It also shall not apply if the device’s native colour space is not DeviceCMYK; in that case, source colours shall be converted to the device’s native colour space, and all components participate in the conversion, whatever their values.
            *
            *NOTE 2    This is shown explicitly in the alternate colour space and tint transformation function of the DeviceN colour space(see EXAMPLE 3 in 8.6.6, "Special Colour Spaces").
            *
            *See 11.7.4, "Overprinting and Transparency", for further discussion of the role of overprinting in the transparent imaging model.
            */

            /*8.6.8 Colour Operators
            *
            *Table 74 lists the PDF operators that control colour spaces and colour values. Also colour-related is the graphics state operator ri, listed in Table 57 and discussed under 8.6.5.8, "Rendering Intents". 
            *Colour operators may appear at the page description level or inside text objects (see Figure 9 in Annex L).
            *
            *Table 74 - Colour Operators
            *
            *      [Operands]              [Operator]              [Desscription]
            *
            *      name                    CS                      (PDF 1.1) Set the current colour space to use for stroking operations. The operand name shall be a name object. 
            *                                                      If the colour space is one that can be specified by a name and no additional parameters (DeviceGray, DeviceRGB, DeviceCMYK, and certain cases of Pattern), the name may be specified directly. 
            *                                                      Otherwise, it shall be a name defined in the ColorSpace subdictionary of the current resource dictionary (see 7.8.3, "Resource Dictionaries"); the associated value shall be an array describing the colour space (see 8.6.3, "Colour Space Families").
            *                                                      The names DeviceGray, DeviceRGB, DeviceCMYK, and Patternalways identify the corresponding colour spaces directly; they never refer to resources in the ColorSpace subdictionary.
            *                                                       
            *                                                      The CS operator shall also set the current stroking colour to its initial value, which depends on the colour space:
            *                                                      In a DeviceGray, DeviceRGB, CalGray, or CalRGB colour space, the initial colour shall have all components equal to 0.0.
            *                                                      In a DeviceCMYK colour space, the initial colour shall be[0.0 0.0 0.0 1.0].
            *                                                      In a Lab or ICCBased colour space, the initial colour shall have all components equal to 0.0 unless that falls outside the intervals specified by the space’s Range entry, in which case the nearest valid value shall be substituted.
            *                                                      In an Indexed colour space, the initial colour value shall be 0.
            *                                                      In a Separation or DeviceN colour space, the initial tint value shall be 1.0 for all colorants.
            *                                                      In a Pattern colour space, the initial colour shall be a pattern object that causes nothing to be painted.
            *
            *      name                    cs                      (PDF 1.1) Same as CS but used for nonstroking operations.
            *
            *      c(1)...c(n)             SC                      (PDF 1.1) Set the colour to use for stroking operations in a device, CIE-based (other than ICCBased), or Indexed colour space. 
            *                                                      
            *                                                      The number of operands required and their interpretation depends on the current stroking colour space:
            *                                                      For DeviceGray, CalGray, and Indexed colour spaces, one operand shall be required(n = 1).
            *                                                      For DeviceRGB, CalRGB, and Lab colour spaces, three operands shall be required(n = 3).
            *                                                      For DeviceCMYK, four operands shall be required(n = 4).
            *
            *      c(1)...c(n)             SCN                     (PDF 1.2) Same as SC but also supports Pattern, Separation, DeviceN and ICCBased colour spaces.
            *      c(1)...c(n) name        SCN                     If the current stroking colour space is a Separation, DeviceN, or ICCBased colour space, the operands c1…cn shall be numbers. 
            *                                                      The number of operands and their interpretation depends on the colour space.
            *                                                      If the current stroking colour space is a Pattern colour space, name shall be the name of an entry in the Pattern subdictionary of the current resource dictionary(see 7.8.3, "Resource Dictionaries"). 
            *                                                      For an uncoloured tiling pattern(PatternType = 1 and PaintType = 2), c1…cn shall be component values specifying a colour in the pattern’s underlying colour space. For other types of patterns, these operands shall not be specified.
            *
            *      c(1)...c(n)             sc                      (PDF 1.1) Same as SC but used for nonstroking operations.
            *
            *      c(1)...c(n)             scn                     (PDF 1.2) Same as SCN but used for nonstroking operations.
            *      c(1)...c(n) name        scn
            *
            *      gray                    G                       Set the stroking colour space to DeviceGray (or the DefaultGray colour space; see 8.6.5.6, "Default Colour Spaces") and set the gray level to use for stroking operations. gray shall be a number between 0.0 (black) and 1.0 (white).
            *
            *      r g b                   RG                      Set the stroking colour space to DeviceRGB (or the DefaultRGB colour space; see 8.6.5.6, "Default Colour Spaces") and set the colour to use for stroking operations. Each operand shall be a number between 0.0 (minimum intensity) and 1.0 (maximum intensity).
            *
            *      c m y k                 K                       Set the stroking colour space to DeviceCMYK (or the DefaultCMYKcolour space; see 8.6.5.6, "Default Colour Spaces") and set the colour to use for stroking operations. Each operand shall be a number between 0.0 (zero concentration) and 1.0 (maximum concentration). 
            *                                                      The behaviour of this operator is affected by the overprint mode (see 8.6.7, "Overprint Control").
            *
            *      c m y k                 k                       Same as K but used for nonstroking operations.
            *
            *Invoking operators that specify colours or other colour-related parameters in the graphics state is restricted in certain circumstances. 
            *This restriction occurs when defining graphical figures whose colours shall be specified separately each time they are used. 
            *
            *Specifically, the restriction applies in these circumstances:
            *
            *  •   In any glyph description that uses the d1 operator (see 9.6.5, "Type 3 Fonts")
            *
            *  •   In the content stream of an uncoloured tiling pattern(see 8.7.3.3, "Uncoloured Tiling Patterns")
            *
            *In these circumstances, the following actions cause an error:
            *
            *  •   Invoking any of the following operators:
            *
            *      CS      scn     K
            *      cs      G       k
            *      SC      g       ri
            *      SCN     RG      sh
            *      sc      rg
            *
            *  •   Invoking the gs operator with any of the following entries in the graphics state parameter dictionary:
            *
            *      TR      BG      UCR
            *      TR2     BG2     UCR2
            *      HT
            *
            *  •   Painting an image.
            *      However, painting an image mask(see 8.9.6.2, "Stencil Masking") shall be permitted because it does not specify colours; instead, it designates places where the current colour shall be painted.
            */

            public void getColourSpace(ref PDF_Color_Spaces pdf_color_spaces, ref string stream_hexa, int offset)
            {

            }

        }

        //8.7 Patterns
        public class Patterns
        {
            /*8.7.1 General
            *
            *Patterns come in two varieties:
            *
            *  •   Tiling patterns consist of a small graphical figure(called a pattern cell) that is replicated at fixed horizontal and vertical intervals to fill the area to be painted.
            *      The graphics objects to use for tiling shall be described by a content stream.
            *
            *  •   Shading patterns define a gradient fill that produces a smooth transition between colours across the area.
            *      The colour to use shall be specified as a function of position using any of a variety of methods.
            *
            *NOTE 1    When operators such as S(stroke), f(fill), and Tj(show text) paint an area of the page with the current colour, they ordinarily apply a single colour that covers the area uniformly.
            *          However, it is also possible to apply “paint” that consists of a repeating graphical figure or a smoothly varying colour gradient instead of a simple colour. 
            *          Such a repeating figure or smooth gradient is called a pattern.Patterns are quite general, and have many uses; for example, they can be used to create various graphical textures, such as weaves, brick walls, sunbursts, and similar geometrical and chromatic effects.
            *
            *NOTE 2    Older techniques such as defining a pattern by using character glyphs in a special font and painting them repeatedly with the Tj operator should not be used.
            *          Another technique, defining patterns as halftone screens, should not be used because the effects produced are device-dependent.
            *
            *Patterns shall be specified in a special family of colour spaces named Pattern. These spaces shall use pattern objects as the equivalent of colour values instead of the numeric component values used with other spaces.
            *A pattern object shall be a dictionary or a stream, depending on the type of pattern; the term pattern dictionary is used generically throughout this sub-clause to refer to either a dictionary object or the dictionary portion of a stream object. 
            *(Those pattern objects that are streams are specifically identified as such in the descriptions of particular pattern types; unless otherwise stated, they are understood to be simple dictionaries instead.) 
            *This sub-clause describes Pattern colour spaces and the specification of colour values within them.
            *
            *NOTE 3    See 8.6, "Colour Spaces", for information about colour spaces and colour values in general and 11.6.7, "Patterns and Transparency", for further discussion of the treatment of patterns in the transparent imaging model.
            */

            /*8.7.2 General Properties of Patterns
            *
            *A pattern dictionary contains descriptive information defining the appearance and properties of a pattern. 
            *All pattern dictionaries shall contain an entry named PatternType, whose value identifies the kind of pattern the dictionary describes: type 1 for a tiling pattern or type 2 for a shading pattern. 
            *The remaining contents of the dictionary depend on the pattern type and are detailed in the sub-clauses on individual pattern types.
            *
            *All patterns shall be treated as colours; a Pattern colour space shall be established with the CS or cs operator just like other colour spaces, and a particular pattern shall be installed as the current colour with the SCN or scn operator (see Table 74).
            *
            *A pattern’s appearance is described with respect to its own internal coordinate system.
            *Every pattern has a pattern matrix, a transformation matrix that maps the pattern’s internal coordinate system to the default coordinate system of the pattern’s parent content stream(the content stream in which the pattern is defined as a resource). 
            *The concatenation of the pattern matrix with that of the parent content stream establishes the pattern coordinate space, within which all graphics objects in the pattern shall be interpreted.
            *
            *NOTE 1    If a pattern is used on a page, the pattern appears in the Pattern subdictionary of that page’s resource dictionary, and the pattern matrix maps pattern space to the default (initial) coordinate space of the page.
            *          Changes to the page’s transformation matrix that occur within the page’s content stream, such as rotation and scaling, have no effect on the pattern; 
            *          it maintains its original relationship to the page no matter where on the page it is used.Similarly, if a pattern is used within a form XObject(see 8.10, "Form XObjects"), the pattern matrix maps pattern space to the form’s default user space(that is, the form coordinate space at the time the form is painted with the Do operator). 
            *          A pattern may be used within another pattern; the inner pattern’s matrix defines its relationship to the pattern space of the outer pattern.
            *
            *NOTE 2    PostScript allows a pattern to be defined in one context but used in another.
            *          For example, a pattern might be defined on a page(that is, its pattern matrix maps the pattern coordinate space to the user space of the page) but be used in a form on that page, so that its relationship to the page is independent of each individual placement of the form.
            *          PDF does not support this feature; in PDF, all patterns shall be local to the context in which they are defined.
            */

            /*8.7.3 Tiling Patterns
            */

            /*8.7.3.1 General
            *
            *A tiling pattern consists of a small graphical figure called a pattern cell.
            *Painting with the pattern replicates the cell at fixed horizontal and vertical intervals to fill an area. 
            *The effect is as if the figure were painted on the surface of a clear glass tile, identical copies of which were then laid down in an array covering the area and trimmed to its boundaries. 
            *This process is called tiling the area.
            *
            *The pattern cell can include graphical elements such as filled areas, text, and sampled images.
            *Its shape need not be rectangular, and the spacing of tiles can differ from the dimensions of the cell itself.
            *When performing painting operations such as S (stroke)or f(fill), the conforming reader shall paint the cell on the current page as many times as necessary to fill an area. 
            *The order in which individual tiles(instances of the cell) are painted is unspecified and unpredictable; figures on adjacent tiles should not overlap.
            *
            *The appearance of the pattern cell shall be defined by a content stream containing the painting operators needed to paint one instance of the cell.
            *Besides the usual entries common to all streams(see Table 5), this stream’s dictionary may contain the additional entries listed in Table 75.
            *
            *Table 75 - Additional Entries Specific to a Type 1 Pattern Dictionary
            *
            *          [Key]               [Type]                  [Value]
            *
            *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Pattern for a pattern dictionary.
            *
            *          PatternType         integer                 (Required) A code identifying the type of pattern that this dictionary describes; shall be 1 for a tiling pattern.
            *
            *          PaintType           integer                 (Required) A code that determines how the colour of the pattern cell shall be specified:
            *              
            *                                                      a) Coloured tiling pattern. The pattern’s content stream shall specifythe colours used to paint the pattern cell. 
            *                                                      When the content stream begins execution, the current colour is the one that was initially in effect in the pattern’s parent content stream. 
            *                                                      This is similar to the definition of the pattern matrix; see 8.7.2, "General Properties of Patterns".
            *
            *                                                      b) Uncoloured tiling pattern.The pattern’s content stream shall not specify any colour information.
            *                                                      Instead, the entire pattern cell is painted with a separately specified colour each time the pattern is used.
            *                                                      Essentially, the content stream describes a stencil through which the current colour shall be poured. 
            *                                                      The content stream shall not invoke operators that specify colours or other colour - related parameters in the graphics state; otherwise, an error occurs(see 8.6.8, "Colour Operators").
            *                                                      The content stream may paint an image mask, however, since it does not specify any colour information(see 8.9.6.2, "Stencil Masking").
            *
            *          TilingType          integer                 (Required) A code that controls adjustments to the spacing of tiles relative to the device pixel grid:
            *
            *                                                      a) Constant spacing. Pattern cells shall be spaced consistently—that is, by a multiple of a device pixel.
            *                                                      To achieve this, the conforming reader may need to distort the pattern cell slightly by making small adjustments to XStep, YStep, and the transformation matrix. 
            *                                                      The amount of distortion shall not exceed 1 device pixel.
            *
            *                                                      b) No distortion. The pattern cell shall not be distorted, but the spacing between pattern cells may vary by as much as 1 device pixel, both horizontally and vertically, when the pattern is painted.
            *                                                      This achieves the spacing requested by XStep and YStep on average but not necessarily for each individual pattern cell.
            *                                                      
            *                                                      c) Constant spacing and faster tiling.Pattern cells shall be spaced consistently as in tiling type 1 but with additional distortion permitted to enable a more efficient implementation.
            *
            *          BBox                rectangle               (Required) An array of four numbers in the pattern coordinate system giving the coordinates of the left, bottom, right, and top edges, respectively, of the pattern cell’s bounding box. These boundaries shall be used to clip the pattern cell.
            *
            *          XStep               number                  (Required) The desired horizontal spacing between pattern cells, measured in the pattern coordinate system.
            *
            *          YStep               number                  (Required) The desired vertical spacing between pattern cells, measured in the pattern coordinate system.
            *
            *                                                      NOTE    XStep and YStep may differ from the dimensions of the pattern cell implied by the BBox entry.This allows tiling with irregularly shaped figures.
            *                                                              XStep and YStep may be either positive or negative but shall not bezero.
            *
            *          Resources           dictionary              (Required) A resource dictionary that shall contain all of the named resources required by the pattern’s content stream (see 7.8.3, "Resource Dictionaries").
            *
            *          Matrix              array                   (Optional) An array of six numbers specifying the pattern matrix (see 8.7.2, "General Properties of Patterns"). Default value: the identity matrix [1 0 0 1 0 0].
            *
            *The pattern dictionary’s BBox, XStep, and YStep values shall be interpreted in the pattern coordinate system, and the graphics objects in the pattern’s content stream shall be defined with respect to that coordinate system. 
            *The placement of pattern cells in the tiling is based on the location of one key pattern cell, which is then displaced by multiples of XStep and YStep to replicate the pattern. 
            *The origin of the key pattern cell coincides with the origin of the pattern coordinate system. 
            *The phase of the tiling can be controlled by the translation components of the Matrix entry in the pattern dictionary.
            *
            *Prior to painting with a tiling pattern, the conforming writer shall establish the pattern as the current colour in the graphics state.
            *Subsequent painting operations tile the painted areas with the pattern cell described by the pattern’s content stream.
            *To obtain the pattern cell, the conforming reader shall perform these steps:
            *
            *  a)  Saves the current graphics state(as if by invoking the q operator)
            *
            *  b)  Installs the graphics state that was in effect at the beginning of the pattern’s parent content stream, with the current transformation matrix altered by the pattern matrix as described in 8.7.2, "General Properties of Patterns"
            *
            *  c)  Paints the graphics objects specified in the pattern’s content stream
            *
            *  d)  Restores the saved graphics state(as if by invoking the Q operator)
            *
            *NOTE      The pattern’s content stream should not set any of the device-dependent parameters in the graphics state(see Table 53) because it may result in incorrect output.
            */

            /*8.7.3.2 Coloured Tiling Patterns
            *
            *A coloured tiling pattern is a pattern whose colour is self - contained.In the course of painting the pattern cell, the pattern’s content stream explicitly sets the colour of each graphical element it paints.
            *A single pattern cell may contain elements that are painted different colours; it may also contain sampled grayscale or colour images.
            *This type of pattern is identified by a pattern type of 1 and a paint type of 1 in the pattern dictionary.
            *
            *When the current colour space is a Pattern space, a coloured tiling pattern shall be selected as the current colour by supplying its name as the single operand to the SCN or scn operator. 
            *This name shall be the key of an entry in the Pattern subdictionary of the current resource dictionary(see 7.8.3, "Resource Dictionaries"), whose value shall be the stream object representing the pattern. 
            *Since the pattern defines its own colour information, no additional operands representing colour components shall be specified to SCN or scn.
            *
            *EXAMPLE 1     If P1 is the name of a pattern resource in the current resource dictionary, the following code establishes it as the current nonstroking colour:
            *
            *              / Pattern cs
            *              / P1 scn
            *
            *NOTE 1    Subsequent executions of nonstroking painting operators, such as f (fill), Tj(show text), or Do(paint external object) with an image mask, use the designated pattern to tile the areas to be painted.
            *
            *NOTE 2    The following defines a page(object 5) that paints three circles and a triangle using a coloured tiling pattern(object 15) over a yellow background. 
            *          The pattern consists of the symbols for the four suits of playing cards(spades, hearts, diamonds, and clubs), which are character glyphs taken from the ZapfDingbats font(see D.6, "ZapfDingbats Set and Encoding"); the pattern’s content stream specifies the colour of each glyph.
            *          Figure L.8in Annex L shows the results.
            *
            *EXAMPLE 2     5 0 obj                                         % Page object
            *                  << / Type / Page
            *                     / Parent 2 0 R
            *                     / Resources 10 0 R
            *                     / Contents 30 0 R
            *                     / CropBox[0 0 225 225]
            *                  >>
            *              endobj
            *              10 0 obj                                        % Resource dictionary for page
            *                  << / Pattern << / P1 15 0 R >>
            *                  >>
            *              endobj
            *              15 0 obj                                        % Pattern definition
            *                  << / Type / Pattern
            *                     / PatternType 1 % Tiling pattern
            *                     / PaintType 1 % Coloured
            *                     / TilingType 2
            *                     / BBox[0 0 100 100]
            *                     / XStep 100
            *                     / YStep 100
            *                     / Resources 16 0 R
            *                     / Matrix[0.4 0.0 0.0 0.4 0.0 0.0]
            *                     / Length 183
            *                  >>
            *              stream
            *              BT                                              % Begin text object
            *                  / F1 1 Tf                                   % Set text font and size
            *                  64 0 0 64 7.1771 2.4414 Tm                  % Set text matrix
            *                  0 Tc                                        % Set character spacing
            *                  0 Tw                                        % Set word spacing
            *                  1.0 0.0 0.0 rg                              % Set nonstroking colour to red
            *                  (\001) Tj                                   % Show spade glyph
            *                  0.7478 - 0.007 TD                           % Move text position
            *                  0.0 1.0 0.0 rg                              % Set nonstroking colour to green
            *                  (\002) Tj                                   % Show heart glyph
            *                  -0.7323 0.7813 TD                           % Move text position
            *                  0.0 0.0 1.0 rg                              % Set nonstroking colour to blue
            *                  (\003) Tj                                   % Show diamond glyph
            *                  0.6913 0.007 TD                             % Move text position
            *                  0.0 0.0 0.0 rg                              % Set nonstroking colour to black
            *                  (\004) Tj                                   % Show club glyph
            *              ET                                              % End text object
            *              endstream
            *              endobj
            *              16 0 obj                                        % Resource dictionary for pattern
            *                  << / Font << / F1 20 0 R >>
            *                  >>
            *              endobj
            *              20 0 obj                                        % Font for pattern
            *                  << / Type / Font
            *                     / Subtype / Type1
            *                     / Encoding 21 0 R
            *                     / BaseFont / ZapfDingbats
            *                  >>
            *              endobj
            *              21 0 obj                                        % Font encoding
            *                  << / Type / Encoding
            *                     / Differences[1 / a109 / a110 / a111 / a112]
            *                  >>
            *              endobj
            *              30 0 obj                                        % Contents of page
            *                  << / Length 1252 >>
            *              stream
            *              0.0 G                                           % Set stroking colour to black
            *              1.0 1.0 0.0 rg                                  % Set nonstroking colour to yellow
            *              25 175 175 - 150 re                             % Construct rectangular path
            *              f % Fill path
            *              / Pattern cs                                    % Set pattern colour space
            *              / P1 scn                                        % Set pattern as nonstroking colour
            *              99.92 49.92 m                                   % Start new path
            *              99.92 77.52 77.52 99.92 49.92 99.92 c           % Construct lower - left circle
            *              22.32 99.92 - 0.08 77.52 -0.08 49.92 c
            *              -0.08 22.32 22.32 - 0.08 49.92 -0.08 c
            *              77.52 -0.08 99.92 22.32 99.92 49.92 c
            *              B                                               % Fill and stroke path
            *              224.96 49.92 m                                  % Start new path
            *              224.96 77.52 202.56 99.92 174.96 99.92 c        % Construct lower - right circle
            *              147.36 99.92 124.96 77.52 124.96 49.92 c
            *              124.96 22.32 147.36 -0.08 174.96 -0.08 c
            *              202.56 -0.08 224.96 22.32 224.96 49.92 c
            *              B                                               % Fill and stroke path
            *              87.56 201.70 m                                  % Start new path
            *              63.66 187.90 55.46 157.32 69.26 133.40 c        % Construct upper circle
            *              83.06 109.50 113.66 101.30 137.56 115.10 c
            *              161.46 128.90 169.66 159.50 155.86 183.40 c
            *              142.06 207.30 111.46 215.50 87.56 201.70 c
            *              B                                               % Fill and stroke path
            *              50 50 m                                         % Start new path
            *              175 50 l                                        % Construct triangular path
            *              112.5 158.253 l
            *              b                                               % Close, fill, and stroke path
            *      endstream
            *      endobj
            *
            *NOTE 3    Several features of EXAMPLE 2 in this sub-clause are noteworthy:
            *
            *          The three circles and the triangle are painted with the same pattern. 
            *          The pattern cells align, even though the circles and triangle are not aligned with respect to the pattern cell. 
            *          For example, the position of the blue diamonds varies relative to the three circles.
            *
            *          The pattern cell does not completely cover the tile: it leaves the spaces between the glyphs unpainted. 
            *          When the tiling pattern is used as a colour, the existing background(the yellow rectangle) shows through these unpainted areas.
            */

            /*8.7.3.3 Uncoloured Tiling Patterns
            *
            *An uncoloured tiling pattern is a pattern that has no inherent colour: the colour shall be specified separately whenever the pattern is used.It provides a way to tile different regions of the page with pattern cells having the same shape but different colours.
            *This type of pattern shall be identified by a pattern type of 1 and a paint type of 2 in the pattern dictionary.The pattern’s content stream shall not explicitly specify any colours; it may paint an image mask(see 8.9.6.2, "Stencil Masking") but no other kind of image.
            *
            *A Pattern colour space representing an uncoloured tiling pattern shall have a parameter: an object identifying the underlying colour space in which the actual colour of the pattern shall be specified. 
            *The underlying colour space shall be given as the second element of the array that defines the Pattern colour space.
            *
            *EXAMPLE 1    The array
            *
            *              [/ Pattern / DeviceRGB]
            *
            *              defines a Pattern colour space with DeviceRGB as its underlying colour space.
            *
            *NOTE      The underlying colour space cannot be another Pattern colour space.
            *
            *Operands supplied to the SCN or scn operator in such a colour space shall include a colour value in the underlying colour space, specified by one or more numeric colour components, as well as the name of a pattern object representing an uncoloured tiling pattern.
            *
            *EXAMPLE 2     If the current resource dictionary(see 7.8.3, "Resource Dictionaries") defines Cs3 as the name of a ColorSpace resource 
            *              whose value is the Pattern colour space shown above and P2 as a Patternresource denoting an uncoloured tiling pattern, the code
            *
            *              / Cs3 cs
            *              0.30 0.75 0.21 / P2 scn
            *
            *
            *              establishes Cs3 as the current nonstroking colour space and P2 as the current nonstroking colour, to be painted in the colour represented by the specified components in the DeviceRGB colour space. 
            *              Subsequent executions of nonstroking painting operators, such as f (fill), Tj(show text), and Do(paint external object) with an image mask, use the designated pattern and colour to tile the areas to be painted. 
            *              The same pattern can be used repeatedly with a different colour each time.
            *
            *EXAMPLE 3     This example is similar to EXAMPLE 2 in 8.7.3.2, except that it uses an uncoloured tiling pattern to paint the three circles and the triangle, each in a different colour(see Figure L.9 in Annex L).
            *              To do so, it supplies four operands each time it invokes the scn operator: three numbers denoting the colour components in the underlying DeviceRGB colour space, along with the name of the pattern.
            *
            *              5 0 obj                                             % Page object
            *                  << / Type / Page
            *                     / Parent 2 0 R
            *                     / Resources 10 0 R
            *                     / Contents 30 0 R
            *                     / CropBox[0 0 225 225]
            *                  >>
            *              endobj
            *              10 0 obj                                            % Resource dictionary for page
            *                  << / ColorSpace << / Cs12 12 0 R >>
            *                     / Pattern << / P1 15 0 R >>
            *                  >>
            *              endobj
            *              12 0 obj                                            % Colour space
            *                  [/ Pattern / DeviceRGB]
            *              endobj
            *              15 0 obj                                            % Pattern definition
            *                  << / Type / Pattern
            *                     / PatternType 1                              % Tiling pattern
            *                     / PaintType 2                                % Uncoloured
            *                     / TilingType 2
            *                     / BBox[0 0 100 100]
            *                     / XStep 100
            *                     / YStep 100
            *                     / Resources 16 0 R
            *                     / Matrix[0.4 0.0 0.0 0.4 0.0 0.0]
            *                     / Length 127
            *                  >>
            *
            *              stream
            *                  BT                                              % Begin text object
            *                      / F1 1 Tf                                   % Set text font and size
            *                      64 0 0 64 7.1771 2.4414 Tm                  % Set text matrix
            *                      0 Tc                                        % Set character spacing
            *                      0 Tw                                        % Set word spacing
            *                      (\001) Tj                                   % Show spade glyph
            *                      0.7478 -0.007 TD                            % Move text position
            *                      (\002) Tj                                   % Show heart glyph
            *                      -0.7323 0.7813 TD                           % Move text position
            *                      (\003) Tj                                   % Show diamond glyph
            *                      0.6913 0.007 TD                             % Move text position
            *                      (\004) Tj                                   % Show club glyph
            *                      ET                                          % End text object
            *              endstream
            *              endobj
            *                      16 0 obj                                    % Resource dictionary for pattern
            *                          << / Font << / F1 20 0 R >>
            *                          >>
            *              endobj
            *              20 0 obj                                            % Font for pattern
            *                  << / Type / Font
            *                     / Subtype / Type1
            *                     / Encoding 21 0 R
            *                     / BaseFont / ZapfDingbats
            *                  >>
            *              endobj
            *              21 0 obj                                            % Font encoding
            *                  << / Type / Encoding
            *                     / Differences[1 / a109 / a110 / a111 / a112]
            *                  >>
            *              endobj
            *              30 0 obj                                            % Contents of page
            *                  << / Length 1316 >>
            *              stream
            *                  0.0 G                                           % Set stroking colour to black
            *                  1.0 1.0 0.0 rg                                  % Set nonstroking colour to yellow
            *                  25 175 175 - 150 re                             % Construct rectangular path
            *                  f                                               % Fill path
            *                  / Cs12 cs                                       % Set pattern colour space
            *                  0.77 0.20 0.00 / P1 scn                         % Set nonstroking colour and pattern
            *                  99.92 49.92 m                                   % Start new path
            *                  99.92 77.52 77.52 99.92 49.92 99.92 c           % Construct lower - left circle
            *                  22.32 99.92 -0.08 77.52 -0.08 49.92 c
            *                  -0.08 22.32 22.32 -0.08 49.92 -0.08 c
            *                  77.52 -0.08 99.92 22.32 99.92 49.92 c
            *                  B                                               % Fill and stroke path
            *                  0.2 0.8 0.4 / P1 scn                            % Change nonstroking colour
            *                  224.96 49.92 m                                  % Start new path
            *                  224.96 77.52 202.56 99.92 174.96 99.92 c        % Construct lower - right circle
            *                  147.36 99.92 124.96 77.52 124.96 49.92 c
            *                  124.96 22.32 147.36 -0.08 174.96 -0.08 c
            *                  202.56 -0.08 224.96 22.32 224.96 49.92 c
            *                  B                                               % Fill and stroke path
            *                  0.3 0.7 1.0 / P1 scn                            % Change nonstroking colour
            *                  87.56 201.70 m                                  % Start new path
            *                  63.66 187.90 55.46 157.30 69.26 133.40 c        % Construct upper circle
            *                  83.06 109.50 113.66 101.30 137.56 115.10 c
            *                  161.46 128.90 169.66 159.50 155.86 183.40 c
            *                  142.06 207.30 111.46 215.50 87.56 201.70 c
            *                  B                                               % Fill and stroke path
            *                  0.5 0.2 1.0 /P1 scn                             % Change nonstroking colour
            *                  50 50 m                                         % Start new path
            *                  175 50 l                                        % Construct triangular path
            *                  112.5 158.253 l
            *                  b                                               % Close, fill, and stroke path
            *              endstream
            *              endobj
            */

            /*8.7.4 Shading Patterns
            */

            /*8.7.4.1 General
            *
            *Shading patterns (PDF 1.3) provide a smooth transition between colours across an area to be painted, independent of the resolution of any particular output device and without specifying the number of steps in the colour transition. 
            *Patterns of this type shall be described by pattern dictionaries with a pattern type of 2. Table 76 shows the contents of this type of dictionary.
            *
            *Table 76 - Entries in a Type 2 Pattern Dictionary
            *
            *          [Key]               [Type]                  [Value]
            *
            *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Pattern for a pattern dictionary.
            *
            *          PatternType         integer                 (Required) A code identifying the type of pattern that this dictionary describes; shall be 2 for a shading pattern.
            *
            *          Shading             dictionary              (Required) A shading object (see below) defining the shading pattern’s gradient fill.
            *                              or stream               The contents of the dictionary shall consist of the entries in Table 78 and those in one of Tables 79 to 84.
            *
            *          Matrix              array                   (Optional) An array of six numbers specifying the pattern matrix (see 8.7.2, "General Properties of Patterns"). Default value: the identity matrix [1 0 0 1 0 0].
            *
            *          ExtGState           dictionary              (Optional) A graphics state parameter dictionary (see 8.4.5, "Graphics State Parameter Dictionaries") containing graphics state parameters to be put into effect temporarily while the shading pattern is painted. 
            *                                                      Any parameters that are so specified shall be inherited from the graphics state that was in effect at the beginning of the content stream in which the pattern is defined as a resource.
            *
            *The most significant entry is Shading, whose value shall be a shading object defining the properties of the shading pattern’s gradient fill. 
            *This is a complex “paint” that determines the type of colour transition the shading pattern produces when painted across an area. 
            *A shading object shall be a dictionary or a stream, depending on the type of shading; the term shading dictionary is used generically throughout this sub-clause to refer to either a dictionary object or the dictionary portion of a stream object. 
            *(Those shading objects that are streams are specifically identified as such in the descriptions of particular shading types; unless otherwise stated, they are understood to be simple dictionaries instead.)
            *
            *By setting a shading pattern as the current colour in the graphics state, a PDF content stream may use it with painting operators such as f (fill), S(stroke), Tj(show text), or Do(paint external object) with an image mask to paint a path, character glyph, or mask with a smooth colour transition.
            *When a shading is used in this way, the geometry of the gradient fill is independent of that of the object being painted.
            */

            /*8.7.4.2 Shading Operator
            *
            *When the area to be painted is a relatively simple shape whose geometry is the same as that of the gradient fill itself, the sh operator may be used instead of the usual painting operators. sh accepts a shading dictionary as an operand and applies the corresponding gradient fill directly to current user space. 
            *This operator does not require the creation of a pattern dictionary or a path and works without reference to the current colour in the graphics state. 
            *Table 77 describes the sh operator.
            *
            *NOTE      Patterns defined by type 2 pattern dictionaries do not tile. 
            *          To create a tiling pattern containing a gradient fill, invoke the sh operator from within the content stream of a type 1(tiling) pattern.
            *
            *Table 77 - Shading Operator
            *
            *          [Operands]              [Operator]              [Description]
            *
            *          name                    sh                      (PDF 1.3) Paint the shape and colour shading described by a shading dictionary, subject to the current clipping path. The current colour in the graphics state is neither used nor altered. The effect is different from that of painting a path using a shading pattern as the current colour.
            *
            *                                                          name is the name of a shading dictionary resource in the Shadingsubdictionary of the current resource dictionary (see 7.8.3, "Resource Dictionaries"). All coordinates in the shading dictionary are interpreted relative to the current user space.
            *                                                          (By contrast, when a shading dictionary is used in a type 2 pattern, the coordinates are expressed in pattern space.) All colours are interpreted in the colour space identified by the shading dictionary’s ColorSpace entry (see Table 78). The Background entry, if present, is ignored.
            *
            *                                                          This operator should be applied only to bounded or geometrically defined shadings. If applied to an unbounded shading, it paints the shading’s gradient fill across the entire clipping region, which may be time-consuming.
            *
            */

            /*8.7.4.3 Shading Dictionaries
            *
            *A shading dictionary specifies details of a particular gradient fill, including the type of shading to be used, the geometry of the area to be shaded, and the geometry of the gradient fill. 
            *Various shading types are available, depending on the value of the dictionary’s ShadingType entry:
            *
            *  •   Function - based shadings(type 1) define the colour of every point in the domain using a mathematical function(not necessarily smooth or continuous).
            *
            *  •   Axial shadings(type 2) define a colour blend along a line between two points, optionally extended beyond the boundary points by continuing the boundary colours.
            *
            *  •   Radial shadings(type 3) define a blend between two circles, optionally extended beyond the boundary circles by continuing the boundary colours. 
            *      This type of shading is commonly used to represent three - dimensional spheres and cones.
            *
            *  •   Free - form Gouraud - shaded triangle meshes(type 4) define a common construct used by many three-dimensional applications to represent complex coloured and shaded shapes. 
            *      Vertices are specified in free - form geometry.
            *
            *  •   Lattice - form Gouraud - shaded triangle meshes(type 5) are based on the same geometrical construct as type 4 but with vertices specified as a pseudorectangular lattice.
            *
            *  •   Coons patch meshes(type 6) construct a shading from one or more colour patches, each bounded by four cubic Bézier curves.
            *
            *  •   Tensor - product patch meshes(type 7) are similar to type 6 but with additional control points in each patch, affording greater control over colour mapping.
            *
            *NOTE 1        Table 78 shows the entries that all shading dictionaries share in common; entries specific to particular shading types are described in the relevant sub-clause.
            *
            *NOTE 2        The term target coordinate space, used in many of the following descriptions, refers to the coordinate space into which a shading is painted.
            *              For shadings used with a type 2 pattern dictionary, this is the pattern coordinate space, discussed in 8.7.2, "General Properties of Patterns". 
            *              For shadings used directly with the shoperator, it is the current user space.
            *
            *Table 78 - Entries Common to All Shading Dictionaries
            *
            *          [Key]               [Type]                  [Value]
            *
            *          ShadingType         integer                 (Required) The shading type:
            *                                                      1 Function - based shading
            *                                                      2 Axial shading
            *                                                      3 Radial shading
            *                                                      4 Free - form Gouraud - shaded triangle mesh
            *                                                      5 Lattice - form Gouraud - shaded triangle mesh
            *                                                      6 Coons patch mesh
            *                                                      7 Tensor - product patch mesh
            *
            *          ColorSpace          name or                 (Required) The colour space in which colour values shall beexpressed. This may be any device, CIE-based, or special colour space except a Pattern space. See 8.7.4.4, "Colour Space: Special Considerations" for further information.
            *                              array
            *
            *          Background          array                   (Optional) An array of colour components appropriate to the colour space, specifying a single background colour value. If present, this colour shall be used, before any painting operation involving the shading, to fill those portions of the area to be painted that lie outside the bounds of the shading object.
            *
            *                                                      NOTE    In the opaque imaging model, the effect is as if the painting operation were performed twice: first with the background colour and then with the shading.
            *
            *                                                      NOTE    The background colour is applied only when the shading is used as part of a shading pattern, not when it is painted directly with the sh operator.
            *
            *          BBox                rectangle               (Optional) An array of four numbers giving the left, bottom, right, and top coordinates, respectively, of the shading’s bounding box. The coordinates shall be interpreted in the shading’s target coordinate space.
            *                                                      If present, this bounding box shall be applied as a temporary clipping boundary when the shading is painted, in addition to the current clipping path and any other clipping boundaries in effect at that time.
            *
            *          AntiAlias           boolean                 (Optional) A flag indicating whether to filter the shading function to prevent aliasing artifacts.
            *
            *                                                      NOTE    The shading operators sample shading functions at a rate determined by the resolution of the output device. 
            *                                                              Aliasing can occur if the function is not smooth—that is, if it has a high spatial frequency relative to the sampling rate. Anti-aliasing can be computationally expensive and is usually unnecessary, since most shading functions are smooth enough or are sampled at a high enough frequency to avoid aliasing effects. 
            *                                                              Anti -aliasing may not be implemented on some output devices, in which case this flag is ignored.
            *                                                      Default value: false.      
            *
            *Shading types 4 to 7 shall be defined by a stream containing descriptive data characterizing the shading’s gradient fill. 
            *In these cases, the shading dictionary is also a stream dictionary and may contain any of the standard entries common to all streams (see Table 5). 
            *In particular, shall include a Length entry.
            *
            *In addition, some shading dictionaries also include a Function entry whose value shall be a function object(dictionary or stream) defining how colours vary across the area to be shaded. 
            *In such cases, the shading dictionary usually defines the geometry of the shading, and the function defines the colour transitions across that geometry. 
            *The function is required for some types of shading and optional for others. Functions are described in detail in 7.10, "Functions".
            *
            *NOTE 3    Discontinuous colour transitions, or those with high spatial frequency, may exhibit aliasing effects when painted at low effective resolutions.
            */
            
            /*8.7.4.4 Colour Space: Special Considerations
            */

            /*8.7.4.4.1 General
            *
            *Conceptually, a shading determines a colour value for each individual point within the area to be painted. 
            *In practice, however, the shading may actually be used to compute colour values only for some subset of the points in the target area, with the colours of the intervening points determined by interpolation between the ones computed. 
            *Conforming readers are free to use this strategy as long as the interpolated colour values approximate those defined by the shading to within the smoothness tolerance specified in the graphics state (see 10.6.3, "Smoothness Tolerance"). 
            *The ColorSpace entry common to all shading dictionaries not only defines the colour space in which the shading specifies its colour values but also determines the colour space in which colour interpolation is performed.
            *
            *NOTE 1    Some types of shading(4 to 7) perform interpolation on a parametric value supplied as input to the shading’s colour function, as described in the relevant sub - clause.
            *          This form of interpolation is conceptually distinct from the interpolation described here, which operates on the output colour values produced by the colour function and takes place within the shading’s target colour space.
            *
            *Gradient fills between colours defined by most shadings may be implemented using a variety of interpolation algorithms, and these algorithms may be sensitive to the characteristics of the colour space.
            *
            *NOTE 2    Linear interpolation, for example, may have observably different results when applied in a DeviceCMYK colour space than in a Lab colour space, even if the starting and ending colours are perceptually identical.
            *          The difference arises because the two colour spaces are not linear relative to each other.
            *
            *Shadings shall be rendered according to the following rules:
            *
            *  •   If ColorSpace is a device colour space different from the native colour space of the output device, colour values in the shading shall be converted to the native colour space using the standard conversion formulas described in 10.3, "Conversions among Device Colour Spaces". 
            *      To optimize performance, these conversions may take place at any time (before or after any interpolation on the colour values in the shading). 
            *      Thus, shadings defined with device colour spaces may have colour gradient fills that are less accurate and somewhat device-dependent. 
            *      (This does not apply to axial and radial shadings—shading types 2 and 3—because those shading types perform gradient fill calculations on a single variable and then convert to parametric colours.)
            *
            *  •   If ColorSpace is a CIE - based colour space, all gradient fill calculations shall be performed in that space. 
            *      Conversion to device colours shall occur only after all interpolation calculations have been performed. 
            *      Thus, the colour gradients are device - independent for the colours generated at each point.
            *
            *  •   If ColorSpace is a Separation or DeviceN colour space, a colour conversion(to the alternate colour space) occurs only if one or more of the specified colorants is not supported by the device.
            *      In that case, gradient fill calculations shall be performed in the designated Separation or DeviceN colour space before conversion to the alternate space.
            *      Thus, nonlinear tint transformation functions shall be accommodated for the best possible representation of the shading.
            *
            *  •   If ColorSpace is an Indexed colour space, all colour values specified in the shading shall be immediately converted to the base colour space.
            *      Depending on whether the base colour space is a device or CIE - based space, gradient fill calculations shall be performed as stated above.
            *      Interpolation shall never occur in an Indexed colour space, which is quantized and therefore inappropriate for calculations that assume a continuous range of colours. 
            *      For similar reasons, an Indexed colour space shall not be used in any shading whose colour values are generated by a function; this rule applies to any shading dictionary that contains a Function entry.
            */

            /*8.7.4.5 Shading Types
            */

            /*8.7.4.5.1 General
            *
            *In addition to the entries listed in Table 78, all shading dictionaries have entries specific to the type of shading they represent, as indicated by the value of their ShadingType entry.
            *The following sub - clauses describe the available shading types and the dictionary entries specific to each.
            */

            /*8.7.4.5.2 Type 1(Function - Based) Shadings
            *
            *In Type 1(function - based) shadings, the colour at every point in the domain is defined by a specified mathematical function. 
            *The function need not be smooth or continuous. This type is the most general of the available shading types and is useful for shadings that cannot be adequately described with any of the other types.Table 79 shows the shading dictionary entries specific to this type of shading, in addition to those common to all shading dictionaries(see Table 78).
            *
            *This type of shading shall not be used with an Indexed colour space.
            *
            *Table 79 - Additional Entries Specific to a Type 1 Shading Dictionary
            *
            *          [Key]                   [Type]                      [Value]
            *
            *          Domain                  array                       (Optional) An array of four numbers [xmin xmax ymin ymax] specifying the rectangular domain of coordinates over which the colour function(s) are defined. Default value: [0.0 1.0 0.0 1.0].
            *
            *          Matrix                  array                       (Optional) An array of six numbers specifying a transformation matrix mapping the coordinate space specified by the Domain entry into the shading’s target coordinate space.
            *                                  
            *                                                              NOTE    To map the domain rectangle [0.0 1.0 0.0 1.0] to a 1-inch square with lower-left corner at coordinates (100, 100) in default user space, the Matrix value would be [72 0 0 72 100 100].
            *
            *                                                              Default value: the identity matrix[1 0 0 1 0 0].
            *
            *          Function                function                    (Required) A 2-in, n-out function or an array of n 2-in, 1-out functions (where n is the number of colour components in the shading dictionary’s colour space). Each function’s domain shall be a superset of that of the shading dictionary. 
            *                                                              If the value returned by the function for a given colour component is out of range, it shall be adjusted to the nearest valid value.
            *
            *The domain rectangle (Domain) establishes an internal coordinate space for the shading that is independent of the target coordinate space in which it shall be painted. 
            *The colour function(s) (Function) specify the colour of the shading at each point within this domain rectangle. The transformation matrix (Matrix) then maps the domain rectangle into a corresponding rectangle or parallelogram in the target coordinate space. 
            *Points within the shading’s bounding box (BBox) that fall outside this transformed domain rectangle shall be painted with the shading’s background colour (Background); if the shading dictionary has no Background entry, such points shall be left unpainted. 
            *If the function is undefined at any point within the declared domain rectangle, an error may occur, even if the corresponding transformed point falls outside the shading’s bounding box.
            */

            /*8.7.4.5.3 Type 2 (Axial) Shadings
            *
            *Type 2 (axial) shadings define a colour blend that varies along a linear axis between two endpoints and extends indefinitely perpendicular to that axis. 
            *The shading may optionally be extended beyond either or both endpoints by continuing the boundary colours indefinitely. 
            *Table 80 shows the shading dictionary entries specific to this type of shading, in addition to those common to all shading dictionaries (see Table 78).
            *
            *This type of shading shall not be used with an Indexed colour space.
            *
            *Table 80 - Additional Entries Specific to a Type 2 Shading Dictionary
            *
            *          [Key]               [Type]              [Value]
            *
            *          Coords              array               (Required) An array of four numbers [x0 y0 x1 y1] specifying the starting and ending coordinates of the axis, expressed in the shading’s target coordinate space.
            *
            *          Domain              array               (Optional) An array of two numbers [t0 t1] specifying the limiting values of a parametric variable t. The variable is considered to vary linearly between these two values as the colour gradient varies between the starting and ending points of the axis. 
            *                                                  The variable t becomes the input argument to the colour function(s). Default value: [0.0 1.0].
            *
            *          Function            function            (Required) A 1-in, n-out function or an array of n 1-in, 1-out functions (where n is the number of colour components in the shading dictionary’s colour space). The function(s) shall be called with values of the parametric variable t in the domain defined by the Domain entry. Each function’s domain shall be a superset of that of the shading dictionary. 
            *                                                  If the value returned by the function for a given colour component is out of range, it shall be adjusted to the nearest valid value.
            *
            *          Extend              array               (Optional) An array of two boolean values specifying whether to extend the shading beyond the starting and ending points of the axis, respectively. Default value: [false false].
            *
            *The colour blend shall be accomplished by linearly mapping each point (x, y) along the axis between the endpoints (x0, y0) and (x1, y1) to a corresponding point in the domain specified by the shading dictionary’s Domain entry. 
            *The points (0, 0) and (1, 0) in the domain correspond respectively to (x0, y0) and (x1, y1) on the axis. Since all points along a line in domain space perpendicular to the line from (0, 0) to (1, 0) have the same colour, only the new value of x needs to be computed:
            *
            *(see Equation on Page 186)
            *
            *The value of the parametric variable t is then determined from x¢ as follows:
            *
            *  •   For 0£ x¢ £ 1, t = t0 + (t1 - t0) ¥ x¢.
            *
            *  •   For x¢ < 0, if the first element of the Extend array is true, then t = t0; otherwise, t is undefined and the point shall be left unpainted.
            *
            *  •   For x¢ > 1, if the second element of the Extend array is true, then t = t1; otherwise, t is undefined and the point shall be left unpainted.
            *
            *The resulting value of t shall be passed as input to the function(s) defined by the shading dictionary’s Functionentry, yielding the component values of the colour with which to paint the point(x, y).
            *
            *NOTE  Figure L.10 in Annex L shows three examples of the use of an axial shading to fill a rectangle and display text.
            *      The area to be filled extends beyond the shading’s bounding box.The shading is the same in all three cases, except for the values of the Background and Extend entries in the shading dictionary.
            *      In the first example, the shading is not extended at either end and no background colour is specified; therefore, the shading is clipped to its bounding box at both ends. 
            *      The second example still has no background colour specified, but the shading is extended at both ends; the result is to fill the remaining portions of the filled area with the colours defined at the ends of the shading. 
            *      In the third example, the shading is extended at both ends and a background colour is specified; therefore, the background colour is used for the portions of the filled area beyond the ends of the shading.
            */

            /*8.7.4.5.4 Type 3 (Radial) Shadings
            *
            *Type 3(radial) shadings define a colour blend that varies between two circles. Shadings of this type are commonly used to depict three - dimensional spheres and cones.
            *Shading dictionaries for this type of shading contain the entries shown in Table 81, as well as those common to all shading dictionaries(see Table 78).
            *
            *This type of shading shall not be used with an Indexed colour space.
            *
            *Table 81 - Additional Entries Specific to a Type 3 Shading Dictionary
            *
            *              [Key]               [Type]                  [Value]
            *
            *              Coords              array                   (Required) An array of six numbers [x0 y0 r0 x1 y1 r1] specifying the centres and radii of the starting and ending circles, expressed in the shading’s target coordinate space. The radii r0 and r1 shall both be greater than or equal to 0. 
            *                                                          If one radius is 0, the corresponding circle shall be treated as a point; if both are 0, nothing shall be painted.
            *
            *              Domain              array                   (Optional) An array of two numbers [t0 t1] specifying the limiting values of a parametric variable t. The variable is considered to vary linearly between these two values as the colour gradient varies between the starting and ending circles. 
            *                                                          The variable t becomes the input argument to the colour function(s). Default value: [0.0 1.0].
            *              
            *              Function            function                (Required) A 1-in, n-out function or an array of n 1-in, 1-out functions (where n is the number of colour components in the shading dictionary’s colour space). 
            *                                                          The function(s) shall be called with values of the parametric variable t in the domain defined by the shading dictionary’s Domain entry. Each function’s domain shall be a superset of that of the shading dictionary. 
            *                                                          If the value returned by the function for a given colour component is out of range, it shall be adjusted to the nearest valid value.
            *
            *              Extend              array                   (Optional) An array of two boolean values specifying whether to extend the shading beyond the starting and ending circles, respectively. Default value: [false false].
            *
            *The colour blend is based on a family of blend circles interpolated between the starting and ending circles that shall be defined by the shading dictionary’s Coords entry. 
            *The blend circles shall be defined in terms of a subsidiary parametric variable
            *
            *      s = (t - t(0)) / (t(1) - t(0))
            *
            *which varies linearly between 0.0 and 1.0 as t varies across the domain from t0 to t1, as specified by the dictionary’s Domain entry. 
            *The centre and radius of each blend circle shall be given by the following parametric equations:
            *
            *      Xc(S) = X0 + S x ( X1 - X0 ) 
            *      Yc(S) = Y0 + S x ( Y1 - Y0 )
            *      r(S) = r0 + S x ( r1 - r0 )
            *
            *Each value of s between 0.0 and 1.0 determines a corresponding value of t, which is passed as the input argument to the function(s) defined by the shading dictionary’s Function entry. 
            *This yields the component values of the colour with which to fill the corresponding blend circle. For values of s not lying between 0.0 and 1.0, the boolean elements of the shading dictionary’s Extend array determine whether and how the shading is extended. 
            *If the first of the two elements is true, the shading shall be extended beyond the defined starting circle to values of s less than 0.0; if the second element is true, the shading shall be extended beyond the defined ending circle to s values greater than 1.0.
            *
            *NOTE 1    Either of the starting and ending circles may be larger than the other.
            *          If the shading is extended at the smaller end, the family of blend circles continues as far as that value of s for which the radius of the blend circle r(s) = 0.
            *          If the shading is extended at the larger end, the blend circles continue as far as that s value for which r(s) is large enough to encompass the shading’s entire bounding box(BBox).
            *          Extending the shading can thus cause painting to extend beyond the areas defined by the two circles themselves.The two examples in the rightmost column of Figure L.11 in Annex L depict the results of extending the shading at the smaller and larger ends, respectively.
            *
            *Conceptually, all of the blend circles shall be painted in order of increasing values of s, from smallest to largest.Blend circles extending beyond the starting circle shall be painted in the same colour defined by the shading dictionary’s Function entry for the starting circle(t = t0, s = 0.0).
            *Blend circles extending beyond the ending circle shall be painted in the colour defined for the ending circle(t = t1, s = 1.0).
            *The painting is opaque, with the colour of each circle completely overlaying those preceding it.
            *Therefore, if a point lies within more than one blend circle, its final colour shall be that of the last of the enclosing circles to be painted, corresponding to the greatest value of s.
            *
            *NOTE 2    If one of the starting and ending circles entirely contains the other, the shading depicts a sphere, as in Figure L.12 and Figure L.13 in Annex L. 
            *          In Figure L.12 in Annex L, the inner circle has zero radius; it is the starting circle in the figure on the left and the ending circle in the figure on the right. 
            *          Neither shading is extended at either the smaller or larger end. In Figure L.13 in Annex L, the inner circle in both figures has a nonzero radius and the shading is extended at the larger end. 
            *          In each plate, a background colour is specified for the figure on the right but not for the figure on the left.
            *
            *NOTE 3    If neither circle contains the other, the shading depicts a cone. If the starting circle is larger, the cone appears to point out of the page.
            *          If the ending circle is larger, the cone appears to point into the page(see Figure L.11 in Annex L).
            *
            *EXAMPLE 1 This example paints the leaf-covered branch shown in Figure L.14 in Annex L. Each leaf is filled with the same radial shading (object number 5). 
            *          The colour function (object 10) is a stitching function (described in 7.10.4, "Type 3 (Stitching) Functions") whose two subfunctions (objects 11 and 12) are both exponential interpolation functions (see 7.10.3, "Type 2 (Exponential Interpolation) Functions").
            *
            *          5 0 obj                                                     % Shading dictionary
            *              << / ShadingType 3
            *                 / ColorSpace / DeviceCMYK
            *                 / Coords[0.0 0.0 0.096 0.0 0.0 1.000]                % Concentric circles
            *                 / Function 10 0 R
            *                 / Extend[true true]
            *              >>
            *          endobj
            *          10 0 obj                                                    % Colour function
            *              << / FunctionType 3
            *                 / Domain[0.0 1.0]
            *                 / Functions[11 0 R 12 0 R]
            *                 / Bounds[0.708]
            *                 / Encode[1.0 0.0 0.0 1.0]
            *              >>
            *          endobj
            *          11 0 obj                                                    % First subfunction
            *              << / FunctionType 2
            *                 / Domain[0.0 1.0]
            *                 / C0[0.929 0.357 1.000 0.298]
            *                 / C1[0.631 0.278 1.000 0.027]
            *                 / N 1.048
            *              >>
            *          endobj
            *          12 0 obj                                                    % Second subfunction
            *              << / FunctionType 2
            *                 / Domain[0.0 1.0]
            *                 / C0[0.929 0.357 1.000 0.298]
            *                 / C1[0.941 0.400 1.000 0.102]
            *                 / N 1.374
            *              >>
            *          endobj
            *
            *EXAMPLE 2     This example shows how each leaf shown in Figure L.14 in Annex L is drawn as a path and then filled with the shading (where the name Sh1 is associated with object 5 by the Shading subdictionary of the current resource dictionary; see 7.8.3, "Resource Dictionaries").
            *
            *          316.789 140.311 m                                           % Move to start of leaf
            *          303.222 146.388 282.966 136.518 279.122 121.983 c           % Curved segment
            *          277.322 120.182 l                                           % Straight line
            *          285.125 122.688 291.441 121.716 298.156 119.386 c           % Curved segment
            *          336.448 119.386 l                                           % Straight line
            *          331.072 128.643 323.346 137.376 316.789 140.311 c           % Curved segment
            *          W n                                                         % Set clipping path
            *          q                                                           % Save graphics state
            *          27.7843 0.0000 0.0000 -27.7843 310.2461 121.1521 cm         % Set matrix
            *          / Sh1 sh                                                    % Paint shading
            *          Q                                                           % Restore graphics state
            */

            /*8.7.4.5.5 Type 4 Shadings (Free-Form Gouraud-Shaded Triangle Meshes)
            *
            *Type 4 shadings(free - form Gouraud - shaded triangle meshes) are commonly used to represent complex coloured and shaded three-dimensional shapes.
            *The area to be shaded is defined by a path composed entirely of triangles. The colour at each vertex of the triangles is specified, and a technique known as Gouraud interpolation is used to colour the interiors.
            *The interpolation functions defining the shading may be linear or nonlinear.
            *Table 82 shows the entries specific to this type of shading dictionary, in addition to those common to all shading dictionaries(see Table 78) and stream dictionaries(see Table 5).
            *
            *Table 82 - Additional Entries Specific to a Type 4 Shading Dictionary
            *
            *          [Key]                   [Type]              [Value]
            *
            *          BitsPerCoordinate       integer             (Required) The number of bits used to represent each vertex coordinate. The value shall be 1, 2, 4, 8, 12, 16, 24, or 32.
            *
            *          BitsPerComponent        integer             (Required) The number of bits used to represent each colour component. The value shall be 1, 2, 4, 8, 12, or 16.
            *
            *          BitsPerFlag             integer             (Required) The number of bits used to represent the edge flag for each vertex (see below). The value of BitsPerFlag shall be 2, 4, or8, but only the least significant 2 bits in each flag value shall beused. The value for the edge flag shall be 0, 1, or 2.
            *
            *          Decode                  array               (Required) An array of numbers specifying how to map vertex coordinates and colour components into the appropriate ranges of values. The decoding method is similar to that used in image dictionaries (see 8.9.5.2, "Decode Arrays"). The ranges shall bespecified as follows:
            *
            *                                                      [xmin xmax ymin ymax c1,min c1,max … cn,min cn,max]
            *
            *                                                      Only one pair of c values shall be specified if a Function entry is present.
            *
            *          Function                function            (Optional) A 1-in, n-out function or an array of n 1-in, 1-out functions (where n is the number of colour components in the shading dictionary’s colour space). 
            *                                                      If this entry is present, the colour data for each vertex shall be specified by a single parametric variable rather than by n separate colour components. 
            *                                                      The designated function(s) shall be called with each interpolated value of the parametric variable to determine the actual colour at each point. 
            *                                                      Each input value shall be forced into the range interval specified for the corresponding colour component in the shading dictionary’s Decode array. Each function’s domain shall be a superset of that interval. 
            *                                                      If the value returned by the function for a given colour component is out of range, it shall be adjusted to the nearest valid value.
            *                                                      This entry shall not be used with an Indexed colour space.
            *
            *Unlike shading types 1 to 3, types 4 to 7 shall be represented as streams. Each stream contains a sequence of vertex coordinates and colour data that defines the triangle mesh. In a type 4 shading, each vertex is specified by the following values, in the order shown:
            *
            *  f x y c1…cn
            *
            *where
            *
            *  f is the vertex’s edge flag(discussed below)
            *
            *  x and y are its horizontal and vertical coordinates
            *
            *  c1…cn are its colour components
            *
            *All vertex coordinates shall be expressed in the shading’s target coordinate space. If the shading dictionary includes a Function entry, only a single parametric value, t, shall be specified for each vertex in place of the colour components c1…cn.
            *
            *The edge flag associated with each vertex determines the way it connects to the other vertices of the triangle mesh.A vertex va with an edge flag value fa = 0 begins a new triangle, unconnected to any other.At least two more vertices(vb and vc) shall be provided, but their edge flags shall be ignored.
            *These three vertices define a triangle(va, vb, vc), as shown in Figure 24.
            *
            *(see Figure 24 - Starting a New Triangle in a Free-form Gouraud-shaded Triangle Mesh, on page 190)
            *
            *Subsequent triangles shall be defined by a single new vertex combined with two vertices of the preceding triangle. 
            *Given triangle (va, vb, vc), where vertex va precedes vertex vb in the data stream and vb precedes vc, a new vertex vd can form a new triangle on side vbc or side vac, as shown in Figure 25. 
            *(Side vab is assumed to be shared with a preceding triangle and therefore is not available for continuing the mesh.) 
            *If the edge flag is fd = 1 (side vbc), the next vertex forms the triangle (vb, vc, vd); if the edge flag is fd = 2 (side vac), the next vertex forms the triangle (va, vc, vd). An edge flag of fd = 0 starts a new triangle, as described above.
            *
            *(see Figure 25 - Connecting Triangles in a Free-form Gouraud-shaded Triangle Mesh, on page 191)
            *
            *Complex shapes can be created by using the edge flags to control the edge on which subsequent triangles are formed.
            *
            *EXAMPLE   Figure 26 shows two simple examples. Mesh 1 begins with triangle 1 and uses the following edge flags to draw each succeeding triangle:
            *
            *          1 (fa = fb = fc = 0)                    7 (fi = 2)
            *          2 (fd = 1)                              8 (fj = 2)
            *          3 (fe = 1)                              9 (fk = 2)
            *          4 (ff = 1)                             10 (fl = 1)
            *          5 (fg = 1)                             11 (fm = 1)
            *          6 (fh = 1)
            *
            *          Mesh 2 again begins with triangle 1 and uses the following edge flags:
            *
            *          1 (fa = fb = fc = 0)                    4 (ff = 2)
            *          2 (fd = 1)                              5 (fg = 2)
            *          3 (fe = 2)                              6 (fh = 2)
            *
            *The stream shall provide vertex data for a whole number of triangles with appropriate edge flags; otherwise, an error occurs.
            *
            *(see Figure 26 - Varying the Value of the Edge Flag to Create Different Shapes, on page 192)
            *
            *The data for each vertex consists of the following items, reading in sequence from higher-order to lower-order bit positions:
            *
            *  •   An edge flag, expressed in BitsPerFlag bits
            *
            *  •   A pair of horizontal and vertical coordinates, expressed in BitsPerCoordinate bits each
            *
            *  •   A set of n colour components(where n is the number of components in the shading’s colour space), expressed in BitsPerComponent bits each, in the order expected by the sc operator
            *
            *Each set of vertex data shall occupy a whole number of bytes. If the total number of bits required is not divisible by 8, the last data byte for each vertex is padded at the end with extra bits, which shall be ignored.
            *The coordinates and colour values shall be decoded according to the Decode array in the same way as in an image dictionary(see 8.9.5.2, "Decode Arrays").
            *
            *If the shading dictionary contains a Function entry, the colour data for each vertex shall be specified by a single parametric value t rather than by n separate colour components.
            *All linear interpolation within the triangle mesh shall be done using the t values.
            *After interpolation, the results shall be passed to the function(s) specified in the Function entry to determine the colour at each point.
            */

            /*8.7.4.5.6 Type 5 Shadings (Lattice-Form Gouraud-Shaded Triangle Meshes)
            *
            *Type 5 shadings (lattice-form Gouraud-shaded triangle meshes) are similar to type 4, but instead of using free-form geometry, their vertices are arranged in a pseudorectangular lattice, which is topologically equivalent to a rectangular grid. 
            *The vertices are organized into rows, which need not be geometrically linear (see Figure 27).
            *
            *(see Figure 27 - Lattice-form Triangle Meshes, on page 192)
            *
            *Table 83 shows the shading dictionary entries specific to this type of shading, in addition to those common to all shading dictionaries (see Table 78) and stream dictionaries (see Table 5).
            *
            *The data stream for a type 5 shading has the same format as for type 4, except that type 5 does not use edge flags to define the geometry of the triangle mesh.
            *The data for each vertex thus consists of the following values, in the order shown:
            *
            *      x y c1…cn
            *
            *where
            *
            *      x and y shall be the vertex’s horizontal and vertical coordinates
            *
            *      c1…cn shall be its colour components
            *
            *Table 83 - Additional Entries Specific to a Type 5 Shading Dictionary
            *
            *              [Key]                     [Type]                      [Value]
            *
            *              BitsPerCoordinate         integer                     (Required) The number of bits used to represent each vertex coordinate. The value shall be 1, 2, 4, 8, 12, 16, 24, or 32.
            *
            *              BitsPerComponent          integer                     (Required) The number of bits used to represent each colour component. The value shall be 1, 2, 4, 8, 12, or 16.
            *
            *              VerticesPerRow            integer                     (Required) The number of vertices in each row of the lattice; the value shall be greater than or equal to 2. The number of rows need not be specified.
            *
            *              Decode                    array                       (Required) An array of numbers specifying how to map vertex coordinates and colour components into the appropriate ranges of values. The decoding method is similar to that used in image dictionaries (see 8.9.5.2, "Decode Arrays"). 
            *                                                                    The ranges shall bespecified as follows:
            *
            *                                                                    [xmin xmax ymin ymax c1,min c1,max … cn,min cn,max]
            *
            *                                                                    Only one pair of c values shall be specified if a Function entry is present.
            *
            *              Function                  function                    (Optional) A 1-in, n-out function or an array of n 1-in, 1-out functions 
            *                                                                    (where n is the number of colour components in the shading dictionary’s colour space). If this entry is present, the colour data for each vertex shall be specified by a single parametric variable rather than by n separate colour components. 
            *                                                                    The designated function(s) shall be called with each interpolated value of the parametric variable to determine the actual colour at each point. Each input value shall be forced into the range interval specified for the corresponding colour component in the shading dictionary’s Decode array. 
            *                                                                    Each function’s domain shall be a superset of that interval. If the value returned by the function for a given colour component is out of range, it shall be adjusted to the nearest valid value.
            *                                                                    This entry shall not be used with an Indexed colour space.
            *
            *All vertex coordinates are expressed in the shading’s target coordinate space. 
            *If the shading dictionary includes a Function entry, only a single parametric value, t, shall be present for each vertex in place of the colour components c1…cn.
            *
            *The VerticesPerRow entry in the shading dictionary gives the number of vertices in each row of the lattice.
            *All of the vertices in a row shall be specified sequentially, followed by those for the next row.Given m rows of kvertices each, the triangles of the mesh shall be constructed using the following triplets of vertices, as shown in Figure 27:
            *
            *(see Equation on Page 194)
            *
            *
            *See 8.7.4.5.5, "Type 4 Shadings (Free-Form Gouraud-Shaded Triangle Meshes)" for further details on the format of the vertex data.
            */

            /*8.7.4.5.7 Type 6 Shadings (Coons Patch Meshes)
            *
            *Type 6 shadings(Coons patch meshes) are constructed from one or more colour patches, each bounded by four cubic Bézier curves. Degenerate Bézier curves are allowed and are useful for certain graphical effects.At least one complete patch shall be specified.
            *
            *A Coons patch generally has two independent aspects:
            *
            *  •   Colours are specified for each corner of the unit square, and bilinear interpolation is used to fill in colours over the entire unit square(see the upper figure in Figure L.15 in Annex L).
            *
            *  •   Coordinates are mapped from the unit square into a four - sided patch whose sides are not necessarily linear(see the lower figure in Figure L.15 in Annex L).The mapping is continuous: the corners of the unit square map to corners of the patch and the sides of the unit square map to sides of the patch, as shown in Figure 28.
            *
            *The sides of the patch are given by four cubic Bézier curves, C1, C2, D1, and D2, defined over a pair of parametric variables, u and v, that vary horizontally and vertically across the unit square.
            *The four corners of the Coons patch satisfy the following equations:
            *
            *          C1(0) = D1(0)
            *          C1(1) = D2(0)
            *          C2(0) = D1(1)
            *          C2(1) = D2(1)
            *
            *(see Figure 28 - Coordinate Mapping from a Unit Square to a Four-sided Coons Patch, on page 194)
            *
            *Two surfaces can be described that are linear interpolations between the boundary curves. Along the u axis, the surface SC is defined by
            *
            *          Sc(u,v) = (1 - v) x C1(u) + V x C2(u)
            *          
            *Along the V axis, the surface Sd is given by
            *
            *          Sd(u,v) = (1 - u) x D1(v) + u x D2(v)
            *
            *A third surface is the bilinear interpolation of the four corners:
            *
            *          Sb(u,v) = (1 - v) x [ (1 - u) x C1(0) + u x C1(1) ]
            *                  + V x [ (1 - u) x C2(0) + u x C2(1) ]
            *
            *The coordinate mapping for the shading is given by the surface S, defined as 
            *
            *          S = Sc + Sd -Sb
            *
            *This defines the geometry of each patch. A patch mesh is constructed from a sequence of one or more such coloured patches.
            *
            *Patches can sometimes appear to fold over on themselves—for example, if a boundary curve intersects itself. As the value of parameter u or v increases in parameter space, the location of the corresponding pixels in device space may change direction so that new pixels are mapped onto previous pixels already mapped.
            *If more than one point(u, v) in parameter space is mapped to the same point in device space, the point selected shall be the one with the largest value of v. 
            *If multiple points have the same v, the one with the largest value of u shall be selected.If one patch overlaps another, the patch that appears later in the data stream shall paint over the earlier one.
            *
            *NOTE      The patch is a control surface rather than a painting geometry. 
            *          The outline of a projected square(that is, the painted area) might not be the same as the patch boundary if, for example, the patch folds over on itself, as shown in Figure 29.
            *
            *(see Figure 29 - Painted Area and Boundary of a Coons Patch, on page 195)
            *
            *Table 84 shows the shading dictionary entries specific to this type of shading, in addition to those common to all shading dictionaries (see Table 78) and stream dictionaries (see Table 5).
            *
            *Table 84 - Additional Entries Specific to a Type 6 Shading Dictionary
            *
            *              [Key]                    [Type]                  [Value]
            *
            *              BitsPerCoordinate        integer                (Required) The number of bits used to represent each geometric coordinate. The value shall be 1, 2, 4, 8, 12, 16, 24, or 32.
            *
            *              BitsPerComponent         integer                (Required) The number of bits used to represent each colour component. The value shall be 1, 2, 4, 8, 12, or 16.
            *
            *              BitsPerFlag              integer                (Required) The number of bits used to represent the edge flag for each patch (see below). The value shall be 2, 4, or 8, but only the least significant 2 bits in each flag value shall be used. Valid values for the edge flag shall be 0, 1, 2, and 3.
            *
            *              Decode                   array                  (Required) An array of numbers specifying how to map coordinates and colour components into the appropriate ranges of values. The decoding method is similar to that used in image dictionaries (see 8.9.5.2, "Decode Arrays"). The ranges shall be specified as follows:
            *
            *                                                              [xmin xmax ymin ymax c1,min c1,max … cn,min cn,max]
            *
            *                                                              Only one pair of c values shall be specified if a Function entry is present.
            *
            *              Function                 function               (Optional) A 1-in, n-out function or an array of n 1-in, 1-out functions (where n is the number of colour components in the shading dictionary’s colour space). 
            *                                                              If this entry is present, the colour data for each vertex shall be specified by a single parametric variable rather than by n separate colour components. 
            *                                                              The designated function(s) shall be called with each interpolated value of the parametric variable to determine the actual colour at each point. 
            *                                                              Each input value shall be forced into the range interval specified for the corresponding colour component in the shading dictionary’s Decode array. 
            *                                                              Each function’s domain shall be a superset of that interval. If the value returned by the function for a given colour component is out of range, it shall be adjusted to the nearest valid value.
            *                                                              This entry shall not be used with an Indexed colour space.
            *
            *The data stream provides a sequence of Bézier control points and colour values that define the shape and colours of each patch. 
            *All of a patch’s control points are given first, followed by the colour values for its corners. 
            *This differs from a triangle mesh (shading types 4 and 5), in which the coordinates and colour of each vertex are given together. 
            *All control point coordinates are expressed in the shading’s target coordinate space. See 8.7.4.5.5, "Type 4 Shadings (Free-Form Gouraud-Shaded Triangle Meshes)" for further details on the format of the data.
            *
            *As in free - form triangle meshes(type 4), each patch has an edge flag that indicates which edge, if any, it shares with the previous patch. 
            *An edge flag of 0 begins a new patch, unconnected to any other. 
            *This shall be followed by 12 pairs of coordinates, x1 y1 x2 y2 … x12 y12, which specify the Bézier control points that define the four boundary curves.
            *Figure 30 shows how these control points correspond to the cubic Bézier curves C1, C2, D1, and D2 identified in Figure 28.
            *Colour values shall be given for the four corners of the patch, in the same order as the control points corresponding to the corners.
            *Thus, c1 is the colour at coordinates(x1, y1), c2at(x4, y4), c3 at(x7, y7), and c4 at(x10, y10), as shown in the figure.
            *
            *(see Figure 30 - Colour Values and Edge Flags in Coons Patch Meshes, on page 197)
            *
            *Figure 30 also shows how nonzero values of the edge flag (f = 1, 2, or 3) connect a new patch to one of the edges of the previous patch. 
            *In this case, some of the previous patch’s control points serve implicitly as control points for the new patch as well (see Figure 31), and therefore shall not be explicitly repeated in the data stream. 
            *Table 85 summarizes the required data values for various values of the edge flag.
            *
            *(see Figure 31 - Edge Connections in a Coons Patch Mesh, on page 197)
            *
            *If the shading dictionary contains a Function entry, the colour data for each corner of a patch shall be specified by a single parametric value t rather than by n separate colour components c1…cn. 
            *All linear interpolation within the mesh shall be done using the t values. 
            *After interpolation, the results shall be passed to the function(s) specified in the Function entry to determine the colour at each point.
            *
            *Table 85 - Data Values in a Coons Patch Mesh
            *
            *          [Edge Flag]                 [Next Set of Data Values]
            *
            *          f = 0                       x1 y1 x2 y2 x3 y3 x4 y4 x5 y5 x6 y6
            *                                      x7 y7 x8 y8 x9 y9 x10 y10 x11 y11 x12 y12
            *                                      c1 c2 c3 c4
            *                                      New patch; no implicit values
            *
            *          f = 1                       x5 y5 x6 y6 x7 y7 x8 y8 x9 y9 x10 y10 x11 y11 x12 y12
            *                                      c3 c4
            *                                      Implicit values:
            *                                      (x1, y1) = (x4, y4) previousc1 = c2 previous
            *                                      (x2, y2) = (x5, y5) previousc2 = c3 previous
            *                                      (x3, y3) = (x6, y6) previous
            *                                      (x4, y4) = (x7, y7) previous
            *
            *          f = 2                       x5 y5 x6 y6 x7 y7 x8 y8 x9 y9 x10 y10 x11 y11 x12 y12
            *                                      c3 c4
            *                                      Implicit values:
            *                                      (x1, y1) = (x7, y7) previousc1 = c3 previous
            *                                      (x2, y2) = (x8, y8) previousc2 = c4 previous
            *                                      (x3, y3) = (x9, y9) previous
            *                                      (x4, y4) = (x10, y10) previous
            *
            *          f = 3                       x5 y5 x6 y6 x7 y7 x8 y8 x9 y9 x10 y10 x11 y11 x12 y12
            *                                      c3 c4
            *                                      Implicit values:
            *                                      (x1, y1) = (x10, y10) previousc1 = c4 previous
            *                                      (x2, y2) = (x11, y11) previousc2 = c1 previous
            *                                      (x3, y3) = (x12, y12) previous
            *                                      (x4, y4) = (x1, y1) previous
            */

            /*8.7.4.5.8 Type 7 Shadings (Tensor-Product Patch Meshes)
            *
            *Type 7 shadings(tensor - product patch meshes) are identical to type 6, except that they are based on a bicubic tensor - product patch defined by 16 control points instead of the 12 control points that define a Coons patch.
            *The shading dictionaries representing the two patch types differ only in the value of the ShadingType entry and in the number of control points specified for each patch in the data stream.
            *
            *NOTE      Although the Coons patch is more concise and easier to use, the tensor - product patch affords greater control over colour mapping.
            *
            *Like the Coons patch mapping, the tensor-product patch mapping is controlled by the location and shape of four cubic Bézier curves marking the boundaries of the patch. 
            *However, the tensor-product patch has four additional, “internal” control points to adjust the mapping. 
            *The 16 control points can be arranged in a 4-by-4 array indexed by row and column, as follows (see Figure 32):
            *
            *              p03 p13 p23 p33
            *              p02 p12 p22 p32
            *              p01 p11 p21 p31
            *              p00 p10 p20 p30
            *
            *(see Figure 32 - Control Points in a Tensor-product Patch, on page 199)
            *
            *As in a Coons patch mesh, the geometry of the tensor-product patch is described by a surface defined over a pair of parametric variables, u and v, which vary horizontally and vertically across the unit square. 
            *The surface is defined by the equation
            *
            *(see Equation on page 199) S(u,v)
            *
            *where pij is the control point in column i and row j of the tensor, and Bi and Bj are the Bernstein polynomials
            *
            *      B0(t) = (1 - t)^3
            *      B1(t) = 3t x (1 - t)^2
            *      B2(t) = 3t^2 x (1 - t)
            *      B3(t) = t^3
            *
            *Since each point pij is actually a pair of coordinates (xij, yij), the surface can also be expressed as
            *
            *(see Equations on page 200)
            *
            *The geometry of the tensor-product patch can be visualized in terms of a cubic Bézier curve moving from the bottom boundary of the patch to the top. 
            *At the bottom and top, the control points of this curve coincide with those of the patch’s bottom (p00…p30) and top (p03…p33) boundary curves, respectively. 
            *As the curve moves from the bottom edge of the patch to the top, each of its four control points follows a trajectory that is in turn a cubic Bézier curve defined by the four control points in the corresponding column of the array. 
            *That is, the starting point of the moving curve follows the trajectory defined by control points p00…p03, the trajectory of the ending point is defined by points p30…p33, and those of the two intermediate control points by p10…p13 and p20…p23. 
            *Equivalently, the patch can be considered to be traced by a cubic Bézier curve moving from the left edge to the right, with its control points following the trajectories defined by the rows of the coordinate array instead of the columns.
            *
            *The Coons patch(type 6) is actually a special case of the tensor - product patch(type 7) in which the four internal control points(p11, p12, p21, p22) are implicitly defined by the boundary curves.
            *The values of the internal control points are given by these equations:
            *
            *(see Equations on page 200)
            *
            *In the more general tensor-product patch, the values of these four points are unrestricted.
            *
            *The coordinates of the control points in a tensor-product patch shall be specified in the shading’s data stream in the following order:
            *
            *      4   5   6   7
            *      3   14  15  8
            *      2   13  16  9
            *      1   12  11  10
            *
            *All control point coordinates shall be expressed in the shading’s target coordinate space. 
            *These shall be followed by the colour values for the four corners of the patch, in the same order as the corners themselves.
            *If the patch’s edge flag f is 0, all 16 control points and four corner colours shall be explicitly specified in the data stream.
            *If f is 1, 2, or 3, the control points and colours for the patch’s shared edge are implicitly understood to be the same as those along the specified edge of the previous patch and shall not be repeated in the data stream.
            *Table 86 summarizes the data values for various values of the edge flag f, expressed in terms of the row and column indices used in Figure 32. 
            *See 8.7.4.5.5, "Type 4 Shadings (Free-Form Gouraud-Shaded Triangle Meshes)" for further details on the format of the data.
            *
            *Table 86 - Data values in a tensor-product patch mesh
            *
            *              [Edge Flag]                 [Next Set of Data Values]
            *
            *              f = 0                       x00 y00 x01 y01 x02 y02 x03 y03 x13 y13 x23 y23 x33 y33 x32 y32
            *                                          x31 y31 x30 y30 x20 y20 x10 y10 x11 y11 x12 y12 x22 y22 x21 y21
            *                                          c00 c03 c33 c30
            *                                          New patch; no implicit values
            *
            *              f = 1                       x13 y13 x23 y23 x33 y33 x32 y32 x31 y31 x30 y30
            *                                          x20 y20 x10 y10 x11 y11 x12 y12 x22 y22 x21 y21
            *                                          c33 c30
            *                                          Implicit values:
            *                                          (x00, y00) = (x03, y03) previousc00 = c03 previous
            *                                          (x01, y01) = (x13, y13) previousc03 = c33 previous
            *                                          (x02, y02) = (x23, y23) previous
            *                                          (x03, y03) = (x33, y33) previous
            *
            *              f = 2                       x13 y13 x23 y23 x33 y33 x32 y32 x31 y31 x30 y30
            *                                          x20 y20 x10 y10 x11 y11 x12 y12 x22 y22 x21 y21
            *                                          c33 c30
            *                                          Implicit values:
            *                                          (x00, y00) = (x33, y33) previousc00 = c33 previous
            *                                          (x01, y01) = (x32, y32) previousc03 = c30 previous
            *                                          (x02, y02) = (x31, y31) previous
            *                                          (x03, y03) = (x30, y30) previous
            *
            *              f = 3                       x13 y13 x23 y23 x33 y33 x32 y32 x31 y31 x30 y30
            *                                          x20 y20 x10 y10 x11 y11 x12 y12 x22 y22 x21 y21
            *                                          c33 c30
            *                                          Implicit values:
            *                                          (x00, y00) = (x30, y30) previousc00 = c30 previous
            *                                          (x01, y01) = (x20, y20) previousc03 = c00 previous
            *                                          (x02, y02) = (x10, y10) previous
            *                                          (x03, y03) = (x00, y00) previous
            */

            public void getPatternDictionary(ref PDF_Patterns pdf_patterns, ref string stream_hexa, int offset)
            {

            }

            public void getShadingDictionary(ref PDF_Shading_Dictionary pdf_shading_dictionary, ref string stream_hexa, int offset)
            {

            }
            
        }
            
        //8.8 External Objects
        public class External_Objects
        {
            /*8.8.1 General
            *
            *An external object (commonly called an XObject) is a graphics object whose contents are defined by a self-contained stream, separate from the content stream in which it is used.
            *There are three types of external objects:
            *
            *  •   An image XObject(8.9.5, "Image Dictionaries") represents a sampled visual image such as a photograph.
            *
            *  •   A form XObject (8.10, "Form XObjects") is a self-contained description of an arbitrary sequence of graphics objects.
            *
            *  •   A PostScript XObject(8.8.2, "PostScript XObjects") contains a fragment of code expressed in the PostScript page description language.
            *      PostScript XObjects should not be used.
            *
            *Two further categories of external objects, group XObjects and reference XObjects (both PDF 1.4), are actually specialized types of form XObjects with additional properties. 
            *See 8.10.3, "Group XObjects" and 8.10.4, "Reference XObjects" for additional information.
            *
            *Any XObject can be painted as part of another content stream by means of the Do operator (see Table 87). 
            *This operator applies to any type of XObject—image, form, or PostScript. 
            *The syntax is the same in all cases, although details of the operator’s behaviour differ depending on the type.
            *
            *Table 87 - XObject Operator
            *
            *          [Operands]              [Operator]              [Description]
            *
            *          name                    Do                      Paint the specified XObject. The operand name shall appear as a key in the XObject subdictionary of the current resource dictionary (see 7.8.3, "Resource Dictionaries"). The associated value shall be a stream whose Type entry, if present, is XObject.
            *                                                          The effect of Do depends on the value of the XObject’s Subtype entry, which may be Image (see 8.9.5, "Image Dictionaries"), Form (see 8.10, "Form XObjects"), or PS (see 8.8.2, "PostScript XObjects").
            */
            
            /*8.8.2 PostScript XObjects
            *
            *Beginning with PDF 1.1, a content stream may include PostScript language fragments. 
            *These fragments may be used only when printing to a PostScript output device; they shall have no effect either when viewing the document on-screen or when printing it to a non-PostScript device. 
            *In addition, conforming readers may not be able to interpret the PostScript fragments. 
            *Hence, this capability should be used with extreme caution and only if there is no other way to achieve the same result. 
            *Inappropriate use of PostScript XObjects can cause PDF files to print incorrectly.
            *
            *A PostScript XObject is an XObject stream whose Subtype entry has the value PS. 
            *A PostScript XObject dictionary may contain the entries shown in Table 88 in addition to the usual entries common to all streams(see Table 5).
            *
            *Table 88 - Additional Entries Specific to a PostScript XObject Dictionary
            *
            *          [Key]               [Type]                  [Value]
            *
            *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be XObject for a PostScript XObject.
            *
            *          Subtype             name                    (Required) The type of XObject that this dictionary describes; shall be PS for a PostScript XObject.
            *                                                      Alternatively, the value of this entry may be Form, with an additional Subtype2 entry whose value shall be PS.
            *
            *          Level1              stream                  (Optional) A stream whose contents shall be used in place of the PostScript XObject’s stream when the target PostScript interpreter is known to support only LanguageLevel 1.
            *
            *
            *If a PDF content stream is translated by a conforming reader into the PostScript language, any Do operation that references a PostScript XObject may be replaced by the contents of the XObject stream itself. 
            *The stream shall be copied without interpretation. The PostScript fragment may use Type 1 and TrueType fonts listed in the Font subdictionary of the current resource dictionary (see 7.8.3, "Resource Dictionaries"), 
            *accessing them by their BaseFont names using the PostScript findfont operator. The fragment shall not use other types of fonts listed in the Font subdictionary. 
            *It should not reference the PostScript definitions corresponding to PDF procedure sets (see 14.2, "Procedure Sets"), which are subject to change.
            */

            public void getExternalObject(ref PDF_External_Object pdf_external_objects, ref string stream_hexa, int offset)
            {

            }

        }

        //8.9 Images
        public class Images
        {
            /*8.9.1 General
            *
            *PDF’s painting operators include general facilities for dealing with sampled images. A sampled image (or just image for short) is a rectangular array of sample values, each representing a colour. 
            *The image may approximate the appearance of some natural scene obtained through an input scanner or a video camera, or it may be generated synthetically.
            *
            *(see Figure 33 - Typical Sampled Image, on page 203)
            *
            *NOTE 1    An image is defined by a sequence of samples obtained by scanning the image array in row or column order. 
            *          Each sample in the array consists of as many colour components as are needed for the colour space in which they are specified—for example, one component for DeviceGray, three for DeviceRGB, four for DeviceCMYK, or whatever number is required by a particular DeviceN space. 
            *          Each component is a 1-, 2-, 4-, 8-, or (PDF 1.5) 16-bit integer, permitting the representation of 2, 4, 16, 256, or (PDF 1.5) 65536 distinct values for each component. 
            *          Other component sizes can be accommodated when a JPXDecode filter is used; see 7.4.9, "JPXDecode Filter".
            *
            *NOTE 2    PDF provides two means for specifying images:
            *
            *          An image XObject(described in 8.9.5, "Image Dictionaries") is a stream object whose dictionary specifies attributes of the image and whose data contains the image samples.
            *          Like all external objects, it is painted on the page by invoking the Do operator in a content stream(see 8.8, "External Objects"). 
            *          Image XObjects have other uses as well, such as for alternate images(see 8.9.5.4, "Alternate Images"), image masks(8.9.6, "Masked Images"), and thumbnail images(12.3.4, "Thumbnail Images").
            *
            *          An inline image is a small image that is completely defined—both attributes and data—directly inline within a content stream.
            *          The kinds of images that can be represented in this way are limited; see 8.9.7, "Inline Images"for details.
            */

            /*8.9.2 Image Parameters
                *
                *The properties of an image—resolution, orientation, scanning order, and so forth—are entirely independent of the characteristics of the raster output device on which the image is to be rendered. 
                *A conforming reader usually renders images by a sampling technique that attempts to approximate the colour values of the source as accurately as possible. 
                *The actual accuracy achieved depends on the resolution and other properties of the output device.
                *
                *To paint an image, four interrelated items shall be specified:
                *
                *  •   The format of the image: number of columns(width), number of rows(height), number of colour components per sample, and number of bits per colour component
                *
                *  •   The sample data constituting the image’s visual content
                *
                *  •   The correspondence between coordinates in user space and those in the image’s own internal coordinate space, defining the region of user space that will receive the image
                *
                *  •   The mapping from colour component values in the image data to component values in the image’s colour space
                *
                *All of these items shall be specified explicitly or implicitly by an image XObject or an inline image.
                *
                *NOTE      For convenience, the following sub-clauses refer consistently to the object defining an image as an image dictionary.
                *          Although this term properly refers only to the dictionary portion of the stream object representing an image XObject, it should be understood to apply equally to the stream’s data portion or to the parameters and data of an inline image.
                */
                
            /*8.9.3 Sample Representation
                *
                *The source format for an image shall be described by four parameters:
                *
                *  •   The width of the image in samples
                *
                *  •   The height of the image in samples
                *
                *  •   The number of colour components per sample
                *
                *  •   The number of bits per colour component
                *
                *The image dictionary shall specify the width, height, and number of bits per component explicitly. 
                *The number of colour components shall be inferred from the colour space specified in the dictionary.
                *
                *NOTE      For images using the JPXDecode filter(see 7.4.9, "JPXDecode Filter"), the number of bits per component is determined from the image data and not specified in the image dictionary.
                *          The colour space may or may not be specified in the dictionary.
                *
                *Sample data shall be represented as a stream of bytes, interpreted as 8 - bit unsigned integers in the range 0 to 255.
                *The bytes constitute a continuous bit stream, with the high-order bit of each byte first. 
                *This bit stream, in turn, is divided into units of n bits each, where n is the number of bits per component. 
                *Each unit encodes a colour component value, given with high - order bit first; units of 16 bits shall be given with the most significant byte first. 
                *Byte boundaries shall be ignored, except that each row of sample data shall begin on a byte boundary. 
                *If the number of data bits per row is not a multiple of 8, the end of the row is padded with extra bits to fill out the last byte.
                *A conforming reader shall ignore these padding bits.
                *
                *Each n-bit unit within the bit stream shall be interpreted as an unsigned integer in the range 0 to 2n - 1, with the high - order bit first. 
                *The image dictionary’s Decode entry maps this integer to a colour component value, equivalent to what could be used with colour operators such as sc or g. 
                *Colour components shall be interleaved sample by sample; for example, in a three - component RGB image, the red, green, and blue components for one sample are followed by the red, green, and blue components for the next.
                *
                *If the image dictionary's ImageMask entry is false or absent, the colour samples in an image shall be interpreted according to the colour space specified in the image dictionary (see 8.6, "Colour Spaces"), 
                *without reference to the colour parameters in the graphics state. However, if the image dictionary’s ImageMask entry is true, the sample data shall be interpreted as a stencil mask for applying the graphics 
                *state’s nonstroking colour parameters (see 8.9.6.2, "Stencil Masking").
                */
            
            /*8.9.4 Image Coordinate System
                *
                *Each image has its own internal coordinate system, or image space. 
                *The image occupies a rectangle in image space w units wide and h units high, where w and h are the width and height of the image in samples. 
                *Each sample occupies one square unit. The coordinate origin (0, 0) is at the upper-left corner of the image, with coordinates ranging from 0 to w horizontally and 0 to h vertically.
                *
                *The image’s sample data is ordered by row, with the horizontal coordinate varying most rapidly. 
                *This is shown in Figure 34, where the numbers inside the squares indicate the order of the samples, counting from 0. The upper-left corner of the first sample is at coordinates (0, 0), the second at (1, 0), and so on through the last sample of the first row, whose upper-left corner is at (w - 1, 0) and whose upper-right corner is at (w, 0). 
                *The next samples after that are at coordinates (0, 1), (1, 1), and so on to the final sample of the image, whose upper-left corner is at (w - 1, h - 1) and whose lower-right corner is at (w, h).
                *
                *NOTE      The image coordinate system and scanning order imposed by PDF do not preclude using different conventions in the actual image.
                *          Coordinate transformations can be used to map from other conventions to the PDF convention.
                *
                *The correspondence between image space and user space is constant: the unit square of user space, bounded by user coordinates(0, 0) and(1, 1), corresponds to the boundary of the image in image space(see Figure 35). 
                *Following the normal convention for user space, the coordinate(0, 0) is at the lower - left corner of this square, corresponding to coordinates(0, h) in image space.
                *The implicit transformation from image space to user space, if specified explicitly, would be described by the matrix [1⁄w 0 0 -1⁄h 0 1].
                *
                *(see Figure 34 - Source Image Coordinate System, on page 205)
                *
                *(see Figure 35 - Mapping the Source Image, on page 205)
                *
                *An image can be placed on the output page in any position, orientation, and size by using the cm operator to modify the current transformation matrix (CTM) so as to map the unit square of user space to the rectangle or parallelogram in which the image shall be painted. 
                *Typically, this is done within a pair of q and Q operators to isolate the effect of the transformation, which can include translation, rotation, reflection, and skew (see 8.3, "Coordinate Systems").
                *
                *EXAMPLE       If the XObject subdictionary of the current resource dictionary defines the name Image1 to denote an image XObject, 
                *              the code shown in this example paints the image in a rectangle whose lower-left corner is at coordinates (100, 200), 
                *              that is rotated 45 degrees counter clockwise, and that is 150 units wide and 80 units high.
                *
                *              q                                                   % Save graphics state
                *              1 0 0 1 100 200 cm                                  % Translate
                *              0.7071 0.7071 - 0.7071 0.7071 0 0 cm                % Rotate
                *              150 0 0 80 0 0 cm                                   % Scale
                *              / Image1 Do                                         % Paint image
                *              Q                                                   % Restore graphics state
                *
                *              As discussed in 8.3.4, "Transformation Matrices", these three transformations could be combined into one. Of course, 
                *              if the aspect ratio(width to height) of the original image in this example is different from 150:80, the result will be distorted.
                */
            
            /*8.9.5 Image Dictionaries
            */
            
                /*8.9.5.1 General
                *
                *An image dictionary—that is, the dictionary portion of a stream representing an image XObject—may contain the entries listed in Table 89 in addition to the usual entries common to all streams (see Table 5). 
                *There are many relationships among these entries, and the current colour space may limit the choices for some of them. Attempting to use an image dictionary whose entries are inconsistent with each other or 
                *with the current colour space shall cause an error.
                *
                *The entries described here are appropriate for a base image—one that is invoked directly with the Do operator. 
                *Some of the entries should not be used for images used in other ways, such as for alternate images (see 8.9.5.4, "Alternate Images"), image masks(see 8.9.6, "Masked Images"), or thumbnail images(see 12.3.4, "Thumbnail Images").
                *Except as noted, such irrelevant entries are simply ignored by a conforming reader
                *
                *Table 89 - Additional Entries Specific to an Image Dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be XObject for an image XObject.
                *
                *          Subtype             name                (Required) The type of XObject that this dictionary describes; shall be Image for an image XObject.
                *
                *          Width               integer             (Required) The width of the image, in samples.
                *
                *          Height              integer             (Required) The height of the image, in samples.
                *
                *          ColorSpace          name or             (Required for images, except those that use the JPXDecode filter; not allowed forbidden for image masks) The colour space in which image samples shall be specified; it can be any type of colour space except Pattern.
                *                              array               
                *                                                  If the image uses the JPXDecode filter, this entry may be present:
                *                                                      •   If ColorSpace is present, any colour space specifications in the JPEG2000 data shall be ignored.
                *                                                      •   If ColorSpace is absent, the colour space specifications in the JPEG2000 data shall be used. The Decode array shall also be ignored unless ImageMask is true.
                *
                *          BitsPerComponent    integer             (Required except for image masks and images that use the JPXDecode filter) The number of bits used to represent each colour component. 
                *                                                  Only a single value shall be specified; the number of bits shall be the same for all colour components. The value shall be 1, 2, 4, 8, or (in PDF 1.5) 16. If ImageMask is true, this entry is optional, but if specified, its value shall be 1.
                *                                                  If the image stream uses a filter, the value of BitsPerComponentshall be consistent with the size of the data samples that the filter delivers.In particular, a CCITTFaxDecode or JBIG2Decode filter shall always deliver 1 - bit samples, a RunLengthDecode or 
                *                                                  DCTDecode filter shall always deliver 8 - bit samples, and an LZWDecode or FlateDecode filter shall deliver samples of a specified size if a predictor function is used.
                *                                                  If the image stream uses the JPXDecode filter, this entry is optional and shall be ignored if present.
                *                                                  The bit depth is determined by the conforming reader in the process of decoding the JPEG2000 image.
                *
                *          Intent              name                (Optional; PDF 1.1) The name of a colour rendering intent to be used in rendering the image (see 8.6.5.8, "Rendering Intents"). 
                *                                                  Default value: the current rendering intent in the graphics state.
                *
                *          ImageMask           boolean             (Optional) A flag indicating whether the image shall be treated as an image mask (see 8.9.6, "Masked Images"). If this flag is true, the value of BitsPerComponent shall be 1 and Mask and ColorSpace shall not be specified; unmasked areas shall bepainted using the current nonstroking colour. 
                *                                                  Default value: false.
                *
                *          Mask                stream              (Optional except for image masks; not allowed for image masks; PDF 1.3) An image XObject defining an image mask to be applied to this image (see 8.9.6.3, "Explicit Masking"), or an array specifying a range of colours to be applied to it as a colour key mask (see 8.9.6.4, "Colour Key Masking"). 
                *                              or array            If ImageMask is true, this entry shall not be present.
                *
                *          Decode              array               (Optional) An array of numbers describing how to map image samples into the range of values appropriate for the image’s colour space (see 8.9.5.2, "Decode Arrays"). 
                *                                                  If ImageMask is true, the array shall be either [0 1] or [1 0]; otherwise, its length shall betwice the number of colour components required by ColorSpace. 
                *                                                  If the image uses the JPXDecode filter and ImageMask is false, Decode shall be ignored by a conforming reader.
                *                                                  Default value: see 8.9.5.2, "Decode Arrays".
                *
                *          Interpolate         boolean             (Optional) A flag indicating whether image interpolation shall beperformed by a conforming reader (see 8.9.5.3, "Image Interpolation"). 
                *                                                  Default value: false.
                *
                *          Alternates          array               (Optional; PDF 1.3) An array of alternate image dictionaries for this image (see 8.9.5.4, "Alternate Images"). The order of elements within the array shall have no significance. 
                *                                                  This entry shall not bepresent in an image XObject that is itself an alternate image.
                *
                *          SMask               stream              (Optional; PDF 1.4) A subsidiary image XObject defining a soft-mask image (see 11.6.5.3, "Soft-Mask Images") that shall be used as a source of mask shape or mask opacity values in the transparent imaging model. 
                *                                                  The alpha source parameter in the graphics state determines whether the mask values shall beinterpreted as shape or opacity.
                *                                                  If present, this entry shall override the current soft mask in the graphics state, as well as the image’s Mask entry, if any.
                *                                                  However, the other transparency-related graphics state parameters—blend mode and alpha constant—shall remain in effect.
                *                                                  If SMask is absent, the image shall have no associated soft mask(although the current soft mask in the graphics state may still apply).
                *
                *          SMaskInData         integer             (Optional for images that use the JPXDecode filter, meaningless otherwise; PDF 1.5) 
                *                                                  A code specifying how soft-mask information (see 11.6.5.3, "Soft-Mask Images") encoded with image samples shall be used:
                *
                *                                                  0   If present, encoded soft - mask image information shall beignored.
                *
                *                                                  1   The image’s data stream includes encoded soft-mask values.Aconforming reader may create a soft-mask image from the information to be used as a source of mask shape or mask opacity in the transparency imaging model.
                *                                                  
                *                                                  2   The image’s data stream includes colour channels that have been preblended with a background; the image data also includes an opacity channel. 
                *                                                      A conforming reader may create a soft - mask image with a Matte entry from the opacity channel information to be used as a source of mask shape or mask opacity in the transparency model.
                *                                                  
                *                                                  If this entry has a nonzero value, SMask shall not be specified.See also 7.4.9, "JPXDecode Filter".
                *                                                  Default value: 0.
                *
                *          Name                name                (Required in PDF 1.0; optional otherwise) The name by which this image XObject is referenced in the XObject subdictionary of the current resource dictionary (see 7.8.3, "Resource Dictionaries").
                *                                                  This entry is obsolescent and shall no longer be used.
                *
                *          StructParent        integer             (Required if the image is a structural content item; PDF 1.3) The integer key of the image’s entry in the structural parent tree (see 14.7.4.4, "Finding Structure Elements from Content Items").
                *
                *          ID                  byte string         (Optional; PDF 1.3; indirect reference preferred) The digital identifier of the image’s parent Web Capture content set (see 14.10.6, "Object Attributes Related to Web Capture").
                *
                *          OPI                 dictionary          (Optional; PDF 1.2) An OPI version dictionary for the image; see 14.11.7, "Open Prepress Interface (OPI)". If ImageMask is true, this entry shall be ignored.
                *
                *          Metadata            stream              (Optional; PDF 1.4) A metadata stream containing metadata for the image (see 14.3.2, "Metadata Streams").
                *
                *          OC                  dictionary          (Optional; PDF 1.5) An optional content group or optional content membership dictionary (see 8.11, "Optional Content"), specifying the optional content properties for this image XObject. 
                *                                                  Before the image is processed by a conforming reader, its visibility shall bedetermined based on this entry. 
                *                                                  If it is determined to be invisible, the entire image shall be skipped, as if there were no Do operator to invoke it.
                *
                *
                *EXAMPLE       This example defines an image 256 samples wide by 256 high, with 8 bits per sample in the DeviceGray colour space. 
                *              It paints the image on a page with its lower-left corner positioned at coordinates (45, 140) in current user space and scaled to a width and height of 132 user space units.
                *
                *              20 0 obj                                    % Page object
                *                  << / Type / Page
                *                     / Parent 1 0 R
                *                     / Resources 21 0 R
                *                     / MediaBox[0 0 612 792]
                *                     / Contents 23 0 R
                *                  >>
                *              endobj
                *              21 0 obj                                    % Resource dictionary for page
                *                  << / ProcSet[/ PDF / ImageB]
                *                     / XObject << / Im1 22 0 R >>
                *                  >>
                *              endobj
                *              22 0 obj                                    % Image XObject
                *                  << / Type / XObject
                *                     / Subtype / Image
                *                     / Width 256
                *                     / Height 256
                *                     / ColorSpace / DeviceGray
                *                     / BitsPerComponent 8
                *                     / Length 83183
                *                     / Filter / ASCII85Decode
                *                  >>
                *              stream
                *              9LhZI9h\GY9i + bb;,p: e; G9SP92 /)X9MJ >^:f14d;,U(X8P; cO; G9e]; c$= k9Mn\]
                *              …Image data representing 65,536 samples…
                *              8P; cO; G9e]; c$= k9Mn\]~>
                *              endstream
                *              endobj
                *              23 0 obj                                    % Contents of page
                *                  << / Length 56 >>
                *              stream
                *                  q                                           % Save graphics state
                *                      132 0 0 132 45 140 cm                   % Translate to(45, 140) and scale by 132
                *                      / Im1 Do                                % Paint image
                *                  Q                                           % Restore graphics state
                *              endstream
                *              endobj
                */

                /*8.9.5.2 Decode Arrays
                *
                *An image’s data stream is initially decomposed into integers in the domain 0 to 2n - 1, where n is the value of the image dictionary’s BitsPerComponent entry.
                *The image’s Decode array specifies a linear mapping of each integer component value to a number that would be appropriate as a component value in the image’s colour space.
                *
                *Each pair of numbers in a Decode array specifies the lower and upper values to which the domain of sample values in the image is mapped.
                *A Decode array shall contain one pair of numbers for each component in the colour space specified by the image’s ColorSpace entry.
                *The mapping for each colour component, by a conforming reader shall be a linear transformation; that is, it shall use the following formula for linear interpolation:
                *
                *(see Equation on page 210)
                *
                *This formula is used to convert a value x between xmin and xmax to a corresponding value y between ymin and ymax, projecting along the line defined by the points (xmin, ymin) and (xmax, ymax).
                *
                *NOTE 1        While this formula applies to values outside the domain xmin to xmax and does not require that xmin<xmax, 
                *              note that interpolation used for colour conversion, such as the Decode array, does require that xmin < xmax and clips x values to this domain so that y = ymin for all x £ xmin, and y = ymax for all x Š xmax.
                *
                *For a Decode array of the form[Dmin Dmax], this can be written as
                *
                *(see Equation on page 210)
                *
                *where
                *
                *      n shall be the value of BitsPerComponent
                *
                *      x shall be the input value, in the domain 0 to 2n - 1
                *
                *      Dmin and Dmax shall be the values specified in the Decode array
                *
                *      y is the output value, which shall be interpreted in the image’s colour space
                *
                *Samples with a value of 0 shall be mapped to Dmin, those with a value of 2n - 1 shall be mapped to Dmax, and those with intermediate values shall be mapped linearly between Dmin and Dmax.
                *Table 90 lists the default Decode arrays which shall be used with the various colour spaces by a conforming reader.
                *
                *NOTE 2    For most colour spaces, the Decode arrays listed in the table map into the full range of allowed component values.
                *          For an Indexed colour space, the default Decode array ensures that component values that index a colour table are passed through unchanged.
                *
                *
                *Table 90 - Default Decode Arrays
                *
                *          [Colour Space]              [Decode Array]
                *
                *          DeviceGray                  [0.0 1.0]
                *
                *          DeviceRGB                   [0.0 1.0 0.0 1.0 0.0 1.0]
                *
                *          DeviceCMYK                  [0.0 1.0 0.0 1.0 0.0 1.0 0.0 1.0]
                *
                *          CalGray                     [0.0 1.0]
                *
                *          CalRGB                      [0.0 1.0 0.0 1.0 0.0 1.0]
                *
                *          Lab                         [0 100 amin amax bmin bmax] where amin, amax, bmin, and bmax correspond to the values in the Range array of the image’s colour space
                *
                *          ICCBased                    Same as the value of Range in the ICC profile of the image’s colour space
                *
                *          Indexed                     [0 N], where N = 2n − 1
                *
                *          Pattern                     (Not permitted with images)
                *
                *          Separation                  [0.0 1.0]
                *
                *          DeviceN                     [ 0.0 1.0 0.0 1.0 … 0.0 1.0] (one pair of elements for each colour component)
                *
                *NOTE 3    It is possible to specify a mapping that inverts sample colour intensities by specifying a Dmin value greater than Dmax. 
                *          For example, if the image’s colour space is DeviceGray and the Decode array is [1.0 0.0], an input value of 0 is mapped to 1.0 (white); an input value of 2n - 1 is mapped to 0.0 (black).
                *
                *The Dmin and Dmax parameters for a colour component need not fall within the range of values allowed for that component.
                *
                *NOTE 4    For instance, if an application uses 6 - bit numbers as its native image sample format, it can represent those samples in PDF in 8 - bit form, 
                *          setting the two unused high - order bits of each sample to 0.The image dictionary should then specify a Decode array of[0.00000 4.04762], which maps 
                *          input values from 0 to 63 into the range 0.0 to 1.0(4.04762 being approximately equal to 255 ³ 63).
                *
                *If an output value falls outside the range allowed for a component, it shall be automatically adjusted to the nearest allowed value.
                */

                /*8.9.5.3 Image Interpolation
                *
                *When the resolution of a source image is significantly lower than that of the output device, each source sample covers many device pixels. As a result, images can appear jaggy or blocky. 
                *These visual artifacts can be reduced by applying an image interpolation algorithm during rendering. Instead of painting all pixels covered by a source sample with the same colour, image interpolation attempts to produce a smooth transition between adjacent sample values.
                *
                *Image interpolation is enabled by setting the Interpolate entry in the image dictionary to true.It shall be disabled by default because it may increase the time required to render the image.
                *
                *NOTE      A conforming Reader may choose to not implement this feature of PDF, or may use any specific implementation of interpolation that it wishes.
                */

                /*8.9.5.4   Alternate Images
                *
                *Alternate images(PDF 1.3) provide a straightforward and backward - compatible way to include multiple versions of an image in a PDF file for different purposes. 
                *These variant representations of the image may differ, for example, in resolution or in colour space.
                *The primary goal is to reduce the need to maintain separate versions of a PDF document for low - resolution on - screen viewing and high - resolution printing.
                *
                *A base image(that is, the image XObject referred to in a resource dictionary) may contain an Alternates entry.
                *The value of this entry shall be an array of alternate image dictionaries specifying variant representations of the base image.
                *Each alternate image dictionary shall contain an image XObject for one variant and shall specify its properties.Table 91 shows the contents of an alternate image dictionary.
                *
                *Table 91 - Entries in an Alternate Image Dictionary
                *
                *          [Key]                   [Type]              [Value]
                *
                *          Image                   stream              (Required) The image XObject for the alternate image.
                *
                *          DefaultForPrinting      boolean             (Optional) A flag indicating whether this alternate image shall be the default version to be used for printing. 
                *                                                      At most one alternate for a given base image shall be so designated. 
                *                                                      If no alternate has this entry set to true, the base image shall be used for printing by a conforming reader.
                *
                *          OC                      dictionary          (Optional; PDF 1.5) An optional content group (see 8.11.2, "Optional Content Groups") or optional content membership dictionary (see 8.11.2.2, "Optional Content Membership Dictionaries") that facilitates the selection of which alternate image to use.
                *
                *
                *EXAMPLE       The following shows an image with a single alternate. The base image is a grayscale image, and the alternate is a high-resolution RGB image stored on a Web server.
                *
                *              10 0 obj                                                % Image XObject
                *                  << / Type / XObject
                *                     / Subtype / Image
                *                     / Width 100
                *                     / Height 200
                *                     / ColorSpace / DeviceGray
                *                     / BitsPerComponent 8
                *                     / Alternates 15 0 R
                *                     / Length 2167
                *                     / Filter / DCTDecode
                *                  >>
                *              stream
                *              …Image data…
                *              endstream
                *              endobj
                *              15 0 obj                                                % Alternate images array
                *                  [ << / Image 16 0 R
                *                       / DefaultForPrinting true
                *                    >>
                *                  ]
                *              endobj
                *              16 0 obj                                                % Alternate image
                *                  << / Type / XObject
                *                     / Subtype / Image
                *                     / Width 1000
                *                     / Height 2000
                *                     / ColorSpace / DeviceRGB
                *                     / BitsPerComponent 8
                *                     / Length 0                                       % This is an external stream
                *                     / F << / FS / URL
                *                            / F(http:*www.myserver.mycorp.com/images/exttest.jpg)
                *                         >>
                *                     / FFilter / DCTDecode
                *                  >>
                *              stream
                *              endstream
                *              endobj
                *
                *In PDF 1.5, optional content (see 8.11, "Optional Content") may be used to facilitate selection between alternate images. 
                *If an image XObject contains both an Alternates entry and an OC entry, the choice of which image to use shall be determined as follows:
                *
                *  a)  If the image’s OC entry specifies that the base image is visible, that image shall be displayed.
                *
                *  b)  Otherwise, the list of alternates specified by the Alternates entry is examined, and the first alternate containing an OCentry specifying that its content should be visible shall be shown. 
                *      (Alternate images that have no OC entry shall not be shown.)
                */
            
            /*8.9.6 Masked Images
                */

                /*8.9.6.1 General
                *
                *Ordinarily, in the opaque imaging model, images mark all areas they occupy on the page as if with opaque paint. 
                *All portions of the image, whether black, white, gray, or colour, completely obscure any marks that may previously have existed in the same place on the page. 
                *In the graphic arts industry and page layout applications, however, it is common to crop or mask out the background of an image and then place the masked image on a different background so that the existing background shows through the masked areas.
                *A number of PDF features are available for achieving such masking effects:
                *
                *  •   The ImageMask entry in the image dictionary, specifies that the image data shall be used as a stencil mask for painting in the current colour.
                *
                *  •   The Mask entry in the image dictionary(PDF 1.3) specifies a separate image XObject which shall be used as an explicit mask specifying which areas of the image to paint and which to mask out.
                *
                *  •   Alternatively, the Mask entry (PDF 1.3) specifies a range of colours which shall be masked out wherever they occur within the image.This technique is known as colour key masking.
                *
                *NOTE 5    Earlier versions of PDF commonly simulated masking by defining a clipping path enclosing only those of an image’s samples that are to be painted.
                *          However, if the clipping path is very complex (or if there is more than one clipping path) not all conforming Readers will render the results in the same way.
                *          An alternative way to achieve the effect of an explicit mask is to define the image being clipped as a pattern, make it the current colour, and then paint the explicit mask as an image whose ImageMask entry is true.
                *
                *In the transparent imaging model, a fourth type of masking effect, soft masking, is available through the SMaskentry (PDF 1.4) or the SMaskInData entry(PDF 1.5) in the image dictionary; see 11.6.5, "Specifying Soft Masks", for further discussion.
                */

                /*8.9.6.2 Stencil Masking
                *
                *An image mask(an image XObject whose ImageMask entry is true) is a monochrome image in which each sample is specified by a single bit.However, instead of being painted in opaque black and white, the image mask is treated as a stencil mask that is partly opaque and partly transparent.
                *Sample values in the image do not represent black and white pixels; rather, they designate places on the page that should either be marked with the current colour or masked out (not marked at all). 
                *Areas that are masked out retain their former contents. 
                *The effect is like applying paint in the current colour through a cut-out stencil, which lets the paint reach the page in some places and masks it out in others.
                *
                *An image mask differs from an ordinary image in the following significant ways:
                *
                *  •   The image dictionary shall not contain a ColorSpace entry because sample values represent masking properties(1 bit per sample) rather than colours.
                *
                *  •   The value of the BitsPerComponent entry shall be 1.
                *
                *  •   The Decode entry determines how the source samples shall be interpreted.If the Decode array is [0 1](the default for an image mask), a sample value of 0 shall mark the page with the current colour, and a 1 shall leave the previous contents unchanged. 
                *      If the Decode array is [1 0], these meanings shall be reversed.
                *
                *NOTE 6    One of the most important uses of stencil masking is for painting character glyphs represented as bitmaps.Using such a glyph as a stencil mask transfers only its “black” bits to the page, leaving the “white” bits(which are really just background) unchanged.
                *          For reasons discussed in 9.6.5, "Type 3 Fonts", an image mask, rather than an image, should almost always be used to paint glyph bitmaps.
                *
                *          If image interpolation(see 8.9.5.3, "Image Interpolation") is requested during stencil masking, the effect shall be to smooth the edges of the mask, not to interpolate the painted colour values.
                *          This effect can minimize the jaggy appearance of a low - resolution stencil mask.
                */

                /*8.9.6.3 Explicit Masking
                *
                *In PDF 1.3, the Mask entry in an image dictionary may be an image mask, as described in sub - clause 8.9.6.2, "Stencil Masking", which serves as an explicit mask for the primary (base) image.
                *The base image and the image mask need not have the same resolution(Width and Height values), but since all images shall be defined on the unit square in user space, their boundaries on the page will coincide; that is, they will overlay each other.
                *The image mask indicates which places on the page shall be painted and which shall be masked out (left unchanged). 
                *Unmasked areas shall be painted with the corresponding portions of the base image; masked areas shall not be.
                */
                
                /*8.9.6.4 Colour Key Masking
                *
                *In PDF 1.3, the Mask entry in an image dictionary may be an array specifying a range of colours to be masked out. Samples in the image that fall within this range shall not be painted, allowing the existing background to show through.
                *
                *NOTE 1    The effect is similar to that of the video technique known as chroma-key.
                *
                *For colour key masking, the value of the Mask entry shall be an array of 2 ¥ n integers, [min1 max1 … minn maxn], where n is the number of colour components in the image’s colour space.
                *Each integer shall be in the range 0 to 2BitsPerComponent - 1, representing colour values before decoding with the Decode array.
                *An image sample shall be masked (not painted) if all of its colour components before decoding, c1…cn, fall within the specified ranges (that is, if mini £ ci £ maxi for all 1 £ i £ n).
                *
                *When colour key masking is specified, the use of a DCTDecode or lossy JPXDecode filter for the stream can produce unexpected results.
                *
                *NOTE 2    DCTDecode is always a lossy filter while JPXDecode has a lossy filter option. 
                *          The use of a lossy filter mean that the output is only an approximation of the original input data. 
                *          Therefore, the use of this filter may lead to slight changes in the colour values of image samples, possibly causing samples that were intended to be masked to be unexpectedly painted instead, in colours slightly different from the mask colour.
                */

            /*8.9.7 Inline Images
                *
                *As an alternative to the image XObjects described in 8.9.5, "Image Dictionaries", a sampled image may be specified in the form of an inline image. 
                *This type of image shall be defined directly within the content stream in which it will be painted rather than as a separate object.Because the inline format gives the reader less flexibility in managing the image data, 
                *it shall be used only for small images (4 KB or less).
                *
                *An inline image object shall be delimited in the content stream by the operators BI(begin image), ID(image data), and EI(end image).
                *These operators are summarized in Table 92.BI and ID shall bracket a series of key - value pairs specifying the characteristics of the image, such as its dimensions and colour space; the image data shall follow between the ID and EI operators.
                *The format is thus analogous to that of a stream object such as an image XObject:
                *
                *          BI
                *          …Key - value pairs…
                *          ID
                *          …Image data…
                *          EI
                *
                *Table 92 - Inline Image Operators
                *
                *          [Operands]              [Operator]              [Description]
                *
                *          -                       BI                      Begin an inline image object.
                *
                *          -                       ID                      Begin the image data for an inline image object.
                *
                *          -                       EI                      End an inline image object.
                *
                *Inline image objects shall not be nested; that is, two BI operators shall not appear without an intervening EI to close the first object. 
                *Similarly, an ID operator shall only appear between a BI and its balancing EI. 
                *Unless the image uses ASCIIHexDecode or ASCII85Decode as one of its filters, the ID operator shall be followed by a single white-space character, and the next character shall be interpreted as the first byte of image data.
                *
                *The key-value pairs appearing between the BI and ID operators are analogous to those in the dictionary portion of an image XObject(though the syntax is different).
                *Table 93 shows the entries that are valid for an inline image, all of which shall have the same meanings as in a stream dictionary(see Table 5) or an image dictionary(see Table 89).
                *Entries other than those listed shall be ignored; in particular, the Type, Subtype, and Lengthentries normally found in a stream or image dictionary are unnecessary.
                *For convenience, the abbreviations shown in the table may be used in place of the fully spelled -out keys.
                *Table 94 shows additional abbreviations that can be used for the names of colour spaces and filters.
                *
                *These abbreviations are valid only in inline images; they shall not be used in image XObjects. 
                *JBIG2Decodeand JPXDecode are not listed in Table 94 because those filters shall not be used with inline images.
                *
                *Table 93 - Entries in an Inline Image Object
                *
                *          [Full Name]             [Abbreviation]
                *
                *          BitsPerComponent        BPC
                *
                *          ColorSpace              CS
                *
                *          Decode                  D
                *
                *          DecodeParms             DP
                *
                *          Filter                  F
                *
                *          Height                  H
                *
                *          ImageMask               IM
                *
                *          Intent (PDF 1.1)        No abbreviation
                *
                *          Interpolate             I (uppercase I)
                *
                *          Width                   W
                *
                *Table 94 - Additional Abbreviations in an Inline Image Object
                *
                *          [Full Name]             [Abbreviation]
                *
                *          DeviceGray              G
                *
                *          DeviceRGB               RGB
                *
                *          DeviceCMYK              CMYK
                *
                *          Indexed                 I (uppercase I)
                *
                *          ASCIIHexDecode          AHx
                *
                *          ASCII85Decode           A85
                *
                *          LZWDecode               LZW
                *
                *          FlateDecode (PDF 1.2)   Fl (upppercase F, lowersace L)
                *
                *          RunLengthDecode         RL
                *
                *          CCITTFaxDecode          CCF
                *
                *          DCTDecode               DCT
                *
                *
                *
                *The colour space specified by the ColorSpace(or CS) entry shall be one of the standard device colour spaces(DeviceGray, DeviceRGB, or DeviceCMYK). 
                *It shall not be a CIE-based colour space or a special colour space, with the exception of a limited form of Indexed colour space whose base colour space is a device space and whose colour table is specified by a byte string(see 8.6.6.3, "Indexed Colour Spaces").
                *Beginning with PDF 1.2, the value of the ColorSpace entry may also be the name of a colour space in the ColorSpacesubdictionary of the current resource dictionary(see 7.8.3, "Resource Dictionaries").
                *In this case, the name may designate any colour space that can be used with an image XObject.
                *
                *NOTE 1    The names DeviceGray, DeviceRGB, and DeviceCMYK(as well as their abbreviations G, RGB, and CMYK) always identify the corresponding colour spaces directly; 
                *          they never refer to resources in the ColorSpacesubdictionary.
                *
                *The image data in an inline image may be encoded by using any of the standard PDF filters except JPXDecode and JBIG2Decode. 
                *The bytes between the ID and EI operators shall be treated the same as a stream object’s data(see 7.3.8, "Stream Objects"), even though they do not follow the standard stream syntax.
                *
                *NOTE 2    This is an exception to the usual rule that the data in a content stream shall be interpreted according to the standard PDF syntax for objects.
                *
                *EXAMPLE   This example shows an inline image 17 samples wide by 17 high with 8 bits per component in the DeviceRGB colour space. 
                *          The image has been encoded using LZW and ASCII base-85 encoding. 
                *          The cm operator is used to scale it to a width and height of 17 units in user space and position it at coordinates (298, 388). 
                *          The q and Q operators encapsulate the cm operation to limit its effect to resizing the image.
                *
                *          q                                       % Save graphics state
                *          17 0 0 17 298 388 cm                    % Scale and translate coordinate space
                *          BI                                      % Begin inline image object
                *              / W 17                              % Width in samples
                *              / H 17                              % Height in samples
                *              / CS / RGB                          % Colour space
                *              / BPC 8                             % Bits per component
                *              / F[/ A85 / LZW]                    % Filters
                *          ID                                      % Begin image data
                *          J1 / gKA >.]AN & J ?]-< HW]aRVcg* bb.\eKAdVV %/ PcZ
                *          …Omitted data…
                *          R.s(4KE3 & d & 7hb * 7[% Ct2HCqC~>
                *          EI                                      % End inline image object
                *          Q                                       % Restore graphics state
                */

        }

        //8.10 Form XObjects
        public class Form_XObjects
        {
            /*8.10.1 General
            *
            *A form XObject is a PDF content stream that is a self-contained description of any sequence of graphics objects(including path objects, text objects, and sampled images). 
            *A form XObject may be painted multiple times—either on several pages or at several locations on the same page—and produces the same results each time, subject only to the graphics state at the time it is invoked.
            *Not only is this shared definition economical to represent in the PDF file, but under suitable circumstances the conforming reader can optimize execution by caching the results of rendering the form XObject for repeated reuse.
            *
            *NOTE 1    The term form also refers to a completely different kind of object, an interactive form (sometimes called an AcroForm), discussed in 12.7, "Interactive Forms". 
            *          Whereas the form XObjects described in this sub-clause correspond to the notion of forms in the PostScript language, interactive forms are the PDF equivalent of the familiar paper instrument.
            *          Any unqualified use of the word form is understood to refer to an interactive form; the type of form described here is always referred to explicitly as a form XObject.
            *
            *Form XObjects have various uses:
            *
            *  •   As its name suggests, a form XObject may serve as the template for an entire page.
            *
            *EXAMPLE   A program that prints filled-in tax forms can first paint the fixed template as a form XObject and then paint the variable information on top of it.
            *
            *  •   Any graphical element that is to be used repeatedly, such as a company logo or a standard component in the output from a computer-aided design system, may be defined as a form XObject.
            *
            *  •   Certain document elements that are not part of a page’s contents, such as annotation appearances(see 12.5.5, "Appearance Streams"), shall be represented as form XObjects.
            *
            *  •   A specialized type of form XObject, called a group XObject (PDF 1.4), can be used to group graphical elements together as a unit for various purposes (see 8.10.3, "Group XObjects"). 
            *      In particular, group XObjects shall be used to define transparency groups and soft masks for use in the transparent imaging model (see 11.6.5.2, "Soft-Mask Dictionaries" and 11.6.6, "Transparency Group XObjects").
            *
            *  •   Another specialized type of form XObject, a reference XObject(PDF 1.4), may be used to import content from one PDF document into another(see 8.10.4, "Reference XObjects").
            *
            *A writer shall perform the following two specific operations in order to use a form XObject:
            *
            *  a)  Define the appearance of the form XObject.A form XObject is a PDF content stream.The dictionary portion of the stream (called the form dictionary) shall contain descriptive information about the form XObject; the body of the stream shall describe the graphics objects that produce its appearance.
            *      The contents of the form dictionary are described in 8.10.2, "Form Dictionaries".
            *
            *  b)  Paint the form XObject.The Do operator (see 8.8, "External Objects") shall paint a form XObject whose name is supplied as an operand.
            *      The name shall be defined in the XObject subdictionary of the current resource dictionary. 
            *      Before invoking this operator, the content stream in which it appears should set appropriate parameters in the graphics state.In particular, it should alter the current transformation matrix to control the position, size, and orientation of the form XObject in user space.
            *
            *Each form XObject is defined in its own coordinate system, called form space.
            *The BBox entry in the form dictionary shall be expressed in form space, as shall be any coordinates used in the form XObject’s content stream, such as path coordinates. 
            *The Matrix entry in the form dictionary shall specify the mapping from form space to the current user space.Each time the form XObject is painted by the Do operator, this matrix shall be concatenated with the current transformation matrix to define the mapping from form space to device space.
            *
            *NOTE 2    This differs from the Matrix entry in a pattern dictionary, which maps pattern space to the initial user space of the content stream in which the pattern is used.
            *
            *When the Do operator is applied to a form XObject, a conforming reader shall perform the following tasks:
            *
            *  a)  Saves the current graphics state, as if by invoking the q operator (see 8.4.4, "Graphics State Operators")
            *
            *  b)  Concatenates the matrix from the form dictionary’s Matrix entry with the current transformation matrix(CTM)
            *
            *  c)  Clips according to the form dictionary’s BBox entry
            *
            *  d)  Paints the graphics objects specified in the form’s content stream
            *
            *  e)  Restores the saved graphics state, as if by invoking the Q operator (see 8.4.4, "Graphics State Operators")
            *
            *Except as described above, the initial graphics state for the form shall be inherited from the graphics state that is in effect at the time Do is invoked.
            */

            /*8.10.2 Form Dictionaries
            *
            *Every form XObject shall have a form type, which determines the format and meaning of the entries in its form dictionary. This specification only defines one form type, Type 1. 
            *Form XObject dictionaries may contain the entries shown in Table 95, in addition to the usual entries common to all streams (see Table 5).
            *
            *Table 95 - Additional Entries Specific to a Type 1 Form Dictionary
            *
            *          [Key]               [Type]                  [Value]
            *
            *          Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be XObject for a form XObject.
            *
            *          Subtype             name                    (Required) The type of XObject that this dictionary describes; shall beForm for a form XObject.
            *
            *          FormType            integer                 (Optional) A code identifying the type of form XObject that this dictionary describes. The only valid value is 1. Default value: 1.
            *
            *          BBox                rectangle               (Required) An array of four numbers in the form coordinate system (see above), giving the coordinates of the left, bottom, right, and top edges, respectively, of the form XObject’s bounding box. 
            *                                                      These boundaries shall be used to clip the form XObject and to determine its size for caching.
            *
            *          Matrix              array                   (Optional) An array of six numbers specifying the form matrix, which maps form space into user space (see 8.3.4, "Transformation Matrices"). 
            *                                                      Default value: the identity matrix [1 0 0 1 0 0].
            *
            *          Resources           dictionary              (Optional but strongly recommended; PDF 1.2) A dictionary specifying any resources (such as fonts and images) required by the form XObject (see 7.8, "Content Streams and Resources").
            *                                                      In a PDF whose version is 1.1 and earlier, all named resources used in the form XObject shall be included in the resource dictionary of each page object on which the form XObject appears, regardless of whether they also appear in the resource dictionary of the form XObject.
            *                                                      These resources should also be specified in the form XObject’s resource dictionary as well, to determine which resources are used inside the form XObject. 
            *                                                      If a resource is included in both dictionaries, it shall have the same name in both locations.
            *                                                      In PDF 1.2 and later versions, form XObjects may be independent of the content streams in which they appear, and this is strongly recommended although not required.
            *                                                      In an independent form XObject, the resource dictionary of the form XObject is required and shall contain all named resources used by the form XObject. 
            *                                                      These resources shall not be promoted to the outer content stream’s resource dictionary, although that stream’s resource dictionary refers to the form XObject.
            *
            *          Group               dictionary              (Optional; PDF 1.4) A group attributes dictionary indicating that the contents of the form XObject shall be treated as a group and specifying the attributes of that group (see 8.10.3, "Group XObjects").
            *                                                      If a Ref entry(see below) is present, the group attributes shall also apply to the external page imported by that entry, which allows such an imported page to be treated as a group without further modification.
            *
            *          Ref                 dictionary              (Optional; PDF 1.4) A reference dictionary identifying a page to be imported from another PDF file, and for which the form XObject serves as a proxy (see 8.10.4, "Reference XObjects").
            *
            *          Metadata            stream                  (Optional; PDF 1.4) A metadata stream containing metadata for the form XObject (see 14.3.2, "Metadata Streams").
            *
            *          PieceInfo           dictionary              (Optional; PDF 1.3) A page-piece dictionary associated with the form XObject (see 14.5, "Page-Piece Dictionaries").
            *
            *          LastModified        date                    (Required if PieceInfo is present; optional otherwise; PDF 1.3) The date and time (see 7.9.4, "Dates") when the form XObject’s contents were most recently modified. 
            *                                                      If a page-piece dictionary (PieceInfo) is present, the modification date shall be used to ascertain which of the application data dictionaries it contains correspond to the current content of the form (see 14.5, "Page-Piece Dictionaries").
            *
            *          StructParent        integer                 (Required if the form XObject is a structural content item; PDF 1.3)The integer key of the form XObject’s entry in the structural parent tree (see 14.7.4.4, "Finding Structure Elements from Content Items").
            *
            *          StructParents       integer                 (Required if the form XObject contains marked-content sequences that are structural content items; PDF 1.3) The integer key of the form XObject’s entry in the structural parent tree (see 14.7.4.4, "Finding Structure Elements from Content Items").
            *                                                      At most one of the entries StructParent or StructParents shall bepresent.
            *                                                      A form XObject shall be either a content item in its entirety or a container for marked - content sequences that are content items, but not both.
            *
            *          OPI                 dictionary              (Optional; PDF 1.2) An OPI version dictionary for the form XObject (see 14.11.7, "Open Prepress Interface (OPI)").
            *
            *          OC                  dictionary              (Optional; PDF 1.5) An optional content group or optional content membership dictionary (see 8.11, "Optional Content") specifying the optional content properties for the form XObject. Before the form is processed, its visibility shall be determined based on this entry. 
            *                                                      If it is determined to be invisible, the entire form shall be skipped, as if there were no Do operator to invoke it.
            *
            *          Name                name                    (Required in PDF 1.0; optional otherwise) The name by which this form XObject is referenced in the XObject subdictionary of the current resource dictionary (see 7.8.3, "Resource Dictionaries").
            *
            *                                                      NOTE    This entry is obsolescent and its use is no longer recommended.
            *
            *EXAMPLE       The following shows a simple form XObject that paints a filled square 1000 units on each side.
            *
            *              6 0 obj                                         % Form XObject
            *                  << / Type / XObject
            *                     / Subtype / Form
            *                     / FormType 1
            *                     / BBox[0 0 1000 1000]
            *                     / Matrix[1 0 0 1 0 0]
            *                     / Resources << / ProcSet[/ PDF] >>
            *                     /Length 58
            *                  >>
            *              stream
            *                  0 0 m
            *                  0 1000 l
            *                  1000 1000 l
            *                  1000 0 l
            *                  f
            *              endstream
            *              endobj
            */              

            /*8.10.3 Group XObjects
            *A group XObject (PDF 1.4) is a special type of form XObject that can be used to group graphical elements together as a unit for various purposes. 
            *It shall be distinguished by the presence of the optional Group entry in the form dictionary (see 8.10.2, "Form Dictionaries"). 
            *The value of this entry shall be a subsidiary group attributes dictionary describing the properties of the group.
            *
            *As shown in Table 96, every group XObject shall have a group subtype(specified by the S entry in the group attributes dictionary) that determines the format and meaning of the dictionary’s remaining entries.
            *This specification only defines one subtype, a transparency group XObject(subtype Transparency) representing a transparency group for use in the transparent imaging model(see 11.4, "Transparency Groups").
            *The remaining contents of this type of dictionary are described in 11.6.6, "Transparency Group XObjects".
            *
            *Table 96 - Entries Common to all Group Attributes Dictionaries
            *
            *          [Key]               [Type]              [Value]
            *
            *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Group for a group attributes dictionary.
            *
            *          S                   name                (Required) The group subtype, which identifies the type of group whose attributes this dictionary describes and determines the format and meaning of the dictionary’s remaining entries. 
            *                                                  The only group subtype defined is Transparency; see 11.6.6, "Transparency Group XObjects", for the remaining contents of this type of dictionary
            */
            
            /*8.10.4 Reference XObjects
           */
               
                /*8.10.4.1 General
                *
                *Reference XObjects(PDF 1.4) enable one PDF document to import content from another.
                *The document in which the reference occurs is called the containing document; the one whose content is being imported is the target document.
                *The target document may reside in a file external to the containing document or may be included within it as an embedded file stream(see 7.11.4, "Embedded File Streams").
                *
                *The reference XObject in the containing document shall be a form XObject containing the Ref entry in its form dictionary, as described below.
                *This form XObject shall serve as a proxy that shall be displayed or printed by a conforming reader in place of the imported content.
                *
                *NOTE 3    The proxy might consist of a low - resolution image of the imported content, a piece of descriptive text referring to it, a gray box to be displayed in its place, or any other similar placeholder.
                *
                *Conforming readers that do not recognize the Ref entry shall simply display or print the proxy as an ordinary form XObject.
                *Those readers that do implement reference XObjects shall use the proxy in place of the imported content if the latter is unavailable.
                *A conforming reader may also provide a user interface to allow editing and updating of imported content links.
                *
                *The imported content shall consist of a single, complete PDF page in the target document. 
                *It shall be designated by a reference dictionary, which in turn shall be the value of the Ref entry in the reference XObject’s form dictionary (see 8.10.2, "Form Dictionaries"). 
                *The presence of the Ref entry shall distinguish reference XObjects from other types of form XObjects. Table 97 shows the contents of the reference dictionary.
                *
                *Table 97 - Entries in a Reference Dictionary
                *
                *          [Key]               [Type]                        [Value]
                *
                *          F                   file specification            (Required) The file containing the target document.
                *
                *          Page                integer or                    (Required) A page index or page label (see 12.4.2, "Page Labels") identifying the page of the target document containing the content to be imported.
                *                              text string                   This reference is a weak one and may be inadvertently invalidated if the referenced page is changed or replaced in the target document after the reference is created.
                *
                *          ID                  array                         (Optional) An array of two byte strings constituting a file identifier (see 14.4, "File Identifiers") for the file containing the target document. 
                *                                                            The use of this entry improves an reader’s chances of finding the intended file and allows it to warn the user if the file has changed since the reference was created.
                *
                *When the imported content replaces the proxy, it shall be transformed according to the proxy object’s transformation matrix and clipped to the boundaries of its bounding box, as specified by the Matrix and BBoxentries in the proxy’s form dictionary (see 8.10.2, "Form Dictionaries"). 
                *The combination of the proxy object’s matrix and bounding box thus implicitly defines the bounding box of the imported page. 
                *This bounding box typically coincides with the imported page’s crop box or art box (see 14.11.2, "Page Boundaries"), but may not correspond to any of the defined page boundaries. 
                *If the proxy object’s form dictionary contains a Group entry, the specified group attributes shall apply to the imported page as well, which allows the imported page to be treated as a group without further modification.
                */

                /*8.10.4.2 Printing Reference XObjects
                *
                *When printing a page containing reference XObjects, an application may emit any of the following items, depending on the capabilities of the conforming reader, the user’s preferences, and the nature of the print job:
                *
                *  •   The imported content designated by the reference XObject
                *
                *  •   The reference XObject as a proxy for the imported content
                *
                *  •   An OPI proxy or substitute image taken from the reference XObject’s OPI dictionary, if any(see 14.11.7, "Open Prepress Interface (OPI)")
                *
                *The imported content or the reference XObject may also be emitted, by a conforming reader, in place of an OPI proxy when generating OPI comments in a PostScript output stream.
                */

                /*8.10.4.3 Special Considerations
                *
                *Certain special considerations arise when reference XObjects interact with other PDF features:
                *
                *  •   When the page imported by a reference XObject contains annotations(see 12.5, "Annotations"), all annotations that contain a printable, unhidden, visible appearance stream(12.5.5, "Appearance Streams") shall be included in the rendering of the imported page. 
                *      If the proxy is a snapshot image of the imported page, it shall also include the annotation appearances.
                *      These appearances shall therefore be converted into part of the proxy’s content stream, either as subsidiary form XObjects or by flattening them directly into the content stream.
                *
                *  •   Logical structure information associated with a page(see 14.7, "Logical Structure") may be ignored when importing the page into another document with a reference XObject. 
                *      In a target document with multiple pages, structure elements occurring on the imported page are typically part of a larger structure pertaining to the document as a whole; such elements cannot meaningfully be incorporated into the structure of the containing document. 
                *      In a one-page target document or one made up of independent, structurally unrelated pages, the logical structure for the imported page may be wholly self-contained; in this case, it may be possible to incorporate this structure information into that of the containing document. 
                *      However, PDF provides no mechanism for the logical structure hierarchy of one document to refer indirectly to that of another.
                */

        }

        //8.11 Optional Content
        public class Optional_Content
        {
            /*8.11.1 General
            *
            *Optional content(PDF 1.5) refers to sub-clauses of content in a PDF document that can be selectively viewed or hidden by document authors or consumers.
            *This capability is useful in items such as CAD drawings, layered artwork, maps, and multi-language documents.
            *
            *NOTE      The following sub-clauses describe the PDF structures used to implement optional content:
            *          8.11.2, "Optional Content Groups", describes the primary structures used to control the visibility of content.
            *          8.11.3, "Making Graphical Content Optional", describes how individual pieces of content in a document may declare themselves as belonging to one or more optional content groups.
            *          8.11.4, "Configuring Optional Content", describes how the states of optional content groups are set.
            */

            /*8.11.2 Optional Content Groups
            */
                
                /*8.11.2.1 General
                *
                *An optional content group is a dictionary representing a collection of graphics that can be made visible or invisible dynamically by users of conforming readers. 
                *The graphics belonging to such a group may reside anywhere in the document: they need not be consecutive in drawing order, nor even belong to the same content stream.
                *Table 98 shows the entries in an optional content group dictionary.
                *
                *Table 98 - Entries in an Optional Content Group Dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Required) The type of PDF object that this dictionary describes; shall beOCG for an optional content group dictionary.
                *
                *          Name                text string         (Required) The name of the optional content group, suitable for presentation in a reader’s user interface.
                *
                *          Intent              name or             (Optional) A single intent name or an array containing any combination of names. PDF defines two names, View and Design, that may indicate the intended use of the graphics in the group.
                *                              array               A conforming reader may choose to use only groups that have a specific intent and ignore others.
                *                                                  Default value: View. See 8.11.2.3, "Intent" for more information.
                *
                *          Usage               dictionary          (Optional) A usage dictionary describing the nature of the content controlled by the group. It may be used by features that automatically control the state of the group based on outside factors. 
                *                                                  See 8.11.4.4, "Usage and Usage Application Dictionaries" for more information.
                *
                *In its simplest form, each dictionary shall contain a Type entry and a Name for presentation in a user interface. 
                *It may also have an Intent entry that may describe its intended use (see 8.11.2.3, "Intent") and a Usage entry that shall describe the nature of its content (see 8.11.4.4, "Usage and Usage Application Dictionaries").
                *
                *Individual content elements in a document may specify the optional content group or groups that affect their visibility (see 8.11.3, "Making Graphical Content Optional"). Any content whose visibility shall be affected by a given optional content group is said to belong to that group.
                *
                *A group shall be assigned a state, which is either ON or OFF. 
                *States themselves are not part of the PDF document but may be set programmatically or through the reader’s user interface to change the visibility of content.
                *When a document is first opened by a conforming reader, the groups’ states shall be initialized based on the document’s default configuration dictionary(see 8.11.4.3, "Optional Content Configuration Dictionaries").
                *
                *Content belonging to a group shall be visible when the group is ON and invisible when it is OFF.
                *Content may belong to multiple groups, which may have conflicting states.
                *These cases shall be described by the use of optional content membership dictionaries, described in the next sub-clause.
                */

                /*8.11.2.2 Optional Content Membership Dictionaries
                *
                *As mentioned above, content may belong to a single optional content group and shall be visible when the group is ON and invisible when it is OFF.
                *To express more complex visibility policies, content shall not declare itself to belong directly to an optional content group but rather to an optional content membership dictionary, whose entries are shown in Table 99.
                *
                *NOTE 1    8.11.3, "Making Graphical Content Optional" describes how content declares its membership in a group or membership dictionary.
                *
                *Table 99 - Entries in an Optional Content Membership Dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Type                name                    (Required) The type of PDF object that this dictionary describes; shall beOCMD for an optional content membership dictionary.
                *
                *          OCGs                dictionary or           (Optional) A dictionary or array of dictionaries specifying the optional content groups whose states shall determine the visibility of content controlled by this membership dictionary.
                *                              array                   Null values or references to deleted objects shall be ignored. If this entry is not present, is an empty array, or contains references only to null or deleted objects, the membership dictionary shall have no effect on the visibility of any content.
                *
                *          P                   name                    (Optional) A name specifying the visibility policy for content belonging to this membership dictionary. 
                *                                                      Valid values shall be:
                *                                                      
                *                                                      AllOnvisible only if all of the entries in OCGs are ON
                *                                                      AnyOnvisible if any of the entries in OCGs are ON
                *                                                      AnyOffvisible if any of the entries in OCGs are OFF
                *                                                      AllOffvisible only if all of the entries in OCGs are OFF
                *                                                      Default value: AnyOn
                *
                *          VE                  array                   (Optional; PDF 1.6) An array specifying a visibility expression, used to compute visibility of content based on a set of optional content groups; see discussion below.
                *
                *An optional content membership dictionary may express its visibility policy in two ways:
                *
                *  •   The P entry may specify a simple boolean expression indicating how the optional content groups specified by the OCGs entry determine the visibility of content controlled by the membership dictionary.
                *
                *  •   PDF 1.6 introduced the VE entry, which is a visibility expression that may be used to specify an arbitrary boolean expression for computing the visibility of content from the states of optional content groups.
                *
                *NOTE 2    Since the VE entry is more general, if it is present and supported by the conforming reader software, it should be used in preference to OCGs and P. 
                *          However, for compatibility purposes, conforming writers should use OCGs and P entries where possible. 
                *          When the use of VE is necessary to express the intended behaviour, OCGs and P entries should also be provided to approximate the behaviour in non-conforming reader software.
                *
                *A visibility expression is an array with the following characteristics:
                *
                *  •   Its first element shall be a name representing a boolean operator (And, Or, or Not).
                *
                *  •   Subsequent elements shall be either optional content groups or other visibility expressions.
                *
                *  •   If the first element is Not, it shall have only one subsequent element.If the first element is And or Or, it shall have one or more subsequent elements.
                *
                *  •   In evaluating a visibility expression, the ON state of an optional content group shall be equated to the boolean value true; OFF shall be equated to false.
                *
                *Membership dictionaries are useful in cases such as these:
                *
                *  •   Some content may choose to be invisible when a group is ON and visible when it is OFF.In this case, the content would belong to a membership dictionary whose OCGs entry consists of a single optional content group and whose P entry is AnyOff or AllOff.
                *
                *NOTE 3    It is legal to have an OCGs entry consisting of a single group and a P entry that is AnyOn or AllOn.However, in this case it is preferable to use an optional content group directly because it uses fewer objects.
                *
                *  •  Some content may belong to more than one group and needs to specify its policy when the groups are in conflicting states. In this case, the content would belong to a membership dictionary whose OCGs entry consists of an array of optional content groups and whose P entry specifies the visibility policy, as illustrated in EXAMPLE 1 in this sub-clause. EXAMPLE 2 in this sub-clause shows the equivalent policy using visibility expressions.
                *
                *EXAMPLE 1     This example shows content belonging to a membership dictionary whose OCGs entry consists of an array of optional content groups and whose P entry specifies the visibility policy.
                *
                *              << / Type / OCMD                                    % Content belonging to this optional content
                *                                                                  % membership dictionary is controlled by the states
                *                 / OCGs[12 0 R 13 0 R 14 0 R]                     % of three optional content groups.
                *                 / P / AllOn                                      % Content is visible only if the state of all three
                *              >>                                                  % groups is ON; otherwise it’s hidden.
                *
                *EXAMPLE 2     This example shows a visibility expression equivalent to EXAMPLE 1 in this sub - clause
                *
                *              << / Type / OCMD
                *                 / VE[/ And 12 0 R 13 0 R 14 0 R] % Visibility expression equivalent to EXAMPLE 1.
                *              >>
                *
                *EXAMPLE 3     This example shows a more complicated visibility expression based on five optional content groups, represented by objects 1 through 5. 
                *              It is equivalent to
                *
                *              “OCG 1” OR(NOT “OCG 2”) OR(“OCG 3” AND “OCG 4” AND “OCG 5”)
                *
                *              << / Type / OCMD
                *                 / VE[/ Or                            % Visibility expression: OR
                *                      1 0 R                           % OCG 1
                *                      [/ Not 2 0 R]                   % NOT OCG 2
                *                      [/ And 3 0 R 4 0 R 5 0 R]       % OCG 3 AND OCG 4 AND OCG 5
                *                     ]
                *              >>
                */

                /*8.11.2.3 Intent
                *
                *PDF defines two intents: Design, which may be used to represent a document designer’s structural organization of artwork, and View, which may be used for interactive use by document consumers.A conforming writer shall not use a value other than Design or View.
                *
                *NOTE      The Intent entry in Table 98 provides a way to distinguish between different intended uses of optional content.
                *          For example, many document design applications, such as CAD packages, offer layering features for collecting groups of graphics together and selectively hiding or viewing them for the convenience of the author.
                *          However, this layering may be different(at a finer granularity, for example) than would be useful to consumers of the document.
                *          Therefore, it is possible to specify different intents for optional content groups within a single document.
                *          A conforming reader may decide to use only groups that are of a specific intent.
                *
                *Configuration dictionaries(see 8.11.4.3, "Optional Content Configuration Dictionaries") may also contain an Intent entry.
                *If one or more of a group’s intents is contained in the current configuration’s set of intents, the group shall be used in determining visibility.
                *If there is no match, the group shall have no effect on visibility.
                *
                *If the configuration’s Intent is an empty array, no groups shall be used in determining visibility; therefore, all content shall be considered visible.
                */


            /*8.11.3 Making Graphical Content Optional
            */

                /*8.11.3.1 General
                *
                *Graphical content in a PDF file may be made optional by specifying membership in an optional content group or optional content membership dictionary.
                *Two primary mechanisms exist for defining membership:
                *
                *  •   Sections of content streams delimited by marked - content operators may be made optional, as described in 8.11.3.2, "Optional Content in Content Streams".
                *
                *  •   Form and image XObjects and annotations may be made optional in their entirety by means of a dictionary entry, as described in 8.11.3.3, "Optional Content in XObjects and Annotations".
                *
                *When a piece of optional content in a PDF file is determined that it shall be hidden, the following occurs:
                *
                *  •   The content shall not be drawn.
                *
                *  •   Graphics state operations, such as setting the colour, transformation matrix, and clipping, shall still be applied.
                *      In addition, graphics state side effects that arise from drawing operators shall be applied; in particular, the current text position shall be updated even for text wrapped in optional content.
                *      In other words, graphics state parameters that persist past the end of a marked - content section shall be the same whether the optional content is visible or not.
                *
                *Hiding a section of optional content shall not change the colour of objects that do not belong to the same optional content group.
                *
                *  •   This rule shall also apply to operators that set state that is not strictly graphics state; for example, BX and EX.
                *
                *  •   Objects such as form XObjects and annotations that have been made optional may be skipped entirely, because their contents are encapsulated such that no changes to the graphics state(or other state) persist beyond the processing of their content stream.
                *
                *Other features in conforming readers, such as searching and editing, may be affected by the ability to selectively show or hide content. 
                *A conforming reader may choose whether to use the document’s current state of optional content groups (and, correspondingly, the document’s visible graphics) or to supply their own states of optional content groups to control the graphics they process.
                *
                *NOTE 4    Tools to select and move annotations should honour the current on - screen visibility of annotations when performing cursor tracking and mouse - click processing.
                *          A full text search engine, however, may need to process all content in a document, regardless of its current visibility on-screen. 
                *          Export filters might choose the current on-screen visibility, the full content, or present the user with a selection of OCGs to control visibility.
                *
                *NOTE 5    A non-conforming reader that does not support optional content, such as one that only supports PDF 1.4 functionality, will draw and process all content in a document.
                */

                /*8.11.3.2 Optional Content in Content Streams
                *
                *Sections of content in a content stream(including a page's Contents stream, a form or pattern’s content stream, glyph descriptions a Type 3 font as specified by its CharProcs entry, or an annotation’s appearance) may be made optional by enclosing them between the marked-content operators BDC and EMC (see 14.6, "Marked Content") with a marked-content tag of OC. 
                *In addition, a DP marked-content operator may be placed in a page’s content stream to force a reference to an optional content group or groups on the page, even when the page has no current content in that layer.
                *
                *The property list associated with the marked content shall specify either an optional content group or optional content membership dictionary to which the content belongs.
                *Because a group shall be an indirect object and a membership dictionary contains references to indirect objects, the property list shall be a named resource listed in the Properties subdictionary of the current resource dictionary(see 14.6.2, "Property Lists"), as shown in EXAMPLE 1 and EXAMPLE 2 in this sub - clause.
                *
                *Although the marked - content tag shall be OC, other applications of marked content are not precluded from using OC as a tag.
                *The marked content shall be considered to be for optional content only if the tag is OC and the dictionary operand is a valid optional content group or optional content membership dictionary.
                *
                *NOTE 1    To avoid conflict with other features that used marked content(such as logical structure; see 14.7, "Logical Structure"), the following strategy is recommended:
                *
                *          Where content is to be tagged with optional content markers as well as other markers, the optional content markers should be nested inside the other marked content.
                *          Where optional content and the other markers would overlap but there is not strict containment, the optional content should be broken up into two or more BDC/ EMC sections, nesting the optional content sections inside the others as necessary.
                *          Breaking up optional content spans does not damage the nature of the visibility of the content, whereas the same guarantee cannot be made for all other uses of marked content.
                *
                *NOTE 2    Any marked content tagged for optional content that is nested inside other marked content tagged for optional content is visible only if all the levels indicate visibility. 
                *          In other words, if the settings that apply to the outer level indicate that the content should be hidden, the inner level is hidden regardless of its settings.
                *
                *          In the following example, the state of the Show Greeting optional content group directly controls the visibility of the text string “Hello” on the page.
                *          When the group is ON, the text shall be visible; when the group is OFF, the text shall be hidden.
                *
                *EXAMPLE 1             % Within a content stream
                *                      ...
                *                      / OC / oc1 BDC                                  % Optional content follows
                *                          BT
                *                              / F1 1 Tf
                *                              12 0 0 12 100 600 Tm
                *                              (Hello) Tj
                *                          ET
                *                      EMC % End of optional content
                *                      ...
                *                      <<                                              % In the resources dictionary
                *                          / Properties << / oc1 5 0 R >>              % This dictionary maps the name oc1 to an
                *                      ...                                             % optional content group(object 5)
                *                      >>
                *                      5 0 obj                                         % The OCG controlling the visibility
                *                      <<                                              % of the text.
                *                          / Type / OCG
                *                          / Name(Show Greeting)
                *                      >>
                *                      endobj
                *
                *The example above shows one piece of content associated with one optional content group. 
                *There are other possibilities:
                *
                *  •   More than one section of content may refer to the same group or membership dictionary, in which case the visibility of both sections is always the same.
                *
                *  •   Equivalently, although less space - efficient, different sections may have separate membership dictionaries with the same OCGs and P entries. The sections shall have identical visibility behaviour.
                *
                *  •   Two sections of content may belong to membership dictionaries that refer to the same group(s) but with different P settings.For example, if one section has no P entry, and the other has a P entry of AllOff, the visibility of the two sections of content shall be opposite.That is, the first section shall be visible when the second is hidden, and vice versa.
                *
                *The following example demonstrates both the direct use of optional content groups and the indirect use of groups through a membership dictionary. 
                *The content(a black rectangle frame) is drawn if either of the images controlled by the groups named Image A or Image B is shown.
                *If both groups are hidden, the rectangle frame shall be hidden.
                *
                *EXAMPLE 2             % Within a content stream
                *                      ...
                *                      / OC / OC2 BDC                          % Draws a black rectangle frame
                *                          0 g
                *                          4 w
                *                          100 100 412 592 re s
                *                      EMC
                *                      / OC / OC3 BDC                          % Draws an image XObject
                *                          q
                *                          412 0 0 592 100 100 cm
                *                          / Im3 Do
                *                          Q
                *                      EMC
                *                      / OC / OC4 BDC                          % Draws an image XObject
                *                          q
                *                          412 0 0 592 100 100 cm
                *                          / Im4 Do
                *                          Q
                *                      EMC
                *                      ...
                *                      <<                                      % The resource dictionary
                *                          / Properties << / OC2 20 0 R / OC3 30 0 R / OC4 40 0 R >>
                *                          / XObject << / lm3 50 0 R / lm4 / 60 0 R >>
                *                      >>
                *                      20 0 obj
                *                      <<                                      % Optional content membership dictionary
                *                          / Type / OCMD
                *                          / OCGs[30 0 R 40 0 R]
                *                          / P / AnyOn
                *                      >>
                *                      endobj
                *                      30 0 obj                                % Optional content group “Image A”
                *                      <<
                *                          / Type / OCG
                *                          / Name(Image A)
                *                      >>
                *                      endobj
                *                      40 0 obj                                % Optional content group “Image B”
                *                      <<
                *                          / Type / OCG
                *                          / Name(Image B)
                *                      >>
                *                      endobj
                */

                /*8.11.3.3 Optional Content in XObjects and Annotations
                *
                *In addition to marked content within content streams, form XObjects and image XObjects(see 8.8, "External Objects") and annotations(see 12.5, "Annotations") may contain an OC entry, which shall be an optional content group or an optional content membership dictionary.
                *
                *A form or image XObject's visibility shall be determined by the state of the group or those of the groups referenced by the membership dictionary in conjunction with its P (or VE) entry, 
                *along with the current visibility state in the context in which the XObject is invoked (that is, whether objects are visible in the contents stream at the place where the Do operation occurred).
                *
                *Annotations have various flags controlling on-screen and print visibility(see 12.5.3, "Annotation Flags").
                *If an annotation contains an OC entry, it shall be visible for screen or print only if the flags have the appropriate settings and the group or membership dictionary indicates it shall be visible.
                */

            /*8.11.4 Configuring Optional Content
            */

                /*8.11.4.1 General
                *
                *A PDF document containing optional content may specify the default states for the optional content groups in the document and indicate which external factors shall be used to alter the states.
                *
                *NOTE      The following sub - clauses describe the PDF structures that are used to specify this information.
                *
                *          8.11.4.2, "Optional Content Properties Dictionary" describes the structure that lists all the optional content groups in the document and their possible configurations.
                *
                *          8.11.4.3, "Optional Content Configuration Dictionaries" describes the structures that specify initial state settings and other information about the groups in the document.
                *
                *          8.11.4.4, "Usage and Usage Application Dictionaries" and 8.11.4.5, "Determining the State of Optional Content Groups" describe how the states of groups can be affected based on external factors.
                */

                /*8.11.4.2 Optional Content Properties Dictionary
                *
                *The optional OCProperties entry in the document catalog(see 7.7.2, "Document Catalog") shall contain, when present, the optional content properties dictionary, which contains a list of all the optional content groups in the document, as well as information about the default and alternate configurations for optional content. 
                *This dictionary shall be present if the file contains any optional content; if it is missing, a conforming reader shall ignore any optional content structures in the document.
                *
                *This dictionary contains the following entries:
                *
                *Table 100 – Entries in the Optional Content Properties Dictionary
                *
                *          [Key]                   [Type]              [Value]
                *
                *          OCGs                    array               (Required) An array of indirect references to all the optional content groups in the document (see 8.11.2, "Optional Content Groups"), in any order. Every optional content group shall be included in this array.
                *
                *          D                       dictionary          (Required) The default viewing optional content configuration dictionary (see 8.11.4.3, "Optional Content Configuration Dictionaries").
                *
                *          Configs                 array               (Optional) An array of alternate optional content configuration dictionaries (see 8.11.4.3, "Optional Content Configuration Dictionaries").
                */

            public void getOptionalContentPropertiesDictionary(ref PDF_Optional_Content_Properties_Dictionary pdf_optional_content_properties_dictionary, ref string stream_hexa, int offset)
            {

            }

                /*8.11.4.3 Optional Content Configuration Dictionaries
                *
                *The D and Configs entries in Table 100 are configuration dictionaries, which represent different presentations of a document’s optional content groups for use by conforming readers. 
                *The D configuration dictionary shall be used to specify the initial state of the optional content groups when a document is first opened. 
                *Configs lists other configurations that may be used under particular circumstances. The entries in a configuration dictionary are shown in Table 101.
                *
                *Table 101 – Entries in an Optional Content Configuration Dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Name                text string         (Optional) A name for the configuration, suitable for presentation in a user interface.
                *
                *          Creator             text string         (Optional) Name of the application or feature that created this configuration dictionary.
                *
                *          BaseState           name                (Optional) Used to initialize the states of all the optional content groups in a document when this configuration is applied. 
                *                                                  The value of this entry shall be one of the following names:
                *
                *                                                  ON The states of all groups shall be turned ON.
                *                                                  OFF The states of all groups shall be turned OFF.
                *                                                  Unchanged The states of all groups shall be left unchanged.
                *                                                  After this initialization, the contents of the ON and OFF arrays shall beprocessed, overriding the state of the groups included in the arrays.
                *                                                  Default value: ON.
                *                                                  If BaseState is present in the document’s default configuration dictionary, its value shall be ON.
                *
                *          ON                  array               (Optional) An array of optional content groups whose state shall beset to ON when this configuration is applied.
                *                                                  If the BaseState entry is ON, this entry is redundant.
                *
                *          OFF                 array               (Optional) An array of optional content groups whose state shall beset to OFF when this configuration is applied.
                *                                                  If the BaseState entry is OFF, this entry is redundant.
                *
                *          Intent              name or array       (Optional) A single intent name or an array containing any combination of names. it shall be used to determine which optional content groups’ states to consider and which to ignore in calculating the visibility of content (see 8.11.2.3, "Intent").
                *                                                  PDF defines two intent names, View and Design. In addition, the name All shall indicate the set of all intents, including those not yet defined. 
                *                                                  Default value: View. The value shall be View for the document’s default configuration.
                *
                *          AS                  array               (Optional) An array of usage application dictionaries (see Table 103) specifying which usage dictionary categories (see Table 102) shall beconsulted by conforming readers to automatically set the states of optional content groups based on external factors, 
                *                                                  such as the current system language or viewing magnification, and when they shall beapplied.
                *
                *          Order               array               (Optional) An array specifying the order for presentation of optional content groups in a conforming reader’s user interface. The array elements may include the following objects:
                *
                *                                                  Optional content group dictionaries, whose Name entry shall bedisplayed in the user interface by the conforming reader.
                *
                *                                                  Arrays of optional content groups which may be displayed by a conforming reader in a tree or outline structure. Each nested array may optionally have as its first element a text string to be used as a non-selectable label in a conforming reader’s user interface.
                *
                *                                                  Text labels in nested arrays shall be used to present collections of related optional content groups, and not to communicate actual nesting of content inside multiple layers of groups (see EXAMPLE 1 in 8.11.4.3, "Optional Content Configuration Dictionaries"). To reflect actual nesting of groups in the content, such as for layers with sublayers, nested arrays of groups without a text label shall be used (see EXAMPLE 2 in 8.11.4.3, "Optional Content Configuration Dictionaries").
                *
                *                                                  An empty array [] explicitly specifies that no groups shall bepresented.
                *
                *                                                  In the default configuration dictionary, the default value shall be an empty array; in other configuration dictionaries, the default shall be the Order value from the default configuration dictionary.
                *
                *                                                  Any groups not listed in this array shall not be presented in any user interface that uses the configuration.
                *
                *          ListMode             name               (Optional) A name specifying which optional content groups in the Order array shall be displayed to the user. 
                *                                                  Valid values shall be:
                *                                                  AllPages                    Display all groups in the Order array.
                *                                                  VisiblePages                Display only those groups in the Order array that are referenced by one or more visible pages.
                *                                                  Default value: AllPages.
                *
                *          RBGroups             array              (Optional) An array consisting of one or more arrays, each of which represents a collection of optional content groups whose states shall be intended to follow a radio button paradigm. That is, the state of at most one optional content group in each array shall be ON at a time. If one group is turned ON, all others shall be turned OFF. However, turning a group from ON to OFF does not force any other group to be turned ON.
                *
                *                                                  An empty array [] explicitly indicates that no such collections exist.
                *
                *                                                  In the default configuration dictionary, the default value shall be an empty array; in other configuration dictionaries, the default is the RBGroups value from the default configuration dictionary.
                *
                *          Locked              array               (Optional; PDF 1.6) An array of optional content groups that shall belocked when this configuration is applied. The state of a locked group cannot be changed through the user interface of a conforming reader. Conforming writers can use this entry to prevent the visibility of content that depends on these groups from being changed by users.
                *
                *                                                  Default value: an empty array.
                *
                *                                                  A conforming reader may allow the states of optional content groups from being changed by means other than the user interface, such as JavaScript or items in the AS entry of a configuration dictionary.
                *
                *
                *NOTE      EXAMPLE 1 and EXAMPLE 2 in this sub-clause illustrate the use of the Order entry to control the display of groups in a user interface.
                *
                *EXAMPLE 1     Given the following PDF objects:
                *
                *              1 0 obj <</ Type / OCG / Name(Skin) >> endobj                           % Optional content groups
                *              2 0 obj <</ Type / OCG / Name(Bones) >> endobj
                *              3 0 obj <</ Type / OCG / Name(Bark) >> endobj
                *              4 0 obj <</ Type / OCG / Name(Wood) >> endobj
                *              5 0 obj                                                                 % Configuration dictionary
                *                  << / Order[[(Frog Anatomy) 1 0 R 2 0 R] [(Tree Anatomy) 3 0 R 4 0 R] ] >>
                *              A conforming reader should display the optional content groups as follows:
                *              Frog Anatomy
                *              Skin
                *              Bones
                *              Tree Anatomy
                *              Bark
                *              Wood
                *
                *EXAMPLE 2     Given the following PDF objects:
                *
                *                                                                  % Page contents
                *              / OC / L1 BDC                                       % Layer 1
                *                  / OC / L1a BDC                                  % Sublayer A of layer 1
                *                      0 0 100 100 re f
                *                  EMC
                *                  / OC / L1b BDC                                  % Sublayer B of layer 1
                *                      0 100 100 100 re f
                *                  EMC
                *              EMC
                *              ...
                *              << / L1 1 0 R                                       % Resource names
                *                 / L1a 2 0 R
                *                 / L1b 3 0 R
                *              >>
                *              ...                                                 % Optional content groups
                *              1 0 obj <</ Type / OCG / Name(Layer 1) >> endobj
                *              2 0 obj <</ Type / OCG / Name(Sublayer A) >> endobj
                *              3 0 obj <</ Type / OCG / Name(Sublayer B) >> endobj
                *              ...
                *              4 0 obj                                             % Configuration dictionary
                *                  << / Order[1 0 R[2 0 R 3 0 R]] >>
                *              A conforming reader should display the OCGs as follows:
                *              Layer 1
                *                  Sublayer A
                *                  Sublayer B
                *
                *The AS entry is an auto state array consisting of one or more usage application dictionaries that specify how conforming readers shall automatically set the state of optional content groups based on external factors, as discussed in the following sub-clause.
                */

                /*8.11.4.4 Usage and Usage Application Dictionaries
                *
                *Optional content groups are typically constructed to control the visibility of graphic objects that are related in some way. 
                *Objects can be related in several ways; for example, a group may contain content in a particular language or content suitable for viewing at a particular magnification.
                *
                *An optional content group’s usage dictionary(the value of the Usage entry in an optional content group dictionary; see Table 98) shall contain information describing the nature of the content controlled by the group.
                *This dictionary can contain any combination of the entries shown in Table 102.
                *
                *Table 102 – Entries in an Optional Content Usage Dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          CreatorInfo         dictionary          (Optional) A dictionary used by the creating application to store application-specific data associated with this optional content group. 
                *                                                  It shall contain two required entries:
                *
                *                                                  Creator             A text string specifying the application that created the group.
                *                                                  Subtype             A name defining the type of content controlled by the group. Suggested values include but shall not be limited to Artwork, 
                *                                                                      for graphic - design or publishing applications, and Technical, for technical designs such as building plans or schematics.
                *
                *                                                  Additional entries may be included to present information relevant to the creating application or related applications.
                *
                *                                                  Groups whose Intent entry contains Design typically include a CreatorInfo entry.
                *
                *          Language            dictionary          (Optional) A dictionary specifying the language of the content controlled by this optional content group. 
                *                                                  It may contain the following two entries:
                *
                *                                                  Lang        (required) A text string that specifies a language and possibly a locale(see 14.9.2, "Natural Language Specification").For example, es - MX represents Mexican Spanish.
                *
                *                                                  Preferred   (optional) A name whose values shall be either ON or OFF. Default value: OFF.
                *                                                              it shall be used by conforming readers when there is a partial match but no exact match between the system language and the language strings in all usage dictionaries.
                *                                                              See 8.11.4.4, "Usage and Usage Application Dictionaries" for more information.
                *
                *          Export              dictionary          (Optional) A dictionary containing one entry, ExportState, a name whose value shall be either ON or OFF. 
                *                                                  This value shall indicate the recommended state for content in this group when the document (or part of it) is saved by a conforming reader to a format that does not support optional content (for example, a raster image format).
                *
                *          Zoom                dictionary          (Optional) A dictionary specifying a range of magnifications at which the content in this optional content group is best viewed. 
                *                                                  It shall contain one or both of the following entries:
                *
                *                                                  min             The minimum recommended magnification factor at which the group shall be ON.
                *                                                                  Default value: 0.
                *
                *                                                  max             The magnification factor below which the group shall be ON.
                *                                                                  Default value: infinity.
                *
                *          Print               dictionary          (Optional) A dictionary specifying that the content in this group is shall be used when printing. 
                *                                                  It may contain the following optional entries:
                *
                *                                                  Subtype             A name object specifying the kind of content controlled by the group; for example, Trapping, PrintersMarks and Watermark.
                *
                *                                                  PrintState          A name that shall be either ON or OFF, indicating that the group shall be set to that state when the document is printed from a conforming reader.
                *
                *          View                dictionary          (Optional) A dictionary that shall have a single entry, ViewState, a name that shall have a value of either ON or OFF, indicating that the group shall be set to that state when the document is opened in a conforming reader.
                *
                *          User                dictionary          (Optional) A dictionary specifying one or more users for whom this optional content group is primarily intended. Each dictionary shall have two required entries:
                *                                                  
                *                                                  Type                        A name object that shall be either Ind(individual), Ttl(title), or Org(organization).
                *
                *                                                  Name                        A text string or array of text strings representing the name(s) of the individual, position or organization.
                *
                *          PageElement         dictionary          (Optional) A dictionary declaring that the group contains a pagination artifact. It shall contain one entry, Subtype, whose value shall be a name that is either HF (header/footer), FG (foreground image or graphic), BG(background image or graphic), or L (logo).
                *
                *While the data in the usage dictionary serves as information for a document user to examine, it may also be used by conforming readers to automatically manipulate the state of optional content groups based on external factors such as current system language settings or zoom level. 
                *Document authors may use usage application dictionaries to specify which entries in the usage dictionary shall be consulted to automatically set the state of optional content groups based on such factors. 
                *Usage application dictionaries shall be listed in the AS entry in an optional content configuration dictionary (see Table 101). If no AS entry is present, states shall not be automatically adjusted based on usage information.
                *
                *A usage application dictionary specifies the rules for which usage entries shall be used by conforming readers to automatically manipulate the state of optional content groups, which groups shall be affected, and under which circumstances.
                *Table 103 shows the entries in a usage application dictionary.
                *
                *Usage application dictionaries shall only be used by interactive conforming readers, and shall not be used by applications that use PDF as final form output(see 8.11.4.5, "Determining the State of Optional Content Groups" for more information).
                *
                *Table 103 – Entries in a Usage Application Dictionary
                *
                *          [Key]           [Type]              [Value]
                *
                *          Event           name                (Required) A name defining the situation in which this usage application dictionary should be used. Shall be one of View, Print, or Export.
                *
                *          OCGs            array               (Optional) An array listing the optional content groups that shall have their states automatically managed based on information in their usage dictionary (see 8.11.4.4, "Usage and Usage Application Dictionaries"). Default value: an empty array, indicating that no groups shall be affected.
                *
                *          Category        array               (Required) An array of names, each of which corresponds to a usage dictionary entry (see Table 102). When managing the states of the optional content groups in the OCGs array, each of the corresponding categories in the group’s usage dictionary shall be considered.
                *
                *The Event entry specifies whether the usage settings shall be applied during viewing, printing, or exporting the document. 
                *The OCGs entry specifies the set of optional content groups to which usage settings shall be applied. 
                *For each of the groups in OCGs, the entries in its usage dictionary (see Table 102) specified by Category shall be examined to yield a recommended state for the group. 
                *If all the entries yield a recommended state of ON, the group’s state shall be set to ON; otherwise, its state shall be set to OFF.
                *
                *The entries in the usage dictionary shall be used as follows:
                *
                *  •   View: The state shall be the value of the ViewState entry. This entry allows a document to contain content that is relevant only when the document is viewed interactively, such as instructions for how to interact with the document.
                *
                *  •   Print: The state shall be the value of the PrintState entry.If PrintState is not present, the state of the optional content group shall be left unchanged.
                *
                *  •   Export: The state shall be the value of the ExportState entry.
                *
                *  •   Zoom: If the current magnification level of the document is greater than or equal to min and less than max, the ON state shall be used; otherwise, OFF shall be used.
                *
                *  •   User: The Name entry shall specify a name or names to match with the user’s identification. The Typeentry determines how the Name entry shall be interpreted(name, title, or organization). 
                *      If there is an exact match, the ON state shall be used; otherwise OFF shall be used.
                *
                *  •   Language: This category shall allow the selection of content based on the language and locale of the application.
                *      If an exact match to the language and locale is found among the Lang entries of the optional content groups in the usage application dictionary’s OCGs list, all groups that have exact matches shall receive an ON recommendation.
                *      If no exact match is found, but a partial match is found (that is, the language matches but not the locale), all partially matching groups that have Preferred entries with a value of ON shall receive an ON recommendation. 
                *      All other groups shall receive an OFF recommendation.
                *
                *There shall be no restriction on multiple entries with the same value of Event, in order to allow documents with incompatible usage application dictionaries to be combined into larger documents and have their behaviour preserved. 
                *If a given optional content group appears in more than one OCGs array, its state shall be ON only if all categories in all the usage application dictionaries it appears in shall have a state of ON.
                *
                *EXAMPLE       This example shows the use of an auto state array with usage application dictionaries. 
                *              The AS entry in the default configuration dictionary is an array of three usage application dictionaries, one for each of the Event values View, Print, and Export.
                *
                *              / OCProperties                                                          % OCProperties dictionary in document catalog
                *                  << / OCGs[1 0 R 2 0 R 3 0 R 4 0 R]
                *                     / D << / BaseState / OFF                                         % The default configuration
                *                          / ON[1 0 R]
                *                          / AS[                                                       % Auto state array of usage application dictionaries
                *                              << / Event / View / Category[/ Zoom] / OCGs[1 0 R 2 0 R 3 0 R 4 0 R] >>
                *                              << / Event / Print / Category[/ Print] / OCGs[4 0 R] >>
                *                              << / Event / Export / Category[/ Export] / OCGs[3 0 R 4 0 R] >>
                *                              ]
                *                          >>
                *                  >>
                *              ...
                *              1 0 obj
                *                  << / Type / OCG
                *                     / Name(20000 foot view)
                *                     / Usage << / Zoom << / max 1.0 >> >>
                *                  >>
                *              endobj
                *              2 0 obj
                *                  << / Type / OCG
                *                     /Name (10000 foot view)
                *                     / Usage << / Zoom << / min 1.0 / max 2.0 >> >>
                *                  >>
                *              endobj
                *              3 0 obj
                *                  << / Type / OCG
                *                     / Name(1000 foot view)
                *                     / Usage << / Zoom << / min 2.0 / max 20.0 >>
                *                     / Export << / ExportState / OFF >> >>
                *                  >>
                *              endobj
                *              4 0 obj
                *                  << / Type / OCG
                *                     / Name(Copyright notice)
                *                     / Usage << / Print << / PrintState / ON >>
                *                     / Export << / ExportState / ON >> >>
                *                  >>
                *              endobj
                *
                *In the example, the usage application dictionary with event type View specifies that all optional content groups shall have their states managed based on zoom level when viewing. 
                *Three groups (objects 1, 2, and 3) contain Zoom usage information. Object 4 has none; therefore, it shall not be affected by zoom level changes. 
                *Object 3 shall receive an OFF recommendation when exporting. When printing or exporting, object 4 shall receive an ON recommendation.
                */

            

                /*8.11.4.5 Determining the State of Optional Content Groups
                *
                *This sub-clause summarizes the rules by which conforming readers make use of the configuration and usage application dictionaries to set the state of optional content groups. 
                *For purposes of this discussion, it is useful to distinguish the following types of conforming readers:
                *
                *  •   Viewer applications which allow users to interact with the document in various ways.
                *
                *  •   Design applications, which offer layering features for collecting groups of graphics together and selectively hiding or viewing them.
                *
                *NOTE 1        The following rules are not meant to apply to design applications; they may manage their states in an entirely different manner if they choose.
                *
                *  •   Aggregating applications, which import PDF files as graphics.
                *
                *  •   Printing applications, which print PDF files.
                *
                *When a document is first opened, its optional content groups shall be assigned a state based on the D(default) configuration dictionary in the OCProperties dictionary:
                *
                *  a)  The value of BaseState shall be applied to all the groups.
                *
                *  b)  The groups listed in either the ON or OFF array(depending on which one is opposite to BaseState) shall have their states adjusted.
                *
                *This state shall be the state used by printing and aggregating application. Such applications shall not apply the changes based on usage application dictionaries described below. 
                *However, for more advanced functionality, they may provide user control for manipulating the individual states of optional content groups.
                *
                *NOTE 2        Viewer applications may also provide users with an option to view documents in this state (that is, to disable the automatic adjustments discussed below).
                *
                *This option permits an accurate preview of the content as it will appear when placed into an aggregating application or sent to a stand-alone printing system.
                *The remaining discussion in this sub - clause applies only to viewer applications.
                *Such applications shall examine the AS array for usage application dictionaries that have an Event of type View.
                *For each one found, the groups listed in its OCGs array shall be adjusted as described in 8.11.4.4, "Usage and Usage Application Dictionaries".
                *
                *Subsequently, the document is ready for interactive viewing by a user.
                *Whenever there is a change to a factor that the usage application dictionaries with event type View depend on(such as zoom level), the corresponding dictionaries shall be reapplied.
                *
                *The user may manipulate optional content group states manually or by triggering SetOCGState actions (see 12.6.4.12, "Set-OCG-State Actions") by, for example, clicking links or bookmarks.
                *Manual changes shall override the states that were set automatically.
                *The states of these groups remain overridden and shall not be readjusted based on usage application dictionaries with event type View as long as the document is open(or until the user reverts the document to its original state).
                *
                *When a document is printed by a viewer application, usage application dictionaries with an event type Printshall be applied over the current states of optional content groups.
                *These changes shall persist only for the duration of the print operation; then all groups shall revert to their prior states.
                *
                *Similarly, when a document is exported to a format that does not support optional content, usage application dictionaries with an event type Export shall be applied over the current states of optional content groups.
                *Changes shall persist only for the duration of the export operation; then all groups shall revert to their prior states.
                *
                *NOTE 3        Although the event types Print and Export have identically named counterparts that are usage categories, the corresponding usage application dictionaries are permitted to specify that other categories may be applied.
                */

            }

    }

}
