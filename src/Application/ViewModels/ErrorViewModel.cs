namespace Masny.QRAnimal.Application.ViewModels
{
    /// <summary>
    /// ViewModel для ошибки.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Id запроса.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Показать Id запроса.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
