namespace DigitR.Core.Common.Logging
{
    /// <summary>
    /// The static class that provides access to instance of <see cref="Logger"/>
    /// </summary>
    public static class Log
    {
        private static readonly Logger Logger = new Logger();

        /// <summary>
        /// The current instance of <see cref="Logger"/> class.
        /// </summary>
        public static Logger Current
        {
            get
            {
                return Logger;
            }
        }
    }
}
