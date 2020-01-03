namespace Masny.QRAnimal.Application.Interfaces
{
    /// <summary>
    /// Интерфейс для формирования QR кода.
    /// </summary>
    public interface IQRCodeGeneratorService
    {
        /// <summary>
        /// Создать QR код.
        /// </summary>
        /// <param name="text">Данные для формирования кода.</param>
        /// <returns>QR код.</returns>
        byte[] CreateQRCode(string text);
    }
}
