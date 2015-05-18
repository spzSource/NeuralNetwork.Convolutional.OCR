using System;
using System.Collections.Generic;
using System.IO;

using Core.Common.Image.Converters;

using DigitR.Core.InputProvider;

namespace DigitR.Core.NeuralNetwork.InputProvider.Training.Mnist
{
    public class MnistImageInputProvider : IInputProvider
    {
        private readonly string labelPath;
        private readonly string sourcePath;
        private readonly Action<object> itemRetrievedCallback;

        private readonly InputLabelConverter labelConverter;
        private readonly ThresholdConverter imageConverter;

        public MnistImageInputProvider(
            string labelPath, 
            string sourcePath,
            Action<object> itemRetrievedCallback)
        {
            this.labelPath = labelPath;
            this.sourcePath = sourcePath;
            this.itemRetrievedCallback = itemRetrievedCallback;

            labelConverter = new InputLabelConverter();
            imageConverter = new ThresholdConverter(20);
        }

        public object Current
        {
            get;
            private set;
        }

        public IEnumerable<object> Retrieve()
        {
            using (FileStream labelsStream = new FileStream(labelPath, FileMode.Open))
            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open))
            using (BinaryReader labelsReader = new BinaryReader(labelsStream))
            using (BinaryReader sourceReader = new BinaryReader(sourceStream))
            {
                MnistHeaderInfo header = ReadHeader(sourceReader, labelsReader);

                for (int imageIndex = 0; imageIndex < header.ImagesCount; imageIndex++)
                {
                    Current = new MnistImagePattern(
                        labelsReader.ReadByte(),
                        sourceReader.ReadBytes(MnistImagePattern.MnistPatternSizeInBytes),
                        labelConverter,
                        imageConverter);

                    itemRetrievedCallback(Current);

                    yield return Current;
                }
            }
        }

        private MnistHeaderInfo ReadHeader(BinaryReader sourceReader, BinaryReader labelsReader)
        {
            sourceReader.BaseStream.Seek(4, SeekOrigin.Begin);

            int imagesCount = ReverseBytes(sourceReader.ReadInt32());
            int rowsCount = ReverseBytes(sourceReader.ReadInt32());
            int columnsCount = ReverseBytes(sourceReader.ReadInt32());

            labelsReader.BaseStream.Seek(4, SeekOrigin.Begin);

            int labelsCount = ReverseBytes(labelsReader.ReadInt32());

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
