namespace DigitR.Core.InputProvider.Mnist
{
    internal class MnistHeaderInfo
    {
        public int ImagesCount
        {
            get;
            private set;
        }

        public int RowsCount
        {
            get;
            private set;
        }

        public int ColumnsCount
        {
            get;
            private set;
        }

        public int LabelsCount
        {
            get;
            private set;
        }

        public MnistHeaderInfo(
            int imagesCount, 
            int rowsCount, 
            int columnsCount, 
            int labelsCount)
        {
            ImagesCount = imagesCount;
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            LabelsCount = labelsCount;
        }
    }
}
