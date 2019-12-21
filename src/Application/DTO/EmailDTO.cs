namespace Masny.QRAnimal.Application.DTO
{
    /// <summary>
    /// DataTransferObject для Email.
    /// </summary>
    public class EmailDTO
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Confirmation Token.
        /// </summary>
        public string Code { get; set; }
    }
}
