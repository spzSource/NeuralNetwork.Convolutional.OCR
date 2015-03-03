using System;
using System.Collections.Generic;
using System.IO;

using DigitR.Core.InputProvider.Image;

namespace DigitR.Core.InputProvider.Mnist
{
    public class MnistImageInputProvider : IImageInputProvider<byte, byte[]>
    {
        private readonly string labelPath;
        private readonly string sourcePath;

        public MnistImageInputProvider(
            string labelPath,
            string sourcePath)
        {
            if (String.IsNullOrEmpty(labelPath))
            {
                throw new ArgumentNullException("labelPath");
            }
            if (String.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentNullException("sourcePath");
            }

            this.labelPath = labelPath;
            this.sourcePath = sourcePath;
        }

        public IEnumerable<IInputPattern<byte, byte[]>> Retrieve()
        {
            using (FileStream labelsStream = new FileStream(labelPath, FileMode.Open))
            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open))
            using (BinaryReader labelsReader = new BinaryReader(labelsStream))
            using (BinaryReader sourceReader = new BinaryReader(sourceStream))
            {
                const int batchSize = MnistImagePattern.MnistSideSize * MnistImagePattern.MnistSideSize;

                MnistHeaderInfo header = ReadHeader(sourceReader, labelsReader);
                for (int imageIndex = 0; imageIndex < header.ImagesCount; imageIndex++)
                {
                    yield return new MnistImagePattern(
                        labelsReader.ReadByte(), 
                        sourceReader.ReadBytes(batchSize));
                }
            }
        }

        private MnistHeaderInfo ReadHeader(BinaryReader sourceReader, BinaryReader labelsReader)
        {
            sourceReader.BaseStream.Seek(4, SeekOrigin.Begin);

            int imagesCount  = ReverseBytes(sourceReader.ReadInt32());
            int rowsCount    = ReverseBytes(sourceReader.ReadInt32());
            int columnsCount = ReverseBytes(sourceReader.ReadInt32());

            labelsReader.BaseStream.Seek(4, SeekOrigin.Begin);

            int labelsCount  = ReverseBytes(labelsReader.ReadInt32());

            return new MnistHeaderInfo(imagesCount, rowsCount, columnsCount, labelsCount);
        }

        private static int ReverseBytes(int number)
        {
            return (number & 0x000000FF) << 24 
                | (number & 0x0000FF00) << 8 
                | (number & 0x00FF0000) >> 8 
                | ((int)(number & 0xFF000000)) >> 24;
        }
    }
}
