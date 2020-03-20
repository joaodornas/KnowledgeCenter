using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfPlugin
{
    //10 Rendering
    public class Rendering
    {

        /*10.1 General
        *
        *Nearly all of the rendering facilities that are under the control of a PDF file pertain to the reproduction of colour. 
        *Colours shall be rendered by a conforming reader using the following multiple-step process outlined.
        *
        *NOTE 1        The PDF imaging model separates graphics(the specification of shapes and colours) from rendering(controlling a raster output device). 
        *              Figures 20 and 21 in 8.6.3, "Colour Space Families" illustrate this division. 
        *              8, "Graphics" describes the facilities for specifying the appearance of pages in a device-independent way. 
        *              This clause describes the facilities for controlling how shapes and colours are rendered on the raster output device. 
        *              All of the facilities discussed here depend on the specific characteristics of the output device.
        *              PDF files that are intended to be device-independent should limit themselves to the general graphics facilities described in 8, "Graphics".
        *
        *Depending on the current colour space and on the characteristics of the device, it is not always necessary to perform every step.
        *
        *  a)  If a colour has been specified in a CIE-based colour space (see 8.6.5, "CIE-Based Colour Spaces"), it shall first be transformed to the native colour space of the raster output device(also called its process colour model).
        *
        *  b)  If a colour has been specified in a device colour space that is inappropriate for the output device(for example, RGB colour with a CMYK or grayscale device), a colour conversion function shall be invoked.
        *
        *  c)  The device colour values shall now be mapped through transfer functions, one for each colour component.
        *
        *NOTE 2        The transfer functions compensate for peculiarities of the output device, such as nonlinear gray-level response.This step is sometimes called gamma correction.
        *
        *  d)  If the device cannot reproduce continuous tones, but only certain discrete colours such as black and white pixels, a halftone function shall be invoked, which approximates the desired colours by means of patterns of pixels.
        *
        *  e)  Finally, scan conversion shall be performed to mark the appropriate pixels of the raster output device with the requested colours.
        *
        *Once these operations have been performed for all graphics objects on the page, the resulting raster data shall be used to mark the physical output medium, such as pixels on a display or ink on a printed page.A PDF file may specify very little about the properties of the physical medium on which the output will be produced; 
        *that information may be obtained from the following sources by a conforming reader:
        *
        *  •   The media box and a few other entries in the page dictionary(see 14.11.2, "Page Boundaries").
        *
        *  •   An interactive dialogue conducted when the user requests viewing or printing.
        *
        *  •   A job ticket, either embedded in the PDF file or provided separately, that may specify detailed instructions for imposing PDF pages onto media and for controlling special features of the output device.
        *      Various standards exist for the format of job tickets.Two of them, JDF(Job Definition Format) and PJTF(Portable Job Ticket Format), are described in the CIP4 document JDF Specification and in Adobe Technical Note #5620, Portable Job Ticket Format (see the Bibliography), respectively.
        *
        *Table 58 in 8.4.5, "Graphics State Parameter Dictionaries" lists the various device-dependent graphics state parameters that may be used to control certain aspects of rendering.
        *To invoke these parameters, the gsoperator shall be used.
        */

        //10.2 CIE_Based_Colour_to_Device_Colour
        public class CIE_Based_Colour_to_Device_Colour
        {
            /*10.2 CIE_Based_Colour_to_Device_Colour
            *To render CIE-based colours on an output device, the conforming reader shall convert from the specified CIE-based colour space to the device’s native colour space (typically DeviceGray, DeviceRGB, or DeviceCMYK), taking into account the known properties of the device.
            *
            *NOTE 1    As discussed in 8.6.5, "CIE-Based Colour Spaces" CIE-based colour is based on a model of human colour perception.
            *          The goal of CIE-based colour rendering is to produce output in the device’s native colour space that accurately reproduces the requested CIE-based colour values as perceived by a human observer.
            *          CIE-based colour specification and rendering are a feature of PDF 1.1 (CalGray, CalRGB, and Lab) and PDF 1.3 (ICCBased).
            *
            *NOTE 2    The conversion from CIE-based colour to device colour is complex, and the theory on which it is based is beyond the scope of this specification.
            *          The algorithm has many parameters, including an optional, full three-dimensional colour lookup table. 
            *          The colour fidelity of the output depends on having these parameters properly set, usually by a method that includes some form of calibration. 
            *          The colours that a device can produce are characterized by a device profile, which is usually specified by an ICC profile associated with the device (and entirely separate from the profile that is specified in an ICCBased colour space).
            *
            *NOTE 3    PDF has no equivalent of the PostScript colour rendering dictionary.
            *          The means by which a device profile is associated with a conforming reader’s output device are implementation-dependent and not specified in a PDF file.
            *          Typically, this is done through a colour management system (CMS) that is provided by the operating system.Beginning with PDF 1.4, a PDF file can also specify one or more output intents providing possible profiles that may be used to process the file (see 14.11.5, "Output Intents").
            *
            *Conversion from a CIE-based colour value to a device colour value requires two main operations:
            *
            *  a)  The CIE-based colour value shall be adjusted according to a CIE-based gamut mapping function.
            *
            *NOTE 4    A gamut is a subset of all possible colours in some colour space.
            *          A page description has a source gamutconsisting of all the colours it uses.An output device has a device gamut consisting of all the colours it can reproduce.
            *          This step transforms colours from the source gamut to the device gamut in a way that attempts to preserve colour appearance, visual contrast, or some other explicitly specified rendering intent(see 8.6.5.8, "Rendering Intents").
            *
            *  b)  A corresponding device colour value shall be generated according to a CIE-based colour mapping function.For a given CIE-based colour value, this function shall compute a colour value in the device’s native colour space.
            *
            *The CIE-based gamut and colour mapping functions shall be applied only to colour values presented in a CIE-based colour space.
            *Colour values in device colour spaces directly control the device colour components though this may be altered by the DefaultGray, DefaultRGB, and DefaultCMYK colour space resources (see 8.6.5.6, "Default Colour Spaces").
            *
            *The source gamut shall be specified by the information contained in the definition of the CIE-based colour space when selected.
            *This specification shall be device-independent.
            *The corresponding properties of the output device shall be given in the device profile associated with the device.
            *The gamut mapping and colour mapping functions are part of the implementation of the conforming reader.
            */
        }

        //10.3 Conversions among Device Colour Spaces
        public class Conversions_Among_Device_Colour_Spaces
        {

            /*10.3.1 General
            *
            *Each raster output device has a native colour space, which typically is one of the standard device colour spaces(DeviceGray, DeviceRGB, or DeviceCMYK). 
            *In other words, most devices support reproduction of colours according to a grayscale(monochrome), RGB(red-green-blue), or CMYK(cyan-magenta-yellow-black) model.
            *If the device supports continuous-tone output, reproduction shall occur directly.Otherwise, it shall be accomplished by means of halftoning.
            *
            *A device’s native colour space is also called its process colour model. Process colours are ones that are produced by combinations of one or more standard process colorants. 
            *Colours specified in any device or CIE-based colour space shall be rendered as process colours. 
            *A device may also support additional spot colorants, which shall be painted only by means of Separation or DeviceN colour spaces. 
            *They shall not be involved in the rendering of device or CIE-based colour spaces, nor shall they be subject to the conversions described in the Note.
            *
            *NOTE      Some devices provide a native colour space that is not one of the three named previously but consists of a different combination of colorants.
            *          In that case, conversion from the standard device colour spaces to the device’s native colour space may be performed by the conforming reader in a manner of its own choosing.
            *
            *Knowing the native colour space and other output capabilities of the device, the conforming reader shall automatically convert the colour values specified in a file to those appropriate for the device’s native colour space. 
            *If the file specifies colours directly in the device’s native colour space, no conversions shall be performed.
            *
            *EXAMPLE   If a file specifies colours in the DeviceRGB colour space but the device supports grayscale (such as a monochrome display) or CMYK(such as a colour printer), the conforming reader shall perform the necessary conversions.
            *
            *The algorithms used to convert among device colour spaces are very simple.
            *As perceived by a human viewer, these conversions produce only crude approximations of the original colours. 
            *More sophisticated control over colour conversion may be achieved by means of CIE-based colour specification and rendering.
            *Additionally, device colour spaces may be remapped into CIE-based colour spaces (see 8.6.5.6, "Default Colour Spaces").
            */

            /*10.3.2 Conversion between DeviceGray and DeviceRGB
           *
                *Black, white, and intermediate shades of gray can be considered special cases of RGB colour. 
                *
                *A grayscale value shall be described by a single number: 0.0 corresponds to black, 1.0 to white, and intermediate values to different gray levels.
                *
                *A gray level shall be equivalent to an RGB value with all three components the same. 
                *In other words, the RGBcolour value equivalent to a specific gray value shall be
                *
                *      red = gray
                *      green = gray
                *      blue = gray
                *
                *The gray value for a given RGB value shall be computed according to the NTSC video standard, which determines how a colour television signal is rendered on a black - and - white television set:
                *
                *      gray = 0.3 x red + 0.59 x green + 0.11 x blu
                *
                */

            /*10.3.3 Conversion between DeviceGray and DeviceCMYK
            *
                *Nominally, a gray level is the complement of the black component of CMYK. Therefore, the CMYK colour value equivalent to a specific gray level shall be
                *
                *          cyan = 0.0
                *          magenta = 0.0
                *          yellow = 0.0
                *          black = 1.0 - gray
                *
                *To obtain the equivalent gray level for a given CMYK value, the contributions of all components shall be taken into account:
                *
                *          gray = 1.0 - min(1.0,0.3 x cyan + 0.59 x magenta + 0.11 x yellow + black
                *
                *The interactions between the black component and the other three are elaborated in 10.3.4.
            */

            /*10.3.4 Conversion from DeviceRGB to DeviceCMYK
            *
                *Conversion of a colour value from RGB to CMYK is a two-step process. The first step shall be to convert the red-green-blue value to equivalent cyan, magenta, and yellow components. 
                *The second step shall be to generate a black component and alter the other components to produce a better approximation of the original colour.
                *
                *NOTE 1        The subtractive colour primaries cyan, magenta, and yellow are the complements of the additive primaries red, green, and blue.
                *
                *EXAMPLE       A cyan ink subtracts the red component of white light. In theory, the conversion is very simple:
                *
                *              cyan = 1.0 - red
                *              magenta = 1.0 - green
                *              yellow = 1.0 - blue
                *
                *              A colour that is 0.2 red, 0.7 green, and 0.4 blue can also be expressed as 1.0 − 0.2 = 0.8 cyan, 1.0 − 0.7 = 0.3 magenta, and 1.0 − 0.4 = 0.6 yellow.
                *
                *NOTE 2        Logically, only cyan, magenta, and yellow are needed to generate a printing colour. 
                *              An equal level of cyan, magenta, and yellow should create the equivalent level of black. 
                *              In practice, however, coloured printing inks do not mix perfectly; such combinations often form dark brown shades instead of true black. 
                *              To obtain a truer colour rendition on a printer, true black ink is often substituted for the mixed-black portion of a colour. 
                *              Most colour printers support a black component (the K component of CMYK). 
                *              Computing the quantity of this component requires some additional steps:
                *
                *              Black generation calculates the amount of black to be used when trying to reproduce a particular colour.
                *
                *              Undercolor removal reduces the amounts of the cyan, magenta, and yellow components to compensate for the amount of black that was added by black generation.
                *
                *The complete conversion from RGB to CMYK shall be as follows, where BG(k) and UCR(k) are invocations of the black-generation and undercolor-removal functions, respectively:
                *
                *              c = 1.0 - red
                *              m = 1.0 - green
                *              y = 1.0 - blue
                *              k = min(c,m,y)
                *
                *              cyan = min(1.0,max(0.0,c - UCR(k)))
                *              magenta = min(1.0,max(0.0,m - UCR(k)))
                *              yellow = min(1.0, max(0.0, y - UCR(k)))
                *              black = min(1.0, max(0.0,BG(k)))
                *
                *The black-generation and undercolor-removal functions shall be defined as PDF function dictionaries (see 7.10, "Functions") that are parameters in the graphics state. 
                *They shall be specified as the values of the BGand UCR (or BG2 and UCR2) entries in a graphics state parameter dictionary (see Table 58). 
                *Each function shall be called with a single numeric operand and shall return a single numeric result.
                *
                *The input of both the black-generation and undercolor-removal functions shall be k, the minimum of the intermediate c, m, and y values that have been computed by subtracting the original red, green, and bluecomponents from 1.0.
                *
                *NOTE 3        Nominally, k is the amount of black that can be removed from the cyan, magenta, and yellow components and substituted as a separate black component.
                *
                *The black-generation function shall compute the black component as a function of the nominal k value. 
                *It may simply return its k operand unchanged, or it may return a larger value for extra black, a smaller value for less black, or 0.0 for no black at all.
                *
                *The undercolor-removal function shall compute the amount to subtract from each of the intermediate c, m, and y values to produce the final cyan, magenta, and yellow components. 
                *It may simply return its k operand unchanged, or it may return 0.0(so that no colour is removed), some fraction of the black amount, or even a negative amount, thereby adding to the total amount of colorant.
                *
                *The final component values that result after applying black generation and undercolor removal should be in the range 0.0 to 1.0.
                *If a value falls outside this range, the nearest valid value shall be substituted automatically without error indication.
                *
                *NOTE 4        This substitution is indicated explicitly by the min and max operations in the preceding formulas.
                *
                *The correct choice of black - generation and undercolor-removal functions depends on the characteristics of the output device.
                *Each device shall be configured with default values that are appropriate for that device.
                *
                *NOTE 5        See 11.7.5, "Rendering Parameters and Transparency" and, in particular, 11.7.5.3, "Rendering Intent and Colour Conversions" for further discussion of the role of black - generation and undercolor - removal functions in the transparent imaging model.
                *
            */

            /*10.3.5 Conversion_From_DeviceCMYK_to_DeviceRGB
            *
                *Conversion of a colour value from CMYK to RGB is a simple operation that does not involve black generation or undercolour removal:
                *
                *          red = 1.0 - min(1.0,cyan + black)
                *          green = 1.0 - min(1.0,magenta + black)
                *          blue = 1.0 - min(1.0,yellow + black)
                *
                *The black component shall be added to each of the other components, which shall then be converted to their complementary colours by subtracting them each from 1.0.
                */
            
        }

        //10.4 Transfer Functions
        public class Transfer_Functions
        {
            /*10.4 Transfer Functions
            *In the sequence of steps for processing colours, the conforming reader shall apply the transfer function afterperforming any needed conversions between colour spaces, but before applying a halftone function, if necessary. 
            *Each colour component shall have its own separate transfer function; there shall not be interaction between components.
            *
            *NOTE 1        Starting with PDF 1.2, a transfer function may be used to adjust the values of colour components to compensate for nonlinear response in an output device and in the human eye.
            *              Each component of a device colour space—for example, the red component of the DeviceRGB space—is intended to represent the perceived lightness or intensity of that colour component in proportion to the component’s numeric value.
            *
            *NOTE 2        Many devices do not actually behave this way, however; the purpose of a transfer function is to compensate for the device’s actual behaviour.
            *              This operation is sometimes called gamma correction (not to be confused with the CIE-based gamut mapping function performed as part of CIE-based colour rendering).
            *
            *Transfer functions shall always operate in the native colour space of the output device, regardless of the colour space in which colours were originally specified. 
            *(For example, for a CMYK device, the transfer functions apply to the device’s cyan, magenta, yellow, and black colour components, even if the colours were originally specified in, for example, a DeviceRGB or CalRGB colour space.) 
            *The transfer function shall be called with a numeric operand in the range 0.0 to 1.0 and shall return a number in the same range.
            *The input shall be the value of a colour component in the device’s native colour space, either specified directly or produced by conversion from some other colour space.
            *The output shall be the transformed component value to be transmitted to the device(after halftoning, if necessary).
            *
            *Both the input and the output of a transfer function shall always be interpreted as if the corresponding colour component were additive (red, green, blue, or gray): the greater the numeric value, the lighter the colour. 
            *If the component is subtractive (cyan, magenta, yellow, black, or a spot colour), it shall be converted to additive form by subtracting it from 1.0 before it is passed to the transfer function. 
            *The output of the function shall always be in additive form and shall be passed on to the halftone function in that form.
            *
            *Starting with PDF 1.2, transfer functions shall be defined as PDF function objects(see 7.10, "Functions"). There are two ways to specify transfer functions:
            *
            *  •   The current transfer function parameter in the graphics state shall consist of either a single transfer function or an array of four separate transfer functions, one each for red, green, blue, and gray or their complements cyan, magenta, yellow, and black.
            *      If only a single function is specified, it shall apply to all components.
            *      An RGB device shall use the first three, a monochrome device shall use the gray transfer function only, and a CMYK device shall use all four. 
            *      The current transfer function may be specified as the value of the TR or TR2 entry in a graphics state parameter dictionary; see Table 58.
            *
            *  •   The current halftone parameter in the graphics state may specify transfer functions as optional entries in halftone dictionaries(see 10.5.5, "Halftone Dictionaries"). 
            *      This is the only way to set transfer functions for nonprimary colour components or for any component in devices whose native colour space uses components other than the ones listed previously.
            *      A transfer function specified in a halftone dictionary shall override the corresponding one specified by the current transfer function parameter in the graphics state.
            *
            *In addition to their intended use for gamma correction, transfer functions may be used to produce a variety of special, device-dependent effects.
            *Because transfer functions produce device-dependent effects, a page description that is intended to be device-independent shall not alter them.
            *
            *When the current colour space is DeviceGray and the output device’s native colour space is DeviceCMYK, a conforming reader shall use only the gray transfer function. 
            *The normal conversion from DeviceGray to DeviceCMYK produces 0.0 for the cyan, magenta, and yellow components. 
            *These components shall not be passed through their respective transfer functions but are rendered directly, producing output containing no coloured inks. 
            *This special case exists for compatibility with existing conforming readers that use a transfer function to obtain special effects on monochrome devices, and shall apply only to colours specified in the DeviceGray colour space.
            *
            *NOTE 3    See 11.7.5, "Rendering Parameters and Transparency" and, in particular, 11.7.5.2, "Halftone and Transfer Function" for further discussion of the role of transfer functions in the transparent imaging model.
            */
        }

        //10.5 Halftones
        public class Half_Tones
        {
            /*10.5.1 General
            *
            *Halftoning is a process by which continuous-tone colours are approximated on an output device that can achieve only a limited number of discrete colours.
            *Colours that the device cannot produce directly are simulated by using patterns of pixels in the colours available.
            *
            *NOTE 1    Perhaps the most familiar example is the rendering of gray tones with black and white pixels, as in a newspaper photograph.
            *
            *Some output devices can reproduce continuous-tone colours directly.
            *Halftoning is not required for such devices; after gamma correction by the transfer functions, the colour components shall be transmitted directly to the device.
            *On devices that do require halftoning, it shall occur after all colour components have been transformed by the applicable transfer functions.
            *The input to the halftone function shall consist of continuous-tone, gamma-corrected colour components in the device’s native colour space. 
            *Its output shall consist of pixels in colours the device can reproduce.
            *
            *PDF provides a high degree of control over details of the halftoning process.
            *
            *NOTE 2    When rendering on low-resolution displays, fine control over halftone patterns is needed to achieve the best approximations of gray levels or colours and to minimize visual artifacts.
            *
            *NOTE 3    In colour printing, independent halftone screens can be specified for each of several colorants.
            *
            *NOTE 4    Remember that everything pertaining to halftones is, by definition, device-dependent.
            *          In general, when a PDF file provides its own halftone specifications, it sacrifices portability.
            *          Associated with every output device is a default halftone definition that is appropriate for most purposes. 
            *          Only relatively sophisticated files need to define their own halftones to achieve special effects. 
            *          For correct results, a PDF file that defines a new halftone depends on certain assumptions about the resolution and orientation of device space.
            *          The best choice of halftone parameters often depends on specific physical properties of the output device, such as pixel shape, overlap between pixels, and the effects of electronic or mechanical noise.
            *
            *All halftones are defined in device space, and shall be unaffected by the current transformation matrix.
            */

            /*10.5.2 Halftone Screens
            *
                *In general, halftoning methods are based on the notion of a halftone screen, which divides the array of device pixels into cells that may be modified to produce the desired halftone effects. 
                *A screen is defined by conceptually laying a uniform rectangular grid over the device pixel array. 
                *Each pixel belongs to one cell of the grid; a single cell typically contains many pixels. 
                *The screen grid shall be defined entirely in device space and shall be unaffected by modifications to the current transformation matrix.
                *
                *NOTE      This property is essential to ensure that adjacent areas coloured by halftones are properly stitched together without visible seams.
                *
                *On a bilevel(black - and - white) device, each cell of a screen may be made to approximate a shade of gray by painting some of the cell’s pixels black and some white. 
                *Numerically, the gray level produced within a cell shall be the ratio of white pixels to the total number of pixels in the cell. 
                *A cell containing n pixels can render n +1 different gray levels, ranging from all pixels black to all pixels white.A gray value g in the range 0.0 to 1.0 shall be produced by making i pixels white, where i = floor(g × n).
                *
                *The foregoing description also applies to colour output devices whose pixels consist of primary colours that are either completely on or completely off.
                *Most colour printers, but not colour displays, work this way.Halftoning shall be applied to each colour component independently, producing shades of that colour.
                *
                *Colour components shall be presented to the halftoning machinery in additive form, regardless of whether they were originally specified additively(RGB or gray) or subtractively(CMYK or tint). 
                *Larger values of a colour component represent lighter colours—greater intensity in an additive device such as a display or less ink in a subtractive device such as a printer.
                *Transfer functions produce colour values in additive form; see 10.4, "Transfer Functions".
            */

            /*10.5.3 Spot Functions
            *
                *A common way of defining a halftone screen is by specifying a frequency, angle, and spot function. 
                *The frequency indicates the number of halftone cells per inch; the angle indicates the orientation of the grid lines relative to the device coordinate system. 
                *As a cell’s desired gray level varies from black to white, individual pixels within the cell change from black to white in a well-defined sequence: if a particular gray level includes certain white pixels, lighter grays will include the same white pixels along with some additional ones. 
                *The order in which pixels change from black to white for increasing gray levels shall be determined by a spot function, which specifies that order in an indirect way that minimizes interactions with the screen frequency and angle.
                *
                *Consider a halftone cell to have its own coordinate system: the centre of the cell is the origin and the corners are at coordinates ±1.0 horizontally and vertically.
                *Each pixel in the cell is centred at horizontal and vertical coordinates that both lie in the range −1.0 to + 1.0.
                *For each pixel, the spot function shall be invoked with the pixel’s coordinates as input and shall return a single number in the range −1.0 to + 1.0, defining the pixel’s position in the whitening order.
                *
                *The specific values the spot function returns are not significant; all that matters are the relative values returned for different pixels. 
                *As a cell’s gray level varies from black to white, the first pixel whitened shall be the one for which the spot function returns the lowest value, the next pixel shall be the one with the next higher spot function value, and so on. 
                *If two pixels have the same spot function value, their relative order shall be chosen arbitrarily.
                *
                *PDF provides built -in definitions for many of the most commonly used spot functions that a conforming reader shall implement.A halftone may simply specify any of these predefined spot functions by name instead of giving an explicit function definition.
                *
                *EXAMPLE       The name SimpleDot designates a spot function whose value is inversely related to a pixel’s distance from the center of the halftone cell.
                *              This produces a “dot screen” in which the black pixels are clustered within a circle whose area is inversely proportional to the gray level. 
                *              The name Line designates a spot function whose value is the distance from a given pixel to a line through the center of the cell, producing a “line screen” in which the white pixels grow away from that line.
                *
                *Table 128 shows the predefined spot functions.
                *The table gives the mathematical definition of each function along with the corresponding PostScript language code as it would be defined in a PostScript calculator function (see 7.10.5, "Type 4 (PostScript Calculator) Functions"). 
                *The image accompanying each function shows how the relative values of the function are distributed over the halftone cell, indicating the approximate order in which pixels are whitened.Pixels corresponding to darker points in the image are whitened later than those corresponding to lighter points.
                *
                *Table 128 - Predefined spot functions
                *
                *          [Name]              [Appearance]                        [Definition]
                *
                *          SimpleDot           (see Table on page 303)             1 - (x^2 + y^2)
                *                                                                  { dup mul exch dup mul add 1 exch sub }
                *
                *          InvertedSimpleDot   (see Table on page 303)             x^2 + y^2 - 1
                *                                                                  { dup mul exch dup mul add 1 sub }
                *
                *          DoubleDot           (see Table on page 303)             sin(360 x X)/2 + sin(360 x Y)/2
                *                                                                  {360 mul sin 2 div exch 360 mul sin 2 div add }
                *
                *          InvertedDoubleDot   (see Table on page 303)             - ( sin(360 x X)/2 + sin(360 x Y)/2 )
                *                                                                  { 360 mul sin 2 div exch 360 mul sin 2 div add neg }
                *
                *          CosineDot           (see Table on page 303)             cos(180 x X)/2 + cos(180 x Y)/2
                *                                                                  { 180 mul cos exch 180 mul cos add 2 div }
                *
                *          Double              (see Table on page 303)             sin(360 x X/2)/2 + sin(360 x Y)/2
                *                                                                  { 360 mul sin 2 div exch 2 div 360 mul sin 2 div add }
                *
                *          InvertedDouble      (see Table on page 303)             - ( sin(360 x X/2)/2 + sin(360 x Y)/2 )
                *                                                                  { 360 mul sin 2 div exch 2 div 360 mul sin 2 div add neg }
                *
                *          Line                (see Table on page 303)             -|y|
                *                                                                  { exch pop abs neg }
                *
                *          LineX               (see Table on page 303)             x
                *                                                                  { pop }
                *
                *          LineY               (see Table on page 303)             y
                *                                                                  { exch pop }
                *
                *          Round               (see Table on page 303)             if |x| + |y| <= 1 then 1 - (x^2 + y^2)
                *                                                                  else (|x| - 1)^2 + (|y| - 1)^2 - 1
                *                                                                  { abs exch abs
                *                                                                      2 copy add 1 le
                *                                                                          { dup mul exch dup mul add 1 exch sub }
                *                                                                          { 1 sub dup mul exch 1 sub dup mul add 1
                *                                                                          sub }
                *                                                                      ifelse }
                *
                *          Ellipse             (see Table on page 303)             let w = (3 x |x|) + (4 x |y|) - 3
                *                                                                  if w < 0 then 1 - (x^2 + (|y|/0.75)^2 )/4
                *                                                                  else if w > 1 then ( (1-|x|)^2 + ( (1-|y|)/0.75 )^2 )/4 - 1
                *                                                                  else 0.5 - w
                *                                                                  { abs exch abs 2 copy 3 mul exch 4 mul add 3
                *                                                                  sub dup 0 lt
                *                                                                      { pop dup mul exch 0.75 div dup mul add
                *                                                                          4 div 1 exch sub }
                *                                                                      { dup 1 gt
                *                                                                              { pop 1 exch sub dup mul
                *                                                                                  exch 1 exch sub 0.75 div dup
                *                                                                               mul add
                *                                                                                  4 div 1 sub }
                *                                                                              { 0.5 exch sub exch pop exch pop }
                *                                                                          ifelse }
                *                                                                      ifelse }
                *
                *          EllipseA            (see Table on page 303)         1 - (X^2 + 0.9 x Y^2)
                *                                                              { dup mul 0.9 mul exch dup mul add 1 exch sub }
                *
                *          InvertedEllipseA    (see Table on page 303)         X^2 + 0.9 x Y^2 - 1
                *                                                              { dup mul 0.9 mul exch dup mul add 1 sub }
                *
                *          EllipseB            (see Table on page 303)         1 - sqrt( X^2 + 5/8 x Y^2 )
                *                                                              { dup 5 mul 8 div mul exch dup mul exch add sqrt 1 exch sub }
                *
                *          EllipseC            (see Table on page 303)         1 - ( 0.9 x X^2 + Y^2 )
                *                                                              { dup mul exch dup mul 0.9 mul add 1 exch sub }
                *
                *          InvertedEllipseC    (see Table on page 303)         0.9 x X^2 + Y^2 - 1
                *                                                              { dup mul exch dup mul 0.9 mul add 1 sub }
                *
                *          Square              (see Table on page 303)         -max(|x|,|y|)
                *                                                              { abs exch abs 2 copy lt
                *                                                              { exch }
                *                                                              if
                *                                                                  pop neg }
                *
                *          Cross               (see Table on page 303)         -min(|x|,|y|)
                *                                                              { abs exch abs 2 copy gt
                *                                                              { exch }
                *                                                              if
                *                                                                  pop neg }
                *
                *          Rhomboid            (see Table on page 303)         ( 0.9 x |X| + |Y| )/2
                *                                                              { abs exch abs 0.9 mul add 2 div }
                *
                *          Diamond             (see Table on page 303)         if |x| + |y| <= 0.75 then 1 - (x^2 + y^2)
                *                                                              else if |x| + |y| <= 1.23 then 1 - (0.85 x |X| + |Y|)
                *                                                              else (|x| - 1)^2 + (|y| - 1)^2 - 1
                *                                                              { abs exch abs 2 copy add 0.75 le
                *                                                                  { dup mul exch dup mul add 1 exch sub }
                *                                                                  { 2 copy add 1.23 le
                *                                                                      { 0.85 mul add 1 exch sub }
                *                                                                      { 1 sub dup mul exch 1 sub du
                *                                                                      mul add 1 sub }
                *                                                                  ifelse }
                *                                                              ifelse }
                *
                *
                *Figure 49 illustrates the effects of some of the predefined spot functions.
                *
                *(see Figure 49 - Various Haltoning effects, on page 307)
                */

            /*10.5.4 Threshold Arrays
            *
                *Another way to define a halftone screen is with a threshold array that directly controls individual device pixels in a halftone cell. 
                *This technique provides a high degree of control over halftone rendering. 
                *It also permits halftone cells to be arbitrary rectangles, whereas those controlled by a spot function are always square.
                *
                *A threshold array is much like a sampled image—a rectangular array of pixel values—but shall be defined entirely in device space. 
                *Depending on the halftone type, the threshold values occupy 8 or 16 bits each. 
                *Threshold values nominally represent gray levels in the usual way, from 0 for black up to the maximum(255 or 65, 535) for white.
                *The threshold array shall be replicated to tile the entire device space: each pixel in device space shall be mapped to a particular sample in the threshold array.
                *On a bilevel device, where each pixel is either black or white, halftoning with a threshold array shall proceed as follows:
                *
                *  a) For each device pixel that is to be painted with some gray level, consult the corresponding threshold value from the threshold array.
                *
                *  b)  If the requested gray level is less than the threshold value, paint the device pixel black; otherwise, paint it white.
                *      Gray levels in the range 0.0 to 1.0 correspond to threshold values from 0 to the maximum available(255 or 65,535).
                *
                *A threshold value of 0 shall be treated as if it were 1; therefore, a gray level of 0.0 paints all pixels black, regardless of the values in the threshold array.
                *
                *This scheme easily generalizes to monochrome devices with multiple bits per pixel, where each pixel can directly represent intermediate gray levels in addition to black and white.
                *For any device pixel that is specified with some in-between gray level, the halftoning algorithm shall consult the corresponding value in the threshold array to determine whether to use the next-lower or next-higher representable gray level.
                *In this situation, the threshold values do not represent absolute gray levels, but rather gradations between any two adjacent representable gray levels.
                *
                *EXAMPLE       If there are 2 bits per pixel, each pixel can directly represent one of four different gray levels: black, dark gray, light gray, or white, encoded as 0, 1, 2, and 3, respectively.
                *
                *NOTE          A halftone defined in this way can also be used with colour displays that have a limited number of values for each colour component.
                *              The red, green, and blue components are simply treated independently as gray levels, applying the appropriate threshold array to each. 
                *              (This technique also works for a screen defined as a spot function, since the spot function is used to compute a threshold array internally.)
            */

            /*10.5.5 Halftone Dictionaries
            */
                
                /*10.5.5.1 General
                *
                *In PDF 1.2, the graphics state includes a current halftone parameter, which determines the halftoning process that a conforming reader shall use to perform painting operations. 
                *The current halftone may be specified as the value of the HT entry in a graphics state parameter dictionary; see Table 58.It may be defined by either a dictionary or a stream, depending on the type of halftone; the term halftone dictionary is used generically throughout this clause to refer to either a dictionary object or the dictionary portion of a stream object. 
                *(The halftones that are defined by streams are specifically identified as such in the descriptions of particular halftone types; unless otherwise stated, they are understood to be defined by simple dictionaries instead.)
                *
                *Every halftone dictionary shall have a HalftoneType entry whose value shall be an integer specifying the overall type of halftone definition.
                *The remaining entries in the dictionary are interpreted according to this type.PDF supports the halftone types listed in Table 129.
                *
                *Table 129 - PDF halftone types
                *
                *          [Type]              [Meaning]
                *
                *          1                   Defines a single halftone screen by a frequency, angle, and spot function.
                *
                *          5                   Defines an arbitrary number of halftone screens, one for each colorantor colour component (including both primary and spot colorants). 
                *                              The keys in this dictionary are names of colorants; the values are halftone dictionaries of other types, each defining the halftone screen for a single colorant.
                *
                *          6                   Defines a single halftone screen by a threshold array containing 8-bit sample values.
                *
                *          10                  Defines a single halftone screen by a threshold array containing 8-bit sample values, representing a halftone cell that may have a nonzero screen angle.
                *
                *          16                  (PDF 1.3) Defines a single halftone screen by a threshold array containing 16-bit sample values, representing a halftone cell that may have a nonzero screen angle.
                *
                *
                *NOTE 1        The dictionaries representing these halftone types contain the same entries as the corresponding PostScript language halftone dictionaries (as described in Section 7.4 of the PostScript Language Reference, Third Edition), with the following exceptions:
                *
                *              The PDF dictionaries may contain a Type entry with the value Halftone, identifying the type of PDF object that the dictionary describes.
                *              
                *              Spot functions and transfer functions are represented by function objects instead of PostScript procedures.
                *
                *              Threshold arrays are specified as streams instead of files.
                *  
                *              In type 5 halftone dictionaries, the keys for colorants shall be name objects; they may not be strings as they may in PostScript.
                *
                *Halftone dictionaries have an optional entry, HalftoneName, that identifies the halftone by name. 
                *In PDF 1.3, if this entry is present, all other entries, including HalftoneType, are optional. 
                *At rendering time, if the output device has a halftone with the specified name, that halftone shall be used, overriding any other halftone parameters specified in the dictionary.
                *
                *NOTE 2        This provides a way for PDF files to select the proprietary halftones supplied by some device manufacturers, which would not otherwise be accessible because they are not explicitly defined in PDF.
                *
                *If there is no HalftoneName entry, or if the requested halftone name does not exist on the device, the halftone’s parameters shall be defined by the other entries in the dictionary, if any.
                *If no other entries are present, the default halftone shall be used.
                *
                *NOTE 3        See 11.7.5, "Rendering Parameters and Transparency" and, in particular, “Halftone and Transfer Function” in 11.7.5.2 for further discussion of the role of halftones in the transparent imaging model.
                */

                /*10.5.5.2 Type 1 Halftones
                *
                *Table 130 describes the contents of a halftone dictionary of type 1, which defines a halftone screen in terms of its frequency, angle, and spot function.
                *
                *Table 130 - Entries in a type 1 halftone dictionary
                *
                *              [Key]               [Type]                  [Value]
                *
                *              Type                name                    (Optional) The type of PDF object that this dictionary describes; if present, shall be Halftone for a halftone dictionary.
                *
                *              HalftoneType        integer                 (Required) A code identifying the halftone type that this dictionary describes; shall be 1 for this type of halftone.
                *
                *              HalftoneName        byte string             (Optional) The name of the halftone dictionary.
                *
                *              Frequency           number                  (Required) The screen frequency, measured in halftone cells per inch in device space.
                *
                *              Angle               number                  (Required) The screen angle, in degrees of rotation counterclockwise with respect to the device coordinate system.
                *      
                *                                                          NOTE        Most output devices have left - handed device spaces. 
                *                                                                      On such devices, a counterclockwise angle in device space corresponds to a clockwise angle in default user space and on the physical medium.
                *
                *              SpotFunction        function or             (Required) A function object defining the order in which device pixels within a screen cell shall be adjusted for different gray levels, or the name of one of the predefined spot functions (see Table 128).
                *                                  name
                *
                *              AccurateScreens     boolean                 (Optional) A flag specifying whether to invoke a special halftone algorithm that is extremely precise but computationally expensive; see Note 1 for further discussion. Default value: false.
                *
                *              TransferFunction    function or             (Optional) A transfer function, which overrides the current transfer function in the graphics state for the same component. This entry shall be present if the dictionary is a component of a type 5 halftone (see “Type 5 Halftones” in 10.5.5.6) and represents either a nonprimary or nonstandard primary colour component (see 10.4, "Transfer Functions"). 
                *                                  name                    The name Identitymay be used to specify the identity function.
                *
                *
                *If the AccurateScreens entry has a value of true, a highly precise halftoning algorithm shall be substituted in place of the standard one. 
                *If AccurateScreens is false or not present, ordinary halftoning shall be used.
                *
                *NOTE 1        Accurate halftoning achieves the requested screen frequency and angle with very high accuracy, whereas ordinary halftoning adjusts them so that a single screen cell is quantized to device pixels. 
                *              High accuracy is important mainly for making colour separations on high - resolution devices.However, it may be computationally expensive and therefore is ordinarily disabled.
                *
                *NOTE 2        In principle, PDF permits the use of halftone screens with arbitrarily large cells—in other words, arbitrarily low frequencies.However, cells that are very large relative to the device resolution or that are oriented at unfavorable angles may exceed the capacity of available memory.
                *              If this happens, an error occurs.The AccurateScreens feature often requires very large amounts of memory to achieve the highest accuracy.
                *
                *EXAMPLE       The following shows a halftone dictionary for a type 1 halftone.
                *
                *              28 0 obj
                *                  << / Type / Halftone
                *                     / HalftoneType 1
                *                     / Frequency 120
                *                     / Angle 30
                *                     / SpotFunction / CosineDot
                *                     / TransferFunction / Identity
                *                  >>
                *              endobj
                */

                /*10.5.5.3 Type 6 Halftones
                *
                *A type 6 halftone defines a halftone screen with a threshold array.
                *The halftone shall be represented as a stream containing the threshold values; the parameters defining the halftone shall be specified by entries in the stream dictionary.
                *This dictionary may contain the entries shown in Table 131 in addition to the usual entries common to all streams(see Table 5).
                *The Width and Height entries shall specify the dimensions of the threshold array in device pixels; the stream shall contain Width × Height bytes, each representing a single threshold value.
                *Threshold values are defined in device space in the same order as image samples in image space(see Figure 34), with the first value at device coordinates(0, 0) and horizontal coordinates changing faster than vertical coordinates.
                *
                *Table 131 - Additional entries specific to a type 6 halftone dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Halftone for a halftone dictionary.
                *
                *          HalftoneType        integer             (Required) A code identifying the halftone type that this dictionary describes; shall be 6 for this type of halftone.
                *
                *          HalftoneName        byte string         (Optional) The name of the halftone dictionary.
                *
                *          Width               integer             (Required) The width of the threshold array, in device pixels.
                *
                *          Height              integer             (Required) The height of the threshold array, in device pixels.
                *
                *          TransferFunctiion   function or         (Optional) A transfer function, which shall override the current transfer function in the graphics state for the same component.
                *                              name                This entry shall be present if the dictionary is a component of a type 5 halftone (see “Type 5 Halftones” in 10.5.5.6) and represents either a nonprimary or nonstandard primary colour component (see 10.4, "Transfer Functions"). 
                *                                                  The name Identitymay be used to specify the identity function.
                */

                /*10.5.5.4 Type 10 Halftones
                *
                *Type 6 halftones specify a threshold array with a zero screen angle; they make no provision for other angles. 
                *The type 10 halftone removes this restriction and allows the use of threshold arrays for halftones with nonzero screen angles as well.
                *
                *Halftone cells at nonzero angles can be difficult to specify because they may not line up well with scan lines and because it may be difficult to determine where a given sampled point goes.
                *The type 10 halftone addresses these difficulties by dividing the halftone cell into a pair of squares that line up at zero angles with the output device’s pixel grid.
                *The squares contain the same information as the original cell but are much easier to store and manipulate.
                *In addition, they can be mapped easily into the internal representation used for all rendering.
                *
                *NOTE 1    Figure 50 shows a halftone cell with a frequency of 38.4 cells per inch and an angle of 50.2 degrees, represented graphically in device space at a resolution of 300 dots per inch.
                *          Each asterisk in the figure represents a location in device space that is mapped to a specific location in the threshold array.
                *
                *(see Figure 50 - Halftone cell with a nonzero angle, on page 311)
                *
                *NOTE 2    Figure 51 shows how the halftone cell can be divided into two squares. If the squares and the original cell are tiled across device space, the area to the right of the upper square maps exactly into the empty area of the lower square, and vice versa (see Figure 52). 
                *          The last row in the first square is immediately adjacent to the first row in the second square and starts in the same column.
                *
                *(see Figure 51 - Angled halftone cell divided into two squares, on page 312)
                *
                *(see Figure 52 - Halftone cell and two squares tiled across device space, on page 312)
                *
                *NOTE 3    Any halftone cell can be divided in this way. The side of the upper square (X) is equal to the horizontal displacement from a point in one halftone cell to the corresponding point in the adjacent cell, such as those marked by asterisks in Figure 52. 
                *          The side of the lower square (Y) is the vertical displacement between the same two points. 
                *          The frequency of a halftone screen constructed from squares with sides X and Y is thus given by
                *
                *          frequency = resolution / sqrt( x^2 + y^2 )
                *
                *          and the angle by
                *
                *          angle = atan( y/x )
                *
                *Like a type 6 halftone, a type 10 halftone shall be represented as a stream containing the threshold values, with the parameters defining the halftone specified by entries in the stream dictionary. 
                *This dictionary may contain the entries shown in Table 132 in addition to the usual entries common to all streams (see Table 5). 
                *The Xsquare and Ysquare entries replace the type 6 halftone’s Width and Height entries.
                *
                *Table 132 - Additional entries specific to a type 10 halftone dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Halftone for a halftone dictionary.
                *
                *          HalftoneType        integer             (Required) A code identifying the halftone type that this dictionary describes; shall be 10 for this type of halftone.
                *
                *          HalftoneName        byte string         (Optional) The name of the halftone dictionary.
                *
                *          Xsquare             integer             (Required) The side of square X, in device pixels; see below.
                *
                *          Ysquare             integer             (Required) The side of square Y, in device pixels; see below.
                *
                *          TransferFunction    function or         (Optional) A transfer function, which shall override the current transfer function in the graphics state for the same component.
                *                              name                This entry shall be present if the dictionary is a component of a type 5 halftone (see “Type 5 Halftones” in 10.5.5.6) and represents either a nonprimary or nonstandard primary colour component (see 10.4, "Transfer Functions"). 
                *                                                  The name Identity may be used to specify the identity function.
                *
                *The Xsquare and Ysquare entries shall specify the dimensions of the two squares in device pixels. 
                *The stream shall contain Xsquare2 + Ysquare2 bytes, each representing a single threshold value. 
                *The contents of square X shall be specified first, followed by those of square Y. 
                *Threshold values within each square shall be defined in device space in the same order as image samples in image space (see Figure 34), with the first value at device coordinates (0, 0) and horizontal coordinates changing faster than vertical coordinates.
                */

                /*10.5.5.5 Type 16 Halftones
                *
                *Like type 10, a type 16 halftone(PDF 1.3) defines a halftone screen with a threshold array and allows nonzero screen angles.
                *In type 16, however, each element of the threshold array shall be 16 bits wide instead of 8.
                *This allows the threshold array to distinguish 65,536 levels of colour rather than only 256 levels.The threshold array may consist of either one rectangle or two rectangles. 
                *If two rectangles are specified, they shall tile the device space as shown in Figure 53.The last row in the first rectangle shall be immediately adjacent to the first row in the second and shall start in the same column.
                *
                *(see Figure 53 - Tiling of device space in a type 16 halftone, on page 313)
                *
                *A type 16 halftone, like type 6 and type 10, shall be represented as a stream containing the threshold values, with the parameters defining the halftone specified by entries in the stream dictionary. 
                *This dictionary may contain the entries shown in Table 133 in addition to the usual entries common to all streams (see Table 5). 
                *The dictionary’s Width and Height entries define the dimensions of the first (or only) rectangle. 
                *The dimensions of the second, optional rectangle are defined by the optional entries Width2 and Height2. 
                *Each threshold value shall be represented as 2 bytes, with the high-order byte first. 
                *The stream shall contain 2 × Width × Heightbytes if there is only one rectangle or 2 × (Width × Height + Width2 × Height2) bytes if there are two rectangles. 
                *The contents of the first rectangle are specified first, followed by those of the second rectangle. 
                *Threshold values within each rectangle shall be defined in device space in the same order as image samples in image space (see Figure 34), with the first value at device coordinates (0, 0) and horizontal coordinates changing faster than vertical coordinates.
                *
                *Table 133 - Additional entries specific to a type 16 halftone dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Halftone for a halftone dictionary.
                *
                *          HalftoneType        integer             (Required) A code identifying the halftone type that this dictionary describes; shall be 16 for this type of halftone.
                *
                *          HalftoneName        byte string         (Optional) The name of the halftone dictionary.
                *
                *          Width               integer             (Required) The width of the first (or only) rectangle in the threshold array, in device pixels.
                *
                *          Height              integer             (Required) The height of the first (or only) rectangle in the threshold array, in device pixels.
                *
                *          Width2              integer             (Optional) The width of the optional second rectangle in the threshold array, in device pixels. If this entry is present, the Height2 entry shall be present as well. 
                *                                                  If this entry is absent, the Height2 entry shall also be absent, and the threshold array has only one rectangle.
                *
                *          Height2             integer             (Optional) The height of the optional second rectangle in the threshold array, in device pixels.
                *
                *          TransferFunction    function or         (Optional) A transfer function, which shall override the current transfer function in the graphics state for the same component. 
                *                              name                This entry shall be present if the dictionary is a component of a type 5 halftone (see 10.5.5.6, "Type 5 Halftones") and represents either a nonprimary or nonstandard primary colour component (see 10.4, "Transfer Functions"). 
                *                                                  The name Identitymay be used to specify the identity function.
                *
                */

                /*10.5.5.6 Type 5 Halftones
                *
                *Some devices, particularly colour printers, require separate halftones for each individual colorant.
                *Also, devices that can produce named separations may require individual halftones for each separation. 
                *Halftone dictionaries of type 5 allow individual halftones to be specified for an arbitrary number of colorants or colour components.
                *
                *A type 5 halftone dictionary(Table 134) is a composite dictionary containing independent halftone definitions for multiple colorants. 
                *Its keys shall be name objects representing the names of individual colorants or colour components.
                *The values associated with these keys shall be other halftone dictionaries, each defining the halftone screen and transfer function for a single colorant or colour component.
                *The component halftone dictionaries shall not be of halftone type 5.
                *
                *Table 134 - Entries in a type 5 halftone dictionary
                *
                *          [Key]               [Type]              [Value]
                *
                *          Type                name                (Optional) The type of PDF object that this dictionary describes; if present, shall be Halftone for a halftone dictionary.
                *
                *          HalftoneType        number              (Required) A code identifying the halftone type that this dictionary describes; shall be 5 for this type of halftone.
                *
                *          HalftoneName        byte string         (Optional) The name of the halftone dictionary.
                *
                *          any colorant        dictionary          (Required, one per colorant) The halftone corresponding to the colorant or colour component named by the key.
                *          name                or stream           The halftone may be of any type other than 5.
                *  
                *          Default             dictionary          (Required) A halftone to be used for any colorant or colour component that does not have an entry of its own. The value shall not be 5.
                *                              or stream           If there are any nonprimary colorants, the default halftone shall have a transfer function.
                *
                *The colorants or colour components represented in a type 5 halftone dictionary (aside from the Default entry) fall into two categories:
                *
                *  •   Primary colour components for the standard native device colour spaces(Gray for DeviceGray; Red, Green, and Blue for DeviceRGB; Cyan, Magenta, Yellow, and Black for DeviceCMYK;).
                *
                *  •   Nonstandard colour components for use as spot colorants in Separation and DeviceN colour spaces.
                *      Some of these may also be used as process colorants if the native colour space is nonstandard.
                *
                *When a halftone dictionary of some other type appears as the value of an entry in a type 5 halftone dictionary, it shall apply only to the single colorant or colour component named by that entry’s key. 
                *This is in contrast to such a dictionary’s being used as the current halftone parameter in the graphics state, which shall apply to all colour components.
                *If nonprimary colorants are requested when the current halftone is defined by any means other than a type 5 halftone dictionary, the gray halftone screen and transfer function shall be used for all such colorants.
                *
                *EXAMPLE       In this example, the halftone dictionaries for the colour components and for the default all use the same spot function. 
                *              In this example, the halftone dictionaries for the colour components and for the default all use the same spot function.
                *
                *              27 0 obj
                *                  << / Type / Halftone
                *                     / HalftoneType 5
                *                     / Cyan 31 0 R
                *                     / Magenta 32 0 R
                *                     / Yellow 33 0 R
                *                     / Black 34 0 R
                *                    / Default 35 0 R
                *                  >>
                *              endobj
                *
                *              31 0 obj
                *                  << / Type / Halftone
                *                     / HalftoneType 1
                *                     / Frequency 89.827
                *                     / Angle 15
                *                     / SpotFunction / Round
                *                     / AccurateScreens true
                *                  >>
                *              endobj
                *
                *              32 0 obj
                *                  << /Type /Halftone
                *                     / HalftoneType 1
                *                     / Frequency 89.827
                *                     / Angle 75
                *                     / SpotFunction / Round
                *                     / AccurateScreens true
                *                  >>
                *              endobj
                *
                *              33 0 obj
                *                  << / Type / Halftone
                *                     / HalftoneType 1
                *                     / Frequency 90.714
                *                     / Angle 0
                *                     / SpotFunction / Round
                *                     / AccurateScreens true
                *                  >>
                *              endobj
                *
                *              34 0 obj
                *                  << / Type / Halftone
                *                     / HalftoneType 1
                *                     / Frequency 89.803
                *                     / Angle 45
                *                     / SpotFunction / Round
                *                     / AccurateScreens true
                *                  >>
                *              endobj
                *
                *              35 0 obj
                *                  << / Type / Halftone
                *                     / HalftoneType 1
                *                     / Frequency 90.000
                *                     / Angle 45
                *                     / SpotFunction / Round
                *                     / AccurateScreens true
                *                  >>
                *              endobj
                */


        }

        //10.6 Scan Conversion Details
        public class Scan_Conversion_Details
        {
            /*10.6.1 General
            *
            *The final step of rendering shall be scan conversion.
            *The conforming reader executes a scan conversion algorithm to paint graphics, text, and images in the raster memory of the output device.
            *
            *NOTE     The specifics of the scan conversion algorithm are not defined as part of PDF.
            *          Different implementations may perform scan conversion in different ways; techniques that are appropriate for one device may be inappropriate for another.
            *          Still, it is useful to have a general understanding of how scan conversion works, particularly when creating PDF files intended for viewing on a display.
            *          At the low resolutions typical of displays, variations of even one pixel’s width can have a noticeable effect on the appearance of painted shapes.
            *
            *Most scan conversion details are not under program control, but a few are; the parameters for controlling them are described here.
            */
            
            /*10.6.2 Flatness Tolerance
            *
                *The flatness tolerance controls the maximum permitted distance in device pixels between the mathematically correct path and an approximation constructed from straight line segments, as shown in Figure 54. 
                *Flatness may be specified as the operand of the i operator (see Table 57) or as the value of the FL entry in a graphics state parameter dictionary (see Table 58). 
                *It shall be a positive number.
                *
                *NOTE 1    Smaller values yield greater precision at the cost of more computation.
                *
                *NOTE 2    Although the figure exaggerates the difference between the curved and flattened paths for the sake of clarity, the purpose of the flatness tolerance is to control the precision of curve rendering, not to draw inscribed polygons.
                *          If the parameter’s value is large enough to cause visible straight line segments to appear, the result is unpredictable.
                *
                *(see Figure 54 - Flatness tolerance, on page 317)
                */
            
            /*10.6.3 Smoothness Tolerance
            *
                *The smoothness tolerance (PDF 1.3) controls the quality of smooth shading (type 2 patterns and the shoperator) and thus indirectly controls the rendering performance. 
                *Smoothness is the allowable colour error between a shading approximated by piecewise linear interpolation and the true value of a (possibly nonlinear) shading function. 
                *The error shall be measured for each colour component, and the maximum independent error shall be used. The allowable error (or tolerance) shall be expressed as a fraction of the range of the colour component, from 0.0 to 1.0. 
                *Thus, a smoothness tolerance of 0.1 represents a tolerance of 10 percent in each colour component. Smoothness may be specified as the value of the SM entry in a graphics state parameter dictionary (see Table 58).
                *
                *EXAMPLE       Each output device may have internal limits on the maximum and minimum tolerances attainable.
                *              setting smoothness to 1.0 may result in an internal smoothness of 0.5 on a high-quality colour device, while setting it to 0.0 on the same device may result in an internal smoothness of 0.01 if an error of that magnitude is imperceptible on the device.
                *
                *NOTE 1        The smoothness tolerance may also interact with the accuracy of colour conversion. 
                *              In the case of a colour conversion defined by a sampled function, the conversion function is unknown.
                *              Thus the error may be sampled at too low a frequency, in which case the accuracy defined by the smoothness tolerance cannot be guaranteed. 
                *              In most cases, however, where the conversion function is smooth and continuous, the accuracy should be within the specified tolerance.
                *
                *NOTE 2        The effect of the smoothness tolerance is similar to that of the flatness tolerance.However, that flatness is measured in device-dependent units of pixel width, whereas smoothness is measured as a fraction of colour component range.
                */
            
            /*10.6.4 Scan Conversion Rules
            *
                *The following rules determine which device pixels a painting operation affects. 
                *All references to coordinates and pixels are in device space. A shape is a path to be painted with the current colour or with an image. 
                *Its coordinates are mapped into device space but not rounded to device pixel boundaries. At this level, curves have been flattened to sequences of straight lines, and all “insideness” computations have been performed.
                *
                *Pixel boundaries always fall on integer coordinates in device space. 
                *A pixel is a square region identified by the location of its corner with minimum horizontal and vertical coordinates. 
                *The region is half - open, meaning that it includes its lower but not its upper boundaries.
                *More precisely, for any point whose real - number coordinates are (x, y), let i = floor(x) and j = floor(y). 
                *The pixel that contains this point is the one identified as (i, j). 
                *The region belonging to that pixel is defined to be the set of points (x′, y′) such that i ≤ x′ < i + 1 and j ≤ y′ < j + 1. 
                *Like pixels, shapes to be painted by filling and stroking operations are also treated as half-open regions that include the boundaries along their “floor” sides, but not along their “ceiling” sides.
                *
                *A shape shall be scan - converted by painting any pixel whose square region intersects the shape, no matter how small the intersection is.
                *This ensures that no shape ever disappears as a result of unfavourable placement relative to the device pixel grid, as might happen with other possible scan conversion rules.
                *The area covered by painted pixels shall always be at least as large as the area of the original shape.This rule applies both to fill operations and to strokes with nonzero width.Zero - width strokes may be done in an implementation-defined manner that may include fewer pixels than the rule implies.
                *
                *NOTE 1    Normally, the intersection of two regions is defined as the intersection of their interiors. However, for purposes of scan conversion, a filling region is considered to intersect every pixel through which its boundary passes, even if the interior of the filling region is empty.
                *
                *EXAMPLE   A zero - width or zero-height rectangle paints a line 1 pixel wide.
                *
                *The region of device space to be painted by a sampled image is determined similarly to that of a filled shape, though not identically. 
                *The conforming reader transforms the image’s source rectangle into device space and defines a half-open region, just as for fill operations. 
                *However, only those pixels whose centres lie within the region shall be painted.The position of the centre of such a pixel—in other words, the point whose coordinate values have fractional parts of one - half—shall be mapped back into source space to determine how to colour the pixel.
                *There shall not be averaging over the pixel area;
                *
                *NOTE 2    If the resolution of the source image is higher than that of device space, some source samples may not be used.
                *
                *For clipping, the clipping region consists of the set of pixels that would be included by a fill operation. 
                *Subsequent painting operations shall affect a region that is the intersection of the set of pixels defined by the clipping region with the set of pixels for the region to be painted.
                *
                *Scan conversion of character glyphs may be performed by a different algorithm from the preceding one.
                *
                *NOTE 3    That font rendering algorithm uses hints in the glyph descriptions and techniques that are specialized to glyph rasterization.
                */
            
            /*10.6.5 Automatic Stroke Adjustment
            *
                *When a stroke is drawn along a path, the scan conversion algorithm may produce lines of nonuniform thickness because of rasterization effects. 
                *In general, the line width and the coordinates of the endpoints, transformed into device space, are arbitrary real numbers not quantized to device pixels. 
                *A line of a given width can intersect with different numbers of device pixels, depending on where it is positioned. Figure 55 illustrates this effect.
                *
                *For best results, it is important to compensate for the rasterization effects to produce strokes of uniform thickness.
                *This is especially important in low - resolution display applications.
                *To meet this need, PDF 1.2 provides an optional automatic stroke adjustment feature.
                *When stroke adjustment is enabled, the line width and the coordinates of a stroke shall automatically be adjusted as necessary to produce lines of uniform thickness.
                *The thickness shall be as near as possible to the requested line width—no more than half a pixel different.
                *
                *(see Figure 55 - Rasterization without stroke adjustment, on page 319)
                *
                *If stroke adjustment is enabled and the requested line width, transformed into device space, is less than half a pixel, the stroke shall be rendered as a single-pixel line.
                *
                *NOTE      This is the thinnest line that can be rendered at device resolution. 
                *          It is equivalent to the effect produced by setting the line width to 0(see 10.6.4, "Scan Conversion Rules").
                *
                *Because automatic stroke adjustment can have a substantial effect on the appearance of lines, PDF provides means to control whether the adjustment shall be performed. 
                *This may be specified with the stroke adjustment parameter in the graphics state, set by means of the SA entry in a graphics state parameter dictionary(see 8.4.5, "Graphics State Parameter Dictionaries").
                *
                */
            
        }

    }
}
