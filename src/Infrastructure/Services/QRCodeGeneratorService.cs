using Masny.QRAnimal.Application.Interfaces;
using QRCoder;
using System.Drawing;
using System.IO;

namespace Masny.QRAnimal.Infrastructure.Services
{
    /// <summary>
    /// Сервис для формирования QR кода.
    /// </summary>
    public class QRCodeGeneratorService : IQRCodeGeneratorService
    {
        /// <inheritdoc />
        public byte[] CreateQRCode(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var code = BitmapToBytes(qrCodeImage);

            return code;
        }

        private byte[] BitmapToBytes(Bitmap img)
        {
            using MemoryStream stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
