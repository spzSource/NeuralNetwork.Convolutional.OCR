using DigitR.Core.InputProvider.Image;

namespace DigitR.Core.InputProvider.Mnist
{
    public class MnistImagePattern : IImagePattern<byte, byte[]>
    {
        public const int MnistSideSize = 28;

        private readonly byte label;
        private readonly byte[] source;

        public MnistImagePattern(
            byte label,
            byte[] source)
        {
            this.label = label;
            this.source = source;
        }

        public int Height
        {
            get
            {
                return MnistSideSize;
            }
        }

        public int Weight
        {
            get
            {
                return MnistSideSize;
            }
        }

        public byte Label
        {
            get
            {
                return label;
            }
        }

        public byte[] Source
        {
            get
            {
                return source;
            }
        }
    }
}
