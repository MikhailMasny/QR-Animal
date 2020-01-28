using Masny.QRAnimal.Domain.Enums;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Метод расширения для конвертации пола животного.
    /// </summary>
    public static class GenderConvertor
    {
        /// <summary>
        /// Конвертация на основе полученных данных от ViewModel.
        /// </summary>
        /// <param name="genderName">Название пола.</param>
        /// <returns>Конвертированное значение в виде GenderTypes.</returns>
        public static GenderTypes ToLocalType(this string genderName)
        {
            GenderTypes result;

            switch (genderName)
            {
                case nameof(GenderTypes.Male): { result = GenderTypes.Male; } break;

                case nameof(GenderTypes.Female): { result = GenderTypes.Female; } break;

                default: { result = GenderTypes.None; } break;
            }

            return result;
        }
    }
}
