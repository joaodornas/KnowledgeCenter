using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Ionic.Zip;
using IronOcr;
using IronPdf;

namespace uploadPDF
{
    class Program
    {
        static void Main(string[] args)
        {

            string pdfFile = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_DOCUMENT_DATABASES\RETINA\10.1038-134065a0.pdf";
            //string pdfFile = @"C:\Users\Dornas\Dropbox\__ XX - HARD-QUALE\_KNOWLEDGE-CENTER\_DOCUMENT_DATABASES\RETINA\10.1038-280064a0.pdf";
            //string pdfFile = @"C:\Users\Dornas\Dropbox\__ D - BE-HAPPY\y. HARD-QUALE\_PROJECT (IN)\_KNOWLEDGE-CENTER\_DOC\_ADOBE\pdf_test.pdf";

            var Ocr = new AdvancedOcr()
            {
                CleanBackgroundNoise = false,
                ColorDepth = 4,
                ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                EnhanceContrast = false,
                DetectWhiteTextOnDarkBackgrounds = false,
                RotateAndStraighten = false,
                Language = IronOcr.Languages.English.OcrLanguagePack,
                EnhanceResolution = false,
                InputImageType = AdvancedOcr.InputTypes.Document,
                ReadBarCodes = true,
                Strategy = AdvancedOcr.OcrStrategy.Fast
            };
            var PagesToRead = new[] { 1 };
            var Results = Ocr.ReadPdf(pdfFile, PagesToRead);
            var Pages = Results.Pages;
            var Barcodes = Results.Barcodes;
            var FullPdfText = Results.Text;

            //string fileString = string.Empty;

            //PdfReader pdfReader = new PdfReader(pdfFile);

            ////PdfReaderContentParser pdfParser = new PdfReaderContentParser(pdfReader);

            //int nPages = pdfReader.NumberOfPages;

            //var strategy = new MyLocationTextExtractionStrategy();

            //for (int iPage = 1; iPage <= nPages; iPage++)
            //{
            //    var ex = PdfTextExtractor.GetTextFromPage(pdfReader, iPage, strategy);

            //    foreach (var p in strategy.myPoints)
            //    {
            //        fileString = fileString + p.Text;
            //    }

            //}

            int x = 0;
               
            // EXTRACT TEXT
            
            //List<string> organizedText = new List<string>();
            //List<int> organizedTextPages = new List<int>();

            //getOrganizedText(ref organizedText, ref organizedTextPages, ref pdfReader);

            //string HTML = string.Empty;

            //formatTextAsHTML(ref organizedText, ref organizedTextPages, ref HTML);


            //File.WriteAllText(@"C:\Users\Dornas\Dropbox\__ D - BE-HAPPY\y. HARD-QUALE\_PROJECT (IN)\_KNOWLEDGE-CENTER\_DOC\_ADOBE\SanityCheck\Text.html", HTML);


            //// EXTRACT IMAGES

            //List<RectAndImage> myImages = new List<RectAndImage>();

            //getOrganizedImages(ref myImages, ref pdfReader, ref pdfParser);

            //for (int i = 0; i < myImages.Count; i++)
            //{
            //    //myImages[i].image.Save(@"C:\Users\Dornas\Dropbox\__ D - BE-HAPPY\y. HARD-QUALE\_PROJECT (IN)\_KNOWLEDGE-CENTER\_DOC\_ADOBE\SanityCheck\" + Convert.ToString(i) + "." + myImages[i].fileExtension, myImages[i].format);

            //    File.WriteAllBytes(@"C:\Users\Dornas\Dropbox\__ D - BE-HAPPY\y. HARD-QUALE\_PROJECT (IN)\_KNOWLEDGE-CENTER\_DOC\_ADOBE\SanityCheck\" + Convert.ToString(i) + "." + myImages[i].fileExtension, myImages[i].image);
            //}

            //int x = 0;

        }

        //public static void getOrganizedText(ref List<string> organizedText, ref List<int> organizedTextPages, ref PdfReader pdfReader)
        //{
        //    int nPages = pdfReader.NumberOfPages;

        //    float[] X = new float[2];
        //    float[] Y = new float[2];
        //    float[] Top = new float[2];
        //    float[] Height = new float[2];
        //    float[] Width = new float[2];

        //    Boolean firstText = true;

        //    float Top_threshold = 2;
        //    float Y_threshold = 2;

        //    for (int iPage = 1; iPage <= nPages; iPage++)
        //    {
        //        var strategy = new MyLocationTextExtractionStrategy();

        //        var ex = PdfTextExtractor.GetTextFromPage(pdfReader, iPage, strategy);

        //        foreach (var p in strategy.myPoints)
        //        {

        //            if (firstText)
        //            {
        //                organizedText.Add(p.Text);
        //                organizedTextPages.Add(iPage);

        //                firstText = false;

        //                Y[1] = p.Rect.Bottom;
        //                X[1] = p.Rect.Left;
        //                Top[1] = p.Rect.Top;
        //                Height[1] = p.Rect.Height;
        //                Width[1] = p.Rect.Width;

        //                Y[0] = Y[1];
        //                X[0] = X[1] - strategy.myPoints[2].Rect.Left - X[1];
        //                Top[0] = Top[1];
        //                Height[0] = Height[1];
        //                Width[0] = Width[1];
        //            }
        //            else
        //            {
        //                string space = string.Empty;

        //                Y_threshold = 2 * p.Rect.Height;

        //                if ((Math.Abs(p.Rect.Bottom - Y[1]) > Y_threshold) && (Math.Abs(p.Rect.Top - Top[1]) > Top_threshold) && (Height[1] <= 2 * p.Rect.Height))
        //                {
        //                    organizedText.Add(p.Text);
        //                    organizedTextPages.Add(iPage);
        //                }
        //                else if (p.Rect.Height > 2 * Height[1])
        //                {
        //                    organizedText.Add(p.Text);
        //                    organizedTextPages.Add(iPage);
        //                }
        //                else if (Height[1] > 2 * p.Rect.Height)
        //                {
        //                    space = " ";

        //                    organizedText[organizedText.Count - 1] = organizedText[organizedText.Count - 1] + space + p.Text;
        //                }
        //                else
        //                {
        //                    space = " ";

        //                    organizedText[organizedText.Count - 1] = organizedText[organizedText.Count - 1] + space + p.Text;
        //                }

        //                Y[0] = Y[1];
        //                X[0] = X[1];
        //                Top[0] = Top[1];
        //                Height[0] = Height[1];
        //                Width[0] = Width[1];

        //                Y[1] = p.Rect.Bottom;
        //                X[1] = p.Rect.Left;
        //                Top[1] = p.Rect.Top;
        //                Height[1] = p.Rect.Height;
        //                Width[1] = p.Rect.Width;

        //            }

        //        }

        //    }
        //}

        //public static void getOrganizedImages(ref List<RectAndImage> myImages, ref PdfReader pdfReader, ref PdfReaderContentParser pdfParser)
        //{
        //    int nPages = pdfReader.NumberOfPages;

        //    for (int iPage = 1; iPage <= nPages; iPage++)
        //    {

        //        MyImageRenderListener listener = new MyImageRenderListener();

        //        pdfParser.ProcessContent(iPage, listener);

        //        myImages.AddRange(listener.myImages);

        //    }
        //}

        //public static void formatTextAsHTML(ref List<string> organizedText, ref List<int> organizedTextPages, ref string HTML)
        //{
           
        //    for (int i = 0; i < organizedText.Count; i++)
        //    {
        //        HTML = HTML + "<p " + "id=" + Convert.ToString(i) + " page=" + Convert.ToString(organizedTextPages[i]) + ">" + organizedText[i] + "</p>";
        //        HTML = HTML + "<p></p>";
        //    }
        //}

        //public class RectAndText
        //{
        //    public iTextSharp.text.Rectangle Rect;
        //    public String Text;
        //    public RectAndText(iTextSharp.text.Rectangle rect, String text)
        //    {
        //        this.Rect = rect;
        //        this.Text = text;
        //    }
        //}

        //public class MyLocationTextExtractionStrategy:LocationTextExtractionStrategy
        //{
        //    //Hold each coordinate
        //    public List<RectAndText> myPoints = new List<RectAndText>();

        //    //Automatically called for each chunk of text in the PDF
        //    public override void RenderText(iTextSharp.text.pdf.parser.TextRenderInfo renderInfo)
        //    {
        //        base.RenderText(renderInfo);

        //        //Get the bounding box for the chunk of text
        //        var bottomLeft = renderInfo.GetDescentLine().GetStartPoint();
        //        var topRight = renderInfo.GetAscentLine().GetEndPoint();

        //        //Create a rectangle from it
        //        var rect = new iTextSharp.text.Rectangle(
        //                                                bottomLeft[iTextSharp.text.pdf.parser.Vector.I1],
        //                                                bottomLeft[iTextSharp.text.pdf.parser.Vector.I2],
        //                                                topRight[iTextSharp.text.pdf.parser.Vector.I1],
        //                                                topRight[iTextSharp.text.pdf.parser.Vector.I2]
        //                                                );

        //        //Add this to our main collection
        //        this.myPoints.Add(new RectAndText(rect, renderInfo.GetText()));
        //    }
        //}

        //public class RectAndImage
        //{
        //    public byte[] image;
        //    public string name;
        //    public float x;
        //    public float y;
        //    public ImageFormat format;
        //    public string fileExtension;

        //    public RectAndImage(byte[] image, string name, float x, float y, ImageFormat format, string fileExtension)
        //    {
        //        this.image = image;
        //        this.name = name;
        //        this.x = x;
        //        this.y = y;
        //        this.format = format;
        //        this.fileExtension = fileExtension;
        //    }
        //}

        //public class MyImageRenderListener : IRenderListener
        //{

        //    public List<RectAndImage> myImages = new List<RectAndImage>();
            
        //    public void RenderImage(ImageRenderInfo renderInfo)
        //    {
                
        //        PdfImageObject imageObj = renderInfo.GetImage();
                
        //        Matrix imagePosition = renderInfo.GetImageCTM();

        //        byte[] image = imageObj.GetImageAsBytes();

        //        string fileExtension = imageObj.GetImageBytesType().FileExtension;

        //        ImageFormat format = ImageFormat.Jpeg;

        //        if (String.Compare(fileExtension,"jpg") == 0)
        //        {
        //            format = ImageFormat.Jpeg;
        //        }
        //        else if ((String.Compare(fileExtension, "bmp") == 0))
        //        {
        //            format = ImageFormat.Bmp;
        //        }
        //        else if ((String.Compare(fileExtension, "tif") == 0))
        //        {
        //            format = ImageFormat.Tiff;
        //        }
        //        else if ((String.Compare(fileExtension, "png") == 0))
        //        {
        //            format = ImageFormat.Png;
        //        }
                
        //        string name = Convert.ToString(this.myImages.Count());

        //        float x = imagePosition[Matrix.I31];
        //        float y = imagePosition[Matrix.I32];

        //        this.myImages.Add(new RectAndImage(image, name, x, y, format, fileExtension));
                
                
        //    }

        //    public void BeginTextBlock()
        //    {

        //    }

        //    public void RenderText(TextRenderInfo renderInfo)
        //    {

        //    }

        //    public void EndTextBlock()
        //    {

        //    }
        //}


    }

}

