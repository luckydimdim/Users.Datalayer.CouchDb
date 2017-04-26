namespace Cmas.DataLayers.CouchDb.Users
{
    /// <summary>
    /// Константы БД
    /// </summary>
    internal class DbConsts
    {
        /// <summary>
        /// Имя БД
        /// </summary>
        public const string DbName = "users"; //FIXME: перенести в конфиг

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public const string DbConnectionString = "http://cmas-backend:backend967@cm-ylng-msk-03:5984"; //FIXME: перенести в конфиг

        /// <summary>
        /// Имя дизайн документа
        /// </summary>
        public const string DesignDocumentName = "users";

        /// <summary>
        /// Имя представления всех документов
        /// </summary>
        public const string AllDocsViewName = "all";

        /// <summary>
        /// Имя представления документов, сгрупированных по логину
        /// </summary>
        public const string ByLoginDocsViewName = "byLogin";
    }
}