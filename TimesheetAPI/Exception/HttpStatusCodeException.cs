
namespace TimesheetAPI.Exception
{
    public class HttpStatusCodeException : System.Exception
    {
        /// <summary>
        /// Il codice di stato dell'http 
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Inizializza una nuova eccezione specificando il codice di stato http 
        /// </summary>
        /// <param name="statusCode">
        /// Il codice di stato http 
        /// </param>
        public HttpStatusCodeException(int statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Inizializza una nuova eccezione specificando il codice di stato http e il messaggio d'errore 
        /// </summary>
        /// <param name="statusCode">
        /// Il codice di stato http 
        /// </param>
        /// <param name="errorMessage">
        /// Il messaggio d'errore 
        /// </param>
        public HttpStatusCodeException(int statusCode, string errorMessage) : base(errorMessage)
        {
            StatusCode = statusCode;
        }
    }
}
