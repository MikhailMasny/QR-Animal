namespace Masny.QRAnimal.Application.Constants
{
    /// <summary>
    /// Класс для констант с ошибками.
    /// </summary>
    public static class ErrorConstants
    {
        /// <summary>
        /// Эл. адрес уже существует.
        /// </summary>
        public const string RegistrationEmailExist = "Email is already existed.";

        /// <summary>
        /// Неверные данные для входа.
        /// </summary>
        public const string LoginIncorrectData = "Incorrect username and / or password";

        /// <summary>
        /// Аккаунт подтвержден.
        /// </summary>
        public const string AccountConfirm = "Confirm your account";

        /// <summary>
        /// Пароль сброшен.
        /// </summary>
        public const string AccountResetPassword = "Reset password";

        /// <summary>
        /// Пользователь не подтвердил свою эл. почту.
        /// </summary>
        public const string UserNotVerifiedEmail = "You have not verified your email.";

        /// <summary>
        /// Пользователь не найден.
        /// </summary>
        public const string UserNotFound = "User is not found.";

        /// <summary>
        /// Ошибка токена.
        /// </summary>
        public const string TokenIssues = "Unexpected token issues..";
    }
}
