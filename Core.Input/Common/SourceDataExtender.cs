namespace DigitR.Core.InputProvider.Common
{
    public class SourceDataExtender
    {
        public static T[] ExtendSource<T>(T[] sourceForExtend, int defaultSize, int desiredSize)
        {
            T[] source = new T[desiredSize * desiredSize];

            for (int rowIndex = 0; rowIndex < desiredSize; rowIndex++)
            {
                if (rowIndex == 0 || rowIndex == desiredSize - 1)
                {
                    for (int columnIndex = 0; columnIndex < desiredSize; columnIndex++)
                    {
                        source[desiredSize * rowIndex + columnIndex] = default(T);
                    }
                }
                else
                {
                    for (int columnIndex = 0; columnIndex < desiredSize; columnIndex++)
                    {
                        if (columnIndex == 0 || columnIndex == desiredSize - 1)
                        {
                            source[desiredSize * rowIndex + columnIndex] = default(T);
                        }
                        else
                        {
                            source[desiredSize * rowIndex + columnIndex] =
                                sourceForExtend[defaultSize * rowIndex + columnIndex];
                        }
                    }
                }
            }

            return source;
        }
    }
}