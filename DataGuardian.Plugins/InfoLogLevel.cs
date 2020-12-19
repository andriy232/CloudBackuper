namespace DataGuardian.Plugins
{
    public enum InfoLogLevel
    {
        /// <summary>
        /// Technical messages
        /// </summary>
        Information = 1,

        /// <summary>
        /// Errors
        /// </summary>
        Error,

        /// <summary>
        /// Warnings
        /// </summary>
        Warning,

        /// <summary>
        /// Write only to log, do not warn
        /// </summary>
        Debug
    }
}