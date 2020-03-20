using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfPlugin
{
    //11 Transparency
    public class Transparency
    {

        /*11.1 General
        *
        *The PDF imaging model includes the notion of transparency. 
        *Transparent objects do not necessarily obey a strict opaque painting model but may blend (composite) in interesting ways with other overlapping objects. 
        *This clause describes the general transparency model but does not cover how it is implemented. 
        *At various points it uses implementation-like descriptions to describe how things work, for the purpose of elucidating the behaviour of the model. 
        *The actual implementation will almost certainly be different from what these descriptions might imply.
        *
        *NOTE      Transparency was added to PDF in version 1.4
        *
        *The clause is organized as follows:
        *
        *  •   11.2, "Overview of Transparency," introduces the basic concepts of the transparency model and its associated terminology.
        *
        *  •   11.3, "Basic Compositing Computations," describes the mathematics involved in compositing a single object with its backdrop.
        *
        *  •   11.4, "Transparency Groups," introduces the concept of transparency groups and describes their properties and behaviour.
        *
        *  •   11.5, "Soft Masks," covers the creation and use of masks to specify position-dependent shape and opacity.
        *
        *  •   11.6, "Specifying Transparency in PDF," describes how transparency properties are represented in a PDF document.
        *
        *  •   11.7, "Colour Space and Rendering Issues," deals with some specific interactions between transparency and other aspects of colour specification and rendering.
        */

        //11.2 Overview of Transparency
        public class Overview_of_Transparency
        {
            /*11.2 Overview of Transparency
            *The original PDF imaging model paints objects (fills, strokes, text, and images), possibly clipped by a path, opaquely onto a page. 
            *The colour of the page at any point shall be that of the topmost enclosing object, disregarding any previous objects it may overlap. 
            *This effect may be—and often is—realized simply by rendering objects directly to the page in the order in which they are specified, with each object completely overwriting any others that it overlaps.
            *
            *Under the transparent imaging model, all of the objects on a page may potentially contribute to the result.
            *Objects at a given point may be thought of as forming a transparency stack(or stack for short). 
            *The objects shall be arranged from bottom to top in the order in which they are specified.
            *The colour of the page at each point shall be determined by combining the colours of all enclosing objects in the stack according to compositing rules defined by the transparency model.
            *
            *NOTE 1    The order in which objects are specified determines the stacking order but not necessarily the order in which the objects are actually painted onto the page.
            *          In particular, the transparency model does not require aconforming reader to rasterize objects immediately or to commit to a raster representation at any time before rendering the entire stack onto the page. 
            *          This is important, since rasterization often causes significant loss of information and precision that is best avoided during intermediate stages of the transparency computation.
            *
            *A given object shall be composited with a backdrop.Ordinarily, the backdrop consists of the stack of all objects that have been specified previously. 
            *The result of compositing shall then be treated as the backdrop for the next object. 
            *However, within certain kinds of transparency groups (see “Transparency Groups”), a different backdrop may be chosen.
            *
            *During the compositing of an object with its backdrop, the colour at each point shall be computed using a specified blend mode, which is a function of both the object’s colour and the backdrop colour. 
            *The blend mode shall determine how colours interact; different blend modes may be used to achieve a variety of useful effects. 
            *A single blend mode shall be in effect for compositing all of a given object, but different blend modes may be applied to different objects.
            *
            *Two scalar quantities called shape and opacity mediate compositing of an object with its backdrop.
            *Conceptually, for each object, these quantities shall be defined at every point in the plane, just as if they were additional colour components. 
            *(In actual practice, they may be obtained from auxiliary sources rather than being intrinsic to the object.)
            *
            *Both shape and opacity vary from 0.0 (no contribution) to 1.0 (maximum contribution). 
            *At any point where either the shape or the opacity of an object is equal to 0.0, its colour shall be undefined.At points where the shape is equal to 0.0, the opacity shall also be undefined. 
            *The shape and opacity shall be subject to compositing rules; therefore, the stack as a whole also has a shape and opacity at each point.
            *
            *An object’s opacity, in combination with the backdrop’s opacity, shall determine the relative contributions of the backdrop colour, the object’s colour, and the blended colour to the resulting composite colour.
            *The object’s shape shall then determine the degree to which the composite colour replaces the backdrop colour. 
            *Shape values of 0.0 and 1.0 identify points that lie outside and inside a conventional sharp-edged object; intermediate values are useful in defining soft-edged objects.
            *
            *Shape and opacity are conceptually very similar. 
            *In fact, they can usually be combined into a single value, called alpha, which controls both the colour compositing computation and the fading between an object and its backdrop. 
            *However, there are a few situations in which they shall be treated separately; see 11.4.6, "Knockout Groups."
            *
            *NOTE 2    Raster-based implementations may need to maintain a separate shape parameter to do anti-aliasing properly; it is therefore convenient to have shape as an explicit part of the model.
            *
            *One or more consecutive objects in a stack may be collected together into a transparency group(often referred to hereafter simply as a group). 
            *The group as a whole may have various properties that modify the compositing behaviour of objects within the group and their interactions with its backdrop.
            *An additional blend mode, shape, and opacity may also be associated with the group as a whole and used when compositing it with its backdrop. 
            *Groups may be nested within other groups, forming a tree-structured hierarchy.
            *
            *EXAMPLE       Figure L.16 in Annex L illustrates the effects of transparency grouping. In the upper two figures, three coloured circles are painted as independent objects with no grouping.At the upper left, the three objects are painted opaquely (opacity = 1.0); each object completely replaces its backdrop(including previously painted objects) with its own colour.
            *              At the upper right, the same three independent objects are painted with an opacity of 0.5, causing them to composite with each other and with the gray and white backdrop. 
            *              In the lower two figures, the three objects are combined as a transparency group.At the lower left, the individual objects have an opacity of 1.0 within the group, but the group as a whole is painted in the Normal blend mode with an opacity of 0.5. 
            *              The objects thus completely overwrite each other within the group, but the resulting group then composites transparently with the gray and white backdrop.
            *              At the lower right, the objects have an opacity of 0.5 within the group and thus composite with each other.
            *              The group as a whole is painted against the backdrop with an opacity of 1.0 but in a different blend mode (HardLight), producing a different visual effect.
            *
            *The colour result of compositing a group may be converted to a single-component luminosity value and treated as a soft mask. Such a mask may then be used as an additional source of shape or opacity values for subsequent compositing operations. 
            *When the mask is used as a shape, this technique is known as soft clipping; it is a generalization of the current clipping path in the opaque imaging model (see “Clipping Path Operators”).
            *
            *The notion of current page is generalized to refer to a transparency group consisting of the entire stack of objects placed on the page, composited with a backdrop that is pure white and fully opaque.
            *Logically, this entire stack shall then be rasterized to determine the actual pixel values to be transmitted to the output device.
            *
            *NOTE 3        In contexts where a PDF page is treated as a piece of artwork to be placed on some other page it is treated not as a page but as a group, whose backdrop may be defined differently from that of a page.
            */
        }

        //11.3 Basic Compositing Computations
        public class Basic_Compositing_Computations
        {

            /*11.3.1 General
            *
            *This sub-clause describes the basic computations for compositing a single object with its backdrop.
            *These computations are extended in 11.4, "Transparency Groups," to cover groups consisting of multiple objects.
            */

            /*11.3.2 Basic Notation for Compositing Computations
            *
                *In general, variable names in this clause consisting of a lowercase letter denote a scalar quantity, such as an opacity. 
                *Uppercase letters denote a value with multiple scalar components, such as a colour. 
                *In the descriptions of the basic colour compositing computations, colour values are generally denoted by the letter C, with a mnemonic subscript indicating which of several colour values is being referred to; for instance, Cs stands for “source colour.” 
                *Shape and opacity values are denoted respectively by the letters f (for “form factor”) and q (for “opaqueness”)—again with a mnemonic subscript, such as qs for “source opacity.” The symbol α (alpha) stands for a product of shape and opacity values.
                *
                *In certain computations, one or more variables may have undefined values; for instance, when opacity is equal to zero, the corresponding colour is undefined.
                *A quantity can also be undefined if it results from division by zero.In any formula that uses such an undefined quantity, the quantity has no effect on the ultimate result because it is subsequently multiplied by zero or otherwise cancelled out. 
                *It is significant that while any arbitrary value may be chosen for such an undefined quantity, the computation shall not malfunction because of exceptions caused by overflow or division by zero.
                *The further convention that 0 ÷ 0 = 0 should also be adopted.
                */
            
            /*11.3.3 Basic Compositing Formula
            *
                *The primary change in the imaging model to accommodate transparency is in how colours are painted. 
                *In the transparent model, the result of painting (the result colour) is a function of both the colour being painted (the source colour) and the colour it is painted over (the backdrop colour). 
                *Both of these colours may vary as a function of position on the page; however, this sub-clause focuses on some fixed point on the page and assumes a fixed backdrop and source colour.
                *
                *This computation uses two other parameters: alpha, which controls the relative contributions of the backdrop and source colours, and the blend function, which specifies how they shall be combined in the painting operation.
                *The resulting basic colour compositing formula(or just basic compositing formula for short) shall determine the result colour produced by the painting operation:
                *
                *(see Equation on page 322)
                *
                *where the variables have the meanings shown in Table 135.
                *
                *Table 135 - Variables used in the basic compositing formula
                *
                *              [Variable]              [Meaning]
                *
                *              Cb                      Backdrop colour
                *
                *              Cs                      Source colour
                *
                *              Cr                      Result colour
                *
                *              alpha(b)                Backdrop alpha
                *
                *              alpha(s)                Source alpha
                *
                *              alpha(r)                Result alpha
                *
                *              B(Cb,Cs)                Blend function
                *
                *
                *This formula represents a simplified form of the compositing formula in which the shape and opacity values are combined and represented as a single alpha value; the more general form is presented later. 
                *This function is based on the over operation defined in the article “Compositing Digital Images,” by Porter and Duff (see the Bibliography), extended to include a blend mode in the region of overlapping coverage. 
                *The following sub-clauses elaborate on the meaning and implications of this formula.
                */

            /*11.3.4 Blending Colour Space
            *
                *The compositing formula shown in 11.3.3, "Basic Compositing Formula," represents a vector function: the colours it operates on are represented in the form of n-element vectors, where n denotes the number of components required by the colour space in used in the compositing process. 
                *The ith component of the result colour Cr shall be obtained by applying the compositing formula to the ith components of the constituent colours Cb, Cs, and B(Cb, Cs). 
                *The result of the computation thus depends on the colour space in which the colours are represented. 
                *For this reason, the colour space used for compositing, called the blending colour space, is explicitly made part of the transparent imaging model. 
                *When necessary, backdrop and source colours shall be converted to the blending colour space before the compositing computation.
                *
                *Of the PDF colour spaces described in Section 8.6, the following shall be supported as blending colour spaces:
                *
                *  •   DeviceGray
                *
                *  •   DeviceRGB
                *
                *  •   DeviceCMYK
                *
                *  •   CalGray
                *
                *  •   CalRGB
                *
                *  •   ICCBased colour spaces equivalent to the preceding(including calibrated CMYK)
                *
                *The Lab space and ICCBased spaces that represent lightness and chromaticity separately (such as L*a*b*, L*u*v*, and HSV) shall not be used as blending colour spaces because the compositing computations in such spaces do not give meaningful results when applied separately to each component. 
                *In addition, an ICCBasedspace used as a blending colour space shall be bidirectional; that is, the ICC profile shall contain both AToBand BToA transformations.
                *
                *The blending colour space shall be consulted only for process colours. 
                *Although blending may also be done on individual spot colours specified in a Separation or DeviceN colour space, such colours shall not be converted to a blending colour space(except in the case where they first revert to their alternate colour space, as described under Section 8.6.6.4 and “DeviceN Colour Spaces”).
                *Instead, the specified colour components shall be blended individually with the corresponding components of the backdrop.
                *
                *The blend functions for the various blend modes are defined such that the range for each colour component shall be 0.0 to 1.0 and that the colour space shall be additive.
                *When performing blending operations in subtractive colour spaces (DeviceCMYK, Separation, and DeviceN), the colour component values shall be complemented (subtracted from 1.0) before the blend function is applied and the results of the function shall then be complemented back before being used.
                *
                *NOTE      This adjustment makes the effects of the various blend modes numerically consistent across all colour spaces. 
                *          However, the actual visual effect produced by a given blend mode still depends on the colour space. 
                *          Blending in a device colour space produces device-dependent results, whereas in a CIE-based space it produces results that are consistent across all devices. 
                *          See 11.7, "Colour Space and Rendering Issues," for additional details concerning colour spaces.
            */

            /*11.3.5 Blend Mode
            *
                *In principle, any function of the backdrop and source colours that yields another colour, Cr, for the result may be used as a blend function B (Cb, Cs), in the compositing formula to customize the blending operation. 
                *PDF defines a standard set of named blend functions, or blend modes, listed in Tables 136 and 137. 
                *Figures L.18and L.19 in Annex L illustrate the resulting visual effects for RGB and CMYK colours, respectively.
                *
                *A blend mode is termed separable if each component of the result colour is completely determined by the corresponding components of the constituent backdrop and source colours—that is, if the blend mode function B is applied separately to each set of corresponding components:
                *
                *      Cr = B(Cb,Cs)
                *
                *where the lowercase variables cr, cb, and cs denote corresponding components of the colours Cr, Cb, and Cs, expressed in additive form. A separable blend mode may be used with any colour space, since it applies independently to any number of components.Only separable blend modes shall be used for blending spot colours.
                *
                *NOTE 1        Theoretically, a blend mode could have a different function for each colour component and still be separable; however, none of the standard PDF blend modes have this property.
                *
                *Table 136 lists the standard separable blend modes available in PDF and the algorithms/ formulas that shall be used in the calculation of blended colours.
                *
                *Table 136 - Standard separable blend modes
                *
                *          [Name]              [Result]
                *
                *          Normal              B(Cb,Cs) = Cs
                *
                *                              NOTE        Selects the source colour, ignoring the backdrop.
                *
                *          Compatible          Same as Normal. This mode existis only for compatibility and should not be used.
                *
                *          Multiply            B(Cb,Cs) = Cb x Cs
                *
                *                              NOTE 1      Multiplies the backdrop and source colour values.
                *
                *                              NOTE 2      The result colour is always at least as dark as either of the two constituent colours. 
                *                                          Multiplying any colour with black produces black; multiplying with white leaves the original colour unchanged. 
                *                                          Painting successive overlapping objects with a colour other than black or white produces progressively darker colours.
                *
                *          Screen              B(Cb,Cs) = 1 - [(1-Cb) x (1-Cs)]
                *                                       = Cb + Cs - (Cb x Cs)
                *
                *                              NOTE 3      Multiplies the complements of the backdrop and source colour values, then complements the result.
                *
                *                              NOTE 4      The result colour is always at least as light as either of the two constituent colours.
                *                                          Screening any colour with white produces white; screening with black leaves the original colour unchanged. 
                *                                          The effect is similar to projecting multiple photographic slides simultaneously onto a single screen.
                *
                *          Overlay             B(Cb,Cs) = HardLight(Cs,Cb)
                *
                *                              NOTE 5      Multiplies or screens the colours, depending on the backdrop colour value. Source colours overlay the backdrop while preserving its highlights and shadows. 
                *                                          The backdrop colour is not replaced but is mixed with the source colour to reflect the lightness or darkness of the backdrop.
                *
                *          Darken              B(Cb,Cs) = min(Cb,Cs)
                *
                *                              NOTE 6      Selects the darker of the backdrop and source colours.
                *                              
                *                              NOTE 7      The backdrop is replaced with the source where the source is darker; otherwise, it is left unchanged.
                *
                *          Lighten             B(Cb,Cs) = max(Cb,Cs)
                *
                *                              NOTE 8      Selects the lighter of the backdrop and source colours.
                *
                *                              NOTE 9      The backdrop is replaced with the source where the source is lighter; otherwise, it is left unchanged.
                *
                *          ColorDodge          B(Cb,Cs) = min(1,Cb/(1-Cs))         if Cs < 1
                *                                       = 1                        if Cs = ??
                *
                *                              NOTE 11     Darkens the backdrop colour to reflect the source colour. Painting with white produces no change.
                *
                *          HardLight           B(Cb,Cs) = Multiply(Cb,2 x Cs)      if Cs <= 0.5
                *                                       = Screen(Cb,2 x Cs - 1)    if Cs > 0.5
                *
                *                              NOTE 12     Multiplies or screens the colours, depending on the source colour value. 
                *                                          The effect is similar to shining a harsh spotlight on the backdrop.
                *
                *          SoftLight           B(Cb,Cs) = Cb - (1 - 2 x Cs) x Cb x (1 - Cb)            if Cs <= 0.5
                *                                       = Cb + (2 x Cs - 1) x (D(Cb) - Cb)             if Cs > 0.5
                *
                *                              where
                *
                *                              D(X) = ((16 x X - 12) x X + 4) x X                      if X <= 0.25
                *                                   = sqrt(X)                                          if X > 0.25
                *
                *                              NOTE 13     Darkens or lightens the colours, depending on the source colour value. The effect is similar to shining a diffused spotlight on the backdrop.
                *
                *          Difference          B(Cb,Cs) = |Cb - Cs|
                *
                *                              NOTE 14     Subtracts the darker of the two constituent colours from the lighter colour: ??
                *
                *                              NOTE 15     Painting with white inverts the backdrop colour; painting with black produces no change.
                *
                *          Exclusion           B(Cb,Cs) = Cb + Cs - 2 x Cb x Cs
                *
                *                              NOTE 16     Produces an effect similar to that of the Difference mode but lower in contrast. 
                *                                          Painting with white inverts the backdrop colour; painting with black produces no change.
                *
                *
                *Table 137 lists the standard nonseparable blend modes. Since the nonseparable blend modes consider all colour components in combination, their computation depends on the blending colour space in which the components are interpreted. 
                *They may be applied to all multiple-component colour spaces that are allowed as blending colour spaces (see “Blending Colour Space”).
                *
                *NOTE 2    All of these blend modes conceptually entail the following steps:
                *
                *          a)  Convert the backdrop and source colours from the blending colour space to an intermediate HSL(hue - saturation - luminosity) representation.
                *
                *          b)  Create a new colour from some combination of hue, saturation, and luminosity components selected from the backdrop and source colours.
                *
                *          c)  Convert the result back to the original(blending) colour space.
                *
                *          However, the following formulas given do not actually perform these conversions. 
                *          Instead, they start with whichever colour(backdrop or source) is providing the hue for the result; then they adjust this colour to have the proper saturation and luminosity.
                *
                *The nonseparable blend mode formulas make use of several auxiliary functions.
                *These functions operate on colours that are assumed to have red, green, and blue components.
                *Blending of CMYK colour spaces requires special treatment, as described in this sub - clause.
                *
                *These functions shall have the following definitions:
                *
                *          Lum(C) = 0.3 x Cred + 0.59 x Cgreen + 0.11 x Cblue
                *
                *          SetLum(C,l)
                *              let d = l - Lum(C)
                *              Cred = Cred + d
                *              Cgreen = Cgreen + d
                *              Cblue = Cblue + d
                *              returnClipColor(C)
                *
                *          ClipColor(C)
                *              let l = Lum(C)
                *              let n = min(Cred,Cgreen,Cblue)
                *              let x = max(Cred,Cgreen,Cblue)
                *              if n < 0.0
                *                  Cred = l + (((Cred - l) x l)/(l - n))
                *                  Cgreen = l + (((Cgreen - l) x l)/(l - n))
                *                  Cblue = l + (((Cblue - l) x l)/(l - n))
                *              if x > 1.0
                *                  Cred = l + (((Cred - l) x (l - 1))/(x - 1)
                *                  Cgreen = l + (((Cred - l) x (l - 1))/(x - 1)
                *                  Cblue = l + (((Cred - l) x (l - 1))/(x - 1)
                *              return C
                *
                *          Sat(C) = max(Cred,Cgree,Cblue) - min(Cred,Cgreen,Cblue)
                *
                *The subscripts min, mid, and max (in the next function) refer to the colour components having the minimum, middle, and maximum values upon entry to the function.
                *
                *          SetSat(C,s)
                *              if Cmax > Cmin
                *                  Cmid = (((Cmid - Cmin) x s)/Cmax - Cmin))
                *                  Cmax = s
                *              else
                *                  Cmid = Cmax = 0.0
                *              Cmin = 0.0
                *              return C
                *
                *Table 137 - Standard nonseparable blend modes
                *
                *              [Name]              [Result]
                *
                *              Hue                 3(Cb,Cs) = SetLum(SetSat(Cs,Sat(Cb)),Lum(Cb))
                *
                *                                  NOTE 1      Creates a colour with the hue of the source colour and the saturation and luminosity of the backdrop colour.
                *
                *              Saturation          3(Cb,Cs) = SetLum(SetSat(Cb,Sat(Cb)),Lum(Cb))
                *
                *                                  NOTE 2      Creates a colour with the saturation of the source colour and the hue and luminosity of the backdrop colour. 
                *                                              Painting with this mode in an area of the backdrop that is a pure gray (no saturation) produces no change.
                *
                *              Color               B(Cb,Cs) = SetLum(Cs,Lum(Cb))
                *
                *                                  NOTE 3      Creates a colour with the hue and saturation of the source colour and the luminosity of the backdrop colour. 
                *                                              This preserves the gray levels of the backdrop and is useful for colouring monochrome images or tinting colour images.
                *
                *              Luminosity          B(Cb,Cs) = SetLum(Cb,Lum(Cs))
                *
                *                                  NOTE 4      Creates a colour with the luminosity of the source colour and the hue and saturation of the backdrop colour. 
                *                                              This produces an inverse effect to that of the Color mode.
                *
                *The formulas in this sub-clause apply to RGB spaces. 
                *Blending in CMYK spaces (including both DeviceCMYKand ICCBased calibrated CMYK spaces) shall be handled in the following way:
                *
                *  •   The C, M, and Y components shall be converted to their complementary R, G, and B components in the usual way.
                *      The preceding formulas shall be applied to the RGB colour values.The results shall be converted back to C, M, and Y.
                *
                *  •   For the K component, the result shall be the K component of Cb for the Hue, Saturation, and Color blend modes; it shall be the K component of Cs for the Luminosity blend mode.
                */

            /*11.3.6 Interpretation of Alpha
            *
                *The colour compositing formula
                *
                *(see Equation on page 328)
                *
                *produces a result colour that is a weighted average of the backdrop colour, the source colour, and the blended B(Cb, Cs) term, with the weighting determined by the backdrop and source alphas αβ and αs. 
                *For the simplest blend mode, Normal, defined by
                *
                *      B(Cb,Cs) = Cs ????
                *
                *the compositing formula collapses to a simple weighted average of the backdrop and source colours, controlled by the backdrop and source alpha values. 
                *For more interesting blend functions, the backdrop and source alphas control whether the effect of the blend mode is fully realized or is toned down by mixing the result with the backdrop and source colours.
                *
                *The result alpha, αρ , actually represents a computed result, described in 11.3.7, "Shape and Opacity Computations." 
                *The result colour shall be normalized by the result alpha, ensuring that when this colour and alpha are subsequently used together in another compositing operation, the colour’s contribution is correctly represented.
                *
                *NOTE 1    If αρ is zero, the result colour is undefined.
                *
                *NOTE 2    The preceding formula represents a simplification of the following formula, which presents the relative contributions of backdrop, source, and blended colours in a more straightforward way:
                *
                *          (see Equation on page 328)
                *
                *          (The simplification requires a substitution based on the alpha compositing formula, which is presented in the next sub-clause.) 
                *          Thus, mathematically, the backdrop and source alphas control the influence of the backdrop and source colours, respectively, while their product controls the influence of the blend function. 
                *          An alpha value of αs = 0.0 or αβ = 0.0 results in no blend mode effect; setting αs = 1.0 and αβ = 1.0 results in maximum blend mode effect.
                */

            /*11.3.7 Shape and Opacity Computations
            */
                
                /*11.3.7.1 General
                *
                *As stated earlier, the alpha values that control the compositing process shall be defined as the product of shape and opacity:
                *
                *      (see Equations on page 329)
                *
                *This sub-clause examines the various shape and opacity values individually. 
                *Once again, keep in mind that conceptually these values are computed for every point on the page.
                */
                
                /*11.3.7.2  Source Shape and Opacity
                *
                *Shape and opacity values may come from several sources.The transparency model provides for three independent sources for each.
                *However, the PDF representation imposes some limitations on the ability to specify all of these sources independently(see “Specifying Shape and Opacity”).
                *
                *  •   Object shape.Elementary objects such as strokes, fills, and text have an intrinsic shape, whose value shall be 1.0 for points inside the object and 0.0 outside.
                *      Similarly, an image with an explicit mask(see “Explicit Masking”) has a shape that shall be 1.0 in the unmasked portions and 0.0 in the masked portions.
                *      The shape of a group object shall be the union of the shapes of the objects it contains.
                *
                *NOTE 1    Mathematically, elementary objects have “hard” edges, with a shape value of either 0.0 or 1.0 at every point.However, when such objects are rasterized to device pixels, the shape values along the boundaries may be anti-aliased, taking on fractional values representing fractional coverage of those pixels.
                *          When such anti-aliasing is performed, it is important to treat the fractional coverage as shape rather than opacity.
                *
                *  •   Mask shape. Shape values for compositing an object may be taken from an additional source, or soft mask, independent of the object itself, as described in 11.5, "Soft Masks."
                *
                *NOTE 2    The use of a soft mask to modify the shape of an object or group, called soft clipping, can produce effects such as a gradual transition between an object and its backdrop, as in a vignette.
                *
                *  •   Constant shape. The source shape may be modified at every point by a scalar shape constant.
                *
                *NOTE 3    This is merely a convenience, since the same effect could be achieved with a shape mask whose value is the same everywhere.
                *
                *  •   Object opacity. Elementary objects have an opacity of 1.0 everywhere.
                *      The opacity of a group object shall be the result of the opacity computations for all of the objects it contains.
                *
                *  •   Mask opacity. Opacity values, like shape values, may be provided by a soft mask independent of the object being composited.
                *
                *  •   Constant opacity. The source opacity may be modified at every point by a scalar opacity constant.
                *
                *NOTE 4    It is useful to think of this value as the “current opacity,” analogous to the current colour used when painting elementary objects.
                *
                *All of the shape and opacity inputs shall have values in the range 0.0 to 1.0 (inclusive), with a default value of 1.0.
                *
                *The three shape inputs shall be multiplied together, producing an intermediate value called the source shape.
                *
                *      fs = fj x fm x fk
                *
                *The three opacity inputs shall be multiplied together, producing an intermediate value called the source opacity.
                *
                *      qs = qj x qm x qk
                *
                *Where the variables have the meanings shown in Table 138.
                *
                *Table 138 - Variables used in the source shape and opacity formulas
                *
                *              [Variable]              [Meaning]
                *
                *              fs                      Source shape
                *
                *              fj                      Object shape
                *
                *              fm                      Mask shape
                *
                *              fk                      Constant shape
                *
                *              qs                      Source opacity
                *
                *              qj                      Object opacity
                *
                *              qm                      Mask opacity
                *
                *              qk                      Constant opacity
                *
                *NOTE 5    The effect of each of these inputs is that the painting operation becomes more transparent as the input values decreases.
                *
                *When an object is painted with a tiling pattern, the object shape and object opacity for points in the object’s interior are determined by those of corresponding points in the pattern, rather than being 1.0 everywhere (see “Patterns and Transparency”).
                */

                /*11.3.7.3 Result Shape and Opacity
                *
                *In addition to a result colour, the painting operation also shall compute an associated result shape and result opacity. These computations shall be based on the union function
                *
                *      Union(b,s) = 1 - [(1 - b) x (1 - s)]
                *                 = b + s - (b x s)
                *
                *where b and s shall be the backdrop and source values to be composited.
                *
                *NOTE 1    This is a generalization of the conventional concept of union for opaque shapes, and it can be thought of as an “inverted multiplication”—a multiplication with the inputs and outputs complemented.
                *          The result tends toward 1.0: if either input is 1.0, the result is 1.0.
                *
                *The result shape and opacity shall be given by
                *
                *      fr = Union(fb,fs)
                *
                *      qr = Union(fb x qb, fs x qs) / fr
                *
                *where the variables have the meanings shown in Table 139.
                *
                *Table 139 - Variables used in the result shape and opacity formulas
                *
                *              [Variable]              [Meaning]
                *
                *              fr                      Result shape
                *
                *              fb                      Backdrop shape
                *
                *              fs                      Source shape
                *
                *              qr                      Result opacity
                *
                *              qb                      Backdrop opacity
                *
                *              qs                      Source opacity
                *
                *These formulas shall be interpreted as follows:
                *
                *  •   The result shape shall be the union of the backdrop and source shapes.
                *
                *  •   The result opacity shall be the union of the backdrop and source opacities, weighted by their respective shapes.
                *      The result shall then be divided by(normalized by) the result shape.
                *
                *NOTE 2    Since alpha is just the product of shape and opacity, it can easily be shown that
                *
                *          alpha(r) = Union(alpha(b),alpha(s)
                *
                *          This formula can be used whenever the independent shape and opacity are not needed.
                */

            /*11.3.8 Summary of Basic Compositing Computations
            *
                *This sub-clause is a summary of all the computations presented in this sub-clause. 
                *They are given in an order such that no variable is used before it is computed; also, some of the formulas have been rearranged to simplify them. 
                *See Tables 135, 138, and 139 for the meanings of the variables used in these formulas.
                *
                *      Union(b,s) = 1 - [(1 - b) x (1 - s)]
                *                 = b + s - (b x s)
                *
                *      fs = fj x fm x fk
                *      qs = qj x qm x qk
                *      fr = Union(fb,fs)
                *
                *      alpha(b) = fb x qb
                *      alpha(s) = fs x qs
                *      alpha(r) = Union(alpha(b),alpha(s))
                *
                *      qr = alpha(r)/fr
                *
                *      Cr = (1 - alpha(s)/alpha(r)) x Cb + alpha(s)/alpha(r) x [(1 - alpha(b)) x Cs + alpha(b) x B(Cb,Cs)]
                */


        }

        //11.4 Transparency Groups
        public class Transparency_Groups
        {
            /*11.4.1 General
            *
            *A transparency group is a sequence of consecutive objects in a transparency stack that shall be collected together and composited to produce a single colour, shape, and opacity at each point.
            *The result shall then be treated as if it were a single object for subsequent compositing operations.Groups may be nested within other groups to form a tree-structured group hierarchy.
            *
            *NOTE      This facilitates creating independent pieces of artwork, each composed of multiple objects, and then combining them, possibly with additional transparency effects applied during the combination.
            *
            *The objects contained within a group shall be treated as a separate transparency stack called the group stack. 
            *The objects in the stack shall be composited against an initial backdrop (discussed later), producing a composite colour, shape, and opacity for the group as a whole. 
            *The result is an object whose shape is the union of the shapes of its constituent objects and whose colour and opacity are the result of the compositing operations.
            *This object shall then be composited with the group’s backdrop in the usual way.
            *
            *In addition to its computed colour, shape, and opacity, the group as a whole may have several further attributes:
            *
            *  •   All of the input variables that affect the compositing computation for individual objects may also be applied when compositing the group with its backdrop.
            *      These variables include mask and constant shape, mask and constant opacity, and blend mode.
            *
            *  •   The group may be isolated or non-isolated, which shall determine the initial backdrop against which its stack is composited.
            *
            *  •   The group may be knockout or non-knockout, which shall determine whether the objects within its stack are composited with one another or only with the group’s backdrop.
            *
            *  •   An isolated group may specify its own blending colour space, independent of that of the group’s backdrop.
            *
            *  •   Instead of being composited onto the current page, a group’s results may be used as a source of shape or opacity values for creating a soft mask(see “Soft Masks”).
            */

            /*11.4.2 Notation for Group Compositing Computations
            *
                *This sub-clause introduces some notation for dealing with group compositing. 
                *Subsequent sub-clauses describe the group compositing formulas for a non-isolated, non-knockout group and the special properties of isolated and knockout groups.
                *
                *Since we are now dealing with multiple objects at a time, it is useful to have some notation for distinguishing among them. 
                *Accordingly, the variables introduced earlier are altered to include a second-level subscript denoting an object’s position in the transparency stack. 
                *
                *Cs(1) stands for the source colour of the ith object in the stack. 
                *The subscript 0 represents the initial backdrop; subscripts 1 to n denote the bottommost to topmost objects in an n-element stack. 
                *In addition, the subscripts band r are dropped from the variables Cb , fβ , qβ , αβ , Cr, fρ, qρ, and αρ; other variables retain their mnemonic subscripts.
                *
                *These conventions permit the compositing formulas to be restated as recurrence relations among the elements of a stack.
                *For instance, the result of the colour compositing computation for object i is denoted by Ci(formerly Cr).
                *This computation takes as one of its inputs the immediate backdrop colour, which is the result of the colour compositing computation for object i − 1; this is denoted by Ci − 1(formerly Cb).
                *
                *The revised formulas for a simple n - element stack(not including any groups) shall be, for i = 1, …, n:
                *
                *          fs(i) = fj(i) x fm(i) x fk(i)
                *          qs(i) = qj(i) x qm(i) x qk(i)
                *
                *          alpha_s(i) = fs(i) x qs(i)
                *          alpha(i) = Union(alpha(i-1),alpha_s(i))
                *
                *          f(i) = Union(f(i-1),fs(i))
                *          q(i) = alpha(i)/f(i)
                *
                *          C(i) = (1 - alpha_s(i)/alpha(i)) x C(i-1) + alpha_s(i)/alpha(i) x [(1 - alpha(i-1)) x Cs(i) + alpha(i-1) x B(i)(C(i-1),Cs(i))]
                *
                *where the variables have the meanings shown in Table 140.
                *
                *NOTE  Compare these formulas with those shown in 11.3.8, "Summary of Basic Compositing Computations."
                *
                *Table 140 - Revised variables for the basic compositing formulas
                *
                *              [Variable]                  [Meaning]
                *
                *              fs(i)                       Source shape for object i
                *
                *              fj(i)                       Object shape for object i
                *
                *              fm(i)                       Mask shape for object i
                *
                *              fk(i)                       Constant shape for object i
                *
                *              f(i)                        Result shape after compositing object i
                *
                *              qs(i)                       Source opacity for object i
                *
                *              qj(i)                       Object opacity for obect i
                *
                *              qmi                         Mask opacity for object i
                *
                *              qi                          Result opacity after compositing object i
                *
                *              alpha_s(i)                  Source alpha for object i
                *
                *              alpha(i)                    Result alpha after compositing object i
                *
                *              Cs(i)                       Source colour for object i
                *
                *              C(i)                        Result colour after compositing object i
                *
                *              B(i)(C(i-1),Cs(i))          Blend function for object i
                */

            /*11.4.3 Group Structure and Nomenclature
            *
                *As stated earlier, the elements of a group shall be treated as a separate transparency stack, referred to as the group stack. 
                *These objects shall be composited against a selected initial backdrop and the resulting colour, shape, and opacity shall then be treated as if they belonged to a single object. 
                *The resulting object is in turn composited with the group’s backdrop in the usual way.
                *
                *NOTE      This computation entails interpreting the stack as a tree.
                *          For an n - element group that begins at position i in the stack, it treats the next n objects as an n - element substack, whose elements are given an independent numbering of 1 to n. 
                *          These objects are then removed from the object numbering in the parent(containing) stack and replaced by the group object, numbered i, followed by the remaining objects to be painted on top of the group, renumbered starting at i + 1.
                *          This operation applies recursively to any nested subgroups.
                *
                *The term element(denoted Ei) refers to a member of some group; it can be either an individual object or a contained subgroup.
                *
                *From the perspective of a particular element in a nested group, there are three different backdrops of interest:
                *
                *  •   The group backdrop is the result of compositing all elements up to but not including the first element in the group. 
                *      (This definition is altered if the parent group is a knockout group; see 11.4.6, "Knockout Groups")
                *
                *  •   The initial backdrop is a backdrop that is selected for compositing the group’s first element.
                *      This is either the same as the group backdrop(for a non-isolated group) or a fully transparent backdrop(for an isolated group).
                *
                *  •   The immediate backdrop is the result of compositing all elements in the group up to but not including the current element.
                *
                *When all elements in a group have been composited, the result shall be treated as if the group were a single object, which shall then be composited with the group backdrop. 
                *This operation shall occur whether the initial backdrop chosen for compositing the elements of the group was the group backdrop or a transparent backdrop. 
                *A conforming reader shall ensure that the backdrop’s contribution to the overall result is applied only once.
            */

            /*11.4.4 Group Compositing Computations
            *
                *The colour and opacity of a group shall be defined by the group compositing function:
                *
                *  <C,f,alpha> = Composite(C0,alpha0,G)
                *
                *where the variables have the meanings shown in Table 141.
                *
                *Table 141 - Arguments and results of the group compositing function
                *
                *          [Variable]              [Meaning]
                *
                *          G                       The transparency group: a compound object consisting of all elements E1, …, En of the group — the n constituent objects’ colours, shapes, opacities, and blend modes
                *
                *          C0                      Colour of the group’s backdrop
                *
                *          C                       Computed colour of the group, which shall be used as the source colour when the group is treated as an object
                *
                *          f                       Computed shape of the group, which shall be used as the object shape when the group is treated as an object
                *
                *          alpha0                  Alpha of the group’s backdrop
                *
                *          alpha                   Computed alpha of the group, which shall be used as the object alpha when the group is treated as an object
                *
                *NOTE 1        The opacity is not given explicitly as an argument or result of this function. 
                *              Almost all of the computations use the product of shape and opacity (alpha) rather than opacity alone; therefore, it is usually convenient to work directly with shape and alpha rather than shape and opacity. 
                *              When needed, the opacity can be computed by dividing the alpha by the associated shape.
                *
                *The result of applying the group compositing function shall then be treated as if it were a single object, which in turn is composited with the group’s backdrop according to the formulas defined in this sub - clause.
                *In those formulas, the colour, shape, and alpha(C, f, and α) calculated by the group compositing function shall be used, respectively, as the source colour Cs, the object shape fj, and the object alpha αj.
                *
                *The group compositing formulas for a non-isolated, non - knockout group are defined as follows:
                *
                *  •   Initialization:
                *
                *      fg0 = alphag0 = 0.0
                *
                *  •   For each group element Ei ∈ G (i = 1, ..., n):
                *
                *      <Cs(i),fj(i),alphaj(i)> = Composite(C(i-1),alpha(i-1),E(i))                             if E(i) is a group
                *                        = intrinsice color, shape, and (shape x opacity) of Ei        otherwise
                *
                *      fs(i) = fj(i) x fm(i) x fk(i)
                *      alphas(i) = alphaj(i) x (fm(i) x qm(i)) x (fk(i) x qk(i))
                *
                *      fg(i) = Union(fg(i-1),fs(i))
                *      alphag(i) = Union(alphag(i-1),alphas(i))
                *      alpha(i) = Union(alpha0, alphag(i))
                *
                *      C(i) = (1 - alphas(i)/alpha(i)) x C(i-1) + alphas(i)/alpha(i) x ((1 - alpha(i-1) x Cs(i) + alpha(i-1) x B(i)(C(i-1),Cs(i))
                *
                *  •   Result:
                *
                *      C = Cn + (Cn - C0) x (alpha0/alphag(n) - alpha0)
                *
                *      f = fg(n)
                *
                *      alpha = alphag(n)
                *
                *where the variables have the meanings shown in Table 142 (in addition to those in Table 141).
                *
                *For an element Ei that is an elementary object, the colour, shape, and alpha values Cs(i), fj(i), and alphaj(i) are intrinsic attributes of the object.For an element that is a group, 
                *the group compositing function shall be applied recursively to the subgroup and the resulting C, f, and α values shall be used for its Cs(i), fj(i), and alphaj(i) in the calculations for the parent group.
                *
                *Table 142 - Variables used in the group compositing formulas
                *
                *          [Variable]              [Meaning]
                *
                *          E(i)                    Element i of the group: a compound variable representing the element’s colour, shape, opacity, and blend mode
                *
                *          fs(i)                   Source shape for element Ei
                *
                *          fj(i)                   Object shape for element Ei
                *
                *          fm(i)                   Mask shape for element Ei
                *
                *          fk(i)                   Constant shape for element Ei
                *
                *          fg(i)                   Group shape: the accumulated source shapes of group elements E1 to Ei , excluding the initial backdrop
                *
                *          qm(i)                   Mask opacity for element Ei
                *
                *          qk(i)                   Constant opacity for element Ei
                *
                *          alphas(i)               Source alpha for element Ei
                *
                *          alphaj(i)               Object alpha for element Ei : the product of its object shape and object opacity
                *
                *          alphag(i)               Group alpha: the accumulated source alphas of group elements E1 to Ei, excluding the initial backdrop
                *
                *          alpha(i)                Accumulated alpha after compositing element Ei, including the initial backdrop
                *
                *          Cs(i)                   Source colour for element Ei
                *
                *          C(i)                    Accumulated colour after compositing element Ei, including the initial backdrop
                *
                *          B(i)(C(i-1),Cs(i))      Blend function for element Ei
                *
                *
                *NOTE 2        The elements of a group are composited onto a backdrop that includes the group’s initial backdrop. 
                *              This is done to achieve the correct effects of the blend modes, most of which are dependent on both the backdrop and source colours being blended. 
                *              This feature is what distinguishes non-isolated groups from isolated groups, discussed in the next sub-clause.
                *
                *NOTE 3        Special attention should be directed to the formulas at the end that compute the final results C, f, and α, of the group compositing function. 
                *              Essentially, these formulas remove the contribution of the group backdrop from the computed results.
                *              This ensures that when the group is subsequently composited with that backdrop(possibly with additional shape or opacity inputs or a different blend mode), the backdrop’s contribution is included only once.
                *
                *              For colour, the backdrop removal is accomplished by an explicit calculation, whose effect is essentially the reverse of compositing with the Normal blend mode. 
                *              The formula is a simplification of the following formulas, which present this operation more intuitively:
                *
                *              (see Equation on page 337)
                *
                *              where phi(b) is the backdrop fraction, the relative contribution of the backdrop colour to the overall colour.   
                *
                *NOTE 4        For shape and alpha, backdrop removal is accomplished by maintaining two sets of variables to hold the accumulated values. 
                *              There is never any need to compute the corresponding complete shape, fi, that includes the backdrop contribution.
                *
                *The group shape and alpha, and , shall accumulate only the shape and alpha of the group elements, excluding the group backdrop. 
                *Their final values shall become the group results returned by the group compositing function. 
                *The complete alpha, αi, includes the backdrop contribution as well; its value is used in the colour compositing computations.
                *
                *NOTE 5        As a result of these corrections, the effect of compositing objects as a group is the same as that of compositing them separately(without grouping) if the following conditions hold:
                *
                *              The group is non - isolated and has the same knockout attribute as its parent group(see 11.4.5, "Isolated Groups," and “Knockout Groups”).
                *
                *              When compositing the group’s results with the group backdrop, the Normal blend mode is used, and the shape and opacity inputs are always 1.0.
                */

            /*11.4.5 Isolated Groups
            *
                *An isolated group is one whose elements shall be composited onto a fully transparent initial backdrop rather than onto the group’s backdrop. 
                *The resulting source colour, object shape, and object alpha for the group shall be therefore independent of the group backdrop. 
                *The only interaction with the group backdrop shall occur when the group’s computed colour, shape, and alpha are composited with it.
                *
                *In particular, the special effects produced by the blend modes of objects within the group take into account only the intrinsic colours and opacities of those objects; they shall not be influenced by the group’s backdrop.
                *
                *EXAMPLE       Applying the Multiply blend mode to an object in the group produces a darkening effect on other objects lower in the group’s stack but not on the group’s backdrop.
                *
                *              Figure L.17 in Annex L illustrates this effect for a group consisting of four overlapping circles in a light gray colour(C = M = Y = 0.0; K = 0.15). 
                *              The circles are painted within the group with opacity 1.0 in the Multiply blend mode; the group itself is painted against its backdrop in Normal blend mode.
                *              In the top row, the group is isolated and thus does not interact with the rainbow backdrop. 
                *              In the bottom row, the group is non - isolated and composites with the backdrop.
                *              The figure also illustrates the difference between knockout and non-knockout groups(see “Knockout Groups”).
                *
                *NOTE 1        Conceptually, the effect of an isolated group could be represented by a simple object that directly specifies a colour, shape, and opacity at each point. 
                *              This flattening of an isolated group is sometimes useful for importing and exporting fully composited artwork in applications.
                *              Furthermore, a group that specifies an explicit blending colour space shall be an isolated group.
                *
                *For an isolated group, the group compositing formulas shall be altered by adding one statement to the initialization:
                *
                *              alpha(0) = 0.0              if the group is isolated
                *
                *That is, the initial backdrop on which the elements of the group are composited shall be transparent rather than inherited from the group’s backdrop.
                *
                *NOTE 2        This substitution also makes C0 undefined, but the normal compositing formulas take care of that. 
                *              Also, the result computation for C automatically simplifies to C = Cn, since there is no backdrop contribution to be factored out.
                */

            /*11.4.6 Knockout Groups
            *
                *In a knockout group, each individual element shall be composited with the group’s initial backdrop rather than with the stack of preceding elements in the group. 
                *When objects have binary shapes (1.0 for inside, 0.0 for outside), each object shall overwrite (knocks out) the effects of any earlier elements it overlaps within the same group. 
                *At any given point, only the topmost object enclosing the point shall contribute to the result colour and opacity of the group as a whole.
                *
                *EXAMPLE       Figure L.17 in Annex L about 11.4.5, "Isolated Groups," illustrates the difference between knockout and non - knockout groups.
                *              In the left column, the four overlapping circles are defined as a knockout group and therefore do not composite with each other within the group.
                *              In the right column, the circles form a non-knockout group and thus do composite with each other.
                *              In each column, the upper and lower figures depict an isolated and a non - isolated group, respectively.
                *
                *NOTE 1        This model is similar to the opaque imaging model, except that the “topmost object wins” rule applies to both the colour and the opacity.
                *              Knockout groups are useful in composing a piece of artwork from a collection of overlapping objects, where the topmost object in any overlap completely obscures those beneath. 
                *              At the same time, the topmost object interacts with the group’s initial backdrop in the usual way, with its opacity and blend mode applied as appropriate.
                *
                *The concept of knockout is generalized to accommodate fractional shape values. 
                *In that case, the immediate backdrop shall be only partially knocked out and shall be replaced by only a fraction of the result of compositing the object with the initial backdrop.
                *
                *The restated group compositing formulas deal with knockout groups by introducing a new variable, b, which is a subscript that specifies which previous result to use as the backdrop in the compositing computations: 0 in a knockout group or i − 1 in a non-knockout group. 
                *When b = i − 1, the formulas simplify to the ones given in 11.4.4, "Group Compositing Computations."
                *
                *In the general case, the computation shall proceed in two stages:
                *
                *  a)  Composite the source object with the group’s initial backdrop, disregarding the object’s shape and using a source shape value of 1.0 everywhere.
                *      This produces unnormalized temporary alpha and colour results, αtand Ct.
                *
                *NOTE 2        For colour, this computation is essentially the same as the unsimplified colour compositing formula given in 11.3.6, "Interpretation of Alpha," but using a source shape of 1.0.
                *
                *              alpha(t) = Union(alphag(b),qs(i))
                *
                *              Ct = (1 - qs(i)) x alpha(b) x Cb + qs(i) x ((1 - alpha(b)) x Cs(i) + alpha(b) x B(i)(C(b),Cs(i))
                *
                *  b)  Compute a weighted average of this result with the object’s immediate backdrop, using the source shape as the weighting factor. 
                *      Then normalize the result colour by the result alpha:
                *
                *      alphag(i) = (1 - fs(i)) x alphag(i-1) + fs(i) x alpha(t)
                *
                *      alpha(i) = Union(alpha(0),alphag(i))
                *
                *      C(i) = ( (1 - fs(i)) x alpha(i-1) x C(i-1) + fs(i) x Ct ) / alpha(i)
                *
                *This averaging computation shall be performed for both colour and alpha.
                *
                *NOTE 3        The preceding formulas show this averaging directly. 
                *              The formulas in 11.4.8, "Summary of Group Compositing Computations," are slightly altered to use source shape and alpha rather than source shape and opacity, avoiding the need to compute a source opacity value explicitly.
                *
                *NOTE 4        Ct in Group Compositing Computations is slightly different from the preceding Ct: it is premultiplied by.
                *
                *NOTE 5        The extreme values of the source shape produce the straightforward knockout effect. 
                *              That is, a shape value of 1.0(inside) yields the colour and opacity that result from compositing the object with the initial backdrop.
                *              A shape value of 0.0(outside) leaves the previous group results unchanged.
                *The existence of the knockout feature is the main reason for maintaining a separate shape value rather than only a single alpha that combines shape and opacity.
                *The separate shape value shall be computed in any group that is subsequently used as an element of a knockout group.
                *
                *A knockout group may be isolated or non - isolated; that is, isolated and knockout are independent attributes. 
                *A non-isolated knockout group composites its topmost enclosing element with the group’s backdrop. 
                *An isolated knockout group composites the element with a transparent backdrop.
                *
                *NOTE 6        When a non - isolated group is nested within a knockout group, the initial backdrop of the inner group is the same as that of the outer group; it is not the immediate backdrop of the inner group. 
                *              This behaviour, although perhaps unexpected, is a consequence of the group compositing formulas when b = 0.
                *
                *
                */

            /*11.4.7 Page Group
            *
                *All of the elements painted directly onto a page—both top-level groups and top-level objects that are not part of any group—shall be treated as if they were contained in a transparency group P, which in turn is composited with a context-dependent backdrop. 
                *This group is called the page group.
                *
                *The page group shall be treated in one of two distinctly different ways:
                *
                *  •   Ordinarily, the page shall be imposed directly on an output medium, such as paper or a display screen. 
                *      The page group shall be treated as an isolated group, whose results shall then be composited with a backdrop colour appropriate for the medium. 
                *      The backdrop is nominally white, although varying according to the actual properties of the medium.
                *      However, some conforming readers may choose to provide a different backdrop, such as a checker board or grid to aid in visualizing the effects of transparency in the artwork.
                *
                *  •   A “page” of a PDF file may be treated as a graphics object to be used as an element of a page of some other document.
                *
                *EXAMPLE       This case arises, for example, when placing a PDF file containing a piece of artwork produced by a drawing program into a page layout produced by a layout program.
                *              In this situation, the PDF “page” is not composited with the media colour; instead, it is treated as an ordinary transparency group, which can be either isolated or non - isolated and is composited with its backdrop in the normal way.
                *
                *The remainder of this sub - clause pertains only to the first use of the page group, where it is to be imposed directly on the medium.
                *
                *The colour C of the page at a given point shall be defined by a simplification of the general group compositing formula:
                *
                *      <Cg,fg,alphag> = Composite(U,0,P)
                *
                *      C = (1 - alphag) x W + alphag x Cg
                *
                *where the variables have the meanings shown in Table 143. 
                *The first formula computes the colour and alpha for the group given a transparent backdrop—in effect, treating P as an isolated group. 
                *The second formula composites the results with the context-dependent backdrop (using the equivalent of the Normal blend mode).
                *
                *Table 143 - Variables used in the page group compositing formulas
                *
                *          [Variable]                  [Meaning]
                *
                *          P                           The page group, consisting of all elements E1, …, En in the page’s top-level stack
                *
                *          Cg                          Computed colour of the page group
                *
                *          fg                          Computed shaoe of the page group
                *
                *          alphag                      Computed alpha of the page group
                *
                *          C                           Computed colour of the page
                *
                *          W                           Initial colour of the page (nominally white but may vary depending on the properties of the medium or the needs of the application)
                *
                *          U                           An undefined colour (which is not used, since the α0argument of Composite is 0)
                *
                *
                *If not otherwise specified, the page group’s colour space shall be inherited from the native colour space of the output device—that is, a device colour space, such as DeviceRGB or DeviceCMYK. 
                *An explicit colour space should be specified, particularly a CIE-based space, to ensure more predictable results of the compositing computations within the page group. 
                *In this case, all page-level compositing shall be done in the specified colour space, and the entire result shall then be converted to the native colour space of the output device before being composited with the context-dependent backdrop.
                *
                *NOTE      This case also arises when the page is not actually being rendered but is converted to a flattened representation in an opaque imaging model, such as PostScript.
                */

            /*11.4.8 Summary of Group Compositing Computations
            *
                *This sub-clause is a restatement of the group compositing formulas that also takes isolated groups and knockout groups into account. 
                *See Tables 141 and 142 in 11.4.4, "Group Compositing Computations," for the meanings of the variables.
                *
                *      <C,f,alpha> = Composite(c0,alpha0,G)
                *
                *Initialization:
                *
                *      fg0 = alphag0 = 0
                *
                *      alpha0 = 0                      if the group is isolated
                *
                *For each group element Ei ∈ G (i = 1, ..., n):
                *
                *      b = 0                           if the group is knockout
                *
                *        = i-1                         otherwise
                *
                *      <Csi,fji,alphaji> = Composite(Cb,alphab,Ei)                                             if Ei is a group
                *
                *                        = intrinsic color, shape, and (shape x opacity) of Ei                 otherwise        
                *
                *      fsi = fji x fmi x fki
                *
                *      alphasi = alphaji x (fmi x qmi) x (fki x qki)
                *
                *      fgi = Union(fg(i-1),fs(i))
                *
                *      alphagi = (1 - fsi) x alphag(i-1)   + (fsi - alphasi) x alphagb + alphas
                *
                *      alphai = Union(alpha0, alphagi)
                *
                *      Ct = (fsi - alphasi) x alphab x Cb + alphasi x ((1 - alphab) x Csi + alphab x Bi(Cb,Csi)
                *
                *      Ci = ( (1 - fsi) x alpha(i-1) x C(i-1) + Ct ) / alphai
                *
                *Result:
                *
                *      C = Cn + (Cn - C0) x ( alpha0/alphagn - alpha0 )
                *
                *      f = fgn
                *
                *      alpha = alphagn
                *
                *NOTE          Once again, keep in mind that these formulas are in their most general form. 
                *              They can be significantly simplified when some sources of shape and opacity are not present or when shape and opacity need not be maintained separately. 
                *              Furthermore, in each specific type of group (isolated or not, knockout or not), some terms of these formulas cancel or drop out. 
                *              An efficient implementation should use the simplified derived formulas.
                */

            public void getTransparencyGroupXObject(ref PDF_Group_Attributes_Dictionary pdf_group_attributes_dictionary, ref string stream_hexa, int offset)
            {

            }

        }

        //11.5 Soft Masks
        public class Soft_Masks
        {
            /*11.5.1 General
            *
            *As stated in earlier sub-clauses, the shape and opacity values used in compositing an object may include components called the mask shape(fm) and mask opacity(qm), which may be supplied in a PDF file from a source independent of the object. 
            *Such an independent source, called a soft mask, defines values that may vary across different points on the page.
            *
            *NOTE 1        The word soft emphasizes that the mask value at a given point is not limited to just 0.0 or 1.0 but can take on intermediate fractional values as well.
            *              Such a mask is typically the only means of providing position-dependent opacity values, since elementary objects do not have intrinsic opacity of their own.
            *
            *NOTE 2        A mask used as a source of shape values is also called a soft clip, by analogy with the “hard” clipping path of the opaque imaging model(see Section 8.5.4). 
            *              The soft clip is a generalization of the hard clip: a hard clip can be represented as a soft clip having shape values of 1.0 inside and 0.0 outside the clipping path.Everywhere inside a hard clipping path, the source object’s colour replaces the backdrop; everywhere outside, the backdrop shows through unchanged.
            *              With a soft clip, by contrast, a gradual transition can be created between an object and its backdrop, as in a vignette.
            *
            *A mask may be defined by creating a transparency group and painting objects into it, thereby defining colour, shape, and opacity in the usual way./
            *The resulting group may then be used to derive the mask in either of two ways, as described in the following sub-clauses.
            */

            /*11.5.2 Deriving a Soft Mask from Group Alpha
            *
                *
                *In the first method of defining a soft mask, the colour, shape, and opacity of a transparency group G shall be first computed by the usual formula
                *
                *      <C,f,alpha> = Composite(C0,alpha0,G)
                *
                *where C0 and α0 represent an arbitrary backdrop whose value does not contribute to the eventual result.The C, f, and α results shall be the group’s colour, shape, and alpha, respectively, with the backdrop factored out.
                *
                *The mask value at each point shall then be derived from the alpha of the group.
                *The alpha value shall be passed through a separately specified transfer function, allowing the masking effect to be customized.
                *
                *NOTE      Since the group’s colour is not used in this case, there is no need to compute it.
            */

            /*11.5.3 Deriving a Soft Mask from Group Luminosity
            *
                *The second method of deriving a soft mask from a transparency group shall begin by compositing the group with a fully opaque backdrop of a specified colour. 
                *The mask value at any given point shall then be defined to be the luminosity of the resulting colour.
                *
                *NOTE 1        This allows the mask to be derived from the shape and colour of an arbitrary piece of artwork drawn with ordinary painting operators.
                *
                *The colour C used to create the mask from a group G shall be defined by
                *
                *          <Cg,fg,alphag> = Composite(C0,1,G)
                *
                *                         C = (1 - alphag) x C0 + alphag x Cg
                *
                *where C0 is the selected backdrop colour.
                *
                *G may be any kind of group—isolated or not, knockout or not—producing various effects on the C result in each case. 
                *The colour C shall then be converted to luminosity in one of the following ways, depending on the group’s colour space:
                *
                *  •   For CIE-based spaces, convert to the CIE 1931 XYZ space and use the Y component as the luminosity.
                *      This produces a colourimetrically correct luminosity.
                *
                *NOTE 2        In the case of a PDF CalRGB space, the formula is
                *
                *              Y = Ya x A^Gr + Yb x B^Gg + Yc x C^Gb
                *
                *              using components of the Gamma and Matrix entries of the colour space dictionary (see Table 64 in "CIE-Based Colour Spaces"). An analogous computation applies
                *              to other CIE-based colour spaces.
                *
                *  •   For device colour spaces, convert the colour to DeviceGray by implementation-defined means and use the resulting gray value as the luminosity, with no compensation for gamma or other colour calibration.
                *
                *NOTE 3        This method makes no pretence of colourimetric correctness; it merely provides a numerically simple means to produce continuous-tone mask values. 
                *              The following are formulas for converting from DeviceRGB and DeviceCMYK, respectively:
                *
                *              Y = 0.30 x R + 0.59 x G + 0.11 x B
                *
                *              Y = 0.30 x (1 - C) x (1 - K)
                *
                *                      + 0.59 x (1 - M) x (1 - K)
                *
                *                      + 0.11 x (1 - Y) x (1 - K)
                *
                *Following this conversion, the result shall be passed through a separately specified transfer function, allowing the masking effect to be customized.
                *
                *NOTE 4        The backdrop colour most likely to be useful is black, which causes any areas outside the group’s shape to have zero luminosity values in the resulting mask.
                *              If the contents of the group are viewed as a positive mask, this produces the results that would be expected with respect to points outside the shape.
                */

            public void getSoftMask(ref PDF_Soft_Masks pdf_soft_mask, ref string stream_hexa, int offset)
            {

            }
            

        }

        //11.6 Specifying Transparency in PDF
        public class Specifying_Transparency_in_PDF
        {
            /*11.6.1 General
            *
            *The preceding sub-clauses have presented the transparent imaging model at an abstract level, with little mention of its representation in PDF.
            *This sub-clause describes the facilities available for specifying transparency in PDF.
            */

            /*11.6.2 Specifying Source and Backdrop Colours
            *
                *Single graphics objects, as defined in “Graphics Objects”, shall be treated as elementary objects for transparency compositing purposes (subject to special treatment for text objects, as described in “Text Knockout”). 
                *That is, all of a given object shall be considered to be one element of a transparency stack. 
                *Portions of an object shall not be composited with one another, even if they are described in a way that would seem to cause overlaps (such as a self-intersecting path, combined fill and stroke of a path, or a shading pattern containing an overlap or fold-over). 
                *An object’s source colour Cs, used in the colour compositing formula, shall be specified in the same way as in the opaque imaging model: by means of the current colour in the graphics state or the source samples in an image. 
                *The backdrop colour Cb shall be the result of previous painting operations.
            */

            /*11.6.3 Specifying Blending Colour Space and Blend Mode
            *
                *The blending colour space shall be an attribute of the transparency group within which an object is painted; its specification is described in 11.6.6, "Transparency Group XObjects." 
                *The page as a whole shall also be treated as a group, the page group (see “Page Group”), with a colour space attribute of its own. 
                *If not otherwise specified, the page group’s colour space shall be inherited from the native colour space of the output device.
                *
                *The blend mode B(Cb, Cs) shall be determined by the current blend mode parameter in the graphics state(see “Graphics State”), which is specified by the BM entry in a graphics state parameter dictionary(“Graphics State Parameter Dictionaries”).
                *Its value shall be either a name object, designating one of the standard blend modes listed in Tables 136 and 137 in 11.3.5, "Blend Mode," or an array of such names. 
                *In the latter case, the application shall use the first blend mode in the array that it recognizes(or Normal if it recognizes none of them).
                *
                *NOTE      New blend modes may be introduced in the future, and conforming readers that do not recognize them should have reasonable fallback behavior.
                *
                *The current blend mode shall always apply to process colour components; but only sometimes may apply to spot colorants, see 11.7.4.2, "Blend Modes and Overprinting," for details.
            */

            /*11.6.4 Specifying Shape and Opacity
            */
               
            /*11.6.4.1 General
                *
                *As discussed under 11.3.7.2, "Source Shape and Opacity," the shape(f) and opacity(q) values used in the compositing computation shall come from one or more of the following sources:
                *
                *  •   The intrinsic shape(fj) and opacity(qj) of the object being composited
                *
                *  •   A separate shape(fm) or opacity(qm) mask independent of the object itself
                *
                *  •   A scalar shape(fk) or opacity(qk) constant to be added at every point
                *
                *The following sub-clauses describe how each of these shape and opacity sources shall be specified in PDF.
                */

                /*11.6.4.2 Object Shape and Opacity
                *
                *The shape value fj of an object painted with PDF painting operators shall be defined as follows:
                *
                *  •   For objects defined by a path or a glyph and painted in a uniform colour with a path-painting or text-showing operator (“Path - Painting Operators”, and “Text - Showing Operators”), the shape shall always be 1.0 inside and 0.0 outside the path.
                *
                *  •   For images(“Images”), the shape shall be 1.0 inside the image rectangle and 0.0 outside it. 
                *      This may be further modified by an explicit or colour key mask (“Explicit Masking” and “Colour Key Masking”).
                *
                *  •   For image masks(“Stencil Masking”), the shape shall be 1.0 for painted areas and 0.0 for masked areas.
                *
                *  •   For objects painted with a tiling pattern (“Tiling Patterns”) or a shading pattern(“Shading Patterns”), the shape shall be further constrained by the objects that define the pattern(see “Patterns and Transparency”).
                *
                *  •   For objects painted with the sh operator (“Shading Operator”), the shape shall be 1.0 inside and 0.0 outside the bounds of the shading’s painting geometry, disregarding the Background entry in the shading dictionary(see “Shading Dictionaries”).
                *
                *All elementary objects shall have an intrinsic opacity qj of 1.0 everywhere.
                *Any desired opacity less than 1.0 shall be applied by means of an opacity mask or constant, as described in the following sub-clauses.
                */

                /*11.6.4.3 Mask Shape and Opacity
                *
                *At most one mask input—called a soft mask, or alpha mask—shall be provided to any PDF compositing operation. 
                *The mask may serve as a source of either shape(fm) or opacity(qm) values, depending on the setting of the alpha source parameter in the graphics state(see “Graphics State”).
                *This is a boolean flag, set with the AIS(“alpha is shape”) entry in a graphics state parameter dictionary(“Graphics State Parameter Dictionaries”): true if the soft mask contains shape values, false for opacity.
                *
                *The soft mask shall be specified in one of the following ways:
                *
                *  •   The current soft mask parameter in the graphics state, set with the SMask entry in a graphics state parameter dictionary, contains a soft - mask dictionary(see “Soft - Mask Dictionaries”) defining the contents of the mask.
                *      The name None may be specified in place of a soft - mask dictionary, denoting the absence of a soft mask.In this case, the mask shape or opacity shall be implicitly 1.0 everywhere.
                *
                *  •   An image XObject may contain its own soft - mask image in the form of a subsidiary image XObject in the SMask entry of the image dictionary(see “Image Dictionaries”).
                *      This mask, if present, shall override any explicit or colour key mask specified by the image dictionary’s Mask entry.
                *      Either form of mask in the image dictionary shall override the current soft mask in the graphics state.
                *
                *  •   An image XObject that has a JPXDecode filter as its data source may specify an SMaskInData entry, indicating that the soft mask is embedded in the data stream(see “JPXDecode Filter”).
                *
                *NOTE      The current soft mask in the graphics state is intended to be used to clip only a single object at a time (either an elementary object or a transparency group). 
                *          If a soft mask is applied when painting two or more overlapping objects, the effect of the mask multiplies with itself in the area of overlap (except in a knockout group), producing a result shape or opacity that is probably not what is intended. 
                *          To apply a soft mask to multiple objects, it is usually best to define the objects as a transparency group and apply the mask to the group as a whole. 
                *          These considerations also apply to the current alpha constant (see the next sub-clause).
                */

                /*11.6.4.4 Constant Shape and Opacity
                *
                *The current alpha constant parameter in the graphics state(see “Graphics State”) shall be two scalar values—one for strokes and one for all other painting operations—to be used for the constant shape(fk) or constant opacity(qk) component in the colour compositing formulas.
                *
                *NOTE 1        This parameter is analogous to the current colour used when painting elementary objects.
                *
                *The nonstroking alpha constant shall also be applied when painting a transparency group’s results onto its backdrop.
                *
                *The stroking and nonstroking alpha constants shall be set, respectively, by the CA and ca entries in a graphics state parameter dictionary(see “Graphics State Parameter Dictionaries”).
                *As described previously for the soft mask, the alpha source flag in the graphics state shall determine whether the alpha constants are interpreted as shape values(true) or opacity values(false).
                *
                *NOTE 2        The note at the end of 11.6.4.3, "Mask Shape and Opacity," applies to the current alpha constant parameter as well as the current soft mask.
                */

            /*11.6.5 Specifying Soft Masks
            */
                
                /*11.6.5.1 General
                *
                *As noted under 11.6.4.3, "Mask Shape and Opacity," soft masks for use in compositing computations may be specified in one of the following ways:
                *
                *  •   As a soft - mask dictionary in the current soft mask parameter of the graphics state; see 11.6.5.2, "Soft-Mask Dictionaries," for more details.
                *
                *  •   As a soft - mask image associated with a sampled image; see 11.6.5.3, "Soft-Mask Images," for more details.
                *
                *  •   (PDF 1.5) as a mask channel embedded in JPEG2000 encoded data; see “JPXDecode Filter”, and the SMaskInData entry of Table 89 for more details.
                */

                /*11.6.5.2 Soft-Mask Dictionaries
                *
                *The most common way of defining a soft mask is with a soft - mask dictionary specified as the current soft mask in the graphics state(see “Graphics State”).
                *Table 144 shows the contents of this type of dictionary.
                *
                *The mask values shall be derived from those of a transparency group, using one of the two methods described in 11.5.2, "Deriving a Soft Mask from Group Alpha," and 11.5.3, "Deriving a Soft Mask from Group Luminosity."
                *The group shall be defined by a transparency group XObject(see “Transparency Group XObjects”) designated by the G entry in the soft-mask dictionary.The S(subtype) entry shall specify which of the two derivation methods to use:
                *
                *  •   If the subtype is Alpha, the transparency group XObject G shall be evaluated to compute a group alpha only. 
                *      The colours of the constituent objects shall be ignored and the colour compositing computations shall not be performed. 
                *      The transfer function TR shall then be applied to the computed group alpha to produce the mask values. Outside the bounding box of the transparency group, the mask value shall be the result of applying the transfer function to the input value 0.0.
                *  •   If the subtype is Luminosity, the transparency group XObject G shall be composited with a fully opaque backdrop whose colour is everywhere defined by the soft - mask dictionary’s BC entry.The computed result colour shall then be converted to a single - component luminosity value, and the transfer function TR shall be applied to this luminosity to produce the mask values. 
                *      Outside the transparency group’s bounding box, the mask value shall be derived by transforming the BC colour to luminosity and applying the transfer function to the result.
                *
                *The mask’s coordinate system shall be defined by concatenating the transformation matrix specified by the Matrix entry in the transparency group’s form dictionary (see “Form Dictionaries”) with the current transformation matrix at the moment the soft mask is established in the graphics state with the gs operator.
                *
                *In a transparency group XObject that defines a soft mask, spot colour components shall never be available, even if they are available in the group or page on which the soft mask is used.
                *If the group XObject’s content stream specifies a Separation or DeviceN colour space that uses spot colour components, the alternate colour space shall be substituted(see “Separation Colour Spaces” and “DeviceN Colour Spaces”).
                *
                *Table 144 - Entries in a soft-mask dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Mask for a soft-mask dictionary.
                *
                *          S                   name                (Required) A subtype specifying the method to be used in deriving the mask values from the transparency group specified by the G entry:
                *
                *                                                  Alpha           The group’s computed alpha shall be used, disregarding its colour(see “Deriving a Soft Mask from Group Alpha”).
                *
                *                                                  Luminosity      The group’s computed colour shall be converted to a single - component luminosity value(see “Deriving a Soft Mask from Group Luminosity”).
                *
                *          G                   stream              (Required) A transparency group XObject (see “Transparency Group XObjects”) to be used as the source of alpha or colour values for deriving the mask. 
                *                                                  If the subtype S is Luminosity, the group attributes dictionary shall contain a CS entry defining the colour space in which the compositing computation is to be performed.
                *
                *          BC                  array               (Optional) An array of component values specifying the colour to be used as the backdrop against which to composite the transparency group XObject G. 
                *                                                  This entry shall be consulted only if the subtype S is Luminosity. 
                *                                                  The array shall consist of n numbers, where n is the number of components in the colour space specified by the CS entry in the group attributes dictionary (see “Transparency Group XObjects”). 
                *                                                  Default value: the colour space’s initial value, representing black.
                *
                *          TR                  function or         (Optional) A function object (see “Functions”) specifying the transfer function to be used in deriving the mask values. 
                *                              name                The function shall accept one input, the computed group alpha or luminosity (depending on the value of the subtype S), and shall return one output, the resulting mask value. 
                *                                                  The input shall be in the range 0.0 to 1.0. The computed output shall be in the range 0.0 to 1.0; if it falls outside this range, it shall be forced to the nearest valid value. 
                *                                                  The name Identitymay be specified in place of a function object to designate the identity function. 
                *                                                  Default value: Identity.
                */  

                /*11.6.5.3 Soft-Mask Images
                *
                *The second way to define a soft mask is by associating a soft-mask image with an image XObject.
                *This is a subsidiary image XObject specified in the SMask entry of the parent XObject’s image dictionary(see “Image Dictionaries”).
                *Entries in the subsidiary image dictionary for such a soft - mask image shall have the same format and meaning as in that of an ordinary image XObject(as described in Table 89 in “Image Dictionaries”), subject to the restrictions listed in Table 145.This type of image dictionary may contain an additional entry, Matte.
                *
                *When an image is accompanied by a soft - mask image, it is sometimes advantageous for the image data to be preblended with some background colour, called the matte colour.
                *Each image sample represents a weighted average of the original source colour and the matte colour, using the corresponding mask sample as the weighting factor. 
                *(This is a generalization of a technique commonly called premultiplied alpha.)
                *
                *If the image data is preblended, the matte colour shall be specified by a Matte entry in the soft-mask image dictionary (see Table 145). 
                *The preblending computation, performed independently for each component, shall be
                *
                *      c' = m + alpha x (c - m)
                *
                *where
                *
                *      c′ is the value to be provided in the image source data
                *
                *      c is the original image component value
                *
                *      m is the matte colour component value
                *
                *      α is the corresponding mask sample
                *
                *This computation shall use actual colour component values, with the effects of the Filter and Decodetransformations already performed.
                *The computation shall be the same whether the colour space is additive or subtractive.
                *
                *Table 145 - Restrictions on the entries in a soft-mask image dictionary
                *
                *          [Key]               [Restriction]
                *
                *          Type                If present, shall be XObject.
                *
                *          Subtype             Shall be Image.
                *
                *          Width               If a Matte entry (see Table 146) is present, shall be the same as the Width value of the parent image; otherwise independent of it. 
                *                              Both images shall be mapped to the unit square in user space (as are all images), regardless of whether the samples coincide individually.
                *
                *          Height              Same considerations as for Width.
                *
                *          ColorSpace          Required; shall be DeviceGray.
                *
                *          BitsPerComponent    Required.
                *
                *          Intent              Ignored.
                *
                *          ImageMask           Shall be false or absent.
                *
                *          Mask                Shall be absent.
                *
                *          Decode              Default value: [0 1].
                *
                *          Interpolate         Optional.
                *
                *          Alternates          Ignored.
                *
                *          Name                Ignored.
                *
                *          StructParent        Ignored.
                *
                *          ID                  Ignored.
                *
                *          OPI                 Ignored.
                *
                *
                *
                *Table 146 - Additional entry in a soft-mask image dictionary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          Matte               array                   (Optional; PDF 1.4) An array of component values specifying the matte colour with which the image data in the parent image shall have been preblended. 
                *                                                      The array shall consist of n numbers, where n is the number of components in the colour space specified by the ColorSpace entry in the parent image’s image dictionary; the numbers shall be valid colour components in that colour space. 
                *                                                      If this entry is absent, the image data shall not be preblended.
                *
                *When preblended image data is used in transparency blending and compositing computations, the results shall be the same as if the original, unblended image data were used and no matte colour were specified. 
                *In particular, the inputs to the blend function shall be the original colour values. 
                *To derive c from c′, the conforming reader may sometimes need to invert the formula shown previously. 
                *The resulting c value shall lie within the range of colour component values for the image colour space.
                *
                *The preblending computation shall be done in the colour space specified by the parent image’s ColorSpaceentry. 
                *This is independent of the group colour space into which the image may be painted.
                *If a colour conversion is required, inversion of the preblending shall precede the colour conversion.
                *If the image colour space is an Indexed space(see “Indexed Colour Spaces”), the colour values in the colour table(not the index values themselves) shall be preblended.
                */
            
            /*11.6.6 Transparency Group XObjects
            *
                *A transparency group is represented in PDF as a special type of group XObject (see “Group XObjects”) called a transparency group XObject. 
                *A group XObject is in turn a type of form XObject, distinguished by the presence of a Group entry in its form dictionary (see “Form Dictionaries”). 
                *The value of this entry is a subsidiary group attributes dictionary defining the properties of the group. 
                *The format and meaning of the dictionary’s contents shall be determined by its group subtype, which is specified by the dictionary’s S entry. 
                *The entries for a transparency group (subtype Transparency) are shown in Table 147.
                *
                *A page object(see “Page Objects”) may also have a Group entry, whose value is a group attributes dictionary specifying the attributes of the page group(see “Page Group”).
                *Some of the dictionary entries are interpreted slightly differently for a page group than for a transparency group XObject; see their descriptions in the table for details.
                *
                *Table 147 - Additional entries specific to a transparency group attributes dicitonary
                *
                *          [Key]               [Type]                  [Value]
                *
                *          S                   name                    (Required) The group subtype, which identifies the type of group whose attributes this dictionary describes; shall be Transparency for a transparency group.
                *
                *          CS                  name or array           (Sometimes required) The group colour space, which is used for the following purposes:
                *                                                      
                *                                                      •   As the colour space into which colours shall be converted when painted into the group
                *
                *                                                      •   As the blending colour space in which objects shall be composited within the group(see “Blending Colour Space”)
                *                                                      
                *                                                      •   As the colour space of the group as a whole when it in turn is painted as an object onto its backdrop
                *
                *                                                      The group colour space shall be any device or CIE-based colour space that treats its components as independent additive or subtractive values in the range 0.0 to 1.0, subject to the restrictions described in 11.3.4, "Blending Colour Space." 
                *                                                      These restrictions exclude Lab and lightness-chromaticity ICCBased colour spaces, as well as the special colour spaces Pattern, Indexed, Separation, and DeviceN. 
                *                                                      Device colour spaces shall be subject to remapping according to the DefaultGray, DefaultRGB, and DefaultCMYK entries in the ColorSpace subdictionary of the current resource dictionary (see “Default Colour Spaces”).
                *
                *                                                      Ordinarily, the CS entry may be present only for isolated transparency groups(those for which I is true), and even then it is optional.
                *                                                      However, this entry shall be present in the group attributes dictionary for any transparency group XObject that has no parent group or page from which to inherit—in particular, one that is the value of the G entry in a soft - mask dictionary of subtype Luminosity(see “Soft - Mask Dictionaries”).
                *
                *                                                      Additionally, the CS entry may be present in the group attributes dictionary associated with a page object, even if I is false or absent. 
                *                                                      In the normal case in which the page is imposed directly on the output medium, the page group is effectively isolated regardless of the I value, and the specified CS value shall therefore be honoured.
                *                                                      But if the page is in turn used as an element of some other page and if the group is non - isolated, CS shall be ignored and the colour space shall be inherited from the actual backdrop with which the page is composited (see “Page Group”).
                *
                *                                                      Default value: the colour space of the parent group or page into which this transparency group is painted. 
                *                                                      (The parent’s colour space in turn may be either explicitly specified or inherited.)
                *
                *                                                      For a transparency group XObject used as an annotation appearance(see “Appearance Streams”), the default colour space shall be inherited from the page on which the annotation appears.
                *
                *          I                   boolean                 (Optional) A flag specifying whether the transparency group is isolated (see “Isolated Groups”). If this flag is true, objects within the group shall be composited against a fully transparent initial backdrop; if false, they shall be composited against the group’s backdrop. 
                *                                                      Default value: false.
                *
                *                                                      In the group attributes dictionary for a page, the interpretation of this entry shall be slightly altered. 
                *                                                      In the normal case in which the page is imposed directly on the output medium, the page group is effectively isolated and the specified I value shall be ignored. 
                *                                                      But if the page is in turn used as an element of some other page, it shall be treated as if it were a transparency group XObject; the I value shall be interpreted in the normal way to determine whether the page group is isolated.
                *
                *          K                   boolean                 (Optional) A flag specifying whether the transparency group is a knockout group (see “Knockout Groups”). 
                *                                                      If this flag is false, later objects within the group shall be composited with earlier ones with which they overlap; if true, they shall be composited with the group’s initial backdrop and shall overwrite (“knock out”) any earlier overlapping objects. 
                *                                                      Default value: false.
                *
                *
                *The transparency group XObject’s content stream shall define the graphics objects belonging to the group. 
                *When applied to a transparency group XObject, the Do operator shall execute its content stream and shall composite the resulting group colour, shape, and opacity into the group’s parent group or page as if they had come from an elementary graphics object. 
                *Do shall perform the following actions in addition to the normal ones for a form XObject (as described in “Form XObjects”):
                *
                *  •   If the transparency group is non - isolated(the value of the I entry in its group attributes dictionary is false), its initial backdrop, within the bounding box specified by the XObject’s BBox entry, shall be defined to be the accumulated colour and alpha of the parent group or page—that is, the result of everything that has been painted in the parent up to that point. 
                *      However, if the parent is a knockout group, the initial backdrop shall be the same as that of the parent.If the group is isolated (I is true), its initial backdrop shall be defined to be transparent.
                *
                *  •   Before execution of the transparency group XObject’s content stream, the current blend mode in the graphics state shall be initialized to Normal, the current stroking and nonstroking alpha constants to 1.0, and the current soft mask to None.
                *
                *NOTE 1        The purpose of initializing these graphics state parameters at the beginning of execution is to ensure that they are not applied twice: once when member objects are painted into the group and again when the group is painted into the parent group or page.
                *
                *  •   Objects painted by operators in the transparency group XObject’s content stream shall be composited into the group according to the rules described in 11.3.3, "Basic Compositing Formula." 
                *      The knockout flag (K) in the group attributes dictionary and the transparency-related parameters of the graphics state shall be honoured during this computation.
                *
                *  •   If a group colour space(CS) is specified in the group attributes dictionary, all painting operators shall convert source colours to that colour space before compositing objects into the group, and the resulting colour at each point shall be interpreted in that colour space.
                *      If no group colour space is specified, the prevailing colour space shall be dynamically inherited from the parent group or page. 
                *      (If not otherwise specified, the page group’s colour space shall be inherited from the native colour space of the output device.)
                *
                *  •   After execution of the transparency group XObject’s content stream, the graphics state shall revert to its former state before the invocation of the Do operator (as it does for any form XObject).
                *      The group’s shape—the union of all objects painted into the group, clipped by the group XObject’s bounding box— shall then be painted into the parent group or page, using the group’s accumulated colour and opacity at each point.
                *
                *If the Do operator is invoked more than once for a given transparency group XObject, each invocation shall be treated as a separate transparency group. 
                *That is, the result shall be as if the group were independently composited with the backdrop on each invocation.
                *
                *NOTE 2        Applications that perform caching of rendered form XObjects shall take this requirement into account.
                *
                *The actions described previously shall occur only for a transparency group XObject—a form XObject having a Group entry that designates a group attributes subdictionary whose group subtype(S) is Transparency.
                *An ordinary form XObject—one having no Group entry—shall not be subject to any grouping behaviour for transparency purposes. 
                *That is, the graphics objects it contains shall be composited individually, just as if they were painted directly into the parent group or page.
            */

            /*11.6.7 Patterns and Transparency
            *
                *In the transparent imaging model, the graphics objects making up the pattern cell of a tiling pattern (see “Tiling Patterns”) may include transparent objects and transparency groups. 
                *Transparent compositing may occur both within the pattern cell and between it and the backdrop wherever the pattern is painted. 
                *Similarly, a shading pattern (“Shading Patterns”) composites with its backdrop as if the shading dictionary were applied with the shoperator.
                *
                *In both cases, the pattern definition shall be treated as if it were implicitly enclosed in a non-isolated transparency group: a non-knockout group for tiling patterns, a knockout group for shading patterns. 
                *The definition shall not inherit the current values of the graphics state parameters at the time it is evaluated; those parameters shall take effect only when the resulting pattern is later used to paint an object.
                *Instead, the graphics state parameters shall be initialized as follows:
                *
                *  •   As always for transparency groups, those parameters related to transparency(blend mode, soft mask, and alpha constant) shall be initialized to their standard default values.
                *
                *  •   All other parameters shall be initialized to their values at the beginning of the content stream(such as a page or a form XObject) in which the pattern shall be defined as a resource. (This is the normal behaviour for all patterns, in both the opaque and transparent imaging models.)
                *
                *  •   In the case of a shading pattern, the parameter values may be augmented by the contents of the ExtGState entry in the pattern dictionary(see “Shading Patterns”).
                *      Only those parameters that affect the sh operator, such as the current transformation matrix and rendering intent, shall be used. 
                *      Parameters that affect path-painting operators shall not be used, since the execution of sh does not entail painting a path.
                *
                *  •   If the shading dictionary has a Background entry, the pattern’s implicit transparency group shall be filled with the specified background colour before the sh operator is invoked.
                *
                *When the pattern is later used to paint a graphics object, the colour, shape, and opacity values resulting from the evaluation of the pattern definition shall be used as the object’s source colour (Cs), object shape (fj), and object opacity (qj) in the transparency compositing formulas. 
                *This painting operation is subject to the values of the graphics state parameters in effect at the time, just as in painting an object with a constant colour.
                *
                *NOTE 1    Unlike the opaque imaging model, in which the pattern cell of a tiling pattern may be evaluated once and then replicated indefinitely to fill the painted area, the effect in the general transparent case is as if the pattern definition were reexecuted independently for each tile, taking into account the colour of the backdrop at each point.
                *          However, in the common case in which the pattern consists entirely of objects painted with the Normalblend mode, this behaviour can be optimized by treating the pattern cell as if it were an isolated group. Since in this case the results depend only on the colour, shape, and opacity of the pattern cell and not on those of the backdrop, the pattern cell can be evaluated once and then replicated, just as in opaque painting.
                *
                *NOTE 2    In a raster - based implementation of tiling, all tiles should be treated as a single transparency group.
                *          This avoids artifacts due to multiple marking of pixels along the boundaries between adjacent tiles.
                *
                *The foregoing discussion applies to both coloured(PaintType 1) and uncoloured(PaintType 2) tiling patterns. 
                *In the latter case, the restriction that an uncoloured pattern’s definition shall not specify colours extends as well to any transparency group that the definition may include.
                *There are no corresponding restrictions, however, on specifying transparency - related parameters in the graphics state.
            */

        }

        //11.7 Colour Space and Rendenring Issues
        public class Colour_Space_and_Rendenring_Issues
        {
            /*11.7.1 General
            *
            *This sub-clause describes the interactions between transparency and other aspects of colour specification and rendering in the PDF imaging model.
            */

            /*11.7.2 Colour Spaces for Transparency Groups
            *
                *
                *As discussed in 11.6.6, "Transparency Group XObjects," a transparency group shall either have an explicitly declared colour space of its own or inherit that of its parent group.
                *In either case, the colours of source objects within the group shall be converted to the group’s colour space, if necessary, and all blending and compositing computations shall be done in that space (see “Blending Colour Space”). 
                *The resulting colours shall then be interpreted in that colour space when the group is subsequently composited with its backdrop.
                *
                *NOTE 1    Under this arrangement, it is envisioned that all or most of a given piece of artwork will be created in a single colour space—most likely, the working colour space of the application generating it.
                *          The use of multiple colour spaces typically will arise only when assembling independently produced artwork onto a page. 
                *          After all the artwork has been placed on the page, the conversion from the group’s colour space to the page’s device colour space will be done as the last step, without any further transparency compositing.
                *          The transparent imaging model does not require that this convention be followed, however; the reason for adopting it is to avoid the loss of colour information and the introduction of errors resulting from unnecessary colour space conversions.
                *
                *Only an isolated group may have an explicitly declared colour space of its own. Non-isolated groups shall inherit their colour space from the parent group (subject to special treatment for the page group, as described in “Page Group”).
                *
                *NOTE 2    This is because the use of an explicit colour space in a non-isolated group would require converting colours from the backdrop’s colour space to that of the group in order to perform the compositing computations. 
                *          Such conversion may not be possible (since some colour conversions can be performed only in one direction), and even if possible, it would entail an excessive number of colour conversions.
                *
                *NOTE 3    The choice of a group colour space has significant effects on the results that are produced:
                *
                *          As noted in 11.3.4, "Blending Colour Space," the results of compositing in a device colour space is device-dependent.
                *          For the compositing computations to work in a device-independent way, the group’s colour space should be CIE-based.
                *
                *          A consequence of choosing a CIE-based group colour space is that only CIE-based spaces can be used to specify the colours of objects within the group.
                *          This is because conversion from device to CIE-based colours is not possible in general; the defined conversions work only in the opposite direction.
                *          See further discussion subsequently.
                *
                *          The compositing computations and blend functions generally compute linear combinations of colour component values, on the assumption that the component values themselves are linear. 
                *          For this reason, it is usually best to choose a group colour space that has a linear gamma function. 
                *          If a nonlinear colour space is chosen, the results are still well-defined, but the appearance may not match the user’s expectations.
                *
                *NOTE 4    The CIE-based sRGB colour space (see “CIE-Based Colour Spaces”) is nonlinear and hence may be unsuitable for use as a group colour space.
                *
                *NOTE 5    Implementations of the transparent imaging model should use as much precision as possible in representing colours during compositing computations and in the accumulated group results. 
                *          To minimize the accumulation of roundoff errors and avoid additional errors arising from the use of linear group colour spaces, more precision is needed for intermediate results than is typically used to represent either the original source data or the final rasterized results.
                *
                *If a group’s colour space—whether specified explicitly or inherited from the parent group—is CIE-based, any use of device colour spaces for painting objects shall be subject to special treatment. 
                *Device colours cannot be painted directly into such a group, since there is no generally defined method for converting them to the CIE-based colour space. 
                *This problem arises in the following cases:
                *
                *DeviceGray, DeviceRGB, and DeviceCMYK colour spaces, unless remapped to default CIE-based colour spaces (see “Default Colour Spaces”)
                *
                *  •   Operators(such as rg) that specify a device colour space implicitly, unless that space is remapped
                *
                *  •   Special colour spaces whose base or underlying space is a device colour space, unless that space is remapped
                *
                *The default colour space remapping mechanism should always be employed when defining a transparency group whose colour space is CIE-based.
                *If a device colour is specified and is not remapped, it shall be converted to the CIE-based colour space in an implementation-dependent fashion, producing unpredictable results.
                *
                *NOTE 6    The foregoing restrictions do not apply if the group’s colour space is implicitly converted to DeviceCMYK, as discussed in “Implicit Conversion of CIE-Based Colour Spaces”.
                */
            
            /*11.7.3 Spot Colours and Transparency
            *
                *The foregoing discussion of colour spaces has been concerned with process colours—those produced by combinations of an output device’s process colorants. 
                *Process colours may be specified directly in the device’s native colour space (such as DeviceCMYK), or they may be produced by conversion from some other colour space, such as a CIE-based (CalRGB or ICCBased) space. 
                *Whatever means is used to specify them, process colours shall be subject to conversion to and from the group’s colour space.
                *
                *A spot colour is an additional colour component, independent of those used to produce process colours.
                *It may represent either an additional separation to be produced or an additional colorant to be applied to the composite page(see “Separation Colour Spaces” and “DeviceN Colour Spaces”).
                *The colour component value, or tint, for a spot colour specifies the concentration of the corresponding spot colorant.
                *Tints are conventionally represented as subtractive, rather than additive, values.
                *
                *Spot colours are inherently device - dependent and are not always available.
                *In the opaque imaging model, each use of a spot colour component in a Separation or DeviceN colour space is accompanied by an alternate colour space and a tint transformation function for mapping tint values into that space.
                *This enables the colour to be approximated with process colorants when the corresponding spot colorant is not available on the device.
                *
                *Spot colours can be accommodated straightforwardly in the transparent imaging model(except for issues relating to overprinting, discussed in “Overprinting and Transparency”).
                *When an object is painted transparently with a spot colour component that is available in the output device, that colour shall be composited with the corresponding spot colour component of the backdrop, independently of the compositing that is performed for process colours. 
                *A spot colour retains its own identity; it shall not be subject to conversion to or from the colour space of the enclosing transparency group or page. 
                *If the object is an element of a transparency group, one of two things shall happen:
                *
                *  •   The group shall maintain a separate colour value for each spot colour component, independently of the group’s colour space. 
                *      In effect, the spot colour passes directly through the group hierarchy to the device, with no colour conversions performed. 
                *      However, it shall still be subject to blending and compositing with other objects that use the same spot colour.
                *
                *  •   The spot colour shall be converted to its alternate colour space.
                *      The resulting colour shall then be subject to the usual compositing rules for process colours. 
                *      In particular, spot colours shall not be available in a transparency group XObject that is used to define a soft mask; the alternate colour space shall always be substituted in that case.
                *
                *Only a single shape value and opacity value shall be maintained at each point in the computed group results; they shall apply to both process and spot colour components. 
                *In effect, every object shall be considered to paint every existing colour component, both process and spot.
                *Where no value has been explicitly specified for a given component in a given object, an additive value of 1.0 (or a subtractive tint value of 0.0) shall be assumed.
                *For instance, when painting an object with a colour specified in a DeviceCMYK or ICCBased colour space, the process colour components shall be painted as specified and the spot colour components shall be painted with an additive value of 1.0. 
                *Likewise, when painting an object with a colour specified in a Separationcolour space, the named spot colour shall be painted as specified and all other components (both process colours and other spot colours) shall be painted with an additive value of 1.0. 
                *The consequences of this are discussed in 11.7.4, "Overprinting and Transparency."
                *
                *Under the opaque imaging model, a Separation or DeviceN colour space may specify the individual process colour components of the output device, as if they were spot colours.
                *However, within a transparency group, this should be done only if the group inherits the native colour space of the output device (or is implicitly converted to DeviceCMYK, as discussed in 8.6.5.7, "Implicit Conversion of CIE-Based Colour Spaces"). 
                *If any other colour space has been specified for the group, the Separation or DeviceN colour space shall be converted to its alternate colour space.
                *
                *NOTE      In general, within a transparency group containing an explicitly - specified colour space, the group's process colour components are different from the device's process colour components.
                *          Conversion to the device's process colour components occurs only after all colour compositing computations for the group have been completed. 
                *          Consequently, the device's process colour components are not accessible within the group.
                *
                *          For instance, outside of any transparency group, a device whose native colour space is DeviceCMYK has a Cyan component that may be specified in a Separation or DeviceN colour space.
                *          On the other hand, within a transparency group whose colour space is ICCBased, the group has no Cyan component available to be painted.
                */

            /*11.7.4 Overprinting and Transparency
            */
                
                /*11.7.4.1 General
                *
                *In the opaque imaging model, overprinting is controlled by two parameters of the graphics state: the overprint parameter and the overprint mode(see “Overprint Control”).
                *Painting an object causes some specific set of device colorants to be marked, as determined by the current colour space and current colour in the graphics state.
                *The remaining colorants shall be either erased or left unchanged, depending on whether the overprint parameter is false or true.
                *When the current colour space is DeviceCMYK, the overprint mode parameter additionally enables this selective marking of colorants to be applied to individual colour components according to whether the component value is zero or nonzero.
                *
                *NOTE 1    Because this model of overprinting deals directly with the painting of device colorants, independently of the colour space in which source colours have been specified, it is highly device - dependent and primarily addresses production needs rather than design intent.
                *          Overprinting is usually reserved for opaque colorants or for very dark colours, such as black.It is also invoked during late - stage production operations such as trapping (see “Trapping Support”), when the actual set of device colorants has already been determined.
                *
                *NOTE 2    Consequently, it is best to think of transparency as taking place in appearance space, but overprinting of device colorants in device space. 
                *          This means that colorant overprint decisions should be made at output time, based on the actual resultant colorants of any transparency compositing operation. 
                *          On the other hand, effects similar to overprinting can be achieved in a device-independent manner by taking advantage of blend modes, as described in the next sub - clause.
                */

                /*11.7.4.2 Blend Modes and Overprinting
               *
                    *
                    *As stated in 11.7.3, "Spot Colours and Transparency," each graphics object that is painted shall affect all existing colour components: all process colorants in the transparency group’s colour space as well as any available spot colorants.
                    *For colour components whose value has not been specified, a source colour value of 1.0 shall be assumed; when objects are fully opaque and the Normal blend mode is used, this shall have the effect of erasing those components. 
                    *This treatment is consistent with the behaviour of the opaque imaging model with the overprint parameter set to false.
                    *
                    *The transparent imaging model defines some blend modes, such as Darken, that can be used to achieve effects similar to overprinting. 
                    *The blend function for Darken is
                    *
                    *      B(Cb,Cs) = min(Cb,Cs)
                    *
                    *In this blend mode, the result of compositing shall always be the same as the backdrop colour when the source colour is 1.0, as it is for all unspecified colour components. 
                    *When the backdrop is fully opaque, this shall leave the result colour unchanged from that of the backdrop. 
                    *This is consistent with the behaviour of the opaque imaging model with the overprint parameter set to true.
                    *
                    *If the object or backdrop is not fully opaque, the actions described previously are altered accordingly.
                    *That is, the erasing effect shall be reduced, and overprinting an object with a colour value of 1.0 may affect the result colour.
                    *While these results may or may not be useful, they lie outside the realm of the overprinting and erasing behaviour defined in the opaque imaging model.
                    *
                    *When process colours are overprinted or erased(because a spot colour is being painted), the blending computations described previously shall be done independently for each component in the group’s colour space.
                    *If that space is different from the native colour space of the output device, its components are not the device’s actual process colorants; the blending computations shall affect the process colorants only after the group’s results have been converted to the device colour space. 
                    *Thus the effect is different from that of overprinting or erasing the device’s process colorants directly. 
                    *On the other hand, this is a fully general operation that works uniformly, regardless of the type of object or of the computations that produced the source colour.
                    *
                    *NOTE 1    The discussion so far has focused on those colour components whose values are not specified and that are to be either erased or left unchanged.
                    *          However, the Normal or Darken blend modes used for these purposes may not be suitable for use on those components whose colour values are specified.
                    *          In particular, using the Darken blend mode for such components would preclude overprinting a dark colour with a lighter one.
                    *          Moreover, some other blend mode may be specifically desired for those components.
                    *
                    *The PDF graphics state specifies only one current blend mode parameter, which shall always apply to process colorants and sometimes to spot colorants as well. 
                    *Specifically, only separable, white-preserving blend modes shall be used for spot colours. 
                    *If the specified blend mode is not separable and white-preserving, it shall apply only to process colour components, and the Normal blend mode shall be substituted for spot colours.
                    *
                    *A blend mode is white - preserving if its blend function B has the property that B(1.0, 1.0) = 1.0.
                    *
                    *NOTE 2        Of the standard separable blend modes listed in Table 136 in 11.3.5, "Blend Mode," all except Difference and Exclusion are white - preserving.
                    *              This ensures that when objects accumulate in an isolated transparency group, the accumulated values for unspecified components remain 1.0 as long as only white - preserving blend modes are used.
                    *              The group’s results can then be overprinted using Darken(or other useful modes) while avoiding unwanted interactions with components whose values were never specified within the group.
                 */

                /*11.7.4.3 Compatibility with Opaque Overprinting
                *
                    *
                    *Because the use of blend modes to achieve effects similar to overprinting does not make direct use of the overprint control parameters in the graphics state, such methods are usable only by transparency - aware applications.
                    *For compatibility with the methods of overprint control used in the opaque imaging model, a special blend mode, CompatibleOverprint, is provided that consults the overprint - related graphics state parameters to compute its result.
                    *This mode shall apply only when painting elementary graphics objects(fills, strokes, text, images, and shadings). 
                    *It shall not be invoked explicitly and shall not be identified by any PDF name object; rather, it shall be implicitly invoked whenever an elementary graphics object is painted while overprinting is enabled (that is, when the overprint parameter in the graphics state is true).
                    *
                    *NOTE 1    Earlier designs of the transparent imaging model included an additional blend mode named Compatible, which explicitly invoked the CompatibleOverprint blend mode described here. 
                    *          Because CompatibleOverprint is now invoked implicitly whenever appropriate, it is never necessary to specify the Compatible blend mode for use in compositing.
                    *
                    *The Compatible blend mode shall be treated as equivalent to Normal.
                    *
                    *The value of the blend function B(cb, cs) in the CompatibleOverprint mode shall be either cb or cs, depending on the setting of the overprint mode parameter, the current and group colour spaces, and the source colour value cs:
                    *
                    *  •   If the overprint mode is 1 (nonzero overprint mode) and the current colour space and group colour space are both DeviceCMYK, then process colour components with nonzero values shall replace the corresponding component values of the backdrop; components with zero values leave the existing backdrop value unchanged. 
                    *      That is, the value of the blend function B(cb, cs) shall be the source component cs for any process (DeviceCMYK) colour component whose (subtractive) colour value is nonzero; otherwise it shall be the backdrop component cb. 
                    *      For spot colour components, the value shall always be cb.
                    *
                    *  •   In all other cases, the value of B(cb, cs) shall be cs for all colour components specified in the current colour space, otherwise cb.
                    *
                    *EXAMPLE 1     If the current colour space is DeviceCMYK or CalRGB, the value of the blend function is cs for process colour components and cb for spot components. 
                    *              On the other hand, if the current colour space is a Separation space representing a spot colour component, the value is cs for that spot component and cbfor all process components and all other spot components.
                    *
                    *NOTE 2        In the previous descriptions, the term current colour space refers to the colour space used for a painting operation.
                    *              This may be specified by the current colour space parameter in the graphics state(see “Colour Values”), implicitly by colour operators such as rg (“Colour Operators”), or by the ColorSpace entry of an image XObject(“Image Dictionaries”).
                    *              In the case of an Indexed space, it refers to the base colour space(see “Indexed Colour Spaces”); likewise for Separation and DeviceN spaces that revert to their alternate colour space, as described under “Separation Colour Spaces” and “DeviceN Colour Spaces”.
                    *
                    *If the current blend mode when CompatibleOverprint is invoked is any mode other than Normal, the object being painted shall be implicitly treated as if it were defined in a non-isolated, non - knockout transparency group and painted using the CompatibleOverprint blend mode. 
                    *The group’s results shall then be painted using the current blend mode in the graphics state.
                    *
                    *NOTE 3        It is not necessary to create such an implicit transparency group if the current blend mode is Normal; simply substituting the CompatibleOverprint blend mode while painting the object produces equivalent results. 
                    *              There are some additional cases in which the implicit transparency group can be optimized out.
                    *
                    *EXAMPLE 2     Figure L.20 in Annex L shows the effects of all four possible combinations of blending and overprinting, using the Screen blend mode in the DeviceCMYK colour space. 
                    *              The label “overprint enabled” means that the overprint parameter in the graphics state is true and the overprint mode is 1.
                    *              In the upper half of the figure, a light green oval is painted opaquely (opacity = 1.0) over a backdrop shading from pure yellow to pure magenta. 
                    *              In the lower half, the same object is painted with transparency(opacity = 0.5).
                    */
                    
                /*11.7.4.4 Special Path-Painting Considerations
                *
                    *The overprinting considerations discussed in 11.7.4.3, "Compatibility with Opaque Overprinting," also affect those path-painting operations that combine filling and stroking a path in a single operation.
                    *These include the B, B*, b, and b*operators(see “Path - Painting Operators”) and the painting of glyphs with text rendering mode 2 or 6(“Text Rendering Mode”).
                    *For transparency compositing purposes, the combined fill and stroke shall be treated as a single graphics object, as if they were enclosed in a transparency group.
                    *This implicit group is established and used as follows:
                    *
                    *  •   If overprinting is enabled (the overprint parameter in the graphics state is true) and the current stroking and nonstroking alpha constants are equal, a non-isolated, non-knockout transparency group shall be established.
                    *      Within the group, the fill and stroke shall be performed with an alpha value of 1.0 but with the CompatibleOverprint blend mode. 
                    *      The group results shall then be composited with the backdrop, using the originally specified alpha and blend mode.
                    *
                    *  •   In all other cases, a non-isolated knockout group shall be established. 
                    *      Within the group, the fill and stroke shall be performed with their respective prevailing alpha constants and the prevailing blend mode. 
                    *      The group results shall then be composited with the backdrop, using an alpha value of 1.0 and the Normalblend mode.
                    *
                    *NOTE 1    In the case of showing text with the combined filling and stroking text rendering modes, this behaviour is independent of the text knockout parameter in the graphics state (see “Text Knockout”).
                    *
                    *NOTE 2    The purpose of these rules is to avoid having a non - opaque stroke composite with the result of the fill in the region of overlap, which would produce a double border effect that is usually undesirable. 
                    *          The special case that applies when the overprint parameter is true is for backward compatibility with the overprinting behavior of the opaque imaging model.
                    *          If a desired effect cannot be achieved with a combined filling and stroking operator or text rendering mode, it can be achieved by specifying the fill and stroke with separate path objects and an explicit transparency group.
                    *
                    *NOTE 3    Overprinting of the stroke over the fill does not work in the second case described previously 
                    *          (although either the fill or the stroke can still overprint the backdrop). Furthermore, if the overprint graphics state parameter is true, the results are discontinuous at the transition between equal and unequal values of the stroking and nonstroking alpha constants.
                    *          For this reason, it is best not to use overprinting for combined filling and stroking operations if the stroking and nonstroking alpha constants are being varied independently.
                    */

                /*11.7.4.5 Summary of Overprinting Behaviour
                *
                    *Tables 148 and 149 summarize the overprinting and erasing behaviour in the opaque and transparent imaging models, respectively. 
                    *Table 148 shows the overprinting rules used in the opaque model, as described in “Overprint Control”. 
                    *Table 149 shows the equivalent rules as implemented by the CompatibleOverprint blend mode in the transparent model. 
                    *The names OP and OPM in the tables refer to the overprint and overprint mode parameters of the graphics state.
                    *
                    *Table 148 - Overprinting behavior in the opaque imaging model
                    *
                    *          [Source Colour Space]               [Affected Colour Component]                             [Effect on Colour Component]
                    *
                    *                                                                                           [OP false]        [OP true, OPM 0]         [OP true, OPM 1]
                    *
                    *          (DeviceCMYK,                        C, M, Y, or K                                Paint source       Paint source            Paint source
                    *          specified directly,                                                                                                         if ~= 0.0 
                    *          not in a sampled                                                                                                            Do not paint if = 0.0
                    *          image
                    *
                    *                                              Process colorant                            Paint source        Paint source            Paint source
                    *                                              other than CMYK
                    *
                    *                                              Spot colorant                               Paint 0.0           Do not paint            Do not paint
                    *
                    *          ______________________________________________________________________________________________________________________________________________________
                    *
                    *          Any process colour                  Process colorant                            Paint source        Paint source            Paint source
                    *          space (including
                    *          other cases of                      Spot colorant                               Paint 0.0           Do not paint            Do not paint
                    *          DeviceCMYK)
                    *
                    *          _____________________________________________________________________________________________________________________________________________________
                    *
                    *          Separation or                       Process colorant                            Paint 0.0           Do not paint            Do not paint
                    *          DeviceN     
                    *
                    *                                              Spot colorant                               Paint source        Paint source            Paint source            
                    *                                              named in source
                    *                                              space
                    *
                    *
                    *                                              Spot colorant not                           Paint 0.0           Do not paint            Do not paint
                    *                                              named in source
                    *                                              space
                    *          _____________________________________________________________________________________________________________________________________________________
                    *
                    *          DeviceCMYK,                         C, M, Y, or K                               Cs                  Cs                      Cs if Cs ~= 0.0
                    *          specified directly,                                                                                                         Cb if Cs = 0.0
                    *          not in a sampled
                    *          image                               Process colour                              Cs                  Cs                      Cs
                    *                                              component other
                    *                                              than CMYK     
                    *
                    *                                              Spot colorant                               Cs (= 0.0)          Cb                      Cb
                    *
                    *          ____________________________________________________________________________________________________________________________________________________
                    *
                    *          Separation or                       Process colour                              Cs (= 0.0)          Cb                      Cb
                    *          DeviceN                             component
                    *
                    *                                              Spot colorant                               Cs                  Cs                      Cs
                    *                                              named in source
                    *                                              space
                    *
                    *                                              Spot colorant not                           Cs (= 0.0)          Cb                      Cb
                    *                                              named in source
                    *                                              space
                    *
                    *          ___________________________________________________________________________________________________________________________________________________
                    *
                    *          A group (not an                     All colour                                  Cs                  Cs                      Cs
                    *          elementary object)                  components
                    *
                    *          ___________________________________________________________________________________________________________________________________________________
                    *
                    *Colour component values are represented in these tables as subtractive tint values because overprinting is typically applied to subtractive colorants such as inks rather than to additive ones such as phosphors on a display screen. 
                    *The CompatibleOverprint blend mode is therefore described as if it took subtractive arguments and returned subtractive results. 
                    *In reality, however, CompatibleOverprint (like all blend modes) shall treat colour components as additive values; subtractive components shall be complemented before and after application of the blend function.
                    *
                    *NOTE 1        This note describes an important difference between Table 148 and Table 149.
                    *              In Table 148, the process colour components being discussed are the actual device colorants—the colour components of the output device’s native colour space (DeviceGray, DeviceRGB, or DeviceCMYK). 
                    *              In Table 149, the process colour components are those of the group’s colour space, which is not necessarily the same as that of the output device (and can even be something like CalRGB or ICCBased). 
                    *              For this reason, the process colour components of the group colour space cannot be treated as if they were spot colours in a Separation or DeviceN colour space(see “Spot Colours and Transparency”). 
                    *              This difference between opaque and transparent overprinting and erasing rules arises only within a transparency group (including the page group, if its colour space is different from the native colour space of the output device). 
                    *              There is no difference in the treatment of spot colour components.
                    *
                    *NOTE 2        Table 149 has one additional row at the bottom.
                    *              It applies when painting an object that is a transparency group rather than an elementary object (fill, stroke, text, image, or shading).
                    *              As stated in 11.7.3, "Spot Colours and Transparency," a group is considered to paint all colour components, both process and spot.
                    *              Colour components that were not explicitly painted by any object in the group have an additive colour value of 1.0 (subtractive tint 0.0).
                    *              Since no information is retained about which components were actually painted within the group, compatible overprinting is not possible in this case; the CompatibleOverprint blend mode reverts to Normal, with no consideration of the overprint and overprint mode parameters. 
                    *              A transparency-aware conforming writer can choose a more suitable blend mode, such as Darken, to produce an effect similar to overprinting.
                    */

            /*11.7.5 Rendenring Parameters and Transparency
            */

                /*11.7.5.1 General
                *
                *The opaque imaging model has several graphics state parameters dealing with the rendering of colour: the current halftone(see “Halftone Dictionaries”), transfer functions(“Transfer Functions”), rendering intent(“Rendering Intents”), and black-generation and undercolor-removal functions(“Conversion from DeviceRGB to DeviceCMYK”).
                *All of these rendering parameters may be specified on a per-object basis; they control how a particular object is rendered.When all objects are opaque, it is easy to define what this means.
                *But when they are transparent, more than one object may contribute to the colour at a given point; it is unclear which rendering parameters to apply in an area where transparent objects overlap. At the same time, the transparent imaging model should be consistent with the opaque model when only opaque objects are painted.
                *
                *There are two categories of rendering parameters that are treated somewhat differently in the presence of transparency. 
                *In the first category are halftone and transfer functions, which are applied only when the final colour at a given point on the page is known.
                *In the second category are rendering intent, black generation, and undercolor removal, which are applied whenever colours are converted from one colour space to another.
                */

                /*11.7.5.2 Halftone and Transfer Function
                *
                    *When objects are transparent, rendering of an object does not occur when the object is specified but at some later time. 
                    *Hence, the implementation shall keep track of the halftone and transfer function parameters at each point on the page from the time they are specified until the time rendering actually occurs. 
                    *This means that these rendering parameters shall be associated with regions of the page rather than with individual objects.
                    *
                    *The halftone and transfer function to be used at any given point on the page shall be those in effect at the time of painting the last(topmost) elementary graphics object enclosing that point, but only if the object is fully opaque. 
                    *Only elementary objects shall be relevant; the rendering parameters associated with a group object are ignored.
                    *The topmost object at any point shall be defined to be the topmost elementary object in the entire page stack that has a nonzero object shape value(fj) at that point(that is, for which the point is inside the object).
                    *An object shall be considered to be fully opaque if all of the following conditions hold at the time the object is painted:
                    *
                    *  •   The current alpha constant in the graphics state(stroking or nonstroking, depending on the painting operation) is 1.0.
                    *
                    *  •   The current blend mode in the graphics state is Normal (or Compatible, which is treated as equivalent to Normal).
                    *
                    *  •   The current soft mask in the graphics state is None.If the object is an image XObject, there is not an SMask entry in its image dictionary.
                    *
                    *  •   The foregoing three conditions were also true at the time the Do operator was invoked for the group containing the object, as well as for any direct ancestor groups.
                    *
                    *  •   If the current colour is a tiling pattern, all objects in the definition of its pattern cell also satisfy the foregoing conditions.
                    *
                    *Together, these conditions ensure that only the object itself shall contribute to the colour at the given point, completely obscuring the backdrop. 
                    *For portions of the page whose topmost object is not fully opaque or that are never painted at all, the default halftone and transfer function for the page shall be used.
                    *
                    *If a graphics object is painted with overprinting enabled—that is, if the applicable(stroking or nonstroking) overprint parameter in the graphics state is true—the halftone and transfer function to use at a given point shall be determined independently for each colour component.
                    *Overprinting implicitly invokes the CompatibleOverprint blend mode(see “Compatibility with Opaque Overprinting”).
                    *An object shall be considered opaque for a given component only if CompatibleOverprint yields the source colour(not the backdrop colour) for that component.
               */

                /*11.7.5.3 Rendenring Intent and Colour Conversions
                *
                    *The rendering intent, black-generation, and undercolor-removal parameters control certain colour conversions. 
                    *In the presence of transparency, they may need to be applied earlier than the actual rendering of colour onto the page.
                    *
                    *The rendering intent influences the conversion from a CIE-based colour space to a target colour space, taking into account the target space’s colour gamut(the range of colours it can reproduce).
                    *Whereas in the opaque imaging model the target space shall always be the native colour space of the output device, in the transparent model it may instead be the group colour space of a transparency group into which an object is being painted.
                    *
                    *The rendering intent is needed at the moment such a conversion is performed—that is, when painting an elementary or group object specified in a CIE-based colour space into a parent group having a different colour space.
                    *
                    *NOTE 1    This differs from the current halftone and transfer function, whose values are used only when all colour compositing has been completed and rasterization is being performed.
                    *
                    *In all cases, the rendering intent to use for converting an object’s colour(whether that of an elementary object or of a transparency group) shall be determined by the rendering intent parameter associated with the object.
                    *In particular:
                    *
                    *  •   When painting an elementary object with a CIE - based colour into a transparency group having a different colour space, the rendering intent used shall be the current rendering intent in effect in the graphics state at the time of the painting operation.
                    *
                    *  •   When painting a transparency group whose colour space is CIE - based into a parent group having a different colour space, the rendering intent used shall be the current rendering intent in effect at the time the Do operator is applied to the group.
                    *
                    *  •   When the colour space of the page group is CIE-based, the rendering intent used to convert colours to the native colour space of the output device shall be the default rendering intent for the page.
                    *
                    *NOTE 2        Since there may be one or more nested transparency groups having different CIE - based colour spaces, the colour of an elementary source object may be converted to the device colour space in multiple stages, controlled by the rendering intent in effect at each stage. 
                    *              The proper choice of rendering intent at each stage depends on the relative gamuts of the source and target colour spaces.
                    *              It is specified explicitly by the document producer, not prescribed by the PDF specification, since no single policy for managing rendering intents is appropriate for all situations.
                    *
                    *A similar approach works for the black-generation and undercolor - removal functions, which shall be applied only during conversion from DeviceRGB to DeviceCMYK colour spaces:
                    *
                    *  •   When painting an elementary object with a DeviceRGB colour directly into a transparency group whose colour space is DeviceCMYK, the functions used shall be the current black - generation and undercolor - removal functions in effect in the graphics state at the time of the painting operation.
                    *
                    *  •   When painting a transparency group whose colour space is DeviceRGB into a parent group whose colour space is DeviceCMYK, the functions used shall be the ones in effect at the time the Do operator is applied to the group.
                    *
                    *  •   When the colour space of the page group is DeviceRGB and the native colour space of the output device is DeviceCMYK, the functions used to convert colours to the device’s colour space shall be the default functions for the page.
                    */


        }



    }
}
