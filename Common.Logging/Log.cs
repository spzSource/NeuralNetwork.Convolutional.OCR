namespace DigitR.Common.Logging
{
    public static class Log
    {
        private static readonly Logger Logger = new Logger();

        public static Logger Current
        {
            get
            {
                return Logger;
            }
        }
    }
}
