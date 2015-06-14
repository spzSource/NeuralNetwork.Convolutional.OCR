using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Aspose.OCR;

using DigitR.Core.InputProvider;

namespace DigitR.NeuralNetwork.InputProvider.Processing.Ocr
{
    public class OcrInputProvider : IInputProvider
    {
        private readonly string filePath;
        private readonly Bitmap sourceBitmap;

        private object current;

        public OcrInputProvider(
            string filePath)
        {
            this.filePath = filePath;
            sourceBitmap = new Bitmap(filePath);   
        }

        public IEnumerable<object> Retrieve()
        {
            IList<object> result = new List<object>();

            OcrEngine ocrEngine = new OcrEngine
            {
                Image = ImageStream.FromFile(filePath)
            };

            if (ocrEngine.Process())
            {
                foreach (IRecognizedPartInfo recognizedPartInfo in ocrEngine.Text.PartsInfo)
                {
                    IRecognizedTextPartInfo info = (IRecognizedTextPartInfo)recognizedPartInfo;

                    Bitmap itemSource = sourceBitmap.Clone(info.Box, PixelFormat.DontCare);
                    
                    using(MemoryStream stream = new MemoryStream())
                    {
                        itemSource.Save(stream, ImageFormat.MemoryBmp);
                        current = new OcrInputPattern(stream.ToArray());
                        result.Add(current);
                    }
                }
            }

            return result;
        }

        public object Current
        {
            get
            {
                return current;
            }
        }
    }
}
