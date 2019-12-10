using Masny.QRAnimal.Domain.Enums;

namespace Masny.QRAnimal.Web.Extensions
{
    /// <summary>
    /// Метод расширения для конвертации пола животного из ViewModel для DTO.
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
                case "Male": { result = GenderTypes.Male; } break;

                case "Female": { result = GenderTypes.Female; } break;

                default: { result = GenderTypes.None; } break;
            }

            return result;
        }

        /// <summary>
        /// Конвертация на основе полученных данных от DTO.
        /// </summary>
        /// <param name="genderTypes">Тип пола (enum).</param>
        /// <returns>Конвертированное значение в виде string.</returns>
        public static string ToLocalString(this GenderTypes genderTypes)
        {
            string result;

            switch (genderTypes)
            {
                case GenderTypes.Male: { result = "Male"; } break;

                case GenderTypes.Female: { result = "Female"; } break;

                default: { result = "Неизвестно"; } break;
            }

            return result;
        }
    }
}
